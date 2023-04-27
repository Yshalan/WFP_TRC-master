Imports SmartV.UTILITIES
Imports System.Data
Imports SmartV.Security
Imports Telerik.Web.UI

Partial Class UserControls_AdminLeft
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private PageName As String
    Shared SelectedPage As String
    Private lang As String

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
        Dim BuildMenu As New SmartV.Security.MENU.SYSMenuCreator


        Dim ModuleDT As DataTable
        Dim ModuleName As String

        Dim FormsDT As DataTable
        Dim ModuleID As Integer

        Dim SubFormsDT As DataTable
        Dim FormID As Integer

        Dim subFormID As Integer
        Dim AllowedPages As String = "," 'To save allowed Pages for Login User


        '  If Session("MenuBuilt") <> "Filled" Then
        '


        Dim ModuleRadPanelItem_Home As New Telerik.Web.UI.RadPanelItem
        With ModuleRadPanelItem_Home
            If SessionVariables.CultureInfo = "en-US" Then
                .Text = "Home"
            Else
                .Text = "الصفحة الرئيسية"
            End If
            .NavigateUrl = "../Default/Default.aspx"
            .ImageUrl = "../Icons/home.gif"
        End With

        RadPanelBar1.Items.Add(ModuleRadPanelItem_Home)



        'ModuleDT = BuildMenu.BuildMenu(SessionVariables.Sys_LoginUser.GroupId, 0, 0, 1)
        ModuleDT = BuildMenu.BuildMenu(1, 0, 0, 1)
        'Session("ModuleDT") = ModuleDT
        'Else
        'ModuleDT = Session("ModuleDT")
        'End If



        Dim i As Integer
        Dim j, k As Integer
        j = 0
        For Each row In ModuleDT.Rows
            Dim ModuleRadPanelItem As New Telerik.Web.UI.RadPanelItem
            ModuleID = ModuleDT.Rows(j)("ModuleID")
            If SessionVariables.CultureInfo = "en-US" Then
                ModuleName = ModuleDT.Rows(j)("Desc_En")
            Else
                ModuleName = ModuleDT.Rows(j)("Desc_Ar")
            End If
            ModuleRadPanelItem.Text = ModuleName
            If Not ModuleDT.Rows(j)("icon") Is Nothing Then
                ModuleRadPanelItem.ImageUrl = ModuleDT.Rows(j)("icon")
            End If

            FormsDT = BuildMenu.BuildMenu(1, ModuleID, 0, 2)
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
                '***By Mohammad Shanaa 04/12/2010
                FormRadPanelItem.NavigateUrl = FormsDT.Rows(i)("FormPath")
                'Dim strPath As String
                FormID = FormsDT.Rows(i)("FormID")
                'strPath = FormsDT.Rows(i)("FormPath") & "?FormID=" & FormID
                'Dim securl As SecureUrl = New SecureUrl(strPath)
                'FormRadPanelItem.NavigateUrl = securl.ToString()

                If Not AllowedPages.Contains("," & FormID & ",") Then
                    AllowedPages = AllowedPages & FormID & ","
                End If
                '*********************

                SubFormsDT = BuildMenu.BuildMenu(1, ModuleID, FormID, 3)

                k = 0
                If SubFormsDT.Rows.Count > 0 Then

                    For Each row3 In SubFormsDT.Rows

                        Dim SubFormRadPanelItem As New Telerik.Web.UI.RadPanelItem
                        If SessionVariables.CultureInfo = "en-US" Then
                            SubFormRadPanelItem.Text = Trim(SubFormsDT.Rows(k)("Desc_En"))
                        Else
                            SubFormRadPanelItem.Text = Trim(SubFormsDT.Rows(k)("Desc_Ar"))
                        End If

                        '***By Mohammad Shanaa 04/12/2010
                        SubFormRadPanelItem.NavigateUrl = Trim(SubFormsDT.Rows(k)("FormPath"))
                        'Dim strSubPath As String
                        subFormID = SubFormsDT.Rows(k)("FormID")
                        'strSubPath = Trim(SubFormsDT.Rows(k)("FormPath")) & "?FormID=" & subFormID
                        'Dim securlSub As SecureUrl = New SecureUrl(strSubPath)
                        'SubFormRadPanelItem.NavigateUrl = securlSub.ToString()

                        If Not AllowedPages.Contains("," & subFormID & ",") Then
                            AllowedPages = AllowedPages & subFormID & ","
                        End If
                        '*********************

                        FormRadPanelItem.Items.Add(SubFormRadPanelItem)
                        ' FormRadPanelItem.ItemTemplate.InstantiateIn(SubFormRadPanelItem)



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
            '.Attributes.Add("onclick", "javascript:popup();")
            'ModuleRadPanelItem_Password.NavigateUrl = "ChangePassword.aspx"
            .ImageUrl = "../Icons/password.png"
        End With

        RadPanelBar1.Items.Add(ModuleRadPanelItem_Password)

        'SessionVariables.LoginUser.AllowedFormsIDs = AllowedPages


    End Sub

#End Region
    
End Class
