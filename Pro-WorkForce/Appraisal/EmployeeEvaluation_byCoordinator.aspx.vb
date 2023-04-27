Imports SmartV.UTILITIES
Imports TA.Appraisal
Imports System.Data

Partial Class Appraisal_EmployeeEvaluation
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objAppraisal_EmployeeGoals As Appraisal_EmployeeGoals
    Private objAppraisal_EvaluationPoints As Appraisal_EvaluationPoints

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
            .FK_EmployeeId = EmployeeFilterUC.EmployeeId
            .FK_AppraisalStatusId = AppraisalStatus.ApprovedbyManager
            dt = .Get_By_FK_EmployeeId_StatusId
        End With
        dgrdEmployeeGoals.DataSource = dt
    End Sub

    Protected Sub btnSendtoMgr_Click(sender As Object, e As EventArgs) Handles btnSendtoMgr.Click
        objAppraisal_EmployeeGoals = New Appraisal_EmployeeGoals
        Dim err As Integer = -1
        If chkConfirm.Checked = True Then
            With objAppraisal_EmployeeGoals
                For Each item As GridDataItem In dgrdEmployeeGoals.Items
                    .GoalId = Convert.ToInt32(item("GoalId").Text.ToString)
                    .GetByPK()
                    .EvaluationPointbyEmployee = DirectCast(item.FindControl("radEvaluationPointbyEmployee"), RadRating).Value
                    .EmployeeRemarks = DirectCast(item.FindControl("txtEmployeeRemarks"), TextBox).Text
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    err = .Update()
                    .FK_AppraisalStatusId = AppraisalStatus.EvaluatedByEmployee
                    .UpdateStatus()
                Next
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

#End Region

#Region "Methods"

    Public Sub FillGrid()
        Dim dt As DataTable
        objAppraisal_EmployeeGoals = New Appraisal_EmployeeGoals
        With objAppraisal_EmployeeGoals
            .FK_EmployeeId = EmployeeFilterUC.EmployeeId
            .FK_AppraisalStatusId = AppraisalStatus.ApprovedbyManager
            dt = .Get_By_FK_EmployeeId_StatusId
        End With
        dgrdEmployeeGoals.DataSource = dt
        dgrdEmployeeGoals.DataBind()
    End Sub

    Private Sub ClearAll()
        chkConfirm.Checked = False
        EmployeeFilterUC.ClearValues()
    End Sub

#End Region

End Class
