Imports TA.Employees
Imports System.Data
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.LookUp
Imports TA.Definitions
Imports TA.SelfServices
Imports System.IO
Imports TA.Security
Imports TA.Admin
Imports Telerik.Web.UI.Calendar
Imports TA.DailyTasks

Partial Class Admin_LeavesRequest
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_LeavesRequest As Emp_LeavesRequest
    Private objProjectCommon As New ProjectCommon
    Private objRequestStatus As New RequestStatus
    Private objLeavesTypes As LeavesTypes
    Private objEmp_Leaves_BalanceHistory As New Emp_Leaves_BalanceHistory
    Private Lang As CtlCommon.Lang
    Private objEmp_Leaves As Emp_Leaves
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Shared SortExepression As String
    Shared dtCurrent As DataTable
    Private objAPP_Settings As APP_Settings
    Private objEmployee_Manager As Employee_Manager
    Private objEmployee As Employee
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objRecalculateRequest As RecalculateRequest

#End Region

#Region "Public Properties"

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

    Public Property FileExtension() As String
        Get
            Return ViewState("FileExtension")
        End Get
        Set(ByVal value As String)
            ViewState("FileExtension") = value
        End Set
    End Property

    Public Property dtCurrentControls() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

    Private Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Private Property ManagerName() As String
        Get
            Return ViewState("ManagerName")
        End Get
        Set(ByVal value As String)
            ViewState("ManagerName") = value
        End Set
    End Property

    Public Property IsAnnual() As Boolean
        Get
            Return ViewState("IsAnnual")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsAnnual") = value
        End Set
    End Property

    Private Property TotalBalance() As Integer
        Get
            Return ViewState("TotalBalance")
        End Get
        Set(ByVal value As Integer)
            ViewState("TotalBalance") = value
        End Set
    End Property

    Private Property Gender() As String
        Get
            Return ViewState("Gender")
        End Get
        Set(ByVal value As String)
            ViewState("Gender") = value
        End Set
    End Property

    Private Property IsFirstGrid() As String
        Get
            Return ViewState("IsFirstGrid")
        End Get
        Set(ByVal value As String)
            ViewState("IsFirstGrid") = value
        End Set
    End Property

    Public Property ApprovalRequired() As Boolean
        Get
            Return ViewState("ApprovalRequired")
        End Get
        Set(value As Boolean)
            ViewState("ApprovalRequired") = value
        End Set
    End Property

    Public Property EmpLeaveId() As String
        Get
            Return ViewState("EmpLeaveId")
        End Get
        Set(value As String)
            ViewState("EmpLeaveId") = value
        End Set
    End Property

    Public Property OffAndHolidayDays() As Integer
        Get
            Return ViewState("OffAndHolidayDays")
        End Get
        Set(value As Integer)
            ViewState("OffAndHolidayDays") = value
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

    Public Property FK_LeaveTypeId() As Integer
        Get
            Return ViewState("FK_LeaveTypeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_LeaveTypeId") = value
        End Set
    End Property

    Public Property Year() As Integer
        Get
            Return ViewState("Year")
        End Get
        Set(ByVal value As Integer)
            ViewState("Year") = value
        End Set
    End Property

#End Region

