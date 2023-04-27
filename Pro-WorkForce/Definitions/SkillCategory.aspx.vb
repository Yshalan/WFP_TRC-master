Imports TA.Definitions
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Security

Partial Class Definitions_SkillCategory
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objSkillCategory As SkillCategory
    Private objSkills As Skills

#End Region

#Region "Properties"

    Private Property CategoryId() As Integer
        Get
            Return ViewState("CategoryId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CategoryId") = value
        End Set
    End Property

    Private Property SkillId() As Integer
        Get
            Return ViewState("SkillId")
        End Get
        Set(ByVal value As Integer)
            ViewState("SkillId") = value
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

    Protected Sub Definitions_SkillCategory_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            FillCategoryGrid()
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdSkillCategory.ClientID + "');")
        btnDeleteSkill.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdSkills.ClientID + "');")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not pnlSkillCategory.FindControl(row("AddBtnName")) Is Nothing Then
                        pnlSkillCategory.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not pnlSkillCategory.FindControl(row("DeleteBtnName")) Is Nothing Then
                        pnlSkillCategory.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not pnlSkillCategory.FindControl(row("EditBtnName")) Is Nothing Then
                        pnlSkillCategory.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not pnlSkillCategory.FindControl(row("PrintBtnName")) Is Nothing Then
                        pnlSkillCategory.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub RadToolBarCategory_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIconCategory() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdSkillCategory.Skin))
    End Function

    Protected Sub RadToolBarSkills_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIconSkills() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdSkills.Skin))
    End Function

    Protected Sub dgrdSkillCategory_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdSkillCategory.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilterCategory.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdSkills_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdSkills.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilterSkills.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdSkillCategory_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdSkillCategory.NeedDataSource
        objSkillCategory = New SkillCategory
        Dim dt As DataTable
        With objSkillCategory
            dt = .GetAll
            dgrdSkillCategory.DataSource = dt
        End With
    End Sub

    Protected Sub dgrdSkillCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdSkillCategory.SelectedIndexChanged
        CategoryId = CInt(DirectCast(dgrdSkillCategory.SelectedItems(0), GridDataItem).GetDataKeyValue("CategoryId").ToString())
        FillCategoryControls()
        FillSkillsGrid()
        ChangeSkillsCaptions()
    End Sub

    Protected Sub dgrdSkills_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdSkills.NeedDataSource
        objSkills = New Skills
        Dim dt As DataTable
        With objSkills
            .FK_CategoryId = CategoryId
            dt = .GetAll_ByFK_CategoryId
            dgrdSkills.DataSource = dt
        End With
    End Sub

    Protected Sub dgrdSkills_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdSkills.SelectedIndexChanged
        SkillId = CInt(DirectCast(dgrdSkills.SelectedItems(0), GridDataItem).GetDataKeyValue("SkillId").ToString())
        FillSkillControls()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        objSkillCategory = New SkillCategory
        Dim err As Integer = -1
        With objSkillCategory
            .CategoryName = txtCategoryName.Text
            .CategoryArabicName = txtCategoryArabicName.Text
            .DisplayName = txtDisplayName.Text
            .DisplayArabicName = txtDisplayArabicName.Text
            .HasDate = chkHasDate.Checked
            If CategoryId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add
                .CategoryId = CategoryId
            Else
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .CategoryId = CategoryId
                err = .Update
            End If
        End With

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ClearCategory()
            FillCategoryGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If

    End Sub

    Protected Sub btnSaveSkill_Click(sender As Object, e As EventArgs) Handles btnSaveSkill.Click
        objSkills = New Skills
        Dim err = -1
        With objSkills
            .SkillName = txtSkillName.Text
            .SkillArabicName = txtSkillArabicName.Text
            .Desc_En = txtDesc_En.Text
            .Desc_Ar = txtDesc_Ar.Text
            .FK_CategoryId = CategoryId

            If SkillId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add
                .SkillId = SkillId
            Else
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .SkillId = SkillId
                err = .Update
            End If
        End With

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ClearSkills()
            FillSkillsGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearCategory()
    End Sub

    Protected Sub btnClearSkill_Click(sender As Object, e As EventArgs) Handles btnClearSkill.Click
        ClearSkills()
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdSkillCategory.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intCategoryId As Integer = Convert.ToInt32(row.GetDataKeyValue("CategoryId").ToString())
                objSkillCategory = New SkillCategory
                objSkillCategory.CategoryId = intCategoryId
                errNum = objSkillCategory.Delete()

            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillCategoryGrid()
        ElseIf errNum = -5 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("SkillsExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        End If
        ClearCategory()
    End Sub

    Protected Sub btnDeleteSkill_Click(sender As Object, e As EventArgs) Handles btnDeleteSkill.Click
        Dim errNum As Integer = -1
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdSkillCategory.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intSkillId As Integer = Convert.ToInt32(row.GetDataKeyValue("SkillId").ToString())
                objSkills = New Skills
                objSkills.SkillId = intSkillId
                errNum = objSkills.Delete()

            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillCategoryGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        End If
        ClearCategory()
    End Sub

#End Region

#Region "Methods"

    Private Sub FillCategoryGrid()
        objSkillCategory = New SkillCategory
        Dim dt As DataTable
        With objSkillCategory
            dt = .GetAll
            dgrdSkillCategory.DataSource = dt
            dgrdSkillCategory.DataBind()
        End With
    End Sub

    Private Sub FillSkillsGrid()
        objSkills = New Skills
        Dim dt As DataTable
        With objSkills
            .FK_CategoryId = CategoryId
            dt = .GetAll_ByFK_CategoryId
            dgrdSkills.DataSource = dt
            dgrdSkills.DataBind()
        End With
    End Sub

    Private Sub FillCategoryControls()
        objSkillCategory = New SkillCategory
        With objSkillCategory
            .CategoryId = CategoryId
            .GetByPK()
            txtCategoryName.Text = .CategoryName
            txtCategoryArabicName.Text = .CategoryArabicName
            txtDisplayName.Text = .DisplayName
            txtDisplayArabicName.Text = .DisplayArabicName
            tabSkills.Visible = True
            chkHasDate.Checked = .HasDate
        End With
    End Sub

    Private Sub FillSkillControls()
        objSkills = New Skills
        With objSkills
            .SkillId = SkillId
            .GetByPK()
            txtSkillName.Text = .SkillName
            txtSkillArabicName.Text = .SkillArabicName
            txtDesc_En.Text = .Desc_En
            txtDesc_Ar.Text = .Desc_Ar
        End With
    End Sub

    Private Sub ClearCategory()
        txtCategoryName.Text = String.Empty
        txtCategoryArabicName.Text = String.Empty
        txtDisplayName.Text = String.Empty
        txtDisplayArabicName.Text = String.Empty
        chkHasDate.Checked = False
        CategoryId = 0
        tabSkills.Visible = False
        ClearSkills()

    End Sub

    Private Sub ClearSkills()
        txtSkillName.Text = String.Empty
        txtSkillArabicName.Text = String.Empty
        txtDesc_En.Text = String.Empty
        txtDesc_Ar.Text = String.Empty
        SkillId = 0
    End Sub

    Private Sub ChangeSkillsCaptions()
        objSkillCategory = New SkillCategory
        With objSkillCategory
            .CategoryId = CategoryId
            .GetByPK()
            If Lang = CtlCommon.Lang.AR Then
                lblSkillName.Text = .DisplayArabicName
                rfvCategoryName.ErrorMessage = "الرجاء ادخال " & .DisplayArabicName

                lblSkillArabicName.Text = .DisplayArabicName & " باللغة العربية"
                rfvCategoryArabicName.ErrorMessage = "الرجاء ادخال " & .DisplayArabicName & " باللغة العربية"

                lblDesc_En.Text = "وصف " & .DisplayArabicName
                lblDesc_Ar.Text = "وصف " & .DisplayArabicName & " باللغة العربية"
            Else
                lblSkillName.Text = .DisplayName
                rfvCategoryName.ErrorMessage = "Please Enter " & .DisplayName

                lblSkillArabicName.Text = .DisplayName & " Arabic"
                rfvCategoryArabicName.ErrorMessage = "Please Enter " & .DisplayName & " Arabic"

                lblDesc_En.Text = .DisplayName & " Description"
                lblDesc_Ar.Text = .DisplayName & " Arabic Description"
            End If
        End With
    End Sub
#End Region


End Class
