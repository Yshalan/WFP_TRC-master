Imports Microsoft.VisualBasic
Imports SmartV.UTILITIES
Imports System.Data
Imports System.Reflection
Imports System.Data.SqlClient
Imports SmartV.DB
Imports TA.Events

Namespace TA.Security
    Public Class SYSUsers
#Region "MEMBERS"
        Private _ID As Integer
        Private _userID As String
        Private _loginName As String
        Private _employeename As String
        Private _password As String
        Private _groupid As Integer
        Private _locid As Integer
        Private _encryptPass As String
        Private _status As Boolean
        Private _encrptPassOld As String
        Private _encrptPassNew As String
        Private _updatetime As Date
        Private _fullName As String
        Private _LocationName As String
        Private _UserType As Integer
        Private _UserStatus As Integer

        Private _AllowedFormsIDs As String

        Private _CountryID As String
        Private _EmbassyID As Integer
        Private _GroupType As String
        Private _printPerv As Boolean
        Private _approvePerv As Boolean
        Private _savePerv As Boolean
        Private _deletePerv As Boolean

        Private _UserEmail As String
        Private _JobDesc As String
        Private _UserPhone As String
        Private _Remarks As String
        Private _Active As Integer
        Private _IsFirstLogin As Integer
        Private _FK_EmployeeId As Integer
        Private _FK_CompanyId As Integer

        Private _DefaultEmaiLang As String = "AR"
        Private _DefaultSMSLang As String = "AR"

        Private _FK_SecurityLevel As Integer

        Private _RegisteredUser As Boolean
        Private _IsMobile As Boolean
        Private _IsSelfService As Boolean
        Private _RegisteredDevice As String
        Private _DeviceType As String
        Private _HasSelfServiceReports As Boolean
        Private _HasMobilePunch As Boolean
        Private _NoOfAllowedMobile As String
        Private _HasMobile As Boolean
        Private _AllowAutoPunch As Boolean

        Private _HasEntity_Privilege As Boolean
        Private _HasLogicalGroup_Privilege As Boolean
        Private _HasWorkLocation_Privilege As Boolean

        Private _DefaultSystemLang As String = "AR"
        Private _CREATED_BY As String
        Private _LAST_UPDATE_BY As String

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

        Public Property UpdateTime() As Date
            Get
                Return _updatetime
            End Get
            Set(ByVal value As Date)
                _updatetime = value
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

        Public Property UsrID() As String
            Get
                Return _userID
            End Get
            Set(ByVal value As String)
                _userID = value
            End Set
        End Property
        Public Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal value As Integer)
                _ID = value
            End Set
        End Property
        Public Property UserType() As Integer
            Get
                Return _UserType
            End Get
            Set(ByVal value As Integer)
                _UserType = value
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
                Return _employeename
            End Get
            Set(ByVal value As String)
                _employeename = value
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

        Public Property GroupId() As Integer
            Get
                Return _groupid
            End Get
            Set(ByVal value As Integer)
                _groupid = value
            End Set
        End Property
        Public Property LocID() As Integer
            Get
                Return _locid
            End Get
            Set(ByVal value As Integer)
                _locid = value
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
        Public Property LocationName() As String
            Get
                Return _LocationName
            End Get
            Set(ByVal value As String)
                _LocationName = value
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
        Public Property ApprovePerv() As Boolean
            Get
                Return _approvePerv
            End Get
            Set(ByVal value As Boolean)
                _approvePerv = value
            End Set
        End Property

        Public Property SavePerv() As Boolean
            Get
                Return _savePerv
            End Get
            Set(ByVal value As Boolean)
                _savePerv = value
            End Set
        End Property
        Public Property DeletePerv() As Boolean
            Get
                Return _deletePerv
            End Get
            Set(ByVal value As Boolean)
                _deletePerv = value
            End Set
        End Property
        Public Property PrintPerv() As Boolean
            Get
                Return _printPerv
            End Get
            Set(ByVal value As Boolean)
                _printPerv = value
            End Set
        End Property

        Public Property UserEmail() As String
            Get
                Return _UserEmail
            End Get
            Set(ByVal value As String)
                _UserEmail = value
            End Set
        End Property
        Public Property JobDesc() As String
            Get
                Return _JobDesc
            End Get
            Set(ByVal value As String)
                _JobDesc = value
            End Set
        End Property
        Public Property UserPhone() As String
            Get
                Return _UserPhone
            End Get
            Set(ByVal value As String)
                _UserPhone = value
            End Set
        End Property
        Public Property Remarks() As String
            Get
                Return _Remarks
            End Get
            Set(ByVal value As String)
                _Remarks = value
            End Set
        End Property
        Public Property Active() As Integer
            Get
                Return _Active
            End Get
            Set(ByVal value As Integer)
                _Active = value
            End Set
        End Property
        Public Property IsFirstLogin() As Integer
            Get
                Return _IsFirstLogin
            End Get
            Set(ByVal value As Integer)
                _IsFirstLogin = value
            End Set
        End Property

        Public Property FK_EmployeeId() As Integer
            Get
                Return _FK_EmployeeId
            End Get
            Set(ByVal value As Integer)
                _FK_EmployeeId = value
            End Set
        End Property

        Public Property FK_CompanyId() As Integer
            Get
                Return _FK_CompanyId
            End Get
            Set(ByVal value As Integer)
                _FK_CompanyId = value
            End Set
        End Property

        Public Property DefaultEmaiLang() As String
            Get
                Return _DefaultEmaiLang
            End Get
            Set(ByVal value As String)
                _DefaultEmaiLang = value
            End Set
        End Property
        Public Property DefaultSMSLang() As String
            Get
                Return _DefaultSMSLang
            End Get
            Set(ByVal value As String)
                _DefaultSMSLang = value
            End Set
        End Property
        'Public Property FK_SecurityLevel() As Integer
        '    Set(ByVal value As Integer)
        '        _FK_SecurityLevel = value
        '    End Set
        '    Get
        '        Return (_FK_SecurityLevel)
        '    End Get
        'End Property
        Public Property UserStatus() As Integer
            Set(ByVal value As Integer)
                _UserStatus = value
            End Set
            Get
                Return (_UserStatus)
            End Get
        End Property

        Public Property RegisteredUser() As Boolean
            Set(ByVal value As Boolean)
                _RegisteredUser = value
            End Set
            Get
                Return (_RegisteredUser)
            End Get
        End Property
        Public Property IsMobile() As Boolean
            Set(ByVal value As Boolean)
                _IsMobile = value
            End Set
            Get
                Return (_IsMobile)
            End Get
        End Property
        Public Property IsSelfService() As Boolean
            Set(ByVal value As Boolean)
                _IsSelfService = value
            End Set
            Get
                Return (_IsSelfService)
            End Get
        End Property
        Public Property RegisteredDevice() As String
            Set(ByVal value As String)
                _RegisteredDevice = value
            End Set
            Get
                Return (_RegisteredDevice)
            End Get
        End Property
        Public Property DeviceType() As String
            Set(ByVal value As String)
                _DeviceType = value
            End Set
            Get
                Return (_DeviceType)
            End Get
        End Property
        Public Property HasSelfServiceReports() As Boolean
            Set(ByVal value As Boolean)
                _HasSelfServiceReports = value
            End Set
            Get
                Return (_HasSelfServiceReports)
            End Get
        End Property
        Public Property HasMobilePunch() As Boolean
            Set(ByVal value As Boolean)
                _HasMobilePunch = value
            End Set
            Get
                Return (_HasMobilePunch)
            End Get
        End Property
        Public Property NoOfAllowedMobile() As String
            Set(ByVal value As String)
                _NoOfAllowedMobile = value
            End Set
            Get
                Return (_NoOfAllowedMobile)
            End Get
        End Property
        Public Property HasMobile() As Boolean
            Set(ByVal value As Boolean)
                _HasMobile = value
            End Set
            Get
                Return (_HasMobile)
            End Get
        End Property
        Public Property AllowAutoPunch() As Boolean
            Set(ByVal value As Boolean)
                _AllowAutoPunch = value
            End Set
            Get
                Return (_AllowAutoPunch)
            End Get
        End Property

        Public Property HasEntity_Privilege() As Boolean
            Set(ByVal value As Boolean)
                _HasEntity_Privilege = value
            End Set
            Get
                Return (_HasEntity_Privilege)
            End Get
        End Property
        Public Property HasLogicalGroup_Privilege() As Boolean
            Set(ByVal value As Boolean)
                _HasLogicalGroup_Privilege = value
            End Set
            Get
                Return (_HasLogicalGroup_Privilege)
            End Get
        End Property
        Public Property HasWorkLocation_Privilege() As Boolean
            Set(ByVal value As Boolean)
                _HasWorkLocation_Privilege = value
            End Set
            Get
                Return (_HasWorkLocation_Privilege)
            End Get
        End Property

        Public Property DefaultSystemLang() As String
            Get
                Return _DefaultSystemLang
            End Get
            Set(ByVal value As String)
                _DefaultSystemLang = value
            End Set
        End Property
        Public Property CREATED_BY() As String
            Get
                Return _CREATED_BY
            End Get
            Set(ByVal value As String)
                _CREATED_BY = value
            End Set
        End Property
        Public Property LAST_UPDATE_BY() As String
            Get
                Return _LAST_UPDATE_BY
            End Get
            Set(ByVal value As String)
                _LAST_UPDATE_BY = value
            End Set
        End Property
