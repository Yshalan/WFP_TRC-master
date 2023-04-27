Imports SmartV.UTILITIES
Imports TA.Appraisal
Imports System.Data
Imports TA.Employees

Partial Class Appraisal_EmployeeEvaluation
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objAppraisal_EmployeeGoals As Appraisal_EmployeeGoals
    Private objAppraisal_EvaluationPoints As Appraisal_EvaluationPoints
    Private objEmp_Skills As Emp_Skills
    Private objAppraisal_EmployeeSkills As Appraisal_EmployeeSkills

#End Region

#Region "Public Properties"

    Private Enum AppraisalStatus
        Pending = 1
        FilledByEmployee = 2
        ApprovedbyManager = 3
        EvaluatedByEmployee = 4
        EvaluatedByManager = 5
    End Enum

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
            FillGridSkills()
        End If
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

    Protected Sub dgrdEmployeeGoals_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdEmployeeGoals.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            If Not item("Weight").Text = "&nbsp;" Then
                item("Weight").Text = Convert.ToDouble(item("Weight").Text) * 100
            End If

            objAppraisal_EvaluationPoints = New Appraisal_EvaluationPoints
            With objAppraisal_EvaluationPoints

                Dim radrating As RadRating = CType(e.Item.FindControl("radEvaluationPointbyEmployee"), RadRating)
                Dim dt As DataTable = .Get_PointsCount
                radrating.ItemCount = Convert.ToInt32(dt(0)("ItemPointsCount").ToString)
            End With
        End If
    End Sub

    Protected Sub dgrdEmployeeGoals_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdEmployeeGoals.NeedDataSource
        Dim dt As DataTable
        objAppraisal_EmployeeGoals = New Appraisal_EmployeeGoals
        With objAppraisal_EmployeeGoals
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FK_AppraisalStatusId = AppraisalStatus.ApprovedbyManager
            .Year = ddlYear.SelectedValue
            dt = .Get_By_FK_EmployeeId_StatusId
        End With
        dgrdEmployeeGoals.DataSource = dt
    End Sub

    Protected Sub btnSendtoMgr_Click(sender As Object, e As EventArgs) Handles btnSendtoMgr.Click
        objAppraisal_EmployeeGoals = New Appraisal_EmployeeGoals
        Dim err As Integer = -1
        Dim err2 As Integer = -1

        With objAppraisal_EmployeeGoals
            For Each item As GridDataItem In dgrdEmployeeGoals.Items
                .GoalId = Convert.ToInt32(item("GoalId").Text.ToString)
                .GetByPK()
                .EvaluationPointbyEmployee = DirectCast(item.FindControl("radEvaluationPointbyEmployee"), RadRating).Value
                .EmployeeRemarks = DirectCast(item.FindControl("txtEmployeeRemarks"), TextBox).Text
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                err = .Update()
                If chkConfirm.Checked = True Then
                    .FK_AppraisalStatusId = AppraisalStatus.EvaluatedByEmployee
                    .UpdateStatus()
                End If
            Next
        End With

        If dgrdSkills.Items.Count > 0 Then
            For Each item As GridDataItem In dgrdSkills.Items
                objAppraisal_EmployeeSkills = New Appraisal_EmployeeSkills
                With objAppraisal_EmployeeSkills
                    .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                    .FK_SkillId = Convert.ToInt32(item("SkillId").Text.ToString)
                    .Year = ddlYear.SelectedValue
                    .GetByPK()

                    .Weight = (DirectCast(item.FindControl("radnumSkillWeight"), RadNumericTextBox).Text) / 100
                    .EvaluationPointbyEmployee = DirectCast(item.FindControl("radSkillEvaluationPointbyEmployee"), RadRating).Value
                    .EmployeeRemarks = DirectCast(item.FindControl("txtEmployeeSkillRemarks"), TextBox).Text
                    If .FK_AppraisalStatusId = 0 Then
                        .FK_AppraisalStatusId = AppraisalStatus.Pending
                        .CREATED_BY = SessionVariables.LoginUser.ID
                        err2 = .Add
                    Else
                        .FK_AppraisalStatusId = AppraisalStatus.Pending
                        .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                        err2 = .Update
                    End If

                    If chkConfirm.Checked = True Then
                        .FK_AppraisalStatusId = AppraisalStatus.EvaluatedByEmployee
                        .UpdateStatus()
                    End If
                End With
            Next
        Else
            err2 = 0
        End If


        If err = 0 And err2 = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            FillGrid()
            FillGridSkills()
            ClearAll()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
        End If

    End Sub

    Protected Sub dgrdSkills_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBarSkills_ButtonClick1(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetSkillsFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdSkills.Skin))
    End Function

    Protected Sub dgrdSkills_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdSkills.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            If Not CType(e.Item.FindControl("radnumSkillWeight"), RadNumericTextBox).Text = "" Then
                Dim radnumSkillWeight As Double = Convert.ToDouble(CType(e.Item.FindControl("radnumSkillWeight"), RadNumericTextBox).Text)
                CType(e.Item.FindControl("radnumSkillWeight"), RadNumericTextBox).Text = radnumSkillWeight * 100
            End If

            objAppraisal_EvaluationPoints = New Appraisal_EvaluationPoints
            With objAppraisal_EvaluationPoints

                Dim radrating As RadRating = CType(e.Item.FindControl("radSkillEvaluationPointbyEmployee"), RadRating)
                Dim dt As DataTable = .Get_PointsCount
                radrating.ItemCount = Convert.ToInt32(dt(0)("ItemPointsCount").ToString)
            End With
        End If
    End Sub

    Protected Sub dgrdSkills_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdSkills.NeedDataSource
        Dim dt As DataTable
        dgrdSkills.DataBind()
        objEmp_Skills = New Emp_Skills
        objAppraisal_EmployeeSkills = New Appraisal_EmployeeSkills
        With objAppraisal_EmployeeSkills
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .Year = ddlYear.SelectedValue
            .FK_AppraisalStatusId = AppraisalStatus.Pending
            dt = .GetBy_FK_EmployeeId
        End With
        If dt Is Nothing Or dt.Rows.Count = 0 Then
            With objEmp_Skills
                .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                dt = .Get_By_FK_EmployeeId_Appraisal
            End With
        End If
        dgrdSkills.DataSource = dt
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        FillGrid()
        FillGridSkills()
    End Sub

