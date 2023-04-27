Imports SmartV.UTILITIES
Imports TA.Employees
Imports TA.Admin
Imports Telerik.Web.UI
Imports TA.Definitions
Imports TA.SelfServices
Imports System.IO
Imports System.Data
Imports TA.DailyTasks

Partial Class Requests_UserControls_HR_HR_LeaveApproval
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private objEmp_LeavesRequest As New Emp_LeavesRequest
    Private objLeavesTypes As LeavesTypes
    Private Lang As CtlCommon.Lang
    Private objEmp_Leaves As Emp_Leaves
    Dim objAPP_Settings As New APP_Settings
    Public MsgLang As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Shared SortExepression As String
    Shared dtCurrent As DataTable
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objEmployee As Employee
    Private objEmployee_Manager As Employee_Manager
    Private objRecalculateRequest As RecalculateRequest

    Private Enum RequestStatus
        Pending = 1
        ApprovedByDM = 2
        ApprovedByHR = 3
        RejectedByDM = 4
        RejectedByHR = 5
    End Enum

    Public Property LeaveID() As String
        Get
            Return ViewState("LeaveID")
        End Get
        Set(ByVal value As String)
            ViewState("LeaveID") = value
        End Set
    End Property

    Public Property FK_LeaveTypeId() As String
        Get
            Return ViewState("FK_LeaveTypeId")
        End Get
        Set(ByVal value As String)
            ViewState("FK_LeaveTypeId") = value
        End Set
    End Property

    Public Property LeaveApproval() As Integer
        Get
            Return ViewState("LeaveApproval")
        End Get
        Set(ByVal value As Integer)
            ViewState("LeaveApproval") = value
        End Set
    End Property

    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Public Property ManagerName() As String
        Get
            Return ViewState("ManagerName")
        End Get
        Set(ByVal value As String)
            ViewState("ManagerName") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
            MsgLang = "ar"
        Else
            Lang = CtlCommon.Lang.EN
            MsgLang = "en"
        End If
        If Not IsPostBack Then
            mvLeaveApproval.SetActiveView(viewDMApproval)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                MsgLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                MsgLang = "en"
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            lblLeaveRequests.Text = ResourceManager.GetString("HRLeaveApprove", CultureInfo)
            Page.Title = "Work Force Pro : :" + ResourceManager.GetString("HRLeaveApprove", CultureInfo)

            ShowHideControls()
            FillGridView()
        End If
    End Sub

    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0

        objRECALC_REQUEST = New RECALC_REQUEST
        Dim temp_str_date As String
        Dim err2 As Integer

        FK_LeaveTypeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("LeaveTypeId"))

        objAPP_Settings = New APP_Settings
        objLeavesTypes = New LeavesTypes

        objAPP_Settings = objAPP_Settings.GetByPK()

        If Not objAPP_Settings.LeaveApprovalfromLeave Then
            LeaveApproval = objAPP_Settings.LeaveApproval
        Else
            objLeavesTypes.LeaveId = FK_LeaveTypeId
            objLeavesTypes = objLeavesTypes.GetByPK()
            LeaveApproval = objLeavesTypes.LeaveApproval
        End If

        If (LeaveApproval = 2 Or LeaveApproval = 3) Then
            Dim FromDate As DateTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FromDate"))
            Dim ToDate As DateTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("ToDate"))
            Dim EmployeeId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_EmployeeId"))
            Dim LeaveId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("LeaveId"))
            FK_LeaveTypeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("LeaveTypeId"))
            Dim Requestdate As DateTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Requestdate"))
            Dim AttachedFile As String = DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("AttachedFile").ToString()
            Dim Remarks As String = String.Empty
            If (DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Remarks").ToString() <> "") Then
                Remarks = CStr(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Remarks").ToString())
            End If

            Dim EmpLeaveTotalBalance As Double = 0
            Dim OffAndHolidayDays As Integer = 0
            Dim ErrorMessage As String = String.Empty
            objEmp_Leaves = New Emp_Leaves
            objLeavesTypes = New LeavesTypes

            If (objEmp_Leaves.ValidateEmployeeLeave(objLeavesTypes, FromDate, ToDate, FK_LeaveTypeId, EmployeeId, _
                                                     ErrorMessage, OffAndHolidayDays, LeaveId, EmpLeaveTotalBalance) = False) Then
                If (ErrorMessage <> String.Empty) Then
                    CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                    Return
                End If

            Else
                objEmp_Leaves.LeaveId = 0
                objEmp_Leaves.AddLeaveAllProcess(EmployeeId, OffAndHolidayDays, FromDate, ToDate, FromDate, _
                ToDate, Requestdate, Remarks, FK_LeaveTypeId, EmpLeaveTotalBalance, AttachedFile, ErrorMessage, LeaveId, True)
            End If

            With objEmp_LeavesRequest
                .LeaveId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("LeaveId"))
                .GetByPK()
                .FK_StatusId = RequestStatus.ApprovedByHR
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .FK_HREmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                Err = .UpdateLeaveStatus()
                strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                If (File.Exists(Server.MapPath("~/LeaveRequestFiles//" + LeaveId.ToString() + AttachedFile))) Then
                    My.Computer.FileSystem.RenameFile(Server.MapPath("~/LeaveRequestFiles//" + LeaveId.ToString() + AttachedFile), _
                                                     (objEmp_Leaves.LeaveId.ToString() + AttachedFile).ToString())

                    If (File.Exists(Server.MapPath("~/LeaveRequestFiles//" + objEmp_Leaves.LeaveId.ToString() + AttachedFile))) Then
                        Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("~\LeaveRequestFiles\\" + objEmp_Leaves.LeaveId.ToString() + AttachedFile))
                        CopyFile.CopyTo(Server.MapPath("~/LeaveFiles//" + objEmp_Leaves.LeaveId.ToString() + AttachedFile))
                    End If

                End If
                If Err = 0 Then
                    FillGridView()

                    Dim dteFrom As DateTime = FromDate
                    Dim dteTo As DateTime = ToDate

                    If objAPP_Settings.ApprovalRecalMethod = 1 Then
                        While dteFrom <= dteTo
                            If Not dteFrom = Date.Now.AddDays(1).ToShortDateString() Then
                                temp_str_date = DateToString(dteFrom)
                                objRECALC_REQUEST = New RECALC_REQUEST()
                                objRECALC_REQUEST.EMP_NO = EmployeeId
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
                        If Not FromDate > Date.Today Then
                            objRecalculateRequest = New RecalculateRequest
                            With objRecalculateRequest
                                .Fk_EmployeeId = EmployeeId
                                .FromDate = FromDate
                                .ToDate = ToDate
                                .ImmediatelyStart = True
                                .RecalStatus = 0
                                .CREATED_BY = SessionVariables.LoginUser.ID
                                .Remarks = "Leave Request Approval - SYSTEM"
                                err2 = .Add
                            End With
                        End If
                    End If
                    CtlCommon.ShowMessage(Me.Page, strMessage, "success")
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
                End If
            End With
        ElseIf LeaveApproval = 4 Then
            objEmp_LeavesRequest = New Emp_LeavesRequest()
            With objEmp_LeavesRequest

                .LeaveId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("LeaveId"))
                .GetByPK()
                .FK_StatusId = RequestStatus.ApprovedByHR
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .FK_HREmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                Err = .UpdateLeaveStatus()

                strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
                If Err = 0 Then
                    FillGridView()
                    CtlCommon.ShowMessage(Me.Page, strMessage, "success")
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
                End If
            End With
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("LeaveWorkFlowValidation", CultureInfo), "info")
        End If
    End Sub

    Protected Sub lnkReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReject.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        objAPP_Settings = New APP_Settings
        With objEmp_LeavesRequest
            .LeaveId = LeaveID
            .GetByPK()
            .FK_StatusId = RequestStatus.RejectedByHR
            .RejectedReason = txtRejectedReason.Text.Trim(",")
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .FK_HREmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            Err = .UpdateLeaveStatus()

            strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

            If Err = 0 Then
                FillGridView()
                CtlCommon.ShowMessage(Me.Page, strMessage, "success")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If
        End With
    End Sub

    Protected Sub dgrdEmpLeaveRequest_OnItemCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles dgrdEmpLeaveRequest.ItemCommand
        If (e.CommandName = "reject") Then
            txtRejectedReason.Text = String.Empty
            LeaveID = e.CommandArgument
            mpeRejectPopupLeave.Show()
        ElseIf e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdEmpLeaveRequest_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdEmpLeaveRequest.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromDate").ToString())) And (Not item.GetDataKeyValue("FromDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("FromDate")
                'item("FromDate").Text = fromDate.ToShortDateString()
                item("FromDate").Text = Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy")
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ToDate").ToString())) And (Not item.GetDataKeyValue("ToDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("ToDate")
                'item("ToDate").Text = fromDate.ToShortDateString()
                item("ToDate").Text = Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy")
            End If

            Dim LeaveId As Integer = item.GetDataKeyValue("LeaveId")
            Dim AttachedFile As String = item.GetDataKeyValue("AttachedFile").ToString()

            If AttachedFile = "" Or AttachedFile = "&nbsp;" Then
                DirectCast(e.Item.FindControl("lnbView"), LinkButton).Visible = False
            Else
                DirectCast(e.Item.FindControl("lnbView"), LinkButton).Visible = True
            End If

            Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            scriptManager__1.RegisterPostBackControl(DirectCast(e.Item.FindControl("lnbView"), LinkButton))

            Dim scriptManager__2 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            scriptManager__2.RegisterPostBackControl(DirectCast(e.Item.FindControl("lnkLeaveForm"), LinkButton))

            Dim lnkLeaveForm As LinkButton = DirectCast(item.FindControl("lnkLeaveForm"), LinkButton)
            lnkLeaveForm.Visible = True

            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
                item("LeaveName").Text = DirectCast(item.FindControl("hdnLeaveTpeAr"), HiddenField).Value
                item("StatusName").Text = DirectCast(item.FindControl("hdnStatusNameAr"), HiddenField).Value
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("LeaveTypeId").ToString())) And (Not item.GetDataKeyValue("LeaveTypeId").ToString() = "")) Then
                Dim leaveTypeId As Integer = Convert.ToInt32(item.GetDataKeyValue("LeaveTypeId"))
                Dim objLeavesTypesGrid As New LeavesTypes()
                With objLeavesTypesGrid
                    .LeaveId = leaveTypeId
                    .GetByPK()
                    If .IsAnnual Then
                        DirectCast(item.FindControl("chbWithAdvancedSalary"), CheckBox).Visible = True

                        If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("HasAdvancedSalary").ToString())) And (Not item.GetDataKeyValue("HasAdvancedSalary").ToString() = "")) Then
                            DirectCast(item.FindControl("chbWithAdvancedSalary"), CheckBox).Checked = Convert.ToBoolean(item.GetDataKeyValue("HasAdvancedSalary"))
                        End If
                    Else
                        DirectCast(item.FindControl("chbWithAdvancedSalary"), CheckBox).Visible = False
                    End If
                End With
            End If

        End If
    End Sub

    Protected Sub dgrdEmpLeaveRequest_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmpLeaveRequest.NeedDataSource

        objEmp_LeavesRequest = New Emp_LeavesRequest()
        objEmp_LeavesRequest.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
        dtCurrent = objEmp_LeavesRequest.GetByHR()
        dgrdEmpLeaveRequest.DataSource = dtCurrent
        If Not (dtCurrent Is Nothing) Then
            Dim dv As New DataView(dtCurrent)
            dv.Sort = SortExepression
            dgrdEmpLeaveRequest.DataSource = dv
        End If

    End Sub

    Protected Sub lnkLeaveForm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        EmployeeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_EmployeeId"))
        LeaveID = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("LeaveId"))

        '---HR Information---'
        objEmployee = New Employee
        objEmployee.EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        objEmployee.GetByPK()
        'If Lang = CtlCommon.Lang.AR Then
        '    ManagerName = objEmployee.EmployeeArabicName
        'Else
        ManagerName = objEmployee.EmployeeName
        'End If
        '---HR Information---'

        Response.Redirect("~/Reports/LeaveForm_ReportViewer.aspx?EmployeeId=" & EmployeeId & "&LeaveId=" & LeaveID & "&ManagerName=" & ManagerName & "&Signature=" & 3) '---3= HR Signature'
        'Dim QueryString As String = "../Reports/LeaveForm_ReportViewer.aspx?EmployeeId=" & EmployeeId & "&LeaveId=" & LeaveID & "&ManagerName=" & ManagerName & "&Signature=" & 1
        'Dim newWin As String = (Convert.ToString("window.open('") & QueryString) + "', 'popup_window', 'width=600,height=200,left=400,top=100,resizable=yes,scrollbars=1');"
        'ClientScript.RegisterStartupScript(Me.GetType(), "pop", newWin, True)
    End Sub

    Protected Sub lnkDownloadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        objEmp_LeavesRequest = New Emp_LeavesRequest
        With objEmp_LeavesRequest
            .LeaveId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("LeaveId").ToString())
            .GetByPK()
            Dim FileName As String = .LeaveId.ToString() + .AttachedFile
            Dim FilePath As String = Server.MapPath("..\\LeaveRequestFiles\") & FileName
            CtlCommon.Open_Download_File(FileName, FilePath)

        End With
    End Sub

#End Region

#Region "Methods"

    Public Sub FillGridView()
        Try
            Dim dtLeaveRequet As New DataTable
            objEmp_LeavesRequest = New Emp_LeavesRequest()
            objEmp_LeavesRequest.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            objAPP_Settings = New APP_Settings()
            objAPP_Settings = objAPP_Settings.GetByPK()
            If Not objAPP_Settings.LeaveApprovalfromLeave = True Then
                LeaveApproval = objAPP_Settings.LeaveApproval
                objEmp_LeavesRequest.FK_StatusId = LeaveApproval
            End If

            'objEmp_LeavesRequest.FK_StatusId = RequestStatus.ApprovedByDM
            'dgrdEmpLeaveRequest.DataSource = objEmp_LeavesRequest.GetByDirectManager()
            dtLeaveRequet = objEmp_LeavesRequest.GetByHR()
            dgrdEmpLeaveRequest.DataSource = dtLeaveRequet
            dgrdEmpLeaveRequest.DataBind()

            If Not dtLeaveRequet Is Nothing Then
                If dtLeaveRequet.Rows.Count > 0 Then
                    If Not lblRequestNo.Text.Contains(":") Then
                        lblRequestNo.Text += " : " + (dtLeaveRequet.Rows.Count).ToString()
                    Else
                        Dim strArr() As String = lblRequestNo.Text.Split(":")
                        lblRequestNo.Text = strArr(0) + " : " + (dtLeaveRequet.Rows.Count).ToString()
                    End If
                Else
                    lblRequestNo.Visible = False
                    divrdbProceed.Visible = False
                    divbtnProceed.Visible = False
                End If
            End If
        Catch ex As Exception

        End Try
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

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmpLeaveRequest.Skin))
    End Function

