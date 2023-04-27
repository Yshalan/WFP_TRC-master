Imports SmartV.UTILITIES
Imports TA.Lookup
Imports System.Data
Imports TA.Employees
Imports TA.Definitions
Imports TA.Admin
Imports TA.Security
Imports Telerik.Web.UI
Imports TA.Employees.Employee

Partial Class Employee_UserControls_UserPrevileges_Worklocations
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objSYSUsers As SYSUsers
    Private objVersion As SmartV.Version.version
    Shared SortExepression As String
    Shared dtCurrent As DataTable

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

    Private Property WorkLocationId() As Integer
        Get
            Return ViewState("WorkLocationId")
        End Get
        Set(ByVal value As Integer)
            ViewState("WorkLocationId") = value
        End Set
    End Property

    Public Property dtCurrentControls() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

    Public Property SelectedID() As Integer
        Get
            Return ViewState("ID")
        End Get
        Set(ByVal value As Integer)
            ViewState("ID") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Employee_UserControls_UserPrevileges_Worklocations_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            objEmp_FilterLocation.IsEmployeeRequired = True
            objEmp_FilterLocation.CompanyRequiredValidationGroup = btnSave.ValidationGroup
            objEmp_FilterLocation.EmployeeRequiredValidationGroup = btnSave.ValidationGroup

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN

            End If
            fillCompanies()
            fillgrid()
        End If

        objEmp_FilterLocation.IsEntityClick = "True"

        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN

        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdManagers.ClientID + "');")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrentControls.Rows
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

    Protected Sub dgrdManagers_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs) Handles dgrdManagers.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdManagers.Skin))
    End Function

    Protected Sub dgrdManagers_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdManagers.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item) Then
            Dim Itm As GridDataItem = e.Item
        End If
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
        objEmp_FilterLocation.ClearValues()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim objUserPrivileg_WorkLocations = New UserPrivileg_WorkLocations
        Dim errNum As Integer = -1
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdManagers.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objUserPrivileg_WorkLocations = New UserPrivileg_WorkLocations
                objUserPrivileg_WorkLocations.Id = Convert.ToInt32(row.GetDataKeyValue("Id").ToString())
                errNum = objUserPrivileg_WorkLocations.Delete()

            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            ClearAll()
            fillgrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim err As Integer = 0
        Dim flag As Boolean = False
        Dim UserPrivileg_Entities As New UserPrivileg_WorkLocations
        With UserPrivileg_Entities
            EmployeeId = objEmp_FilterLocation.hEmployeeId
            .FK_CompanyId = CompanyId
            .FK_EmployeeId = EmployeeId
            For Each item As ListItem In cblWLlist.Items
                If item.Selected Then
                    flag = True
                    .FK_WorkLocationId = item.Value
                    err = .Add()
                End If
            Next

            If err = 0 Then
                If flag = False Then
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("WorkLocationSelect", CultureInfo), "info")
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                    ClearAll()
                    fillgrid()
                End If
            ElseIf err = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")

            End If
          
        End With
    End Sub

    Protected Sub RadCmbBxCompanies_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompaniesLocation.SelectedIndexChanged
        If (RadCmbBxCompaniesLocation.SelectedValue <> -1) Then
            CompanyId = RadCmbBxCompaniesLocation.SelectedValue
        Else
            CompanyId = 0
        End If
        fillWorkLocation()
    End Sub

    'Protected Sub RadCmbBxWorkLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxWorkLocation.SelectedIndexChanged
    '    If RadCmbBxWorkLocation.SelectedValue <> -1 Then
    '        WorkLocationId = RadCmbBxWorkLocation.SelectedValue
    '    Else
    '        WorkLocationId = 0
    '    End If
    'End Sub

    Protected Sub dgrdManagers_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdManagers.NeedDataSource
        Dim objUserPrivileg_WorkLocations = New UserPrivileg_WorkLocations
        dtCurrent = objUserPrivileg_WorkLocations.GetManagerWorkLocation()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdManagers.DataSource = dv
    End Sub

    Protected Sub dgrdManagers_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdManagers.SelectedIndexChanged
        FillControls()
    End Sub

#End Region

