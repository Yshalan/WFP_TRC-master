Imports System.Data
Imports TA.Definitions
Imports SmartV.UTILITIES
Imports TA.Security

Partial Class Definitions_DefineSemesters
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objSemesters As Semesters

#End Region
    
#Region "Public Properties"

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

    Public Property SemesterId() As Integer
        Get
            Return ViewState("SemesterId")
        End Get
        Set(ByVal value As Integer)
            ViewState("SemesterId") = value
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

    Protected Sub Definitions_DefineSemesters_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            FillGrid()
            PageHeader1.HeaderText = ResourceManager.GetString("DefineSemesters", CultureInfo)
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdSemesters.ClientID + "');")

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

    Protected Sub dgrdSemesters_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdSemesters.Skin))
    End Function

    Protected Sub dgrdSemesters_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdSemesters.NeedDataSource
        Dim dt As DataTable
        objSemesters = New Semesters
        With objSemesters
            dt = .GetAll
            dgrdSemesters.DataSource = dt
        End With
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim err As Integer = -1
        objSemesters = New Semesters
        With objSemesters
            .SemesterName = txtSemesterName.Text
            .SemesterArabicName = txtSemesterName.Text 'txtSemesterArabicName.Text

            If SemesterId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add
            Else
                .SemesterId = SemesterId
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                err = .Update
            End If
        End With

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGrid()
            ClearAll()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdSemesters.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objSemesters = New Semesters
                objSemesters.SemesterId = Convert.ToInt32(row.GetDataKeyValue("SemesterId").ToString())
                errNum = objSemesters.Delete()

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

    Protected Sub dgrdSemesters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdSemesters.SelectedIndexChanged
        SemesterId = CInt(CType(dgrdSemesters.SelectedItems(0), GridDataItem).GetDataKeyValue("SemesterId").ToString())
        FillControls()
    End Sub

#End Region

#Region "Methods"

    Private Sub FillGrid()
        Dim dt As DataTable
        objSemesters = New Semesters
        With objSemesters
            dt = .GetAll
            dgrdSemesters.DataSource = dt
            dgrdSemesters.DataBind()
        End With
    End Sub

    Private Sub FillControls()
        objSemesters = New Semesters
        With objSemesters
            .SemesterId = SemesterId
            .GetByPK()
            txtSemesterName.Text = .SemesterName
            txtSemesterArabicName.Text = .SemesterArabicName
        End With
    End Sub

    Private Sub ClearAll()
        txtSemesterName.Text = String.Empty
        txtSemesterArabicName.Text = String.Empty
        SemesterId = 0
    End Sub

#End Region

End Class
