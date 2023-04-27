Imports Telerik.Web.UI
Imports TA.Security
Imports SmartV.UTILITIES
Imports System.Data
Imports TA.DailyTasks
Imports TA.Employees
Imports TA.Admin

Partial Class DailyTasks_HR_ManualEntryApproval
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmpMove As Emp_Move
    Private objHR_Emp_Move As HR_Emp_Move
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objREADER_KEYS As READER_KEYS
    Private objEmployee As Employee
    Private objTA_Reason As TA_Reason
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Public MsgLang As String
    Public strLang As String

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
            PageHeader1.HeaderText = ResourceManager.GetString("HRManualEntryApproval", CultureInfo)

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

            Dim MoveRequestId As Integer = item.GetDataKeyValue("MoveRequestId")
            Dim AttachedFile As String = IIf(Not IsDBNull(item.GetDataKeyValue("AttachedFile")), item.GetDataKeyValue("AttachedFile"), "")
            If AttachedFile = "" Then
                'DirectCast(e.Item.FindControl("lnbView"), HtmlAnchor).Visible = False
                DirectCast(e.Item.FindControl("lnbView"), LinkButton).Visible = False
            Else
                'DirectCast(e.Item.FindControl("lnbView"), HtmlAnchor).HRef = "..\UpdateTransactionsRequestFiles\" + MoveRequestId.ToString() + AttachedFile
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
        End If
    End Sub

    Protected Sub dgrdRejectedRequests_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdRejectedRequests.NeedDataSource
        dgrdRejectedRequests.DataSource = Nothing
        objHR_Emp_Move = New HR_Emp_Move
        objHR_Emp_Move.UserId = UserId
        dtCurrent = objHR_Emp_Move.GetAll_Rejected()
        dgrdRejectedRequests.DataSource = dtCurrent
    End Sub

    Protected Sub dgrdEntries_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEntries.NeedDataSource
        dgrdEntries.DataSource = Nothing
        objHR_Emp_Move = New HR_Emp_Move
        objHR_Emp_Move.UserId = UserId
        dtCurrent = objHR_Emp_Move.GetAll_ByStatus()
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
            Dim FilePath As String = Server.MapPath("..\\UpdateTransactionsRequestFiles\") & filename
            CtlCommon.Open_Download_File(FileName, FilePath)
        End If
    End Sub
#End Region

#Region "Methods"

    Public Sub FillGrid()
        dgrdEntries.DataSource = Nothing
        objHR_Emp_Move = New HR_Emp_Move
        objHR_Emp_Move.UserId = UserId
        dtCurrent = objHR_Emp_Move.GetAll_ByStatus()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdEntries.DataSource = dv
        dgrdEntries.DataBind()

    End Sub

    Private Sub FillGridRejected()
        dgrdRejectedRequests.DataSource = Nothing
        objHR_Emp_Move = New HR_Emp_Move
        objHR_Emp_Move.UserId = UserId
        dtCurrent = objHR_Emp_Move.GetAll_Rejected()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdRejectedRequests.DataSource = dv
        dgrdRejectedRequests.DataBind()
    End Sub

    Private Sub AcceptManualEntryRequest()
        Dim temp_date As Date
        Dim temp_str_date As String
        Dim formats As String() = New String() {"HHmm"}
        Dim temp_time As DateTime
        Dim temp_str_time As String
        Dim err As Integer
        objHR_Emp_Move = New HR_Emp_Move
        With objHR_Emp_Move
            .MoveRequestId = MoveRequestId
            .GetByPK()
            FK_EmployeeId = .FK_EmployeeId
        End With
        objEmpMove = New Emp_Move
        objRECALC_REQUEST = New RECALC_REQUEST
        objREADER_KEYS = New READER_KEYS
        objRECALC_REQUEST.EMP_NO = FK_EmployeeId

        objEmployee = New Employee
        FK_EmployeeId = FK_EmployeeId
        If objEmployee.GetEmpNo(FK_EmployeeId) = False Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NumIsntForThisLevel", CultureInfo), "info")
            Return
        End If
        objEmpMove.FK_EmployeeId = FK_EmployeeId
        objEmpMove.Remarks = objHR_Emp_Move.Remarks
        objEmpMove.Reader = String.Empty
        objEmpMove.Status = 1
        objEmpMove.IsManual = True
        objEmpMove.CREATED_BY = SessionVariables.LoginUser.ID
        objEmpMove.Remarks = objHR_Emp_Move.Remarks
        objREADER_KEYS.READER_KEY = objHR_Emp_Move.FK_ReasonId
        objREADER_KEYS.GetByPK()

        objEmpMove.FK_ReasonId = objREADER_KEYS.CHANGE_TO
        objEmpMove.Type = objREADER_KEYS.Type
        If objHR_Emp_Move.MoveDate <> Nothing Then
            objEmpMove.MoveDate = objHR_Emp_Move.MoveDate
            temp_date = objHR_Emp_Move.MoveDate
            temp_str_date = DateToString(temp_date)
            objEmpMove.M_DATE_NUM = temp_str_date
            objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)
        Else
            objEmpMove.MoveDate = Nothing
            objEmpMove.M_DATE_NUM = Nothing
        End If
        'DateTime.TryParseExact(objHR_Emp_Move.MoveTime, formats, System.System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, temp_time)
        temp_time = objHR_Emp_Move.MoveTime
        objEmpMove.MoveTime = temp_time
        temp_str_time = temp_time.Minute + temp_time.Hour * 60
        objEmpMove.M_TIME_NUM = temp_str_time

        If tbl_id = 0 Then
            err = objEmpMove.Add()
        Else
            objEmpMove.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            objEmpMove.MoveId = tbl_id
            err = objEmpMove.Update()
        End If
        If err = 0 Then
            Dim err2 As Integer
            Dim count As Integer
            While count < 5
                err2 = objRECALC_REQUEST.RECALCULATE()
                If err2 = 0 Then
                    Exit While
                End If
                count += 1
            End While
        End If

        If err = 0 Then
            objHR_Emp_Move = New HR_Emp_Move
            With objHR_Emp_Move
                .MoveRequestId = MoveRequestId
                .IsRejected = False
                .RejectionReason = Me.txtRejectedReason.Text
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .Update_Request_Status()
            End With
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            'If Lang = CtlCommon.Lang.AR Then
            '    ScriptManager.RegisterStartupScript(Me, Page.GetType, "Redirect", "ShowMessageAndRedirect('تم الحفظ بنجاح','../DailyTasks/HR_ManualEntryApproval.aspx');", True)
            'Else
            '    ScriptManager.RegisterStartupScript(Me, Page.GetType, "Redirect", "ShowMessageAndRedirect('Saved Successfully','../DailyTasks/HR_ManualEntryApproval.aspx');", True)
            'End If
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
        objHR_Emp_Move = New HR_Emp_Move
        Dim Err As Integer = 0
        Dim strMessage As String = String.Empty
        With objHR_Emp_Move
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
        objHR_Emp_Move = New HR_Emp_Move
        objHR_Emp_Move.MoveRequestId = tbl_id
        objHR_Emp_Move.GetByPK()

        EmployeeFilterUC.IsEntityClick = "True"
        EmployeeFilterUC.GetEmployeeInfo(objHR_Emp_Move.FK_EmployeeId)

        txtremarks.Text = objHR_Emp_Move.Remarks

        If objHR_Emp_Move.MoveDate > RadDatePicker1.MinDate Then
            RadDatePicker1.SelectedDate = objHR_Emp_Move.MoveDate
        Else
            RadDatePicker1.Clear()
        End If
        rmtToTime2.Text = SetTimeFormat(objHR_Emp_Move.MoveTime)
        ddlReason.SelectedValue = objHR_Emp_Move.FK_ReasonId

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
