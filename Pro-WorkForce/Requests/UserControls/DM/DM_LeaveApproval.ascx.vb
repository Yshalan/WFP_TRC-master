Imports SmartV.UTILITIES
Imports TA.Employees
Imports TA.Admin
Imports Telerik.Web.UI
Imports TA.Definitions
Imports TA.SelfServices
Imports System.IO
Imports System.Data
Imports TA.DailyTasks
Imports System.Web.UI.ClientScriptManager
Imports System.Web.UI.WebControls
Imports TA.Security
Imports System
'Imports SmartV.Version

Partial Class Requests_UserControls_DM_DM_LeaveApproval
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
    Private objVersion As SmartV.Version.version
    Dim objSYSUsers As SYSUsers
    Delegate Sub EmployeeSelectedChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)
    Public Event eventEmployeeSelect As EmployeeSelectedChanged
    ''' <summary>
    ''' ''''''''''''''''''''
    ''' </summary>
    ''' <remarks></remarks>
    'Dim NextApprovalStatus As Integer = 0
    'Dim FromDate As Date
    'Dim ToDate As Date
    'Dim Ap_EmployeeId As Integer = 0
    'Dim AP_LeaveId As Integer = 0
    ''Dim FK_LeaveTypeId As Integer = 0
    'Dim Requestdate As Date
    'Dim AttachedFile As String = String.Empty
    'Dim Remarks As String = String.Empty
#End Region

#Region "Properties"

    Private Enum RequestStatus
        Pending = 1
        ApprovedByDM = 2
        ApprovedByHR = 3
        RejectedByDM = 4
        RejectedByHR = 5
        ApprovedByGM = 6
        RejectedByGM = 7
    End Enum
#Region "Property :: EntityId"
    Public Property EntityId() As Integer
        Get
            Return ViewState("EntityId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EntityId") = value
        End Set
    End Property
#End Region
#Region "Property :: CompanyId"
    Public Property CompanyId() As Integer
        Get
            Return ViewState("CompanyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CompanyId") = value
        End Set
    End Property
