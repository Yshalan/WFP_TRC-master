Imports Telerik.Web.UI
Imports SmartV.UTILITIES
Imports TA.DailyTasks
Imports System.Data
Imports TA.Security
Imports TA.Employees

Partial Class DailyTasks_HR_TAExceptionsApproval
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objHR_TAExceptionRequest As New HR_TAExceptionRequest
    Dim objEmp_TAExceptions As Emp_TAExceptions
    Shared SortExepression As String
    Public strLang As String
    Public MsgLang As String
    Private objNotification_Exception As Notification_Exception

#End Region

#Region "Properties"

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

    Public Property dtCurrentControls() As DataTable
        Get
            Return ViewState("dtCurrentControls")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrentControls") = value
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

    Public Property FromDate() As DateTime
        Get
            Return ViewState("FromDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("FromDate") = value
        End Set
    End Property

    Public Property UserId() As Integer
        Get
            Return ViewState("UserId")
        End Get
        Set(ByVal value As Integer)
            ViewState("UserId") = value
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
                strLang = "ar"
                MsgLang = "ar"
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                Lang = CtlCommon.Lang.EN
                strLang = "en"
                MsgLang = "en"
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo

        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            UserId = SessionVariables.LoginUser.ID
            PageHeader1.HeaderText = ResourceManager.GetString("HR_TAExceptionApproval", CultureInfo)
            FillGrid()
            FillGridRejected()
            mvTAExceptionApproval.ActiveViewIndex = 0
        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrentControls.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not mvTAExceptionApproval.FindControl(row("AddBtnName")) Is Nothing Then
                        mvTAExceptionApproval.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not mvTAExceptionApproval.FindControl(row("DeleteBtnName")) Is Nothing Then
                        mvTAExceptionApproval.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not mvTAExceptionApproval.FindControl(row("EditBtnName")) Is Nothing Then
                        mvTAExceptionApproval.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not mvTAExceptionApproval.FindControl(row("PrintBtnName")) Is Nothing Then
                        mvTAExceptionApproval.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub lnkReject_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FK_EmployeeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_EmployeeId").ToString())
        FromDate = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FromDate").ToString())
        'RejectTAExceptionRequest()

    End Sub

    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FK_EmployeeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_EmployeeId").ToString())
        FromDate = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FromDate").ToString())
        AcceptTAExceptionRequest()
    End Sub

    Protected Sub btnAccept_Click(sender As Object, e As System.EventArgs) Handles btnAccept.Click
        AcceptTAExceptionRequest()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        mvTAExceptionApproval.ActiveViewIndex = 0
        FillGrid()
    End Sub

    Protected Sub btnReject_Click(sender As Object, e As System.EventArgs) Handles btnReject.Click
        'RejectTAExceptionRequest()
    End Sub

    Protected Sub dgrdRequests_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdRequests.NeedDataSource
        objHR_TAExceptionRequest = New HR_TAExceptionRequest
        dtCurrent = objHR_TAExceptionRequest.HR_TAExceptionRequest_Select_AllInner_Rejected()
        dgrdRequests.DataSource = dtCurrent
    End Sub

    Protected Sub dgrdTAExceptions_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdTAExceptions.NeedDataSource
        objHR_TAExceptionRequest = New HR_TAExceptionRequest
        dtCurrent = objHR_TAExceptionRequest.HR_TAExceptionRequest_Select_AllInner_ByStatus()
        dgrdTAExceptions.DataSource = dtCurrent
    End Sub

    Protected Sub btnRejectPOP_Click(sender As Object, e As System.EventArgs) Handles btnRejectPOP.Click
        RejectTAExceptionRequest()
    End Sub

#End Region

#Region "Methods"

    Private Sub AcceptTAExceptionRequest()
        Dim err As Integer = -1
        objHR_TAExceptionRequest = New HR_TAExceptionRequest
        objNotification_Exception = New Notification_Exception

        objHR_TAExceptionRequest.FK_EmployeeId = FK_EmployeeId
        objHR_TAExceptionRequest.FromDate = FromDate
        objHR_TAExceptionRequest.GetByPK()

        objEmp_TAExceptions = New Emp_TAExceptions
        With objEmp_TAExceptions
            .FK_EmployeeId = FK_EmployeeId
            .FromDate = FromDate
            .ToDate = objHR_TAExceptionRequest.ToDate
            .Reason = objHR_TAExceptionRequest.Reason
            .CREATED_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_BY = 0
            err = .Add()
        End With
        If err = 0 Then
            With objNotification_Exception
                .FK_EmployeeId = FK_EmployeeId
                .FromDate = FromDate
                .ToDate = objHR_TAExceptionRequest.ToDate
                .Reason = objHR_TAExceptionRequest.Reason
                .Active = True
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add()
            End With
        End If
        If err = 0 Then
            objHR_TAExceptionRequest = New HR_TAExceptionRequest
            With objHR_TAExceptionRequest
                .FK_EmployeeId = FK_EmployeeId
                .FromDate = FromDate
                .IsRejected = False
                .RejectionReason = Me.txtRejectedReason.Text
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .Update_TAException_RequestStatus()
            End With
        End If

        If err = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            mvTAExceptionApproval.ActiveViewIndex = 0
            FillGrid()
        ElseIf err = -99 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DateExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Private Sub RejectTAExceptionRequest()
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        objHR_TAExceptionRequest = New HR_TAExceptionRequest
        objHR_TAExceptionRequest.FK_EmployeeId = FK_EmployeeId
        objHR_TAExceptionRequest.FromDate = FromDate
        objHR_TAExceptionRequest.IsRejected = True
        objHR_TAExceptionRequest.RejectionReason = txtRejectedReason.Text
        Err = objHR_TAExceptionRequest.Update_TAException_RequestStatus
        strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
        If Err = 0 Then
            mvTAExceptionApproval.ActiveViewIndex = 0
            FillGrid()
            FillGridRejected()
            CtlCommon.ShowMessage(Me.Page, strMessage, "success")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "success")
        End If
    End Sub

    Private Sub FillGrid()

        objHR_TAExceptionRequest = New HR_TAExceptionRequest
        objHR_TAExceptionRequest.UserId = UserId
        dtCurrent = objHR_TAExceptionRequest.HR_TAExceptionRequest_Select_AllInner_ByStatus()
        dgrdTAExceptions.DataSource = dtCurrent
        dgrdTAExceptions.DataBind()

    End Sub

    Private Sub FillGridRejected()

        objHR_TAExceptionRequest = New HR_TAExceptionRequest
        'objHR_TAExceptionRequest.UserId = UserId
        dtCurrent = objHR_TAExceptionRequest.HR_TAExceptionRequest_Select_AllInner_Rejected()
        dgrdRequests.DataSource = dtCurrent
        dgrdRequests.DataBind()

    End Sub

    Private Sub FillControls()
        EmployeeFilter1.IsEntityClick = "True"
        EmployeeFilter1.GetEmployeeInfo(FK_EmployeeId)
        objHR_TAExceptionRequest = New HR_TAExceptionRequest
        With objHR_TAExceptionRequest
            .FK_EmployeeId = FK_EmployeeId
            .FromDate = FromDate
            .GetByPK()
            dtpFromdate.SelectedDate = .FromDate
            If .ToDate = DateTime.MinValue Then
                chckTemporary.Checked = False
                pnlEndDate.Visible = False
            Else
                chckTemporary.Checked = True
                pnlEndDate.Visible = True
                dtpEndDate.SelectedDate = .ToDate
            End If
            txtReason.Text = .Reason
        End With
    End Sub

    Private Sub EnableControl(ByVal status As Boolean)
        EmployeeFilter1.EnabledDisbaledControls(False)
        dtpFromdate.Enabled = status
        dtpEndDate.Enabled = status
        txtReason.Enabled = status
        chckTemporary.Enabled = status
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
        ElseIf (e.CommandName = "reject") Then
            FK_EmployeeId = Convert.ToInt32(DirectCast(e.Item, GridDataItem).GetDataKeyValue("FK_EmployeeId").ToString())
            FromDate = Convert.ToDateTime(DirectCast(e.Item, GridDataItem).GetDataKeyValue("FromDate").ToString())
            txtRejectedReason.Text = String.Empty
            mpeRejectPopupTAException.Show()
        ElseIf (e.CommandName = "RowClick") Then
            FK_EmployeeId = Convert.ToInt32(DirectCast(e.Item, GridDataItem).GetDataKeyValue("FK_EmployeeId").ToString())
            FromDate = Convert.ToDateTime(DirectCast(e.Item, GridDataItem).GetDataKeyValue("FromDate").ToString())
            FillControls()
            EnableControl(False)
            mvTAExceptionApproval.ActiveViewIndex = 1
        End If

    End Sub

    Protected Sub RadToolBar2_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon1() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdRequests.Skin))
    End Function

    Protected Sub dgrdRequests_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdRequests.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If

    End Sub

#End Region

End Class
