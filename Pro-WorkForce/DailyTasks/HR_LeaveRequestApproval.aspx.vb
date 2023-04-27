Imports Telerik.Web.UI
Imports TA.Security
Imports SmartV.UTILITIES
Imports TA.DailyTasks
Imports System.Data
Imports TA.Definitions
Imports TA.Employees
Imports System.IO
Imports TA.Admin

Partial Class DailyTasks_HR_LeaveRequestApproval
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objHR_LeaveRequest As HR_LeaveRequest
    Private objEmp_Leaves As Emp_Leaves
    Private objLeavesTypes As LeavesTypes
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Public strLang As String
    Public MsgLang As String
    Private objProjectCommon As New ProjectCommon
    Private objAPP_Settings As APP_Settings
    Private objRecalculateRequest As RecalculateRequest

#End Region

#Region "Properties"

    Public Property LeaveRequestID() As Integer
        Get
            Return ViewState("LeaveRequestID")
        End Get
        Set(ByVal value As Integer)
            ViewState("LeaveRequestID") = value
        End Set

    End Property

    Public Property LeaveID() As Integer
        Get
            Return ViewState("LeaveID")
        End Get
        Set(ByVal value As Integer)
            ViewState("LeaveID") = value
        End Set

    End Property

    Public Property FK_LeaveTypeId() As Integer
        Get
            Return ViewState("FK_LeaveTypeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_LeaveTypeId") = value
        End Set

    End Property
    Public Property EmpDt() As DataTable
        Get
            Return ViewState("EmpDt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("EmpDt") = value
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

    Public Property FileExtension() As String
        Get
            Return ViewState("FileExtension")
        End Get
        Set(ByVal value As String)
            ViewState("FileExtension") = value
        End Set
    End Property

    Public Property FileName() As String
        Get
            Return ViewState("FileName")
        End Get
        Set(ByVal value As String)
            ViewState("FileName") = value
        End Set
    End Property

    Public Property UserId As Integer
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
            mvLeaveApproval.SetActiveView(vLeaves)
            UserId = SessionVariables.LoginUser.ID
            FillEmpLeaveGrid()
            FillRejectedGrid()
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("HRLeaveRequestApproval", CultureInfo)
        End If
    End Sub

    Protected Sub grdEmpLeaves_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles grdEmpLeaves.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("RequestDate").ToString())) And (Not item.GetDataKeyValue("RequestDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("RequestDate").ToString()
                item("RequestDate").Text = fromDate.ToShortDateString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromDate").ToString())) And (Not item.GetDataKeyValue("FromDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("FromDate").ToString
                item("FromDate").Text = fromDate.ToShortDateString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ToDate").ToString())) And (Not item.GetDataKeyValue("ToDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("ToDate").ToString()
                item("ToDate").Text = fromDate.ToShortDateString()
            End If

            If Lang = CtlCommon.Lang.AR Then
                item("LeaveName").Text = DirectCast(item.FindControl("hdnLeaveArabicType"), HiddenField).Value
            End If

            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
            End If

        End If
    End Sub

    Protected Sub dgrdRejected_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdRejected.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("RequestDate").ToString())) And (Not item.GetDataKeyValue("RequestDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("RequestDate").ToString()
                item("RequestDate").Text = fromDate.ToShortDateString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromDate").ToString())) And (Not item.GetDataKeyValue("FromDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("FromDate").ToString()
                item("FromDate").Text = fromDate.ToShortDateString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ToDate").ToString())) And (Not item.GetDataKeyValue("ToDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("ToDate").ToString()
                item("ToDate").Text = fromDate.ToShortDateString()
            End If

            If Lang = CtlCommon.Lang.AR Then
                item("LeaveName").Text = DirectCast(item.FindControl("hdnLeaveArabicType"), HiddenField).Value
            End If

            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
            End If

        End If
    End Sub

    Protected Sub lnkReject_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        LeaveRequestID = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("LeaveRequestID").ToString())
        'RejectLeaveRequest()

    End Sub

    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        LeaveRequestID = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("LeaveRequestID").ToString())
        AcceptLeaveRequest()
    End Sub

    Protected Sub btnAccept_Click(sender As Object, e As System.EventArgs) Handles btnAccept.Click
        AcceptLeaveRequest()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        mvLeaveApproval.ActiveViewIndex = 0
        FillEmpLeaveGrid()
    End Sub

    Protected Sub btnReject_Click(sender As Object, e As System.EventArgs) Handles btnReject.Click
        'RejectLeaveRequest()
    End Sub

    Protected Sub grdEmpLeaves_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdEmpLeaves.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        ElseIf (e.CommandName = "reject") Then
            LeaveRequestID = Convert.ToInt32(DirectCast(e.Item, GridDataItem).GetDataKeyValue("LeaveRequestID").ToString())
            'RejectLeaveRequest()
            txtRejectedReason.Text = String.Empty
            mpeRejectPopupLeave.Show()
        ElseIf (e.CommandName = "RowClick") Then
            LeaveRequestID = Convert.ToInt32(DirectCast(grdEmpLeaves.SelectedItems(0), GridDataItem).GetDataKeyValue("LeaveRequestID").ToString())
            FillControls()
            EnableControl(False)
            mvLeaveApproval.ActiveViewIndex = 1
        End If

    End Sub

    Protected Sub dgrdRejected_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdRejected.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter2.FireApplyCommand()
        End If

    End Sub

    Protected Sub grdEmpLeaves_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdEmpLeaves.NeedDataSource
        objHR_LeaveRequest = New HR_LeaveRequest
        With objHR_LeaveRequest
            .FromDate = DateSerial(Today.Year, Today.Month, 1)
            .ToDate = DateSerial(Today.Year, Today.Month + 1, 0)
            EmpDt = .GetLeavesByLeaveStatus()
            grdEmpLeaves.DataSource = EmpDt
        End With
    End Sub

    Protected Sub dgrdRejected_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdRejected.NeedDataSource
        objHR_LeaveRequest = New HR_LeaveRequest
        With objHR_LeaveRequest
            .FromDate = DateSerial(Today.Year, Today.Month, 1)
            .ToDate = DateSerial(Today.Year, Today.Month + 1, 0)
            EmpDt = .GetAll_RejectedLeaves()
            dgrdRejected.DataSource = EmpDt
        End With
    End Sub

    Protected Sub btnRejectPOP_Click(sender As Object, e As System.EventArgs) Handles btnRejectPOP.Click
        RejectLeaveRequest()
    End Sub