#End Region
    '#Region "Property :: Hdn EmpId"
    '    Public Property hEmployeeId() As Integer
    '        Get
    '            'Return hdnEmployeeId.Value
    '            If Not (String.IsNullOrEmpty(hdnEmployeeId.Value)) Then
    '                Return Convert.ToInt32(hdnEmployeeId.Value)
    '            Else
    '                Return 0

    '            End If
    '        End Get
    '        Set(ByVal value As Integer)
    '            hdnEmployeeId.Value = value
    '        End Set
    '    End Property
    '#End Region
    Public Property EmployeeIdtxt() As Integer
        Get
            Return ViewState("EmployeeIdtxt")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeIdtxt") = value
        End Set
    End Property

    Public Property EmployeeName() As String
        Get
            Return ViewState("EmployeeName")
        End Get
        Set(ByVal value As String)
            ViewState("EmployeeName") = value
        End Set
    End Property
    Public Property FK_ManagerId() As Integer
        Get
            Return ViewState("FK_ManagerId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_ManagerId") = value
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
            mvLeaveApproval.SetActiveView(viewDMApproval)
            lblLeaveRequests.Text = ResourceManager.GetString("DirectMgrLeaveHeader", CultureInfo)
            Page.Title = "Work Force Pro : :" + ResourceManager.GetString("DirectMgrLeaveHeader", CultureInfo)
            ShowHideControls()
            FillGridView()
            'FillCompanies()
        End If

    End Sub

    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'Dim url As String = "../../admin/AssignEmployee.aspx"
        'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=700,height=300,left=400,top=100,resizable=yes');"
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)


        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0

        objRECALC_REQUEST = New RECALC_REQUEST '----Immediate
        objRecalculateRequest = New RecalculateRequest '----Recalculate Request Table

        Dim temp_str_date As String
        Dim err2 As Integer

        FK_LeaveTypeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("LeaveTypeId").ToString())

        objAPP_Settings = New APP_Settings()
        objLeavesTypes = New LeavesTypes()

        objAPP_Settings = objAPP_Settings.GetByPK()

        If Not objAPP_Settings.LeaveApprovalfromLeave Then
            LeaveApproval = objAPP_Settings.LeaveApproval
        Else
            objLeavesTypes.LeaveId = FK_LeaveTypeId
            objLeavesTypes = objLeavesTypes.GetByPK()
            LeaveApproval = objLeavesTypes.LeaveApproval
        End If

        Dim NextApprovalStatus As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("NextApprovalStatus").ToString())


        If (LeaveApproval = 1 Or NextApprovalStatus = 2) Then
            Dim FromDate As DateTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FromDate").ToString())
            Dim ToDate As DateTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("ToDate").ToString())
            Dim EmployeeId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_EmployeeId").ToString())
            Dim LeaveId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("LeaveId").ToString())
            FK_LeaveTypeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("LeaveTypeId").ToString())
            Dim Requestdate As DateTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Requestdate").ToString())
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

            If (objEmp_Leaves.ValidateEmployeeLeave(objLeavesTypes, FromDate, ToDate, FK_LeaveTypeId, EmployeeId,
                                                    ErrorMessage, OffAndHolidayDays, LeaveId, EmpLeaveTotalBalance) = False) Then
                If (ErrorMessage <> String.Empty) Then
                    CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                    Return
                End If

            Else
                objEmp_Leaves.LeaveId = 0
                objEmp_Leaves.AddLeaveAllProcess(EmployeeId, OffAndHolidayDays, FromDate, ToDate, FromDate,
                ToDate, Requestdate, Remarks, FK_LeaveTypeId, EmpLeaveTotalBalance, AttachedFile, ErrorMessage, LeaveId, True)
            End If

            With objEmp_LeavesRequest
                .FK_StatusId = NextApprovalStatus
                .LeaveId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("LeaveId").ToString())
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
                Err = .UpdateLeaveStatus()
                strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                If (File.Exists(Server.MapPath("~\LeaveRequestFiles\\" + LeaveId.ToString() + AttachedFile))) Then
                    My.Computer.FileSystem.RenameFile(Server.MapPath("~\LeaveRequestFiles\\" + LeaveId.ToString() + AttachedFile),
                                                      (objEmp_Leaves.LeaveId.ToString() + AttachedFile).ToString())

                    If (File.Exists(Server.MapPath("~\LeaveRequestFiles\\" + objEmp_Leaves.LeaveId.ToString() + AttachedFile))) Then
                        Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("~\LeaveRequestFiles\\" + objEmp_Leaves.LeaveId.ToString() + AttachedFile))
                        CopyFile.CopyTo(Server.MapPath("~\LeaveFiles\\" + objEmp_Leaves.LeaveId.ToString() + AttachedFile))
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
                    CtlCommon.ShowMessage(Me.Page, strMessage = ResourceManager.GetString("ErrorSave", CultureInfo), "error")
                End If
            End With
        ElseIf (LeaveApproval = 3 Or LeaveApproval = 4 Or (LeaveApproval = 1 And NextApprovalStatus = 9)) Then
            objEmp_LeavesRequest = New Emp_LeavesRequest()
            With objEmp_LeavesRequest
                .FK_StatusId = NextApprovalStatus
                .LeaveId = CStr(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("LeaveId").ToString())
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
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
            .FK_StatusId = RequestStatus.RejectedByDM
            .RejectedReason = txtRejectedReason.Text.Trim(",")
            .LeaveId = LeaveID
            '.LeaveId = CStr(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("LeaveId").Text)
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
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

    Protected Sub dgrdEmpLeaveRequest_OnRowCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles dgrdEmpLeaveRequest.ItemCommand
        If (e.CommandName = "reject") Then
            txtRejectedReason.Text = String.Empty
            LeaveID = e.CommandArgument
            mpeRejectPopupLeave.Show()
            'ElseIf (e.CommandName = "accept") Then
            '    Dim item As GridDataItem = e.Item
            '    NextApprovalStatus = item.GetDataKeyValue("NextApprovalStatus").ToString()
            '    FromDate = item.GetDataKeyValue("FromDate").ToString()
            '    ToDate = item.GetDataKeyValue("ToDate").ToString()
            '    EmployeeId = item.GetDataKeyValue("FK_EmployeeId").ToString()
            '    LeaveID = item.GetDataKeyValue("LeaveId").ToString()
            '    FK_LeaveTypeId = item.GetDataKeyValue("LeaveTypeId").ToString()
            '    Requestdate = item.GetDataKeyValue("Requestdate").ToString()
            '    AttachedFile = item.GetDataKeyValue("AttachedFile").ToString()
            '    If (item.GetDataKeyValue("Remarks").ToString() <> "") Then
            '        Remarks = item.GetDataKeyValue("Remarks").ToString()
            '    End If
            '    mpeAcceptPopupLeave.Show()

        ElseIf e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdEmpLeaveRequest_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdEmpLeaveRequest.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromDate").ToString())) And (Not item.GetDataKeyValue("FromDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("FromDate").ToString()
                item("FromDate").Text = fromDate.ToShortDateString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ToDate").ToString())) And (Not item.GetDataKeyValue("ToDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("ToDate").ToString()
                item("ToDate").Text = fromDate.ToShortDateString()
            End If

            Dim LeaveId As Integer = item.GetDataKeyValue("LeaveId").ToString()
            Dim AttachedFile As String = item.GetDataKeyValue("AttachedFile").ToString()
            If AttachedFile = "&nbsp;" Or AttachedFile = "" Then
                DirectCast(e.Item.FindControl("lnbView"), LinkButton).Visible = False
            Else
                DirectCast(e.Item.FindControl("lnbView"), LinkButton).Visible = True
            End If

            Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            scriptManager__1.RegisterPostBackControl(DirectCast(e.Item.FindControl("lnbView"), LinkButton))

            Dim scriptManager__2 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            scriptManager__2.RegisterPostBackControl(DirectCast(e.Item.FindControl("lnkLeaveForm"), LinkButton))

            ' If item("StatusId").Text = 2 Then
            Dim lnkLeaveForm As LinkButton = DirectCast(item.FindControl("lnkLeaveForm"), LinkButton)
            lnkLeaveForm.Visible = True
            'End If

            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
                item("LeaveName").Text = DirectCast(item.FindControl("hdnLeaveTpeAr"), HiddenField).Value
                item("StatusName").Text = DirectCast(item.FindControl("hdnStatusNameAr"), HiddenField).Value
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("LeaveTypeId").ToString())) And (Not item.GetDataKeyValue("LeaveTypeId").ToString() = "")) Then
                Dim leaveTypeId As Integer = Convert.ToInt32(item.GetDataKeyValue("LeaveTypeId").ToString())
                Dim objLeavesTypesGrid As New LeavesTypes()
                With objLeavesTypesGrid
                    .LeaveId = leaveTypeId
                    .GetByPK()
                    If .IsAnnual Then
                        DirectCast(item.FindControl("chbWithAdvancedSalary"), CheckBox).Visible = True

                        If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("HasAdvancedSalary").ToString())) And (Not item.GetDataKeyValue("HasAdvancedSalary").ToString() = "")) Then
                            DirectCast(item.FindControl("chbWithAdvancedSalary"), CheckBox).Checked = Convert.ToBoolean(item.GetDataKeyValue("HasAdvancedSalary").ToString())
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
        objEmp_LeavesRequest.FK_StatusId = RequestStatus.Pending
        dtCurrent = objEmp_LeavesRequest.GetByDirectManager()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdEmpLeaveRequest.DataSource = dv

    End Sub

    Protected Sub lnkLeaveForm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        EmployeeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_EmployeeId").ToString())
        LeaveID = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("LeaveId").ToString())

        '---Employee Signature Information---'
        Dim dt As DataTable
        objEmployee_Manager = New Employee_Manager
        objEmployee_Manager.FK_EmployeeId = EmployeeId
        dt = objEmployee_Manager.GetActiveManagerByEmpId(Date.Today)
        If Not dt Is Nothing Then
            If dt.Rows.Count > 0 Then
                objEmployee = New Employee
                objEmployee.EmployeeId = dt.Rows(0)("FK_ManagerId").ToString
                objEmployee.GetByPK()
                ManagerName = objEmployee.EmployeeName
            End If
        End If

        '---Employee Signature Information---'

        Response.Redirect("~/Reports/LeaveForm_ReportViewer.aspx?EmployeeId=" & EmployeeId & "&LeaveId=" & LeaveID & "&ManagerName=" & ManagerName & "&Signature=" & 2) '---2= Manager Signature'

        'Dim QueryString As String = "../Reports/LeaveForm_ReportViewer.aspx?EmployeeId=" & EmployeeId & "&LeaveId=" & LeaveID & "&ManagerName=" & ManagerName & "&Signature=" & 1
        'Dim newWin As String = (Convert.ToString("window.open('") & QueryString) + "', 'popup_window', 'width=600,height=200,left=400,top=100,resizable=yes,scrollbars=1');"
        'ClientScript.RegisterStartupScript(Me.GetType(), "pop", newWin, True)
    End Sub

    Protected Sub lnkDownloadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        objEmp_LeavesRequest = New Emp_LeavesRequest
        LeaveID = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("LeaveId").ToString())
        With objEmp_LeavesRequest
            .LeaveId = LeaveID
            .GetByPK()

            Dim FileName As String = LeaveID.ToString() + .AttachedFile
            Dim FilePath As String = Server.MapPath("..\\LeaveRequestFiles\") & FileName
            CtlCommon.Open_Download_File(FileName, FilePath)

        End With
    End Sub

#End Region

#Region "Methods"

    Public Sub FillGridView()
        Try
            Dim dtLeaveRequets As New DataTable
            objEmp_LeavesRequest = New Emp_LeavesRequest()
            objEmp_LeavesRequest.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            objEmp_LeavesRequest.FK_StatusId = RequestStatus.Pending
            dtLeaveRequets = objEmp_LeavesRequest.GetByDirectManager()
            dgrdEmpLeaveRequest.DataSource = dtLeaveRequets
            dgrdEmpLeaveRequest.DataBind()



            If Not dtLeaveRequets Is Nothing Then
                If dtLeaveRequets.Rows.Count > 0 Then
                    If Not lblRequestNo.Text.Contains(":") Then
                        lblRequestNo.Text += " : " + (dtLeaveRequets.Rows.Count).ToString()
                    Else
                        Dim strArr() As String = lblRequestNo.Text.Split(":")
                        lblRequestNo.Text = strArr(0) + " : " + (dtLeaveRequets.Rows.Count).ToString()
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

        Dim LeaveType As Integer = CInt(item.GetDataKeyValue("LeaveTypeId").ToString())

        objAPP_Settings = New APP_Settings()
        objLeavesTypes = New LeavesTypes()

        objAPP_Settings = objAPP_Settings.GetByPK()

        If Not objAPP_Settings.LeaveApprovalfromLeave Then
            LeaveApproval = objAPP_Settings.LeaveApproval
        Else
            objLeavesTypes.LeaveId = LeaveType
            objLeavesTypes = objLeavesTypes.GetByPK()
            LeaveApproval = objLeavesTypes.LeaveApproval
        End If

        Dim NextApprovalStatus As Integer = CInt(item.GetDataKeyValue("NextApprovalStatus").ToString())

        If (LeaveApproval = 1 And NextApprovalStatus = 2) Then
            Dim FromDate As DateTime = CDate(item.GetDataKeyValue("FromDate").ToString())
            Dim ToDate As DateTime = CDate(item.GetDataKeyValue("ToDate").ToString())
            Dim EmployeeId As Integer = CInt(item.GetDataKeyValue("FK_EmployeeId").ToString())
            Dim LeaveId As Integer = CInt(item.GetDataKeyValue("LeaveId").ToString())
            LeaveType = CInt(item.GetDataKeyValue("LeaveTypeId").ToString())
            FK_LeaveTypeId = CInt(item.GetDataKeyValue("LeaveTypeId").ToString())
            Dim Requestdate As DateTime = CDate(item.GetDataKeyValue("Requestdate").ToString())
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

            If (objEmp_Leaves.ValidateEmployeeLeave(objLeavesTypes, FromDate, ToDate, FK_LeaveTypeId, EmployeeId,
                                                    ErrorMessage, OffAndHolidayDays, LeaveId, EmpLeaveTotalBalance) = False) Then
                If (ErrorMessage <> String.Empty) Then
                    CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                    Msg = ErrorMessage
                    Return
                End If

            Else
                objEmp_Leaves.LeaveId = 0
                objEmp_Leaves.AddLeaveAllProcess(EmployeeId, OffAndHolidayDays, FromDate, ToDate, FromDate,
                ToDate, Requestdate, Remarks, LeaveType, EmpLeaveTotalBalance, AttachedFile, ErrorMessage, Nothing, True)
            End If

            With objEmp_LeavesRequest
                .FK_StatusId = NextApprovalStatus
                .LeaveId = CInt(item.GetDataKeyValue("LeaveId").ToString())
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
                Err = .UpdateLeaveStatus()
                strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                If (File.Exists(Server.MapPath("~\LeaveRequestFiles\\" + LeaveId.ToString() + AttachedFile))) Then
                    My.Computer.FileSystem.RenameFile(Server.MapPath("~\LeaveRequestFiles\\" + LeaveId.ToString() + AttachedFile),
                                                      (objEmp_Leaves.LeaveId.ToString() + AttachedFile).ToString())

                    If (File.Exists(Server.MapPath("~\LeaveRequestFiles\\" + objEmp_Leaves.LeaveId.ToString() + AttachedFile))) Then
                        Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("~\LeaveRequestFiles\\" + objEmp_Leaves.LeaveId.ToString() + AttachedFile))
                        CopyFile.CopyTo(Server.MapPath("~\LeaveFiles\\" + objEmp_Leaves.LeaveId.ToString() + AttachedFile))
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
        ElseIf (LeaveApproval = 3 Or LeaveApproval = 4 Or (LeaveApproval = 1 And NextApprovalStatus = 9)) Then
            objEmp_LeavesRequest = New Emp_LeavesRequest()
            With objEmp_LeavesRequest
                .FK_StatusId = NextApprovalStatus
                .LeaveId = CStr(item.GetDataKeyValue("LeaveId").ToString())
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
                Err = .UpdateLeaveStatus()

                strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
                'If Err = 0 Then
                '    FillGridView()
                '    CtlCommon.ShowMessage(Me.Page, strMessage)
                'Else
                '    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo))
                'End If
            End With
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("LeaveWorkFlowValidation", CultureInfo), "info")
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
            .FK_StatusId = RequestStatus.RejectedByDM
            .RejectedReason = txtRejectAllReason.Text.Trim(",")
            .LeaveId = CInt(item.GetDataKeyValue("LeaveId").ToString())
            '.LeaveId = CStr(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("LeaveId").Text)
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
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
