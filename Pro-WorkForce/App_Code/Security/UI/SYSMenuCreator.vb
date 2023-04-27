
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports SmartV.UTILITIES
Imports SmartV.DB
Imports System.Collections.Generic
Imports SmartV.Security



Namespace SmartV.Security.MENU
    Public Class SYSMenuCreator

#Region "Class Variables"

        Dim lang As Integer

#End Region

#Region "Methods"

        Private Shared Function getPriviligedForms(ByVal roleID As Integer, ByVal moduleID As Integer) As List(Of SECForm)
            Dim dac As DAC = dac.getDAC
            Dim dt As DataTable
            Dim returnList As New List(Of SECForm)
            Dim sqlparam1 As New SqlParameter("@ModuleID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, moduleID)
            Dim sqlparam2 As New SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, roleID)

            dt = dac.GetDataTable("SYS_GetPrivForms", sqlparam1, sqlparam2)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim f As New SECForm
                    f.formID = dr.Item(0)
                    f.parentID = dr.Item(1)
                    f.arabicName = dr.Item(2)
                    f.englishName = dr.Item(3)
                    f.FormURL = dr.Item(4)
                    f.sequance = dr.Item(5)
                    f.imageURL = dr.Item(6)
                    f.show = dr.Item(7)
                    f.description = dr.Item(8)
                    f.moduleID = dr.Item(9)
                    returnList.Add(f)
                Next
            End If
            Return returnList
        End Function

        Private Shared Function getPriviligedForms_sys(ByVal roleID As Integer, ByVal moduleID As Integer, Optional ByVal langID As Integer = 0) As List(Of SECForm)
            Dim dac As DAC = dac.getDAC
            Dim dt As DataTable
            Dim AISMn As AISMenu = Nothing
            'Dim f As New SECForm
            Dim returnList As New List(Of SECForm)
            Dim sqlparam1 As New SqlParameter("@ModuleID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, moduleID)
            Dim sqlparam2 As New SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, roleID)
            Dim sqlparam3 As New SqlParameter("@langID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, langID)
            dt = dac.GetDataTable("SYS_GetPrivForms", sqlparam1, sqlparam2, sqlparam3)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim f As New SECForm
                    f.FormURL = dr.Item(4)
                    f.englishName = dr.Item(3)
                    f.arabicName = dr.Item(3)
                    returnList.Add(f)
                Next

            End If
            Return returnList
        End Function

        Public Shared Function getPriviligedModules(ByVal roleID As Integer) As List(Of SECModule)
            Dim dac As DAC = dac.getDAC
            Dim dt As DataTable
            Dim returnList As New List(Of SECModule)
            Dim sqlparam1 As New SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, roleID)
            dt = dac.GetDataTable("SEC_GetRoleModules", sqlparam1)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim m As New SECModule
                    m.moduleID = dr.Item(0)
                    m.moduleArabicName = dr.Item(1)
                    m.moduleEnglishName = dr.Item(2)
                    m.UpperImage = dr.Item(3)
                    m.LowerImage = dr.Item(4)
                    m.defaultPage = dr.Item(5)
                    returnList.Add(m)
                Next
            End If
            Return returnList
        End Function

        Public Shared Sub getFormNighbours(ByVal forms As List(Of SECForm), ByVal currForm As SECForm)
            For Each f As SECForm In forms
                If (f.formID = currForm.formID) Then
                    Continue For
                End If
                If (f.parentID = currForm.parentID) And (f.sequance > currForm.sequance) Then
                    If currForm.nextMenu Is Nothing Then
                        currForm.nextMenu = f
                    Else
                        If CType(currForm.nextMenu, SECForm).sequance > f.sequance Then
                            currForm.nextMenu = f
                        End If
                    End If
                End If
                If f.parentID = currForm.formID Then
                    If currForm.subMenu Is Nothing Then
                        currForm.subMenu = f
                    Else
                        If CType(currForm.subMenu, SECForm).sequance > f.sequance Then
                            currForm.subMenu = f
                        End If
                    End If
                End If
            Next
        End Sub

        Public Function BuildMenu(ByVal groupID As Integer, ByVal ModuleID As Integer, ByVal ParentID As Integer, ByVal Type As Integer) As DataTable
            Dim dac As DAC = dac.getDAC
            Dim dt As DataTable
            Dim sp1 As New SqlParameter("@GroupID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, groupID)
            Dim sp2 As New SqlParameter("@ModuleID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ModuleID)
            '
            Dim sp3 As New SqlParameter("@ParentID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ParentID)

            '
            Dim sp4 As New SqlParameter("@type", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Type)

            dt = dac.GetDataTable("Sys_BuildMenu", sp1, sp2, sp3, sp4)

            BuildMenu = dt

        End Function

        Public Function BuildListMenu(ByVal UserId As Integer, ByVal VersionType As String) As DataTable
            Dim dac As DAC = dac.getDAC
            Dim dt As DataTable
            Dim sp1 As New SqlParameter("@UserId", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UserId)
            Dim sp2 As New SqlParameter("@PackageId", SqlDbType.VarChar, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, VersionType)
            dt = dac.GetDataTable("Sys_BuildListMenu", sp1, sp2)

            BuildListMenu = dt

        End Function

        Public Function BuildLeftMenu(ByVal groupID As Integer, ByVal ModuleID As Integer, ByVal ParentID As Integer, ByVal Type As Integer, ByVal PackageId As String) As DataTable
            Dim dac As DAC = dac.getDAC
            Dim dt As DataTable
            Dim sp1 As New SqlParameter("@GroupID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, groupID)
            Dim sp2 As New SqlParameter("@ModuleID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ModuleID)
            '
            Dim sp3 As New SqlParameter("@ParentID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ParentID)

            '
            Dim sp4 As New SqlParameter("@type", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Type)

            Dim sp5 As New SqlParameter("@PackageId", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, PackageId)
            dt = dac.GetDataTable("Sys_GetMenuModules", sp1, sp2, sp3, sp4, sp5)

            BuildLeftMenu = dt

        End Function

        Public Function GetModuleByFormId(ByVal Forms As String) As DataTable
            Dim dac As DAC = dac.getDAC
            Dim dt As DataTable
            If (SessionVariables.CultureInfo = "en-US") Then
                lang = 0
            Else
                lang = 1
            End If
            Dim sp1 As New SqlParameter("@Forms", SqlDbType.VarChar, 5000, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Forms)
            Dim sp2 As New SqlParameter("@lang", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, lang)
            dt = dac.GetDataTable("GetLicenseModulesBy_FormId", sp1, sp2)

            GetModuleByFormId = dt

        End Function

        Public Function BuildAdminMenu(ByVal groupID As Integer, ByVal ModuleID As Integer, ByVal ParentID As Integer, ByVal Type As Integer) As DataTable
            Dim dac As DAC = dac.getDAC
            Dim dt As DataTable
            Dim sp1 As New SqlParameter("@GroupID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, groupID)
            Dim sp2 As New SqlParameter("@ModuleID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ModuleID)
            Dim sp3 As New SqlParameter("@ParentID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, ParentID)

            Dim sp4 As New SqlParameter("@type", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Type)


            dt = dac.GetDataTable("Sys_Build_AdminMenu", sp1, sp2, sp3, sp4)

            BuildAdminMenu = dt

        End Function

        Public Shared Function GetModuleList(ByVal ModuleList As DataTable) As DataTable
            Dim dac As DAC = dac.getDAC
            Dim dt As DataTable
            dt = dac.GetDataTable("Sys_GetModules")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt
            End If
            Return dt
        End Function

        Public Shared Function getMenuString(ByVal roleID As String, ByVal isArabic As Boolean) As String
            Dim Module_List As DataTable = Nothing
            Dim headForm As SECForm = Nothing
            Dim AIMMn As New AISMenu()
            Dim str As StringBuilder = New StringBuilder()
            Dim strs As StringBuilder = New StringBuilder()
            Dim forms As List(Of SECForm)
            Module_List = GetModuleList(Module_List)
            For i As Integer = 0 To Module_List.Rows.Count - 1
                If SessionVariables.CultureInfo = "en-US" Then
                    forms = getPriviligedForms_sys(roleID, CInt(Module_List.Rows(i).Item(0).ToString), 0)
                Else
                    forms = getPriviligedForms_sys(roleID, CInt(Module_List.Rows(i).Item(0).ToString), 1)
                End If

                If (Not forms.Count = 0) Then
                    For Each f As SECForm In forms

                        If isArabic Then
                            f.name = f.arabicName
                        Else
                            f.name = f.englishName
                        End If
                        f.link = f.FormURL
                        If isArabic Then
                            str.Append(AIMMn.getList(f.link, f.name, 1))
                        Else
                            str.Append(AIMMn.getList(f.link, f.name, 0))
                        End If

                        If headForm Is Nothing Then
                            headForm = f
                        Else
                            If headForm.sequance > f.sequance Then
                                headForm = f
                            End If
                        End If
                    Next
                End If
                If SessionVariables.CultureInfo = "en-US" Then
                    strs.Append(AIMMn.getUl(Module_List.Rows(i).Item(1).ToString, str.ToString, 0))
                Else
                    strs.Append(AIMMn.getUl(Module_List.Rows(i).Item(2).ToString, str.ToString, 1))
                End If
                str = New StringBuilder()
            Next
            str = strs
            If isArabic Then
                AISMenu.dir = AISMenu.menusDirection.rtl
            Else
                AISMenu.dir = AISMenu.menusDirection.ltr
            End If
            Return headForm.getMenuString_sys(strs.ToString)
        End Function

        Public Shared Function getMenuStringVertical(ByVal roleID As String, ByVal moduleID As String, ByVal isArabic As Boolean) As String
            Dim forms As List(Of SECForm) = getPriviligedForms(roleID, moduleID)
            Dim headForm As SECForm = Nothing
            If (Not forms.Count = 0) Then
                For Each f As SECForm In forms
                    getFormNighbours(forms, f)
                    f.name = f.englishName
                    f.link = f.FormURL
                    If headForm Is Nothing Then
                        headForm = f
                    Else
                        If headForm.sequance > f.sequance Then
                            headForm = f
                        End If
                    End If
                Next
            End If

            If isArabic Then
                AISMenu.dir = AISMenu.menusDirection.rtl
            Else
                AISMenu.dir = AISMenu.menusDirection.ltr
            End If


            If Not headForm Is Nothing Then
                Return headForm.getMenuStringVertical
            Else
                Return String.Empty
            End If


        End Function


        'Public Shared Function IsAccessible(ByVal FormID As String) As Boolean
        '    Try
        '        If Not SessionVariables.LoginUser.AllowedFormsIDs Is Nothing Then
        '            If SessionVariables.LoginUser.AllowedFormsIDs <> "" Then
        '                If IsNumeric(FormID) Then
        '                    Dim strFormID As String
        '                    strFormID = "," & FormID & ","
        '                    If SessionVariables.LoginUser.AllowedFormsIDs.Contains(strFormID) Then
        '                        Return True
        '                    Else
        '                        Return False
        '                    End If
        '                Else
        '                    Return False
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

#End Region

       
    End Class
End Namespace

