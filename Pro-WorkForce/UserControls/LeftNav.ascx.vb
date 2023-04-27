Imports SmartV.UTILITIES
Imports System.Data
Imports SmartV.Security
Imports SmartV.Version
Imports Telerik.Web.UI

Partial Class UserControls_LeftNav
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private PageName As String
    Shared SelectedPage As String
    Private lang As String
    Private objVersion As version

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                lang = CtlCommon.Lang.AR
            Else
                lang = CtlCommon.Lang.EN
            End If
            'RadPanelBar1.ExpandMode = Telerik.Web.UI.PanelBarExpandMode.SingleExpandedIte

            ''
            RadPanelBar1.ExpandMode = Telerik.Web.UI.PanelBarExpandMode.MultipleExpandedItems
            BuildMenu()

        End If


    End Sub

#End Region

#Region "Methods"

    Public Sub BuildMenu()

        Dim VersionType As Integer
        VersionType = objVersion.GetVersionType()

        Dim BuildMenu As New SmartV.Security.MENU.SYSMenuCreator


        Dim ModuleDT As DataTable
        Dim ModuleName As String

        Dim FormsDT As DataTable
        Dim ModuleID As Integer

        Dim SubFormsDT As DataTable
        Dim FormID As Integer

        Dim subFormID As Integer
        Dim AllowedPages As String = ","
        Dim ModuleRadPanelItem_Home As New Telerik.Web.UI.RadPanelItem
        With ModuleRadPanelItem_Home
            If SessionVariables.CultureInfo = "en-US" Then
                .Text = "Home"
            Else
                .Text = "الصفحة الرئيسية"
            End If
            .NavigateUrl = "../Default/Home.aspx"
            .ImageUrl = "../Icons/home.gif"
        End With

        RadPanelBar1.Items.Add(ModuleRadPanelItem_Home)


        ModuleDT = BuildMenu.BuildMenu(SessionVariables.LoginUser.GroupId, 0, 0, 1)
        Dim rows As DataRow()

        If (SessionVariables.UserModuleId = 1 Or SessionVariables.UserModuleId = 0) Then
            rows = ModuleDT.Select("ModuleId=1")
        ElseIf SessionVariables.UserModuleId = 2 Then
            rows = ModuleDT.Select("ModuleId=2")
        ElseIf SessionVariables.UserModuleId = 3 Then
            rows = ModuleDT.Select("ModuleId=3")
        ElseIf SessionVariables.UserModuleId = 4 Then
            rows = ModuleDT.Select("ModuleId=4")
        ElseIf SessionVariables.UserModuleId = 5 Then
            rows = ModuleDT.Select("ModuleId=5")
        ElseIf SessionVariables.UserModuleId = 6 Then
            rows = ModuleDT.Select("ModuleId=6")
        ElseIf SessionVariables.UserModuleId = 7 Then
            rows = ModuleDT.Select("ModuleId=7")
        ElseIf SessionVariables.UserModuleId = 8 Then
            rows = ModuleDT.Select("ModuleId=8")
        ElseIf SessionVariables.UserModuleId = 9 Then
            rows = ModuleDT.Select("ModuleId=9")
        End If

        Dim i As Integer
        Dim j, k As Integer
        j = 0
        For Each row In rows
            Dim ModuleRadPanelItem As New Telerik.Web.UI.RadPanelItem
            ModuleID = row("ModuleID")
            If SessionVariables.CultureInfo = "en-US" Then
                ModuleName = row("Desc_En")
            Else
                ModuleName = row("Desc_Ar")
            End If
            ModuleRadPanelItem.Text = ModuleName
            If Not row("icon") Is Nothing Then
                ModuleRadPanelItem.ImageUrl = row("icon")
            End If

            FormsDT = BuildMenu.BuildLeftMenu(SessionVariables.LoginUser.GroupId, ModuleID, 0, 2, VersionType)
            i = 0
            For Each row2 In FormsDT.Rows
                Dim FormRadPanelItem As New Telerik.Web.UI.RadPanelItem
                If SessionVariables.CultureInfo = "en-US" Then
                    FormRadPanelItem.Text = FormsDT.Rows(i)("Desc_En")
                Else
                    FormRadPanelItem.Text = FormsDT.Rows(i)("Desc_Ar")
                End If
                If ModuleID = 4 Or (ModuleID = 8 And FormID <> 801) Or (ModuleID = 8 And FormID <> 802) Then
                    FormRadPanelItem.Target = "_blank"
                End If
                FormRadPanelItem.NavigateUrl = FormsDT.Rows(i)("FormPath")
                FormID = FormsDT.Rows(i)("FormID")
                If Not AllowedPages.Contains("," & FormID & ",") Then
                    AllowedPages = AllowedPages & FormID & ","
                End If
                SubFormsDT = BuildMenu.BuildLeftMenu(SessionVariables.LoginUser.GroupId, ModuleID, FormID, 3, VersionType)

                k = 0
                If SubFormsDT.Rows.Count > 0 Then

                    For Each row3 In SubFormsDT.Rows

                        Dim SubFormRadPanelItem As New Telerik.Web.UI.RadPanelItem
                        If SessionVariables.CultureInfo = "en-US" Then
                            SubFormRadPanelItem.Text = Trim(SubFormsDT.Rows(k)("Desc_En"))
                        Else
                            SubFormRadPanelItem.Text = Trim(SubFormsDT.Rows(k)("Desc_Ar"))
                        End If

                        SubFormRadPanelItem.NavigateUrl = Trim(SubFormsDT.Rows(k)("FormPath"))
                        subFormID = SubFormsDT.Rows(k)("FormID")
                        If Not AllowedPages.Contains("," & subFormID & ",") Then
                            AllowedPages = AllowedPages & subFormID & ","
                        End If

                        FormRadPanelItem.Items.Add(SubFormRadPanelItem)
                        k = k + 1
                    Next

                End If
                ModuleRadPanelItem.Items.Add(FormRadPanelItem)
                i = i + 1
            Next
            RadPanelBar1.Items.Add(ModuleRadPanelItem)
            j = j + 1

        Next

        Dim ModuleRadPanelItem_Password As New Telerik.Web.UI.RadPanelItem
        With ModuleRadPanelItem_Password
            If SessionVariables.CultureInfo = "en-US" Then
                .Text = "Change Password"
            Else
                .Text = "تغيير كلمة السر"
            End If
            .NavigateUrl = "../Security/ChangePassword.aspx"
            .ImageUrl = "../Icons/password.png"
        End With

        RadPanelBar1.Items.Add(ModuleRadPanelItem_Password)
    End Sub

#End Region

End Class
