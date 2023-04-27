Imports SmartV.UTILITIES
Imports System.Data
Imports TA.Appraisal

Partial Class Appraisal_EmployeeGoals_byCoordinator
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objAppraisal_EmployeeGoals As Appraisal_EmployeeGoals

#End Region

#Region "Public Properties"

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
            FillGrid()
        End If
        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdEmployeeGoals.ClientID + "')")
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        objAppraisal_EmployeeGoals = New Appraisal_EmployeeGoals
        Dim err As Integer = -1
        With objAppraisal_EmployeeGoals
            .FK_EmployeeId = EmployeeFilterUC.EmployeeId
            .GoalName = txtGoalName.Text
            .GoalDetails = txtGoalDetails.Text
            .Weight = Convert.ToInt32(radnumGoalWeight.Text) / 100
            .Year = ddlYear.SelectedValue
            .FK_AppraisalStatusId = AppraisalStatus.Pending
            .Get_GoalSequence_By_EmpId_Year()


            If GoalId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                .GoalSequence += 1 '----Increment
                err = .Add
            Else
                .GoalId = GoalId
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                err = .Update
            End If
        End With

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGrid()
            ClearAll()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim err As Integer = -1
        objAppraisal_EmployeeGoals = New Appraisal_EmployeeGoals
        For Each row As GridDataItem In dgrdEmployeeGoals.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intGoalId As Integer = Convert.ToInt32(row.GetDataKeyValue("GoalId").ToString())
                objAppraisal_EmployeeGoals.GoalId = intGoalId
                err = objAppraisal_EmployeeGoals.Delete()
            End If
        Next
        If err = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGrid()
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If
        ClearAll()
        FillGrid()
    End Sub

    Protected Sub dgrdEmployeeGoals_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdEmployeeGoals.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            If Not item("Weight").Text = "&nbsp;" Then
                item("Weight").Text = Convert.ToDouble(item("Weight").Text) * 100
            End If
        End If
    End Sub

    Protected Sub dgrdEmployeeGoals_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdEmployeeGoals.NeedDataSource
        Dim dt As DataTable
        objAppraisal_EmployeeGoals = New Appraisal_EmployeeGoals
        With objAppraisal_EmployeeGoals
            .FK_EmployeeId = EmployeeFilterUC.EmployeeId
            .FK_AppraisalStatusId = AppraisalStatus.Pending
            .Year = ddlYear.SelectedValue
            dt = .Get_By_FK_EmployeeId_StatusId
        End With
        dgrdEmployeeGoals.DataSource = dt
    End Sub

    Protected Sub dgrdEmployeeGoals_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdEmployeeGoals.SelectedIndexChanged
        GoalId = Convert.ToInt32(DirectCast(dgrdEmployeeGoals.SelectedItems(0), GridDataItem).GetDataKeyValue("GoalId").ToString())
        FillControls()

    End Sub

    Protected Sub dgrdEmployeeGoals_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmployeeGoals.Skin))
    End Function

    Protected Sub btnSendtoMgr_Click(sender As Object, e As EventArgs) Handles btnSendtoMgr.Click
        objAppraisal_EmployeeGoals = New Appraisal_EmployeeGoals
        Dim err As Integer = -1
        If chkConfirm.Checked = True Then
            With objAppraisal_EmployeeGoals
                Dim dt As DataTable
                Dim TotalWeight As Double

                For Each item In dgrdEmployeeGoals.Items
                    .Year = ddlYear.SelectedValue
                    .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                    dt = .Get_Year_Weight_Sum()
                    If dt.Rows.Count > 0 Then
                        TotalWeight = dt(0)("TotalWeight").ToString
                    End If
                Next
                If TotalWeight = 1 Then
                    For Each item In dgrdEmployeeGoals.Items
                        .GoalId = Convert.ToInt32(item("GoalId").Text.ToString)
                        .FK_AppraisalStatusId = AppraisalStatus.FilledByEmployee
                        err = .UpdateStatus()
                    Next
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("GoalsWeightNotAccurate", CultureInfo), "info")
                    Exit Sub
                End If

            End With

            If err = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
                FillGrid()
                ClearAll()
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If
        Else
            CtlCommon.ShowMessage(Me.Page, "Please Admit", "info")
            'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PleaseAdmit", CultureInfo), "info")
        End If
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        FillGrid()
    End Sub

#End Region

#Region "Methods"

    Public Sub FillGrid()
        Dim dt As DataTable
        objAppraisal_EmployeeGoals = New Appraisal_EmployeeGoals
        With objAppraisal_EmployeeGoals
            .FK_EmployeeId = EmployeeFilterUC.EmployeeId
            .FK_AppraisalStatusId = AppraisalStatus.Pending
            .Year = ddlYear.SelectedValue
            dt = .Get_By_FK_EmployeeId_StatusId
        End With
        dgrdEmployeeGoals.DataSource = dt
        dgrdEmployeeGoals.DataBind()
    End Sub

    Private Sub ClearAll()
        txtGoalName.Text = String.Empty
        txtGoalDetails.Text = String.Empty
        radnumGoalWeight.Text = String.Empty
        ddlYear_Bind()
        GoalId = 0
        EmployeeFilterUC.ClearValues()
        chkConfirm.Checked = False
        FillGrid()
    End Sub

    Private Sub FillControls()
        objAppraisal_EmployeeGoals = New Appraisal_EmployeeGoals
        With objAppraisal_EmployeeGoals
            .GoalId = GoalId
            .GetByPK()
            txtGoalName.Text = .GoalName
            txtGoalDetails.Text = .GoalDetails
            radnumGoalWeight.Text = (.Weight) * 100
            ddlYear.SelectedValue = .Year
        End With
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