#End Region

#Region "Proceed All"

    Protected Sub btnProceed_Click(sender As Object, e As System.EventArgs) Handles btnProceed.Click
        Dim checked As Boolean
        Dim ErrNo As Integer = 1
        Dim Msg As String = ""
        Dim Count As Integer = 0
        If rdbProceed.SelectedValue = "" Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Page, " الرجاء اختيار عملية معينة", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please select one Process", "info")
            End If
            Exit Sub
        End If
        If rdbProceed.SelectedValue = 2 And txtRejectAllReason.Text = "" Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Page, "الرجاء ادخال سبب الرفض", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please Insert Rejection Reason", "info")
            End If
            Exit Sub
        End If

           For Each item As GridDataItem In dgrdEmpLeaveRequest.Items
            If DirectCast(item.FindControl("chk"), CheckBox).Checked = True Then
                If rdbProceed.SelectedValue = 1 Then
                    AcceptAll(item, ErrNo, Msg)
                ElseIf rdbProceed.SelectedValue = 2 Then
                    RejectAll(item, ErrNo, Msg)
                End If
                Count = Count + 1
            End If
        Next

        If Count > 0 Then


            If ErrNo = 0 Then
                FillGridView()
                txtRejectAllReason.Text = String.Empty
                rdbProceed.SelectedIndex = "-1"
                divRejectAllReason.Visible = False
                CtlCommon.ShowMessage(Me.Page, Msg, "success")
            ElseIf ErrNo = 1 Then

            Else

                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")

            End If
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Page, " الرجاء اختيار طلب مغادرة واحد على الاقل", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please select one Permission Request at least", "info")
            End If

        End If
    End Sub

    Private Sub AcceptAll(ByVal item As GridDataItem, ByRef ErrNo As Integer, ByRef Msg As String)
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0

        objRECALC_REQUEST = New RECALC_REQUEST
        Dim temp_str_date As String
        Dim err2 As Integer

        FK_LeaveTypeId = CInt(item.GetDataKeyValue("LeaveTypeId"))

        objAPP_Settings = New APP_Settings
        objLeavesTypes = New LeavesTypes

        objAPP_Settings = objAPP_Settings.GetByPK()

        If Not objAPP_Settings.LeaveApprovalfromLeave Then
            LeaveApproval = objAPP_Settings.LeaveApproval
        Else
            objLeavesTypes.LeaveId = FK_LeaveTypeId
            objLeavesTypes = objLeavesTypes.GetByPK()
            LeaveApproval = objLeavesTypes.LeaveApproval
        End If

        If (LeaveApproval = 2 Or LeaveApproval = 3) Then
            Dim FromDate As DateTime = CDate(item.GetDataKeyValue("FromDate"))
            Dim ToDate As DateTime = CDate(item.GetDataKeyValue("ToDate"))
            Dim EmployeeId As Integer = CInt(item.GetDataKeyValue("FK_EmployeeId"))
            Dim LeaveId As Integer = CInt(item.GetDataKeyValue("LeaveId"))
            FK_LeaveTypeId = CInt(item.GetDataKeyValue("LeaveTypeId"))
            Dim Requestdate As DateTime = CDate(item.GetDataKeyValue("Requestdate"))
            Dim AttachedFile As String = item.GetDataKeyValue("AttachedFile").ToString()
            Dim Remarks As String = String.Empty
            If (item.GetDataKeyValue("Remarks").ToString() <> "") Then
                Remarks = CStr(item.GetDataKeyValue("Remarks").ToString())
            End If

            Dim EmpLeaveTotalBalance As Double = 0
            Dim OffAndHolidayDays As Integer = 0
            Dim ErrorMessage As String = String.Empty
            objEmp_Leaves = New Emp_Leaves
            objLeavesTypes = New LeavesTypes

            If (objEmp_Leaves.ValidateEmployeeLeave(objLeavesTypes, FromDate, ToDate, FK_LeaveTypeId, EmployeeId, _
                                                    ErrorMessage, OffAndHolidayDays, LeaveId, EmpLeaveTotalBalance) = False) Then
                If (ErrorMessage <> String.Empty) Then
                    CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                    Msg = strMessage
                    Return
                End If

            Else
                objEmp_Leaves.LeaveId = 0
                objEmp_Leaves.AddLeaveAllProcess(EmployeeId, OffAndHolidayDays, FromDate, ToDate, FromDate, _
                ToDate, Requestdate, Remarks, FK_LeaveTypeId, EmpLeaveTotalBalance, AttachedFile, ErrorMessage, Nothing, True)
            End If

            With objEmp_LeavesRequest
                .LeaveId = CInt(item.GetDataKeyValue("LeaveId"))
                .GetByPK()
                .FK_StatusId = RequestStatus.ApprovedByHR
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .FK_HREmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                Err = .UpdateLeaveStatus()
                strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                If (File.Exists(Server.MapPath("~/LeaveRequestFiles//" + LeaveId.ToString() + AttachedFile))) Then
                    My.Computer.FileSystem.RenameFile(Server.MapPath("~/LeaveRequestFiles//" + LeaveId.ToString() + AttachedFile), _
                                                     (objEmp_Leaves.LeaveId.ToString() + AttachedFile).ToString())

                    If (File.Exists(Server.MapPath("~/LeaveRequestFiles//" + objEmp_Leaves.LeaveId.ToString() + AttachedFile))) Then
                        Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("~\LeaveRequestFiles\\" + objEmp_Leaves.LeaveId.ToString() + AttachedFile))
                        CopyFile.CopyTo(Server.MapPath("~/LeaveFiles//" + objEmp_Leaves.LeaveId.ToString() + AttachedFile))
                    End If

                End If
                If Err = 0 Then
                   
                    Dim dteFrom As DateTime = FromDate
                    Dim dteTo As DateTime = ToDate

                    If objAPP_Settings.ApprovalRecalMethod = 1 Then
                        While dteFrom <= dteTo
                            If Not dteFrom = Date.Now.AddDays(1).ToShortDateString() Then
                                temp_str_date = DateToString(dteFrom)
                                objRECALC_REQUEST = New RECALC_REQUEST()
                                objRECALC_REQUEST.EMP_NO = EmployeeId
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
                        If Not FromDate > Date.Today Then
                            objRecalculateRequest = New RecalculateRequest
                            With objRecalculateRequest
                                .Fk_EmployeeId = EmployeeId
                                .FromDate = FromDate
                                .ToDate = ToDate
                                .ImmediatelyStart = True
                                .RecalStatus = 0
                                .CREATED_BY = SessionVariables.LoginUser.ID
                                .Remarks = "Leave Request Approval - SYSTEM"
                                err2 = .Add
                            End With
                        End If
                    End If
                End If
            End With
        ElseIf LeaveApproval = 4 Then
            objEmp_LeavesRequest = New Emp_LeavesRequest()
            With objEmp_LeavesRequest

                .LeaveId = CStr(item.GetDataKeyValue("LeaveId"))
                .GetByPK()
                .FK_StatusId = RequestStatus.ApprovedByHR
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .FK_HREmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                Err = .UpdateLeaveStatus()

                strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                'If Err = 0 Then
                '    FillGridView()
                '    CtlCommon.ShowMessage(Me.Page, strMessage)
                'Else
                '    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo))
                'End If
            End With
            'Else
            'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("LeaveWorkFlowValidation", CultureInfo))
        End If
        ErrNo = Err
        Msg = strMessage
        item = Nothing
    End Sub

    Private Sub RejectAll(ByVal item As GridDataItem, ByRef ErrNo As Integer, ByRef Msg As String)
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        objAPP_Settings = New APP_Settings
        With objEmp_LeavesRequest
            .LeaveId = CInt(item.GetDataKeyValue("LeaveId"))
            .GetByPK()
            .FK_StatusId = RequestStatus.RejectedByHR
            .RejectedReason = txtRejectAllReason.Text.Trim(",")
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .FK_HREmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            Err = .UpdateLeaveStatus()

            strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
            ErrNo = Err
            Msg = strMessage
            'If Err = 0 Then
            '    FillGridView()
            '    CtlCommon.ShowMessage(Me.Page, strMessage)
            'Else
            '    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo))
            'End If
        End With
    End Sub

    Protected Sub rdbProceed_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdbProceed.SelectedIndexChanged
        If rdbProceed.SelectedValue = 2 Then
            divRejectAllReason.Visible = True
        Else
            divRejectAllReason.Visible = False
        End If
    End Sub

    Private Sub ShowHideControls()
        objAPP_Settings = New APP_Settings

        With objAPP_Settings
            .GetByPK()
            If .HasMultiApproval Then
                divrdbProceed.Visible = True
                divbtnProceed.Visible = True
            Else
                divrdbProceed.Visible = False
                divbtnProceed.Visible = False
            End If
        End With
    End Sub
#End Region

End Class
