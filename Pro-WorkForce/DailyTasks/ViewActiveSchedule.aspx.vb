Imports TA.Admin.OrgCompany
Imports SmartV.UTILITIES
Imports TA.Lookup
Imports System.Data
Imports TA.Definitions
Imports Telerik.Web.UI
Imports TA.Employees
Imports SmartV.UTILITIES.ProjectCommon

Partial Class Admin_ViewActiveSchedule
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Shared SortExepression As String
    Shared dtCurrent As DataTable
    Private objEmp_WorkSchedule As Emp_WorkSchedule

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
            PageHeader1.HeaderText = ResourceManager.GetString("ViewActiveSchedule", CultureInfo)

            Dim objAppsettings As New TA.Admin.APP_Settings
            objAppsettings.GetByPK()
            If objAppsettings.AllowDeleteSchedule = False Then
                btnDelete.Visible = False
            End If

            rdateviewactive.SelectedDate = Now

        End If
    End Sub

    'Protected Sub dgrdViewSchedule_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
    '    If e.CommandName = "FilterRadGrid" Then
    '        RadFilterdgrdView.FireApplyCommand()
    '    End If
    'End Sub

    Protected Sub btnGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGet.Click
        chkFullList.Checked = True
        BindData()
    End Sub

    Protected Sub dgrdViewSchedule_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles dgrdViewSchedule.PageIndexChanged
        dgrdViewSchedule.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub dgrdViewSchedule_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdViewSchedule.ItemDataBound
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
        End If

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim err As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdViewSchedule.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                If Not row.GetDataKeyValue("EmpWorkScheduleId").ToString() = "" Then
                    Dim intEmpWorkScheduleId As Long = Convert.ToInt64(row.GetDataKeyValue("EmpWorkScheduleId").ToString())
                    objEmp_WorkSchedule = New Emp_WorkSchedule
                    objEmp_WorkSchedule.EmpWorkScheduleId = intEmpWorkScheduleId
                    err = objEmp_WorkSchedule.Delete()
                Else
                    err = -2
                End If
            End If
        Next
        If err = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            BindData()
        ElseIf err = -2 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDeleteDefaultSchedule", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")

        End If
        BindData()
    End Sub

    Protected Sub dgrdViewSchedule_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdViewSchedule.NeedDataSource
        'Dim strFilterType As String
        'strFilterType = EmployeeFilter1.FilterType
        'If EmployeeFilter1.EmployeeId <> 0 Then
        '    Dim objWorkSchedule As New WorkSchedule
        '    objWorkSchedule.EmployeeId = EmployeeFilter1.EmployeeId
        '    objWorkSchedule.FilterType = strFilterType

        '    dgrdViewSchedule.Columns(1).Visible = True
        '    dgrdViewSchedule.Columns(2).Visible = True
        '    dgrdViewSchedule.Columns(3).Visible = False
        '    dgrdViewSchedule.Columns(4).Visible = False

        '    dtCurrent = objWorkSchedule.GetActiveSchedulebyEmpId(rdateviewactive.SelectedDate)

        'Else

        '    Dim objWorkSchedule As New WorkSchedule
        '    objWorkSchedule.CompanyId = EmployeeFilter1.CompanyId
        '    objWorkSchedule.EntityId = EmployeeFilter1.EntityId
        '    objWorkSchedule.ScheduleDate = rdateviewactive.SelectedDate
        '    objWorkSchedule.FilterType = strFilterType
        '    'objWorkSchedule.GetActiveScheduleByCompanyandEntity()
        '    If chkFullList.Checked Then

        '        dgrdViewSchedule.Columns(1).Visible = True
        '        dgrdViewSchedule.Columns(2).Visible = True
        '        dgrdViewSchedule.Columns(3).Visible = False
        '        dgrdViewSchedule.Columns(4).Visible = False
        '        dgrdViewSchedule.Columns(9).Visible = True
        '        dtCurrent = objWorkSchedule.GetActiveScheduleByCompanyandEntity
        '    Else
        '        dgrdViewSchedule.Columns(1).Visible = False
        '        dgrdViewSchedule.Columns(2).Visible = False
        '        dgrdViewSchedule.Columns(9).Visible = False
        '        If Lang = CtlCommon.Lang.AR Then
        '            dgrdViewSchedule.Columns(3).Visible = False
        '            dgrdViewSchedule.Columns(4).Visible = True
        '            Select Case EmployeeFilter1.FilterType
        '                Case "C"
        '                    dgrdViewSchedule.Columns(4).HeaderText = "وحدة العمل"
        '                Case "W"
        '                    dgrdViewSchedule.Columns(4).HeaderText = "موقع العمل"
        '                Case "L"
        '                    dgrdViewSchedule.Columns(4).HeaderText = "اسم المجموعة"
        '            End Select

        '        Else
        '            dgrdViewSchedule.Columns(3).Visible = True
        '            Select Case EmployeeFilter1.FilterType
        '                Case "C"
        '                    dgrdViewSchedule.Columns(3).HeaderText = "Entity Name"
        '                Case "W"
        '                    dgrdViewSchedule.Columns(3).HeaderText = "Work Location Name"
        '                Case "L"
        '                    dgrdViewSchedule.Columns(3).HeaderText = "Group Name"
        '            End Select
        '            dgrdViewSchedule.Columns(4).Visible = False
        '        End If

        '        dtCurrent = objWorkSchedule.GetActiveSchedulebyCompanyorEntityWithNames
        '    End If
        If Not dtCurrent Is Nothing Then
            Dim dv As New DataView(dtCurrent)
            dv.Sort = SortExepression
            dgrdViewSchedule.DataSource = dv
        End If

        'End If
    End Sub

