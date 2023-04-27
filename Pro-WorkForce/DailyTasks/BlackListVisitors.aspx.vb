
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Definitions
Imports TA.Visitors

Partial Class DailyTasks_BlackListVisitors
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_Nationality As Emp_Nationality
    Private objBlackListVisitors As BlackListVisitors
    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

#End Region

#Region "Public Properties"

    Public Property BlacklistId() As Integer
        Get
            Return ViewState("BlacklistId")
        End Get
        Set(ByVal value As Integer)
            ViewState("BlacklistId") = value
        End Set
    End Property

#End Region

#Region "Page Events"

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

    Private Sub DailyTasks_BlackListVisitors_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            FillNationality()
            FillGrid()
            PageHeader1.HeaderText = ResourceManager.GetString("BlackListVisitors")
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdBlacklistVisitors.ClientID + "');")

    End Sub

    Protected Sub dgrdBlacklistVisitors_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdBlacklistVisitors.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(sender As Object, e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdBlacklistVisitors.Skin))
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim err As Integer = -1
        objBlackListVisitors = New BlackListVisitors
        With objBlackListVisitors
            .IDNumber = txtIDNumber.Text
            .VisitorName = txtVisitorName.Text
            .VisitorArabicName = txtVisitorArabicName.Text
            .Nationality = radcmbxNationality.SelectedValue

            If BlacklistId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add
            Else
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .BlacklistId = BlacklistId
                err = .Update
            End If

        End With

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGrid()
            ClearAll()
        ElseIf err = -2 Then
            CtlCommon.ShowMessage(Me, ResourceManager.GetString("IdNumberExists", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        For Each row As GridDataItem In dgrdBlacklistVisitors.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objBlackListVisitors = New BlackListVisitors
                objBlackListVisitors.BlacklistId = Convert.ToInt32(row.GetDataKeyValue("BlacklistId").ToString())
                errNum = objBlackListVisitors.Delete()

            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        End If

        ClearAll()

    End Sub

    Private Sub dgrdBlacklistVisitors_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdBlacklistVisitors.NeedDataSource
        objBlackListVisitors = New BlackListVisitors
        With objBlackListVisitors
            dgrdBlacklistVisitors.DataSource = .GetAll_Inner
        End With
    End Sub

    Private Sub dgrdBlacklistVisitors_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdBlacklistVisitors.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            If Lang = CtlCommon.Lang.AR Then
                item("Nationality").Text = item("NationalityArabicName").Text
            Else
                item("Nationality").Text = item("NationalityName").Text
            End If

        End If
    End Sub

    Private Sub dgrdBlacklistVisitors_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdBlacklistVisitors.SelectedIndexChanged
        BlacklistId = Convert.ToInt32(DirectCast(dgrdBlacklistVisitors.SelectedItems(0), GridDataItem).GetDataKeyValue("BlacklistId").ToString())
        FillControls()
    End Sub

#End Region

#Region "Methods"

    Private Sub FillNationality()
        Dim dt As DataTable
        objEmp_Nationality = New Emp_Nationality
        With objEmp_Nationality
            dt = .GetAll
            ProjectCommon.FillRadComboBox(radcmbxNationality, dt, "NationalityName", "NationalityArabicName")
        End With
    End Sub

    Private Sub ClearAll()
        radcmbxNationality.SelectedValue = -1
        txtIDNumber.Text = String.Empty
        txtVisitorName.Text = String.Empty
        txtVisitorArabicName.Text = String.Empty
        BlacklistId = 0
    End Sub

    Private Sub FillControls()
        objBlackListVisitors = New BlackListVisitors
        With objBlackListVisitors
            .BlacklistId = BlacklistId
            .GetByPK()
            txtIDNumber.Text = .IDNumber
            txtVisitorName.Text = .VisitorName
            txtVisitorArabicName.Text = .VisitorArabicName
            radcmbxNationality.SelectedValue = .Nationality
        End With
    End Sub

    Private Sub FillGrid()
        objBlackListVisitors = New BlackListVisitors
        With objBlackListVisitors
            dgrdBlacklistVisitors.DataSource = .GetAll_Inner
            dgrdBlacklistVisitors.DataBind()
        End With
    End Sub

#End Region


End Class
