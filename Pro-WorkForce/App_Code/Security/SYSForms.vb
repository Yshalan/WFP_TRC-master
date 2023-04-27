Imports Microsoft.VisualBasic
Imports SmartV.UTILITIES
Imports System.Data
Imports System.Reflection
Imports System.Data.SqlClient
Imports SmartV.DB

Namespace TA.Security
    Public Class SYSForms

#Region "MEMBERS"

        Private _formID As Integer
        Private _formURL As String
        Private _page_AR_Name As String
        Private _page_EN_Name As String
        Private _AddBtnName As String
        Private _EditBtnName As String
        Private _DeleteBtnName As String
        Private _PrintBtnName As String

#End Region

#Region "PROPERTIES"

        Public Property FormID() As Integer
            Get
                Return _formID
            End Get
            Set(ByVal value As Integer)
                _formID = value
            End Set
        End Property

        Public Property FormURL() As String
            Get
                Return _formURL
            End Get
            Set(ByVal value As String)
                _formURL = value
            End Set
        End Property

        Public Property PageArName() As String
            Get
                Return _page_AR_Name
            End Get
            Set(ByVal value As String)
                _page_AR_Name = value
            End Set
        End Property

        Public Property PageENName() As String
            Get
                Return _page_EN_Name
            End Get
            Set(ByVal value As String)
                _page_EN_Name = value
            End Set
        End Property

        Public Property AddBtnName() As String
            Get
                Return _AddBtnName
            End Get
            Set(ByVal value As String)
                _AddBtnName = value
            End Set
        End Property

        Public Property EditBtnName() As String
            Get
                Return _EditBtnName
            End Get
            Set(ByVal value As String)
                _EditBtnName = value
            End Set
        End Property

        Public Property DeleteBtnName() As String
            Get
                Return _DeleteBtnName
            End Get
            Set(ByVal value As String)
                _DeleteBtnName = value
            End Set
        End Property

        Public Property PrintBtnName() As String
            Get
                Return _PrintBtnName
            End Get
            Set(ByVal value As String)
                _PrintBtnName = value
            End Set
        End Property

#End Region

#Region "CONSTRUCTOR"

        Public Sub New()
            _formID = -1
            _formURL = String.Empty
        End Sub

        Public Sub New(ByVal i_formURL As String)
            _formID = -1
            _formURL = i_formURL
        End Sub

#End Region

