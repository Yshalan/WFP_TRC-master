Imports SmartV.UTILITIES
Imports TA.Employees
Imports TA.Admin
Imports Telerik.Web.UI
Imports TA.Definitions
Imports TA.SelfServices
Imports System.IO
Imports System.Data
Imports TA.DailyTasks


Partial Class Requests_UserControls_DM_PermissionApproval
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
            lblPermissionRequests.Text = ResourceManager.GetString("DirectMgrPermitHeader", CultureInfo)
            Page.Title = "Work Force Pro : :" + ResourceManager.GetString("DirectMgrPermitHeader", CultureInfo)

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
        Dim IsFlexible As Boolean
        Dim FlexibilePermissionDuration As Integer = 0
        Dim err2 As Integer

        Dim PermTypeId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermTypeId").ToString())

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

            Dim NextApprovalStatus As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("NextApprovalStatus").ToString())

            If (PermissionApproval = 1 And NextApprovalStatus = 2) Then
                Dim FromDate As DateTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermDate").ToString())
                Dim ToDate As DateTime
                If ((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermEndDate").ToString()) <> "") Then
                    ToDate = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermEndDate").ToString())
                Else
                    ToDate = Nothing
                End If

                Dim IsForPeriod As Boolean = CBool(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("IsForPeriod").ToString())
                Dim EmployeeId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_EmployeeId").ToString())
                Dim PermissionId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionId").ToString())
                Dim PermissionRequestId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionId").ToString())
                'Dim PermTypeId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("PermTypeId").Text)
                Dim PermDate As DateTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermDate").ToString())
                Dim FromTime As DateTime
                Dim ToTime As DateTime

                If (Not String.IsNullOrEmpty((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FromTime").ToString()))) AndAlso
                    (Not String.IsNullOrEmpty((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("ToTime").ToString()))) Then
                    FromTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FromTime").ToString())
                    ToTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("ToTime").ToString())
                Else
                    FromTime = CDate(DirectCast(CType(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).FindControl("hdnFromTime"), HiddenField).Value)
                    ToTime = CDate(DirectCast(CType(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).FindControl("hdnToTime"), HiddenField).Value)
                End If

                'Dim PermissionOption As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("PermissionOption").Text)
                Dim days As String = DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Days").ToString()
                'Dim IsFlexible As Boolean = CBool(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("IsFlexible").Text)
                Dim IsFullyDay As Boolean = CBool(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("IsFullDay").ToString())
                Dim AttachedFile As String = DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("AttachedFile").ToString()
                'Dim FlexibilePermissionDuration As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("FlexibilePermissionDuration").Text)

                If ((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("IsFlexible").ToString()) <> "") Then
                    If (DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("IsFlexible").ToString()) = False Then
                        IsFlexible = False
                    ElseIf (DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("IsFlexible").ToString()) = True Then
                        IsFlexible = True
                    End If
                    'IsFlexible = CBool(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("IsFlexible").Text)
                Else
                    IsFlexible = Nothing
                End If
                If ((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FlexibilePermissionDuration").ToString()) <> "") Then
                    FlexibilePermissionDuration = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FlexibilePermissionDuration").ToString())
                End If


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
                If IsFlexible Then
                    If (objEmp_PermissionsRequest.ValidateEmployeeFlexiblePermission(SessionVariables.LoginUser.FK_EmployeeId, PermissionId, New Data.DataTable, IsForPeriod,
                                                                   PermDate, ToDate, PermDate, objPermissionsTypes, PermTypeId,
                                                                   IsFullyDay, ErrorMessage, OffAndHolidayDays, FlexibilePermissionDuration, EmpLeaveTotalBalance) = False) Then
                        If (ErrorMessage <> String.Empty) Then
                            CtlCommon.ShowMessage(Me.Page, ErrorMessage)
                            Return
                        End If

                    Else
                        objEmp_Permissions.AddPermAllProcess(EmployeeId, PermTypeId, False, IsFlexible, False, Remarks, FromDate,
                                                             ToDate, Not IsForPeriod, FromTime, ToTime, PermDate, PermissionDaysCount, OffAndHolidayDays,
                                                             days, EmpLeaveTotalBalance, PermissionId, 1, FlexibilePermissionDuration, AttachedFile, IsFullyDay, Nothing, ErrorMessage)

                    End If
                Else
                    If (objEmp_Permissions.ValidateEmployeePermission(EmployeeId, PermissionId, New Data.DataTable, Not IsForPeriod, FromDate, ToDate, FromTime,
                                                                      ToTime, PermDate, objPermissionsTypes, PermTypeId, IsFullyDay, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
                        If (ErrorMessage <> String.Empty) Then
                            CtlCommon.ShowMessage(Me.Page, ErrorMessage)
                            Return
                        End If

                    Else
                        objEmp_Permissions.AddPermAllProcess(EmployeeId, PermTypeId, False, False, False, Remarks, FromDate,
                                                             ToDate, Not IsForPeriod, FromTime, ToTime, PermDate, PermissionDaysCount, OffAndHolidayDays, days,
                                                             EmpLeaveTotalBalance, PermissionId, 1, 0, AttachedFile, IsFullyDay, Nothing, ErrorMessage)
                    End If
                End If

                With objEmp_PermissionsRequest
                    .FK_StatusId = NextApprovalStatus
                    .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    .PermissionId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionId").ToString())
                    Err = .UpdatePermissionStatus()
                    strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                    If (File.Exists(Server.MapPath("~\PermissionRequestFiles\\" + PermissionRequestId.ToString() + AttachedFile))) Then

                        Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("~\PermissionRequestFiles\\" + PermissionRequestId.ToString() + AttachedFile))
                        CopyFile.CopyTo(Server.MapPath("~\PermissionFiles\\" + PermissionRequestId.ToString() + AttachedFile))

                        Rename(Server.MapPath("~\PermissionFiles\\" + PermissionRequestId.ToString() + AttachedFile),
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
            ElseIf (PermissionApproval = 3 Or PermissionApproval = 4 Or (PermissionApproval = 1 And NextApprovalStatus = 9)) Then
                With objEmp_PermissionsRequest
                    .FK_StatusId = NextApprovalStatus
                    .PermissionId = CStr(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionId").ToString())
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
        With objEmp_PermissionsRequest
            .FK_StatusId = RequestStatus.RejectedByDM
            .RejectedReason = txtRejectedReason.Text.Trim(",")
            .PermissionId = PermissionId
            '.PermissionId = CStr(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("PermissionId").Text)
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

    Protected Sub dgrdEmpPermissionRequest_OnItemCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles dgrdEmpPermissionRequest.ItemCommand
        If (e.CommandName = "reject") Then
            txtRejectedReason.Text = String.Empty
            PermissionId = e.CommandArgument
            mpeRejectPopupPermission.Show()

        ElseIf e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdEmpPermissionRequest_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdEmpPermissionRequest.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            Dim PermDateMonth As Integer = 0

            If Lang = CtlCommon.Lang.AR Then
                item("StatusName").Text = DirectCast(item.FindControl("hdnStatusNameAr"), HiddenField).Value
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("PermDate").ToString())) And (Not item.GetDataKeyValue("PermDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("PermDate").ToString()
                'item("PermDate").Text = fromDate.ToShortDateString()
                item("PermDate").Text = Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy")
                PermDateMonth = fromDate.Month
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("PermEndDate").ToString())) And (Not item.GetDataKeyValue("PermEndDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("PermEndDate").ToString()
                'item("PermEndDate").Text = fromDate.ToShortDateString()
                item("PermEndDate").Text = Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy")
            End If

            Dim PermissionId As Integer = item.GetDataKeyValue("PermissionId").ToString()
            Dim AttachedFile As String = item.GetDataKeyValue("AttachedFile").ToString()

            If AttachedFile = "" Or AttachedFile = "&nbsp;" Then
                DirectCast(e.Item.FindControl("lnbView"), LinkButton).Visible = False
            Else
                DirectCast(e.Item.FindControl("lnbView"), LinkButton).Visible = True
            End If

            Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            scriptManager__1.RegisterPostBackControl(DirectCast(e.Item.FindControl("lnbView"), LinkButton))



            Dim PermEndDate As DateTime
            Dim PermDate As DateTime = Convert.ToDateTime(item.GetDataKeyValue("PermDate").ToString())
            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("PermEndDate").ToString())) And (Not item.GetDataKeyValue("PermEndDate").ToString() = "")) Then
                PermEndDate = Convert.ToDateTime(item.GetDataKeyValue("PermEndDate").ToString())
            Else
                PermEndDate = Nothing
            End If

            Dim IsForPeriod As Boolean = CBool(item.GetDataKeyValue("IsForPeriod").ToString())

            Dim permTypeId As Integer = Convert.ToInt32(item.GetDataKeyValue("PermTypeId").ToString())

            Dim empId As Integer = Convert.ToInt32(item.GetDataKeyValue("FK_EmployeeId").ToString())

            objEmp_Permissions = New Emp_Permissions()

            DirectCast(e.Item.FindControl("lblRemainingBalance"), Label).Text = (objEmp_Permissions.GetPermissionRemainingBalance(permTypeId, empId, Not IsForPeriod, PermDate, PermDate, PermEndDate)).ToString()

            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
                item("PermName").Text = DirectCast(item.FindControl("hdnPermissionTypeAr"), HiddenField).Value
                item("StatusName").Text = DirectCast(item.FindControl("hdnStatusNameAr"), HiddenField).Value
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromTime").ToString())) And (Not item.GetDataKeyValue("FromTime").ToString() = "") And
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


                CType(item.FindControl("hdnFromTime"), HiddenField).Value = item.GetDataKeyValue("FromTime").ToString()
                CType(item.FindControl("hdnToTime"), HiddenField).Value = item.GetDataKeyValue("ToTime").ToString()

            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("IsFullDay").ToString())) And (Not item.GetDataKeyValue("IsFullDay").ToString() = "")) Then
                Dim IsFullDay As Boolean = Convert.ToBoolean(item.GetDataKeyValue("IsFullDay").ToString())
                If IsFullDay Then
                    item("FromTime").Text = String.Empty
                    item("ToTime").Text = String.Empty
                    item("TotalTime").Text = IIf(Lang = CtlCommon.Lang.EN, " Full Day", "يوم عمل كامل ")
                End If
            End If
            Dim strIsFlexible As String = item.GetDataKeyValue("IsFlexible").ToString()
            Dim strFlexibleDuration As String = item.GetDataKeyValue("FlexibilePermissionDuration").ToString()

            If strIsFlexible = "True" Then
                item("FromTime").Text = String.Empty
                item("ToTime").Text = String.Empty
                If Not String.IsNullOrEmpty(strFlexibleDuration) Then
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

                    'Dim timeElapsed As String = CType(strHours, String) & ":" & CType(strMinutes, String)
                    'item("IsFlexible").Text = "(" + ResourceManager.GetString("Yes", CultureInfo) + ")" + "(" + timeElapsed + ")"
                    Dim timeElapsed As String = CType(strHours, String) & ":" & CType(strMinutes, String)
                    strtimeelapsed = timeElapsed
                    item("IsFlexible").Text = "(" + ResourceManager.GetString("Yes", CultureInfo) + ")" + "(" + timeElapsed + ")"

                End If
            ElseIf strIsFlexible = "False" Then
                item("IsFlexible").Text = "(" + ResourceManager.GetString("No", CultureInfo) + ")"
            End If

        End If
    End Sub

    Protected Sub dgrdEmpHR_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmpPermissionRequest.NeedDataSource

        objEmp_PermissionsRequest = New Emp_PermissionsRequest()
        objEmp_PermissionsRequest.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
        objEmp_PermissionsRequest.FK_StatusId = RequestStatus.Pending
        dtCurrent = objEmp_PermissionsRequest.GetByDirectManager()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdEmpPermissionRequest.DataSource = dv

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

    Protected Sub checkAll_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

#End Region

#Region "Methods"

    Public Sub FillGridView()
        Try
            Dim dtPermissionRequest As New DataTable
            objEmp_PermissionsRequest = New Emp_PermissionsRequest()
            objEmp_PermissionsRequest.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            objEmp_PermissionsRequest.FK_StatusId = RequestStatus.Pending
            dtPermissionRequest = objEmp_PermissionsRequest.GetByDirectManager()
            dgrdEmpPermissionRequest.DataSource = dtPermissionRequest
            dgrdEmpPermissionRequest.DataBind()

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

    Private Function GetRemainingBalance(ByVal permTypeId As Integer, ByVal EmpIdParam As Integer, ByVal IsOneDay As Boolean,
                                                 ByVal PermDate As DateTime, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal FromTime As DateTime,
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

    Private Function CheckPermissionTypeDuration(ByVal permTypeId As Integer, ByVal EmpIdParam As Integer, ByVal IsOneDay As Boolean,
                                                 ByVal PermDate As DateTime, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal FromTime As DateTime,
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
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmpPermissionRequest.Skin))
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

        For Each item As GridDataItem In dgrdEmpPermissionRequest.Items
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
        Dim IsFlexible As Boolean
        Dim FlexibilePermissionDuration As Integer = 0

        Dim err2 As Integer

        Dim PermTypeId As Integer = CInt(item.GetDataKeyValue("PermTypeId").ToString())

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

            Dim NextApprovalStatus As Integer = CInt(item.GetDataKeyValue("NextApprovalStatus").ToString())

            If (PermissionApproval = 1 And NextApprovalStatus = 2) Then
                Dim FromDate As DateTime = CDate(item.GetDataKeyValue("PermDate").ToString())
                Dim ToDate As DateTime
                If ((item.GetDataKeyValue("PermEndDate").ToString()) <> "") Then
                    ToDate = CDate(item.GetDataKeyValue("PermEndDate").ToString())
                Else
                    ToDate = Nothing
                End If

                Dim IsForPeriod As Boolean = CBool(item.GetDataKeyValue("IsForPeriod").ToString())
                Dim EmployeeId As Integer = CInt(item.GetDataKeyValue("FK_EmployeeId").ToString())
                Dim PermissionId As Integer = CInt(item.GetDataKeyValue("PermissionId").ToString())
                Dim PermissionRequestId As Integer = CInt(item.GetDataKeyValue("PermissionId").ToString())
                'Dim PermTypeId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("PermTypeId").Text)
                Dim PermDate As DateTime = CDate(item.GetDataKeyValue("PermDate").ToString())
                Dim FromTime As DateTime
                Dim ToTime As DateTime


                If (Not String.IsNullOrEmpty((item.GetDataKeyValue("FromTime").ToString()))) AndAlso
                    (Not String.IsNullOrEmpty((item.GetDataKeyValue("ToTime").ToString()))) Then
                    FromTime = CDate(item.GetDataKeyValue("FromTime").ToString())
                    ToTime = CDate(item.GetDataKeyValue("ToTime").ToString())
                Else
                    'FromTime = CDate(DirectCast(CType(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).FindControl("hdnFromTime"), HiddenField).Value)
                    'ToTime = CDate(DirectCast(CType(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).FindControl("hdnToTime"), HiddenField).Value)
                    FromTime = CType(item.FindControl("hdnFromTime"), HiddenField).Value
                    ToTime = CType(item.FindControl("hdnToTime"), HiddenField).Value


                End If
                Dim days As String = ""
                'Dim PermissionOption As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("PermissionOption").Text)
                If Not item.GetDataKeyValue("Days").ToString() Is Nothing Then
                    days = item.GetDataKeyValue("Days").ToString()
                End If

                'Dim IsFlexible As Boolean = CBool(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("IsFlexible").Text)
                Dim IsFullyDay As Boolean = CBool(item.GetDataKeyValue("IsFullDay").ToString())
                Dim AttachedFile As String = item.GetDataKeyValue("AttachedFile").ToString()
                'Dim FlexibilePermissionDuration As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("FlexibilePermissionDuration").Text)

                If ((item.GetDataKeyValue("IsFlexible").ToString()) <> "") Then
                    If (item.GetDataKeyValue("IsFlexible").ToString()) = False Then
                        IsFlexible = False
                    ElseIf (item.GetDataKeyValue("IsFlexible").ToString()) = True Then
                        IsFlexible = True
                    End If
                    'IsFlexible = CBool(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("IsFlexible").Text)
                Else
                    IsFlexible = Nothing
                End If
                If ((item.GetDataKeyValue("FlexibilePermissionDuration").ToString()) <> "") Then
                    FlexibilePermissionDuration = CInt(item.GetDataKeyValue("FlexibilePermissionDuration").ToString())
                End If


                FromTime = PermDate.AddHours(FromTime.Hour).AddMinutes(FromTime.Minute)
                ToTime = PermDate.AddHours(ToTime.Hour).AddMinutes(ToTime.Minute)

                Dim Remarks As String = String.Empty
                If (item.GetDataKeyValue("Remark").ToString() <> "") Then
                    Remarks = CStr(item.GetDataKeyValue("Remark").ToString())
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
                If IsFlexible Then
                    If (objEmp_PermissionsRequest.ValidateEmployeeFlexiblePermission(SessionVariables.LoginUser.FK_EmployeeId, PermissionId, New Data.DataTable, IsForPeriod,
                                                                   PermDate, ToDate, PermDate, objPermissionsTypes, PermTypeId,
                                                                   IsFullyDay, ErrorMessage, OffAndHolidayDays, FlexibilePermissionDuration, EmpLeaveTotalBalance) = False) Then
                        If (ErrorMessage <> String.Empty) Then
                            CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                            Msg = ErrorMessage
                            Return
                        End If

                    Else
                        objEmp_Permissions.AddPermAllProcess(EmployeeId, PermTypeId, False, IsFlexible, False, Remarks, FromDate,
                                                             ToDate, Not IsForPeriod, FromTime, ToTime, PermDate, PermissionDaysCount, OffAndHolidayDays,
                                                             days, EmpLeaveTotalBalance, PermissionId, 1, FlexibilePermissionDuration, AttachedFile, IsFullyDay, Nothing, ErrorMessage)

                    End If
                Else
                    If (objEmp_Permissions.ValidateEmployeePermission(EmployeeId, PermissionId, New Data.DataTable, Not IsForPeriod, FromDate, ToDate, FromTime,
                                                                      ToTime, PermDate, objPermissionsTypes, PermTypeId, IsFullyDay, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
                        If (ErrorMessage <> String.Empty) Then
                            CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                            Msg = ErrorMessage
                            Return
                        End If

                    Else
                        objEmp_Permissions.AddPermAllProcess(EmployeeId, PermTypeId, False, False, False, Remarks, FromDate,
                                                             ToDate, Not IsForPeriod, FromTime, ToTime, PermDate, PermissionDaysCount, OffAndHolidayDays, days,
                                                             EmpLeaveTotalBalance, PermissionId, 1, 0, AttachedFile, IsFullyDay, Nothing, ErrorMessage)
                    End If
                End If

                With objEmp_PermissionsRequest
                    .FK_StatusId = NextApprovalStatus
                    .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    .PermissionId = CInt(item.GetDataKeyValue("PermissionId").ToString())
                    Err = .UpdatePermissionStatus()
                    strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                    If (File.Exists(Server.MapPath("~\PermissionRequestFiles\\" + PermissionRequestId.ToString() + AttachedFile))) Then

                        Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("~\PermissionRequestFiles\\" + PermissionRequestId.ToString() + AttachedFile))
                        CopyFile.CopyTo(Server.MapPath("~\PermissionFiles\\" + PermissionRequestId.ToString() + AttachedFile))

                        Rename(Server.MapPath("~\PermissionFiles\\" + PermissionRequestId.ToString() + AttachedFile),
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
            ElseIf (PermissionApproval = 3 Or PermissionApproval = 4 Or (PermissionApproval = 1 And NextApprovalStatus = 9)) Then
                With objEmp_PermissionsRequest
                    .FK_StatusId = NextApprovalStatus
                    .PermissionId = CStr(item.GetDataKeyValue("PermissionId").ToString())
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
            ErrNo = Err
            Msg = strMessage
            item = Nothing
        End With
    End Sub

    Private Sub RejectAll(ByVal item As GridDataItem, ByRef ErrNo As Integer, ByRef Msg As String)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        objAPP_Settings = New APP_Settings
        With objEmp_PermissionsRequest
            .FK_StatusId = RequestStatus.RejectedByDM
            .RejectedReason = txtRejectAllReason.Text.Trim(",")
            .PermissionId = CInt(item.GetDataKeyValue("PermissionId").ToString())
            '.PermissionId = CStr(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("PermissionId").Text)
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
