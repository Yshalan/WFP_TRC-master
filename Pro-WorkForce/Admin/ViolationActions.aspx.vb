Imports System.Data
Imports TA.Lookup
Imports SmartV.DB
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.Admin
Imports TA.Security

Partial Class Admin_ViolationActions
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Dim objTAPolicy_ViolationActions As TAPolicy_ViolationActions
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Properties"

    Public Property ActionId() As Integer
        Get
            Return ViewState("ActionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ActionId") = value
        End Set
    End Property

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub RadToolBar1_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs)

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("ViolationActions", CultureInfo)
            Fillgrid()
            ClearAll()
            ibtnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdViolationActions.ClientID + "')")
        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("AddBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("EditBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("PrintBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub dgrdViolationActions_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdViolationActions.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdViolationActions_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdViolationActions.NeedDataSource
        dgrdViolationActions.DataSource = New TAPolicy_ViolationActions().GetAll()
    End Sub

    Protected Sub dgrdViolationActions_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdViolationActions.SelectedIndexChanged
        ActionId = Convert.ToInt32(DirectCast(dgrdViolationActions.SelectedItems(0), GridDataItem).GetDataKeyValue("ActionId").ToString())
        objTAPolicy_ViolationActions = New TAPolicy_ViolationActions()
        objTAPolicy_ViolationActions.ActionId = ActionId
        objTAPolicy_ViolationActions.GetByPK()
        txtActionArabicName.Text = objTAPolicy_ViolationActions.ActionArabicName.Trim
        txtActionName.Text = objTAPolicy_ViolationActions.ActionName.Trim
    End Sub

    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnSave.Click
        Dim err As Integer = 0
        objTAPolicy_ViolationActions = New TAPolicy_ViolationActions()
        With objTAPolicy_ViolationActions
            .ActionName = txtActionName.Text.Trim
            .ActionArabicName = txtActionArabicName.Text.Trim
            .CREATED_BY = ""
            If ActionId = 0 Then
                err = .Add()
            Else
                .LAST_UPDATE_BY = ""
                .ActionId = ActionId
                err = .Update()

            End If
            Select Case err
                Case 0
                    Fillgrid()
                    ClearAll()
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                Case -5
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
                Case -6
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End Select


        End With

    End Sub

    Protected Sub ibtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnDelete.Click
        Dim err As Integer = 0

        For Each row As GridDataItem In dgrdViolationActions.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intActionId As Integer = Convert.ToInt32(row.GetDataKeyValue("ActionId").ToString())

                objTAPolicy_ViolationActions = New TAPolicy_ViolationActions
                objTAPolicy_ViolationActions.ActionId = intActionId
                err += objTAPolicy_ViolationActions.Delete()
            End If
        Next
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            Fillgrid()
            ClearAll()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If
    End Sub

    Protected Sub ibtnRest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnRest.Click
        Fillgrid()
        ClearAll()

    End Sub

#End Region

#Region "Methods"

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdViolationActions.Skin))
    End Function

    Sub Fillgrid()
        dgrdViolationActions.DataSource = New TAPolicy_ViolationActions().GetAll()
        dgrdViolationActions.DataBind()
    End Sub

    Private Sub ClearAll()
        txtActionArabicName.Text = String.Empty
        txtActionName.Text = String.Empty
        ActionId = 0


    End Sub

#End Region

End Class
