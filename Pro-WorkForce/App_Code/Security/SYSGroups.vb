Imports Microsoft.VisualBasic
Imports SmartV.UTILITIES
Imports System.Data
Imports System.Reflection
Imports System.Data.SqlClient
Imports SmartV.DB
Imports TA.Events

Namespace TA.Security
    Public Class SYSGroups
#Region "MEMBERS"

        Private _GroupId As Integer
        Private _arabicName As String
        Private _englishName As String
        'Private _CountryID As String
        'Private _EmbassyID As Integer
        'Private _GroupType As String
        Private _isList As Integer
        'Private _active As Boolean
        Private _DefaultPage As Integer

#End Region

#Region "PROPERTIES"

        Public Property GroupId() As Integer
            Get
                Return _GroupId
            End Get
            Set(ByVal value As Integer)
                _GroupId = value
            End Set
        End Property

        Public Property EnglishName() As String
            Get
                Return _englishName
            End Get
            Set(ByVal value As String)
                _englishName = value
            End Set
        End Property

        Public Property ArabicName() As String
            Get
                Return _arabicName
            End Get
            Set(ByVal value As String)
                _arabicName = value
            End Set
        End Property

        'Public Property CountryID() As String
        '    Get
        '        Return _CountryID
        '    End Get
        '    Set(ByVal value As String)
        '        _CountryID = value
        '    End Set
        'End Property

        'Public Property EmbassyID() As String
        '    Get
        '        Return _EmbassyID
        '    End Get
        '    Set(ByVal value As String)
        '        _EmbassyID = value
        '    End Set
        'End Property

        'Public Property GroupType() As String
        '    Get
        '        Return _GroupType
        '    End Get
        '    Set(ByVal value As String)
        '        _GroupType = value
        '    End Set
        'End Property

        Public Property isList() As Integer
            Get
                Return _isList
            End Get
            Set(ByVal value As Integer)
                _isList = value
            End Set
        End Property

        'Public Property Active() As Boolean
        '    Get
        '        Return _active
        '    End Get
        '    Set(ByVal value As Boolean)
        '        _active = value
        '    End Set
        'End Property

        Public Property DefaultPage() As Integer
            Get
                Return _DefaultPage
            End Get
            Set(ByVal value As Integer)
                _DefaultPage = value
            End Set
        End Property

#End Region

#Region "CONSTRUCTORS"

        Public Sub New()

            _GroupId = 0

        End Sub

        Public Sub New(ByVal i_usrGroupId As Integer)
            _GroupId = i_usrGroupId
        End Sub

#End Region

#Region "METHODS"

        Public Function Add() As Integer
            Dim dac As DAC = DAC.getDAC
            Dim errNo As Integer


            Dim sqlparam1 As New SqlParameter("@GroupId", SqlDbType.Int, 4, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, GroupId)
            Dim sqlparam2 As New SqlParameter("@ArabicName", SqlDbType.NVarChar, 150, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _arabicName)
            Dim sqlparam3 As New SqlParameter("@EnglishName", SqlDbType.NVarChar, 150, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _englishName)
            Dim sqlparam4 As New SqlParameter("@DefaultPage", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _DefaultPage)
            'Dim sqlparam4 As New SqlParameter("@CountryID", SqlDbType.VarChar, 15, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _CountryID)
            'Dim sqlparam5 As New SqlParameter("@EMB_ID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _EmbassyID)
            'Dim sqlparam6 As New SqlParameter("@GroupType", SqlDbType.VarChar, 15, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _GroupType)
            errNo = dac.AddUpdateDeleteSPTrans("Sys_InsGroup", sqlparam1, sqlparam2, sqlparam3, sqlparam4) ', sqlparam4, sqlparam5, sqlparam6)
            If errNo = 0 Then
                GroupId = sqlparam1.Value
                App_EventsLog.Insert_ToEventLog("Add", GroupId, "SYSGroups", "Define User Groups")
            End If

            Return errNo
        End Function

        Public Function update() As Integer
            Dim objDac As DAC = DAC.getDAC
            Dim errNo As Integer

            Dim sqlparam1 As New SqlParameter("@GroupId", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, GroupId)
            Dim sqlparam2 As New SqlParameter("@ArabicName", SqlDbType.NVarChar, 150, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _arabicName)
            Dim sqlparam3 As New SqlParameter("@EnglishName", SqlDbType.NVarChar, 150, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _englishName)
            Dim sqlparam4 As New SqlParameter("@DefaultPage", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _DefaultPage)
            'Dim sqlparam4 As New SqlParameter("@CountryID", SqlDbType.VarChar, 15, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _CountryID)
            'Dim sqlparam5 As New SqlParameter("@EMB_ID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _EmbassyID)
            'Dim sqlparam6 As New SqlParameter("@GroupType", SqlDbType.VarChar, 15, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _GroupType)
            errNo = objDac.AddUpdateDeleteSPTrans("Sys_UpdGroup", sqlparam1, sqlparam2, sqlparam3, sqlparam4) ', sqlparam4, sqlparam5, sqlparam6)

            If errNo = 0 Then
                App_EventsLog.Insert_ToEventLog("Update", GroupId, "SYSGroups", "Define User Groups")
            End If

            Return errNo
        End Function

        Public Function delete() As Integer
            Dim objDac As DAC = DAC.getDAC
            Dim rslt As Integer = objDac.AddUpdateDeleteSPTrans("Sys_DelGroup", New SqlParameter("@GroupId", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, GroupId))
            App_EventsLog.Insert_ToEventLog("Delete", GroupId, "SYSGroups", "Define User Groups")
            Return rslt
        End Function

        Public Function GetAll(Optional ByVal lang As Integer = -1) As DataTable

            Dim objDac As DAC = DAC.getDAC
            Dim objColl As DataTable
            Dim sqlparam As New SqlParameter("@langID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, lang)
            Try
                objColl = objDac.GetDataTable("Sys_GetGroups", sqlparam)
            Catch ex As Exception

                objColl = Nothing
                CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)

            Finally
                objDac = Nothing
            End Try
            Return objColl
        End Function

        Public Function GetGroup() As Integer
            Dim objDac As DAC = DAC.getDAC
            Dim objColl As DataTable

            Dim sqlparam As New SqlParameter("@GroupId", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _GroupId)

            objColl = objDac.GetDataTable("Sys_GetGroup", sqlparam)

            If objColl IsNot Nothing AndAlso objColl.Rows.Count > 0 Then
                With objColl.Rows(0)
                    _GroupId = .Item("GroupID")
                    _englishName = .Item("Desc_En")
                    _arabicName = .Item("Desc_Ar")
                    _DefaultPage = .Item("DefaultPage")
                    '_CountryID = DTable.GetValue(.Item("CountryID"), "S")
                    '_EmbassyID = DTable.GetValue(.Item("EmbassyID"), "I")
                    '_GroupType = DTable.GetValue(.Item("GroupType"), "S")
                    '_active = .Item(3)
                End With

            End If
            Return 0
        End Function

        Public Function GetPrivileges() As DataTable
            Dim objDac As DAC = DAC.getDAC
            Dim objColl As DataTable

            Dim sqlparam As New SqlParameter("@GroupId", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, GroupId)

            objColl = objDac.GetDataTable("Sys_GetPrivileges", sqlparam)

            Return objColl
        End Function


#End Region
    End Class
End Namespace

