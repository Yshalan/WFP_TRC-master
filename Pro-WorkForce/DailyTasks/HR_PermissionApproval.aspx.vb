Imports Telerik.Web.UI
Imports SmartV.UTILITIES
Imports TA.DailyTasks
Imports System.Data
Imports SmartV.UTILITIES.ProjectCommon
Imports System.IO
Imports TA.Employees
Imports TA.Definitions
Imports TA.Admin

Partial Class DailyTasks_HR_PermissionApproval
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objHR_PermissionRequest As HR_PermissionRequest
    Private objEmp_Permissions As Emp_Permissions
    Private objPermissionsTypes As PermissionsTypes
    Private objRECALC_REQUEST As RECALC_REQUEST
    Dim objAPP_Settings As APP_Settings
    Private objEmployee As Employee
    Dim objWeekDays As New WeekDays
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Public MsgLang As String
    Public strLang As String
    Dim babyBirthDate As DateTime
    Private objRecalculateRequest As RecalculateRequest
    Private objWorkSchedule As WorkSchedule

#End Region

#Region "Properties"

    Enum DisplayModeEnum
        Add
        Edit
        View
        ViewAll
        ViewAddEdit
    End Enum

    Public Enum PermissionOption
        Normal = 1
        Nursing = 2
        Study = 3
    End Enum

    Public Property PermissionRequestId() As Integer
        Get
            Return ViewState("PermissionRequestId")
        End Get
        Set(ByVal value As Integer)
            ViewState("PermissionRequestId") = value
        End Set
    End Property

    Public Property AllowedTime() As Integer
        Get
            Return ViewState("AllowedTime")
        End Get
        Set(ByVal value As Integer)
            ViewState("AllowedTime") = value
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

    Public Property EmpLeaveTotalBalance() As Double
        Get
            Return ViewState("EmpLeaveTotalBalance")
        End Get
        Set(ByVal value As Double)
            ViewState("EmpLeaveTotalBalance") = value
        End Set
    End Property

    Public Property OffAndHolidayDays() As Integer
        Get
            Return ViewState("OffAndHolidayDays")
        End Get
        Set(ByVal value As Integer)
            ViewState("OffAndHolidayDays") = value
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

    Public Property PermissionId() As Integer
        Get
            Return ViewState("PersmissionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("PersmissionId") = value
        End Set
    End Property

    Public Property DisplayMode() As DisplayModeEnum
        Get
            Return ViewState("DisplayMode")
        End Get
        Set(ByVal value As DisplayModeEnum)
            ViewState("DisplayMode") = value
        End Set
    End Property

    Public Property PermissionType() As PermissionOption
        Get
            Return ViewState("PermissionType")
        End Get
        Set(ByVal value As PermissionOption)
            ViewState("PermissionType") = value
        End Set
    End Property

    Public Property IsForPeriod() As Boolean
        Get
            Return ViewState("IsForPeriod")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsForPeriod") = value
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

    Public Property dtCurrent_Rejected() As DataTable
        Get
            Return ViewState("dtCurrent_Rejected")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent_Rejected") = value
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

    Public Property PermDate() As DateTime
        Get
            Return ViewState("PermDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("PermDate") = value
        End Set
    End Property

    Public Property PermEndDate() As DateTime
        Get
            Return ViewState("PermEndDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("PermEndDate") = value
        End Set
    End Property

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdVwEmpPermissions.Skin))
    End Function

    Protected Sub dgrdVwEmpPermissions_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdVwEmpPermissions.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        ElseIf e.CommandName = "reject" Then
            PermissionRequestId = Convert.ToInt32(DirectCast(e.Item, GridDataItem).GetDataKeyValue("PermissionRequestId").ToString())
            txtRejectedReason.Text = String.Empty
            mpeRejectPopupPermission.Show()

            'ElseIf (e.CommandName = "RowClick") Then
            '    PermissionRequestId = Convert.ToInt32(DirectCast(dgrdVwEmpPermissions.SelectedItems(0), GridDataItem).GetDataKeyValue("PermissionRequestId").ToString())
            '    'EnableControl(False)
            '    mvPermissionApproval.ActiveViewIndex = 1
            '    HR_PermissionRequest1.PermissionRequestId = PermissionRequestId
            '    HR_PermissionRequest1.FillControls()
        End If
    End Sub

    Protected Sub RadToolBar2_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon1() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdRejected.Skin))
    End Function

    Protected Sub dgrdRejected_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdRejected.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter2.FireApplyCommand()
        End If
    End Sub


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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            FillGridView()
            FillGridRejected()
            mvPermissionApproval.SetActiveView(vPermissions)
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("HRPermissionRequestApproval", CultureInfo)
        End If
    End Sub

    Protected Sub dgrdVwEmpPermissions_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdVwEmpPermissions.NeedDataSource
        Try
            objHR_PermissionRequest = New HR_PermissionRequest()
            objHR_PermissionRequest.UserId = SessionVariables.LoginUser.UsrID
            dtCurrent = objHR_PermissionRequest.HR_Permissions_GetAllByStatus
            dgrdVwEmpPermissions.DataSource = dtCurrent
            dgrdVwEmpPermissions.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub dgrdVwEmpPermissions_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdVwEmpPermissions.ItemDataBound
        Try


            If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
                Dim item As GridDataItem
                item = e.Item

                If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromTime").ToString())) And (Not item.GetDataKeyValue("FromTime").ToString() = "")) Then
                    Dim fromTime As DateTime = item.GetDataKeyValue("FromTime").ToString()
                    item("FromTime").Text = fromTime.ToString("HH:mm")
                End If

                If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ToTime").ToString())) And (Not item.GetDataKeyValue("ToTime").ToString() = "")) Then
                    Dim toTime As DateTime = item.GetDataKeyValue("ToTime").ToString()
                    item("ToTime").Text = toTime.ToString("HH:mm")
                End If

                If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("PermDate").ToString())) And (Not item.GetDataKeyValue("PermDate").ToString() = "")) Then
                    Dim fromDate As DateTime = item.GetDataKeyValue("PermDate").ToString()
                    item("PermDate").Text = fromDate.ToShortDateString
                End If

                If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("PermEndDate").ToString())) And (Not item.GetDataKeyValue("PermEndDate").ToString() = "")) Then
                    Dim toDate As DateTime = item.GetDataKeyValue("PermEndDate").ToString()
                    item("PermEndDate").Text = toDate.ToShortDateString
                End If

                Dim PermId As Integer = item.GetDataKeyValue("PermissionRequestId")
                Dim AttachedFile As String = item.GetDataKeyValue("AttachedFile").ToString()

                If AttachedFile = "" Then
                    'DirectCast(e.Item.FindControl("lnbView"), HtmlAnchor).Visible = False
                    DirectCast(e.Item.FindControl("lnbView"), LinkButton).Visible = False
                Else
                    'DirectCast(e.Item.FindControl("lnbView"), HtmlAnchor).HRef = "~\HR_PermissionRequestFiles\" + PermId.ToString() + AttachedFile
                End If

                Dim strFullDay As String = item.GetDataKeyValue("IsFullDay").ToString()
                If strFullDay = "True" Then
                    item("FromTime").Text = String.Empty
                    item("ToTime").Text = String.Empty
                    item("IsFullDay").Text = ResourceManager.GetString("Yes", CultureInfo)
                ElseIf strFullDay = "False" Then
                    item("IsFullDay").Text = ResourceManager.GetString("No", CultureInfo)
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
                        item("IsFlexible").Text = "(" + ResourceManager.GetString("Yes", CultureInfo) + ")" + "(" + timeElapsed + ")"
                    End If
                ElseIf strIsFlexible = "False" Then
                    item("IsFlexible").Text = "(" + ResourceManager.GetString("No", CultureInfo) + ")"
                End If

                'item("CustomerID").Text = "Telerik"
                If Lang = CtlCommon.Lang.AR Then
                    item("PermName").Text = DirectCast(item.FindControl("hdnPermArabicType"), HiddenField).Value
                End If

                If Lang = CtlCommon.Lang.AR Then
                    item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub dgrdRejected_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdRejected.NeedDataSource
        Try
            objHR_PermissionRequest = New HR_PermissionRequest()
            objHR_PermissionRequest.UserId = SessionVariables.LoginUser.UsrID
            dtCurrent_Rejected = objHR_PermissionRequest.HR_Permissions_GetAllRejected
            dgrdRejected.DataSource = dtCurrent_Rejected
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub dgrdRejected_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdRejected.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromTime").ToString())) And (Not item.GetDataKeyValue("FromTime").ToString() = "")) Then
                Dim fromTime As DateTime = item.GetDataKeyValue("FromTime").ToString()
                item("FromTime").Text = fromTime.ToString("HH:mm")
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ToTime").ToString())) And (Not item.GetDataKeyValue("ToTime").ToString() = "")) Then
                Dim toTime As DateTime = item.GetDataKeyValue("ToTime").ToString()
                item("ToTime").Text = toTime.ToString("HH:mm")
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("PermDate").ToString())) And (Not item.GetDataKeyValue("PermDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("PermDate").ToString()
                item("PermDate").Text = fromDate.ToShortDateString
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("PermEndDate").ToString())) And (Not item.GetDataKeyValue("PermEndDate").ToString() = "")) Then
                Dim toDate As DateTime = item.GetDataKeyValue("PermEndDate").ToString()
                item("PermEndDate").Text = toDate.ToShortDateString
            End If

            Dim strFullDay As String = item.GetDataKeyValue("IsFullDay").ToString()
            If strFullDay = "True" Then
                item("FromTime").Text = String.Empty
                item("ToTime").Text = String.Empty
                item("IsFullDay").Text = ResourceManager.GetString("Yes", CultureInfo)
            ElseIf strFullDay = "False" Then
                item("IsFullDay").Text = ResourceManager.GetString("No", CultureInfo)
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
                    item("IsFlexible").Text = "(" + ResourceManager.GetString("Yes", CultureInfo) + ")" + "(" + timeElapsed + ")"
                End If
            ElseIf strIsFlexible = "False" Then
                item("IsFlexible").Text = "(" + ResourceManager.GetString("No", CultureInfo) + ")"
            End If

            'item("CustomerID").Text = "Telerik"
            If Lang = CtlCommon.Lang.AR Then
                item("PermName").Text = DirectCast(item.FindControl("hdnPermArabicType"), HiddenField).Value
            End If

            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
            End If

        End If
    End Sub

    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PermissionRequestId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionRequestId").ToString())
        AcceptPermissionRequest()
    End Sub

    Protected Sub btnReject_Click(sender As Object, e As System.EventArgs) Handles btnReject.Click
        RejectPermissionRequest()
    End Sub

    Protected Sub lnkDownloadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim MoveRequestId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermissionRequestId"))
        Dim AttachedFile As String = CStr(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("AttachedFile"))
        If AttachedFile = "" Then
            DirectCast(dgrdVwEmpPermissions.FindControl("lnbView"), LinkButton).Visible = False
        Else
            Open_Download_File(MoveRequestId.ToString() + AttachedFile)
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub RejectPermissionRequest()
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        objHR_PermissionRequest = New HR_PermissionRequest
        objHR_PermissionRequest.PermissionRequestId = PermissionRequestId
        objHR_PermissionRequest.IsRejected = True
        objHR_PermissionRequest.RejectionReason = txtRejectedReason.Text
        objHR_PermissionRequest.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
        Err = objHR_PermissionRequest.Update_HR_Permission_RequestStatus
        strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
        If Err = 0 Then
            mvPermissionApproval.ActiveViewIndex = 0
            FillGridView()
            FillGridRejected()
            CtlCommon.ShowMessage(Me.Page, strMessage, "success")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "info")
        End If
    End Sub

    Function AcceptPermissionRequest() As Integer
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim errNo As Integer
        objHR_PermissionRequest = New HR_PermissionRequest()
        objEmp_Permissions = New Emp_Permissions
        OffAndHolidayDays = 0
        EmpLeaveTotalBalance = 0
        Dim errorNum As Integer = -1
        Dim ErrorMessage As String = String.Empty
        Dim tpFromTime As DateTime?
        Dim tpToTime As DateTime?
        Dim isFlixible As Boolean
        Dim strFlexibileDuration As String
        Dim temp_date As Date
        Dim temp_str_date As String
        Dim err2 As Integer

        objHR_PermissionRequest = New HR_PermissionRequest
        objHR_PermissionRequest.PermissionRequestId = PermissionRequestId
        objHR_PermissionRequest.GetByPK()
        isFlixible = objHR_PermissionRequest.IsFlexible
        EmployeeId = objHR_PermissionRequest.FK_EmployeeId
        PermDate = objHR_PermissionRequest.PermDate
        PermEndDate = objHR_PermissionRequest.PermEndDate
        If ((objHR_PermissionRequest.PermissionOption = PermissionOption.Nursing)) Then
            tpFromTime = CType(Nothing, DateTime?)
            tpToTime = CType(Nothing, DateTime?)
            strFlexibileDuration = objHR_PermissionRequest.FlexibilePermissionDuration
            isFlixible = True
        Else
            If isFlixible = True Then
                tpFromTime = CType(Nothing, DateTime?)
                tpToTime = CType(Nothing, DateTime?)
                strFlexibileDuration = objHR_PermissionRequest.FlexibilePermissionDuration
            Else
                tpFromTime = objHR_PermissionRequest.FromTime
                tpToTime = objHR_PermissionRequest.ToTime
                strFlexibileDuration = CtlCommon.GetFullTimeString(objHR_PermissionRequest.FlexibilePermissionDuration)
            End If

        End If

        Dim IsOneDay As Boolean = False
        If objHR_PermissionRequest.IsForPeriod = False Then
            IsOneDay = True
            If objHR_PermissionRequest.PermEndDate Is Nothing Then
                objHR_PermissionRequest.PermEndDate = objHR_PermissionRequest.PermDate
            End If
        End If

        PermissionType = objHR_PermissionRequest.PermissionOption
        IsForPeriod = objHR_PermissionRequest.IsForPeriod

        If objHR_PermissionRequest.PermissionOption = PermissionOption.Normal Then
            If objHR_PermissionRequest.IsFlexible = False Then
                'If (objHR_PermissionRequest.ValidateEmployeePermission(objHR_PermissionRequest.FK_EmployeeId, PermissionRequestId, dtCurrent, objHR_PermissionRequest.IsForPeriod, objHR_PermissionRequest.PermDate, objHR_PermissionRequest.PermEndDate, tpFromTime, _
                '                                                 tpToTime, objHR_PermissionRequest.PermDate, objPermissionsTypes, objHR_PermissionRequest.FK_PermId, objHR_PermissionRequest.IsFullDay, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
                If (objHR_PermissionRequest.ValidateEmployeePermission(objHR_PermissionRequest.FK_EmployeeId, PermissionRequestId, dtCurrent, IsOneDay, objHR_PermissionRequest.PermDate, objHR_PermissionRequest.PermEndDate, tpFromTime, _
                                                                 tpToTime, objHR_PermissionRequest.PermDate, objPermissionsTypes, objHR_PermissionRequest.FK_PermId, objHR_PermissionRequest.IsFullDay, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
                    If (ErrorMessage <> String.Empty) Then
                        CtlCommon.ShowMessage(Me.Page, ErrorMessage)
                        Return -1
                    End If
                End If
            End If
        End If

        'If objHR_PermissionRequest.PermissionOption = PermissionOption.Normal Then
        '    If objHR_PermissionRequest.IsFlexible = False Then
        '        If (objEmp_Permissions.ValidateEmployeePermission(objHR_PermissionRequest.FK_EmployeeId, PermissionRequestId, dtCurrent, objHR_PermissionRequest.IsForPeriod, objHR_PermissionRequest.PermDate, objHR_PermissionRequest.PermEndDate, tpFromTime, _
        '                                                         tpToTime, objHR_PermissionRequest.PermDate, objPermissionsTypes, objHR_PermissionRequest.FK_PermId, objHR_PermissionRequest.IsFullDay, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
        '            If (ErrorMessage <> String.Empty) Then
        '                CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
        '                Return -1
        '            End If
        '        End If
        '    ElseIf objHR_PermissionRequest.IsFlexible = True Then
        '        tpFromTime = DateTime.MinValue
        '        tpToTime = DateTime.MinValue
        '        If (objEmp_Permissions.ValidateEmployeeFlexiblePermission(objHR_PermissionRequest.FK_EmployeeId, PermissionRequestId, dtCurrent, objHR_PermissionRequest.IsForPeriod, _
        '                                                                objHR_PermissionRequest.PermDate, objHR_PermissionRequest.PermEndDate, _
        '                                                                 objHR_PermissionRequest.PermDate, tpFromTime, tpToTime, objPermissionsTypes, objHR_PermissionRequest.FK_PermId, _
        '                                                                objHR_PermissionRequest.IsFullDay, ErrorMessage, OffAndHolidayDays, strFlexibileDuration, EmpLeaveTotalBalance) = False) Then
        '            If (ErrorMessage <> String.Empty) Then
        '                CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
        '                Return -1
        '            End If

        '        End If
        '    End If
        'End If

        Dim days As String = objHR_PermissionRequest.Days

        If objHR_PermissionRequest.IsForPeriod = False Then
            objHR_PermissionRequest.PermEndDate = Date.MinValue
        End If

        If (objHR_PermissionRequest.IsFullDay = True) Then
            EmpLeaveTotalBalance = EmpLeaveTotalBalance - 1
        End If

        objEmp_Permissions.Days = objHR_PermissionRequest.Days

        ' Set data into object for Add / UpdateIf

        objHR_PermissionRequest.PermissionRequestId = PermissionRequestId
        objHR_PermissionRequest.LeaveId = LeaveID

        Dim fileUploadExtension As String = objHR_PermissionRequest.AttachedFile
        If String.IsNullOrEmpty(fileUploadExtension) Then
            fileUploadExtension = FileExtension
        End If

        With objHR_PermissionRequest
            .FK_EmployeeId = objHR_PermissionRequest.FK_EmployeeId
        End With
        If Not objHR_PermissionRequest.PermissionOption = PermissionOption.Nursing Then
            AllowedTime = Nothing
        Else
            AllowedTime = objHR_PermissionRequest.AllowedTime
        End If
        objEmp_Permissions.AddPermAllProcess(objHR_PermissionRequest.FK_EmployeeId, objHR_PermissionRequest.FK_PermId, _
                                             objHR_PermissionRequest.IsDividable, isFlixible, objHR_PermissionRequest.IsSpecificDays, _
                                      objHR_PermissionRequest.Remark, objHR_PermissionRequest.PermDate, objHR_PermissionRequest.PermEndDate, _
                                      Not objHR_PermissionRequest.IsForPeriod, tpFromTime, tpToTime, objHR_PermissionRequest.PermDate, Convert.ToDouble(objHR_PermissionRequest.Days), _
                                      OffAndHolidayDays, days, EmpLeaveTotalBalance, LeaveID, Integer.Parse(objHR_PermissionRequest.PermissionOption), _
                                      Integer.Parse(IIf(strFlexibileDuration = "00:00", 0, strFlexibileDuration)), fileUploadExtension, objHR_PermissionRequest.IsFullDay, _
                                      AllowedTime, ErrorMessage, Nothing, Nothing)

        showResultMessage(Me.Page, ErrorMessage)
        If errNo = 0 Then
            objHR_PermissionRequest = New HR_PermissionRequest
            objHR_PermissionRequest.PermissionRequestId = PermissionRequestId
            objHR_PermissionRequest.IsRejected = False
            objHR_PermissionRequest.RejectionReason = Me.txtRejectedReason.Text
            objHR_PermissionRequest.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            objHR_PermissionRequest.Update_HR_Permission_RequestStatus()

            If PermissionId = 0 Then
                If fileUploadExtension IsNot Nothing Then
                    Dim extention As String = objHR_PermissionRequest.AttachedFile
                    Dim fileName As String = objEmp_Permissions.PermissionId.ToString()
                    Dim fPath As String = String.Empty
                    fPath = Server.MapPath("..\PermissionFiles\\" + fileName + extention)
                    If File.Exists(fPath) Then
                        File.Delete(fPath)
                        Rename(fPath, fPath)

                    End If

                End If
            Else
                If fileUploadExtension IsNot Nothing Then
                    Dim fileName As String = objEmp_Permissions.PermissionId.ToString()
                    Dim fPath As String = String.Empty
                    fPath = Server.MapPath("..\PermissionFiles\\" + fileName + FileExtension)
                    If File.Exists(fPath) Then
                        Dim extention As String = objHR_PermissionRequest.AttachedFile
                        File.Delete(fPath)
                        fPath = Server.MapPath("..\PermissionFiles\\" + fileName + extention)
                        If File.Exists(fPath) Then
                            File.Delete(fPath)
                            Rename(fPath, fPath)
                        End If
                    Else
                        Dim extention As String = objHR_PermissionRequest.AttachedFile
                        Dim fileNameExist As String = objEmp_Permissions.PermissionId.ToString()
                        Dim fPathExist As String = String.Empty
                        fPathExist = Server.MapPath("..\PermissionFiles\\" + fileNameExist + extention)
                        If File.Exists(fPath) Then
                            File.Delete(fPath)
                            Rename(fPath, fPath)
                        End If
                    End If
                End If
            End If

            objRECALC_REQUEST = New RECALC_REQUEST

            '----------------------Validate Employee Schedule------------'
            objWorkSchedule = New WorkSchedule()
            Dim intWorkScheduleType As Integer
            Dim intWorkScheduleId As Integer

            Dim ScheduleDate As DateTime
            If Not IsForPeriod Then
                ScheduleDate = PermDate
            Else
                ScheduleDate = PermDate
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

            Dim dteFrom As DateTime = PermDate
            Dim dteTo As DateTime = PermEndDate
            objAPP_Settings = New APP_Settings()
            objAPP_Settings = objAPP_Settings.GetByPK()

            If objAPP_Settings.ApprovalRecalMethod = 1 Then
                If IsForPeriod = False AndAlso PermissionType = PermissionOption.Normal Then

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
                        PermDate = PreviousPermDate
                        PermEndDate = NextRestDate
                    End If
                End If

                If PermDate = PermEndDate Then
                    temp_date = PermDate
                    temp_str_date = DateToString(DateTime.Today)
                    objRECALC_REQUEST.EMP_NO = objHR_PermissionRequest.FK_EmployeeId
                    objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)

                    objRecalculateRequest = New RecalculateRequest
                    With objRecalculateRequest
                        .Fk_EmployeeId = EmployeeId
                        .FromDate = temp_date
                        If intWorkScheduleType = 3 Then
                            .ToDate = NextRestDate
                        Else
                            .ToDate = temp_date
                        End If
                        .ImmediatelyStart = True
                        .RecalStatus = 0
                        .CREATED_BY = SessionVariables.LoginUser.ID
                        .Remarks = "HR Permission Approval - SYSTEM"
                        err2 = .Add
                    End With

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

                    If Not dteFrom > Date.Today Then
                        objRecalculateRequest = New RecalculateRequest
                        With objRecalculateRequest
                            .Fk_EmployeeId = EmployeeId
                            .FromDate = dteFrom
                            .ToDate = dteTo
                            .ImmediatelyStart = True
                            .RecalStatus = 0
                            .CREATED_BY = SessionVariables.LoginUser.ID

                            If PermissionType = PermissionOption.Nursing Then
                                .Remarks = "HR Nursing Permission Approval - SYSTEM"
                            ElseIf PermissionType = PermissionOption.Study Then
                                .Remarks = "HR Study Permission Approval - SYSTEM"
                            Else
                                .Remarks = "HR Permission Approval - SYSTEM"
                            End If

                            err2 = .Add
                        End With
                    End If
                End If
            End If



            mvPermissionApproval.ActiveViewIndex = 0
            FillGridView()

        End If

    End Function

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

    Public Sub FillGridView()
        Try
            objHR_PermissionRequest = New HR_PermissionRequest()
            objHR_PermissionRequest.UserId = SessionVariables.LoginUser.ID
            dtCurrent = objHR_PermissionRequest.HR_Permissions_GetAllByStatus
            dgrdVwEmpPermissions.DataSource = dtCurrent
            dgrdVwEmpPermissions.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillGridRejected()
        Try
            objHR_PermissionRequest = New HR_PermissionRequest()
            'objHR_PermissionRequest.UserId = UserId
            objHR_PermissionRequest.UserId = SessionVariables.LoginUser.ID
            dtCurrent_Rejected = objHR_PermissionRequest.HR_Permissions_GetAllRejected
            dgrdRejected.DataSource = dtCurrent_Rejected
            dgrdRejected.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim Concat = CType(sender, LinkButton).CommandArgument
    '    Dim ar As String() = Concat.ToString().Split(","c)
    '    Response.ContentType = ContentType
    '    Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName("~\HR_PermissionRequestFiles\" + ar(0).ToString() + ar(1))))
    '    Response.WriteFile("~\HR_PermissionRequestFiles\" + ar(0).ToString() + ar(1))
    '    Response.End()
    'End Sub

    Private Sub Open_Download_File(ByVal filename As String)
        Dim response As HttpResponse = HttpContext.Current.Response
        response.Clear()
        response.Buffer = True
        response.Charset = ""
        response.ContentType = "application/text/plain"
        response.Charset = "UTF-8"
        response.ContentEncoding = System.Text.Encoding.UTF8
        response.AddHeader("Content-Disposition", "attachment;filename=""" & filename)
        response.WriteFile(Server.MapPath("..\\HR_PermissionRequestFiles\") & filename)
        response.Flush()
        response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble())
        response.[End]()
    End Sub

#End Region


End Class