#Region "PageEvents"

    Protected Sub Page_PreRenderComplete(sender As Object, e As EventArgs) Handles Me.PreRenderComplete
        For Each row As DataRow In dtCurrentControls.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not Update1.FindControl(row("AddBtnName")) Is Nothing Then
                        Update1.FindControl(row("AddBtnName")).Visible = False
                        btnSave.Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not Update1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        Update1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not Update1.FindControl(row("EditBtnName")) Is Nothing Then
                        Update1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not Update1.FindControl(row("PrintBtnName")) Is Nothing Then
                        Update1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next
    End Sub

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

        If Not Page.IsPostBack Then

            HideShowViews()
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                dgrdEmpLeaveRequest.Columns(2).Visible = False
                dgrdEmpLeaveRequest.Columns(9).Visible = False
            Else
                Lang = CtlCommon.Lang.EN
                dgrdEmpLeaveRequest.Columns(3).Visible = False
                dgrdEmpLeaveRequest.Columns(10).Visible = False
            End If

            FillGridView()
            FillLeaveTypes()
            FillLeaveStatus()
            FillLeaveBalanceGrid()
            dtpRequestDate.SelectedDate = Date.Today
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            RequiredFieldValidator4.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            'rfvStatus.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            EmpLeaveRequestHeader.HeaderText = ResourceManager.GetString("EmpLeaveRequest", CultureInfo)

            dtpFromDateSearch.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            dtpToDateSearch.SelectedDate = dd
            ArcivingMonths_DateValidation()
        End If
        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdEmpLeaveRequest.ClientID + "');")
        Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        scriptManager__1.RegisterPostBackControl(lnbLeaveFile)

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)


        For Each row As DataRow In dtCurrentControls.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not Update1.FindControl(row("AddBtnName")) Is Nothing Then
                        Update1.FindControl(row("AddBtnName")).Visible = False
                        btnSave.Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not Update1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        Update1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not Update1.FindControl(row("EditBtnName")) Is Nothing Then
                        Update1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not Update1.FindControl(row("PrintBtnName")) Is Nothing Then
                        Update1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("../SelfServices/LeavesRequest.aspx?Id=1")
        'FillGridView()
        'mvEmpLeaverequest.SetActiveView(viewLeaveRequests)
    End Sub

    Protected Sub btnRequestLeave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRequestLeave.Click
        ClearAll()
        ControlsStatus(True)
        btnSave.Text = ResourceManager.GetString("btnSave", CultureInfo)
        mvEmpLeaverequest.SetActiveView(viewAddLeaveRequest)
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            Dim EmpLeaveTotalBalance As Double = 0
            OffAndHolidayDays = 0
            Dim ErrorMessage As String = String.Empty
            Dim ErrMessage As String = String.Empty
            objEmp_Leaves = New Emp_Leaves
            objEmp_LeavesRequest = New Emp_LeavesRequest
            objLeavesTypes = New LeavesTypes

            objLeavesTypes.LeaveId = ddlLeaveType.SelectedValue
            objLeavesTypes.GetByPK()

            If fuAttachFile.HasFile = False Then
                If objLeavesTypes.AttachmentIsMandatory Then
                    If Lang = CtlCommon.Lang.AR Then
                        CtlCommon.ShowMessage(Me.Page, "يرجى ارفاق ملف!", "info")
                    Else
                        CtlCommon.ShowMessage(Me.Page, "Please Attach File!", "info")
                    End If
                    Exit Sub
                End If
            End If

            If txtRemarks.Text.Trim() = "" Then
                If objLeavesTypes.RemarksIsMandatory = True Then
                    If Lang = CtlCommon.Lang.AR Then
                        CtlCommon.ShowMessage(Me.Page, "يرجى ادخال الملاحظات", "info")
                    Else
                        CtlCommon.ShowMessage(Me.Page, "Please Enter Remarks", "info")
                    End If
                    Exit Sub
                End If
            End If

            If Not objLeavesTypes.AllowedAfterDays = Nothing Then
                If dtpFromDate.SelectedDate > DateTime.Today.AddDays(objLeavesTypes.AllowedAfterDays) Then
                    If Lang = CtlCommon.Lang.AR Then
                        ErrMessage = "نوع الاجازة التي قمت باختيارها لايمكن طلبها لتاريخ بعد : " & objLeavesTypes.AllowedAfterDays & " أيام"
                        CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                    Else
                        ErrMessage = "The Selected Leave Type Not Allowed After : " & objLeavesTypes.AllowedAfterDays & " Day(s)"
                        CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                    End If

                    Exit Sub
                End If
            End If

            If Not objLeavesTypes.AllowedBeforeDays = Nothing Then
                If dtpFromDate.SelectedDate <= Date.Today.AddDays(objLeavesTypes.AllowedBeforeDays * -1) Then
                    If Lang = CtlCommon.Lang.AR Then
                        ErrMessage = "نوع الاجازة التي قمت باختيارها لايمكن طلبها لتاريخ قبل : " & objLeavesTypes.AllowedBeforeDays + " أيام "
                        CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                    Else
                        ErrMessage = "The Selected Leave Type Not Allowed Before : " & objLeavesTypes.AllowedBeforeDays & " Day(s)"
                        CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                    End If

                    Exit Sub
                End If
            End If

            If Not objLeavesTypes.MinLeaveApplyDay = Nothing Then
                If DateDiff(DateInterval.Day, Convert.ToDateTime(dtpRequestDate.SelectedDate), Convert.ToDateTime(dtpFromDate.SelectedDate)) < objLeavesTypes.MinLeaveApplyDay And DateDiff(DateInterval.Day, Convert.ToDateTime(dtpRequestDate.SelectedDate), Convert.ToDateTime(dtpFromDate.SelectedDate)) > 0 Then
                    If Lang = CtlCommon.Lang.AR Then
                        ErrMessage = "نوع الاجازة التي قمت بطلبها يجب ان تكون مطلوبة قبل " & objLeavesTypes.MinLeaveApplyDay & " أيام من تاريخ التقديم"
                        CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                    Else
                        ErrMessage = "The Selected Leave Type Should Be Applied Before " & objLeavesTypes.MinLeaveApplyDay & " Day(s) Before The Requested Date"
                        CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                    End If
                    Exit Sub
                End If

            End If

            Dim fileUploadExtension As String = ""
            If Not fuAttachFile.PostedFile Is Nothing Then
                fileUploadExtension = Path.GetExtension(fuAttachFile.PostedFile.FileName)
            End If



            If (objEmp_LeavesRequest.ValidateEmployeeLeaveRequest(objLeavesTypes, dtpFromDate.SelectedDate, dtpToDate.SelectedDate, LeaveRequestID, SessionVariables.LoginUser.FK_EmployeeId,
                                                    ddlLeaveType.SelectedValue, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
                If (ErrorMessage <> String.Empty) Then
                    CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                    Return
                End If

            Else
                Dim err As Integer = -1
                Dim strMessage As String = String.Empty
                objEmp_Leaves = New Emp_Leaves

                Dim OffOrHolidayDaysCount As Integer
                OffOrHolidayDaysCount = 0

                If OffAndHolidayDays > 0 Then
                    OffOrHolidayDaysCount = ((dtpToDate.SelectedDate - dtpFromDate.SelectedDate).Value.Days + 1) - OffAndHolidayDays
                Else
                    OffOrHolidayDaysCount = (dtpToDate.SelectedDate - dtpFromDate.SelectedDate).Value.Days + 1
                End If

                With objEmp_LeavesRequest
                    .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                    .FK_LeaveTypeId = ddlLeaveType.SelectedValue
                    .FromDate = dtpFromDate.DbSelectedDate
                    .ToDate = dtpToDate.DbSelectedDate
                    .RequestDate = dtpRequestDate.DbSelectedDate
                    .Remarks = txtRemarks.Text
                    .IsHalfDay = False ' chkHalfDay.Checked
                    .Days = OffOrHolidayDaysCount
                    .WithAdvancedSalary = chbWithAdvancedSalary.Checked
                    .RejectedReason = String.Empty
                    objLeavesTypes = New LeavesTypes
                    objLeavesTypes.LeaveId = ddlLeaveType.SelectedValue
                    objLeavesTypes.GetByPK()
                    .RequestedByCoordinator = False
                    If Not fuAttachFile.PostedFile Is Nothing Then
                        .AttachedFile = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                    End If
                    If LeaveRequestID = 0 Then
                        .CREATED_BY = SessionVariables.LoginUser.ID
                        If ApprovalRequired Then
                            .FK_StatusId = Convert.ToInt32(CtlCommon.RequestStatusEnum.Pending)
                            err = .Add()
                            strMessage = ResourceManager.GetString("SaveSuccessfully", CultureInfo)
                        Else
                            .FK_StatusId = Convert.ToInt32(CtlCommon.RequestStatusEnum.AutomaticApproved)
                            err = .Add()
                            SaveEmployeeLeave(strMessage)
                            ' strMessage = ResourceManager.GetString("SaveSuccessfully", CultureInfo)
                        End If

                    Else
                        '.AttachedFile = FileExtension
                        .LeaveId = LeaveRequestID
                        .FK_StatusId = Convert.ToInt32(CtlCommon.RequestStatusEnum.Pending)
                        .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                        err = .Update()

                        strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
                    End If

                    If err = 0 Then
                        If Not fileUploadExtension Is Nothing Then
                            'If fuAttachFile.HasFile Then
                            .AttachedFile = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                            Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                            Dim fileName As String = objEmp_LeavesRequest.LeaveId.ToString()
                            Dim fPath As String = String.Empty
                            fPath = Server.MapPath("..\LeaveRequestFiles\\" + fileName + extention)
                            fuAttachFile.PostedFile.SaveAs(fPath)
                            'Else
                            If Not ApprovalRequired Then
                                If (File.Exists(Server.MapPath("..\LeaveRequestFiles\\" + LeaveID.ToString() + extention))) Then

                                    Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("..\LeaveRequestFiles\\" + LeaveID.ToString() + extention))
                                    CopyFile.CopyTo(Server.MapPath("..\LeaveFiles\\" + LeaveID.ToString() + extention))

                                    Rename(Server.MapPath("..\LeaveFiles\\" + LeaveID.ToString() + extention),
                                                            Server.MapPath("..\LeaveFiles\\" + EmpLeaveId + extention))
                                End If
                            End If
                            'End If
                        Else
                            .AttachedFile = String.Empty
                        End If
                    End If

                    If err = 0 Then
                        'CtlCommon.ShowMessage(Me.Page, strMessage)
                        FillGridView()
                        If Lang = CtlCommon.Lang.AR Then
                            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Redirect", "ShowMessageAndRedirect('تم إرسال الطلب الى المدير المباشر','../SelfServices/LeavesRequest.aspx');", True)
                        Else
                            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Redirect", "ShowMessageAndRedirect('The Request has been Sent to Direct Manager','../SelfServices/LeavesRequest.aspx');", True)
                        End If
                        ClearAll()
                        'FillGridView()
                    Else
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
                    End If
                End With
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGet.Click
        objEmp_LeavesRequest = New Emp_LeavesRequest()
        objEmp_LeavesRequest.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        If ddlStatus.SelectedValue = -1 Then
            objEmp_LeavesRequest.FK_StatusId = 0
        Else
            objEmp_LeavesRequest.FK_StatusId = ddlStatus.SelectedValue
        End If
        objEmp_LeavesRequest.FromDate = dtpFromDateSearch.SelectedDate
        objEmp_LeavesRequest.ToDate = dtpToDateSearch.SelectedDate
        dgrdEmpLeaveRequest.DataSource = objEmp_LeavesRequest.GetByStatusType()
        dgrdEmpLeaveRequest.DataBind()

        FillLeaveBalanceGrid()
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Protected Sub dgrdEmpLeaveRequest_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdEmpLeaveRequest.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item
            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("StatusId").ToString())) And (Not item.GetDataKeyValue("StatusId").ToString() = "")) Then
                If item.GetDataKeyValue("StatusId") = 1 Then
                    Dim lnkLeaveForm As LinkButton = DirectCast(item.FindControl("lnkLeaveForm"), LinkButton)
                    lnkLeaveForm.Visible = True
                End If

                If ((item.GetDataKeyValue("StatusId") <> Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByGeneralManager)) And
                    (item.GetDataKeyValue("StatusId") <> Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByHumanResource)) And
                    (item.GetDataKeyValue("StatusId") <> Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedbyManager))) Then

                    item("RejectionReason").Text = String.Empty

                End If

            End If
        End If
    End Sub

    Protected Sub dgrdEmpLeaveRequest_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdEmpLeaveRequest.SelectedIndexChanged
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        mvEmpLeaverequest.SetActiveView(viewAddLeaveRequest)
        LeaveRequestID = Convert.ToInt32(DirectCast(dgrdEmpLeaveRequest.SelectedItems(0), GridDataItem).GetDataKeyValue("LeaveRequestId"))
        objEmp_LeavesRequest = New Emp_LeavesRequest
        With objEmp_LeavesRequest
            .LeaveId = LeaveRequestID
            .GetByPK()
            ddlLeaveType.SelectedValue = .FK_LeaveTypeId
            dtpFromDate.SelectedDate = .FromDate
            dtpToDate.SelectedDate = .ToDate
            dtpRequestDate.SelectedDate = .RequestDate
            txtRemarks.Text = .Remarks
            FileExtension = .AttachedFile
            chbWithAdvancedSalary.Checked = .WithAdvancedSalary
            If .WithAdvancedSalary Then
                trAdvanceSalary.Visible = True
            End If

            fuAttachFile.Visible = True
            lnbLeaveFile.Visible = True
            lnbRemove.Visible = True
            Dim fPath As String = "..\LeaveRequestFiles\" + LeaveRequestID.ToString() + .AttachedFile
            Dim fPathExist As String = String.Empty
            fPathExist = Server.MapPath("..\LeaveRequestFiles\\" + LeaveRequestID.ToString() + .AttachedFile)

            Select Case objEmp_LeavesRequest.FK_StatusId
                Case 1
                    ControlsStatus(False)
                Case 2
                    ControlsStatus(False)
                Case 3
                    ControlsStatus(False)
                Case 4
                    ControlsStatus(True)
            End Select

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

            btnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)
        End With
    End Sub

    Protected Sub dgrdEmpLeaveRequest_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmpLeaveRequest.NeedDataSource

        objEmp_LeavesRequest = New Emp_LeavesRequest()
        objEmp_LeavesRequest.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        objEmp_LeavesRequest.FromDate = DateSerial(Today.Year, Today.Month, 1)
        objEmp_LeavesRequest.ToDate = DateSerial(Today.Year, Today.Month + 1, 0)
        dtCurrent = objEmp_LeavesRequest.GetByEmployee()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdEmpLeaveRequest.DataSource = dv

    End Sub

    Protected Sub lnbRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnbRemove.Click
        If Not (LeaveRequestID = 0) Then
            Dim fileName As String = LeaveRequestID.ToString()
            Dim fPath As String = String.Empty
            fPath = Server.MapPath("..\LeaveRequestFiles\\" + fileName + FileExtension)

            If File.Exists(fPath) Then
                File.Delete(fPath)
                lnbLeaveFile.Visible = False
                lnbRemove.Visible = False
                lblNoAttachedFile.Visible = True
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            End If
        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        For Each Item As GridDataItem In dgrdEmpLeaveRequest.Items
            Dim cb As CheckBox = DirectCast(Item.FindControl("chk"), CheckBox)
            If cb.Checked Then

                ' Get Permission Id from hidden label
                Dim LeaveRequestId As Integer = Item.GetDataKeyValue("LeaveRequestId")

                ' Get Employee Id from hidden label
                Dim Status As Integer = Convert.ToInt32(Item.GetDataKeyValue("StatusId"))
                Dim AttachedFile As String
                If Not IsDBNull(Item.GetDataKeyValue("AttachedFile")) Then
                    AttachedFile = Item.GetDataKeyValue("AttachedFile")
                End If


                If (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.Pending) Or (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByGeneralManager)) Or
                    (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByHumanResource)) Or (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedbyManager))) Then
                    ' Delete current checked item
                    objEmp_LeavesRequest = New Emp_LeavesRequest
                    objEmp_LeavesRequest.LeaveId = LeaveRequestId
                    errNum = objEmp_LeavesRequest.Delete()

                    If errNum = 0 Then
                        Dim fileName As String = LeaveRequestId.ToString()
                        Dim fPath As String = String.Empty
                        fPath = Server.MapPath("..\LeaveRequestFiles\\" + fileName + AttachedFile)
                        If File.Exists(fPath) Then
                            File.Delete(fPath)
                        End If
                    End If
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SelectPendingStatus"), "info")
                    Return
                End If
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully"), "success")
            FillGridView()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete"), "error")
        End If
        ClearAll()
    End Sub

    Protected Sub ddlLeaveType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlLeaveType.SelectedIndexChanged
        If Not ddlLeaveType.SelectedValue = -1 Then
            objLeavesTypes = New LeavesTypes
            With objLeavesTypes
                .LeaveId = Convert.ToInt32(ddlLeaveType.SelectedValue)
                .GetByPK()
                If SessionVariables.CultureInfo = "ar-JO" Then
                    dvGeneralGuide.InnerHtml = .GeneralGuideAr
                Else
                    dvGeneralGuide.InnerHtml = .GeneralGuide
                End If

                GetRemainingBalance()
                If .ShowRemainingBalance = True Then
                    divRemaining.Visible = True
                Else
                    divRemaining.Visible = False
                End If
                lblRemainingBalanceValue.Text = TotalBalance

                dvGeneralGuide.Visible = True
                lblGeneralGuide.Visible = True

                IsAnnual = .IsAnnual
                ApprovalRequired = .ApprovalRequired
                WithAdvanceSalary()

            End With
        Else
            divRemaining.Visible = False
            dvGeneralGuide.Visible = False
            lblGeneralGuide.Visible = False
        End If


    End Sub

    Protected Sub lnkLeaveForm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        LeaveID = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("LeaveRequestId"))
        EmployeeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_EmployeeId"))

        '---Employee Signature Information---'
        objEmployee = New Employee
        objEmployee.EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        objEmployee.GetByPK()
        'If Lang = CtlCommon.Lang.AR Then
        '    ManagerName = objEmployee.EmployeeArabicName
        'Else
        ManagerName = objEmployee.EmployeeName
        'End If
        '---Employee Signature Information---'

        'Response.Redirect("../Reports/LeaveForm_ReportViewer.aspx?EmployeeId=" & EmployeeId & "&LeaveId=" & LeaveID & "&ManagerName=" & ManagerName & "&Signature=" & 1) '--- 1 =Employee Signature'

        Dim QueryString As String = "../Reports/LeaveForm_ReportViewer.aspx?EmployeeId=" & EmployeeId & "&LeaveId=" & LeaveID & "&ManagerName=" & ManagerName & "&Signature=" & 1
        Dim newWin As String = (Convert.ToString("window.open('") & QueryString) + "', 'popup_window', 'width=600,height=200,left=400,top=100,resizable=yes,scrollbars=1');"
        ClientScript.RegisterStartupScript(Me.GetType(), "pop", newWin, True)

    End Sub

    Protected Sub dtpFromDate_SelectedDateChanged(ByVal sender As Object, ByVal e As SelectedDateChangedEventArgs) Handles dtpFromDate.SelectedDateChanged
        WithAdvanceSalary()
    End Sub

    Protected Sub dtpToDate_SelectedDateChanged(ByVal sender As Object, ByVal e As SelectedDateChangedEventArgs) Handles dtpToDate.SelectedDateChanged
        WithAdvanceSalary()
    End Sub

    Protected Sub lnkDownloadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        objEmp_LeavesRequest = New Emp_LeavesRequest
        With objEmp_LeavesRequest
            .LeaveId = LeaveRequestID
            .GetByPK()

            Dim FileName As String = LeaveRequestID.ToString() + .AttachedFile
            Dim FilePath As String = Server.MapPath("..\\LeaveRequestFiles\") & FileName
            CtlCommon.Open_Download_File(FileName, FilePath)

        End With
    End Sub

    Private Sub dgrdLeaveBalance_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdLeaveBalance.NeedDataSource
        objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
        With objEmp_Leaves_BalanceHistory
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FromDate = dtpFromDateSearch.DbSelectedDate
            .ToDate = dtpToDateSearch.DbSelectedDate
            dgrdLeaveBalance.DataSource = .GetEmpAllLeave_LastBalance
        End With
    End Sub

    Protected Sub lnkDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FK_EmployeeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_EmployeeId").ToString())
        FK_LeaveTypeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_LeaveId").ToString())
        Year = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Year").ToString())

        Dim secururl As SecureUrl = Nothing
        Dim QueryString As String = "LeaveBalance_TimeLine.aspx?EmployeeId=" + FK_EmployeeId.ToString + "&FK_LeaveId=" + FK_LeaveTypeId.ToString + "&BalanceYear=" + Year.ToString
        secururl = New SecureUrl(QueryString)
        radwindowLeaveTimeline.NavigateUrl = secururl.ToString '"LeaveBalance_TimeLine.aspx?EmployeeId=" + FK_EmployeeId.ToString + "&FK_LeaveId=" + FK_LeaveTypeId.ToString + "&BalanceYear=" + Year.ToString

        Dim script As String = "function f(){$find(""" + radwindowLeaveTimeline.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)

    End Sub

