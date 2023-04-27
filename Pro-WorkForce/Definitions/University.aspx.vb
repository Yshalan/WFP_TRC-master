
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Definitions
Imports TA.Security

Partial Class Definitions_University
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objEmp_University As Emp_University

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

    Public Property UniversityId As Integer
        Get
            Return ViewState("UniversityId")
        End Get
        Set(ByVal value As Integer)
            ViewState("UniversityId") = value
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

    Private Sub Definitions_University_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            FillGrid()
            PageHeader1.HeaderText = ResourceManager.GetString("DefineUniversity", CultureInfo)
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdUniversities.ClientID + "');")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not update1.FindControl(row("AddBtnName")) Is Nothing Then
                        update1.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not update1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        update1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not update1.FindControl(row("EditBtnName")) Is Nothing Then
                        update1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not update1.FindControl(row("PrintBtnName")) Is Nothing Then
                        update1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub dgrdUniversities_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdUniversities.Skin))
    End Function

    Private Sub dgrdUniversities_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdUniversities.NeedDataSource
        objEmp_University = New Emp_University
        With objEmp_University
            dgrdUniversities.DataSource = .GetAll
        End With
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Private Sub dgrdUniversities_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdUniversities.SelectedIndexChanged
        UniversityId = Convert.ToInt32(DirectCast(dgrdUniversities.SelectedItems(0), GridDataItem).GetDataKeyValue("UniversityId").ToString())
        FillControls()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim err As Integer = -1
        objEmp_University = New Emp_University
        With objEmp_University
            .UniversityShortName = txtUniversityShortName.Text
            .UniversityName = txtUniversityName.Text
            .UniversityArabicName = txtUniversityArabicName.Text
            .PhoneNo = txtPhoneNo.Text
            .Address = txtAddress.Text

            If UniversityId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add
            Else
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .UniversityId = UniversityId
                err = .Update
            End If
        End With

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGrid()
            ClearAll()
        ElseIf err = -2 Then
            CtlCommon.ShowMessage(Me, ResourceManager.GetString("ShortNameExist", CultureInfo), "info")
        ElseIf err = -3 Then
            CtlCommon.ShowMessage(Me, ResourceManager.GetString("NameExist", CultureInfo), "info")
        ElseIf err = -4 Then
            CtlCommon.ShowMessage(Me, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        For Each row As GridDataItem In dgrdUniversities.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objEmp_University = New Emp_University
                objEmp_University.UniversityId = Convert.ToInt32(row.GetDataKeyValue("UniversityId").ToString())
                errNum = objEmp_University.Delete()

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

#End Region

#Region "Methods"

    Private Sub FillGrid()
        objEmp_University = New Emp_University
        With objEmp_University
            dgrdUniversities.DataSource = .GetAll
            dgrdUniversities.DataBind()
        End With
    End Sub

    Private Sub ClearAll()
        txtUniversityShortName.Text = String.Empty
        txtUniversityName.Text = String.Empty
        txtUniversityArabicName.Text = String.Empty
        txtPhoneNo.Text = String.Empty
        txtAddress.Text = String.Empty
        UniversityId = 0
    End Sub

    Private Sub FillControls()
        objEmp_University = New Emp_University
        With objEmp_University
            .UniversityId = UniversityId
            .GetByPK()
            txtUniversityShortName.Text = .UniversityShortName
            txtUniversityName.Text = .UniversityName
            txtUniversityArabicName.Text = .UniversityArabicName
            txtPhoneNo.Text = .PhoneNo
            txtAddress.Text = .Address
        End With
    End Sub

#End Region


End Class