#Region "Mehtods"

    Private Sub fillCompanies()
        If SessionVariables.LoginUser IsNot Nothing Then
            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()
            Dim objOrgCompany = New OrgCompany
            CtlCommon.FillTelerikDropDownList(RadCmbBxCompaniesLocation, objOrgCompany.GetAllforddl, Lang)
        End If
    End Sub

    Private Sub fillWorkLocation()
        Dim objEmp_WorkLocation As New Emp_WorkLocation
        objEmp_WorkLocation.FK_CompanyId = CompanyId
        'CtlCommon.FillTelerikDropDownList(RadCmbBxWorkLocation, objEmp_WorkLocation.GetAllByCompany, Lang)

        Dim dt As DataTable
        dt = objEmp_WorkLocation.GetAllByCompany
        If (dt IsNot Nothing) Then
            Dim dtEmployees = dt
            If (dtEmployees IsNot Nothing) Then
                If (dtEmployees.Rows.Count > 0) Then
                    Repeater1.Visible = True
                    Dim dtSource As New DataTable
                    dtSource.Columns.Add("WorkLocationId")
                    dtSource.Columns.Add("WorkLocationName")
                    Dim drRow As DataRow
                    drRow = dtSource.NewRow()
                    For Item As Integer = 0 To dtEmployees.Rows.Count - 1

                        Dim drSource As DataRow
                        drSource = dtSource.NewRow
                        Dim dcCell1 As New DataColumn
                        Dim dcCell2 As New DataColumn
                        dcCell1.ColumnName = "WorkLocationId"
                        dcCell2.ColumnName = "WorkLocationName"
                        dcCell1.DefaultValue = dtEmployees.Rows(Item)("WorkLocationId")
                        dcCell2.DefaultValue = dtEmployees.Rows(Item)("WorkLocationCode") + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)("WorkLocationName"), dtEmployees.Rows(Item)("WorkLocationArabicName"))
                        drSource("WorkLocationId") = dcCell1.DefaultValue
                        drSource("WorkLocationName") = dcCell2.DefaultValue
                        dtSource.Rows.Add(drSource)

                    Next
                    Dim dv As New DataView(dtSource)
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        'dv.Sort = "EmployeeName"
                        For Each row As DataRowView In dv
                            If (Not WorkLocationId = 0) Then
                                If WorkLocationId = row("WorkLocationId") Then
                                    cblWLlist.Items.Add(New ListItem(row("WorkLocationName").ToString(), row("WorkLocationId").ToString()))
                                    Exit For
                                End If
                            Else
                                cblWLlist.Items.Add(New ListItem(row("WorkLocationName").ToString(), row("WorkLocationId").ToString()))
                            End If
                        Next
                    Else
                        For Each row As DataRowView In dv
                            If (Not WorkLocationId = 0) Then
                                If WorkLocationId = row("WorkLocationId") Then
                                    cblWLlist.Items.Add(New ListItem(row("WorkLocationName").ToString(), row("WorkLocationId").ToString()))
                                    Exit For
                                End If
                            Else
                                cblWLlist.Items.Add(New ListItem(row("WorkLocationName").ToString(), row("WorkLocationId").ToString()))
                            End If
                        Next
                    End If
                Else
                    cblWLlist.Items.Clear()
                End If
            End If
        End If

    End Sub

    Private Sub ClearAll()
        RadCmbBxCompaniesLocation.SelectedValue = -1
        'RadCmbBxWorkLocation.Items.Clear()
        'RadCmbBxWorkLocation.Text = String.Empty
        dgrdManagers.SelectedIndexes.Clear()
        objEmp_FilterLocation.ClearValues()
        cblWLlist.Items.Clear()
        SelectedID = 0
    End Sub

    Private Sub fillgrid()
        Dim objUserPrivileg_WorkLocations = New UserPrivileg_WorkLocations
        dgrdManagers.DataSource = objUserPrivileg_WorkLocations.GetManagerWorkLocation()
        dgrdManagers.DataBind()
    End Sub

    Private Sub FillControls()
        CompanyId = CInt(CType(dgrdManagers.SelectedItems(0), GridDataItem).GetDataKeyValue("CompanyId").ToString())
        EmployeeId = CInt(CType(dgrdManagers.SelectedItems(0), GridDataItem).GetDataKeyValue("EmployeeId").ToString())
        WorkLocationId = CInt(CType(dgrdManagers.SelectedItems(0), GridDataItem).GetDataKeyValue("WorkLocationId").ToString())
        SelectedID = CInt(CType(dgrdManagers.SelectedItems(0), GridDataItem).GetDataKeyValue("Id").ToString())

        objEmp_FilterLocation.GetEmployeeInfo(EmployeeId)
        RadCmbBxCompaniesLocation.SelectedValue = CompanyId
        cblWLlist.Items.Clear()
        fillWorkLocation()
        'RadCmbBxWorkLocation.SelectedValue = WorkLocationId

    End Sub

#End Region

End Class
