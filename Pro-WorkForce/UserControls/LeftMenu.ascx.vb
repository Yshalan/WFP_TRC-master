Imports System.Data
Imports TA.Employees
Imports TA.Security
Imports SmartV.UTILITIES

Partial Class UserControls_LeftMenu
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private PageName As String
    Shared SelectedPage As String
    Private lang As String
    Private objVersion As Version
    Dim objSys_Modules As SYSModules
    Dim objSys_Forms As SYSForms
    Private objEmployee As Employee
#End Region
    Public Property VersionType() As Integer
        Get
            Return ViewState("VersionType")
        End Get
        Set(ByVal value As Integer)
            ViewState("VersionType") = value
        End Set
    End Property

    Public Property FormsDT() As DataTable
        Get
            Return ViewState("FormsDT")
        End Get
        Set(ByVal value As DataTable)
            ViewState("FormsDT") = value
        End Set
    End Property

#Region "Page Events"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                lang = CtlCommon.Lang.AR
            Else
                lang = CtlCommon.Lang.EN
            End If

            VersionType = SmartV.Version.version.GetVersionType
            BuildMenu()
        End If
    End Sub

    Protected Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim ModuleID As Integer = e.Item.DataItem("ModuleID")

            Dim Menu As System.Web.UI.WebControls.Repeater = TryCast(e.Item.FindControl("Repeater1"), System.Web.UI.WebControls.Repeater)

            'Dim BuildMenu As New SmartV.Security.MENU.SYSMenuCreator
            ' Dim FormsDT As DataTable

            'FormsDT = BuildMenu.BuildLeftMenu(SessionVariables.LoginUser.GroupId, ModuleID, 0, 2, VersionType)
            Dim dr() As DataRow
            dr = FormsDT.Select("ModuleID='" & ModuleID & "'")

            Dim FormsDTClone = FormsDT.Clone
            If Not dr Is Nothing Then
                For Each row As DataRow In dr
                    FormsDTClone.ImportRow(row)
                Next
            End If

            Dim strForms As String = SessionVariables.LicenseDetails.FormIds
            Dim arrForms As ArrayList = SplitLicenseForms(strForms)
            Dim dt As DataTable



            Menu.DataSource = FormsDTClone
            Menu.DataBind()

        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub BuildMenu()



        Dim BuildMenu As New SmartV.Security.MENU.SYSMenuCreator


        Dim ModuleDT As DataTable

        Dim AllowedPages As String = ","


        ModuleDT = BuildMenu.BuildMenu(SessionVariables.LoginUser.GroupId, 0, 0, 1)

        Repeater2.DataSource = ModuleDT
        Repeater2.DataBind()



    End Sub

    Private Sub UserControls_LeftMenu_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim BuildMenu As New SmartV.Security.MENU.SYSMenuCreator
        FormsDT = BuildMenu.BuildLeftMenu(SessionVariables.LoginUser.GroupId, 0, 0, 2, VersionType)
    End Sub

    Public Function SplitLicenseForms(ByVal LicenseForms As String) As ArrayList

        Dim s As String = LicenseForms
        Dim arrForms As New ArrayList
        For Each value As String In s.Split(","c)
            arrForms.Add(Convert.ToInt32(value))
        Next
        Return arrForms
    End Function

#End Region

End Class