#End Region

#Region "Methods"

    Private Sub BindData()
        Dim strFilterType As String
        strFilterType = EmployeeFilter1.FilterType
        If EmployeeFilter1.EmployeeId <> 0 Then
            Dim objWorkSchedule As New WorkSchedule
            objWorkSchedule.EmployeeId = EmployeeFilter1.EmployeeId
            objWorkSchedule.FilterType = strFilterType

            dgrdViewSchedule.Columns(1).Visible = True
            dgrdViewSchedule.Columns(2).Visible = True
            dgrdViewSchedule.Columns(3).Visible = False
            dgrdViewSchedule.Columns(4).Visible = False

            'dgrdViewSchedule.DataSource = objWorkSchedule.GetActiveSchedulebyEmpId(rdateviewactive.SelectedDate)

            dgrdViewSchedule.DataSource = objWorkSchedule.GetEmployeeActiveScheduleByEmpId(rdateviewactive.SelectedDate)
            dtCurrent = dgrdViewSchedule.DataSource
            dgrdViewSchedule.DataBind()

        Else

            Dim objWorkSchedule As New WorkSchedule
            objWorkSchedule.CompanyId = EmployeeFilter1.CompanyId
            objWorkSchedule.EntityId = EmployeeFilter1.EntityId
            objWorkSchedule.ScheduleDate = rdateviewactive.SelectedDate
            objWorkSchedule.FilterType = strFilterType
            'objWorkSchedule.GetActiveScheduleByCompanyandEntity()
            If chkFullList.Checked Then

                dgrdViewSchedule.Columns(1).Visible = True
                dgrdViewSchedule.Columns(2).Visible = True
                dgrdViewSchedule.Columns(3).Visible = False
                dgrdViewSchedule.Columns(4).Visible = False
                dgrdViewSchedule.Columns(9).Visible = True
                'dgrdViewSchedule.DataSource = objWorkSchedule.GetActiveScheduleByCompanyandEntity
                dgrdViewSchedule.DataSource = objWorkSchedule.GetEmployeeActiveSchedule
                dtCurrent = dgrdViewSchedule.DataSource
            Else
                dgrdViewSchedule.Columns(1).Visible = False
                dgrdViewSchedule.Columns(2).Visible = False
                dgrdViewSchedule.Columns(9).Visible = False
                If Lang = CtlCommon.Lang.AR Then
                    dgrdViewSchedule.Columns(3).Visible = False
                    dgrdViewSchedule.Columns(4).Visible = True
                    Select Case EmployeeFilter1.FilterType
                        Case "C"
                            dgrdViewSchedule.Columns(4).HeaderText = "وحدة العمل"
                        Case "W"
                            dgrdViewSchedule.Columns(4).HeaderText = "موقع العمل"
                        Case "L"
                            dgrdViewSchedule.Columns(4).HeaderText = "اسم المجموعة"
                    End Select

                Else
                    dgrdViewSchedule.Columns(3).Visible = True
                    Select Case EmployeeFilter1.FilterType
                        Case "C"
                            dgrdViewSchedule.Columns(3).HeaderText = "Entity Name"
                        Case "W"
                            dgrdViewSchedule.Columns(3).HeaderText = "Work Location Name"
                        Case "L"
                            dgrdViewSchedule.Columns(3).HeaderText = "Group Name"
                    End Select
                    dgrdViewSchedule.Columns(4).Visible = False
                End If

                'dgrdViewSchedule.DataSource = objWorkSchedule.GetActiveSchedulebyCompanyorEntityWithNames
                dgrdViewSchedule.DataSource = objWorkSchedule.GetEmployeeActiveSchedule
                dtCurrent = dgrdViewSchedule.DataSource
            End If

            dgrdViewSchedule.DataBind()

        End If
    End Sub

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdViewSchedule.Skin))
    End Function

    Protected Sub dgrdViewSchedule_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdViewSchedule.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub


#End Region



End Class
