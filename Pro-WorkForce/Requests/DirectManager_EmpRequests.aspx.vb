Imports SmartV.UTILITIES
Imports TA.Employees
Imports TA.Admin
Imports Telerik.Web.UI
Imports TA.Definitions
Imports TA.SelfServices
Imports System.IO
Imports System.Data
Imports TA.DailyTasks
Partial Class Requests_DirectManager_EmpRequests
    Inherits System.Web.UI.Page

#Region "Class Variables"
    Private Lang As CtlCommon.Lang
    Public MsgLang As String
    Dim CultureInfo As System.Globalization.CultureInfo
    Shared SortExepression As String
    Shared dtCurrent As DataTable
    Private objEmp_Leaves As Emp_Leaves
    Private objLeavesTypes As LeavesTypes
    Private objEmp_LeavesRequest As Emp_LeavesRequest
    Private objEmp_PermissionsRequest As Emp_PermissionsRequest
    Private objEmpPermissions As Emp_Permissions
    Private objPermissionsTypes As PermissionsTypes
    Private objProjectCommon As New ProjectCommon
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
#End Region

#Region "Properties"
    Private Enum RequestStatus
        Pending = 1
        ApprovedByDM = 2
        Approved = 3
        Rejected = 4
    End Enum
    Public Property PermissionId() As Integer
        Get
            Return ViewState("PersmissionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("PersmissionId") = value
        End Set
    End Property
    Public Property LeaveID() As String
        Get
            Return ViewState("LeaveID")
        End Get
        Set(ByVal value As String)
            ViewState("LeaveID") = value
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
    Public Property LeaveRequestID() As Integer
        Get
            Return ViewState("LeaveRequestID")
        End Get
        Set(ByVal value As Integer)
            ViewState("LeaveRequestID") = value
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
#End Region

#Region "Page Events"
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
            MsgLang = "ar"
            dgrdEmpPermRequest.Columns(1).Visible = False
            dgrdEmpPermRequest.Columns(3).Visible = False
            dgrdEmpPermRequest.Columns(21).Visible = False

            dgrdEmpLeaveRequests.Columns(1).Visible = False
            dgrdEmpLeaveRequests.Columns(14).Visible = False
        Else
            Lang = CtlCommon.Lang.EN
            MsgLang = "en"
            dgrdEmpPermRequest.Columns(2).Visible = False
            dgrdEmpPermRequest.Columns(4).Visible = False
            dgrdEmpPermRequest.Columns(22).Visible = False

            dgrdEmpLeaveRequests.Columns(2).Visible = False
            dgrdEmpLeaveRequests.Columns(15).Visible = False
        End If
        If Not IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                MsgLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                MsgLang = "en"
            End If
            rblRequestType.SelectedValue = 1
            FillLeaveTypes()
            FillPermissionTypes()
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            DMLeaveApprovalHeader.HeaderText = ResourceManager.GetString("DircetManager_EmpRequests", CultureInfo)
            Page.Title = "Work Force Pro : :" + ResourceManager.GetString("DircetManager_EmpRequests", CultureInfo)
            raddateFromDate.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            raddateToDate.SelectedDate = dd
        End If
    End Sub
    Protected Sub btnApplyFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApplyFilter.Click
        FillGridView()
        mvEmpRequests.SetActiveView(viewRequests)
    End Sub
    Protected Sub rblRequestType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblRequestType.SelectedIndexChanged

    End Sub
    Protected Sub dgrdEmpPermRequest_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdEmpPermRequest.SelectedIndexChanged
        PermissionId = Convert.ToInt32(DirectCast(dgrdEmpPermRequest.SelectedItems(0), GridDataItem).GetDataKeyValue("PermissionId"))
        FillPermControls()
        mvEmpRequests.SetActiveView(viewPermissionRequest)
    End Sub
    Protected Sub dgrdEmpLeaveRequests_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdEmpLeaveRequests.SelectedIndexChanged
        LeaveRequestID = Convert.ToInt32(DirectCast(dgrdEmpLeaveRequests.SelectedItems(0), GridDataItem).GetDataKeyValue("LeaveID"))
        FillLeaveControls()
        mvEmpRequests.SetActiveView(viewLeaveRequest)
    End Sub
    Protected Sub btnLeaveCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLeaveCancel.Click
        Response.Redirect("../Requests/DirectManager_EmpRequests.aspx")
    End Sub
    Protected Sub btnPermCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPermCancel.Click
        Response.Redirect("../Requests/DirectManager_EmpRequests.aspx")
    End Sub
    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        RefreshControls()
        rblRequestType.SelectedValue = Nothing
    End Sub
    Protected Sub dgrdEmpLeaveRequests_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmpLeaveRequests.NeedDataSource
        objEmp_LeavesRequest = New Emp_LeavesRequest()
        objEmp_LeavesRequest.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
        objEmp_LeavesRequest.FromDate = raddateFromDate.DbSelectedDate
        objEmp_LeavesRequest.ToDate = raddateToDate.DbSelectedDate
        dtCurrent = objEmp_LeavesRequest.GetAllByDirectManager()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdEmpLeaveRequests.DataSource = dv
    End Sub
    Protected Sub dgrdEmpPermRequest_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmpPermRequest.NeedDataSource
        objEmp_PermissionsRequest = New Emp_PermissionsRequest()
        objEmp_PermissionsRequest.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
        objEmp_PermissionsRequest.FromDate = raddateFromDate.DbSelectedDate
        objEmp_PermissionsRequest.ToDate = raddateToDate.DbSelectedDate
        dtCurrent = objEmp_PermissionsRequest.GetAllByDirectManager()
        For Each row As DataRow In dtCurrent.Rows
            If row("IsFullDay") = True Then
                row("FromTime") = DBNull.Value
                row("ToTime") = DBNull.Value
            End If
        Next
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdEmpPermRequest.DataSource = dv
    End Sub
    Protected Sub dgrdEmpPermRequest_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdEmpPermRequest.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            Dim PermissionId As Integer = item.GetDataKeyValue("PermissionId")
            Dim AttachedFile As String = IIf(Not IsDBNull(item.GetDataKeyValue("AttachedFile")), item.GetDataKeyValue("AttachedFile"), "")
            If AttachedFile = "" Then
                DirectCast(e.Item.FindControl("lnbView"), HtmlAnchor).Visible = False
            Else
                DirectCast(e.Item.FindControl("lnbView"), HtmlAnchor).HRef = "../PermissionRequestFiles/" + PermissionId.ToString() + AttachedFile
            End If
        End If
    End Sub
    Protected Sub dgrdEmpLeaveRequests_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdEmpLeaveRequests.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            Dim LeaveId As Integer = item.GetDataKeyValue("LeaveId")
            Dim AttachedFile As String = item.GetDataKeyValue("AttachedFile")
            If AttachedFile = "" Then
                DirectCast(e.Item.FindControl("lnbView"), HtmlAnchor).Visible = False
            Else
                DirectCast(e.Item.FindControl("lnbView"), HtmlAnchor).HRef = "../LeaveRequestFiles/" + LeaveId.ToString() + AttachedFile
            End If
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillGridView()

        If rblRequestType.SelectedValue = "1" Then
            Dim dt As DataTable
            objEmp_PermissionsRequest = New Emp_PermissionsRequest()
            objEmp_PermissionsRequest.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            objEmp_PermissionsRequest.FromDate = raddateFromDate.DbSelectedDate
            objEmp_PermissionsRequest.ToDate = raddateToDate.DbSelectedDate
            dt = objEmp_PermissionsRequest.GetAllByDirectManager()
            For Each row As DataRow In dt.Rows
                If row("IsFullDay") = True Then
                    row("FromTime") = DBNull.Value
                    row("ToTime") = DBNull.Value
                End If
            Next
            dgrdEmpPermRequest.DataSource = dt
            If Not dgrdEmpPermRequest.DataSource Is Nothing Then
                dgrdEmpPermRequest.DataBind()
                dgrdEmpPermRequest.Visible = True
                RadFilter_Perm.Visible = True
                dgrdEmpLeaveRequests.Visible = False
                RadFilter_Leave.Visible = False
            End If
        ElseIf rblRequestType.SelectedValue = "2" Then
            objEmp_LeavesRequest = New Emp_LeavesRequest()
            objEmp_LeavesRequest.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            objEmp_LeavesRequest.FromDate = raddateFromDate.DbSelectedDate
            objEmp_LeavesRequest.ToDate = raddateToDate.DbSelectedDate
            dgrdEmpLeaveRequests.DataSource = objEmp_LeavesRequest.GetAllByDirectManager()
            If Not dgrdEmpLeaveRequests.DataSource Is Nothing Then
                dgrdEmpLeaveRequests.DataBind()
                dgrdEmpLeaveRequests.Visible = True
                RadFilter_Leave.Visible = True
                dgrdEmpPermRequest.Visible = False
                RadFilter_Perm.Visible = False
            End If
        Else
            dgrdEmpLeaveRequests.Visible = False
            dgrdEmpPermRequest.Visible = False
        End If

    End Sub
    Private Sub RefreshControls()
        dgrdEmpLeaveRequests.Visible = False
        dgrdEmpPermRequest.Visible = False
        RadFilter_Leave.Visible = False
        RadFilter_Perm.Visible = False
        dgrdEmpLeaveRequests.DataBind()
        dgrdEmpPermRequest.DataBind()
        raddateFromDate.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
        Dim dd As New Date
        dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
        raddateToDate.SelectedDate = dd
    End Sub

#Region "Leave Controls"
    Private Sub FillLeaveTypes()
        objProjectCommon = New ProjectCommon()
        Dim dt As DataTable = Nothing
        dt = Nothing
        objLeavesTypes = New LeavesTypes()
        dt = objLeavesTypes.GetAllForDDL
        ProjectCommon.FillRadComboBox(ddlLeaveType, dt, "LeaveName", _
                                     "LeaveArabicName", "LeaveId")
    End Sub
    Private Sub FillLeaveControls()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objEmp_LeavesRequest = New Emp_LeavesRequest
        With objEmp_LeavesRequest
            .LeaveId = LeaveRequestID
            .GetByPK()
            ddlLeaveType.SelectedValue = .FK_LeaveTypeId
            dtpFromDate.SelectedDate = .FromDate
            dtpToDate.SelectedDate = .ToDate
            dtpRequestDate.SelectedDate = .RequestDate
            txtRemarks.Text = .Remarks
            fuAttachFile.Visible = False
            lnbLeaveFile.Visible = True
            FileExtension = .AttachedFile
            If .AttachedFile = "" Then
                lnbLeaveFile.Visible = False
                Label1.Visible = False
            Else
                lnbLeaveFile.Visible = True
                Label1.Visible = True
                lnbLeaveFile.HRef = "../LeaveRequestFiles/" + .LeaveId.ToString() + .AttachedFile
            End If
        End With
    End Sub
#End Region

#Region "Permission Controls"
    Private Sub ManageSomeControlsStatus(ByVal IsForPeriod As Boolean, _
                                        ByVal PermDate As DateTime, _
                                        ByVal PermEndDate As DateTime?, _
                                        ByVal FullDay As Boolean)
        ' Manage the DatePickers according 
        ' to the permission type
        ShowHide(IsForPeriod)
        If IsForPeriod = False Then
            dtpPermissionDate.SelectedDate = PermDate
        Else
            dtpStartDatePerm.SelectedDate = PermDate
            dtpEndDatePerm.SelectedDate = _
                IIf(CheckDate(PermEndDate) = Nothing, Nothing, PermEndDate)
        End If
        ' If the permission for full day , means to disable the TimeView(s)
        ' otherwise means to enable or keep it enable
        If FullDay = False Then
            setTimeDifference()
        Else
            txtTimeDifference.Text = String.Empty
        End If
    End Sub
    Private Sub ShowHide(ByVal IsPeriod As Boolean)
        pnlPeriodLeave.Visible = IsPeriod
        PnlOneDayLeave.Visible = Not IsPeriod
        radBtnOneDay.Checked = Not IsPeriod
        radBtnPeriod.Checked = IsPeriod
    End Sub
    Private Function setTimeDifference() As TimeSpan
        Try
            Dim temp1 As DateTime = Nothing
            Dim temp2 As DateTime = Nothing

            temp1 = RadTPfromTime.SelectedDate.Value
            temp2 = RadTPtoTime.SelectedDate.Value
            Dim startTime As New DateTime(2011, 1, 1, _
                                          temp1.Hour(), temp1.Minute(), temp1.Second)

            Dim endTime As New DateTime(2011, 1, 1, _
                                          temp2.Hour(), temp2.Minute(), temp2.Second)


            Dim c As TimeSpan = (endTime.Subtract(startTime))
            Dim result As Integer = _
                DateTime.Compare(endTime, startTime)

            Dim hours As Integer = 0
            Dim minutes As Integer = 0

            If result = 0 Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    txtTimeDifference.Text = 0 & " ساعات," & _
                      0 & " دقائق"
                    txtTimeDifference.Style("text-align") = "right"
                Else
                    txtTimeDifference.Text = 0 & " hours," & _
                      0 & " minuts"
                End If

            ElseIf result > 0 Then
                Dim TotalMinutes = c.TotalMinutes()
                If (TotalMinutes > 59) Then
                    TotalMinutes = Math.Ceiling(TotalMinutes - (60 * c.Hours()))
                Else
                    TotalMinutes = Math.Ceiling(TotalMinutes)
                End If

                If SessionVariables.CultureInfo = "ar-JO" Then
                    txtTimeDifference.Text = c.Hours() & " ساعات," & _
                     TotalMinutes & " دقائق"
                    txtTimeDifference.Style("text-align") = "right"
                Else
                    txtTimeDifference.Text = c.Hours() & " hours," & _
                     TotalMinutes & " minutes"

                End If

            ElseIf result < 0 Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    txtTimeDifference.Text = c.Hours() & " ساعات," & _
                       Math.Ceiling(c.TotalMinutes()) & " دقائق"
                    txtTimeDifference.Style("text-align") = "right"
                Else
                    txtTimeDifference.Text = c.Hours() & " hours," & _
                       Math.Ceiling(c.TotalMinutes()) & " minutes"
                End If

                If c.Hours() <= 0 And c.Minutes() <= 0 Then
                    hours = c.Hours() * -1
                    '''''''''''''''''''''
                    If c.Hours() <> 0 Then
                        hours = 24 - hours
                    Else
                        hours = 23
                    End If
                    '''''''''''''''''''''''''
                    minutes = Math.Ceiling(c.TotalMinutes()) * -1
                    If minutes <> 0 Then
                        minutes = 60 - minutes
                        If minutes = 60 Then
                            hours = hours + 1
                            minutes = 0
                        End If
                    Else
                        minutes = 0

                    End If
                    '''''''''''
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        txtTimeDifference.Text = hours & " ساعات," & _
                          Math.Ceiling(c.TotalMinutes()) & " دقائق"
                        txtTimeDifference.Style("text-align") = "right"
                    Else
                        txtTimeDifference.Text = hours & " hours," & _
                          Math.Ceiling(c.TotalMinutes()) & " minutes"

                    End If
                End If
            End If
        Catch ex As Exception

            MsgBox(ex.Message)

        End Try
    End Function
    Private Function CheckDate(ByVal myDate As Object) As Object
        ' input : DateTime object
        ' Output : Nothing or a DateTime greater tha date time 
        ' minimum value
        ' Processing : Check the input if a valid date time to be used
        ' valid means greater than the minimum value and in valid format
        If IsDate(myDate) Then
            myDate = _
                IIf(myDate > DateTime.MinValue, myDate, Nothing)
            Return (myDate)
        Else
            Return Nothing
        End If
    End Function
    Private Sub FillPermControls()
        objEmp_PermissionsRequest = New Emp_PermissionsRequest()
        objEmp_PermissionsRequest.PermissionId = PermissionId
        ' If the PermissionId is not a valid one , the below line will through 
        ' an exception
        objEmp_PermissionsRequest.GetByPK()
        With objEmp_PermissionsRequest
            ' Fill combo boxes
            'RadCmbEmployee.SelectedValue = .FK_EmployeeId

            RadCmpPermissions.SelectedValue = .FK_PermId
            ' Fill checkBoxes
            chckIsDividable.Checked = .IsDividable
            chckIsFlexible.Checked = .IsFlexible
            chckSpecifiedDays.Checked = .IsSpecificDays
            ' Fill the time & date pickers
            If Not .IsFullDay = True Then
                RadTPfromTime.SelectedDate = .FromTime
                RadTPtoTime.SelectedDate = .ToTime
            Else
                RadTPfromTime.SelectedDate = Nothing
                RadTPtoTime.SelectedDate = Nothing
            End If

            txtPermRemarks.Text = .Remark
            txtDays.Text = .Days
            FileExtension = .AttachedFile
            fuAttachFile.Visible = False
            lnbLeaveFile.Visible = True
            If .AttachedFile = "" Then
                lnbLeaveFile.Visible = False
                Label1.Visible = False
            Else
                lnbLeaveFile.Visible = True
                Label1.Visible = True
                lnbLeaveFile.HRef = "../PermissionRequestFiles/" + .PermissionId.ToString() + .AttachedFile
            End If

            '''''''''''''''''''''''''''''''''''''''''''
            ' Enable / Disable time pickers according to the full day 
            ' value
            chckFullDay.Checked = .IsFullDay
            ManageSomeControlsStatus(.IsForPeriod, .PermDate, .PermEndDate, .IsFullDay)
        End With
    End Sub
    Private Sub FillPermissionTypes()
        Dim dt As DataTable = Nothing
        objPermissionsTypes = New PermissionsTypes()
        dt = objPermissionsTypes.GetAll()
        If dt IsNot Nothing Then
            CtlCommon.FillTelerikDropDownList(RadCmpPermissions, dt, Lang)
        End If
    End Sub
#End Region

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub
    Protected Function GetLeave_FilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmpLeaveRequests.Skin))
    End Function
    Protected Function GetPerm_FilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmpPermRequest.Skin))
    End Function
    Protected Sub ddgrdEmpLeaveRequests_OnItemCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles dgrdEmpLeaveRequests.ItemCommand
        RadFilter_Leave.FireApplyCommand()
    End Sub
    Protected Sub dgrdEmpPermRequest_OnItemCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles dgrdEmpPermRequest.ItemCommand
        RadFilter_Perm.FireApplyCommand()
    End Sub
#End Region

End Class
