Imports SmartV.UTILITIES
Imports TA.Employees
Imports TA.Admin
Imports Telerik.Web.UI
Imports TA.Definitions
Imports TA.SelfServices
Imports System.IO
Imports System.Data
Imports TA.DailyTasks

Partial Class Requests_UserControls_HR_HR_StudyPermissionApproval
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private objEmp_Study_PermissionRequest As New Emp_Study_PermissionRequest
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
    Private objEmp_WorkSchedule As Emp_WorkSchedule
    Private objWorkSchedule As WorkSchedule
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

    Public Property StudyPermissionApproval() As Integer
        Get
            Return ViewState("StudyPermissionApproval")
        End Get
        Set(ByVal value As Integer)
            ViewState("StudyPermissionApproval") = value
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
            lblStudyPermissionRequests.Text = ResourceManager.GetString("HRStudyPermitApprove", CultureInfo)
            Page.Title = "Work Force Pro : :" + ResourceManager.GetString("HRStudyPermitApprove", CultureInfo)
            ShowHideControls()
            FillGridView()
        End If
    End Sub

    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        Dim PermissionOption As Integer = 0
        Dim IsFlexible As Boolean
        Dim IsFullyDay As Boolean
        Dim FlexibilePermissionDuration As Integer = 0

        objRECALC_REQUEST = New RECALC_REQUEST
        Dim temp_date As Date
        Dim temp_str_date As String
        Dim err2 As Integer

        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            If (.StudyPermissionApproval = 2 Or .StudyPermissionApproval = 3) Then
                Dim FromDate As DateTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermDate"))
                Dim ToDate As DateTime
                If ((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermEndDate").ToString()) <> "") Then
                    ToDate = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermEndDate"))
                Else
                    ToDate = Nothing
                End If
                If ((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionOption").ToString()) <> "") Then
                    PermissionOption = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionOption"))
                End If
                Dim IsForPeriod As Boolean = CBool(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("IsForPeriod"))
                Dim EmployeeId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_EmployeeId"))
                Dim PermissionId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionRequestId"))
                'Dim PermTypeId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("PermTypeId").Text)
                Dim PermDate As DateTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermDate"))

                Dim FromTime As DateTime
                Dim ToTime As DateTime

                If (Not String.IsNullOrEmpty((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FromTime").ToString()))) AndAlso
                    (Not String.IsNullOrEmpty((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("ToTime").ToString()))) Then
                    FromTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FromTime"))
                    ToTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("ToTime"))
                Else
                    ' FromTime = CDate(DirectCast(CType(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).FindControl("hdnFromTime"), HiddenField).Value)
                    ' ToTime = CDate(DirectCast(CType(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).FindControl("hdnToTime"), HiddenField).Value)
                    FromTime = Nothing
                    ToTime = Nothing
                End If

                Dim days As String = DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Days").ToString()
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

                If ((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("IsFullDay").ToString()) <> "") Then
                    IsFullyDay = CBool(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("IsFullDay"))
                Else
                    IsFullyDay = Nothing
                End If

                Dim AttachedFile As String = DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("AttachedFile").ToString()
                If ((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FlexibilePermissionDuration").ToString()) <> "") Then
                    FlexibilePermissionDuration = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FlexibilePermissionDuration"))
                End If



                FromTime = PermDate.AddHours(FromTime.Hour).AddMinutes(FromTime.Minute)
                ToTime = PermDate.AddHours(ToTime.Hour).AddMinutes(ToTime.Minute)

                Dim Remarks As String = String.Empty
                If (DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Remark").ToString() <> "") Then
                    Remarks = CStr(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Remark").ToString())
                End If

                Dim StudyYear As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("StudyYear"))
                Dim Semester As String = DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Semester").ToString()
                Dim FK_UniversityId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_UniversityId"))
                Dim Emp_GPAType As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Emp_GPAType"))
                Dim Emp_GPA As Decimal = CDec(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Emp_GPA"))
                Dim FK_MajorId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_MajorId"))
                Dim FK_SpecializationId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_SpecializationId"))

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

                'If (objEmp_Permissions.ValidateEmployeePermission(EmployeeId, PermissionId, New Data.DataTable, Not IsForPeriod, FromDate, ToDate, FromTime, _
                '                                                  ToTime, PermDate, objPermissionsTypes, PermTypeId, IsFullyDay, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
                '    If (ErrorMessage <> String.Empty) Then
                '        CtlCommon.ShowMessage(Me.Page, ErrorMessage)
                '        Return
                '    End If

                'Else
                objEmp_Permissions.AddPermAllProcess(EmployeeId, -1, False, IsFlexible, True, Remarks, FromDate,
                                                     ToDate, Not IsForPeriod, FromTime, ToTime, PermDate, PermissionDaysCount, OffAndHolidayDays,
                                                     days, EmpLeaveTotalBalance, PermissionId, 3, FlexibilePermissionDuration, AttachedFile, IsFullyDay, Nothing, ErrorMessage, StudyYear, Semester,
                                                     FK_UniversityId, Emp_GPAType, Emp_GPA, FK_MajorId, FK_SpecializationId)
                'End If

                With objEmp_Study_PermissionRequest
                    .PermissionRequestId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionRequestId"))
                    .GetByPK()
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    .FK_HREmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                    .FK_StatusId = RequestStatus.ApprovedByHR
                    Err = .UpdatePermissionStatus()

                    strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
                    If (File.Exists(Server.MapPath("~\StudyPermissionRequestFiles\\" + PermissionId.ToString() + AttachedFile))) Then

                        Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("~\StudyPermissionRequestFiles\\" + PermissionId.ToString() + AttachedFile))
                        CopyFile.CopyTo(Server.MapPath("~\PermissionFiles\\" + PermissionId.ToString() + AttachedFile))

                        Rename(Server.MapPath("~\PermissionFiles\\" + PermissionId.ToString() + AttachedFile),
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

                        If Not objAPP_Settings.StudyPermissionSchedule = Nothing Then
                            objEmp_WorkSchedule = New Emp_WorkSchedule
                            objWorkSchedule = New WorkSchedule

                            objWorkSchedule.ScheduleId = objAPP_Settings.StudyPermissionSchedule
                            objWorkSchedule.GetByPK()

                            With objEmp_WorkSchedule
                                .FK_EmployeeId = EmployeeId
                                .FK_ScheduleId = objAPP_Settings.StudyPermissionSchedule
                                .ScheduleType = objWorkSchedule.ScheduleType
                                .FromDate = PermDate
                                .ToDate = ToDate
                                .IsTemporary = True
                                .Add()
                            End With
                        End If


                        FillGridView()

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
                                Dim dteFrom As DateTime = FromDate
                                Dim dteTo As DateTime = ToDate

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
                                    .Remarks = "Study Permission Request Approval - SYSTEM"
                                    err2 = .Add
                                End With
                            End If
                        End If

                        CtlCommon.ShowMessage(Me.Page, strMessage, "success")
                    Else
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
                    End If
                End With
            ElseIf .StudyPermissionApproval = 4 Then
                With objEmp_Study_PermissionRequest
                    .PermissionRequestId = CStr(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionRequestId"))
                    .GetByPK()
                    .FK_StatusId = RequestStatus.ApprovedByHR
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    .FK_HREmployeeId = SessionVariables.LoginUser.FK_EmployeeId
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
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        objAPP_Settings = New APP_Settings
        With objEmp_Study_PermissionRequest
            .PermissionRequestId = PermissionId
            .GetByPK()
            .FK_StatusId = RequestStatus.RejectedByHR
            .RejectionReason = txtRejectedReason.Text.Trim(",")
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .FK_HREmployeeId = SessionVariables.LoginUser.FK_EmployeeId
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
            mpeRejectPopupStudy.Show()
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

            If Not item.GetDataKeyValue("DaysName") Is Nothing Then
                If Lang = CtlCommon.Lang.AR Then

                    item("DaysName").Text = item.GetDataKeyValue("DaysArabicName")
                Else
                    item("DaysName").Text = item.GetDataKeyValue("DaysName")
                End If
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

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("IsFullDay").ToString())) And (Not item.GetDataKeyValue("IsFullDay").ToString() = "")) Then
                Dim IsFullDay As Boolean = Convert.ToBoolean(item.GetDataKeyValue("IsFullDay"))
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

                    Dim timeElapsed As String = CType(strHours, String) & ":" & CType(strMinutes, String)
                    strtimeelapsed = timeElapsed
                    item("IsFlexible").Text = "(" + ResourceManager.GetString("Yes", CultureInfo) + ")" + "(" + timeElapsed + ")"
                End If
            ElseIf strIsFlexible = "False" Then
                item("IsFlexible").Text = "(" + ResourceManager.GetString("No", CultureInfo) + ")"
            End If

        End If
    End Sub

    Protected Sub dgrdEmpLeaveRequest_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmpLeaveRequest.NeedDataSource

        objEmp_Study_PermissionRequest = New Emp_Study_PermissionRequest()
        objEmp_Study_PermissionRequest.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
        objEmp_Study_PermissionRequest.FK_StatusId = RequestStatus.Pending
        dtCurrent = objEmp_Study_PermissionRequest.GetByHR()
        dgrdEmpLeaveRequest.DataSource = dtCurrent
        If Not (dtCurrent Is Nothing) Then
            Dim dv As New DataView(dtCurrent)
            dv.Sort = SortExepression
            dgrdEmpLeaveRequest.DataSource = dv
        End If
    End Sub

    Protected Sub lnkDownloadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        objEmp_Study_PermissionRequest = New Emp_Study_PermissionRequest
        With objEmp_Study_PermissionRequest
            .PermissionRequestId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionRequestId").ToString())
            .GetByPK()
            Dim FileName As String = .PermissionRequestId.ToString() + .AttachedFile
            Dim FilePath As String = Server.MapPath("..\\StudyPermissionRequestFiles\") & FileName
            CtlCommon.Open_Download_File(FileName, FilePath)

        End With
    End Sub

#End Region

#Region "Methods"

    Public Sub FillGridView()
        Try
            Dim dtPermissionRequet As New DataTable
            objEmp_Study_PermissionRequest = New Emp_Study_PermissionRequest()
            objEmp_Study_PermissionRequest.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            objAPP_Settings = New APP_Settings()
            objAPP_Settings = objAPP_Settings.GetByPK()
            If Not objAPP_Settings.LeaveApprovalfromLeave = True Then
                StudyPermissionApproval = objAPP_Settings.StudyPermissionApproval
                objEmp_Study_PermissionRequest.FK_StatusId = StudyPermissionApproval
            End If

            'dgrdEmpLeaveRequest.DataSource = objEmp_PermissionsRequest.GetByDirectManager()
            dtPermissionRequet = objEmp_Study_PermissionRequest.GetByHR()
            dgrdEmpLeaveRequest.DataSource = dtPermissionRequet
            dgrdEmpLeaveRequest.DataBind()

            If Not dtPermissionRequet Is Nothing Then
                If dtPermissionRequet.Rows.Count > 0 Then
                    If Not lblRequestNo.Text.Contains(":") Then
                        lblRequestNo.Text += " : " + (dtPermissionRequet.Rows.Count).ToString()
                    Else
                        Dim strArr() As String = lblRequestNo.Text.Split(":")
                        lblRequestNo.Text = strArr(0) + " : " + (dtPermissionRequet.Rows.Count).ToString()
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
        objEmp_Study_PermissionRequest = New Emp_Study_PermissionRequest()
        objPermissionsTypes = New PermissionsTypes()

        Dim empId As Integer = EmpIdParam
        Dim permId As Integer = permTypeId
        Dim remainingBalanceCount As Integer = 0
        Dim monthlyBalance As Integer = 0

        If (Not permTypeId = -1) Then
            objEmp_Study_PermissionRequest.FK_EmployeeId = empId
            objEmp_Study_PermissionRequest.FK_PermId = permId

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
        Dim PermissionOption As Integer = 0
        Dim IsFlexible As Boolean
        Dim IsFullyDay As Boolean
        Dim FlexibilePermissionDuration As Integer = 0

        objRECALC_REQUEST = New RECALC_REQUEST
        Dim temp_date As Date
        Dim temp_str_date As String
        Dim err2 As Integer

        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            If (.StudyPermissionApproval = 2 Or .StudyPermissionApproval = 3) Then
                Dim FromDate As DateTime = CDate(item.GetDataKeyValue("PermDate"))
                Dim ToDate As DateTime
                If ((item.GetDataKeyValue("PermEndDate").ToString()) <> "") Then
                    ToDate = CDate(item.GetDataKeyValue("PermEndDate"))
                Else
                    ToDate = Nothing
                End If
                If ((item.GetDataKeyValue("PermissionOption").ToString()) <> "") Then
                    PermissionOption = CInt(item.GetDataKeyValue("PermissionOption"))
                End If
                Dim IsForPeriod As Boolean = CBool(item.GetDataKeyValue("IsForPeriod"))
                Dim EmployeeId As Integer = CInt(item.GetDataKeyValue("FK_EmployeeId"))
                Dim PermissionId As Integer = CInt(item.GetDataKeyValue("PermissionRequestId"))
                'Dim PermTypeId As Integer = CInt(item("PermTypeId").Text)
                Dim PermDate As DateTime = CDate(item.GetDataKeyValue("PermDate"))

                Dim FromTime As DateTime
                Dim ToTime As DateTime

                If (Not String.IsNullOrEmpty((item.GetDataKeyValue("FromTime").ToString()))) AndAlso
                    (Not String.IsNullOrEmpty((item.GetDataKeyValue("ToTime").ToString()))) Then
                    FromTime = CDate(item.GetDataKeyValue("FromTime"))
                    ToTime = CDate(item.GetDataKeyValue("ToTime"))
                Else
                    ' FromTime = CDate(DirectCast(CType(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).FindControl("hdnFromTime"), HiddenField).Value)
                    ' ToTime = CDate(DirectCast(CType(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).FindControl("hdnToTime"), HiddenField).Value)
                    FromTime = Nothing
                    ToTime = Nothing
                End If

                Dim days As String = item.GetDataKeyValue("Days").ToString()
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

                If ((item.GetDataKeyValue("IsFullDay").ToString()) <> "") Then
                    IsFullyDay = CBool(item.GetDataKeyValue("IsFullDay"))
                Else
                    IsFullyDay = Nothing
                End If

                Dim AttachedFile As String = item.GetDataKeyValue("AttachedFile").ToString()
                If ((item.GetDataKeyValue("FlexibilePermissionDuration").ToString()) <> "") Then
                    FlexibilePermissionDuration = CInt(item.GetDataKeyValue("FlexibilePermissionDuration"))
                End If



                FromTime = PermDate.AddHours(FromTime.Hour).AddMinutes(FromTime.Minute)
                ToTime = PermDate.AddHours(ToTime.Hour).AddMinutes(ToTime.Minute)

                Dim Remarks As String = String.Empty
                If (item.GetDataKeyValue("Remark").ToString() <> "") Then
                    Remarks = CStr(item.GetDataKeyValue("Remark").ToString())
                End If

                Dim StudyYear As Integer = CInt(item.GetDataKeyValue("StudyYear").ToString())
                Dim Semester As String = item.GetDataKeyValue("StudyYear").ToString()
                Dim FK_UniversityId As Integer = CInt(item.GetDataKeyValue("FK_UniversityId").ToString())
                Dim Emp_GPAType As Integer = CInt(item.GetDataKeyValue("Emp_GPAType").ToString())
                Dim Emp_GPA As Decimal = CDec(item.GetDataKeyValue("Emp_GPA").ToString())
                Dim FK_MajorId As Integer = CInt(item.GetDataKeyValue("FK_MajorId").ToString())
                Dim FK_SpecializationId As Integer = CInt(item.GetDataKeyValue("FK_SpecializationId").ToString())

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

                'If (objEmp_Permissions.ValidateEmployeePermission(EmployeeId, PermissionId, New Data.DataTable, Not IsForPeriod, FromDate, ToDate, FromTime, _
                '                                                  ToTime, PermDate, objPermissionsTypes, PermTypeId, IsFullyDay, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
                '    If (ErrorMessage <> String.Empty) Then
                '        CtlCommon.ShowMessage(Me.Page, ErrorMessage)
                '        Return
                '    End If

                'Else
                objEmp_Permissions.AddPermAllProcess(EmployeeId, -1, False, IsFlexible, False, Remarks, FromDate,
                                                     ToDate, Not IsForPeriod, FromTime, ToTime, PermDate, PermissionDaysCount, OffAndHolidayDays,
                                                     days, EmpLeaveTotalBalance, PermissionId, 3, FlexibilePermissionDuration, AttachedFile, IsFullyDay, Nothing, ErrorMessage, StudyYear, Semester,
                                                     FK_UniversityId, Emp_GPAType, Emp_GPA, FK_MajorId, FK_SpecializationId)
                'End If

                With objEmp_Study_PermissionRequest
                    .PermissionRequestId = CInt(item.GetDataKeyValue("PermissionRequestId"))
                    .GetByPK()
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    .FK_HREmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                    .FK_StatusId = RequestStatus.ApprovedByHR
                    Err = .UpdatePermissionStatus()

                    strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
                    If (File.Exists(Server.MapPath("~\StudyPermissionRequestFiles\\" + PermissionId.ToString() + AttachedFile))) Then

                        Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("~\StudyPermissionRequestFiles\\" + PermissionId.ToString() + AttachedFile))
                        CopyFile.CopyTo(Server.MapPath("~\PermissionFiles\\" + PermissionId.ToString() + AttachedFile))

                        Rename(Server.MapPath("~\PermissionFiles\\" + PermissionId.ToString() + AttachedFile),
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

                        If Not objAPP_Settings.StudyPermissionSchedule = Nothing Then
                            objEmp_WorkSchedule = New Emp_WorkSchedule
                            objWorkSchedule = New WorkSchedule

                            objWorkSchedule.ScheduleId = objAPP_Settings.StudyPermissionSchedule
                            objWorkSchedule.GetByPK()

                            With objEmp_WorkSchedule
                                .FK_EmployeeId = EmployeeId
                                .FK_ScheduleId = objAPP_Settings.StudyPermissionSchedule
                                .ScheduleType = objWorkSchedule.ScheduleType
                                .FromDate = PermDate
                                .ToDate = ToDate
                                .IsTemporary = True
                                .Add()
                            End With
                        End If


                        'FillGridView()

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
                                Dim dteFrom As DateTime = FromDate
                                Dim dteTo As DateTime = ToDate

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
                                    .Remarks = "Study Permission Request Approval - SYSTEM"
                                    err2 = .Add
                                End With
                            End If
                        End If
                    End If
                End With
            ElseIf .StudyPermissionApproval = 4 Then
                With objEmp_Study_PermissionRequest
                    .PermissionRequestId = CStr(item.GetDataKeyValue("PermissionRequestId"))
                    .GetByPK()
                    .FK_StatusId = RequestStatus.ApprovedByHR
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    .FK_HREmployeeId = SessionVariables.LoginUser.FK_EmployeeId
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
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        objAPP_Settings = New APP_Settings
        With objEmp_Study_PermissionRequest
            .PermissionRequestId = CStr(item.GetDataKeyValue("PermissionRequestId"))
            .GetByPK()
            .FK_StatusId = RequestStatus.RejectedByHR
            .RejectionReason = txtRejectAllReason.Text.Trim(",")
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .FK_HREmployeeId = SessionVariables.LoginUser.FK_EmployeeId
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
