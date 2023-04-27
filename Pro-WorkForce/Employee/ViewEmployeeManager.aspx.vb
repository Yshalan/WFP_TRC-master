Imports SmartV.UTILITIES
Imports TA.Employees
Imports Telerik.Web.UI
Imports System.Data
Imports SmartV.UTILITIES.ProjectCommon


Partial Class Admin_ViewEmployeeManager
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim dtCurrent As DataTable
    Shared SortExepression As String
    Private objEmployee_Manager As Employee_Manager
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
        If Not IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("ViewMgr", CultureInfo)
            rdateviewactive.SelectedDate = Now
        End If
        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdViewManager.ClientID + "')")
    End Sub

    Protected Sub btnGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGet.Click
        'If rdateviewactive.SelectedDate = "" Or rdateviewactive.SelectedDate Is Nothing Then
        '    CtlCommon.ShowMessage(Me.Page, IIf(Lang = CtlCommon.Lang.AR, "الرجاء اختيار التاريخ", " Please Select date"))
        '    Return
        'End If
        FillGridView()
    End Sub

    Protected Sub dgrdViewManager_ItemdataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdViewManager.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromDate").ToString())) And (Not item.GetDataKeyValue("FromDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("FromDate").ToString()
                item("FromDate").Text = fromDate.ToShortDateString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ToDate").ToString())) And (Not item.GetDataKeyValue("ToDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("ToDate").ToString()
                item("ToDate").Text = fromDate.ToShortDateString()
            End If

            If SessionVariables.CultureInfo = "ar-JO" Then
                item("EmployeeName").Text = item.GetDataKeyValue("EmployeeArabicName").ToString()
                item("ManagerName").Text = item.GetDataKeyValue("ManagerArabicName").ToString()
            End If

        End If
    End Sub

    Protected Sub dgrdViewManager_SelectedPageIndex(ByVal sender As Object, ByVal e As GridPageChangedEventArgs) Handles dgrdViewManager.PageIndexChanged
        dgrdViewManager.CurrentPageIndex = e.NewPageIndex
        FillGridView()
    End Sub

    Protected Sub dgrdViewManager_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdViewManager.NeedDataSource
        If EmployeeFilter1.EmployeeId <> 0 Then
            Dim objEmployee_Manager As New Employee_Manager
            objEmployee_Manager.FK_EmployeeId = EmployeeFilter1.EmployeeId
            dtCurrent = objEmployee_Manager.GetActiveManagerByEmpId(rdateviewactive.SelectedDate)
        Else
            Dim objEmployee_Manager As New Employee_Manager

            objEmployee_Manager.CompanyId = EmployeeFilter1.CompanyId
            objEmployee_Manager.EntityId = EmployeeFilter1.EntityId
            objEmployee_Manager.AssignDate = rdateviewactive.SelectedDate
            dtCurrent = objEmployee_Manager.GetActiveManagerByCompanyandEntity()
        End If

        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdViewManager.DataSource = dv

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim err As Integer = -1
        Dim counter As Integer = 0
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdViewManager.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                counter = counter + 1
                Dim strCode As String = row.GetDataKeyValue("ManagerName").ToString()
                Dim intManagerId As Long = Convert.ToInt64(row.GetDataKeyValue("EmpManagerId").ToString())
                objEmployee_Manager = New Employee_Manager
                objEmployee_Manager.EmpManagerId = intManagerId
                err = objEmployee_Manager.Delete()
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
        If counter = 0 Then
            CtlCommon.ShowMessage(Me.Page, IIf(Lang = CtlCommon.Lang.AR, "الرجاء اختيار السجل المراد حذفها", " Please Select Record to delete"), "info")
            Return
        End If
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGridView()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If
        FillGridView()
    End Sub

#End Region

#Region "Methods"

    Private Function FillGridView()
        If EmployeeFilter1.EmployeeId <> 0 Then
            Dim objEmployee_Manager As New Employee_Manager
            objEmployee_Manager.FK_EmployeeId = EmployeeFilter1.EmployeeId

            dgrdViewManager.DataSource = objEmployee_Manager.GetActiveManagerByEmpId(rdateviewactive.SelectedDate)
            dgrdViewManager.DataBind()
        Else
            Dim objEmployee_Manager As New Employee_Manager


            objEmployee_Manager.CompanyId = EmployeeFilter1.CompanyId
            objEmployee_Manager.EntityId = EmployeeFilter1.EntityId
            objEmployee_Manager.AssignDate = rdateviewactive.SelectedDate
            dgrdViewManager.DataSource = objEmployee_Manager.GetActiveManagerByCompanyandEntity
            dgrdViewManager.DataBind()
        End If
    End Function

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdViewManager.Skin))
    End Function

    Protected Sub dgrdViewManager_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdViewManager.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

#End Region

End Class