#End Region

#Region "CONSTRUCTOR"

        Public Sub New()
            _ID = 0
        End Sub

        Public Sub New(ByVal i_userID As Integer)
            _ID = i_userID
        End Sub


#End Region

#Region "METHOD"

        'Public Function GetAllUsers(Optional ByVal lan As Integer = 1) As DataTable
        '    Dim objDac As DAC = DAC.getDAC
        '    Return objDac.GetDataTable("SEC_GETAllUsers " & lan)
        'End Function

        Public Function GetAllUsers() As DataTable
            Dim objDac As DAC = DAC.getDAC
            Return objDac.GetDataTable("SYS_GETAllUsers")
        End Function

        Public Function select_usrs() As DataTable
            Dim objDac As DAC = DAC.getDAC
            Return objDac.GetDataTable("User_Details_select_forDDL")
        End Function

        Public Function GetUser() As Integer
            Dim objDac As DAC = DAC.getDAC
            Dim errNo As Integer
            Dim dt As DataTable
            Dim sqlparam1 As New SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ID)
            dt = objDac.GetDataTable("SYS_GETUser", sqlparam1)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                With dt.Rows(0)
                    GroupId = DTable.GetValue(.Item("GroupID"), "I")
                    'LocID = .Item("LocationID")
                    Password = DTable.GetValue(.Item("UserPwd"), "S")
                    UsrID = DTable.GetValue(.Item("UserID"), "S")
                    UserType = DTable.GetValue(.Item("UserType"), "I")
                    Active = DTable.GetValue(.Item("Active"), "I")
                    UserEmail = DTable.GetValue(.Item("UserEmail"), "S")
                    UserPhone = DTable.GetValue(.Item("PhoneNo"), "S")
                    JobDesc = DTable.GetValue(.Item("JobDesc"), "S")
                    Remarks = DTable.GetValue(.Item("Remarks"), "S")
                    fullName = DTable.GetValue(.Item("User_FullName"), "S")
                    FK_EmployeeId = DTable.GetValue(.Item("FK_EmployeeId"), "I")
                    FK_CompanyId = DTable.GetValue(.Item("FK_CompanyId"), "I")
                    ''
                    DefaultEmaiLang = DTable.GetValue(.Item("DefaultEmaiLang"), "S")
                    DefaultSMSLang = DTable.GetValue(.Item("DefaultSMSLang"), "S")
                    UserStatus = DTable.GetValue(.Item("UserStatus"), "I")
                    RegisteredUser = DTable.GetValue(.Item("RegisteredUser"), "C")
                    IsMobile = DTable.GetValue(.Item("IsMobile"), "C")
                    IsSelfService = DTable.GetValue(.Item("IsSelfService"), "C")
                    RegisteredDevice = DTable.GetValue(.Item("RegisteredDevice"), "S")
                    DeviceType = DTable.GetValue(.Item("DeviceType"), "S")
                    HasSelfServiceReports = DTable.GetValue(.Item("HasSelfServiceReports"), "C")
                    HasMobilePunch = DTable.GetValue(.Item("HasMobilePunch"), "C")
                    AllowAutoPunch = DTable.GetValue(.Item("AllowAutoPunch"), "C")

                    HasEntity_Privilege = DTable.GetValue(.Item("HasEntity_Privilege"), "C")
                    HasLogicalGroup_Privilege = DTable.GetValue(.Item("HasLogicalGroup_Privilege"), "C")
                    HasWorkLocation_Privilege = DTable.GetValue(.Item("HasWorkLocation_Privilege"), "C")

                    DefaultSystemLang = DTable.GetValue(.Item("DefaultSystemLang"), "S")

                    Dim dtPer As New DataTable
                    Dim _sysGroup As New SYSGroups
                    _sysGroup.GroupId = GroupId
                    dtPer = _sysGroup.GetPrivileges()
                    If DTable.IsValidDataTable(dtPer) Then
                        SavePerv = DTable.GetValue(dtPer.Rows(0).Item(3), "C")
                        DeletePerv = DTable.GetValue(dtPer.Rows(0).Item(5), "C")
                        ApprovePerv = DTable.GetValue(dtPer.Rows(0).Item(4), "C")
                        PrintPerv = DTable.GetValue(dtPer.Rows(0).Item(6), "C")
                    End If

                End With
                errNo = 0
            Else
                errNo = -1
            End If
            Return errNo
        End Function

        Private Function add() As Integer
            Dim objDac As DAC = DAC.getDAC
            Dim Result As Integer = 0
            Dim UserId As New SqlParameter("@id", SqlDbType.Int, 4, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, ID)
            Result = (objDac.AddUpdateDeleteSP("SYS_InsertUser",
                                                UserId,
                                                 New SqlParameter("@UserID", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UsrID),
                                                 New SqlParameter("@UserFullName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, fullName),
                                                 New SqlParameter("@GrpID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, GroupId),
                                                 New SqlParameter("@LoginPassword", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, IIf(String.IsNullOrEmpty(Password), DBNull.Value, Password)),
                                                 New SqlParameter("@Active", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Active),
                                                 New SqlParameter("@UserEmail", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, IIf(String.IsNullOrEmpty(UserEmail), DBNull.Value, UserEmail)),
                                                 New SqlParameter("@JobDesc", SqlDbType.VarChar, 250, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, IIf(String.IsNullOrEmpty(JobDesc), DBNull.Value, JobDesc)),
                                                 New SqlParameter("@UserPhone", SqlDbType.VarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, IIf(String.IsNullOrEmpty(UserPhone), DBNull.Value, UserPhone)),
                                                 New SqlParameter("@Remarks", SqlDbType.VarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, IIf(String.IsNullOrEmpty(Remarks), DBNull.Value, Remarks)),
                                                 New SqlParameter("@FK_EmployeeId", SqlDbType.VarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, FK_EmployeeId),
                                                 New SqlParameter("@DefaultEmaiLang", SqlDbType.Char, 2, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, DefaultEmaiLang),
                                                 New SqlParameter("@DefaultSMSLang", SqlDbType.Char, 2, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, DefaultSMSLang),
                                                 New SqlParameter("@UserType", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UserType),
                                                 New SqlParameter("@UserStatus", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UserStatus),
                                                 New SqlParameter("@RegisteredUser", SqlDbType.Bit, 5, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, RegisteredUser),
                                                 New SqlParameter("@IsMobile", SqlDbType.Bit, 5, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, IsMobile),
                                                 New SqlParameter("@IsSelfService", SqlDbType.Bit, 5, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, IsSelfService),
                                                 New SqlParameter("@RegisteredDevice", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, RegisteredDevice),
                                                 New SqlParameter("@DeviceType", SqlDbType.NVarChar, 250, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, DeviceType),
                                                 New SqlParameter("@HasSelfServiceReports", SqlDbType.Bit, 5, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, HasSelfServiceReports),
                                                 New SqlParameter("@HasMobilePunch", SqlDbType.Bit, 5, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, HasMobilePunch),
                                                 New SqlParameter("@NoOfAllowedMobile", SqlDbType.VarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, NoOfAllowedMobile),
                                                 New SqlParameter("@HasMobile", SqlDbType.Bit, 5, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, HasMobile),
                                                 New SqlParameter("@AllowAutoPunch", SqlDbType.Bit, 5, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, AllowAutoPunch),
                                                 New SqlParameter("@DefaultSystemLang", SqlDbType.Char, 2, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, DefaultSystemLang),
                                                 New SqlParameter("@CREATED_BY", SqlDbType.VarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, CREATED_BY)))
            If (Result <> -13) Then
                If Not IsDBNull(UserId.Value) Then
                    ID = CInt(UserId.Value)
                End If
            End If
            Return Result 'New SqlParameter("@LocID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, LocID), _
        End Function

        Public Function ClearDevice() As Integer
            Dim objDac As DAC = DAC.getDAC
            Return (objDac.AddUpdateDeleteSP("Sys_Users_ClearDevice",
                New SqlParameter("@ID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ID)))
        End Function
        Private Function update() As Integer
            Dim objDac As DAC = DAC.getDAC
            Return (objDac.AddUpdateDeleteSP("SYS_UPDUser",
                                             New SqlParameter("@id", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ID),
                                             New SqlParameter("@UserID", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UsrID),
                                             New SqlParameter("@newPassword", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Password),
                                             New SqlParameter("@GrpID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, GroupId),
                                             New SqlParameter("@UserFullName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, fullName),
                                             New SqlParameter("@Active", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Active),
                                             New SqlParameter("@UserEmail", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UserEmail),
                                             New SqlParameter("@JobDesc", SqlDbType.VarChar, 250, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, JobDesc),
                                             New SqlParameter("@UserPhone", SqlDbType.VarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UserPhone),
                                             New SqlParameter("@Remarks", SqlDbType.VarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Remarks),
                                             New SqlParameter("@FK_EmployeeId", SqlDbType.VarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, FK_EmployeeId),
                                             New SqlParameter("@DefaultEmaiLang", SqlDbType.Char, 2, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, DefaultEmaiLang),
                                             New SqlParameter("@DefaultSMSLang", SqlDbType.Char, 2, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, DefaultSMSLang),
                                             New SqlParameter("@UserType", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UserType),
                                             New SqlParameter("@UserStatus", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UserStatus),
                                             New SqlParameter("@RegisteredUser", SqlDbType.Bit, 5, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, RegisteredUser),
                                             New SqlParameter("@IsMobile", SqlDbType.Bit, 5, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, IsMobile),
                                             New SqlParameter("@IsSelfService", SqlDbType.Bit, 5, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, IsSelfService),
                                             New SqlParameter("@RegisteredDevice", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, RegisteredDevice),
                                             New SqlParameter("@DeviceType", SqlDbType.NVarChar, 250, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, DeviceType),
                                             New SqlParameter("@HasSelfServiceReports", SqlDbType.Bit, 5, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, HasSelfServiceReports),
                                             New SqlParameter("@HasMobilePunch", SqlDbType.Bit, 5, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, HasMobilePunch),
                                             New SqlParameter("@NoOfAllowedMobile", SqlDbType.VarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, NoOfAllowedMobile),
                                             New SqlParameter("@HasMobile", SqlDbType.Bit, 5, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, HasMobile),
                                             New SqlParameter("@AllowAutoPunch", SqlDbType.Bit, 5, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, AllowAutoPunch),
                                             New SqlParameter("@DefaultSystemLang", SqlDbType.Char, 2, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, DefaultSystemLang),
                                             New SqlParameter("@LAST_UPDATE_BY", SqlDbType.VarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, LAST_UPDATE_BY)))

            'New SqlParameter("@LocID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, LocID), _


        End Function
        Private Function addActiveDirectory() As Integer
            Dim objDac As DAC = DAC.getDAC
            Dim Result As Integer = 0
            Dim UserId As New SqlParameter("@id", SqlDbType.Int, 4, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, ID)
            Result = (objDac.AddUpdateDeleteSP("SYS_InsertUserActiveDirectory",
                                                UserId,
                                                 New SqlParameter("@UserID", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UsrID),
                                                 New SqlParameter("@UserFullName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, fullName),
                                                 New SqlParameter("@GrpID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, GroupId),
                                                 New SqlParameter("@LoginPassword", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, IIf(String.IsNullOrEmpty(Password), DBNull.Value, Password)),
                                                 New SqlParameter("@Active", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Active),
                                                 New SqlParameter("@UserEmail", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, IIf(String.IsNullOrEmpty(UserEmail), DBNull.Value, UserEmail)),
                                                 New SqlParameter("@JobDesc", SqlDbType.VarChar, 250, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, IIf(String.IsNullOrEmpty(JobDesc), DBNull.Value, JobDesc)),
                                                 New SqlParameter("@UserPhone", SqlDbType.VarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, IIf(String.IsNullOrEmpty(UserPhone), DBNull.Value, UserPhone)),
                                                 New SqlParameter("@Remarks", SqlDbType.VarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, IIf(String.IsNullOrEmpty(Remarks), DBNull.Value, Remarks)),
                                                 New SqlParameter("@FK_EmployeeId", SqlDbType.VarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, FK_EmployeeId),
                                                 New SqlParameter("@DefaultEmaiLang", SqlDbType.Char, 2, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, DefaultEmaiLang),
                                                 New SqlParameter("@DefaultSMSLang", SqlDbType.Char, 2, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, DefaultSMSLang),
                                                 New SqlParameter("@UserType", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UserType),
                                                 New SqlParameter("@UserStatus", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UserStatus)))
            If (Result <> -13) Then
                If Not IsDBNull(UserId.Value) Then
                    ID = CInt(UserId.Value)
                End If
            End If
            Return Result 'New SqlParameter("@LocID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, LocID), _
        End Function

        Private Function updateActiveDirectory() As Integer
            Dim objDac As DAC = DAC.getDAC
            Return (objDac.AddUpdateDeleteSP("SYS_UPDUserActiveDirectory",
                                             New SqlParameter("@id", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ID),
                                             New SqlParameter("@UserID", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UsrID),
                                             New SqlParameter("@newPassword", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Password),
                                             New SqlParameter("@GrpID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, GroupId),
                                             New SqlParameter("@UserFullName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, fullName),
                                             New SqlParameter("@Active", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Active),
                                             New SqlParameter("@UserEmail", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UserEmail),
                                             New SqlParameter("@JobDesc", SqlDbType.VarChar, 250, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, JobDesc),
                                             New SqlParameter("@UserPhone", SqlDbType.VarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UserPhone),
                                             New SqlParameter("@Remarks", SqlDbType.VarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Remarks),
                                             New SqlParameter("@FK_EmployeeId", SqlDbType.VarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, FK_EmployeeId),
                                             New SqlParameter("@DefaultEmaiLang", SqlDbType.Char, 2, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, DefaultEmaiLang),
                                             New SqlParameter("@DefaultSMSLang", SqlDbType.Char, 2, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, DefaultSMSLang),
                                             New SqlParameter("@UserType", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UserType),
                                             New SqlParameter("@UserStatus", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UserStatus)))

            'New SqlParameter("@LocID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, LocID), _


        End Function
        Public Function UpdateActive_Users() As Integer
            Dim objDac As DAC = DAC.getDAC
            Return (objDac.AddUpdateDeleteSP("Sys_Users_UpdateActive",
                                             New SqlParameter("@Active", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _Active),
                                             New SqlParameter("@FK_EmployeeId", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _FK_EmployeeId)))



        End Function
        Public Function SaveActiveDirectory() As Integer
            If _ID = 0 Then
                Return addActiveDirectory()
            Else
                Return updateActiveDirectory()
            End If
        End Function
        Public Function save() As Integer
            If _ID = 0 Then
                Dim rslt As Integer = add()
                App_EventsLog.Insert_ToEventLog("Add", ID, "SYSUsers", "Define Users")
                Return rslt
            Else
                Dim rslt As Integer = update()
                App_EventsLog.Insert_ToEventLog("update", ID, "SYSUsers", "Define Users")
                Return rslt
            End If
        End Function

        Public Function delete() As Integer
            Dim objDac As DAC = DAC.getDAC
            Return objDac.AddUpdateDeleteSP("SYS_DELUser", New SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "UserID", DataRowVersion.Default, ID))
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
            objColl = objDac.GetDataTable("SYS_GETActiveUsers",
                                         New SqlParameter("@LoginName", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "LoginName", DataRowVersion.Default, UsrID),
                                         New SqlParameter("@LoginPassword", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "LoginPassword", DataRowVersion.Default, Password), pResult)
            flgExpire = CInt(pResult.Value)

            If objColl IsNot Nothing AndAlso objColl.Rows.Count > 0 Then
                With objColl.Rows(0)
                    ID = .Item("ID")
                    UsrID = (.Item("UserID"))
                    GroupId = CInt(.Item("GroupID"))
                    'Status = CInt(.Item("UserStatus"))
                    'LocID = .Item("locationID")
                    fullName = .Item("Full_Name")
                    'LocationName = .Item("locationName")
                    UserType = .Item("UserType")
                    Active = .Item("Active")
                    If Not IsDBNull(.Item("UserEmail")) Then
                        UserEmail = .Item("UserEmail")
                    End If

                    If Not IsDBNull(.Item("PhoneNo")) Then
                        UserPhone = .Item("PhoneNo")
                    End If
                    ''
                    UserStatus = .Item("UserStatus")

                    If Not IsDBNull(.Item("FK_EmployeeId")) Then
                        FK_EmployeeId = .Item("FK_EmployeeId")
                    End If

                    IsFirstLogin = .Item("IsFirstLogin")
                    ''

                    Dim dt As New DataTable
                    Dim _sysGroup As New SYSGroups
                    _sysGroup.GroupId = GroupId
                    dt = _sysGroup.GetPrivileges()
                    If DTable.IsValidDataTable(dt) Then
                        SavePerv = DTable.GetValue(dt.Rows(0).Item(3), "C")
                        DeletePerv = DTable.GetValue(dt.Rows(0).Item(5), "C")
                        ApprovePerv = DTable.GetValue(dt.Rows(0).Item(4), "C")
                        PrintPerv = DTable.GetValue(dt.Rows(0).Item(6), "C")
                    End If

                    'Dim objSecurity As New SYS_Users_Security
                    'objSecurity.FK_UserId = ID
                    'objSecurity.GetByPK()

                    'FK_SecurityLevel = objSecurity.FK_SecurityLevel

                    ''
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
                objColl = objDac.GetDataTable("SYS_GETLoginUser",
                                             New SqlParameter("@LoginName", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "LoginName", DataRowVersion.Default, UsrID),
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

        Public Function ChangeUserPassword() As Integer

            Dim dac As DAC = DAC.getDAC
            Dim errNo As Integer
            Try
                Dim sqlparam1 As New SqlParameter("@ID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ID)
                Dim sqlparam2 As New SqlParameter("@old_pass", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, IIf(EncrptPassOld Is Nothing, DBNull.Value, EncrptPassOld))
                Dim sqlparam3 As New SqlParameter("@new_pass", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, EncrptPassNew)
                Dim sqlparam4 As New SqlParameter("@IsFirstLogin", IsFirstLogin)
                errNo = dac.AddUpdateDeleteSPTrans("Sys_Password_Update", sqlparam1, sqlparam2, sqlparam3, sqlparam4)

            Catch ex As Exception
                errNo = -1
                CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            Finally
                dac = Nothing
            End Try

            Return errNo
        End Function

        Public Function GetUserByGroupId(ByVal GroupID As Integer) As DataTable

            Dim objDac As DAC = DAC.getDAC
            Return objDac.GetDataTable("GetUsersByGroupID", New SqlParameter("@GroupID", GroupID))

        End Function

        Public Function GetAllUSersByEmployeeID(ByVal EmployeeID As Integer) As DataTable
            Dim objDac As DAC = DAC.getDAC
            Return objDac.GetDataTable("Get_All_Users_By_Employee_ID", New SqlParameter("@FK_EmployeeID", EmployeeID))
        End Function

        Public Function GetActive_UsersCount() As DataTable
            Dim objDac As DAC = DAC.getDAC
            Return objDac.GetDataTable("Sys_GetActive_UsersCount", Nothing)
        End Function

        Public Function GetAllByUserName() As DataTable
            Dim objDac As New DAC(DAC.getConnectionString)
            Dim objColl As DataTable

            objColl = objDac.GetDataTable("Sys_Users_GETEmpIdByUserName", _
                                         New SqlParameter("@UserName", _userID))


            If objColl IsNot Nothing AndAlso objColl.Rows.Count > 0 Then
                With objColl.Rows(0)
                    ID = .Item("ID")
                    UsrID = (.Item("UserID"))
                    GroupId = CInt(.Item("GroupID"))
                    fullName = .Item("User_FullName")
                    UserType = .Item("UserType")
                    Active = .Item("Active")
                    If Not IsDBNull(.Item("UserEmail")) Then
                        UserEmail = .Item("UserEmail")
                    End If

                    If Not IsDBNull(.Item("PhoneNo")) Then
                        UserPhone = .Item("PhoneNo")
                    End If

                    If Not IsDBNull(.Item("FK_EmployeeId")) Then
                        FK_EmployeeId = .Item("FK_EmployeeId")
                    End If

                    IsFirstLogin = .Item("IsFirstLogin")
                End With
            End If
            Return objColl
        End Function

        Public Function GetUser_AndGroupCount() As DataTable
            Dim objDac As DAC = DAC.getDAC
            Return objDac.GetDataTable("Sys_GetUserAndGroup_Count", Nothing)
        End Function

#End Region

    End Class

End Namespace
