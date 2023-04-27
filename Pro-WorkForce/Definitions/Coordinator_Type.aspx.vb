Imports TA.Definitions
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Security

Partial Class Definitions_Coordinator_Type
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objCoordinator_Type As Coordinator_Type

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

    Public Property CoordinatorTypeId() As Integer
        Get
            Return ViewState("CoordinatorTypeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CoordinatorTypeId") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub dgrdCoordinatorType_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdCoordinatorType.Skin))
    End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            FillGrid()
            PageHeader1.HeaderText = ResourceManager.GetString("CoordinatorType", CultureInfo)
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdCoordinatorType.ClientID + "');")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not pnlCoordinator_Type.FindControl(row("AddBtnName")) Is Nothing Then
                        pnlCoordinator_Type.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not pnlCoordinator_Type.FindControl(row("DeleteBtnName")) Is Nothing Then
                        pnlCoordinator_Type.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not pnlCoordinator_Type.FindControl(row("EditBtnName")) Is Nothing Then
                        pnlCoordinator_Type.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not pnlCoordinator_Type.FindControl(row("PrintBtnName")) Is Nothing Then
                        pnlCoordinator_Type.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

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

    Protected Sub dgrdCoordinatorType_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdCoordinatorType.NeedDataSource
        objCoordinator_Type = New Coordinator_Type
        With objCoordinator_Type
            Dim dt As DataTable
            dt = .GetAll
            dgrdCoordinatorType.DataSource = dt
        End With
    End Sub

    Protected Sub dgrdCoordinatorType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdCoordinatorType.SelectedIndexChanged
        CoordinatorTypeId = Convert.ToInt32(DirectCast(dgrdCoordinatorType.SelectedItems(0), GridDataItem).GetDataKeyValue("CoordinatorTypeId").ToString())
        FillControls()
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdCoordinatorType.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objCoordinator_Type = New Coordinator_Type
                objCoordinator_Type.CoordinatorTypeId = Convert.ToInt32(row.GetDataKeyValue("CoordinatorTypeId").ToString())
                errNum = objCoordinator_Type.Delete()

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

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        objCoordinator_Type = New Coordinator_Type
        Dim err As Integer = -1
        With objCoordinator_Type
            .CoordinatorShortName = txtCoordinatorShortName.Text
            .CoordinatorTypeName = txtCoordinatorTypeName.Text
            .CoordinatorTypeArabicName = txtCoordinatorTypeArabicName.Text

            If CoordinatorTypeId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add
            Else
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .CoordinatorTypeId = CoordinatorTypeId
                err = .Update
            End If

        End With

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGrid()
            ClearAll()
        ElseIf err = -4 Then
            CtlCommon.ShowMessage(Me, ResourceManager.GetString("ShortNameExist", CultureInfo), "info")
        ElseIf err = -5 Then
            CtlCommon.ShowMessage(Me, ResourceManager.GetString("NameExist", CultureInfo), "info")
        ElseIf err = -6 Then
            CtlCommon.ShowMessage(Me, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillGrid()
        objCoordinator_Type = New Coordinator_Type
        With objCoordinator_Type
            Dim dt As DataTable
            dt = .GetAll
            dgrdCoordinatorType.DataSource = dt
            dgrdCoordinatorType.DataBind()
        End With
    End Sub

    Private Sub FillControls()
        objCoordinator_Type = New Coordinator_Type
        With objCoordinator_Type
            .CoordinatorTypeId = CoordinatorTypeId
            .GetByPK()
            txtCoordinatorShortName.Text = .CoordinatorShortName
            txtCoordinatorTypeName.Text = .CoordinatorTypeName
            txtCoordinatorTypeArabicName.Text = .CoordinatorTypeArabicName
        End With
    End Sub

    Private Sub ClearAll()
        txtCoordinatorShortName.Text = String.Empty
        txtCoordinatorTypeName.Text = String.Empty
        txtCoordinatorTypeArabicName.Text = String.Empty
        CoordinatorTypeId = 0
    End Sub

#End Region

End Class
