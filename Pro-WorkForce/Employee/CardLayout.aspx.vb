Imports Telerik.Web.UI
Imports SmartV.UTILITIES
Imports TA.Security
Imports System.Data
Imports TA.Card
Imports SmartV.UTILITIES.ProjectCommon
Imports System.Net
Imports System.IO

Partial Class Employee_CardLayout
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objID_CARD_DESIGNS As ID_CARD_DESIGNS
    Dim UploadedFile As System.Web.HttpPostedFile


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

    Public Property FileFullPath() As String
        Get
            Return ViewState("FileFullPath")
        End Get
        Set(ByVal value As String)
            ViewState("FileFullPath") = value
        End Set
    End Property

    Public Property DesignId() As String
        Get
            Return ViewState("DESIGN_ID")
        End Get
        Set(ByVal value As String)
            ViewState("DESIGN_ID") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub dgrdLayOut_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If (e.CommandName = "download") Then
            DesignId = e.CommandArgument
            DownloadFile()
        ElseIf e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdLayOut.Skin))
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
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            FillGrid()
            pageHeader1.HeaderText = ResourceManager.GetString("CardLayOut", CultureInfo)
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdLayOut.ClientID + "');")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not divControls.FindControl(row("AddBtnName")) Is Nothing Then
                        divControls.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not divControls.FindControl(row("DeleteBtnName")) Is Nothing Then
                        divControls.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not divControls.FindControl(row("EditBtnName")) Is Nothing Then
                        divControls.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not divControls.FindControl(row("PrintBtnName")) Is Nothing Then
                        divControls.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        objID_CARD_DESIGNS = New ID_CARD_DESIGNS
        Dim errno As Integer
        With objID_CARD_DESIGNS

            .Get_MaxDesignId()
            .DESIGN_ID = Convert.ToInt32(.DESIGN_ID) + 1

            .DESIGN_DESC = txtDesc.Text
            .DESIGN_ARB_DESC = txtDescAr.Text

            If DesignId = Nothing Then
                DesignId = .DESIGN_ID
                UploadFile()
                .DESIGN_PATH = FileFullPath
                errno = .Add
            Else
                .DESIGN_ID = DesignId
                UploadFile()
                .DESIGN_PATH = FileFullPath
                errno = .Update
            End If
        End With

        If errno = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGrid()
            ClearAll()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Dim errno As Integer
        With objID_CARD_DESIGNS

            Dim strBuilder As New StringBuilder
            For Each row As GridDataItem In dgrdLayOut.Items
                If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                    Dim strCode As String = row.GetDataKeyValue("DESIGN_ID").ToString()
                    objID_CARD_DESIGNS = New ID_CARD_DESIGNS
                    objID_CARD_DESIGNS.DESIGN_ID = strCode
                    objID_CARD_DESIGNS.GetByPK()
                    ' objID_CARD_DESIGNS.DESIGN_ID = Convert.ToInt32(row("DESIGN_ID").Text)
                    errno = objID_CARD_DESIGNS.Delete()

                    If errno = 0 Then
                        Dim fPath As String
                        fPath = (objID_CARD_DESIGNS.DESIGN_PATH)
                        If File.Exists(fPath) Then
                            File.Delete(fPath)
                        End If
                    End If

                End If
            Next
            If errno = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
                FillGrid()
                ClearAll()
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
            End If
        End With
    End Sub 

    Protected Sub dgrdLayOut_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdLayOut.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item
            'If Not item("").ToString = "" Then

            'End If
        End If

    End Sub

    Protected Sub dgrdLayOut_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdLayOut.SelectedIndexChanged
        DesignId = Convert.ToInt32(DirectCast(dgrdLayOut.SelectedItems(0), GridDataItem).GetDataKeyValue("DESIGN_ID").ToString())
        FillControls()
    End Sub

    Protected Sub dgrdLayOut_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdLayOut.NeedDataSource
        objID_CARD_DESIGNS = New ID_CARD_DESIGNS
        With objID_CARD_DESIGNS
            dgrdLayOut.DataSource = .GetAll
        End With
    End Sub

#End Region

#Region "Methods"

    Private Sub FillGrid()

        objID_CARD_DESIGNS = New ID_CARD_DESIGNS
        With objID_CARD_DESIGNS
            dgrdLayOut.DataSource = .GetAll
            dgrdLayOut.DataBind()
        End With

    End Sub

    Protected Sub UploadFile()

        If Not FileUpload1.PostedFile Is Nothing Then
            Dim fileName As String = DesignId
            Dim filepath As String = ConfigurationManager.AppSettings("CardLayOutPath").ToString
            Dim validFileTypes As String() = {"lyt", "LYT"}
            Dim ext As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName)
            Dim isValidFile As Boolean = False
            For i As Integer = 0 To validFileTypes.Length - 1
                If ext = "." & validFileTypes(i) Then
                    isValidFile = True
                    Exit For
                End If
            Next

            If isValidFile Then
                FileFullPath = filepath + fileName + ext
                FileUpload1.PostedFile.SaveAs(FileFullPath)
                CtlCommon.ShowMessage(Me.Page, "Uploaded Successfully", "success")
            Else
                CtlCommon.ShowMessage(Me.Page, "File Extension is not valid. Valid Extensions are " & String.Join(", ", validFileTypes), "info")
            End If
        End If
    End Sub

    Private Sub DownloadFile()
        objID_CARD_DESIGNS = New ID_CARD_DESIGNS
        With objID_CARD_DESIGNS
            .DESIGN_ID = DesignId
            .GetByPK()
            FileFullPath = .DESIGN_PATH
            If Not .DESIGN_PATH = Nothing Then
                Dim strURL As String = FileFullPath
                Dim req As New WebClient()
                Dim response As HttpResponse = HttpContext.Current.Response
                response.Clear()
                response.ClearContent()
                response.ClearHeaders()
                response.Buffer = True
                response.AddHeader("Content-Disposition", "attachment;filename=""" + (strURL) + """")
                Dim data As Byte() = req.DownloadData(strURL)
                response.BinaryWrite(data)
                response.[End]()
            End If
        End With
    End Sub

    Private Sub FillControls()
        objID_CARD_DESIGNS = New ID_CARD_DESIGNS
        With objID_CARD_DESIGNS
            .DESIGN_ID = DesignId
            .GetByPK()
            txtDesc.Text = .DESIGN_DESC
            txtDescAr.Text = .DESIGN_ARB_DESC
            FileFullPath = .DESIGN_PATH
        End With
    End Sub

    Private Sub ClearAll()
        txtDesc.Text = String.Empty
        txtDescAr.Text = String.Empty
        DesignId = Nothing
    End Sub

#End Region


End Class
