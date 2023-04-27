Imports SmartV.UTILITIES
Imports TA.Employees
Imports TA.Admin
Imports Telerik.Web.UI
Imports TA.Definitions
Imports TA.SelfServices
Imports System.IO
Imports System.Data
Imports TA.DailyTasks

Partial Class Requests_UserControls_GM_GM_PermissionApproval
    Inherits System.Web.UI.UserControl


#Region "Class Variables"

    Private objEmp_PermissionsRequest As New Emp_PermissionsRequest
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
    Private objWorkSchedule As WorkSchedule

    Private Enum RequestStatus
        Pending = 1
        ApprovedByDM = 2
        ApprovedByHR = 3
        RejectedByDM = 4
        RejectedByHR = 5
        ApprovedByGM = 6
        RejectedByGM = 7
    End Enum

    Public Property PermissionId() As String
        Get
            Return ViewState("PermissionId")
        End Get
        Set(ByVal value As String)
            ViewState("PermissionId") = value
        End Set
    End Property

    Public Property PermissionApproval() As Integer
        Get
            Return ViewState("PermissionApproval")
        End Get
        Set(ByVal value As Integer)
            ViewState("PermissionApproval") = value
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
            mvPermissionApproval.SetActiveView(viewGMApproval)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                MsgLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                MsgLang = "en"
            End If
            lblPermissionRequests.Text = ResourceManager.GetString("GeneralMgrPermitHeader", CultureInfo)
            Page.Title = "Work Force Pro : :" + ResourceManager.GetString("GeneralMgrPermitHeader", CultureInfo)
            ShowHideControls()
            FillGridView()
        End If
    End Sub

    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        objPermissionsTypes = New PermissionsTypes
        objRECALC_REQUEST = New RECALC_REQUEST
        Dim temp_date As Date
        Dim temp_str_date As String
        Dim err2 As Integer

        Dim PermTypeId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermTypeId"))

        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()

            If Not .PermApprovalFromPermission Then
                PermissionApproval = objAPP_Settings.LeaveApproval
            Else
                objPermissionsTypes.PermId = PermTypeId
                objPermissionsTypes = objPermissionsTypes.GetByPK()
                PermissionApproval = objPermissionsTypes.PermissionApproval
            End If

            If (PermissionApproval = 4) Then
                Dim FromDate As DateTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermDate"))
                Dim ToDate As DateTime
                If ((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermEndDate").ToString()) <> "") Then
                    ToDate = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermEndDate"))
                Else
                    ToDate = Nothing
                End If

                Dim IsForPeriod As Boolean = CBool(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("IsForPeriod"))
                Dim EmployeeId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_EmployeeId"))
                Dim PermissionId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionId"))
                Dim PermissionRequestId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionId"))
                'Dim PermTypeId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("PermTypeId").Text)
                Dim PermDate As DateTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermDate"))
                Dim FromTime As DateTime
                Dim ToTime As DateTime
                Dim IsFlexible As Boolean
                If ((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("IsFlexible").ToString()) <> "") Then
                    IsFlexible = CBool(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("IsFlexible"))
                End If

                If (Not String.IsNullOrEmpty((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FromTime").ToString()))) AndAlso _
                    (Not String.IsNullOrEmpty((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("ToTime").ToString()))) Then
                    FromTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FromTime"))
                    ToTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("ToTime"))
                Else
                    FromTime = CDate(DirectCast(CType(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).FindControl("hdnFromTime"), HiddenField).Value)
                    ToTime = CDate(DirectCast(CType(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).FindControl("hdnToTime"), HiddenField).Value)
                End If

                'Dim PermissionOption As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("PermissionOption").Text)
                Dim days As String = DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Days").ToString()
                'Dim IsFlexible As Boolean = CBool(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("IsFlexible").Text)
                Dim IsFullyDay As Boolean = CBool(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("IsFullDay"))
                Dim AttachedFile As String = DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("AttachedFile").ToString()
                'Dim FlexibilePermissionDuration As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("FlexibilePermissionDuration").Text)


                FromTime = PermDate.AddHours(FromTime.Hour).AddMinutes(FromTime.Minute)
                ToTime = PermDate.AddHours(ToTime.Hour).AddMinutes(ToTime.Minute)

                Dim Remarks As String = String.Empty
                If (DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Remark").ToString() <> "") Then
                    Remarks = CStr(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Remark").ToString())
                End If


                Dim EmpLeaveTotalBalance As Double = 0
                Dim OffAndHolidayDays As Integer = 0
                Dim PermissionDaysCount As Double = 0
                Dim ErrorMessage As String = String.Empty
                objEmp_Permissions = New Emp_Permissions
                objPermissionsTypes = New PermissionsTypes

                'If Not CheckPermissionTypeDuration(PermTypeId, EmployeeId, Not IsForPeriod, PermDate, FromDate, ToDate, FromTime, ToTime) Then
                '    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PermissionTypeDurationLimit"))
                '    Return
                'End If
                PermissionId = 0
                If (objEmp_Permissions.ValidateEmployeePermission(EmployeeId, PermissionId, New Data.DataTable, Not IsForPeriod, FromDate, ToDate, FromTime, _
                                                                  ToTime, PermDate, objPermissionsTypes, PermTypeId, IsFullyDay, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
                    If (ErrorMessage <> String.Empty) Then
                        CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                        Return
                    End If

                Else
                    objEmp_Permissions.AddPermAllProcess(EmployeeId, PermTypeId, False, IsFlexible, False, Remarks, FromDate, _
                                                         ToDate, Not IsForPeriod, FromTime, ToTime, PermDate, PermissionDaysCount, OffAndHolidayDays, days, _
                                                         EmpLeaveTotalBalance, PermissionId, 1, 0, AttachedFile, IsFullyDay, Nothing, ErrorMessage)
                End If

                With objEmp_PermissionsRequest
                    .PermissionId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionId"))
                    .GetByPK()
                    .FK_StatusId = RequestStatus.ApprovedByGM
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    Err = .UpdatePermissionStatus()
                    strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                    If (File.Exists(Server.MapPath("~\PermissionRequestFiles\\" + PermissionRequestId.ToString() + AttachedFile))) Then

                        Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("~\PermissionRequestFiles\\" + PermissionRequestId.ToString() + AttachedFile))
                        CopyFile.CopyTo(Server.MapPath("~\PermissionFiles\\" + PermissionRequestId.ToString() + AttachedFile))

                        Rename(Server.MapPath("~\PermissionFiles\\" + PermissionRequestId.ToString() + AttachedFile), _
                                                Server.MapPath("~\PermissionFiles\\" + objEmp_Permissions.PermissionId.ToString() + AttachedFile))
                    End If

                    If Err = 0 Then
                        '----------------------Validate Employee Schedule------------'
                        objWorkSchedule = New WorkSchedule()
                        Dim intWorkScheduleType As Integer
                        Dim intWorkScheduleId As Integer

                        Dim ScheduleDate As DateTime
                        If Not IsForPeriod Then
                            ScheduleDate = PermDate
                        Else
                            ScheduleDate = FromDate
                        End If

                        objWorkSchedule.EmployeeId = EmployeeId
                        objWorkSchedule = objWorkSchedule.GetActive_SchedulebyEmpId_row(ScheduleDate)
                        If Not objWorkSchedule Is Nothing Then
                            intWorkScheduleType = objWorkSchedule.ScheduleType
                            intWorkScheduleId = objWorkSchedule.ScheduleId
                        End If

                        If objWorkSchedule Is Nothing Then
                            objWorkSchedule = New WorkSchedule()
                            With objWorkSchedule
                                .GetByDefault()
                                intWorkScheduleType = .ScheduleType
                                intWorkScheduleId = .ScheduleId
                            End With
                        End If
                        objEmp_Permissions = New Emp_Permissions
                        Dim dt As DataTable
                        Dim dt2 As DataTable
                        Dim PreviousPermDate As DateTime
                        Dim NextRestDate As DateTime
                        '----------------------Validate Employee Schedule------------'


                        Dim dteFrom As DateTime = FromDate
                        Dim dteTo As DateTime = ToDate

                        FillGridView()
                        If objAPP_Settings.ApprovalRecalMethod = 1 Then
                            If Not IsForPeriod Then
                                If intWorkScheduleType = 3 Then

                                    objEmp_Permissions.FK_EmployeeId = EmployeeId
                                    objEmp_Permissions.PermDate = PermDate
                                    dt = objEmp_Permissions.Get_Previous_Restday_Date()

                                    If dt.Rows.Count > 0 Then

                                        PreviousPermDate = dt.Rows(0)("RestDate")
                                        objEmp_Permissions.FK_EmployeeId = EmployeeId
                                        objEmp_Permissions.PreviousPermDate = PreviousPermDate
                                        dt2 = objEmp_Permissions.Get_Next_Restday_Date()
                                        NextRestDate = dt2.Rows(0)("NextRestDate")
                                        If NextRestDate > Date.Today Then
                                            NextRestDate = Date.Today
                                        End If
                                    End If

                                    If dt.Rows.Count > 0 Then
                                        temp_date = PreviousPermDate
                                        temp_str_date = NextRestDate
                                    Else
                                        temp_date = PermDate
                                        temp_str_date = DateToString(temp_date)
                                    End If
                                Else
                                    temp_date = PermDate
                                    temp_str_date = DateToString(temp_date)
                                End If

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

                                If intWorkScheduleType = 3 Then
                                    objEmp_Permissions.FK_EmployeeId = EmployeeId
                                    objEmp_Permissions.PermDate = dteFrom
                                    dt = objEmp_Permissions.Get_Previous_Restday_Date()

                                    If dt.Rows.Count > 0 Then
                                        PreviousPermDate = dt.Rows(0)("RestDate")
                                        objEmp_Permissions.FK_EmployeeId = EmployeeId
                                        objEmp_Permissions.PreviousPermDate = PreviousPermDate
                                        dt2 = objEmp_Permissions.Get_Next_Restday_Date()
                                        NextRestDate = dt2.Rows(0)("NextRestDate")
                                        If NextRestDate > Date.Today Then
                                            NextRestDate = Date.Today
                                        End If
                                    End If

                                    If dt.Rows.Count > 0 Then
                                        dteFrom = PreviousPermDate
                                        dteTo = NextRestDate
                                    End If
                                End If

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
                            If Not FromDate > Date.Today Then

                                If intWorkScheduleType = 3 Then
                                    objEmp_Permissions.FK_EmployeeId = EmployeeId
                                    objEmp_Permissions.PermDate = FromDate
                                    dt = objEmp_Permissions.Get_Previous_Restday_Date()

                                    If dt.Rows.Count > 0 Then
                                        PreviousPermDate = dt.Rows(0)("RestDate")
                                        objEmp_Permissions.FK_EmployeeId = EmployeeId
                                        objEmp_Permissions.PreviousPermDate = PreviousPermDate
                                        dt2 = objEmp_Permissions.Get_Next_Restday_Date()
                                        NextRestDate = dt2.Rows(0)("NextRestDate")
                                        If NextRestDate > Date.Today Then
                                            NextRestDate = Date.Today
                                        End If
                                    End If

                                    If dt.Rows.Count > 0 Then
                                        FromDate = PreviousPermDate
                                        ToDate = NextRestDate
                                    End If
                                End If

                                objRecalculateRequest = New RecalculateRequest
                                With objRecalculateRequest
                                    .Fk_EmployeeId = EmployeeId
                                    .FromDate = FromDate
                                    If IsForPeriod = True Then
                                        .ToDate = ToDate
                                    Else
                                        If intWorkScheduleType = 3 Then
                                            .ToDate = NextRestDate
                                        Else
                                            .ToDate = FromDate
                                        End If
                                    End If
                                    .ImmediatelyStart = True
                                    .RecalStatus = 0
                                    .CREATED_BY = SessionVariables.LoginUser.ID
                                    .Remarks = "Permission Request Approval - SYSTEM"
                                    err2 = .Add
                                End With
                            End If
                        End If
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
        With objEmp_PermissionsRequest

            .PermissionId = PermissionId
            .GetByPK()
            .FK_StatusId = RequestStatus.RejectedByGM
            .RejectedReason = txtRejectedReason.Text.Trim(",")
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
            mpeRejectPopupPermission.Show()
        ElseIf e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdEmpLeaveRequest_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdEmpLeaveRequest.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item
            Dim PermDateMonth As Integer = 0

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("PermDate").ToString())) And (Not item.GetDataKeyValue("PermDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("PermDate")
                item("PermDate").Text = fromDate.ToShortDateString()
                PermDateMonth = fromDate.Month
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("PermEndDate").ToString())) And (Not item.GetDataKeyValue("PermEndDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("PermEndDate")
                item("PermEndDate").Text = fromDate.ToShortDateString()
            End If

            Dim PermissionId As Integer = item.GetDataKeyValue("PermissionId")
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

            Dim permTypeId As Integer = Convert.ToInt32(item.GetDataKeyValue("PermTypeId"))

            Dim empId As Integer = Convert.ToInt32(item.GetDataKeyValue("FK_EmployeeId"))

            objEmp_Permissions = New Emp_Permissions()

            DirectCast(e.Item.FindControl("lblRemainingBalance"), Label).Text = (objEmp_Permissions.GetPermissionRemainingBalance(permTypeId, empId, Not IsForPeriod, PermDate, PermDate, PermEndDate)).ToString()

            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
                item("PermName").Text = DirectCast(item.FindControl("hdnPermissionTypeAr"), HiddenField).Value
                item("StatusName").Text = DirectCast(item.FindControl("hdnStatusNameAr"), HiddenField).Value
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromTime").ToString())) And (Not item.GetDataKeyValue("FromTime").ToString() = "") And _
                (Not String.IsNullOrEmpty(item.GetDataKeyValue("ToTime").ToString())) And (Not item.GetDataKeyValue("ToTime").ToString() = "")) Then
                Dim fromTime As DateTime = Convert.ToDateTime(item.GetDataKeyValue("FromTime")).ToString("HH:mm")
                Dim toTime As DateTime = Convert.ToDateTime(item.GetDataKeyValue("ToTime")).ToString("HH:mm")
                'item("TotalTime").Text = (toTime.Subtract(fromTime).TotalMinutes).ToString() + IIf(Lang = CtlCommon.Lang.EN, " Minutes", "دقائق ")
                'Dim hour As Integer = item.GetDataKeyValue("TotalTime") / 60
                'Dim minutes As Integer = item.GetDataKeyValue("TotalTime") - (hour * 60)

                'Dim strHours As String = "0"
                'Dim strMinutes As String = "0"

                'If minutes < 0 Then
                '    minutes = minutes * -1
                'End If

                'If minutes < 10 Then
                '    strMinutes = "0" + minutes.ToString()
                'Else
                '    strMinutes = minutes
                'End If

                'If hour < 10 Then
                '    strHours = "0" + hour.ToString()
                'Else
                '    strHours = hour
                'End If

                'item("TotalTime").Text = CType(strHours, String) & ":" & CType(strMinutes, String)
                ''item("TotalTime").Text = item("TotalTime").Text + IIf(Lang = CtlCommon.Lang.EN, " Hours", "ساعات ")
                'item("TotalTime").Text = item("TotalTime").Text

                CType(item.FindControl("hdnFromTime"), HiddenField).Value = item("FromTime").Text
                CType(item.FindControl("hdnToTime"), HiddenField).Value = item("ToTime").Text

            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("IsFullDay").ToString())) And (Not item.GetDataKeyValue("IsFullDay").ToString() = "")) Then
                Dim IsFullDay As Boolean = Convert.ToBoolean(item.GetDataKeyValue("IsFullDay"))
                If IsFullDay Then
                    item("FromTime").Text = String.Empty
                    item("ToTime").Text = String.Empty
                    item("TotalTime").Text = IIf(Lang = CtlCommon.Lang.EN, " Full Day", "يوم عمل كامل ")
                End If
            End If

        End If
    End Sub

    Protected Sub dgrdEmpHR_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmpLeaveRequest.NeedDataSource

        objEmp_PermissionsRequest = New Emp_PermissionsRequest()
        objEmp_PermissionsRequest.FK_StatusId = RequestStatus.ApprovedByHR
        dtCurrent = objEmp_PermissionsRequest.GetByGeneralManager()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdEmpLeaveRequest.DataSource = dv

    End Sub

    Protected Sub lnkDownloadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        objEmp_PermissionsRequest = New Emp_PermissionsRequest
        With objEmp_PermissionsRequest
            .PermissionId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionId").ToString())
            .GetByPK()
            Dim FileName As String = .PermissionId.ToString() + .AttachedFile
            Dim FilePath As String = Server.MapPath("..\\PermissionRequestFiles\") & FileName
            CtlCommon.Open_Download_File(FileName, FilePath)

        End With
    End Sub

#End Region

#Region "Methods"

    Public Sub FillGridView()
        Try
            Dim dtPermissionRequest As New DataTable
            objEmp_PermissionsRequest = New Emp_PermissionsRequest()
            objEmp_PermissionsRequest.FK_StatusId = RequestStatus.ApprovedByHR
            dtPermissionRequest = objEmp_PermissionsRequest.GetByGeneralManager()
            dgrdEmpLeaveRequest.DataSource = dtPermissionRequest
            dgrdEmpLeaveRequest.DataBind()

            If Not dtPermissionRequest Is Nothing Then
                If dtPermissionRequest.Rows.Count > 0 Then
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

    Private Function GetRemainingBalance(ByVal permTypeId As Integer, ByVal EmpIdParam As Integer, ByVal IsOneDay As Boolean, _
                                                 ByVal PermDate As DateTime, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal FromTime As DateTime, _
                                                 ByVal ToTime As DateTime) As String
        objEmp_PermissionsRequest = New Emp_PermissionsRequest()
        objPermissionsTypes = New PermissionsTypes()

        Dim empId As Integer = EmpIdParam
        Dim permId As Integer = permTypeId
        Dim remainingBalanceCount As Integer = 0
        Dim monthlyBalance As Integer = 0

        If (Not permTypeId = -1) Then
            objEmp_PermissionsRequest.FK_EmployeeId = empId
            objEmp_PermissionsRequest.FK_PermId = permId

            'Dim dtpermissionRemainingBalance As DataTable = objEmp_PermissionsRequest.GetAll_ByPermMonthAndType(permMonth)

            objEmp_Permissions = New Emp_Permissions()
            objEmp_Permissions.FK_EmployeeId = empId
            objEmp_Permissions.FK_PermId = permId
            If IsOneDay Then
                objEmp_Permissions.FromTime = DateSerial(PermDate.Year, PermDate.Month, 1)
                objEmp_Permissions.ToTime = DateSerial(PermDate.Year, PermDate.Month + 1, 0)
            Else
                objEmp_Permissions.FromTime = DateSerial(StartDate.Year, StartDate.Month, 1)
                objEmp_Permissions.ToTime = DateSerial(EndDate.Year, EndDate.Month + 1, 0)
            End If

            Dim dtpermissionRemainingBalance As DataTable = objEmp_Permissions.GetAllDurationByEmployee()

            With objPermissionsTypes
                .PermId = permId
                .GetByPK()
                monthlyBalance = .MonthlyBalance
            End With

            For Each row As DataRow In dtpermissionRemainingBalance.Rows
                If (Not IsDBNull(row("ToTime"))) And (Not IsDBNull(row("FromTime"))) Then
                    remainingBalanceCount += (Convert.ToDateTime(row("ToTime")).Subtract(Convert.ToDateTime(row("FromTime"))).TotalMinutes)
                End If
            Next

        End If

        Dim dcmRemainingBalanceCount As Decimal = (monthlyBalance - remainingBalanceCount)

        Dim hours As Integer = dcmRemainingBalanceCount \ 60
        Dim minutes As Integer = dcmRemainingBalanceCount - (hours * 60)
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

        Return timeElapsed

    End Function

    Private Function CheckPermissionTypeDuration(ByVal permTypeId As Integer, ByVal EmpIdParam As Integer, ByVal IsOneDay As Boolean, _
                                                 ByVal PermDate As DateTime, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal FromTime As DateTime, _
                                                 ByVal ToTime As DateTime) As Boolean
        'If Not LeaveTypeDuration = 0 Then

        Dim permId As Integer = permTypeId
        Dim monthlyBalance As Integer = 0
        Dim remainingBalanceCount As Integer = 0
        objPermissionsTypes = New PermissionsTypes()

        With objPermissionsTypes
            .PermId = permId
            .GetByPK()
            monthlyBalance = .MonthlyBalance
        End With

        Dim EmpDt As DataTable
        Dim intFromtime As Integer = 0
        Dim intToTime As Integer = 0
        Dim studyFound As Boolean = False

        objEmp_Permissions = New Emp_Permissions
        With objEmp_Permissions
            .FK_EmployeeId = EmpIdParam
            .FK_PermId = permTypeId

            If IsOneDay Then
                .FromTime = DateSerial(PermDate.Year, PermDate.Month, 1)
                .ToTime = DateSerial(PermDate.Year, PermDate.Month + 1, 0)
            Else
                .FromTime = DateSerial(StartDate.Year, StartDate.Month, 1)
                .ToTime = DateSerial(EndDate.Year, EndDate.Month + 1, 0)
            End If

            EmpDt = .GetAllDurationByEmployee()
        End With

        If EmpDt IsNot Nothing Then
            If EmpDt.Rows.Count > 0 Then
                For Each row As DataRow In EmpDt.Rows
                    If row("PermissionOption") = 3 Then
                        studyFound = True
                        Continue For
                    End If

                    If Not row("PermissionOption") = 2 Then
                        remainingBalanceCount += (Convert.ToDateTime(row("ToTime")).Subtract(Convert.ToDateTime(row("FromTime")))).TotalMinutes
                        'intFromtime += (Convert.ToDateTime(row("FromTime")).Hour * 60) + (Convert.ToDateTime(row("FromTime")).Minute)
                        'intToTime += (Convert.ToDateTime(row("ToTime")).Hour * 60) + (Convert.ToDateTime(row("ToTime")).Minute)

                    End If
                Next

                remainingBalanceCount += (Convert.ToDateTime(ToTime).Subtract(Convert.ToDateTime(FromTime))).TotalMinutes
                'intFromtime += (RadTPfromTime.SelectedDate.Value.Hour * 60) + (RadTPfromTime.SelectedDate.Value.Minute)
                'intToTime += (RadTPtoTime.SelectedDate.Value.Hour * 60) + (RadTPtoTime.SelectedDate.Value.Minute)

                'If studyFound Then
                If remainingBalanceCount > monthlyBalance Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Else
            Return True
        End If

        Return True
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
        objPermissionsTypes = New PermissionsTypes
        objRECALC_REQUEST = New RECALC_REQUEST
        Dim temp_date As Date
        Dim temp_str_date As String
        Dim err2 As Integer

        Dim PermTypeId As Integer = CInt(item.GetDataKeyValue("PermTypeId"))

        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()

            If Not .PermApprovalFromPermission Then
                PermissionApproval = objAPP_Settings.LeaveApproval
            Else
                objPermissionsTypes.PermId = PermTypeId
                objPermissionsTypes = objPermissionsTypes.GetByPK()
                PermissionApproval = objPermissionsTypes.PermissionApproval
            End If

            If (PermissionApproval = 4) Then
                Dim FromDate As DateTime = CDate(item.GetDataKeyValue("PermDate"))
                Dim ToDate As DateTime
                If ((item.GetDataKeyValue("PermEndDate").ToString()) <> "") Then
                    ToDate = CDate(item.GetDataKeyValue("PermEndDate"))
                Else
                    ToDate = Nothing
                End If

                Dim IsForPeriod As Boolean = CBool(item.GetDataKeyValue("IsForPeriod"))
                Dim EmployeeId As Integer = CInt(item.GetDataKeyValue("FK_EmployeeId"))
                Dim PermissionId As Integer = CInt(item.GetDataKeyValue("PermissionId"))
                Dim PermissionRequestId As Integer = CInt(item.GetDataKeyValue("PermissionId"))
                'Dim PermTypeId As Integer = CInt(item("PermTypeId").Text)
                Dim PermDate As DateTime = CDate(item.GetDataKeyValue("PermDate"))
                Dim FromTime As DateTime
                Dim ToTime As DateTime
                Dim IsFlexible As Boolean
                If ((item.GetDataKeyValue("IsFlexible").ToString()) <> "") Then
                    IsFlexible = CBool(item.GetDataKeyValue("IsFlexible"))
                End If

                If (Not String.IsNullOrEmpty((item.GetDataKeyValue("FromTime").ToString()))) AndAlso _
                    (Not String.IsNullOrEmpty((item.GetDataKeyValue("ToTime").ToString()))) Then
                    FromTime = CDate(item.GetDataKeyValue("FromTime"))
                    ToTime = CDate(item.GetDataKeyValue("ToTime"))
                Else
                    FromTime = CType(item.FindControl("hdnFromTime"), HiddenField).Value
                    ToTime = CType(item.FindControl("hdnToTime"), HiddenField).Value
                End If

                'Dim PermissionOption As Integer = CInt(item("PermissionOption").Text)
                Dim days As String = item.GetDataKeyValue("Days").ToString()
                'Dim IsFlexible As Boolean = CBool(item("IsFlexible").Text)
                Dim IsFullyDay As Boolean = CBool(item.GetDataKeyValue("IsFullDay"))
                Dim AttachedFile As String = item.GetDataKeyValue("AttachedFile").ToString()
                'Dim FlexibilePermissionDuration As Integer = CInt(item("FlexibilePermissionDuration").Text)


                FromTime = PermDate.AddHours(FromTime.Hour).AddMinutes(FromTime.Minute)
                ToTime = PermDate.AddHours(ToTime.Hour).AddMinutes(ToTime.Minute)

                Dim Remarks As String = String.Empty
                If Not item.GetDataKeyValue("Remark") Is Nothing Then


                    If (item.GetDataKeyValue("Remark").ToString() <> "") Then
                        Remarks = CStr(item.GetDataKeyValue("Remark").ToString())
                    End If

                End If
                Dim EmpLeaveTotalBalance As Double = 0
                Dim OffAndHolidayDays As Integer = 0
                Dim PermissionDaysCount As Double = 0
                Dim ErrorMessage As String = String.Empty
                objEmp_Permissions = New Emp_Permissions
                objPermissionsTypes = New PermissionsTypes

                'If Not CheckPermissionTypeDuration(PermTypeId, EmployeeId, Not IsForPeriod, PermDate, FromDate, ToDate, FromTime, ToTime) Then
                '    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PermissionTypeDurationLimit"))
                '    Return
                'End If
                PermissionId = 0

                If (objEmp_Permissions.ValidateEmployeePermission(EmployeeId, PermissionId, New Data.DataTable, Not IsForPeriod, FromDate, ToDate, FromTime, _
                                                                  ToTime, PermDate, objPermissionsTypes, PermTypeId, IsFullyDay, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
                    If (ErrorMessage <> String.Empty) Then
                        CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                        Msg = strMessage
                        Return
                    End If

                Else
                    objEmp_Permissions.AddPermAllProcess(EmployeeId, PermTypeId, False, IsFlexible, False, Remarks, FromDate, _
                                                         ToDate, Not IsForPeriod, FromTime, ToTime, PermDate, PermissionDaysCount, OffAndHolidayDays, days, _
                                                         EmpLeaveTotalBalance, PermissionId, 1, 0, AttachedFile, IsFullyDay, Nothing, ErrorMessage)
                End If

                With objEmp_PermissionsRequest
                    .PermissionId = CInt(item.GetDataKeyValue("PermissionId"))
                    .GetByPK()
                    .FK_StatusId = RequestStatus.ApprovedByGM
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    Err = .UpdatePermissionStatus()
                    strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                    If (File.Exists(Server.MapPath("~\PermissionRequestFiles\\" + PermissionRequestId.ToString() + AttachedFile))) Then

                        Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("~\PermissionRequestFiles\\" + PermissionRequestId.ToString() + AttachedFile))
                        CopyFile.CopyTo(Server.MapPath("~\PermissionFiles\\" + PermissionRequestId.ToString() + AttachedFile))

                        Rename(Server.MapPath("~\PermissionFiles\\" + PermissionRequestId.ToString() + AttachedFile), _
                                                Server.MapPath("~\PermissionFiles\\" + objEmp_Permissions.PermissionId.ToString() + AttachedFile))
                    End If

                    'If (File.Exists(Server.MapPath("~\PermissionRequestFiles\\" + PermissionId.ToString() + AttachedFile))) Then
                    '    My.Computer.FileSystem.RenameFile(Server.MapPath("~\PermissionRequestFiles\\" + PermissionId.ToString() + AttachedFile), _
                    '                                     (objEmp_Permissions.PermissionId.ToString() + AttachedFile).ToString())

                    '    If (File.Exists(Server.MapPath("~\PermissionRequestFiles\\" + objEmp_Permissions.PermissionId.ToString() + AttachedFile))) Then
                    '        Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("~\PermissionRequestFiles\\" + objEmp_Permissions.PermissionId.ToString() + AttachedFile))
                    '        CopyFile.CopyTo(Server.MapPath("~\PermissionFiles\\" + objEmp_Permissions.PermissionId.ToString() + AttachedFile))
                    '    End If

                    'End If

                    If Err = 0 Then
                        '----------------------Validate Employee Schedule------------'
                        objWorkSchedule = New WorkSchedule()
                        Dim intWorkScheduleType As Integer
                        Dim intWorkScheduleId As Integer

                        Dim ScheduleDate As DateTime
                        If Not IsForPeriod Then
                            ScheduleDate = PermDate
                        Else
                            ScheduleDate = FromDate
                        End If

                        objWorkSchedule.EmployeeId = EmployeeId
                        objWorkSchedule = objWorkSchedule.GetActive_SchedulebyEmpId_row(ScheduleDate)
                        If Not objWorkSchedule Is Nothing Then
                            intWorkScheduleType = objWorkSchedule.ScheduleType
                            intWorkScheduleId = objWorkSchedule.ScheduleId
                        End If

                        If objWorkSchedule Is Nothing Then
                            objWorkSchedule = New WorkSchedule()
                            With objWorkSchedule
                                .GetByDefault()
                                intWorkScheduleType = .ScheduleType
                                intWorkScheduleId = .ScheduleId
                            End With
                        End If
                        objEmp_Permissions = New Emp_Permissions
                        Dim dt As DataTable
                        Dim dt2 As DataTable
                        Dim PreviousPermDate As DateTime
                        Dim NextRestDate As DateTime
                        '----------------------Validate Employee Schedule------------'

                        Dim dteFrom As DateTime = FromDate
                        Dim dteTo As DateTime = ToDate

                        If objAPP_Settings.ApprovalRecalMethod = 1 Then
                            If Not IsForPeriod Then
                                If intWorkScheduleType = 3 Then

                                    objEmp_Permissions.FK_EmployeeId = EmployeeId
                                    objEmp_Permissions.PermDate = PermDate
                                    dt = objEmp_Permissions.Get_Previous_Restday_Date()

                                    If dt.Rows.Count > 0 Then

                                        PreviousPermDate = dt.Rows(0)("RestDate")
                                        objEmp_Permissions.FK_EmployeeId = EmployeeId
                                        objEmp_Permissions.PreviousPermDate = PreviousPermDate
                                        dt2 = objEmp_Permissions.Get_Next_Restday_Date()
                                        NextRestDate = dt2.Rows(0)("NextRestDate")
                                        If NextRestDate > Date.Today Then
                                            NextRestDate = Date.Today
                                        End If
                                    End If

                                    If dt.Rows.Count > 0 Then
                                        temp_date = PreviousPermDate
                                        temp_str_date = DateToString(NextRestDate)
                                    Else
                                        temp_date = PermDate
                                        temp_str_date = DateToString(temp_date)
                                    End If
                                Else
                                    temp_date = PermDate
                                    temp_str_date = DateToString(temp_date)
                                End If

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

                                If intWorkScheduleType = 3 Then
                                    objEmp_Permissions.FK_EmployeeId = EmployeeId
                                    objEmp_Permissions.PermDate = dteFrom
                                    dt = objEmp_Permissions.Get_Previous_Restday_Date()

                                    If dt.Rows.Count > 0 Then
                                        PreviousPermDate = dt.Rows(0)("RestDate")
                                        objEmp_Permissions.FK_EmployeeId = EmployeeId
                                        objEmp_Permissions.PreviousPermDate = PreviousPermDate
                                        dt2 = objEmp_Permissions.Get_Next_Restday_Date()
                                        NextRestDate = dt2.Rows(0)("NextRestDate")
                                        If NextRestDate > Date.Today Then
                                            NextRestDate = Date.Today
                                        End If
                                    End If

                                    If dt.Rows.Count > 0 Then
                                        dteFrom = PreviousPermDate
                                        dteTo = NextRestDate
                                    End If
                                End If

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
                            If Not FromDate > Date.Today Then

                                If intWorkScheduleType = 3 Then
                                    objEmp_Permissions.FK_EmployeeId = EmployeeId
                                    objEmp_Permissions.PermDate = FromDate
                                    dt = objEmp_Permissions.Get_Previous_Restday_Date()

                                    If dt.Rows.Count > 0 Then
                                        PreviousPermDate = dt.Rows(0)("RestDate")
                                        objEmp_Permissions.FK_EmployeeId = EmployeeId
                                        objEmp_Permissions.PreviousPermDate = PreviousPermDate
                                        dt2 = objEmp_Permissions.Get_Next_Restday_Date()
                                        NextRestDate = dt2.Rows(0)("NextRestDate")
                                        If NextRestDate > Date.Today Then
                                            NextRestDate = Date.Today
                                        End If
                                    End If

                                    If dt.Rows.Count > 0 Then
                                        FromDate = PreviousPermDate
                                        ToDate = NextRestDate
                                    End If
                                End If

                                objRecalculateRequest = New RecalculateRequest
                                With objRecalculateRequest
                                    .Fk_EmployeeId = EmployeeId
                                    .FromDate = FromDate
                                    If IsForPeriod = True Then
                                        .ToDate = ToDate
                                    Else
                                        If intWorkScheduleType = 3 Then
                                            .ToDate = NextRestDate
                                        Else
                                            .ToDate = FromDate
                                        End If
                                    End If
                                    .ImmediatelyStart = True
                                    .RecalStatus = 0
                                    .CREATED_BY = SessionVariables.LoginUser.ID
                                    .Remarks = "Permission Request Approval - SYSTEM"
                                    err2 = .Add
                                End With
                            End If
                        End If
                    End If
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
        With objEmp_PermissionsRequest

            .PermissionId = CInt(item.GetDataKeyValue("PermissionId"))
            .GetByPK()
            .FK_StatusId = RequestStatus.RejectedByGM
            .RejectedReason = txtRejectedReason.Text.Trim(",")
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
