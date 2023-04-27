Imports TA.Definitions
Imports SmartV.UTILITIES
Imports System.Data
Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports TA.Security
Imports TA.Appraisal
Imports SmartV.UTILITIES.ProjectCommon

Partial Class Appraisal_Appraisal_EvaluationPoints
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private EvaluationPoints As Appraisal_EvaluationPoints
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Properties"

    Private Property EvaluationPoint() As Integer
        Get
            Return ViewState("EvaluationPoint")
        End Get
        Set(ByVal value As Integer)
            ViewState("EvaluationPoint") = value
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

#Region "Page events"
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
            FillGrid()

        End If

    End Sub
    Protected Sub RadToolBar1_ButtonClick(sender As Object, e As RadToolBarEventArgs)

    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, _
                              ByVal e As System.EventArgs) Handles btnSave.Click
        EvaluationPoints = New Appraisal_EvaluationPoints
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim errorNum As Integer = -1
        With EvaluationPoints
            .EvaluationPoint = txtEvaluationPoint.Text
            .PointName = txtPointsName.Text.Trim()
            .PointNameArabic = txtPointsNameAr.Text.Trim()
        End With
        If EvaluationPoint = 0 Then
            ' Do add operation 
            errorNum = EvaluationPoints.Add()
            If errorNum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ElseIf errorNum = -5 Then
                CtlCommon.ShowMessage(Me.Page, "Evaluation Point Exist.", "info")
            ElseIf errorNum = -6 Then
                CtlCommon.ShowMessage(Me.Page, "Point Name already Exist.", "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        Else
            ' Do update operations
            EvaluationPoints.OldEvaluationPoint = EvaluationPoint
            errorNum = EvaluationPoints.Update()
            If errorNum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            ElseIf errorNum = -6 Then
                CtlCommon.ShowMessage(Me.Page, "Point Name already Exist.", "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If
        End If
        If errorNum = 0 Then
            FillGrid()
            ClearAll()
        End If
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
        FillGrid()
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdEvaluationPoints.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then

                Dim EvaluationPointId As Integer = Convert.ToInt32(row.GetDataKeyValue("EvaluationPoint").ToString())
                EvaluationPoints = New Appraisal_EvaluationPoints
                EvaluationPoints.EvaluationPoint = EvaluationPointId
                errNum = EvaluationPoints.Delete()
                With strBuilder

                End With
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
            'showResult(CodeResultMessage.CodeDeleteFail)

        End If

        ClearAll()
    End Sub
    Protected Sub dgrdEvaluationPoints_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdEvaluationPoints.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item) Then
            Dim Itm As GridDataItem = e.Item
        End If
    End Sub
    Protected Sub dgrdEvaluationPoints_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEvaluationPoints.NeedDataSource
        Try
            EvaluationPoints = New Appraisal_EvaluationPoints
            dgrdEvaluationPoints.DataSource = EvaluationPoints.GetAll()

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub dgrdEvaluationPoints_ItemCommand(sender As Object, e As GridCommandEventArgs)
        If dgrdEvaluationPoints.SelectedItems.Count = 1 Then
            EvaluationPoint = DirectCast(dgrdEvaluationPoints.SelectedItems(0), GridDataItem).GetDataKeyValue("EvaluationPoint").ToString().Trim
            FillControls()
        End If
    End Sub

#End Region

#Region "Page events"

    Private Sub FillGrid()
        Try
            EvaluationPoints = New Appraisal_EvaluationPoints
            dgrdEvaluationPoints.DataSource = EvaluationPoints.GetAll()
            dgrdEvaluationPoints.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ClearAll()
        ' Clear controls
        txtPointsName.Text = String.Empty
        txtPointsNameAr.Text = String.Empty
        txtEvaluationPoint.Text = String.Empty
        ' reset to id to next add
        EvaluationPoint = 0
        ' Remove sorting and sorting direction

    End Sub

    Private Sub FillControls()
        ' Get the data by the PK and display on the controls
        EvaluationPoints = New Appraisal_EvaluationPoints
        EvaluationPoints.EvaluationPoint = EvaluationPoint
        EvaluationPoints.GetByPK()
        With EvaluationPoints
            txtEvaluationPoint.Text = .EvaluationPoint
            txtPointsName.Text = .PointName
            txtPointsNameAr.Text = .PointNameArabic
        End With
    End Sub

#End Region
End Class
