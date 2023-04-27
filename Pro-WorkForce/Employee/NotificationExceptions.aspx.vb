﻿Imports SmartV.UTILITIES
Imports TA.Employees
Imports System.Data
Imports TA.Security
Imports Telerik.Web.UI

Partial Class Employee_NotificationExceptions
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objNotification_Exception As New Notification_Exception
    Shared SortExepression As String

#End Region

#Region "Page Propereties"

    Public Property dtCurrentControls() As DataTable
        Get
            Return ViewState("dtCurrentControls")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrentControls") = value
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

    Public Property FK_EmployeeID() As Integer
        Get
            Return ViewState("FK_EmployeeID")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_EmployeeID") = value
        End Set
    End Property

    Public Property FromDate() As DateTime
        Get
            Return ViewState("FromDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("FromDate") = value
        End Set
    End Property

    Public Property NotificationExceptionId() As Integer
        Get
            Return ViewState("NotificationExceptionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("NotificationExceptionId") = value
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
        showHide(chckTemporary.Checked)

        If Not Page.IsPostBack Then
            rfvToDate.Enabled = False
            ValidatorCalloutExtender2.Enabled = False
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            EmployeeFilter1.IsEmployeeRequired = True
            EmployeeFilter1.EmployeeRequiredValidationGroup = btnSave.ValidationGroup

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                PageHeader1.HeaderText = ResourceManager.GetString("Notification_Exception", CultureInfo)
            Else
                Lang = CtlCommon.Lang.EN
                PageHeader1.HeaderText = ResourceManager.GetString("Notification_Exception", CultureInfo)

            End If

            Me.dtpFromdate.SelectedDate = Today
            Me.dtpEndDate.SelectedDate = Today

            FillGrid()

        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdTAExceptions.ClientID + "')")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrentControls.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not pnlTAExceptions.FindControl(row("AddBtnName")) Is Nothing Then
                        pnlTAExceptions.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not pnlTAExceptions.FindControl(row("DeleteBtnName")) Is Nothing Then
                        pnlTAExceptions.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not pnlTAExceptions.FindControl(row("EditBtnName")) Is Nothing Then
                        pnlTAExceptions.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not pnlTAExceptions.FindControl(row("PrintBtnName")) Is Nothing Then
                        pnlTAExceptions.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click

        objNotification_Exception = New Notification_Exception
        Dim errornum As Integer
        With objNotification_Exception
            .FK_EmployeeId = EmployeeFilter1.EmployeeId
            .FromDate = dtpFromdate.SelectedDate
            If chckTemporary.Checked Then
                .ToDate = dtpEndDate.SelectedDate
            Else
                .ToDate = Nothing
            End If

            .Reason = txtReason.Text

            If FK_EmployeeID = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                .LAST_UPDATE_BY = String.Empty
                errornum = .Add()
            Else
                .NotificationExceptionId = NotificationExceptionId
                .FK_EmployeeId = FK_EmployeeID
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                errornum = .Update()
            End If

        End With

        If errornum = 0 Then
            If FK_EmployeeID = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            End If
            ClearAll()
            FillGrid()
        ElseIf errornum = -99 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DateExist", CultureInfo), "info")
        Else
            If FK_EmployeeID = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If

        End If

    End Sub

    Protected Sub dgrdTAExceptions_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdTAExceptions.NeedDataSource

        objNotification_Exception = New Notification_Exception()
        dtCurrent = objNotification_Exception.GetInnerEmployee()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdTAExceptions.DataSource = dv

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Dim err As Integer
        objNotification_Exception = New Notification_Exception()
        For Each row As GridDataItem In dgrdTAExceptions.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                NotificationExceptionId = Convert.ToInt32(row.GetDataKeyValue("NotificationExceptionId").ToString())
                With objNotification_Exception
                    .NotificationExceptionId = NotificationExceptionId
                    err += .Delete()
                End With
            End If
        Next

        If err = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            ClearAll()
            FillGrid()
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "success")
        End If

    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click

        ClearAll()


        dgrdTAExceptions.SelectedIndexes.Clear()

    End Sub

    Protected Sub dgrdTAException_SelectedIndexedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgrdTAExceptions.SelectedIndexChanged

        ClearAll()
        NotificationExceptionId = Convert.ToInt32(DirectCast(dgrdTAExceptions.SelectedItems(0), GridDataItem).GetDataKeyValue("NotificationExceptionId").ToString())
        FK_EmployeeID = Convert.ToInt32(DirectCast(dgrdTAExceptions.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_EmployeeId").ToString())
        FromDate = Convert.ToDateTime(DirectCast(dgrdTAExceptions.SelectedItems(0), GridDataItem).GetDataKeyValue("FromDate").ToString())
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objNotification_Exception = New Notification_Exception()

        With objNotification_Exception

            .FK_EmployeeId = FK_EmployeeID
            .FromDate = FromDate
            .NotificationExceptionId = NotificationExceptionId
            .GetByPK()
            EmployeeFilter1.IsEntityClick = "True"
            EmployeeFilter1.GetEmployeeInfo(FK_EmployeeID)
            If .ToDate = DateTime.MinValue Then
                chckTemporary.Checked = False
                pnlEndDate.Visible = False
            Else
                chckTemporary.Checked = True
                pnlEndDate.Visible = True
                dtpEndDate.SelectedDate = .ToDate
            End If

            dtpFromdate.SelectedDate = .FromDate
            dtpFromdate.Enabled = False
            txtReason.Text = .Reason
            btnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)

        End With

    End Sub

    Protected Sub dgrdTAExceptions_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdTAExceptions.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
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

#End Region

#Region "Page Methods"

    Private Sub showHide(ByVal status As Boolean, Optional ByVal order As Integer = 0)

        pnlEndDate.Visible = status

        If (status) Then
            rfvToDate.Enabled = True
            ValidatorCalloutExtender2.Enabled = True
        Else
            rfvToDate.Enabled = False
            ValidatorCalloutExtender2.Enabled = False
        End If

    End Sub

    Private Sub ClearAll()
        ' Clear the controls
        chckTemporary.Checked = False
        pnlEndDate.Visible = False
        dtpFromdate.SelectedDate = Today
        dtpEndDate.SelectedDate = Today
        EmployeeFilter1.ClearValues()
        txtReason.Text = String.Empty
        FK_EmployeeID = 0
        btnSave.Text = ResourceManager.GetString("btnSave", CultureInfo)
        dtpFromdate.Enabled = True
    End Sub

    Private Sub FillGrid()

        objNotification_Exception = New Notification_Exception()
        dtCurrent = objNotification_Exception.GetInnerEmployee()
        dgrdTAExceptions.DataSource = dtCurrent
        dgrdTAExceptions.DataBind()

    End Sub

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdTAExceptions.Skin))
    End Function

    Protected Sub dgrdTAExceptions_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdTAExceptions.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

#End Region

End Class
