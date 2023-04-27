Imports System.Data
Imports TA.Lookup
Imports SmartV.DB
Imports SmartV.UTILITIES
Imports TA.Admin
Imports Telerik.Web.UI
Imports SmartV.Version
Imports TA.Security

Partial Class Admin_OrgLevel
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objOrgLevel As OrgLevel
    Private objOrgcompany As OrgCompany
    Private Lang As CtlCommon.Lang
    Private objVersion As SmartV.Version.version
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

#End Region

#Region "Properties"

    Public Property LevelID() As Integer
        Get
            Return ViewState("_LevelID")
        End Get
        Set(ByVal value As Integer)
            ViewState("_LevelID") = value
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

#Region "Events"
    'Protected Sub dgrdLevel_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
    '    If e.CommandName = "FilterRadGrid" Then
    '        RadFilter1.FireApplyCommand()
    '    End If
    'End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdLevel.Skin))
    End Function

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
            Else
                If SessionVariables.CultureInfo = "ar-JO" Then
                    Lang = CtlCommon.Lang.AR
                    PageHeader1.HeaderText = "مستويات المؤسسة"
                Else
                    Lang = CtlCommon.Lang.EN
                    PageHeader1.HeaderText = "Company Levels"
                End If
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            ClearAll()
            FillCombo()
            If (objVersion.HasMultiCompany = False) Then
                RadComboBoxCompany.SelectedValue = objVersion.GetCompanyId()
                trCompany.Visible = False
                FillGrid()
            End If

        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not pnlEmpNationality.FindControl(row("AddBtnName")) Is Nothing Then
                        pnlEmpNationality.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not pnlEmpNationality.FindControl(row("DeleteBtnName")) Is Nothing Then
                        pnlEmpNationality.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not pnlEmpNationality.FindControl(row("EditBtnName")) Is Nothing Then
                        pnlEmpNationality.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not pnlEmpNationality.FindControl(row("PrintBtnName")) Is Nothing Then
                        pnlEmpNationality.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnSave.Click
        objOrgLevel = New OrgLevel
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim err As Integer
        With objOrgLevel
            .FK_CompanyId = RadComboBoxCompany.SelectedValue
            .LevelArabicName = TextBoxLevelNameArabic.Text.Trim
            .LevelName = TextBoxLevelName.Text.Trim
            If LevelID = 0 Then
                err = .Add()
            Else

                .LevelId = LevelID
                err = .Update()
            End If
        End With
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ClearAll()
            'FillGrid()
        ElseIf err = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
        ElseIf err = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If

        If (objVersion.HasMultiCompany() = False) Then
            FillGrid()
        End If
    End Sub

    Protected Sub ibtnRest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnRest.Click
        ClearAll()
    End Sub

    Protected Sub RadComboBoxCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxCompany.SelectedIndexChanged
        FillGrid()
    End Sub

    Protected Sub dgrdLevel_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdLevel.ItemCommand
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
        If e.CommandName = "Delete" Then
            Dim tmpLevelId As Integer = CInt(DirectCast(e.Item, GridDataItem).GetDataKeyValue("LevelId").ToString().Trim)
            Dim Tmp_FK_CompanyId As Integer = CInt(DirectCast(e.Item, GridDataItem).GetDataKeyValue("FK_CompanyId").ToString().Trim)
            objOrgLevel = New OrgLevel
            With objOrgLevel
                .LevelId = tmpLevelId
                .FK_CompanyId = Tmp_FK_CompanyId
                Dim err As Integer = -1
                err = objOrgLevel.Delete()
                If err = 0 Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
                ElseIf err = -11 Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("RemoveReference", CultureInfo), "info")
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
                End If

            End With
            FillGrid()
        End If
    End Sub

    Protected Sub dgrdLevel_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdLevel.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            If item("Enabledelete").Text = "False" Then
                Dim btn As LinkButton = DirectCast(item("column").Controls(0), LinkButton)
                btn.Visible = False
            End If
        End If
    End Sub

    Protected Sub dgrdLevel_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdLevel.NeedDataSource
        Try
            Dim dt As DataTable
            objOrgLevel = New OrgLevel
            If RadComboBoxCompany.SelectedIndex > 0 Then
                objOrgLevel.FK_CompanyId = RadComboBoxCompany.SelectedValue
                dt = objOrgLevel.GetAllByComapany
            Else
                dt = objOrgLevel.GetAllWithCompany()
            End If
            dgrdLevel.DataSource = dt
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub dgrdLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdLevel.SelectedIndexChanged

        LevelID = Convert.ToInt32(DirectCast(dgrdLevel.SelectedItems(0), GridDataItem).GetDataKeyValue("LevelId").ToString())
        objOrgLevel = New OrgLevel
        With objOrgLevel
            .LevelId = LevelID
            .FK_CompanyId = Convert.ToInt32(DirectCast(dgrdLevel.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_CompanyId").ToString())
            .GetByPK()
            TextBoxLevelName.Text = .LevelName
            TextBoxLevelNameArabic.Text = .LevelArabicName
            RadComboBoxCompany.SelectedValue = .FK_CompanyId
            RadComboBoxCompany.Enabled = False
        End With
    End Sub

#End Region

#Region "Methods"

    Sub FillCombo()
        Dim dt As New DataTable
        objOrgcompany = New OrgCompany
        dt = objOrgcompany.GetAllforddl
        CtlCommon.FillTelerikDropDownList(RadComboBoxCompany, dt, Lang)

    End Sub

    Public Sub FillGrid()

        Try
            Dim dt As DataTable
            objOrgLevel = New OrgLevel
            If RadComboBoxCompany.SelectedIndex > 0 Then
                objOrgLevel.FK_CompanyId = RadComboBoxCompany.SelectedValue
                dt = objOrgLevel.GetAllByComapany
                RadFilter1.Visible = True
                dgrdLevel.Visible = True
            Else
                dt = Nothing
                RadFilter1.Visible = False
                dgrdLevel.Visible = False
            End If


            dgrdLevel.DataSource = dt
            dgrdLevel.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Public Sub ClearAll()
        LevelID = 0
        TextBoxLevelName.Text = ""
        TextBoxLevelNameArabic.Text = ""
        RadComboBoxCompany.Enabled = True
        If (objVersion.HasMultiCompany()) Then
            RadComboBoxCompany.SelectedIndex = 0
        End If

        FillGrid()

    End Sub

#End Region

End Class
