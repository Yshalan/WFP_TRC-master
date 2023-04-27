Imports Telerik.Web.UI
Imports TA.Employees
Imports SmartV.UTILITIES

Partial Class Admin_LeaveBalance
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Dim objEmp_Leaves_BalanceHistory As Emp_Leaves_BalanceHistory
    Dim objEmp_Leaves_BalanceExpired As Emp_Leaves_BalanceExpired
    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

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
                dgrdBalance.Columns(5).Visible = True
            Else
                Lang = CtlCommon.Lang.EN
                dgrdBalance.Columns(4).Visible = True
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            TabContainer1.ActiveTabIndex = 0
            EmployeeFilterUC.IsEmployeeRequired = True
            EmployeeFilterUC.EmployeeRequiredValidationGroup = btnSearch.ValidationGroup
        End If
        PageHeader1.HeaderText = ResourceManager.GetString("LeaveBalanceHistory")
    End Sub

    Protected Sub dgrdBalance_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdBalance.NeedDataSource
        If Not EmployeeFilterUC.EmployeeId = 0 Or Not EmployeeFilterUC.EmployeeId = Nothing Then
            objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
            objEmp_Leaves_BalanceHistory.FK_CompanyId = EmployeeFilterUC.CompanyId
            If (EmployeeFilterUC.EntityId <> 0) Then
                objEmp_Leaves_BalanceHistory.FK_EntityId = EmployeeFilterUC.EntityId
            Else
                objEmp_Leaves_BalanceHistory.FK_EntityId = 0
            End If

            If (EmployeeFilterUC.EmployeeId <> 0) Then
                objEmp_Leaves_BalanceHistory.FK_EmployeeId = EmployeeFilterUC.EmployeeId
            Else
                objEmp_Leaves_BalanceHistory.FK_EmployeeId = 0
            End If

            dgrdBalance.DataSource = objEmp_Leaves_BalanceHistory.GetLeaveBalanceHistory()


            objEmp_Leaves_BalanceExpired = New Emp_Leaves_BalanceExpired
            objEmp_Leaves_BalanceExpired.FK_CompanyId = EmployeeFilterUC.CompanyId
            If (EmployeeFilterUC.EntityId <> 0) Then
                objEmp_Leaves_BalanceExpired.FK_EntityId = EmployeeFilterUC.EntityId
            Else
                objEmp_Leaves_BalanceExpired.FK_EntityId = 0
            End If

            If (EmployeeFilterUC.EmployeeId <> 0) Then
                objEmp_Leaves_BalanceExpired.FK_EmployeeId = EmployeeFilterUC.EmployeeId
            Else
                objEmp_Leaves_BalanceExpired.FK_EmployeeId = 0
            End If

            dgrdBalanceExpired.DataSource = objEmp_Leaves_BalanceExpired.GetLeaveExpiredBalance()
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        FillGrid()
    End Sub

    Protected Sub dgrdBalance_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdBalance.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("BalanceDate").ToString())) And (Not item.GetDataKeyValue("BalanceDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("BalanceDate").ToString()
                item("BalanceDate").Text = fromDate.ToShortDateString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("CREATED_DATE").ToString())) And (Not item.GetDataKeyValue("CREATED_DATE").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("CREATED_DATE").ToString()
                item("CREATED_DATE").Text = fromDate.ToShortDateString()
            End If

            If SessionVariables.CultureInfo = "ar-JO" Then
                item("EmployeeName").Text = item.GetDataKeyValue("EmployeeArabicName").ToString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("Balance").ToString())) And (Not item.GetDataKeyValue("Balance").ToString() = "")) Then
                item("Balance").Text = Decimal.Round(Convert.ToDecimal(item.GetDataKeyValue("Balance").ToString()), 3).ToString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("TotalBalance").ToString())) And (Not item.GetDataKeyValue("TotalBalance").ToString() = "")) Then
                item("TotalBalance").Text = Decimal.Round(Convert.ToDecimal(item.GetDataKeyValue("TotalBalance").ToString()), 3).ToString()
            End If

        End If
    End Sub

    Protected Sub dgrdBalanceExpired_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdBalanceExpired.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ExpireDate").ToString())) And (Not item.GetDataKeyValue("ExpireDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("ExpireDate").ToString()
                item("ExpireDate").Text = fromDate.ToShortDateString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("CREATED_DATE").ToString())) And (Not item.GetDataKeyValue("CREATED_DATE").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("CREATED_DATE").ToString()
                item("CREATED_DATE").Text = fromDate.ToShortDateString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ExpireBalance").ToString())) And (Not item.GetDataKeyValue("ExpireBalance").ToString() = "")) Then
                item("ExpireBalance").Text = Decimal.Round(Convert.ToDecimal(item.GetDataKeyValue("ExpireBalance").ToString()), 3).ToString()
            End If

        End If
    End Sub

    Protected Sub dgrdBalanceExpired_ChangedPageIndex(ByVal sender As Object, ByVal e As GridPageChangedEventArgs) Handles dgrdBalanceExpired.PageIndexChanged
        dgrdBalanceExpired.CurrentPageIndex = e.NewPageIndex
        FillExpiredBalance()
    End Sub

#End Region

#Region "Methods"

    Public Sub FillGrid()

        objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
        objEmp_Leaves_BalanceHistory.FK_CompanyId = EmployeeFilterUC.CompanyId
        If (EmployeeFilterUC.EntityId <> 0) Then
            objEmp_Leaves_BalanceHistory.FK_EntityId = EmployeeFilterUC.EntityId
        Else
            objEmp_Leaves_BalanceHistory.FK_EntityId = 0
        End If

        If (EmployeeFilterUC.EmployeeId <> 0) Then
            objEmp_Leaves_BalanceHistory.FK_EmployeeId = EmployeeFilterUC.EmployeeId
        Else
            objEmp_Leaves_BalanceHistory.FK_EmployeeId = 0
        End If

        dgrdBalance.DataSource = objEmp_Leaves_BalanceHistory.GetLeaveBalanceHistory()
        dgrdBalance.DataBind()

        FillExpiredBalance()
    End Sub

    Public Sub FillExpiredBalance()
        objEmp_Leaves_BalanceExpired = New Emp_Leaves_BalanceExpired
        objEmp_Leaves_BalanceExpired.FK_CompanyId = EmployeeFilterUC.CompanyId
        If (EmployeeFilterUC.EntityId <> 0) Then
            objEmp_Leaves_BalanceExpired.FK_EntityId = EmployeeFilterUC.EntityId
        Else
            objEmp_Leaves_BalanceExpired.FK_EntityId = 0
        End If

        If (EmployeeFilterUC.EmployeeId <> 0) Then
            objEmp_Leaves_BalanceExpired.FK_EmployeeId = EmployeeFilterUC.EmployeeId
        Else
            objEmp_Leaves_BalanceExpired.FK_EmployeeId = 0
        End If

        dgrdBalanceExpired.DataSource = objEmp_Leaves_BalanceExpired.GetLeaveExpiredBalance()
        dgrdBalanceExpired.DataBind()
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdBalanceExpired.Skin))
    End Function

#End Region

End Class
