Imports SmartV.UTILITIES
Imports TA.Lookup
Imports System.Data
Imports TA.Employees
Imports TA.Definitions
Imports TA.Admin
Imports TA.Security
Imports Telerik.Web.UI
Imports TA.Employees.Employee
Imports SmartV.Version

Partial Class Employee_UserControls_UserPrevilege_Companies
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objSYSUsers As SYSUsers
    Private objVersion As SmartV.Version.version
    Private objUserPrivileg_Companies As New UserPrivileg_Companies

#End Region

#Region "Properties"

    Private Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Private Property CompanyId() As Integer
        Get
            Return ViewState("CompanyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CompanyId") = value
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

    Protected Sub Employee_UserControls_UserPrevilege_Companies_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            objEmp_FilterCompany.IsEmployeeRequired = True
            objEmp_FilterCompany.CompanyRequiredValidationGroup = btnSave.ValidationGroup
            objEmp_FilterCompany.EmployeeRequiredValidationGroup = btnSave.ValidationGroup

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN

            End If

            'If (objVersion.HasMultiCompany() = False) Then
            '    RadCmbBxCompaniesCompany.SelectedValue = objVersion.GetCompanyId()
            '    RadCmbBxCompaniesCompany.Enabled = False
            '    'RadCmbBxCompaniesCompany.Visible = False
            '    'lblCompany.Visible = False
            'End If
            fillCompanies()
            fillgrid()

        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdManagers.ClientID + "');")


        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not Upanel1.FindControl(row("AddBtnName")) Is Nothing Then
                        Upanel1.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not Upanel1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        Upanel1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not Upanel1.FindControl(row("EditBtnName")) Is Nothing Then
                        Upanel1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not Upanel1.FindControl(row("PrintBtnName")) Is Nothing Then
                        Upanel1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub dgrdManagers_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdManagers.Skin))
    End Function

    Protected Sub dgrdManagers_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdManagers.NeedDataSource
        Try
            objUserPrivileg_Companies = New UserPrivileg_Companies
            dgrdManagers.DataSource = objUserPrivileg_Companies.GetManagerCompanies()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dgrdManagers_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdManagers.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item) Then
            Dim Itm As GridDataItem = e.Item
        End If
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
        objEmp_FilterCompany.ClearValues()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim objUserPrivileg_Companies = New UserPrivileg_Companies
        Dim errNum As Integer = -1
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdManagers.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim strCode As String = row.GetDataKeyValue("id").ToString()
                objUserPrivileg_Companies = New UserPrivileg_Companies
                objUserPrivileg_Companies.Id = Convert.ToInt32(row.GetDataKeyValue("id").ToString())
                errNum = objUserPrivileg_Companies.Delete()
                'With strBuilder
                '    If errNum = 0 Then
                '        .Append(strCode & " Deleted")
                '        .Append("\n")
                '    Else
                '        .Append(strCode & " Couldn't Delete")
                '        .Append("\n")
                '    End If

                'End With
            End If
        Next
        ClearAll()
        fillgrid()
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "info")
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim err As Integer = 0
        Dim flag As Boolean = False
        Dim objUserPrivileg_Companies = New UserPrivileg_Companies
        With objUserPrivileg_Companies
            EmployeeId = objEmp_FilterCompany.EmployeeId
            .FK_EmployeeId = EmployeeId

            For Each item As ListItem In cblCompanylist.Items
                If item.Selected Then
                    flag = True
                    .FK_CompanyId = item.Value
                    err = .Add()
                End If
            Next

            If err = 0 Then
                If flag = False Then
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("SelectCompany", CultureInfo), "info")
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                    ClearAll()
                    fillgrid()
                End If
            ElseIf err = -5 Or err = -11 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")

            End If

        End With
    End Sub

    'Protected Sub RadCmbBxCompanies_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompaniesCompany.SelectedIndexChanged
    '    If (RadCmbBxCompaniesCompany.SelectedValue <> -1) Then
    '        CompanyId = RadCmbBxCompaniesCompany.SelectedValue
    '    Else
    '        CompanyId = 0
    '    End If
    'End Sub

    Protected Sub dgrdManagers_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdManagers.SelectedIndexChanged
        FillControls()
    End Sub

