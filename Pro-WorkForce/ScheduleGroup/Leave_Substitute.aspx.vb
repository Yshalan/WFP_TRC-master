Imports Telerik.Web.UI
Imports TA.ScheduleGroups
Imports System.Data
Imports TA.Employees
Imports SmartV.UTILITIES

Partial Class ScheduleGroup_Leave_Substitute
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objLeaveSubstitute As LeaveSubstitute
    Private objEmployee As Employee
    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objScheduleGroup_Shifts As ScheduleGroup_Shifts

#End Region

#Region "Properties"

    Public Property SubstituteId() As Integer
        Get
            Return ViewState("SubstituteId")
        End Get
        Set(ByVal value As Integer)
            ViewState("SubstituteId") = value
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

            FillConfirmed()
            FillPending()
            TabContainer1.ActiveTab = TabPending
            mvPending.SetActiveView(vPending)
        End If
        pageheader1.HeaderText = ResourceManager.GetString("LeaveSubstitute", CultureInfo)
    End Sub

    Protected Sub grdPending_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdPending.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item
            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
            End If
        End If
    End Sub

    Protected Sub grdPending_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdPending.NeedDataSource
        objLeaveSubstitute = New LeaveSubstitute
        Dim dt As DataTable
        With objLeaveSubstitute
            .UserId = SessionVariables.LoginUser.ID
            dt = .GetAll_Pending
        End With
        grdPending.DataSource = dt
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        dtpSubstituteDate.MaxDate = Convert.ToDateTime("2099-12-31")
        mvPending.SetActiveView(vPending)
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If ValidateSelectedDate() = True Then
            Dim err = -1
            objLeaveSubstitute = New LeaveSubstitute
            With objLeaveSubstitute
                .Confirmed_By = SessionVariables.LoginUser.ID
                .ConfirmSubstituteDate = dtpSubstituteDate.DbSelectedDate
                .IsConfirmed = True
                .SubstituteId = SubstituteId
                err = .Update()
            End With
            If err = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                FillPending()
                FillConfirmed()
                mvPending.SetActiveView(vPending)
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        End If
    End Sub

    Protected Sub dgrdConfirmed_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdConfirmed.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item
            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr2"), HiddenField).Value
            End If
        End If
    End Sub

    Protected Sub dgrdConfirmed_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdConfirmed.NeedDataSource
        objLeaveSubstitute = New LeaveSubstitute
        Dim dt As DataTable
        With objLeaveSubstitute
            .UserId = SessionVariables.LoginUser.ID
            dt = .GetAll_Confirmed
        End With
        dgrdConfirmed.DataSource = dt
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", grdPending.Skin))
    End Function

    Protected Sub RadToolBar2_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon2() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdConfirmed.Skin))
    End Function

    Protected Sub dgrdConfirmed_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdConfirmed.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub grdPending_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdPending.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillPending()
        objLeaveSubstitute = New LeaveSubstitute
        Dim dt As DataTable
        With objLeaveSubstitute
            .UserId = SessionVariables.LoginUser.ID
            dt = .GetAll_Pending
        End With
        grdPending.DataSource = dt
        grdPending.DataBind()
    End Sub

    Private Sub FillConfirmed()
        objLeaveSubstitute = New LeaveSubstitute
        Dim dt As DataTable
        With objLeaveSubstitute
            .UserId = SessionVariables.LoginUser.ID
            dt = .GetAll_Confirmed
        End With
        dgrdConfirmed.DataSource = dt
        dgrdConfirmed.DataBind()
    End Sub

    Private Sub FillPendingDetails()
        objEmployee = New Employee
        objLeaveSubstitute = New LeaveSubstitute
        With objEmployee
            .EmployeeId = FK_EmployeeId
            .GetByPK()
            txtEmployeeNo.Text = .EmployeeNo
            If Lang = CtlCommon.Lang.AR Then
                txtEmployeeName.Text = .EmployeeArabicName
            Else
                txtEmployeeName.Text = .EmployeeName
            End If

        End With
        With objLeaveSubstitute
            .SubstituteId = SubstituteId
            .GetByPK()
            dtpLeaveDate.SelectedDate = .LeaveDate
            dtpSubstituteDate.SelectedDate = .SubstituteDate
            dtpSubstituteDate.MaxDate = .LeaveDate.AddDays(30)
        End With

    End Sub

    Protected Sub lnkConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        SubstituteId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("SubstituteId"))
        FK_EmployeeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_EmployeeId"))
        mvPending.SetActiveView(vPendingDetails)
        FillPendingDetails()
    End Sub

    Private Function ValidateSelectedDate() As Boolean
        objScheduleGroup_Shifts = New ScheduleGroup_Shifts
        objLeaveSubstitute = New LeaveSubstitute
        With objScheduleGroup_Shifts
            .WorkDate = dtpSubstituteDate.SelectedDate
            .FK_EmployeeId = FK_EmployeeId
            .Get_WorkDay()
            If .FK_ShiftId = Nothing Then
                Dim dt As DataTable
                objLeaveSubstitute.FK_EmployeeId = FK_EmployeeId
                dt = objLeaveSubstitute.Get_ByEmployeeId()
                If Not dt Is Nothing Then
                    For Each row As DataRow In dt.Rows
                        If dtpSubstituteDate.SelectedDate = row("ConfirmSubstituteDate") Then
                            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PleaseSelectSubstituteDate", CultureInfo), "info")
                            Return False
                        End If
                    Next
                End If
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PleaseSelectRestDay", CultureInfo), "info")
                Return False
            End If
        End With
        Return True
    End Function

#End Region

End Class
