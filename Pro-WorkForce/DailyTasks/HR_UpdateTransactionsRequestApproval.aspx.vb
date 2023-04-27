Imports Telerik.Web.UI
Imports TA.Security
Imports SmartV.UTILITIES
Imports System.Data
Imports TA.DailyTasks
Imports TA.Employees
Imports TA.Admin
Imports System.IO

Partial Class DailyTasks_HR_UpdateTransactionsRequestApproval
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmpMove As Emp_Move
    Private objHR_Emp_MoveUpdateRequest As HR_Emp_MoveUpdateRequest
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objREADER_KEYS As READER_KEYS
    Private objEmployee As Employee
    Private objTA_Reason As TA_Reason
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Public MsgLang As String
    Public strLang As String
    Private objEmp_Move As Emp_Move
    Private objAPP_Settings As APP_Settings
    Private objRecalculateRequest As RecalculateRequest

#End Region

#Region "Properties"
    Public Property MoveRequestId() As Integer
        Get
            Return ViewState("MoveRequestId")
        End Get
        Set(ByVal value As Integer)
            ViewState("MoveRequestId") = value
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

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

    Public Property SortExepression() As String
        Get
            Return ViewState("SortExepression")
        End Get
        Set(ByVal value As String)
            ViewState("SortExepression") = value
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

    Public Property tbl_id() As Long
        Get
            Return ViewState("tbl_id")
        End Get
        Set(ByVal value As Long)
            ViewState("tbl_id") = value
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

    Private Enum UpdateApplicationType
        UpdateTransaction = 1
        DeleteTransaction = 2
    End Enum

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

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR

            Else
                Lang = CtlCommon.Lang.EN

            End If

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            UserId = SessionVariables.LoginUser.ID
            mvManualEntry.ActiveViewIndex = 0
            FillGrid()
            FillGridRejected()
            PageHeader1.HeaderText = ResourceManager.GetString("HRUpdateTransactionApproval", CultureInfo)

        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrentControls.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not mvManualEntry.FindControl(row("AddBtnName")) Is Nothing Then
                        mvManualEntry.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not mvManualEntry.FindControl(row("DeleteBtnName")) Is Nothing Then
                        mvManualEntry.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not mvManualEntry.FindControl(row("EditBtnName")) Is Nothing Then
                        mvManualEntry.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not mvManualEntry.FindControl(row("PrintBtnName")) Is Nothing Then
                        mvManualEntry.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next


    End Sub

    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        MoveRequestId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("MoveRequestId").ToString())
        AcceptManualEntryRequest()
    End Sub

    Protected Sub lnkReject_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        MoveRequestId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("MoveRequestId").ToString())
        'RejectManualEntryRequest()

    End Sub

    Protected Sub btnAccept_Click(sender As Object, e As System.EventArgs) Handles btnAccept.Click
        AcceptManualEntryRequest()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        mvManualEntry.ActiveViewIndex = 0
    End Sub

    Protected Sub btnReject_Click(sender As Object, e As System.EventArgs) Handles btnReject.Click
        'RejectManualEntryRequest()
    End Sub

    Protected Sub dgrdEntries_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdEntries.ItemDataBound

        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
            End If

            If Lang = CtlCommon.Lang.AR Then
                item("ReasonName").Text = DirectCast(item.FindControl("hdnReasonArabicName"), HiddenField).Value
            End If

            If Lang = CtlCommon.Lang.AR Then
                item("UpdateTransactionTypeEn").Text = DirectCast(item.FindControl("hdnUpdateTransactionTypeAr"), HiddenField).Value
            End If

            Dim MoveRequestId As Integer = item.GetDataKeyValue("MoveRequestId")
            Dim AttachedFile As String = IIf(Not IsDBNull(item.GetDataKeyValue("AttachedFile")), item.GetDataKeyValue("AttachedFile"), "")
            If AttachedFile = "" Then
                'DirectCast(e.Item.FindControl("lnbView"), HtmlAnchor).Visible = False
                DirectCast(e.Item.FindControl("lnbView"), LinkButton).Visible = False
            Else
                'DirectCast(e.Item.FindControl("lnbView"), HtmlAnchor).HRef = "..\HR_HR_UpdateTransactionsRequestFiles\" + MoveRequestId.ToString() + AttachedFile
            End If
        End If
    End Sub

    Protected Sub dgrdRejectedRequests_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdRejectedRequests.ItemDataBound

        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
            End If

            If Lang = CtlCommon.Lang.AR Then
                item("ReasonName").Text = DirectCast(item.FindControl("hdnReasonArabicName"), HiddenField).Value
            End If

            If Lang = CtlCommon.Lang.AR Then
                item("UpdateTransactionTypeEn").Text = DirectCast(item.FindControl("hdnUpdateTransactionTypeAr"), HiddenField).Value
            End If

        End If
    End Sub

    Protected Sub dgrdRejectedRequests_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdRejectedRequests.NeedDataSource
        dgrdRejectedRequests.DataSource = Nothing
        objHR_Emp_MoveUpdateRequest = New HR_Emp_MoveUpdateRequest
        objHR_Emp_MoveUpdateRequest.UserId = UserId
        dtCurrent = objHR_Emp_MoveUpdateRequest.GetAll_Rejected()
        dgrdRejectedRequests.DataSource = dtCurrent
    End Sub

    Protected Sub dgrdEntries_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEntries.NeedDataSource
        dgrdEntries.DataSource = Nothing
        objHR_Emp_MoveUpdateRequest = New HR_Emp_MoveUpdateRequest
        objHR_Emp_MoveUpdateRequest.UserId = UserId
        dtCurrent = objHR_Emp_MoveUpdateRequest.GetAll_ByStatus()
        dgrdEntries.DataSource = dtCurrent
    End Sub

    Protected Sub btnRejectPOP_Click(sender As Object, e As System.EventArgs) Handles btnRejectPOP.Click
        RejectManualEntryRequest()
    End Sub

    Protected Sub lnkDownloadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim MoveRequestId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("MoveRequestId"))
        Dim AttachedFile As String = CStr(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("AttachedFile"))
        If AttachedFile = "" Then
            DirectCast(dgrdEntries.FindControl("lnbView"), LinkButton).Visible = False
        Else
            Dim FileName As String = MoveRequestId.ToString() + AttachedFile
            Dim FilePath As String = Server.MapPath("..\\HR_UpdateTransactionsRequestFiles\") & FileName
            CtlCommon.Open_Download_File(FileName, FilePath)
        End If
    End Sub
#End Region

#Region "Methods"

    Public Sub FillGrid()
        dgrdEntries.DataSource = Nothing
        objHR_Emp_MoveUpdateRequest = New HR_Emp_MoveUpdateRequest
        objHR_Emp_MoveUpdateRequest.UserId = UserId
        dtCurrent = objHR_Emp_MoveUpdateRequest.GetAll_ByStatus()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdEntries.DataSource = dv
        dgrdEntries.DataBind()

    End Sub

    Private Sub FillGridRejected()
        dgrdRejectedRequests.DataSource = Nothing
        objHR_Emp_MoveUpdateRequest = New HR_Emp_MoveUpdateRequest
        objHR_Emp_MoveUpdateRequest.UserId = UserId
        dtCurrent = objHR_Emp_MoveUpdateRequest.GetAll_Rejected()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdRejectedRequests.DataSource = dv
        dgrdRejectedRequests.DataBind()
    End Sub

    Private Sub AcceptManualEntryRequest()

        Dim err As Integer
        objHR_Emp_MoveUpdateRequest = New HR_Emp_MoveUpdateRequest
        With objHR_Emp_MoveUpdateRequest
            .MoveRequestId = MoveRequestId
            .GetByPK()
            FK_EmployeeId = .FK_EmployeeId
        End With

        Dim err_Update As Integer = -1
        objHR_Emp_MoveUpdateRequest = New HR_Emp_MoveUpdateRequest
        With objHR_Emp_MoveUpdateRequest
            .MoveRequestId = MoveRequestId
            .IsRejected = False
            .RejectionReason = Me.txtRejectedReason.Text
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .Update_Request_Status()
            .GetByPK()

            objEmp_Move = New Emp_Move

            objEmp_Move.MoveId = objHR_Emp_MoveUpdateRequest.MoveId
            objEmp_Move.Type = objHR_Emp_MoveUpdateRequest.Type
            objEmp_Move.FK_ReasonId = objHR_Emp_MoveUpdateRequest.FK_ReasonId
            objEmp_Move.Remarks = objHR_Emp_MoveUpdateRequest.Remarks
            objEmp_Move.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            If .UpdateTransactionType = UpdateApplicationType.UpdateTransaction Then
                err_Update = objEmp_Move.Update_TransactionType()
            Else
                err_Update = objEmp_Move.Delete
            End If

            If (File.Exists(Server.MapPath("~\HR_UpdateTransactionsRequestFiles\\" + MoveRequestId.ToString() + objHR_Emp_MoveUpdateRequest.AttachedFile))) Then
                If (File.Exists(Server.MapPath("~\HR_UpdateTransactionsRequestFiles\\" + objHR_Emp_MoveUpdateRequest.MoveRequestId.ToString() + objHR_Emp_MoveUpdateRequest.AttachedFile))) Then
                    Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("~\HR_UpdateTransactionsRequestFiles\\" + objHR_Emp_MoveUpdateRequest.MoveRequestId.ToString() + objHR_Emp_MoveUpdateRequest.AttachedFile))
                    CopyFile.CopyTo(Server.MapPath("~\ManualEntryFiles\\" + objHR_Emp_MoveUpdateRequest.MoveRequestId.ToString() + objHR_Emp_MoveUpdateRequest.AttachedFile))
                End If

            End If
        End With

        If err = 0 And err_Update = 0 Then
            Dim err2 As Integer = -1
            objAPP_Settings = New APP_Settings
            objRECALC_REQUEST = New RECALC_REQUEST
            objAPP_Settings.GetByPK()
            If objAPP_Settings.ApprovalRecalMethod = 1 Then
                objRECALC_REQUEST.EMP_NO = objHR_Emp_MoveUpdateRequest.FK_EmployeeId
                objRECALC_REQUEST.VALID_FROM_NUM = DateToString(objHR_Emp_MoveUpdateRequest.MoveDate)
                objRECALC_REQUEST.RECALCULATE()
            Else
                If Not objHR_Emp_MoveUpdateRequest.MoveDate > Date.Today Then
                    objRecalculateRequest = New RecalculateRequest
                    With objRecalculateRequest
                        .Fk_EmployeeId = objHR_Emp_MoveUpdateRequest.FK_EmployeeId
                        .FromDate = objHR_Emp_MoveUpdateRequest.MoveDate
                        .ToDate = objHR_Emp_MoveUpdateRequest.MoveDate
                        .ImmediatelyStart = True
                        .RecalStatus = 0
                        .CREATED_BY = SessionVariables.LoginUser.ID
                        If objHR_Emp_MoveUpdateRequest.UpdateTransactionType = UpdateApplicationType.UpdateTransaction Then
                            .Remarks = "Update Transaction Request Approval By HR- SYSTEM"
                        Else
                            .Remarks = "Delete Transaction Request Approval By HR- SYSTEM"
                        End If
                        err2 = .Add
                    End With
                End If
            End If
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")

        End If

    End Sub

    Private Function DateToString(ByVal TempDate As Date) As String
        Dim tempDay As String
        Dim tempMonth As String

        If TempDate.Month.ToString.Length = 1 Then
            tempMonth = "0" + TempDate.Month.ToString
        Else
            tempMonth = TempDate.Month.ToString
        End If
        If TempDate.Day.ToString.Length = 1 Then
            tempDay = "0" + TempDate.Day.ToString
        Else
            tempDay = TempDate.Day.ToString
        End If
        Return TempDate.Year.ToString() + tempMonth + tempDay
    End Function

    Private Sub RejectManualEntryRequest()
        objHR_Emp_MoveUpdateRequest = New HR_Emp_MoveUpdateRequest
        Dim Err As Integer = 0
        Dim strMessage As String = String.Empty
        With objHR_Emp_MoveUpdateRequest
            .MoveRequestId = MoveRequestId
            .IsRejected = True
            .RejectionReason = txtRejectedReason.Text
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            Err = .Update_Request_Status()
        End With
        strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
        If Err = 0 Then
            FillGrid()
            FillGridRejected()
            CtlCommon.ShowMessage(Me.Page, strMessage, "success")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
        End If

    End Sub

    Private Sub FillReasons()
        objTA_Reason = New TA_Reason()
        CtlCommon.FillTelerikDropDownList(ddlReason, objTA_Reason.GetAll, Lang)

    End Sub

    Private Sub FillControls()

        FillReasons()
        tbl_id = CInt(CType(dgrdEntries.SelectedItems(0), GridDataItem).GetDataKeyValue("MoveRequestId").ToString())
        objHR_Emp_MoveUpdateRequest = New HR_Emp_MoveUpdateRequest
        objHR_Emp_MoveUpdateRequest.MoveRequestId = tbl_id
        objHR_Emp_MoveUpdateRequest.GetByPK()

        EmployeeFilterUC.IsEntityClick = "True"
        EmployeeFilterUC.GetEmployeeInfo(objHR_Emp_MoveUpdateRequest.FK_EmployeeId)

        txtremarks.Text = objHR_Emp_MoveUpdateRequest.Remarks

        If objHR_Emp_MoveUpdateRequest.MoveDate > RadDatePicker1.MinDate Then
            RadDatePicker1.SelectedDate = objHR_Emp_MoveUpdateRequest.MoveDate
        Else
            RadDatePicker1.Clear()
        End If
        rmtToTime2.Text = SetTimeFormat(objHR_Emp_MoveUpdateRequest.MoveTime)
        ddlReason.SelectedValue = objHR_Emp_MoveUpdateRequest.FK_ReasonId

    End Sub

    Private Sub EnableControl(ByVal Status As Boolean)
        EmployeeFilterUC.EnabledDisbaledControls(False)
        RadDatePicker1.Enabled = Status
        rmtToTime2.Enabled = Status
        ddlReason.Enabled = Status
        txtremarks.Enabled = Status
    End Sub

    Private Function SetTimeFormat(ByVal TimeToFormat As DateTime) As String
        Dim TimeM As String
        Dim TimeH As String

        If TimeToFormat.Hour.ToString.Length < 2 Then
            TimeH = "0" + TimeToFormat.Hour.ToString()
        Else
            TimeH = TimeToFormat.Hour.ToString()
        End If

        If TimeToFormat.Minute.ToString.Length < 2 Then
            TimeM = "0" + TimeToFormat.Minute.ToString()
        Else
            TimeM = TimeToFormat.Minute.ToString()
        End If

        Return TimeH + TimeM
    End Function

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEntries.Skin))
    End Function

    Protected Sub dgrdEntries_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdEntries.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        ElseIf (e.CommandName = "reject") Then
            MoveRequestId = Convert.ToInt32(DirectCast(e.Item, GridDataItem).GetDataKeyValue("MoveRequestId").ToString())
            txtRejectedReason.Text = String.Empty
            mpeRejectPopupManual.Show()
        ElseIf (e.CommandName = "RowClick") Then
            MoveRequestId = Convert.ToInt32(DirectCast(dgrdEntries.SelectedItems(0), GridDataItem).GetDataKeyValue("MoveRequestId").ToString())
            FillControls()
            EnableControl(False)
            mvManualEntry.ActiveViewIndex = 1
        End If
    End Sub

    Protected Sub dgrdRejectedRequests_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdRejectedRequests.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter2.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar2_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon2() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdRejectedRequests.Skin))
    End Function
#End Region

End Class