#Region "METHOD"

        Public Function GetByPK(ByVal FormID As Integer) As DataTable
            Dim objDac As DAC = DAC.getDAC
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("Sys_Forms_Select", New SqlParameter("@FormID", FormID))
            Catch ex As Exception
                objColl = Nothing
                CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            Finally
                objDac = Nothing
            End Try
            Return objColl
        End Function

        Public Function GetAllObj() As DataTable
            Dim objDac As DAC = DAC.getDAC
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("sp_Sec_Get_FormID", New SqlParameter("@URL", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "URL", DataRowVersion.Default, FormURL))
            Catch ex As Exception
                objColl = Nothing
                CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            Finally
                objDac = Nothing
            End Try
            Return objColl
        End Function

        Public Function GetWhereAmNow() As DataTable
            Dim objDac As DAC = DAC.getDAC
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable("sp_Sec_Get_Page", New SqlParameter("@URL", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "URL", DataRowVersion.Default, FormURL))
            Catch ex As Exception
                objColl = Nothing
                CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            Finally
                objDac = Nothing
            End Try
            Return objColl
        End Function

        Public Shared Function IsAccessible(ByVal FormID As String) As Boolean
            Try
                If Not SessionVariables.LoginUser.AllowedFormsIDs Is Nothing Then
                    If SessionVariables.LoginUser.AllowedFormsIDs <> "" Then
                        If FormID.Contains(",") Then
                            Dim strForms() As String
                            strForms = FormID.Split(",")
                            Dim x As String
                            For index As Integer = 0 To strForms.Length - 1
                                x = strForms(index)
                                If IsNumeric(x) Then
                                    Dim strx As String
                                    strx = "," & x & ","
                                    If SessionVariables.LoginUser.AllowedFormsIDs.Contains(strx) Then
                                        Return True
                                    End If
                                End If
                            Next
                            Return False
                        Else
                            If IsNumeric(FormID) Then
                                Dim strFormID As String
                                strFormID = "," & FormID & ","
                                If SessionVariables.LoginUser.AllowedFormsIDs.Contains(strFormID) Then
                                    Return True
                                Else
                                    Return False
                                End If
                            Else
                                Return False
                            End If
                        End If
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        'Public Shared Function IsOnlineAccessible(ByVal FormID As String) As Boolean
        '    Try
        '        If Not SessionVariables.INT_LoginUser.AllowedFormsIDs Is Nothing Then
        '            If SessionVariables.INT_LoginUser.AllowedFormsIDs <> "" Then
        '                If FormID.Contains(",") Then
        '                    Dim strForms() As String
        '                    strForms = FormID.Split(",")
        '                    Dim x As String
        '                    For index As Integer = 0 To strForms.Length - 1
        '                        x = strForms(index)
        '                        If IsNumeric(x) Then
        '                            Dim strx As String
        '                            strx = "," & x & ","
        '                            If SessionVariables.INT_LoginUser.AllowedFormsIDs.Contains(strx) Then
        '                                Return True
        '                            End If
        '                        End If
        '                    Next
        '                    Return False
        '                Else
        '                    If IsNumeric(FormID) Then
        '                        Dim strFormID As String
        '                        strFormID = "," & FormID & ","
        '                        If SessionVariables.INT_LoginUser.AllowedFormsIDs.Contains(strFormID) Then
        '                            Return True
        '                        Else
        '                            Return False
        '                        End If
        '                    Else
        '                        Return False
        '                    End If
        '                End If
        '            Else
        '                Return False
        '            End If
        '        Else
        '            Return False
        '        End If
        '    Catch ex As Exception
        '        Return False
        '    End Try
        'End Function

        Public Shared Function IsAdminAccessible(ByVal FormID As String) As Boolean
            Try
                If Not SessionVariables.AdminLoginUser.AllowedFormsIDs Is Nothing Then
                    If SessionVariables.AdminLoginUser.AllowedFormsIDs <> "" Then
                        If FormID.Contains(",") Then
                            Dim strForms() As String
                            strForms = FormID.Split(",")
                            Dim x As String
                            For index As Integer = 0 To strForms.Length - 1
                                x = strForms(index)
                                If IsNumeric(x) Then
                                    Dim strx As String
                                    strx = "," & x & ","
                                    If SessionVariables.AdminLoginUser.AllowedFormsIDs.Contains(strx) Then
                                        Return True
                                    End If
                                End If
                            Next
                            Return False
                        Else
                            If IsNumeric(FormID) Then
                                Dim strFormID As String
                                strFormID = "," & FormID & ","
                                If SessionVariables.AdminLoginUser.AllowedFormsIDs.Contains(strFormID) Then
                                    Return True
                                Else
                                    Return False
                                End If
                            Else
                                Return False
                            End If
                        End If
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function GetFormIDByFormPath(ByVal FormPath As String, ByVal GroupID As Integer) As DataTable

            Dim objDac As DAC = DAC.getDAC
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("Get_FormId_ByFormPath", New SqlParameter("@FormPath", FormPath), New SqlParameter("@GroupID", GroupID))

            Catch ex As Exception

            End Try

            Return objColl

        End Function

        Public Function GetFormIDByFormPath_WithoutGroupId(ByVal FormPath As String) As DataTable

            Dim objDac As DAC = DAC.getDAC
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("Get_FormId_ByFormPath_WithoutGroupId", New SqlParameter("@FormPath", FormPath))

            Catch ex As Exception

            End Try

            Return objColl

        End Function

        Public Function GetFormsByModuleID(ByVal ModuleID As Integer) As DataTable

            Dim objDac As DAC = DAC.getDAC
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("GetByModuleID", New SqlParameter("@ModuleID", ModuleID))

            Catch ex As Exception

            End Try

            Return objColl

        End Function

        Public Function GetFormsByParentID(ByVal parentID As Integer, ByVal groupID As Integer) As DataTable

            Dim objDac As DAC = DAC.getDAC
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("GetByParentID", New SqlParameter("@ParentID", parentID), New SqlParameter("@GroupID", groupID))

            Catch ex As Exception

            End Try

            Return objColl

        End Function

        Public Function GetFormsByParentIDManager(ByVal FormID As Integer, ByVal GroupId As Integer, ByVal EmployeeID As Integer) As DataTable

            Dim objDac As DAC = DAC.getDAC
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("GetByParentIDManager", New SqlParameter("@EmployeeID", EmployeeID), New SqlParameter("@ParentID", FormID), New SqlParameter("@GroupID", GroupId))

            Catch ex As Exception

            End Try

            Return objColl

        End Function

        Public Function GetAllPackages() As DataTable

            Dim objDac As DAC = DAC.getDAC
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("GetAllPackages")

            Catch ex As Exception

            End Try

            Return objColl

        End Function

        Public Function GetFormsByPackageID(ByVal packageid As Integer) As DataTable

            Dim objDac As DAC = DAC.getDAC
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("GetFormsByPackageID", New SqlParameter("@packageid", packageid))

            Catch ex As Exception

            End Try

            Return objColl

        End Function

        Public Function GetBy_FormName(ByVal FormName As String) As DataTable

            Dim objDac As DAC = DAC.getDAC
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("Sys_Forms_GetBy_FormName", New SqlParameter("@FormName", FormName))

            Catch ex As Exception

            End Try

            Return objColl

        End Function
#End Region

    End Class
End Namespace