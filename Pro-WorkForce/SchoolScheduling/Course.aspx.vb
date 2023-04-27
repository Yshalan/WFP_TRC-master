Imports Telerik.Web.UI
Imports SmartV.UTILITIES
Imports System.Web.UI.WebControls
Imports TA_SchoolScheduling
Imports System.Data
Imports TA.Security


Partial Class SchoolScheduling_Course
    Inherits System.Web.UI.Page

#Region "Class Variables"
    Private objCourse As Course
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
#End Region

#Region "Properties"
    Public Property Lang() As CtlCommon.Lang
        Get
            Return ViewState("Lang")
        End Get
        Set(ByVal value As CtlCommon.Lang)
            ViewState("Lang") = value
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

    Public Property CourseId() As Integer
        Get
            Return ViewState("CourseId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CourseId") = value
        End Set
    End Property
#End Region

#Region "Page events"

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

        End If
        ibtnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdCourse.ClientID + "')")

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

    Protected Sub ibtnSave_Click(sender As Object, e As System.EventArgs) Handles ibtnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objCourse = New Course
        Dim err As Integer
        With objCourse
            .CourseCode = TxtCourseCode.Text
            .CourseName = TxtCourseName.Text
            .CourseNameAr = txtCourseNameAr.Text
            .Color = IIf(txtTKColor.Text.Trim() = "", "#FFFFFF", txtTKColor.Text)
            .CREATED_BY = SessionVariables.LoginUser.ID
            .CREATED_DATE = Now
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_DATE = Now
        End With
        If CourseId = 0 Then

            err = objCourse.Add()
            If err = 0 Then
                CourseId = objCourse.CourseId
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully"), "success")
                ClearAll()
                FillGrid()
            ElseIf err = -1 Or err = -11 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave"), "error")
            End If
        Else
            objCourse.CourseId = CourseId
            objCourse.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            objCourse.LAST_UPDATE_DATE = Now
            err = objCourse.Update()

            If err = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully"), "success")
                ClearAll()
                FillGrid()
            ElseIf err = -1 Or err = -11 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate"), "error")
            End If
        End If

        If err = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo))
        ElseIf err = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo))
        ElseIf err = -7 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo))
        End If
    End Sub


    Protected Sub ibtnDelete_Click(sender As Object, e As System.EventArgs) Handles ibtnDelete.Click
        Dim err As Integer
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdCourse.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intGradeId As Integer = Convert.ToInt32(row.GetDataKeyValue("CourseId"))
                Dim strCode As String = row.GetDataKeyValue("CourseCode").ToString()
                objCourse = New Course
                objCourse.CourseId = intGradeId
                objCourse.Delete()
                With strBuilder
                    If err = 0 Then
                        .Append(strCode & " Deleted")
                        .Append("\n")
                    Else
                        .Append(strCode & " Could't Delete")
                        .Append("\n")
                    End If

                End With
            End If
        Next
        CtlCommon.ShowMessage(Me.Page, strBuilder.ToString())
        ClearAll()
        FillGrid()
    End Sub

    Protected Sub ibtnRest_Click(sender As Object, e As System.EventArgs) Handles ibtnRest.Click
        ClearAll()
        FillGrid()
    End Sub

    Protected Sub dgrdCourse_DataBound(sender As Object, e As System.EventArgs) Handles dgrdCourse.DataBound
        Dim value As String = ""
        For Each item As GridDataItem In dgrdCourse.MasterTableView.Items
            Dim cell As TableCell = item("Color")
            If Not cell.Text = value Then
                value = cell.Text
                cell.Style.Item(HtmlTextWriterStyle.BackgroundColor) = value
                cell.Text = ""
            End If
        Next
    End Sub

    Protected Sub dgrdCourse_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdCourse.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdCourse_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdCourse.NeedDataSource
        Try
            objCourse = New Course()
            dgrdCourse.DataSource = objCourse.GetAll()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub dgrdCourse_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dgrdCourse.SelectedIndexChanged
        CourseId = CInt(DirectCast(dgrdCourse.SelectedItems(0), GridDataItem).GetDataKeyValue("CourseId").ToString().Trim)
        objCourse = New Course
        With objCourse
            .CourseId = CourseId
            .GetByPK()
            TxtCourseCode.Text = .CourseCode
            TxtCourseName.Text = .CourseName
            txtCourseNameAr.Text = .CourseNameAr
            txtTKColor.Text = .Color
            txtTKColor.Style.Item(HtmlTextWriterStyle.BackgroundColor) = .Color
            txtTKColor.Style.Item(HtmlTextWriterStyle.Color) = .Color

        End With

    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub
#End Region

#Region "Methods"


    Public Sub FillGrid()
        Try
            objCourse = New Course()

            dgrdCourse.DataSource = objCourse.GetAll()
            dgrdCourse.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ClearAll()
        TxtCourseCode.Text = String.Empty
        TxtCourseName.Text = String.Empty
        txtCourseNameAr.Text = String.Empty
        txtTKColor.Text = "#FFFFFF"
        txtTKColor.Style.Item(HtmlTextWriterStyle.BackgroundColor) = "#FFFFFF"
        txtTKColor.Style.Item(HtmlTextWriterStyle.Color) = "#FFFFFF"
        CourseId = 0
        FillGrid()
    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdCourse.Skin))
    End Function
#End Region





End Class
