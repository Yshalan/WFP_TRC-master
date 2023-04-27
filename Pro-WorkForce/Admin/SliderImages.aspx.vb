Imports System.Data
Imports TA.Admin
Imports System.IO
Imports SmartV.UTILITIES

Partial Class Admin_SliderImages
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objSlider_Images As Slider_Images
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Public Properties"

    Public Property ImageId() As String
        Get
            Return ViewState("ImageId")
        End Get
        Set(ByVal value As String)
            ViewState("ImageId") = value
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

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            FillGrid()
            PageHeader1.HeaderText = ResourceManager.GetString("SliderImages", CultureInfo)
        End If
        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdImages.ClientID + "')")
    End Sub

    Protected Sub dgrdImages_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdImages.NeedDataSource
        Dim dt As DataTable
        objSlider_Images = New Slider_Images
        With objSlider_Images
            dt = .GetAll()
            dgrdImages.DataSource = dt
        End With
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        objSlider_Images = New Slider_Images
        Dim err As Integer = -1
        If fuAttachFile.HasFile = True Then
            If ValidateImageExtension() = True Then
                With objSlider_Images
                    .ImageName = fuAttachFile.FileName
                    .ImageOrder = txtImageOrder.Text

                    Dim fileName As String = fuAttachFile.FileName
                    Dim fPath As String = String.Empty
                    fPath = HttpContext.Current.Server.MapPath("~/Images/SliderImages/" + fileName)
                    fuAttachFile.PostedFile.SaveAs(fPath)
                    .ImagePath = fPath
                    If ImageId = 0 Then
                        .CREATED_BY = SessionVariables.LoginUser.ID
                        err = .Add()
                    Else
                        .ImageId = ImageId
                        .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                        err = .Update()
                    End If

                    If err = 0 Then
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                        FillGrid()
                        ClearAll()
                    Else
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
                    End If

                End With
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("InsertValidImageType", CultureInfo), "info")
            End If

        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UploadImage", CultureInfo), "error")
        End If

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim err As Integer = -1
        objSlider_Images = New Slider_Images
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdImages.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intImageId As Integer = Convert.ToInt32(row.GetDataKeyValue("ImageId").ToString())
                objSlider_Images.ImageId = intImageId
                Dim filePath As String = String.Empty
                objSlider_Images.GetByPK()
                filePath = objSlider_Images.ImagePath
                If File.Exists(filePath) Then
                    File.Delete(filePath)
                End If
                err = objSlider_Images.Delete()
            End If
        Next
        If err = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGrid()
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If
        ClearAll()
        FillGrid()
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Sub dgrdImages_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdImages.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdImages.Skin))
    End Function

#End Region

#Region "Methods"

    Private Sub FillGrid()
        Dim dt As DataTable
        objSlider_Images = New Slider_Images
        With objSlider_Images
            dt = .GetAll()
            dgrdImages.DataSource = dt
            dgrdImages.DataBind()
        End With
    End Sub

    Private Sub ClearAll()
        txtImageOrder.Text = String.Empty
        ImageId = 0
    End Sub

    Private Function ValidateImageExtension() As Boolean
        Dim validFileTypes As String() = {"bmp", "gif", "png", "jpg", "jpeg"}
        Dim ext As String = System.IO.Path.GetExtension(fuAttachFile.PostedFile.FileName)
        Dim isValidFile As Boolean = False
        For i As Integer = 0 To validFileTypes.Length - 1
            If ext = "." & validFileTypes(i) Then
                isValidFile = True
                Exit For
            End If
        Next
        Return isValidFile
    End Function

#End Region

End Class