#End Region

#Region "Methods"

    Private Sub fillCompanies()
        If SessionVariables.LoginUser IsNot Nothing Then
            objUserPrivileg_Companies = New UserPrivileg_Companies
            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()
            Dim dt As DataTable
            If Not objSYSUsers.FK_EmployeeId = Nothing Then
                objUserPrivileg_Companies.FK_EmployeeId = objSYSUsers.FK_EmployeeId
                objUserPrivileg_Companies.GetByEmpId()
            End If
            Dim objOrgCompany = New OrgCompany
            If (objUserPrivileg_Companies.FK_CompanyId <> 0) Then

                FillCompanyForUserSecurity()
            Else
                dt = objOrgCompany.GetAllforddl
                If (dt IsNot Nothing) Then
                    Dim dtEmployees = dt
                    If (dtEmployees IsNot Nothing) Then
                        If (dtEmployees.Rows.Count > 0) Then
                            Repeater1.Visible = True
                            Dim dtSource As New DataTable
                            dtSource.Columns.Add("CompanyId")
                            dtSource.Columns.Add("CompanyName")
                            Dim drRow As DataRow
                            drRow = dtSource.NewRow()
                            For Item As Integer = 0 To dtEmployees.Rows.Count - 1

                                Dim drSource As DataRow
                                drSource = dtSource.NewRow
                                Dim dcCell1 As New DataColumn
                                Dim dcCell2 As New DataColumn
                                dcCell1.ColumnName = "CompanyId"
                                dcCell2.ColumnName = "CompanyName"
                                dcCell1.DefaultValue = dtEmployees.Rows(Item)("CompanyId")
                                dcCell2.DefaultValue = IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)("CompanyName"), dtEmployees.Rows(Item)("CompanyArabicName"))
                                drSource("CompanyId") = dcCell1.DefaultValue
                                drSource("CompanyName") = dcCell2.DefaultValue
                                dtSource.Rows.Add(drSource)

                            Next
                            Dim dv As New DataView(dtSource)
                            If SessionVariables.CultureInfo = "ar-JO" Then
                                'dv.Sort = "EmployeeName"
                                For Each row As DataRowView In dv
                                    If (Not CompanyId = 0) Then
                                        If CompanyId = row("CompanyId") Then
                                            cblCompanylist.Items.Add(New ListItem(row("CompanyName").ToString(), row("CompanyId").ToString()))
                                            Exit For
                                        End If
                                    Else
                                        cblCompanylist.Items.Add(New ListItem(row("CompanyName").ToString(), row("CompanyId").ToString()))
                                    End If
                                Next
                            Else
                                For Each row As DataRowView In dv
                                    If (Not CompanyId = 0) Then
                                        If CompanyId = row("CompanyId") Then
                                            cblCompanylist.Items.Add(New ListItem(row("CompanyName").ToString(), row("CompanyId").ToString()))
                                            Exit For
                                        End If
                                    Else
                                        cblCompanylist.Items.Add(New ListItem(row("CompanyName").ToString(), row("CompanyId").ToString()))
                                    End If
                                Next
                            End If
                        Else
                            cblCompanylist.Items.Clear()
                        End If
                    End If
                End If

            End If

        End If
    End Sub

    Private Sub FillCompanyForUserSecurity()
        Dim objOrgCompany As New OrgCompany
        objOrgCompany.CompanyId = objUserPrivileg_Companies.FK_CompanyId
        Dim CompanyInfo = objOrgCompany.GetByPK()

        Dim dtCompanyInfo As New DataTable
        Dim dcCompanyValue As New DataColumn
        dcCompanyValue.ColumnName = "CompanyId"
        dcCompanyValue.DataType = GetType(Integer)

        Dim dcCompanyText As New DataColumn
        dcCompanyText.ColumnName = "CompanyName"
        dcCompanyText.DataType = GetType(String)

        Dim dcCompanyArabicText As New DataColumn
        dcCompanyArabicText.ColumnName = "CompanyArabicName"
        dcCompanyArabicText.DataType = GetType(String)


        dtCompanyInfo.Columns.Add(dcCompanyValue)
        dtCompanyInfo.Columns.Add(dcCompanyText)
        dtCompanyInfo.Columns.Add(dcCompanyArabicText)
        Dim drCompanyRow = dtCompanyInfo.NewRow()
        drCompanyRow("CompanyId") = CompanyInfo.CompanyId
        drCompanyRow("CompanyName") = CompanyInfo.CompanyName
        drCompanyRow("CompanyArabicName") = CompanyInfo.CompanyArabicName
        dtCompanyInfo.Rows.Add(drCompanyRow)

        If (dtCompanyInfo IsNot Nothing) Then
            Dim dtEmployees = dtCompanyInfo
            If (dtEmployees IsNot Nothing) Then
                If (dtEmployees.Rows.Count > 0) Then
                    Repeater1.Visible = True
                    Dim dtSource As New DataTable
                    dtSource.Columns.Add("CompanyId")
                    dtSource.Columns.Add("CompanyName")
                    Dim drRow As DataRow
                    drRow = dtSource.NewRow()
                    For Item As Integer = 0 To dtEmployees.Rows.Count - 1

                        Dim drSource As DataRow
                        drSource = dtSource.NewRow
                        Dim dcCell1 As New DataColumn
                        Dim dcCell2 As New DataColumn
                        dcCell1.ColumnName = "CompanyId"
                        dcCell2.ColumnName = "CompanyName"
                        dcCell1.DefaultValue = dtEmployees.Rows(Item)("CompanyId")
                        dcCell2.DefaultValue = IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)("CompanyName"), dtEmployees.Rows(Item)("CompanyArabicName"))
                        drSource("CompanyId") = dcCell1.DefaultValue
                        drSource("CompanyName") = dcCell2.DefaultValue
                        dtSource.Rows.Add(drSource)

                    Next
                    Dim dv As New DataView(dtSource)
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        'dv.Sort = "EmployeeName"
                        For Each row As DataRowView In dv
                            If (Not CompanyId = 0) Then
                                If CompanyId = row("CompanyId") Then
                                    cblCompanylist.Items.Add(New ListItem(row("CompanyName").ToString(), row("CompanyId").ToString()))
                                    Exit For
                                End If
                            Else
                                cblCompanylist.Items.Add(New ListItem(row("CompanyName").ToString(), row("CompanyId").ToString()))
                            End If
                        Next
                    Else
                        For Each row As DataRowView In dv
                            If (Not CompanyId = 0) Then
                                If CompanyId = row("CompanyId") Then
                                    cblCompanylist.Items.Add(New ListItem(row("CompanyName").ToString(), row("CompanyId").ToString()))
                                    Exit For
                                End If
                            Else
                                cblCompanylist.Items.Add(New ListItem(row("CompanyName").ToString(), row("CompanyId").ToString()))
                            End If
                        Next
                    End If
                Else
                    cblCompanylist.Items.Clear()
                End If
            End If
        End If


    End Sub

    Private Sub ClearAll()
        objEmp_FilterCompany.ClearValues()
        'If RadCmbBxCompaniesCompany.Enabled Then
        '    RadCmbBxCompaniesCompany.ClearSelection()
        'End If

        dgrdManagers.SelectedIndexes.Clear()
        For Each row As GridDataItem In dgrdManagers.Items
            Dim chk As CheckBox = DirectCast(row.FindControl("chk"), CheckBox)
            chk.Checked = False
        Next
        cblCompanylist.ClearSelection()
    End Sub

    Private Sub fillgrid()
        Dim objUserPrivileg_Companies = New UserPrivileg_Companies
        dgrdManagers.DataSource = objUserPrivileg_Companies.GetManagerCompanies()
        dgrdManagers.DataBind()
    End Sub

    Private Sub FillControls()
        EmployeeId = CInt(CType(dgrdManagers.SelectedItems(0), GridDataItem).GetDataKeyValue("EmployeeId").ToString())
        CompanyId = CInt(CType(dgrdManagers.SelectedItems(0), GridDataItem).GetDataKeyValue("CompanyId").ToString())
        objEmp_FilterCompany.IsEntityClick = "True"
        objEmp_FilterCompany.GetEmployeeInfo(EmployeeId)
        ' RadCmbBxCompaniesCompany.SelectedValue = CompanyId
    End Sub

#End Region

End Class
