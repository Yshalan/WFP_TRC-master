Imports TA.Definitions
Imports SmartV.UTILITIES
Imports System.Data
Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports TA.Security
Imports SmartV.UTILITIES.ProjectCommon

Partial Class Definitions_Authorities
    Inherits System.Web.UI.Page

#Region "Class Variables"
    Private objAuthorities As Authorities
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Public MsgLang As String
#End Region

#Region "Properties"

    Private Property AuthorityId() As Integer
        Get
            Return ViewState("AuthorityId")
        End Get
        Set(ByVal value As Integer)
            ViewState("AuthorityId") = value
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

#Region "Page events"
    Protected Sub dgrdAuthority_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdAuthority.Skin))
    End Function

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
                MsgLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
                MsgLang = "en"
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
                MsgLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                MsgLang = "en"
            End If

            FillGridView()
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            UserCtrlAuthorities.HeaderText = IIf(Lang = CtlCommon.Lang.EN, "Define Authorities", "تعريف الجهات")
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdAuthority.ClientID + "');")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not pnlAuthority.FindControl(row("AddBtnName")) Is Nothing Then
                        pnlAuthority.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not pnlAuthority.FindControl(row("DeleteBtnName")) Is Nothing Then
                        pnlAuthority.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not pnlAuthority.FindControl(row("EditBtnName")) Is Nothing Then
                        pnlAuthority.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not pnlAuthority.FindControl(row("PrintBtnName")) Is Nothing Then
                        pnlAuthority.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub



    Protected Sub btnSave_Click(ByVal sender As Object, _
                                ByVal e As System.EventArgs) Handles btnSave.Click
        objAuthorities = New Authorities()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim errorNum As Integer = -1
        With objAuthorities
            ' Set data into object for Add / Update
            .AuthorityCode = txtAuthorityCode.Text.Trim()
            .AuthorityName = txtAuthorityName.Text.Trim()
            .AuthorityArabicName = txtAuthorityarabicName.Text.Trim()
            .Active = chkActive.Checked
            .CREATED_BY = SessionVariables.LoginUser.ID
            .CREATED_DATE = Date.Now
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_DATE = Date.Now
        End With
        If AuthorityId = 0 Then
            ' Do add operation 
            errorNum = objAuthorities.Add()
            If errorNum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ElseIf errorNum = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
            ElseIf errorNum = -6 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
            ElseIf errorNum = -7 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        Else
            ' Do update operations
            objAuthorities.AuthorityId = AuthorityId
            errorNum = objAuthorities.Update()
            If errorNum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            ElseIf errorNum = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
            ElseIf errorNum = -6 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
            ElseIf errorNum = -7 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If
        End If
        If errorNum = 0 Then
            FillGridView()
            ClearAll()
        End If
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, _
                                 ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
        FillGridView()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1
        objAuthorities = New Authorities
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdAuthority.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intAuthorityId As Integer = Convert.ToInt32(row.GetDataKeyValue("AuthorityId").ToString())
                objAuthorities.AuthorityId = intAuthorityId
                errNum = objAuthorities.Delete()

            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGridView()
            ClearAll()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
            'showResult(CodeResultMessage.CodeDeleteFail)
        End If
    End Sub

    Protected Sub dgrdAuthority_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdAuthority.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item) Then
            Dim Itm As GridDataItem = e.Item

        End If
        For Each item As GridDataItem In dgrdAuthority.MasterTableView.Items

            Dim Active As TableCell = item("Active")

            If item.GetDataKeyValue("Active") = False Then
                If Lang = CtlCommon.Lang.AR Then
                    Active.Text = "لا"
                End If
            ElseIf item.GetDataKeyValue("Active") = True Then
                If Lang = CtlCommon.Lang.AR Then
                    Active.Text = "نعم"
                End If
            End If

        Next
    End Sub

    Protected Sub dgrdAuthority_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdAuthority.NeedDataSource
        Try
            objAuthorities = New Authorities()
            dgrdAuthority.DataSource = objAuthorities.GetAll()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dgrdAuthority_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdAuthority.SelectedIndexChanged
        If dgrdAuthority.SelectedItems.Count = 1 Then
            AuthorityId = DirectCast(dgrdAuthority.SelectedItems(0), GridDataItem).GetDataKeyValue("AuthorityId").ToString().Trim
            FillControls()
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillGridView()
        Try
            objAuthorities = New Authorities()
            dgrdAuthority.DataSource = objAuthorities.GetAll()
            dgrdAuthority.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClearAll()
        ' Clear controls
        txtAuthorityarabicName.Text = String.Empty
        txtAuthorityCode.Text = String.Empty
        txtAuthorityName.Text = String.Empty
        chkActive.Checked = False
        ' reset to id to next add
        AuthorityId = 0
        ' Remove sorting and sorting direction

    End Sub

    Private Sub FillControls()

        objAuthorities = New Authorities
        objAuthorities.AuthorityId = AuthorityId
        objAuthorities.GetByPK()
        With objAuthorities
            txtAuthorityCode.Text = .AuthorityCode
            txtAuthorityName.Text = .AuthorityName
            txtAuthorityarabicName.Text = .AuthorityArabicName
            chkActive.Checked = .Active
        End With
    End Sub

#End Region

End Class
