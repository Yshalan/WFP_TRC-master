Imports System.Data
Imports TA.Definitions
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.Security
Imports SmartV.UTILITIES.ProjectCommon

Partial Class Emp_MaritalStatus
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_MaritalStatus As EmpMaritalStatus
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Properties"

    Public Property MaritalStatusId() As Integer
        Get
            Return ViewState("MaritalStatusId")
        End Get
        Set(ByVal value As Integer)
            ViewState("MaritalStatusId") = value
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
            FillGridView()
            UserCtrlMaritalStatus.HeaderText = ResourceManager.GetString("MaritalStatus", CultureInfo)
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdMaritalStatus.ClientID + "');")

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

    Protected Sub btnClear_Click(ByVal sender As Object, _
                                 ByVal e As System.EventArgs) Handles btnClear.Click
        FillGridView()
        ClearAll()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, _
                                  ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1


        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdMaritalStatus.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim strCode As String = row.GetDataKeyValue("MaritalStatusCode").ToString()
                objEmp_MaritalStatus = New EmpMaritalStatus()
                objEmp_MaritalStatus.MaritalStatusId = Convert.ToInt32(row.GetDataKeyValue("MaritalStatusId").ToString())
                errNum = objEmp_MaritalStatus.Delete()

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

    Protected Sub btnSave_Click(ByVal sender As Object, _
                                ByVal e As System.EventArgs) Handles btnSave.Click
        objEmp_MaritalStatus = New EmpMaritalStatus
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim errorNum As Integer = -1
        ' Set data into object for Add / Update
        objEmp_MaritalStatus.MaritalStatusCode = txtMaritalstatusCode.Text.Trim()
        objEmp_MaritalStatus.MatitalStatusName = txtMaritalStatusName.Text.Trim()
        objEmp_MaritalStatus.MaritalStatusArabicName = txtMaritalStatusArabName.Text.Trim()
        If MaritalStatusId = 0 Then
            ' Do add operation 
            errorNum = objEmp_MaritalStatus.Add()

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
            objEmp_MaritalStatus.MaritalStatusId = MaritalStatusId
            errorNum = objEmp_MaritalStatus.Update()

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

    Protected Sub dgrdMaritalStatus_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdMaritalStatus.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdMaritalStatus_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdMaritalStatus.NeedDataSource
        objEmp_MaritalStatus = New EmpMaritalStatus()
        dgrdMaritalStatus.DataSource = objEmp_MaritalStatus.GetAll()
    End Sub

    Protected Sub dgrdMaritalStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdMaritalStatus.SelectedIndexChanged
        MaritalStatusId = Convert.ToInt32(DirectCast(dgrdMaritalStatus.SelectedItems(0), GridDataItem).GetDataKeyValue("MaritalStatusId").ToString())
        FillControls()
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

#End Region

#Region "Methods"

    Private Sub FillGridView()
        Try
            objEmp_MaritalStatus = New EmpMaritalStatus()
            dgrdMaritalStatus.DataSource = objEmp_MaritalStatus.GetAll()
            dgrdMaritalStatus.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClearAll()
        ' Reset the Controls
        txtMaritalstatusCode.Text = String.Empty
        txtMaritalStatusArabName.Text = String.Empty
        txtMaritalStatusName.Text = String.Empty
        ' Reset the Id and prepare to next add
        MaritalStatusId = 0
        ' Remove sorting and sorting arrows
    End Sub

    Private Sub FillControls()
        ' This function get A dataRow by PK , and display data 
        ' in the form controls
        objEmp_MaritalStatus = New EmpMaritalStatus()
        objEmp_MaritalStatus.MaritalStatusId = MaritalStatusId
        objEmp_MaritalStatus.GetByPK()
        txtMaritalStatusName.Text = objEmp_MaritalStatus.MatitalStatusName
        txtMaritalStatusArabName.Text = objEmp_MaritalStatus.MaritalStatusArabicName
        txtMaritalstatusCode.Text = objEmp_MaritalStatus.MaritalStatusCode
    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdMaritalStatus.Skin))
    End Function

#End Region

End Class
