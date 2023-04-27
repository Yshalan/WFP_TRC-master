Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.Reports
Imports System.Data.SqlClient
Imports SmartV.DB
Imports SmartV.UTILITIES.ProjectCommon
Imports System.Data

Partial Class Reports_CreateDynamicReport
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objDynamicReports As DynamicReports
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objDac As SmartV.DB.DAC

#End Region

#Region "Properties"

    Private Property ReportId() As Integer
        Get
            Return ViewState("ReportId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ReportId") = value
        End Set
    End Property

#End Region

#Region "PageEvents"

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            PageHeader1.HeaderText = ResourceManager.GetString("CreateDynamicReport")
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            FillGridView()
        End If
        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdVwDynamicRpt.ClientID + "');")
    End Sub

    Protected Sub dgrdVwDynamicRpt_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdVwDynamicRpt.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If

    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdVwDynamicRpt.Skin))
    End Function

    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        objDynamicReports = New DynamicReports
        Dim SQLStr As String
        Dim errStr As String
        Dim errNo As Integer
        Dim dt As DataTable = Nothing
        With objDynamicReports
            .ReportName = txtReportName.Text
            .ViewName = txtReportView.Text.Trim()
            If ReportId = 0 Then

                errNo = .Add()
                If errNo = 0 Then
                    SQLStr = "CREATE VIEW " & txtReportView.Text.Trim() & " AS " & txtSQLQuery.Text
                    .SQLQuery = SQLStr
                    dt = .ExecSQLQuery()
                    errStr = dt.Rows(0)(0)
                    If errStr = "Success" Then
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully"), "success")
                        ClearAll()
                        FillGridView()
                    Else
                        CtlCommon.ShowMessage(Me.Page, errStr, "info")
                    End If

                ElseIf errNo = -5 Then
                    CtlCommon.ShowMessage(Me.Page, "Report Name Already Exists", "info")
                End If

            Else
                errNo = .Update()
                If errNo = 0 Then
                    SQLStr = "ALTER VIEW " & txtReportView.Text.Trim() & " AS " & txtSQLQuery.Text
                    .SQLQuery = SQLStr
                    dt = .ExecSQLQuery()
                    errStr = dt.Rows(0)(0)
                    If errStr = "Success" Then
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully"), "success")
                        ClearAll()
                        FillGridView()
                    Else
                        CtlCommon.ShowMessage(Me.Page, errStr, "info")
                    End If
                End If

            End If

        End With
    End Sub

    Protected Sub dgrdVwDynamicRpt_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdVwDynamicRpt.NeedDataSource
        objDynamicReports = New DynamicReports()
        dgrdVwDynamicRpt.DataSource = objDynamicReports.GetAll()
    End Sub

    Protected Sub dgrdVwDynamicRpt_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dgrdVwDynamicRpt.SelectedIndexChanged
        ReportId = Convert.ToInt32(DirectCast(dgrdVwDynamicRpt.SelectedItems(0), GridDataItem).GetDataKeyValue("ReportId"))
        objDynamicReports = New DynamicReports
        objDynamicReports.ReportId = ReportId
        objDynamicReports.GetByPK()
        With objDynamicReports
            txtReportName.Text = .ReportName
            txtReportView.Text = .ViewName
            Dim dt As DataTable = Nothing
            Dim ViewDefinition As String = ""
            dt = .GetViewDefinition
            ViewDefinition = dt.Rows(0)(0)
            Dim cut_at As String = "AS"
            Dim x As Integer = InStr(ViewDefinition, cut_at)
            Dim SQLQuery As String = ViewDefinition.Substring(x + cut_at.Length - 1)

            txtSQLQuery.Text = SQLQuery
        End With
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        ClearAll()
        FillGridView()
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1
        Dim SQLStr As String
        Dim dt As DataTable = Nothing
        objDynamicReports = New DynamicReports
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdVwDynamicRpt.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim strCode As String = row.GetDataKeyValue("ViewName")
                objDynamicReports.ReportId = Convert.ToInt32(row.GetDataKeyValue("ReportId"))
                objDynamicReports.ViewName = row.GetDataKeyValue("ViewName")
                errNum = objDynamicReports.Delete()

            End If
        Next
        If errNum = 0 Then
            SQLStr = "DROP VIEW " & objDynamicReports.ViewName
            objDynamicReports.SQLQuery = SQLStr
            dt = objDynamicReports.ExecSQLQuery()
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully"), "success")
            FillGridView()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        End If

        ClearAll()
    End Sub

#End Region

#Region "Methods"

    Private Sub ClearAll()
        txtReportName.Text = String.Empty
        txtReportView.Text = String.Empty
        txtSQLQuery.Text = String.Empty
        ReportId = 0
    End Sub

    Private Sub FillGridView()
        Try
            objDynamicReports = New DynamicReports()
            dgrdVwDynamicRpt.DataSource = objDynamicReports.GetAll()
            dgrdVwDynamicRpt.DataBind()
        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class
