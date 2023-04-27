Imports Microsoft.VisualBasic
Imports SmartV.UTILITIES
Imports System.Data
Imports System.Reflection
Imports System.Data.SqlClient
Imports SmartV.DB
Namespace TA.Security
    Public Class SYSGroupOfCountries
#Region "MEMBERS"

        Private _ListID As Integer
        Private _arabicName As String
        Private _englishName As String
        Private _CountryID As String
        Private _GroupType As String
        'Private _active As Boolean

#End Region

#Region "PROPERTIES"

        Public Property ListID() As Integer
            Get
                Return _ListID
            End Get
            Set(ByVal value As Integer)
                _ListID = value
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

        Public Property CountryID() As String
            Get
                Return _CountryID
            End Get
            Set(ByVal value As String)
                _CountryID = value
            End Set
        End Property

        Public Property GroupType() As String
            Get
                Return _GroupType
            End Get
            Set(ByVal value As String)
                _GroupType = value
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



#End Region

#Region "CONSTRUCTORS"

        Public Sub New()

            _ListID = 0

        End Sub

        Public Sub New(ByVal i_usrListID As Integer)
            _ListID = i_usrListID
        End Sub

#End Region

#Region "METHODS"

        Public Function Add() As Integer
            Dim dac As DAC = dac.getDAC
            Dim errNo As Integer


            Dim sqlparam1 As New SqlParameter("@ListID", SqlDbType.Int, 4, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, ListID)
            Dim sqlparam2 As New SqlParameter("@ArabicName", SqlDbType.NVarChar, 150, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _arabicName)
            Dim sqlparam3 As New SqlParameter("@EnglishName", SqlDbType.NVarChar, 150, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _englishName)
            errNo = dac.AddUpdateDeleteSPTrans("Sys_InsGroupOfCountries", sqlparam1, sqlparam2, sqlparam3)
            If errNo = 0 Then
                ListID = sqlparam1.Value
            End If

            Return errNo
        End Function

        Public Function update() As Integer
            Dim objDac As DAC = DAC.getDAC
            Dim errNo As Integer

            Dim sqlparam1 As New SqlParameter("@ListID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ListID)
            Dim sqlparam2 As New SqlParameter("@ArabicName", SqlDbType.NVarChar, 150, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _arabicName)
            Dim sqlparam3 As New SqlParameter("@EnglishName", SqlDbType.NVarChar, 150, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _englishName)
            errNo = objDac.AddUpdateDeleteSPTrans("Sys_UpdGroupOfCountries", sqlparam1, sqlparam2, sqlparam3)

            Return errNo
        End Function

        Public Function delete() As Integer
            Dim objDac As DAC = DAC.getDAC
            Return objDac.AddUpdateDeleteSPTrans("Sys_DelGroupOfCountries", New SqlParameter("@ListID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ListID))
        End Function

        Public Function GetAll(Optional ByVal lang As Integer = -1) As DataTable

            Dim objDac As DAC = DAC.getDAC
            Dim objColl As DataTable
            Dim sqlparam As New SqlParameter("@langID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, lang)
            Try
                objColl = objDac.GetDataTable("Sys_GetGroupsOfCountries", sqlparam)
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

            Dim sqlparam As New SqlParameter("@ListID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _ListID)

            objColl = objDac.GetDataTable("Sys_GetGroupOfCountries", sqlparam)

            If objColl IsNot Nothing AndAlso objColl.Rows.Count > 0 Then
                With objColl.Rows(0)
                    _ListID = .Item("ListID")
                    _englishName = .Item("Desc_En")
                    _arabicName = .Item("Desc_Ar")
                    '_active = .Item(3)
                End With

            End If
            Return 0
        End Function

        Public Function GetPrivileges() As DataTable
            Dim objDac As DAC = DAC.getDAC
            Dim objColl As DataTable

            Dim sqlparam As New SqlParameter("@ListID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ListID)

            objColl = objDac.GetDataTable("Sys_GetGroupOfCountriesPrivileges", sqlparam)

            Return objColl
        End Function


#End Region
    End Class
End Namespace

