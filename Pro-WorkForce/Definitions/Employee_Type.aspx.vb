Imports Telerik.Web.UI
Imports SmartV.UTILITIES
Imports TA.Security
Imports System.Data
Imports TA.Definitions

Partial Class Definitions_Employee_Type
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmployee_Type As Employee_Type
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Properties"

    Private Property EmployeeTypeId() As Integer
        Get
            Return ViewState("EmployeeTypeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeTypeId") = value
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

    Protected Sub dgrdVwEmployeeType_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdVwEmployeeType.Skin))
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
            PageHeader1.HeaderText = ResourceManager.GetString("EmployeeType", CultureInfo)
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdVwEmployeeType.ClientID + "');")
        'btnDelete.OnClientClick = CtlCommon.ValidateDeletedGridWithConfirmation(dgrdVwEmployeeType.ClientID, btnDelete.UniqueID, "Are you sure you want to delete?", Lang.ToString)

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not pnlEmployeeType.FindControl(row("AddBtnName")) Is Nothing Then
                        pnlEmployeeType.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not pnlEmployeeType.FindControl(row("DeleteBtnName")) Is Nothing Then
                        pnlEmployeeType.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not pnlEmployeeType.FindControl(row("EditBtnName")) Is Nothing Then
                        pnlEmployeeType.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not pnlEmployeeType.FindControl(row("PrintBtnName")) Is Nothing Then
                        pnlEmployeeType.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        objEmployee_Type = New Employee_Type
        Dim err = -1
        With objEmployee_Type
            .TypeName_En = txtTypeName.Text
            .TypeName_Ar = txtTypeNamear.Text
            .IsInternaltype = chkIsInternal.Checked
            .EmployeeNumberInitial = txtEmployeeNumberInitial.Text
            If EmployeeTypeId = 0 Then
                err = .Add
            Else
                .EmployeeTypeId = EmployeeTypeId
                err = .Update
            End If
        End With
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGridView()
            ClearAll()
        ElseIf err = -5 Then
            CtlCommon.ShowMessage(Me, ResourceManager.GetString("NameExist", CultureInfo), "info")
        ElseIf err = -6 Then
            CtlCommon.ShowMessage(Me, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdVwEmployeeType.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objEmployee_Type = New Employee_Type
                objEmployee_Type.EmployeeTypeId = Convert.ToInt32(row.GetDataKeyValue("EmployeeTypeId").ToString())
                errNum = objEmployee_Type.Delete()

            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGridView()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        End If

        ClearAll()

    End Sub

    Protected Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        ClearAll()
        FillGridView()
    End Sub

    Protected Sub dgrdVwEmployeeType_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdVwEmployeeType.NeedDataSource
        objEmployee_Type = New Employee_Type
        dgrdVwEmployeeType.DataSource = objEmployee_Type.GetAll()
    End Sub

    Protected Sub dgrdVwEmployeeType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdVwEmployeeType.SelectedIndexChanged
        EmployeeTypeId = Convert.ToInt32(DirectCast(dgrdVwEmployeeType.SelectedItems(0), GridDataItem).GetDataKeyValue("EmployeeTypeId").ToString())
        FillControls()
    End Sub


#End Region

#Region "Methods"

    Private Sub FillGridView()
        Try
            objEmployee_Type = New Employee_Type
            dgrdVwEmployeeType.DataSource = objEmployee_Type.GetAll()
            dgrdVwEmployeeType.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClearAll()
        EmployeeTypeId = 0
        txtTypeName.Text = String.Empty
        txtTypeNamear.Text = String.Empty
        chkIsInternal.Checked = True
        txtEmployeeNumberInitial.Text = String.Empty
    End Sub

    Private Sub FillControls()
        objEmployee_Type = New Employee_Type
        objEmployee_Type.EmployeeTypeId = EmployeeTypeId
        objEmployee_Type.GetByPK()
        With objEmployee_Type
            txtTypeName.Text = .TypeName_En
            txtTypeNamear.Text = .TypeName_Ar
            chkIsInternal.Checked = .IsInternaltype
            txtEmployeeNumberInitial.Text = .EmployeeNumberInitial
        End With
    End Sub

#End Region

End Class
