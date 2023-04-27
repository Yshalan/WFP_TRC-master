Imports TA.Definitions
Imports SmartV.UTILITIES
Imports System.Data
Imports TA.TaskManagement

Partial Class Definitions_Tasks
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objProjects As Projects
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Page Events"

    Protected Sub Definitions_Tasks_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            FillGridProjects()
            PageHeader1.HeaderText = ResourceManager.GetString("ProjectTasks", CultureInfo)
        End If
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub

    Protected Sub dgrdProjects_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdProjects.NeedDataSource
        objProjects = New Projects
        Dim dt As DataTable
        With objProjects
            dt = .GetAll
            dgrdProjects.DataSource = dt
        End With
    End Sub

    Protected Sub dgrdProjects_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdProjects.SelectedIndexChanged
        Dim ProjectId As Integer = CInt(CType(dgrdProjects.SelectedItems(0), GridDataItem).GetDataKeyValue("ProjectId").ToString())
        Response.Redirect("../TaskManagement/Tasks_Details.aspx?ProjectId=" & ProjectId)
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

#End Region
    
#Region "Methods"

    Private Sub FillGridProjects()
        objProjects = New Projects
        Dim dt As DataTable
        With objProjects
            dt = .GetAll
            dgrdProjects.DataSource = dt
            dgrdProjects.DataBind()
        End With
    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdProjects.Skin))
    End Function

#End Region
    
End Class
