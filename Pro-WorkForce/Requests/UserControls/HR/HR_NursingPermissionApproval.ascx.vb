Imports SmartV.UTILITIES
Imports TA.Employees
Imports TA.Admin
Imports Telerik.Web.UI
Imports TA.Definitions
Imports TA.SelfServices
Imports System.IO
Imports System.Data
Imports TA.DailyTasks

Partial Class Requests_UserControls_HR_HR_NursingPermissionApproval
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private objEmp_Nursing_Permission As New Emp_Nursing_Permission
    Private objPermissionsTypes As PermissionsTypes
    Private Lang As CtlCommon.Lang
    Private objEmp_Permissions As Emp_Permissions
    Dim objAPP_Settings As New APP_Settings
    Public MsgLang As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Shared SortExepression As String
    Shared dtCurrent As DataTable
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objRecalculateRequest As RecalculateRequest

    Private Enum RequestStatus
        Pending = 1
        ApprovedByDM = 2
        ApprovedByHR = 3
        RejectedByDM = 4
        RejectedByHR = 5
    End Enum

#End Region

#Region "Properties"

    Public Property PermissionId() As String
        Get
            Return ViewState("PermissionId")
        End Get
        Set(ByVal value As String)
            ViewState("PermissionId") = value
        End Set
    End Property

    Public Property NursingPermissionApproval() As Integer
        Get
            Return ViewState("NursingPermissionApproval")
        End Get
        Set(ByVal value As Integer)
            ViewState("NursingPermissionApproval") = value
        End Set
    End Property

    Public Property strtimeelapsed() As String
        Get
            Return ViewState("strtimeelapsed")
        End Get
        Set(ByVal value As String)
            ViewState("strtimeelapsed") = value
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
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            mvPermissionApproval.SetActiveView(viewDMApproval)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                MsgLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                MsgLang = "en"
            End If
            lblNursingPermissionRequests.Text = ResourceManager.GetString("HRNurPermitHeader", CultureInfo)
            Page.Title = "Work Force Pro : :" + ResourceManager.GetString("HRNurPermitHeader", CultureInfo)

            ShowHideControls()
            FillGridView()
        End If
    End Sub

    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0

        objRECALC_REQUEST = New RECALC_REQUEST
        Dim temp_date As Date
        Dim temp_str_date As String
        Dim err2 As Integer

        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            If (.NursingPermissionApproval = 2 Or .NursingPermissionApproval = 3) Then
                Dim FromDate As DateTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermDate"))
                Dim ToDate As DateTime
                If ((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermEndDate").ToString()) <> "") Then
                    ToDate = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermEndDate"))
                Else
                    ToDate = Nothing
                End If

                Dim IsForPeriod As Boolean = CBool(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("IsForPeriod"))
                Dim EmployeeId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_EmployeeId"))
                Dim PermissionId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionRequestId"))
                'Dim PermTypeId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("PermTypeId").Text)
                Dim PermDate As DateTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermDate"))
                Dim IsFullyDay As Boolean = CBool(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("IsFullDay"))
                Dim AttachedFile As String = DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("AttachedFile").ToString()
                Dim Remarks As String = String.Empty
                Dim AllowedTime As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("AllowedTime"))
                Dim IsFlexible As Boolean
                If ((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("IsFlexible").ToString()) <> "") Then
                    If (DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("IsFlexible").ToString()) = "(No)" Then
                        IsFlexible = False
                    ElseIf (DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("IsFlexible").ToString()) = "(Yes)" + "(" + strtimeelapsed + ")" Then
                        IsFlexible = True
                    End If
                    'IsFlexible = CBool(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("IsFlexible").Text)
                Else
                    IsFlexible = Nothing
                End If
                Dim FlexibilePermissionDuration As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FlexibilePermissionDuration"))
                If (DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Remark").ToString() <> "") Then
                    Remarks = CStr(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Remark").ToString())
                End If


                Dim EmpLeaveTotalBalance As Double = 0
                Dim OffAndHolidayDays As Integer = 0
                Dim PermissionDaysCount As Double = 0
                Dim ErrorMessage As String = String.Empty
                objEmp_Permissions = New Emp_Permissions
                objPermissionsTypes = New PermissionsTypes



                'If (objEmp_Permissions.ValidateEmployeePermission(EmployeeId, PermissionId, New Data.DataTable, Not IsForPeriod, FromDate, ToDate, Nothing, _
                '                                                  Nothing, PermDate, objPermissionsTypes, PermTypeId, IsFullyDay, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
                '    If (ErrorMessage <> String.Empty) Then
                '        CtlCommon.ShowMessage(Me.Page, ErrorMessage)
                '        Return
                '    End If

                'Else
                objEmp_Permissions.AddPermAllProcess(EmployeeId, -1, False, IsFlexible, False, Remarks, FromDate, _
                                                     ToDate, Not IsForPeriod, Nothing, Nothing, PermDate, PermissionDaysCount, OffAndHolidayDays, Nothing, _
                                                     EmpLeaveTotalBalance, PermissionId, 2, FlexibilePermissionDuration, AttachedFile, IsFullyDay, AllowedTime, ErrorMessage)
                'End If

                With objEmp_Nursing_Permission
                    .FK_StatusId = RequestStatus.ApprovedByHR
                    .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    .PermissionRequestId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionRequestId"))
                    Err = .UpdatePermissionStatus()
                    strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                    If (File.Exists(Server.MapPath("~\NursingPermissionRequestFiles\\" + PermissionId.ToString() + AttachedFile))) Then

                        Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("~\NursingPermissionRequestFiles\\" + PermissionId.ToString() + AttachedFile))
                        CopyFile.CopyTo(Server.MapPath("~\PermissionFiles\\" + PermissionId.ToString() + AttachedFile))

                        Rename(Server.MapPath("~\PermissionFiles\\" + PermissionId.ToString() + AttachedFile), _
                                                Server.MapPath("~\PermissionFiles\\" + objEmp_Permissions.PermissionId.ToString() + AttachedFile))
                    End If

                    If Err = 0 Then

                        FillGridView()

                        Dim dteFrom As DateTime = FromDate
                        Dim dteTo As DateTime = ToDate
                        If objAPP_Settings.ApprovalRecalMethod = 1 Then
                            If Not IsForPeriod Then
                                temp_date = PermDate
                                temp_str_date = DateToString(temp_date)
                                objRECALC_REQUEST.EMP_NO = EmployeeId
                                objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)

                                If Not temp_date = Date.Now.AddDays(1).ToShortDateString() Then
                                    Dim count As Integer
                                    While count < 5
                                        err2 = objRECALC_REQUEST.RECALCULATE()
                                        If err2 = 0 Then
                                            Exit While
                                        End If
                                        count += 1
                                    End While
                                End If
                            Else
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
                            End If
                        Else
                            If Not dteFrom > Date.Today Then
                                objRecalculateRequest = New RecalculateRequest
                                With objRecalculateRequest
                                    .Fk_EmployeeId = EmployeeId
                                    .FromDate = FromDate
                                    If IsForPeriod = True Then
                                        .ToDate = ToDate
                                    Else
                                        .ToDate = FromDate
                                    End If
                                    .ImmediatelyStart = True
                                    .RecalStatus = 0
                                    .CREATED_BY = SessionVariables.LoginUser.ID
                                    .Remarks = "Nursing Permission Request Approval - SYSTEM"
                                    err2 = .Add
                                End With
                            End If
                        End If

                        CtlCommon.ShowMessage(Me.Page, strMessage, "success")

                    Else
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")

                    End If
                End With
            ElseIf (.NursingPermissionApproval = 3 Or .NursingPermissionApproval = 4) Then
                With objEmp_Nursing_Permission
                    .FK_StatusId = RequestStatus.ApprovedByHR
                    .PermissionRequestId = CStr(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionRequestId"))
                    .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    Err = .UpdatePermissionStatus()

                    strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                    If Err = 0 Then
                        FillGridView()
                        CtlCommon.ShowMessage(Me.Page, strMessage, "success")
                    Else
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
                    End If
                End With
            End If
        End With
    End Sub

    Protected Sub lnkReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReject.Click
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        objAPP_Settings = New APP_Settings
        With objEmp_Nursing_Permission

            .PermissionRequestId = PermissionId
            .GetByPK()
            .FK_StatusId = RequestStatus.RejectedByHR
            .RejectionReason = txtRejectedReason.Text.Trim(",")
            .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            Err = .UpdatePermissionStatus()

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
            PermissionId = e.CommandArgument
            mpeRejectPopupNursing.Show()
        ElseIf e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdEmpLeaveRequest_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdEmpLeaveRequest.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item
            Dim PermDateMonth As Integer = 0

            If Lang = CtlCommon.Lang.AR Then
                item("StatusName").Text = DirectCast(item.FindControl("hdnStatusNameAr"), HiddenField).Value
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("PermDate").ToString())) And (Not item.GetDataKeyValue("PermDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("PermDate")
                'item("PermDate").Text = fromDate.ToShortDateString()
                item("PermDate").Text = Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy")
                PermDateMonth = fromDate.Month
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("PermEndDate").ToString())) And (Not item.GetDataKeyValue("PermEndDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("PermEndDate")
                'item("PermEndDate").Text = fromDate.ToShortDateString()
                item("PermEndDate").Text = Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy")
            End If

            Dim PermissionId As Integer = item.GetDataKeyValue("PermissionRequestId")
            Dim AttachedFile As String = item.GetDataKeyValue("AttachedFile").ToString()

            If AttachedFile = "" Or AttachedFile = "&nbsp;" Then
                DirectCast(e.Item.FindControl("lnbView"), LinkButton).Visible = False
            Else
                DirectCast(e.Item.FindControl("lnbView"), LinkButton).Visible = True
            End If

            Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            scriptManager__1.RegisterPostBackControl(DirectCast(e.Item.FindControl("lnbView"), LinkButton))

            Dim PermEndDate As DateTime
            Dim PermDate As DateTime = Convert.ToDateTime(item.GetDataKeyValue("PermDate"))
            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("PermEndDate").ToString())) And (Not item.GetDataKeyValue("PermEndDate").ToString() = "")) Then
                PermEndDate = Convert.ToDateTime(item.GetDataKeyValue("PermEndDate"))
            Else
                PermEndDate = Nothing
            End If

            Dim IsForPeriod As Boolean = CBool(item.GetDataKeyValue("IsForPeriod"))

            Dim empId As Integer = Convert.ToInt32(item.GetDataKeyValue("FK_EmployeeId"))

            objEmp_Permissions = New Emp_Permissions()

            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
                item("StatusName").Text = DirectCast(item.FindControl("hdnStatusNameAr"), HiddenField).Value
            End If

            Dim strIsFlexible As String = item.GetDataKeyValue("IsFlexible").ToString()
            Dim strFlexibleDuration As String = item.GetDataKeyValue("FlexibilePermissionDuration").ToString()

            If strIsFlexible = "True" Then
                '    item("FromTime").Text = String.Empty
                '    item("ToTime").Text = String.Empty
                '    If Not String.IsNullOrEmpty(strFlexibleDuration) Then
                Dim dcmFlexibleDuration As Decimal = Convert.ToDecimal(strFlexibleDuration)
                Dim hours As Integer = dcmFlexibleDuration \ 60
                Dim minutes As Integer = dcmFlexibleDuration - (hours * 60)

                Dim strHours As String = "0"
                Dim strMinutes As String = "0"

                If minutes < 0 Then
                    minutes = minutes * -1
                End If

                If minutes < 10 Then
                    strMinutes = "0" + minutes.ToString()
                Else
                    strMinutes = minutes
                End If

                If hours < 10 Then
                    strHours = "0" + hours.ToString()
                Else
                    strHours = hours
                End If

                Dim timeElapsed As String = CType(strHours, String) & ":" & CType(strMinutes, String)
                strtimeelapsed = timeElapsed
                item("IsFlexible").Text = "(" + ResourceManager.GetString("Yes", CultureInfo) + ")" + "(" + timeElapsed + ")"
                '    End If
            ElseIf strIsFlexible = "False" Then
                item("IsFlexible").Text = "(" + ResourceManager.GetString("No", CultureInfo) + ")"
            End If

        End If
    End Sub

    Protected Sub dgrdEmpHR_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmpLeaveRequest.NeedDataSource

        objEmp_Nursing_Permission = New Emp_Nursing_Permission()
        objEmp_Nursing_Permission.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
        objEmp_Nursing_Permission.FK_StatusId = RequestStatus.ApprovedByDM
        dtCurrent = objEmp_Nursing_Permission.GetByHR()
        If Not (dtCurrent Is Nothing) Then
            Dim dv As New DataView(dtCurrent)
            dv.Sort = SortExepression
            dgrdEmpLeaveRequest.DataSource = dv
        End If
    End Sub

    Protected Sub lnkDownloadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        objEmp_Nursing_Permission = New Emp_Nursing_Permission
        With objEmp_Nursing_Permission
            .PermissionRequestId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionRequestId").ToString())
            .GetByPK()
            Dim FileName As String = .PermissionRequestId.ToString() + .AttachedFile
            Dim FilePath As String = Server.MapPath("..\\NursingPermissionRequestFiles\") & FileName
            CtlCommon.Open_Download_File(FileName, FilePath)

        End With
    End Sub

#End Region

#Region "Methods"

    Public Sub FillGridView()
        Try
            Dim dtPermissionRequest As New DataTable
            objEmp_Nursing_Permission = New Emp_Nursing_Permission
            objEmp_Nursing_Permission.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            objAPP_Settings = New APP_Settings()
            objAPP_Settings = objAPP_Settings.GetByPK()
            If Not objAPP_Settings.LeaveApprovalfromLeave = True Then
                NursingPermissionApproval = objAPP_Settings.NursingPermissionApproval
                objEmp_Nursing_Permission.FK_StatusId = NursingPermissionApproval
            End If

            objEmp_Nursing_Permission.FK_StatusId = RequestStatus.ApprovedByDM
            dtPermissionRequest = objEmp_Nursing_Permission.GetByHR()
            dgrdEmpLeaveRequest.DataSource = dtPermissionRequest
            dgrdEmpLeaveRequest.DataBind()

            If Not dtPermissionRequest Is Nothing Then
                Dim intCount As Integer = dtPermissionRequest.Rows.Count
                If intCount > 0 Then
                    If Not lblRequestNo.Text.Contains(":") Then
                        lblRequestNo.Text += " : " + (dtPermissionRequest.Rows.Count).ToString()
                    Else
                        Dim strArr() As String = lblRequestNo.Text.Split(":")
                        lblRequestNo.Text = strArr(0) + " : " + (dtPermissionRequest.Rows.Count).ToString()
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
        Dim temp_date As Date
        Dim temp_str_date As String
        Dim err2 As Integer

        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            If (.NursingPermissionApproval = 2 Or .NursingPermissionApproval = 3) Then
                Dim FromDate As DateTime = CDate(item.GetDataKeyValue("PermDate"))
                Dim ToDate As DateTime
                If ((item.GetDataKeyValue("PermEndDate").ToString()) <> "") Then
                    ToDate = CDate(item.GetDataKeyValue("PermEndDate"))
                Else
                    ToDate = Nothing
                End If

                Dim IsForPeriod As Boolean = CBool(item.GetDataKeyValue("IsForPeriod"))
                Dim EmployeeId As Integer = CInt(item.GetDataKeyValue("FK_EmployeeId"))
                Dim PermissionId As Integer = CInt(item.GetDataKeyValue("PermissionRequestId"))
                'Dim PermTypeId As Integer = CInt(item("PermTypeId").Text)
                Dim PermDate As DateTime = CDate(item.GetDataKeyValue("PermDate"))
                Dim IsFullyDay As Boolean = CBool(item.GetDataKeyValue("IsFullDay"))
                Dim AttachedFile As String = item.GetDataKeyValue("AttachedFile").ToString()
                Dim Remarks As String = String.Empty
                Dim AllowedTime As Integer = CInt(item.GetDataKeyValue("AllowedTime"))
                Dim IsFlexible As Boolean
                If ((item.GetDataKeyValue("IsFlexible").ToString()) <> "") Then
                    If (item.GetDataKeyValue("IsFlexible").ToString()) = "(No)" Then
                        IsFlexible = False
                    ElseIf (item.GetDataKeyValue("IsFlexible").ToString()) = "(Yes)" + "(" + strtimeelapsed + ")" Then
                        IsFlexible = True
                    End If
                    'IsFlexible = CBool(item("IsFlexible").Text)
                Else
                    IsFlexible = Nothing
                End If
                Dim FlexibilePermissionDuration As Integer = CInt(item.GetDataKeyValue("FlexibilePermissionDuration"))
                If (item.GetDataKeyValue("Remark").ToString() <> "") Then
                    Remarks = CStr(item.GetDataKeyValue("Remark").ToString())
                End If


                Dim EmpLeaveTotalBalance As Double = 0
                Dim OffAndHolidayDays As Integer = 0
                Dim PermissionDaysCount As Double = 0
                Dim ErrorMessage As String = String.Empty
                objEmp_Permissions = New Emp_Permissions
                objPermissionsTypes = New PermissionsTypes



                'If (objEmp_Permissions.ValidateEmployeePermission(EmployeeId, PermissionId, New Data.DataTable, Not IsForPeriod, FromDate, ToDate, Nothing, _
                '                                                  Nothing, PermDate, objPermissionsTypes, PermTypeId, IsFullyDay, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
                '    If (ErrorMessage <> String.Empty) Then
                '        CtlCommon.ShowMessage(Me.Page, ErrorMessage)
                '        Return
                '    End If

                'Else
                objEmp_Permissions.AddPermAllProcess(EmployeeId, -1, False, IsFlexible, False, Remarks, FromDate, _
                                                     ToDate, Not IsForPeriod, Nothing, Nothing, PermDate, PermissionDaysCount, OffAndHolidayDays, Nothing, _
                                                     EmpLeaveTotalBalance, PermissionId, 2, FlexibilePermissionDuration, AttachedFile, IsFullyDay, AllowedTime, ErrorMessage)
                'End If

                With objEmp_Nursing_Permission
                    .FK_StatusId = RequestStatus.ApprovedByHR
                    .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    .PermissionRequestId = CInt(item.GetDataKeyValue("PermissionRequestId"))
                    Err = .UpdatePermissionStatus()
                    strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                    If (File.Exists(Server.MapPath("~\NursingPermissionRequestFiles\\" + PermissionId.ToString() + AttachedFile))) Then

                        Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("~\NursingPermissionRequestFiles\\" + PermissionId.ToString() + AttachedFile))
                        CopyFile.CopyTo(Server.MapPath("~\PermissionFiles\\" + PermissionId.ToString() + AttachedFile))

                        Rename(Server.MapPath("~\PermissionFiles\\" + PermissionId.ToString() + AttachedFile), _
                                                Server.MapPath("~\PermissionFiles\\" + objEmp_Permissions.PermissionId.ToString() + AttachedFile))
                    End If

                    If Err = 0 Then
                        Dim dteFrom As DateTime = FromDate
                        Dim dteTo As DateTime = ToDate
                        If objAPP_Settings.ApprovalRecalMethod = 1 Then
                            If Not IsForPeriod Then
                                temp_date = PermDate
                                temp_str_date = DateToString(temp_date)
                                objRECALC_REQUEST.EMP_NO = EmployeeId
                                objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)

                                If Not temp_date = Date.Now.AddDays(1).ToShortDateString() Then
                                    Dim count As Integer
                                    While count < 5
                                        err2 = objRECALC_REQUEST.RECALCULATE()
                                        If err2 = 0 Then
                                            Exit While
                                        End If
                                        count += 1
                                    End While
                                End If
                            Else
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
                            End If
                        Else
                            If Not dteFrom > Date.Today Then
                                objRecalculateRequest = New RecalculateRequest
                                With objRecalculateRequest
                                    .Fk_EmployeeId = EmployeeId
                                    .FromDate = FromDate
                                    If IsForPeriod = True Then
                                        .ToDate = ToDate
                                    Else
                                        .ToDate = FromDate
                                    End If
                                    .ImmediatelyStart = True
                                    .RecalStatus = 0
                                    .CREATED_BY = SessionVariables.LoginUser.ID
                                    .Remarks = "Nursing Permission Request Approval - SYSTEM"
                                    err2 = .Add
                                End With
                            End If
                        End If
                    End If
                End With
            ElseIf (.NursingPermissionApproval = 3 Or .NursingPermissionApproval = 4) Then
                With objEmp_Nursing_Permission
                    .FK_StatusId = RequestStatus.ApprovedByHR
                    .PermissionRequestId = CStr(item.GetDataKeyValue("PermissionRequestId"))
                    .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    Err = .UpdatePermissionStatus()

                    strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                    'If Err = 0 Then
                    '    FillGridView()
                    '    CtlCommon.ShowMessage(Me.Page, strMessage)
                    'Else
                    '    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo))
                    'End If
                End With
            End If
        End With
        ErrNo = Err
        Msg = strMessage
        item = Nothing
    End Sub

    Private Sub RejectAll(ByVal item As GridDataItem, ByRef ErrNo As Integer, ByRef Msg As String)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        objAPP_Settings = New APP_Settings
        With objEmp_Nursing_Permission
            .PermissionRequestId = CStr(item.GetDataKeyValue("PermissionRequestId"))
            .GetByPK()
            .FK_StatusId = RequestStatus.RejectedByHR
            .RejectionReason = txtRejectAllReason.Text.Trim(",")
            .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            Err = .UpdatePermissionStatus()

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
