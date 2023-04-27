
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Definitions
Imports TA.Security

Partial Class Definitions_StudyMajors
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Dim CultureInfo As System.Globalization.CultureInfo
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Private Lang As CtlCommon.Lang
    Private objStudyMajors As StudyMajors
    Private objStudySpecialization As StudySpecialization

#End Region

#Region "Public Properties"

    Public Property MajorId() As Integer
        Get
            Return ViewState("MajorId")
        End Get
        Set(ByVal value As Integer)
            ViewState("MajorId") = value
        End Set
    End Property

    Public Property SpecializationId() As Integer
        Get
            Return ViewState("SpecializationId")
        End Get
        Set(ByVal value As Integer)
            ViewState("SpecializationId") = value
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

    Private Sub Definitions_StudyMajors_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            FillGridMajor()
            PageHeader1.HeaderText = ResourceManager.GetString("StudyMajors")
        End If
        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdMajors.ClientID + "')")
        btnRemove.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdSpecialization.ClientID + "')")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not Update1.FindControl(row("AddBtnName")) Is Nothing Then
                        Update1.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not Update1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        Update1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not Update1.FindControl(row("EditBtnName")) Is Nothing Then
                        Update1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not Update1.FindControl(row("PrintBtnName")) Is Nothing Then
                        Update1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Private Sub dgrdMajors_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdMajors.NeedDataSource
        objStudyMajors = New StudyMajors
        With objStudyMajors
            dgrdMajors.DataSource = .GetAll
        End With
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAllMajors()
        FillGridSpecialization()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        objStudyMajors = New StudyMajors
        Dim err As Integer = -1
        With objStudyMajors
            .MajorName = txtMajorName.Text
            .MajorArabicName = txtMajorArabicName.Text
            If MajorId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add
            Else
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .MajorId = MajorId
                err = .Update
            End If
        End With

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGridMajor()
            ClearAllMajors()
        ElseIf err = -2 Then
            CtlCommon.ShowMessage(Me, ResourceManager.GetString("NameExist", CultureInfo), "info")
        ElseIf err = -3 Then
            CtlCommon.ShowMessage(Me, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If

    End Sub

    Private Sub dgrdMajors_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdMajors.SelectedIndexChanged
        MajorId = Convert.ToInt32(DirectCast(dgrdMajors.SelectedItems(0), GridDataItem).GetDataKeyValue("MajorId").ToString())
        FillMajorsControls()
        FillGridSpecialization()
        TabSpecializations.Visible = True
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        For Each row As GridDataItem In dgrdMajors.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objStudyMajors = New StudyMajors
                objStudyMajors.MajorId = Convert.ToInt32(row.GetDataKeyValue("MajorId").ToString())
                errNum = objStudyMajors.Delete()

            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGridMajor()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        End If

        ClearAllMajors()
    End Sub

    Private Sub dgrdSpecialization_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdSpecialization.NeedDataSource
        objStudySpecialization = New StudySpecialization
        With objStudySpecialization
            .FK_MajorId = MajorId
            dgrdSpecialization.DataSource = .GetAll_Inner
        End With
    End Sub

    Private Sub btnClearSpecialization_Click(sender As Object, e As EventArgs) Handles btnClearSpecialization.Click
        ClearAllSpecializations()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If Not MajorId = 0 Then


            objStudySpecialization = New StudySpecialization
            Dim err As Integer = -1
            With objStudySpecialization
                .FK_MajorId = MajorId
                .SpecializationName = txtSpecializationName.Text
                .SpecializationArabicName = txtSpecializationArabicName.Text

                If SpecializationId = 0 Then
                    .CREATED_BY = SessionVariables.LoginUser.ID
                    err = .Add
                Else
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    .SpecializationId = SpecializationId
                    err = .Update
                End If
            End With
            If err = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                FillGridSpecialization()
                ClearAllSpecializations()
            ElseIf err = -2 Then
                CtlCommon.ShowMessage(Me, ResourceManager.GetString("NameExist", CultureInfo), "info")
            ElseIf err = -3 Then
                CtlCommon.ShowMessage(Me, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If

        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PleaseMajor", CultureInfo), "info")
        End If
    End Sub

    Private Sub dgrdSpecialization_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdSpecialization.SelectedIndexChanged
        SpecializationId = Convert.ToInt32(DirectCast(dgrdSpecialization.SelectedItems(0), GridDataItem).GetDataKeyValue("SpecializationId").ToString())
        FillSpecializationControls()
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Dim errNum As Integer = -1

        For Each row As GridDataItem In dgrdSpecialization.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objStudySpecialization = New StudySpecialization
                objStudySpecialization.SpecializationId = Convert.ToInt32(row.GetDataKeyValue("SpecializationId").ToString())
                errNum = objStudySpecialization.Delete()

            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGridSpecialization()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        End If

        ClearAllSpecializations()

    End Sub

#End Region

#Region "Methods"

    Private Sub FillGridMajor()
        objStudyMajors = New StudyMajors
        With objStudyMajors
            dgrdMajors.DataSource = .GetAll
            dgrdMajors.DataBind()
        End With
    End Sub

    Private Sub ClearAllMajors()
        txtMajorName.Text = String.Empty
        txtMajorArabicName.Text = String.Empty
        MajorId = 0
        dgrdMajors.Rebind()
        TabSpecializations.Visible = False
    End Sub

    Private Sub FillMajorsControls()
        objStudyMajors = New StudyMajors
        With objStudyMajors
            .MajorId = MajorId
            .GetByPK()
            txtMajorName.Text = .MajorName
            txtMajorArabicName.Text = .MajorArabicName
        End With

    End Sub

    Private Sub FillGridSpecialization()
        objStudySpecialization = New StudySpecialization
        With objStudySpecialization
            .FK_MajorId = MajorId
            dgrdSpecialization.DataSource = .GetAll_Inner
            dgrdSpecialization.DataBind()
        End With
    End Sub

    Private Sub ClearAllSpecializations()
        txtSpecializationName.Text = String.Empty
        txtSpecializationArabicName.Text = String.Empty
        SpecializationId = 0
        dgrdSpecialization.Rebind()
    End Sub

    Private Sub FillSpecializationControls()
        objStudySpecialization = New StudySpecialization
        With objStudySpecialization
            .SpecializationId = SpecializationId
            .GetByPK()
            txtSpecializationName.Text = .SpecializationName
            txtSpecializationArabicName.Text = .SpecializationArabicName
        End With
    End Sub

#End Region




#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdMajors.Skin))
    End Function

    Protected Sub dgrdMajors_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdMajors.ItemCommand
        If e.CommandName = "FilterRadGrid1" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar2_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon2() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdSpecialization.Skin))
    End Function

    Protected Sub dgrdSpecialization_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdSpecialization.ItemCommand
        If e.CommandName = "FilterRadGrid2" Then
            RadFilter2.FireApplyCommand()
        End If
    End Sub

#End Region



End Class
