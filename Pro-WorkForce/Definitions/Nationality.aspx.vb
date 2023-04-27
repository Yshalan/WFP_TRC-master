Imports TA.Definitions
Imports SmartV.UTILITIES
Imports System.Data
Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports TA.Security
Imports SmartV.UTILITIES.ProjectCommon

Partial Class Emp_EMP_Nationality
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_Nationality As Emp_Nationality
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Properties"

    Private Property NationalityId() As Integer
        Get
            Return ViewState("NationalityId")
        End Get
        Set(ByVal value As Integer)
            ViewState("NationalityId") = value
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

#Region "Page events"

    Protected Sub dgrdVwEsubNationality_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdVwEsubNationality.Skin))
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

            FillGridView()
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            UserCtrlNationality.HeaderText = ResourceManager.GetString("Nationalities", CultureInfo)
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdVwEsubNationality.ClientID + "');")

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

    Protected Sub btnSave_Click(ByVal sender As Object, _
                                ByVal e As System.EventArgs) Handles btnSave.Click
        objEmp_Nationality = New Emp_Nationality
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim errorNum As Integer = -1
        With objEmp_Nationality
            ' Set data into object for Add / Update
            .NationalityCode = txtNationalityCode.Text.Trim()
            .NationalityName = txtNationalityName.Text.Trim()
            .NationalityArabicName = txtNationalityarabicName.Text.Trim()
        End With
        If NationalityId = 0 Then
            ' Do add operation 
            errorNum = objEmp_Nationality.Add()
            If errorNum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ElseIf errorNum = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
            ElseIf errorNum = -6 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
            ElseIf errorNum = -7 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        Else
            ' Do update operations
            objEmp_Nationality.NationalityId = NationalityId
            errorNum = objEmp_Nationality.Update()
            If errorNum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            ElseIf errorNum = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
            ElseIf errorNum = -6 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
            ElseIf errorNum = -7 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If
        End If
        If errorNum = 0 Then
            FillGridView()
            ClearAll()
        End If
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, _
                                 ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
        FillGridView()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdVwEsubNationality.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim strCode As String = row.GetDataKeyValue("NationalityCode").ToString()
                Dim intNationalityId As Integer = Convert.ToInt32(row.GetDataKeyValue("NationalityId").ToString())
                objEmp_Nationality = New Emp_Nationality()
                objEmp_Nationality.NationalityId = intNationalityId
                errNum = objEmp_Nationality.Delete()
                With strBuilder

                End With
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGridView()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
            'showResult(CodeResultMessage.CodeDeleteFail)

        End If

        ClearAll()
    End Sub

    Protected Sub dgrdVwEsubNationality_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdVwEsubNationality.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item) Then
            Dim Itm As GridDataItem = e.Item
        End If
    End Sub

    Protected Sub dgrdVwEsubNationality_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdVwEsubNationality.NeedDataSource
        Try
            objEmp_Nationality = New Emp_Nationality()
            dgrdVwEsubNationality.DataSource = objEmp_Nationality.GetAll()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dgrdVwEsubNationality_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdVwEsubNationality.SelectedIndexChanged
        If dgrdVwEsubNationality.SelectedItems.Count = 1 Then
            NationalityId = DirectCast(dgrdVwEsubNationality.SelectedItems(0), GridDataItem).GetDataKeyValue("NationalityId").ToString().Trim
            FillControls()
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillGridView()
        Try
            objEmp_Nationality = New Emp_Nationality()
            dgrdVwEsubNationality.DataSource = objEmp_Nationality.GetAll()
            dgrdVwEsubNationality.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClearAll()
        ' Clear controls
        txtNationalityarabicName.Text = String.Empty
        txtNationalityCode.Text = String.Empty
        txtNationalityName.Text = String.Empty
        ' reset to id to next add
        NationalityId = 0
        ' Remove sorting and sorting direction

    End Sub

    Private Sub FillControls()
        ' Get the data by the PK and display on the controls
        objEmp_Nationality = New Emp_Nationality
        objEmp_Nationality.NationalityId = NationalityId
        objEmp_Nationality.GetByPK()
        With objEmp_Nationality
            txtNationalityCode.Text = .NationalityCode
            txtNationalityName.Text = .NationalityName
            txtNationalityarabicName.Text = .NationalityArabicName
        End With
    End Sub

#End Region

End Class