#End Region

#Region "Methods"

    Private Sub GetRemainingBalance()
        objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
        With objEmp_Leaves_BalanceHistory
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FK_LeaveId = ddlLeaveType.SelectedValue
            .GetLastBalance()
            TotalBalance = .TotalBalance
        End With

    End Sub

    Private Sub FillGridView()
        Try

            objEmp_LeavesRequest = New Emp_LeavesRequest()
            objEmp_LeavesRequest.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            objEmp_LeavesRequest.FromDate = DateSerial(Today.Year, Today.Month, 1)
            objEmp_LeavesRequest.ToDate = DateSerial(Today.Year, Today.Month + 1, 0)
            dgrdEmpLeaveRequest.DataSource = objEmp_LeavesRequest.GetByStatusType()
            dgrdEmpLeaveRequest.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillLeaveTypes()
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        objProjectCommon = New ProjectCommon()
        Dim dt As DataTable = Nothing
        dt = Nothing
        objEmp_LeavesRequest = New Emp_LeavesRequest()
        objEmp_LeavesRequest.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        'dt = objLeavesTypes.GetAllForDDL
        dt = objEmp_LeavesRequest.GetAllowed_ForSelfServiceAndGrade
        GetEmployeeGender()
        If Not Gender = "f" Then
            For Each row In dt.Rows
                If row("LeaveId").ToString = (objAPP_Settings.FK_MaternityLeaveTypeId).ToString Then
                    row.Delete()
                End If
            Next
            dt.AcceptChanges()
        End If

        For Each row In dt.Rows
            If row("AllowedGender").ToString = 1 And Gender = "f" Then
                row.Delete()
            ElseIf row("AllowedGender").ToString = 2 And Gender = "m" Then
                row.Delete()
            End If
        Next
        dt.AcceptChanges()

        objEmployee = New Employee
        objEmployee.EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        objEmployee.GetByPK()

        For Each row In dt.Rows
            If row("AllowForSpecificEmployeeType") = True Then
                If row("FK_EmployeeTypeId").ToString <> objEmployee.FK_LogicalGroup Then
                    row.delete()
                End If
            End If
        Next
        dt.AcceptChanges()


        ProjectCommon.FillRadComboBox(ddlLeaveType, dt, "LeaveName",
                                     "LeaveArabicName", "LeaveId")
    End Sub

    Private Sub FillLeaveStatus()
        objRequestStatus = New RequestStatus()
        Dim dt As DataTable = Nothing
        dt = Nothing
        dt = objRequestStatus.GetAll
        ProjectCommon.FillRadComboBox(ddlStatus, dt, "StatusName",
                                     "StatusNameArabic", "StatusId")
    End Sub

    Sub ClearAll()
        txtRemarks.Text = String.Empty
        ddlLeaveType.SelectedIndex = -1
        dtpFromDate.Clear()
        dtpToDate.Clear()
        dtpRequestDate.SelectedDate = Date.Today
        'chkHalfDay.Checked = False
        LeaveRequestID = 0
        FillGridView()
        fuAttachFile.Visible = True
        lnbLeaveFile.Visible = False
        lnbRemove.Visible = False
        lblGeneralGuide.Text = String.Empty
        trAdvanceSalary.Visible = False
        chbWithAdvancedSalary.Checked = False
        divRemaining.Visible = False
    End Sub

    Private Sub ControlsStatus(ByVal Status As Boolean)
        ddlLeaveType.Enabled = Status
        dtpFromDate.Enabled = Status
        dtpToDate.Enabled = Status
        txtRemarks.Enabled = Status
        btnSave.Visible = Status
        btnClear.Visible = Status
        chbWithAdvancedSalary.Enabled = Status
    End Sub

    Private Sub WithAdvanceSalary()

        If Not ddlLeaveType.SelectedValue = -1 Then
            objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory()
            objEmp_Leaves_BalanceHistory.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            objEmp_Leaves_BalanceHistory.FK_LeaveId = ddlLeaveType.SelectedValue
            Dim dr As DataRow = objEmp_Leaves_BalanceHistory.GetLastBalance()
            Dim lastBalance As Integer = 0

            If Not dr Is Nothing Then
                If Not IsDBNull(dr("TotalBalance")) Then
                    lastBalance = dr("TotalBalance")
                End If

                Dim day As Integer

                If dtpFromDate.SelectedDate.HasValue AndAlso dtpToDate.SelectedDate.HasValue Then
                    day = dtpToDate.SelectedDate.Value.Subtract(dtpFromDate.SelectedDate).Days
                End If

                If IsAnnual AndAlso lastBalance >= 20 AndAlso day >= 20 Then
                    trAdvanceSalary.Visible = True
                Else
                    trAdvanceSalary.Visible = False
                End If
            End If
        End If

    End Sub

    Private Sub HideShowViews()
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            IsFirstGrid = .IsFirstGrid
            If Request.QueryString("id") = 0 Then
                If .IsFirstGrid Then
                    mvEmpLeaverequest.SetActiveView(viewLeaveRequests)
                Else
                    mvEmpLeaverequest.SetActiveView(viewAddLeaveRequest)
                End If
            Else
                mvEmpLeaverequest.SetActiveView(viewLeaveRequests)
            End If
        End With
    End Sub

    'Private Sub HideShowViews()
    '    objAPP_Settings = New APP_Settings
    '    With objAPP_Settings
    '        .GetByPK()
    '        IsFirstGrid = .IsFirstGrid

    '        If .IsFirstGrid Then
    '            mvEmpLeaverequest.SetActiveView(viewLeaveRequests)
    '        Else
    '            mvEmpLeaverequest.SetActiveView(viewAddLeaveRequest)
    '        End If

    '    End With
    'End Sub

    Private Sub GetEmployeeGender()
        objEmployee = New Employee
        With objEmployee
            .EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .GetByPK()
            Gender = .Gender
        End With
    End Sub

    Private Sub ArcivingMonths_DateValidation()
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        Dim ArchivingMonths As Integer = objAPP_Settings.ArchivingMonths

        If Not ArchivingMonths = 0 Then
            ArchivingMonths = ArchivingMonths
            Dim NewDate As Date = DateAdd(DateInterval.Month, ArchivingMonths, Date.Today)
            dtpFromDate.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
            dtpToDate.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
            dtpFromDateSearch.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
            dtpToDateSearch.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
        End If

    End Sub

    Sub SaveEmployeeLeave(ByVal ErrorMessage As String)

        objRECALC_REQUEST = New RECALC_REQUEST '----Immediate
        objRecalculateRequest = New RecalculateRequest '----Recalculate Request Table

        objAPP_Settings = New APP_Settings()
        objAPP_Settings = objAPP_Settings.GetByPK()

        Dim temp_str_date As String
        Dim err2 As Integer

        objEmp_Leaves.LeaveId = 0

        '----------------LEAVE BALANCE------------------'
        objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
        objEmp_Leaves_BalanceHistory.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        objEmp_Leaves_BalanceHistory.FK_EmpLeaveId = objEmp_LeavesRequest.FK_LeaveTypeId
        Dim dtLeaveBalance = objEmp_Leaves_BalanceHistory.GetEmpLeaveBalance()
        Dim EmpLeaveTotalBalance As Double = 0
        If (dtLeaveBalance IsNot Nothing And dtLeaveBalance.Rows.Count > 0) Then
            EmpLeaveTotalBalance = dtLeaveBalance.Rows(0)("TotalBalance")
        End If
        '----------------LEAVE BALANCE------------------'

        objEmp_Leaves.AddLeaveAllProcess(objEmp_LeavesRequest.FK_EmployeeId, OffAndHolidayDays, objEmp_LeavesRequest.FromDate, objEmp_LeavesRequest.ToDate, objEmp_LeavesRequest.FromDate,
       objEmp_LeavesRequest.ToDate, objEmp_LeavesRequest.RequestDate, objEmp_LeavesRequest.Remarks, objEmp_LeavesRequest.FK_LeaveTypeId, EmpLeaveTotalBalance, objEmp_LeavesRequest.AttachedFile, ErrorMessage, objEmp_LeavesRequest.LeaveId, True)

        EmpLeaveId = objEmp_Leaves.LeaveId.ToString()
        Dim dteFrom As DateTime = objEmp_Leaves.FromDate
        Dim dteTo As DateTime = objEmp_Leaves.ToDate

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
            If Not objEmp_Leaves.FromDate > Date.Today Then
                objRecalculateRequest = New RecalculateRequest
                With objRecalculateRequest
                    .Fk_EmployeeId = EmployeeId
                    .FromDate = objEmp_Leaves.FromDate
                    .ToDate = objEmp_Leaves.ToDate
                    .ImmediatelyStart = True
                    .RecalStatus = 0
                    .CREATED_BY = SessionVariables.LoginUser.ID
                    .Remarks = "Automatic Leave Request Approval - SYSTEM"
                    err2 = .Add
                End With
            End If
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

    Private Sub FillLeaveBalanceGrid()
        objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
        With objEmp_Leaves_BalanceHistory
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FromDate = dtpFromDateSearch.DbSelectedDate
            .ToDate = dtpToDateSearch.DbSelectedDate
            dgrdLeaveBalance.DataSource = .GetEmpAllLeave_LastBalance
            dgrdLeaveBalance.DataBind()
        End With
    End Sub
#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmpLeaveRequest.Skin))
    End Function

    Protected Sub dgrdEmpLeaveRequest_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdEmpLeaveRequest.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

#End Region

#Region "Balance Grid Filter"

    Protected Sub RadToolBar2_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon2() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdLeaveBalance.Skin))
    End Function

    Protected Sub dgrdLeaveBalance_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdLeaveBalance.ItemCommand
        If e.CommandName = "FilterRadGrid1" Then
            RadFilter2.FireApplyCommand()
        End If
    End Sub

#End Region





End Class