#End Region

#Region "Methods"

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

    Private Sub FillGridSkills()
        Dim dt As DataTable
        dgrdSkills.DataBind()
        objEmp_Skills = New Emp_Skills
        objAppraisal_EmployeeSkills = New Appraisal_EmployeeSkills
        With objAppraisal_EmployeeSkills
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .Year = ddlYear.SelectedValue
            .FK_AppraisalStatusId = AppraisalStatus.Pending
            dt = .GetBy_FK_EmployeeId
        End With


        If dt Is Nothing Or dt.Rows.Count = 0 Then
            With objEmp_Skills
                .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                .Year = ddlYear.SelectedValue
                dt = .Get_By_FK_EmployeeId_Appraisal
            End With
        End If
        dgrdSkills.DataSource = dt
        dgrdSkills.DataBind()
    End Sub

    Private Sub FillGrid()
        Dim dt As DataTable
        objAppraisal_EmployeeGoals = New Appraisal_EmployeeGoals
        With objAppraisal_EmployeeGoals
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FK_AppraisalStatusId = AppraisalStatus.ApprovedbyManager
            .Year = ddlYear.SelectedValue
            dt = .Get_By_FK_EmployeeId_StatusId
        End With
        dgrdEmployeeGoals.DataSource = dt
        dgrdEmployeeGoals.DataBind()
    End Sub

    Private Sub ClearAll()
        chkConfirm.Checked = False
    End Sub

#End Region

    
End Class
