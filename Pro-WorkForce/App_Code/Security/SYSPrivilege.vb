Imports Microsoft.VisualBasic
Imports SmartV.UTILITIES
Imports System.Data
Imports System.Reflection
Imports System.Data.SqlClient
Imports SmartV.DB

Namespace TA.Security

    Public Class SYSPrivilege

#Region "MEMBERS"

        Private _groupId As Integer
        Private _roleId As Integer
        Private _formId As Integer
        Private _addPerv As Boolean
        Private _printPerv As Boolean
        Private _updatePerv As Boolean
        Private _approvePerv As Boolean
        Private _savePerv As Boolean
        Private _deletePerv As Boolean
        Private _ZoneID As Integer

        Private _PrivDescAr As String
        Private _PrivDescEn As String
        Private _PrivType As String
        Private _Affected As String
        Private _PrevValue As String
        Private _NewValue As String
        Private _Action_User As String

        Private _ListID As Integer
        Private _CountryCode As String

#End Region

#Region "PROPERTIES"

        Public Property RoleId() As Integer
            Get
                Return _roleId
            End Get
            Set(ByVal value As Integer)
                _roleId = value
            End Set
        End Property

        Public Property GroupId() As Integer
            Get
                Return _groupId
            End Get
            Set(ByVal value As Integer)
                _groupId = value
            End Set
        End Property

        Public Property FormId() As Integer
            Get
                Return _formId
            End Get
            Set(ByVal value As Integer)
                _formId = value
            End Set
        End Property

        Public Property AddPerv() As Boolean
            Get
                Return _addPerv
            End Get
            Set(ByVal value As Boolean)
                _addPerv = value
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
        Public Property UpdatePerv() As Boolean
            Get
                Return _updatePerv
            End Get
            Set(ByVal value As Boolean)
                _updatePerv = value
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
        Public Property ZoneID() As Integer
            Get
                Return _ZoneID
            End Get
            Set(ByVal value As Integer)
                _ZoneID = value
            End Set
        End Property

        Public Property PrivDescAr() As String
            Get
                Return _PrivDescAr
            End Get
            Set(ByVal value As String)
                _PrivDescAr = value
            End Set
        End Property
        Public Property PrivDescEn() As String
            Get
                Return _PrivDescEn
            End Get
            Set(ByVal value As String)
                _PrivDescEn = value
            End Set
        End Property
        Public Property PrivType() As String
            Get
                Return _PrivType
            End Get
            Set(ByVal value As String)
                _PrivType = value
            End Set
        End Property
        Public Property Affected() As String
            Get
                Return _Affected
            End Get
            Set(ByVal value As String)
                _Affected = value
            End Set
        End Property
        Public Property PrevValue() As String
            Get
                Return _PrevValue
            End Get
            Set(ByVal value As String)
                _PrevValue = value
            End Set
        End Property
        Public Property NewValue() As String
            Get
                Return _NewValue
            End Get
            Set(ByVal value As String)
                _NewValue = value
            End Set
        End Property
        Public Property Action_User() As String
            Get
                Return _Action_User
            End Get
            Set(ByVal value As String)
                _Action_User = value
            End Set
        End Property

        Public Property ListID() As Integer
            Get
                Return _ListID
            End Get
            Set(ByVal value As Integer)
                _ListID = value
            End Set
        End Property
        Public Property CountryCode() As String
            Get
                Return _CountryCode
            End Get
            Set(ByVal value As String)
                _CountryCode = value
            End Set
        End Property
#End Region

#Region "CONSTRUCTORS"

        Public Sub New()

            _groupId = -1
            _roleId = -1
            _formId = -1

        End Sub

        Public Sub New(ByVal i_roleORFormId As Integer)

            _roleId = i_roleORFormId
            _formId = i_roleORFormId

        End Sub

        Public Sub New(ByVal i_roleId As Integer, ByVal i_formId As Integer)

            _roleId = i_roleId
            _formId = i_formId

        End Sub


#End Region