#End Region

#Region "Methods"

    Sub FillEmpLeaveGrid()
        objHR_LeaveRequest = New HR_LeaveRequest
        With objHR_LeaveRequest
            '.FromDate = DateSerial(Today.Year, Today.Month, 1)
            '.ToDate = DateSerial(Today.Year, Today.Month + 1, 0)
            .UserId = SessionVariables.LoginUser.ID
            EmpDt = .GetLeavesByLeaveStatus()

            grdEmpLeaves.DataSource = EmpDt
            grdEmpLeaves.DataBind()
        End With
    End Sub

    Sub FillRejectedGrid()
        objHR_LeaveRequest = New HR_LeaveRequest
        With objHR_LeaveRequest
            '.FromDate = DateSerial(Today.Year, Today.Month, 1)
            '.ToDate = DateSerial(Today.Year, Today.Month + 1, 0)
            '.UserId = UserId
            EmpDt = .GetAll_RejectedLeaves()

            dgrdRejected.DataSource = EmpDt
            dgrdRejected.DataBind()
        End With
    End Sub

    Private Sub AcceptLeaveRequest()
        Dim EmpLeaveTotalBalance As Double = 0
        Dim OffAndHolidayDays As Integer = 0
        Dim ErrorMessage As String = String.Empty
        objEmp_Leaves = New Emp_Leaves
        objLeavesTypes = New LeavesTypes
        objRECALC_REQUEST = New RECALC_REQUEST
        Dim temp_str_date As String
        Dim err2 As Integer
        objHR_LeaveRequest = New HR_LeaveRequest
        objHR_LeaveRequest.LeaveRequestId = LeaveRequestID
        objHR_LeaveRequest.GetByPK()

        If (objEmp_Leaves.ValidateEmployeeLeave(objLeavesTypes, objHR_LeaveRequest.FromDate, objHR_LeaveRequest.ToDate, objHR_LeaveRequest.FK_LeaveTypeId, objHR_LeaveRequest.FK_EmployeeId, _
                                                ErrorMessage, OffAndHolidayDays, LeaveID, EmpLeaveTotalBalance) = False) Then
            If (ErrorMessage <> String.Empty) Then
                CtlCommon.ShowMessage(Me.Page, ErrorMessage)
            End If

        Else

            objEmp_Leaves.LeaveId = LeaveID
            Dim fileUploadExtension As String = Path.GetExtension(Server.MapPath("..\LeaveFiles\\" + objHR_LeaveRequest.LeaveRequestId.ToString() + FileExtension))

            If String.IsNullOrEmpty(fileUploadExtension) Then
                fileUploadExtension = FileExtension
            End If

            objEmp_Leaves.AddLeaveAllProcess(objHR_LeaveRequest.FK_EmployeeId, OffAndHolidayDays, objHR_LeaveRequest.FromDate, objHR_LeaveRequest.ToDate, objHR_LeaveRequest.FromDate, _
            objHR_LeaveRequest.ToDate, objHR_LeaveRequest.RequestDate, objHR_LeaveRequest.Remarks, objHR_LeaveRequest.FK_LeaveTypeId, EmpLeaveTotalBalance, fileUploadExtension, ErrorMessage, objHR_LeaveRequest.LeaveRequestId, False)

            CtlCommon.ShowMessage(Me.Page, ErrorMessage, "success")

            objHR_LeaveRequest.LeaveRequestId = LeaveRequestID
            objHR_LeaveRequest.IsRejected = True
            objHR_LeaveRequest.RejectionReason = txtRejectedReason.Text
            objHR_LeaveRequest.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            err2 = objHR_LeaveRequest.Update_Leave_RequestStatus

            Dim fPath As String = String.Empty
            fPath = Server.MapPath("..\LeaveFiles\\" + objEmp_Leaves.LeaveId.ToString() + FileExtension)
            Dim fPathExist As String = String.Empty
            fPathExist = Server.MapPath("..\LeaveFiles\\" + FileName)

            If File.Exists(fPathExist) Then
                File.Delete(fPath)
                Rename(fPathExist, fPath)

            End If

            Dim dteFrom As DateTime = objHR_LeaveRequest.FromDate
            Dim dteTo As DateTime = objHR_LeaveRequest.ToDate

            objAPP_Settings = New APP_Settings()
            objAPP_Settings = objAPP_Settings.GetByPK()

            If objAPP_Settings.ApprovalRecalMethod = 1 Then
                While dteFrom <= dteTo
                    If Not dteFrom = Date.Now.AddDays(1).ToShortDateString() Then
                        temp_str_date = DateToString(dteFrom)
                        objRECALC_REQUEST = New RECALC_REQUEST()
                        objRECALC_REQUEST.EMP_NO = objHR_LeaveRequest.FK_EmployeeId
                        objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)
                        err2 = objRECALC_REQUEST.RECALCULATE()
                        If Not err2 = 0 Then
                            Exit While
                        End If
                        dteFrom = dteFrom.AddDays(1)
                    Else
                        Exit While
                    End If
                End While
            Else
                If Not dteFrom > Date.Today Then
                    objRecalculateRequest = New RecalculateRequest
                    With objRecalculateRequest
                        .Fk_EmployeeId = objHR_LeaveRequest.FK_EmployeeId
                        .FromDate = dteFrom
                        .ToDate = dteTo
                        .ImmediatelyStart = True
                        .RecalStatus = 0
                        .CREATED_BY = SessionVariables.LoginUser.ID
                        .Remarks = "HR Leave Approval - SYSTEM"
                        err2 = .Add
                    End With
                End If

            End If

            mvLeaveApproval.ActiveViewIndex = 0
            FillEmpLeaveGrid()
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

    Private Sub RejectLeaveRequest()
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        objHR_LeaveRequest = New HR_LeaveRequest
        objHR_LeaveRequest.LeaveRequestId = LeaveRequestID
        objHR_LeaveRequest.IsRejected = True
        objHR_LeaveRequest.RejectionReason = txtRejectedReason.Text
        objHR_LeaveRequest.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
        Err = objHR_LeaveRequest.Update_Leave_RequestStatus
        strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
        If Err = 0 Then
            mvLeaveApproval.ActiveViewIndex = 0
            FillEmpLeaveGrid()
            FillRejectedGrid()
            CtlCommon.ShowMessage(Me.Page, strMessage, "success")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
        End If
    End Sub

    Private Sub EnableControl(ByVal status As Boolean)
        ddlLeaveType.Enabled = status
        dtpFromDate.Enabled = status
        dtpToDate.Enabled = status
        dtpRequestDate.Enabled = status
        txtRemarks.Enabled = status
        fuAttachFile.Enabled = status
        EmployeeFilterUC.EnabledDisbaledControls(status)
    End Sub

    Sub FillDropDown()
        objProjectCommon = New ProjectCommon()
        Dim dt As DataTable = Nothing
        dt = Nothing
        objLeavesTypes = New LeavesTypes()
        dt = objLeavesTypes.GetAllForDDL
        ProjectCommon.FillRadComboBox(ddlLeaveType, dt, "LeaveName", _
                                     "LeaveArabicName", "LeaveId") '---LeaveId In Types = FK_LeaveTypeId In Emp_Leaves---'

    End Sub

    Private Sub FillControls()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objHR_LeaveRequest = New HR_LeaveRequest
        FillDropDown()
        With objHR_LeaveRequest
            .LeaveRequestId = LeaveRequestID
            .GetByPK()
            ddlLeaveType.SelectedValue = .FK_LeaveTypeId
            ValidateLeaveSufficient()
            dtpFromDate.SelectedDate = .FromDate
            dtpToDate.SelectedDate = .ToDate
            dtpRequestDate.SelectedDate = .RequestDate
            txtRemarks.Text = .Remarks
            FileExtension = .AttachedFile
            EmployeeFilterUC.EmployeeId = .FK_EmployeeId
            EmployeeFilterUC.IsEntityClick = "True"
            EmployeeFilterUC.GetEmployeeInfo(.FK_EmployeeId)

            fuAttachFile.Visible = True
            lnbLeaveFile.Visible = True
            lnbRemove.Visible = True
            Dim fPath As String = "..\HR_LeaveRequestFiles\" + LeaveRequestID.ToString() + .AttachedFile
            Dim fPathExist As String = String.Empty
            fPathExist = Server.MapPath("..\HR_LeaveRequestFiles\\" + LeaveRequestID.ToString() + .AttachedFile)
            If String.IsNullOrEmpty(.AttachedFile) Then
                lnbLeaveFile.Visible = False
                lnbRemove.Visible = False
                lblNoAttachedFile.Visible = True
            ElseIf File.Exists(fPathExist) Then
                lnbLeaveFile.Visible = True
                lnbRemove.Visible = True
                lblNoAttachedFile.Visible = False
            Else
                lnbLeaveFile.Visible = False
                lnbRemove.Visible = False
                lblNoAttachedFile.Visible = True
            End If
        End With
    End Sub

    Private Function ValidateLeaveSufficient() As Boolean
        objLeavesTypes = New LeavesTypes
        With objLeavesTypes
            .LeaveId = ddlLeaveType.SelectedValue
            .GetByPK()

            If .AllowIfBalanceOver Then
                hdnLeaveSufficient.Value = "True"
            Else
                hdnLeaveSufficient.Value = "False"
            End If
        End With

        Return True

    End Function

    Protected Sub lnkDownloadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        objHR_LeaveRequest = New HR_LeaveRequest
        FillDropDown()
        With objHR_LeaveRequest
            .LeaveRequestId = LeaveRequestID
            .GetByPK()

            Dim FileName As String = LeaveRequestID.ToString() + .AttachedFile
            Dim FilePath As String = Server.MapPath("..\\HR_LeaveRequestFiles\") & FileName
            CtlCommon.Open_Download_File(FileName, FilePath)

        End With
    End Sub

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", grdEmpLeaves.Skin))
    End Function

    Protected Sub RadToolBar2_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon2() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdRejected.Skin))
    End Function

#End Region


End Class
