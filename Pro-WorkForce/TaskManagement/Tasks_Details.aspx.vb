Imports SmartV.UTILITIES
Imports TA.Definitions
Imports System.Data
Imports TA.TaskManagement
Imports System.Web.Services

Partial Class Definitions_Tasks_Details
    Inherits System.Web.UI.Page

#Region "Class Variable"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objProjects As Projects
    Private objProject_tasks As Project_tasks

#End Region

#Region "Public Properties"

    Public Property ProjectId() As Integer
        Get
            Return ViewState("ProjectId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ProjectId") = value
        End Set
    End Property

    Public Property TaskId() As Integer
        Get
            Return ViewState("TaskId")
        End Get
        Set(ByVal value As Integer)
            ViewState("TaskId") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Definitions_Tasks_Details_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            If Not Page.Request.QueryString("ProjectId") Is Nothing Then
                ProjectId = Page.Request.QueryString("ProjectId")
                UCGanttChart1.ProjectId = ProjectId
                mvProjects.ActiveViewIndex = 1
            Else
                FillGridProjects()
                mvProjects.ActiveViewIndex = 0
            End If
            PageHeader1.HeaderText = ResourceManager.GetString("TaskDetails", CultureInfo)
        End If

        Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        scriptManager__1.RegisterPostBackControl(lnkviewGraph)
        Dim scriptManager__2 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        scriptManager__2.RegisterPostBackControl(lnkviewTasks)
        Dim scriptManager__3 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        scriptManager__3.RegisterPostBackControl(lnkviewProjects)
        Dim scriptManager__4 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        scriptManager__4.RegisterPostBackControl(lnkviewProjects2)

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

    Protected Sub lnkEditTask_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        TaskId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("TaskId").ToString())
        'ClientScript.RegisterStartupScript(Me.Page.GetType(), "openedit", "openEditRadWin();", True)

        'Dim QueryString As String = "../TaskManagement/TasksPopUp.aspx?ProjectId=" & ProjectId & "&ClickType=Edit" & "&TaskId=" & TaskId
        'Dim newWin As String = (Convert.ToString("window.open('") & QueryString) + "', 'popup_window', 'width=700,height=530,left=400,top=100,resizable=yes,scrollbars=1');"
        'ClientScript.RegisterStartupScript(Me.GetType(), "pop", newWin, True)

    End Sub

    Protected Sub dgrdTasks_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdTasks.ItemDataBound

        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            Dim scriptManager__2 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            scriptManager__2.RegisterPostBackControl(DirectCast(e.Item.FindControl("lnkEditTask"), LinkButton))
        End If

    End Sub

    Protected Sub dgrdTasks_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdTasks.NeedDataSource
        objProject_tasks = New Project_tasks
        Dim dt As DataTable
        With objProject_tasks
            .FK_ProjectId = ProjectId
            dt = .Get_by_FK_ProjectId
            dgrdTasks.DataSource = dt
        End With
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdTasks.Skin))
    End Function

    Protected Function GetFilterIcon1() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdProjects.Skin))
    End Function

    Protected Sub dgrdProjects_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdProjects.NeedDataSource
        objProjects = New Projects
        Dim dt As DataTable
        With objProjects
            dt = .GetAll
            dgrdProjects.DataSource = dt
        End With
    End Sub

    Protected Sub dgrdProjects_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdProjects.SelectedIndexChanged
        ProjectId = CInt(CType(dgrdProjects.SelectedItems(0), GridDataItem).GetDataKeyValue("ProjectId").ToString())
        UCGanttChart1.ProjectId = ProjectId
        UCGanttChart1.loadtasks()
        UCGanttChart1.loadlinks()
        mvProjects.ActiveViewIndex = 1
        FillGridTasks()
    End Sub

    Protected Sub dgrdTasks_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdTasks.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadTasks.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdProjects_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdProjects.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilterProjects.FireApplyCommand()
        End If
    End Sub

    Protected Sub lnkviewProjects_Click(sender As Object, e As EventArgs) Handles lnkviewProjects.Click
        mvProjects.ActiveViewIndex = 0
        FillGridProjects()
    End Sub

    Protected Sub lnkviewProjects2_Click(sender As Object, e As EventArgs) Handles lnkviewProjects2.Click
        mvProjects.ActiveViewIndex = 0
        FillGridProjects()
    End Sub

    Protected Sub lnkviewTasks_Click(sender As Object, e As EventArgs) Handles lnkviewTasks.Click
        mvProjects.SetActiveView(viewTasks)
        FillGridTasks()
    End Sub

    Protected Sub lnkviewGraph_Click(sender As Object, e As EventArgs) Handles lnkviewGraph.Click
        mvProjects.SetActiveView(viewGantt)
        UCGanttChart1.ProjectId = ProjectId
        UCGanttChart1.GetGanttDate()
        UCGanttChart1.LoadTasks()
        UCGanttChart1.LoadLinks()
    End Sub

#End Region

#Region "Methods"

    Private Sub FillGridTasks()
        objProject_tasks = New Project_tasks
        Dim dt As DataTable
        With objProject_tasks
            .FK_ProjectId = ProjectId
            dt = .Get_by_FK_ProjectId
            dgrdTasks.DataSource = dt
            dgrdTasks.DataBind()
        End With
    End Sub

    Private Sub FillGridProjects()
        objProjects = New Projects
        Dim dt As DataTable
        With objProjects
            dt = .GetAll
            dgrdProjects.DataSource = dt
            dgrdProjects.DataBind()
        End With
    End Sub

#End Region

End Class