#Region "METHODS"

        Public Function Add() As Integer
            Dim objDac As DAC = DAC.getDAC
            Dim errNo As Integer

            Dim sqlParam1 As New SqlParameter("@GroupID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, GroupId)
            Dim sqlParam2 As New SqlParameter("@FormID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, FormId)
            Dim sqlParam3 As New SqlParameter("@Save", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Convert.ToInt32(SavePerv))
            Dim sqlParam4 As New SqlParameter("@Approve", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Convert.ToInt32(ApprovePerv))
            Dim sqlParam5 As New SqlParameter("@Delete", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Convert.ToInt32(DeletePerv))
            Dim sqlParam6 As New SqlParameter("@Print", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Convert.ToInt32(PrintPerv))

            errNo = objDac.AddUpdateDeleteSPTrans("Sys_InsPrivileges", sqlParam1, sqlParam2, sqlParam3, sqlParam4, sqlParam5, sqlParam6)

            Return errNo
        End Function

        Public Function AddZone() As Integer
            Dim objDac As DAC = DAC.getDAC
            Dim errNo As Integer

            Dim sqlParam1 As New SqlParameter("@GroupID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, GroupId)
            Dim sqlParam2 As New SqlParameter("@FormID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, FormId)
            Dim sqlParam3 As New SqlParameter("@ZoneID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ZoneID)
            Dim sqlParam4 As New SqlParameter("@Add", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Convert.ToInt32(AddPerv))
            Dim sqlParam5 As New SqlParameter("@Update", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Convert.ToInt32(UpdatePerv))
            Dim sqlParam6 As New SqlParameter("@Delete", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Convert.ToInt32(DeletePerv))
            Dim sqlParam7 As New SqlParameter("@Print", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Convert.ToInt32(PrintPerv))

            errNo = objDac.AddUpdateDeleteSPTrans("Sys_InsZones", sqlParam1, sqlParam2, sqlParam3, sqlParam4, sqlParam5, sqlParam6, sqlParam7)

            Return errNo
        End Function

        Public Function AddCountryTList() As Integer
            Dim objDac As DAC = DAC.getDAC
            Dim errNo As Integer

            Dim sqlParam1 As New SqlParameter("@ListID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ListID)
            Dim sqlParam2 As New SqlParameter("@CountryCode", SqlDbType.VarChar, 15, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, CountryCode)

            errNo = objDac.AddUpdateDeleteSPTrans("Sys_InsCountryList_Privileges", sqlParam1, sqlParam2)

            Return errNo
        End Function

        Public Function Update() As Integer
            Dim objDac As DAC = DAC.getDAC
            Dim errNo As Integer

            Dim sqlParam1 As New SqlParameter("@GroupID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, GroupId)
            Dim sqlParam2 As New SqlParameter("@FormID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, FormId)
            Dim sqlParam3 As New SqlParameter("@AllowSave", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Convert.ToInt32(SavePerv))
            Dim sqlParam4 As New SqlParameter("@AllowApprove", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Convert.ToInt32(ApprovePerv))
            Dim sqlParam5 As New SqlParameter("@AllowDelete", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Convert.ToInt32(DeletePerv))
            Dim sqlParam6 As New SqlParameter("@AllowPrint", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Convert.ToInt32(PrintPerv))
            errNo = objDac.AddUpdateDeleteSPTrans("Sys_UpdPrivilege", sqlParam1, sqlParam2, sqlParam3, sqlParam4, sqlParam5, sqlParam6)

            Return errNo
        End Function

        Public Function UpdateZone() As Integer
            Dim objDac As DAC = DAC.getDAC
            Dim errNo As Integer

            Dim sqlParam1 As New SqlParameter("@GroupID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, GroupId)
            Dim sqlParam2 As New SqlParameter("@FormID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, FormId)
            Dim sqlParam3 As New SqlParameter("@ZoneID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ZoneID)
            Dim sqlParam4 As New SqlParameter("@AllowAdd", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Convert.ToInt32(AddPerv))
            Dim sqlParam5 As New SqlParameter("@AllowUpdate", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Convert.ToInt32(UpdatePerv))
            Dim sqlParam6 As New SqlParameter("@AllowDelete", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Convert.ToInt32(DeletePerv))
            Dim sqlParam7 As New SqlParameter("@AllowPrint", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Convert.ToInt32(PrintPerv))

            errNo = objDac.AddUpdateDeleteSPTrans("Sys_UpdZone", sqlParam1, sqlParam2, sqlParam3, sqlParam4, sqlParam5, sqlParam6, sqlParam7)

            Return errNo
        End Function

        Public Function UpdateCountryTList() As Integer
            Dim objDac As DAC = DAC.getDAC
            Dim errNo As Integer

            Dim sqlParam1 As New SqlParameter("@ListID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ListID)
            Dim sqlParam2 As New SqlParameter("@CountryCode", SqlDbType.VarChar, 15, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, CountryCode)

            errNo = objDac.AddUpdateDeleteSPTrans("Sys_UpdCountryList_Privileges", sqlParam1, sqlParam2)

            Return errNo
        End Function

        Public Function Delete() As Integer
            Dim objDac As DAC = DAC.getDAC
            Return objDac.AddUpdateDeleteSPTrans("Sys_DelPrivilege", New SqlParameter("@GroupID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "groupid", DataRowVersion.Default, GroupId))
        End Function


        Public Function DeleteZones() As Integer
            Dim objDac As DAC = DAC.getDAC
            Dim sqlParam1 As New SqlParameter("@GroupID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, GroupId)
            Dim sqlParam2 As New SqlParameter("@FormID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, FormId)
            Return objDac.AddUpdateDeleteSPTrans("Sys_DelZone", sqlParam1, sqlParam2)
        End Function

        Public Function DeleteCountryList() As Integer
            Dim objDac As DAC = DAC.getDAC
            Return objDac.AddUpdateDeleteSPTrans("Sys_DelCountryList_Privileges", New SqlParameter("@ListID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "ListID", DataRowVersion.Default, ListID))
        End Function

        'Public Function Add() As Integer
        '    Dim objDac As DAC = DAC.getDAC
        '    Dim errNo As Integer
        '    errNo = objDac.AddUpdateDeleteSPTrans("SEC_INSPrivileges", _
        '                                       New SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, RoleId), _
        '                                       New SqlParameter("@FormID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, FormId))

        '    Return errNo
        'End Function

        'Public Function GetParentID(ByVal sqlstr As String) As DataTable
        '    Dim objDac As New DAC(DAC.getConnectionString)
        '    Dim objColl As DataTable
        '    Try
        '        objColl = objDac.GetDataTable(sqlstr)

        '    Catch ex As Exception
        '        objColl = Nothing
        '        CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)
        '    Finally
        '        objDac = Nothing
        '    End Try
        '    Return objColl
        'End Function

        'Public Function Delete() As Integer
        '    Dim objDac As DAC = DAC.getDAC
        '    Return objDac.AddUpdateDeleteSPTrans("SEC_DELPrivilege", New SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "groupid", DataRowVersion.Default, RoleId))
        'End Function

        'Public Function GetWhere(ByVal strSQL As String) As String
        '    Dim objDac As New DAC(DAC.getConnectionString)
        '    Dim strSQL2b As String = ""
        '    Dim objColl As DataTable

        '    Try
        '        objColl = objDac.GetDataTable(strSQL)

        '        If Not objColl Is Nothing AndAlso objColl.Rows.Count > 0 Then
        '            For i As Integer = 0 To objColl.Rows.Count - 1
        '                strSQL2b = strSQL2b & "OR ( parentid = " & objColl.Rows(i).Item("FormID") & " )"
        '            Next
        '        Else
        '            strSQL = String.Empty
        '        End If

        '    Catch ex As Exception
        '        objColl = Nothing
        '        strSQL2b = String.Empty
        '        CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)
        '    Finally
        '        objDac = Nothing
        '    End Try

        '    Return strSQL2b
        'End Function

        'Public Function GetFormsObj(ByVal intModuleID As Integer) As DataTable
        '    Dim objDac As DAC = DAC.getDAC
        '    Dim objColl As DataTable
        '    Try
        '        Dim sqlParam As New SqlParameter("@ModuleID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, intModuleID)
        '        objColl = objDac.GetDataTable("SEC_GETForms", sqlParam)
        '    Catch ex As Exception
        '        objColl = Nothing
        '        CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)
        '    Finally
        '        objDac = Nothing
        '    End Try

        '    Return objColl
        'End Function
        Public Function GetAllForms() As DataTable
            Dim objDac As DAC = DAC.getDAC
            Dim objColl As DataTable
            Try

                objColl = objDac.GetDataTable("SYS_GetAllForms")

            Catch ex As Exception
                objColl = Nothing
                CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            Finally
                objDac = Nothing
            End Try

            Return objColl
        End Function

        Public Function GetFormsObj(Optional ByVal intGroupID As Integer = 0, Optional ByVal lang As CtlCommon.Lang = CtlCommon.Lang.EN) As DataTable
            Dim objDac As DAC = DAC.getDAC
            Dim objColl As DataTable
            Try

                Dim sqlParam1 As New SqlParameter("@ModuleID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, intGroupID)
                Dim sqlParam2 As New SqlParameter("@Lang", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, lang)

                objColl = objDac.GetDataTable("SYS_GetForms", sqlParam1, sqlParam2)

            Catch ex As Exception
                objColl = Nothing
                CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            Finally
                objDac = Nothing
            End Try

            Return objColl
        End Function

        Public Function GetAllFormsObj(ByVal intFormID As Integer, ByVal strsql As String) As DataTable
            Dim objDac As New DAC(DAC.getConnectionString)
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("SELECT FormID FROM Form WHERE (parentid = 1) and (URL<>'Nothing') OR (parentid = 1) and (URL='Nothing') or (FormID = 1) " & strsql)
            Catch ex As Exception
                objColl = Nothing
                CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            Finally
                objDac = Nothing
            End Try

            Return objColl
        End Function

        Public Function GetAllObj(ByVal i_RoleID As Integer) As DataTable
            Dim objDac As DAC = DAC.getDAC
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("sp_Sec_GET_PrivilegeForms", New SqlParameter("@groupid", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "groupid", DataRowVersion.Default, i_RoleID))
            Catch ex As Exception
                objColl = Nothing
                CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            Finally
                objDac = Nothing
            End Try
            Return objColl
        End Function

        Public Function GetFormsPrivileges() As DataTable
            Dim objDac As New DAC(DAC.getConnectionString)
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("sp_Sec_Get_FormsPrivileges", _
                                            New SqlParameter("@groupid", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "groupid", DataRowVersion.Default, RoleId), _
                                            New SqlParameter("@formid", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "formid", DataRowVersion.Default, FormId))
            Catch ex As Exception
                objColl = Nothing
                CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            Finally
                objDac = Nothing
            End Try
            Return objColl
        End Function

        Public Function GetFormsDetails(ByVal RoleID As Integer) As DataTable
            Dim dt As DataTable
            Dim objDac As DAC
            objDac = DAC.getDAC
            Dim sp1 As New SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, RoleID)
            dt = objDac.GetDataTable("SYS_RollForm_Select", sp1)
            Return dt
        End Function

        Public Function GetZonesCount(ByVal FormID As Integer) As Integer
            Dim objDac As DAC
            objDac = DAC.getDAC
            Dim sp1 As New SqlParameter("@FormID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, FormID)
            Return objDac.AddUpdateDeleteSP("SYS_GetZonesCount", sp1)
        End Function

        Public Function GetAllZonesByFormID(ByVal FormID As Integer, ByVal lang As Integer) As DataTable
            Dim objDac As DAC
            objDac = DAC.getDAC
            Dim sp1 As New SqlParameter("@FormID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, FormID)
            Dim sp2 As New SqlParameter("@Lang", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, lang)
            Return objDac.GetDataTable("SYS_GetAllZonesByFormID", sp1, sp2)
        End Function

        Public Function GetAllZonesPrivilagesByFormID(ByVal GroupID As Integer, ByVal FormID As Integer) As DataTable
            Dim objDac As DAC
            objDac = DAC.getDAC
            Dim sp1 As New SqlParameter("@GroupID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, GroupID)
            Dim sp2 As New SqlParameter("@FormID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, FormID)
            Return objDac.GetDataTable("SYS_GetAllZonesPrivilagesByFormID", sp1, sp2)
        End Function
        Public Function SaveHistoryRecord() As Integer
            Dim objDac As DAC = DAC.getDAC
            Dim errNo As Integer
            Dim sqlParam1 As New SqlParameter("@PrivDescAr", PrivDescAr)
            Dim sqlParam2 As New SqlParameter("@PrivDescEn", PrivDescEn)
            Dim sqlParam3 As New SqlParameter("@PrivType", PrivType)
            Dim sqlParam4 As New SqlParameter("@Affected", Affected)
            Dim sqlParam5 As New SqlParameter("@PrevValue", PrevValue)
            Dim sqlParam6 As New SqlParameter("@NewValue", NewValue)
            Dim sqlParam7 As New SqlParameter("@Action_User", SessionVariables.LoginUser.ID)
            errNo = objDac.AddUpdateDeleteSPTrans("Save_Privileges_History", sqlParam1, sqlParam2, sqlParam3, sqlParam4, sqlParam5, sqlParam6, sqlParam7)

            Return errNo
        End Function
#End Region

    End Class
End Namespace