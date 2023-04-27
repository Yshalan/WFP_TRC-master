Imports TA.Appraisal
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Employees

Partial Class Appraisal_ApproveEmployeeGoals
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objEmployee As Employee
    Private objAppraisal_EmployeeGoals As Appraisal_EmployeeGoals

#End Region

#Region "Properties"

    Private Enum AppraisalStatus
        Pending = 1
        FilledByEmployee = 2
        ApprovedbyManager = 3
        EvaluatedByEmployee = 4
        EvaluatedByManager = 5
    End Enum

    Public Property GoalId() As Integer
        Get
            Return ViewState("GoalId")
        End Get
        Set(ByVal value As Integer)
            ViewState("GoalId") = value
        End Set
    End Property

    Public Property FK_EmployeeId() As Integer
        Get
            Return ViewState("FK_EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_EmployeeId") = value
        End Set
    End Property

    Public Property Year() As Integer
        Get
            Return ViewState("Year")
        End Get
        Set(ByVal value As Integer)
            ViewState("Year") = value
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

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            ddlYear_Bind()
            FillGridEmployees()
        End If
    End Sub

    Protected Sub dgrdEmployeeGoals_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter2.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmployeeGoals.Skin))
    End Function

    Protected Sub dgrdEmployees_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick1(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon1() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmployees.Skin))
    End Function

    Protected Sub dgrdEmployees_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdEmployees.NeedDataSource
        Dim dt As DataTable
        objAppraisal_EmployeeGoals = New Appraisal_EmployeeGoals
        With objAppraisal_EmployeeGoals
            .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            .FK_AppraisalStatusId = AppraisalStatus.FilledByEmployee
            dt = .Get_By_Manager_Status()
        End With
        dgrdEmployees.DataSource = dt
    End Sub

    Protected Sub dgrdEmployees_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdEmployees.SelectedIndexChanged
        FK_EmployeeId = Convert.ToInt32(DirectCast(dgrdEmployees.SelectedItems(0), GridDataItem).GetDataKeyValue("EmployeeId").ToString())
        Year = Convert.ToInt32(DirectCast(dgrdEmployees.SelectedItems(0), GridDataItem).GetDataKeyValue("Year").ToString())
        Dim dt As DataTable
        objAppraisal_EmployeeGoals = New Appraisal_EmployeeGoals
        With objAppraisal_EmployeeGoals
            .FK_EmployeeId = FK_EmployeeId
            .Year = Year
            .FK_AppraisalStatusId = AppraisalStatus.FilledByEmployee
            dt = .Get_By_EmployeeId_Year()
        End With
        dgrdEmployeeGoals.DataSource = dt
        dgrdEmployeeGoals.DataBind()
        mvApproveEmpGoals.ActiveViewIndex = 1
        objEmployee = New Employee
        objEmployee.EmployeeId = FK_EmployeeId
        objEmployee.GetByPK()
        PageHeader2.HeaderText = IIf(Lang = CtlCommon.Lang.AR, objEmployee.EmployeeArabicName, objEmployee.EmployeeName)
    End Sub

    Protected Sub dgrdEmployeeGoals_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdEmployeeGoals.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            Dim radnumGoalWeight As RadNumericTextBox
            radnumGoalWeight = CType(e.Item.FindControl("radnumGoalWeight"), RadNumericTextBox)
            radnumGoalWeight.Text = (Convert.ToDouble(radnumGoalWeight.Text) * 100).ToString
        End If
    End Sub

    Protected Sub dgrdEmployeeGoals_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdEmployeeGoals.NeedDataSource
        Dim dt As DataTable
        objAppraisal_EmployeeGoals = New Appraisal_EmployeeGoals
        With objAppraisal_EmployeeGoals
            .FK_EmployeeId = FK_EmployeeId
            .Year = Year
            .FK_AppraisalStatusId = AppraisalStatus.FilledByEmployee
            dt = .Get_By_EmployeeId_Year()
        End With
        dgrdEmployeeGoals.DataSource = dt
    End Sub

    Protected Sub lnkBack_Click(sender As Object, e As EventArgs) Handles lnkBack.Click
        mvApproveEmpGoals.ActiveViewIndex = 0

    End Sub

    Protected Sub btnSendtoMgr_Click(sender As Object, e As EventArgs) Handles btnSendtoMgr.Click
        objAppraisal_EmployeeGoals = New Appraisal_EmployeeGoals
        Dim err As Integer = -1

        With objAppraisal_EmployeeGoals

            Dim dt As DataTable
            Dim TotalWeight As Double = 0
            Dim userTotalWeight As Double = 0
            For Each item As GridDataItem In dgrdEmployeeGoals.Items
                userTotalWeight += Convert.ToInt32(DirectCast(item.FindControl("radnumGoalWeight"), RadNumericTextBox).Text)
            Next
            .Year = ddlYear.SelectedValue
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            dt = .Get_Year_Weight_Sum()
            If dt.Rows.Count > 0 Then
                If Not dt(0)("TotalWeight") Is DBNull.Value Then
                    TotalWeight = Convert.ToDouble(dt(0)("TotalWeight").ToString)
                Else
                    TotalWeight = 0
                End If

            End If

            For Each item As GridDataItem In dgrdEmployeeGoals.Items
                .GoalId = Convert.ToInt32(item("GoalId").Text.ToString)
                .GetByPK()
                .GoalName = DirectCast(item.FindControl("txtGoalName"), TextBox).Text
                .GoalDetails = DirectCast(item.FindControl("txtGoalDetails"), TextBox).Text
                Dim txtWeight As String = DirectCast(item.FindControl("radnumGoalWeight"), RadNumericTextBox).Text
                .Weight = Convert.ToDouble(Convert.ToInt32(txtWeight) / 100)
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                err = .Update()
                If chkConfirm.Checked = True Then
                    If (userTotalWeight / 100) = 1 Then
                        .FK_AppraisalStatusId = AppraisalStatus.ApprovedbyManager
                        .UpdateStatus()
                    Else
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("GoalsWeightNotAccurate", CultureInfo), "info")
                        Exit Sub
                    End If
                End If
            Next
            

        End With

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            FillGridEmployees()
            mvApproveEmpGoals.ActiveViewIndex = 0
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
        End If

    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        FillGridEmployees()
    End Sub

#End Region

#Region "Page Methods"

    Private Sub FillGridEmployees()
        Dim dt As DataTable
        objAppraisal_EmployeeGoals = New Appraisal_EmployeeGoals
        With objAppraisal_EmployeeGoals
            .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            .FK_AppraisalStatusId = AppraisalStatus.FilledByEmployee
            .Year = ddlYear.SelectedValue
            dt = .Get_By_Manager_Status()
        End With
        dgrdEmployees.DataSource = dt
        dgrdEmployees.DataBind()
    End Sub

    Private Sub ddlYear_Bind()
        Dim year As Integer = DateTime.Now.Year
        Dim dt As New DataTable
        dt.Columns.Add("val")
        dt.Columns.Add("txt")

        'Dim dr1 As DataRow = dt.NewRow()
        'dr1("val") = 0
        'dr1("txt") = "year"
        'dt.Rows.Add(dr1)

        For i As Integer = year - 5 To year + 5
            Dim dr As DataRow = dt.NewRow()
            dr("val") = i
            dr("txt") = i
            dt.Rows.Add(dr)
        Next

        ddlYear.SelectedValue = Now.Year
        ddlYear.DataSource = dt
        ddlYear.DataBind()
    End Sub

#End Region

End Class
