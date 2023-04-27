Imports TA.Admin.OrgCompany
Imports SmartV.UTILITIES
Imports TA.Lookup
Imports System.Data
Imports TA.Definitions
Imports Telerik.Web.UI
Imports TA.DailyTasks

Partial Class DailyTasks_RealTimeMovements
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objEmp_Move As Emp_Move
    Shared SortExepression As String

#End Region

#Region "Properties"

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

#End Region

#Region "Events"

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("RealTimeMovements", CultureInfo)

            FillGridView()
            Dim strInterval As Integer = CInt(System.Configuration.ConfigurationManager.AppSettings("TimeInterval").ToString)
            timerAll.Interval = strInterval
            If Lang = CtlCommon.Lang.EN Then
                gvEvents.Columns(1).Visible = True
                gvEvents.Columns(2).Visible = False
                gvEvents.Columns(3).Visible = True
                gvEvents.Columns(4).Visible = False
            Else
                gvEvents.Columns(1).Visible = False
                gvEvents.Columns(2).Visible = True
                gvEvents.Columns(3).Visible = False
                gvEvents.Columns(4).Visible = True
            End If
            timerAll.Enabled = True
            'lblLastUpdate.Text = DateTime.Now.ToShortDateString() & " - " & DateTime.Now.ToLongTimeString()
        End If

    End Sub

    Protected Sub timerAll_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timerAll.Tick
        Try
            ' If dtCurrent.Rows.Count = 100 Then
            FillGridView()
            'End If
            'lblLastUpdate.Text = DateTime.Now.ToShortDateString() & " - " & DateTime.Now.ToLongTimeString()
            ' AddRecord()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvEvents_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvEvents.NeedDataSource
        objEmp_Move = New Emp_Move
        dtCurrent = objEmp_Move.GetAllForRealTime()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        gvEvents.DataSource = dv
    End Sub

#End Region

#Region "Methods"

    Private Sub FillGridView()
        objEmp_Move = New Emp_Move
        dtCurrent = objEmp_Move.GetAllForRealTime
        gvEvents.DataSource = dtCurrent
        gvEvents.DataBind()
    End Sub

    Private Sub AddRecord()
        objEmp_Move = New Emp_Move
        Dim dtTemp As New DataTable
        dtTemp = objEmp_Move.GetAllForRealTime
        For Each dr As DataRow In dtTemp.Rows
            Dim drTemp As DataRow
            drTemp = dtCurrent.Rows(dtCurrent.Rows.Count - 1)
            Dim comparer As IEqualityComparer(Of DataRow) = DataRowComparer.Default
            Dim bEqual = comparer.Equals(drTemp, dr)
            If Not bEqual Then
                dtCurrent.ImportRow(dr)
            End If
        Next
        gvEvents.DataSource = dtCurrent
        gvEvents.DataBind()
    End Sub

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", gvEvents.Skin))
    End Function

    Protected Sub gvEvents_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvEvents.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

#End Region

End Class
