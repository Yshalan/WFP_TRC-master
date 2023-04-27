Imports TA.Employees
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Security
Imports System.IO
Imports System.Net

Partial Class Employee_CardLayOut_Reports
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objCard_Template As Card_Template

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

    Public Property TemplateFilePath() As String
        Get
            Return ViewState("TemplateFilePath")
        End Get
        Set(ByVal value As String)
            ViewState("TemplateFilePath") = value
        End Set
    End Property

    Public Property TemplateId() As Integer
        Get
            Return ViewState("TemplateId")
        End Get
        Set(ByVal value As Integer)
            ViewState("TemplateId") = value
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

    Protected Sub Employee_CardLayOut_Reports_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            FillGrid()
            pageHeader1.HeaderText = ResourceManager.GetString("CardTemplates", CultureInfo)
        End If


        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdTemplate.ClientID + "');")

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

    Protected Sub dgrdTemplate_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If (e.CommandName = "download") Then
            TemplateId = e.CommandArgument
            DownloadFile()
        ElseIf e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdTemplate.Skin))
    End Function

    Protected Sub dgrdTemplate_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdTemplate.NeedDataSource
        Dim dt As DataTable
        objCard_Template = New Card_Template
        With objCard_Template
            dt = .GetAll
            dgrdTemplate.DataSource = dt
        End With
    End Sub

    Protected Sub dgrdTemplate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdTemplate.SelectedIndexChanged
        TemplateId = Convert.ToInt32(DirectCast(dgrdTemplate.SelectedItems(0), GridDataItem).GetDataKeyValue("DESIGN_ID").ToString())
        FillControls()
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Dim errno As Integer
        objCard_Template = New Card_Template
        With objCard_Template
            For Each row As GridDataItem In dgrdTemplate.Items
                If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                    Dim strCode As String = row.GetDataKeyValue("TemplateId").ToString()
                    objCard_Template = New Card_Template
                    objCard_Template.TemplateId = strCode
                    objCard_Template.GetByPK()
                    errno = objCard_Template.Delete()
                    If errno = 0 Then
                        Dim fPath As String
                        fPath = (objCard_Template.TemplateFilePath)
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

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        objCard_Template = New Card_Template
        Dim errno As Integer
        With objCard_Template
            .TemplateName = txtDesc.Text
            .TemplateArabicName = txtDescAr.Text
            .TemplateFilePath = String.Empty

            If TemplateId = Nothing Then
                errno = .Add
                TemplateId = .TemplateId
                UploadFile()
                .TemplateFilePath = TemplateFilePath
                errno = .Card_TemplatePath_Insert
            Else
                .TemplateId = TemplateId
                UploadFile()
                .TemplateFilePath = TemplateFilePath
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

#End Region

#Region "Methods"

    Private Sub ClearAll()
        txtDesc.Text = String.Empty
        txtDescAr.Text = String.Empty
        TemplateId = Nothing
    End Sub

    Private Sub FillGrid()
        Dim dt As DataTable
        objCard_Template = New Card_Template
        With objCard_Template
            dt = .GetAll
            dgrdTemplate.DataSource = dt
            dgrdTemplate.DataBind()
        End With
    End Sub

    Private Sub FillControls()
        objCard_Template = New Card_Template
        With objCard_Template
            .TemplateId = TemplateId
            .GetByPK()
            txtDesc.Text = .TemplateName
            txtDescAr.Text = .TemplateArabicName
            'FileFullPath = .DESIGN_PATH
        End With
    End Sub

    Protected Sub UploadFile()

        If Not FileUpload1.PostedFile Is Nothing Then
            Dim fileName As String = TemplateId
            Dim filepath As String = ConfigurationManager.AppSettings("CardLayOutPath").ToString
            Dim validFileTypes As String() = {"rpt", "RPT"}
            Dim ext As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName)
            Dim isValidFile As Boolean = False
            For i As Integer = 0 To validFileTypes.Length - 1
                If ext = "." & validFileTypes(i) Then
                    isValidFile = True
                    Exit For
                End If
            Next

            If isValidFile Then
                TemplateFilePath = filepath + fileName + ext
                FileUpload1.PostedFile.SaveAs(TemplateFilePath)
                CtlCommon.ShowMessage(Me.Page, "Uploaded Successfully", "success")
            Else
                CtlCommon.ShowMessage(Me.Page, "File Extension is not valid. Valid Extensions are " & String.Join(", ", validFileTypes), "info")
            End If
        End If
    End Sub

    Private Sub DownloadFile()
        objCard_Template = New Card_Template
        With objCard_Template
            .TemplateId = TemplateId
            .GetByPK()
            TemplateFilePath = .TemplateFilePath
            If Not .TemplateFilePath = Nothing Then
                Dim strURL As String = TemplateFilePath
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

#End Region


End Class
