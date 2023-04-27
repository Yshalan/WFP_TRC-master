Imports System.Data
Imports TA.Employees
Imports TA.Lookup
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.Admin
Imports SmartV.Version
Imports TA.DailyTasks
Imports TA.Security

Partial Class Emp_EmpWorkLocations
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_WorkLocation As Emp_WorkLocation
    Private objTaPolicy As TAPolicy
    Private objOrgCompany As New OrgCompany
    Private objVersion As SmartV.Version.version
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Properties"

    Private Property WorkLocationId() As Integer
        Get
            Return ViewState("WorkLocationId")
        End Get
        Set(ByVal value As Integer)
            ViewState("WorkLocationId") = value
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
            ' FillGridView()
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            FillList()
            userCtrlWorkLocation.HeaderText = ResourceManager.GetString("ComWorkLocations", CultureInfo)
            btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdVwWorkLocation.ClientID + "');")

        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not UP_WorkLocation.FindControl(row("AddBtnName")) Is Nothing Then
                        UP_WorkLocation.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not UP_WorkLocation.FindControl(row("DeleteBtnName")) Is Nothing Then
                        UP_WorkLocation.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not UP_WorkLocation.FindControl(row("EditBtnName")) Is Nothing Then
                        UP_WorkLocation.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not UP_WorkLocation.FindControl(row("PrintBtnName")) Is Nothing Then
                        UP_WorkLocation.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub dgrdVwWorkLocation_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdVwWorkLocation.Skin))
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        objEmp_WorkLocation = New Emp_WorkLocation
        Dim errorNum As Integer = -1
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        ' Set data into object for Add / Update
        With objEmp_WorkLocation
            .FK_CompanyId = RadCmbBxCompanyId.SelectedValue
            .WorkLocationArabicName = txtWorkLocationArabicName.Text
            .WorkLocationName = txtWorkLocationName.Text
            .WorkLocationCode = txtWorkLocationCode.Text
            .Active = True
            .FK_TAPolicyId = RadCmbBxPolicy.SelectedValue
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            ' .LAST_UPDATE_DATE = Today.Date
            .CREATED_BY = SessionVariables.LoginUser.ID
            ' .CREATED_DATE = Today.Date
        End With
        If WorkLocationId = 0 Then
            ' Do add operation 
            errorNum = objEmp_WorkLocation.Add()

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
            objEmp_WorkLocation.WorkLocationId = WorkLocationId
            errorNum = objEmp_WorkLocation.Update()


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

        End If
        If errorNum = 0 Then

            FillGridView()
            ClearAll()
        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1
        objEmp_WorkLocation = New Emp_WorkLocation
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdVwWorkLocation.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim strCode As String = row.GetDataKeyValue("WorkLocationCode").ToString()
                Dim intWorkLocationId As Integer = Convert.ToInt32(row.GetDataKeyValue("WorkLocationId").ToString())
                Dim objEmp_DesignationLeavesTypes As New Emp_DesignationLeavesTypes
                objEmp_WorkLocation = New Emp_WorkLocation()
                objEmp_WorkLocation.WorkLocationId = intWorkLocationId
                errNum = objEmp_WorkLocation.Delete()
                With strBuilder
                    If errNum = 0 Then
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
        FillGridView()
        ClearAll()
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click

        ClearAll()
    End Sub

    'Protected Sub dgrdVwWorkLocation_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdVwWorkLocation.ItemCommand
    '    If e.CommandName = "FilterRadGrid" Then
    '        RadFilter1.FireApplyCommand()
    '    End If
    'End Sub
    Protected Sub dgrdVwWorkLocation_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdVwWorkLocation.NeedDataSource
        FillGridView()
    End Sub

    Protected Sub dgrdVwWorkLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdVwWorkLocation.SelectedIndexChanged
        WorkLocationId = Convert.ToInt32(DirectCast(dgrdVwWorkLocation.SelectedItems(0), GridDataItem).GetDataKeyValue("WorkLocationId").ToString())
        FillControls()
    End Sub

    Protected Sub RadCmbBxCompanyId_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompanyId.SelectedIndexChanged
        RadFilter1.RootGroup.Expressions.Clear()
        RadFilter1.RecreateControl()

        FillGridView()
        RadFilter1.FireApplyCommand()
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

#End Region

#Region "Methods"

    Private Sub FillGridView()
        Try
            If RadCmbBxCompanyId.SelectedValue > 0 Then
                objEmp_WorkLocation = New Emp_WorkLocation()
                objEmp_WorkLocation.FK_CompanyId = RadCmbBxCompanyId.SelectedValue
                dgrdVwWorkLocation.DataSource = objEmp_WorkLocation.GetAllPolicyByCompany()
                dgrdVwWorkLocation.DataBind()
            Else
                dgrdVwWorkLocation.DataSource = New DataTable
                dgrdVwWorkLocation.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillList()

        ' Fill terlerik Drop down list represents the FK for the work location  
        objTaPolicy = New TAPolicy()
        Dim dt As DataTable = objTaPolicy.GetAll()
        If dt IsNot Nothing Then
            ProjectCommon.FillRadComboBox(RadCmbBxPolicy, dt, "TAPolicyName", "TAPolicyArabicName")
        End If

        objOrgCompany = New OrgCompany
        CtlCommon.FillTelerikDropDownList(RadCmbBxCompanyId, objOrgCompany.GetAllforddl, Lang)

        If (objVersion.HasMultiCompany = False) Then
            RadCmbBxCompanyId.SelectedValue = objVersion.GetCompanyId()
            trCompany.Visible = False
        End If

    End Sub

    Private Sub ClearAll()
        ' Reset controls
        txtWorkLocationArabicName.Text = String.Empty
        txtWorkLocationCode.Text = String.Empty
        txtWorkLocationName.Text = String.Empty
        If (objVersion.HasMultiCompany()) Then
            RadCmbBxCompanyId.SelectedIndex = 0
        End If
        RadCmbBxPolicy.SelectedIndex = 0


        'chckActive.Checked = False
        WorkLocationId = 0
        FillGridView()
    End Sub

    Private Sub FillControls()
        objEmp_WorkLocation = New Emp_WorkLocation()
        objEmp_WorkLocation.WorkLocationId = WorkLocationId
        objEmp_WorkLocation.GetByPK()
        With objEmp_WorkLocation
            txtWorkLocationCode.Text = .WorkLocationCode
            txtWorkLocationName.Text = .WorkLocationName
            txtWorkLocationArabicName.Text = .WorkLocationArabicName
            RadCmbBxCompanyId.SelectedValue = .FK_CompanyId
            RadCmbBxPolicy.SelectedValue = .FK_TAPolicyId
            'chckActive.Checked = .Active
        End With
    End Sub

#End Region

End Class
