Imports System.Data
Imports TA.Employees
Imports SmartV.UTILITIES
Imports System.Web.UI.WebControls
Imports Telerik.Web.UI
Imports TA.Admin
Imports TA.Security

Partial Class Emp_EmpStatus
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_Status As Emp_Status
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Properties"

    Private Property StatusId() As Integer
        Get
            Return ViewState("StatusId")
        End Get
        Set(ByVal value As Integer)
            ViewState("StatusId") = value
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

#Region "PageEvents"

    Protected Sub dgrdVwEmpStatus_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdVwEmpStatus.Skin))
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
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            FillGridView()
            UserCtrlEmployeeStatus.HeaderText = ResourceManager.GetString("EmpStatus", CultureInfo)
            btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdVwEmpStatus.ClientID + "');")
        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not pnlEmpStatus.FindControl(row("AddBtnName")) Is Nothing Then
                        pnlEmpStatus.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not pnlEmpStatus.FindControl(row("DeleteBtnName")) Is Nothing Then
                        pnlEmpStatus.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not pnlEmpStatus.FindControl(row("EditBtnName")) Is Nothing Then
                        pnlEmpStatus.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not pnlEmpStatus.FindControl(row("PrintBtnName")) Is Nothing Then
                        pnlEmpStatus.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillGridView()
        ClearAll()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objEmp_Status = New Emp_Status()
        Dim errorNum As Integer = -1
        ' Set data into object for Add / Update
        With objEmp_Status
            .statusCode = txtStatusCode.Text
            .StatusName = txtName.Text
            .StatusArabicName = txtArabicName.Text
            .StatusDescription = txtDescribtion.Text
            .statusCode = txtStatusCode.Text
            .CosiderEmployeeActive = chBxConsiderActive.Checked
        End With
        If StatusId = 0 Then
            ' Do add operation 
            errorNum = objEmp_Status.Add()

            If errorNum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ElseIf errorNum = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("StatusCodeExist", CultureInfo), "info")
            ElseIf errorNum = -6 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo), "info")
            ElseIf errorNum = -7 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If

        Else
            ' Do update operations
            objEmp_Status.StatusId = StatusId
            errorNum = objEmp_Status.Update()

            If errorNum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ElseIf errorNum = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("StatusCodeExist", CultureInfo), "info")
            ElseIf errorNum = -6 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo), "info")
            ElseIf errorNum = -7 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        End If

        If errorNum = 0 Then

            FillGridView()
            ClearAll()
        Else

        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdVwEmpStatus.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intReligionId As Integer = row.GetDataKeyValue("StatusId").ToString().Trim
                ' Delete current checked item
                objEmp_Status = New Emp_Status()
                objEmp_Status.StatusId = intReligionId
                errNum = objEmp_Status.Delete()
            End If
        Next

        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGridView()
            ClearAll()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If
      

    End Sub

    Protected Sub dgrdVwEmpStatus_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdVwEmpStatus.NeedDataSource
        Try
            objEmp_Status = New Emp_Status()
            objEmp_Status.StatusId = StatusId
            dgrdVwEmpStatus.DataSource = objEmp_Status.GetAll()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dgrdVwEmpStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdVwEmpStatus.SelectedIndexChanged
        If dgrdVwEmpStatus.SelectedItems.Count = 1 Then
            StatusId = CType(dgrdVwEmpStatus.SelectedItems(0), GridDataItem).GetDataKeyValue("StatusId").ToString()
            FillControls()
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillControls()
        objEmp_Status = New Emp_Status()
        objEmp_Status.StatusId = StatusId
        objEmp_Status.GetByPK()
        With objEmp_Status
            txtStatusCode.Text = .statusCode
            txtName.Text = .StatusName
            txtArabicName.Text = .StatusArabicName
            txtDescribtion.Text = .StatusDescription
            chBxConsiderActive.Checked = .CosiderEmployeeActive
        End With
    End Sub

    Private Sub FillGridView()
        Try

            objEmp_Status = New Emp_Status()
            objEmp_Status.StatusId = StatusId
            dgrdVwEmpStatus.DataSource = objEmp_Status.GetAll()
            dgrdVwEmpStatus.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClearAll()
        'Clear Controls
        txtStatusCode.Text = String.Empty
        txtArabicName.Text = String.Empty
        txtDescribtion.Text = String.Empty
        txtName.Text = String.Empty
        chBxConsiderActive.Checked = False
        ' Reset to prepare to the next add operation
        StatusId = 0
    End Sub

#End Region

End Class
