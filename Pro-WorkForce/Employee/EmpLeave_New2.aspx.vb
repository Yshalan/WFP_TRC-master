Imports TA.Employees
Imports TA.Definitions
Imports SmartV.UTILITIES
Imports System.Data
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports Telerik.Web.UI
Imports TA.LookUp
Imports TA.Admin
Imports System.Web.UI
Imports SmartV.UTILITIES.ProjectCommon
Imports System.IO
Imports TA.Security
Imports TA.DailyTasks
Imports TA.SelfServices

Partial Class EmpLeave_New
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_Leaves As Emp_Leaves
    Private objOrgEntity As OrgEntity
    Private objEmployee As Employee
    Private objOrgCompany As OrgCompany
    Private objLeavesTypes As LeavesTypes
    Private objProjectCommon As New ProjectCommon
    Private objLeaveTypeOccurance As New LeaveTypeOccurance
    Private objWorkSchedule_NormalTime As WorkSchedule_NormalTime
    Private objWorkSchedule_Flexible As WorkSchedule_Flexible
    Private objEmp_Shifts As Emp_Shifts
    Private objHoliday As New Holiday
    Private objEmp_Leaves_BalanceHistory As New Emp_Leaves_BalanceHistory
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Public strLang As String
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objEmp_LeavesRequest As Emp_LeavesRequest
    Private objAPP_Settings As APP_Settings
    Private objRecalculateRequest As RecalculateRequest

#End Region

#Region "Public Properties"

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

    Public Property dtOffDays() As DataTable
        Get
            Return ViewState("dtOffDays")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtOffDays") = value
        End Set
    End Property

    Public Property AllowedOccurancedt() As DataTable
        Get
            Return ViewState("AllowedOccurancedt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("AllowedOccurancedt") = value
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

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
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

    Public Property IsViolationsCorrection() As Boolean
        Get
            Return ViewState("IsViolationsCorrection")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsViolationsCorrection") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Public Sub FillDataHREmployeeViolations()
        objEmployee = New Employee
        Dim EmployeeId As Integer
        Dim ViolationDate As DateTime
        EmployeeId = Request.QueryString("EmployeeId")
        ViolationDate = Convert.ToDateTime(Request.QueryString("ViolationDate"))

        If EmployeeId <> 0 Then
            ''fill Employee Filter
            EmployeeFilterUC.EmployeeId = EmployeeId
            EmployeeFilterUC.IsEntityClick = "True"
            EmployeeFilterUC.GetEmployeeInfo(EmployeeId)
            EmployeeFilterUC.EmployeeId = EmployeeId
            EmployeeFilterUC.EnabledDisbaledControls(False)
            dtpFromDate.SelectedDate = ViolationDate.Date
            dtpToDate.SelectedDate = ViolationDate.Date
            dtpFromDate.Enabled = False
            dtpToDate.Enabled = False
            dtpRequestDate.Enabled = False
            ''''

            ''fill grid 
            objEmp_Leaves = New Emp_Leaves
            With objEmp_Leaves
                If EmployeeFilterUC.EmployeeId <> 0 Then
                    .FK_EmployeeId = EmployeeFilterUC.EmployeeId
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PleaseSelectEmployee", CultureInfo), "info")
                    Return
                End If
                .FromDate = dtpFromDateSearch.SelectedDate
                .ToDate = dtpToDateSearch.SelectedDate
                EmpDt = .GetAllLeavesByEmployee()

                grdEmpLeaves.DataSource = EmpDt
                grdEmpLeaves.DataBind()
            End With
            ''''

            ''hide clear & delete button
            btnClear.Visible = False
            btnDelete.Visible = False
            ''''

            IsViolationsCorrection = True

        End If




    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else

            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                strLang = "ar"
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                Lang = CtlCommon.Lang.EN
                strLang = "en"
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo

        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            EmployeeFilterUC.IsEmployeeRequired = True
            EmployeeFilterUC.CompanyRequiredValidationGroup = btnSave.ValidationGroup
            EmployeeFilterUC.EmployeeRequiredValidationGroup = btnSave.ValidationGroup

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("EmpLeave", CultureInfo)
            lblConfirmed.Text = ResourceManager.GetString("LeaveSufficient", CultureInfo)
            RequiredFieldValidator4.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            dtpRequestDate.SelectedDate = Date.Today
            FillDropDown()

            dtpFromDateSearch.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            dtpToDateSearch.SelectedDate = dd


            FillDataHREmployeeViolations()

            ArcivingMonths_DateValidation()
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + grdEmpLeaves.ClientID + "');")

        'btnSave.Attributes.Add("onclick", "javascript:return confirmSufficientSave();")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not Update1.FindControl(row("AddBtnName")) Is Nothing Then
                        Update1.FindControl(row("AddBtnName")).Visible = False
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

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'If Not String.IsNullOrEmpty(FileExtension) Then
        objLeavesTypes = New LeavesTypes
        Dim ErrMessage As String = String.Empty

        With objLeavesTypes
            .LeaveId = ddlLeaveType.SelectedValue
            .GetByPK()
            If fuAttachFile.HasFile = False Then
                If .AttachmentIsMandatory = True Then
                    If Lang = CtlCommon.Lang.AR Then
                        CtlCommon.ShowMessage(Me.Page, "يرجى ارفاق ملف!", "info")
                    Else
                        CtlCommon.ShowMessage(Me.Page, "Please Attach File!", "info")
                    End If
                    Exit Sub
                End If
            End If
            If txtRemarks.Text.Trim() = "" Then
                If .RemarksIsMandatory = True Then
                    If Lang = CtlCommon.Lang.AR Then
                        CtlCommon.ShowMessage(Me.Page, "يرجى ادخال الملاحظات", "info")
                    Else
                        CtlCommon.ShowMessage(Me.Page, "Please Enter Remarks", "info")
                    End If
                    Exit Sub
                End If
            End If

            If Not objLeavesTypes.AllowedAfterDays = Nothing Then
                If dtpFromDate.SelectedDate > DateTime.Today.AddDays(.AllowedAfterDays) Then
                    If Lang = CtlCommon.Lang.AR Then
                        ErrMessage = "نوع الاجازة التي قمت باختيارها لايمكن طلبها لتاريخ بعد : " & .AllowedAfterDays & " أيام"
                        CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                    Else
                        ErrMessage = "The Selected Leave Type Not Allowed After : " & .AllowedAfterDays & " Day(s)"
                        CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                    End If

                    Exit Sub
                End If
            End If

            If Not objLeavesTypes.AllowedBeforeDays = Nothing Then
                If dtpFromDate.SelectedDate <= Date.Today.AddDays(.AllowedBeforeDays * -1) Then
                    If Lang = CtlCommon.Lang.AR Then
                        ErrMessage = "نوع الاجازة التي قمت باختيارها لايمكن طلبها لتاريخ قبل : " & .AllowedBeforeDays + " أيام "
                        CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                    Else
                        ErrMessage = "The Selected Leave Type Not Allowed Before : " & .AllowedBeforeDays & " Day(s)"
                        CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                    End If

                    Exit Sub
                End If
            End If

        End With
        If fuAttachFile.HasFile Then
            FileExtension = Path.GetExtension(fuAttachFile.PostedFile.FileName)
            FileName = Path.GetFileName(fuAttachFile.PostedFile.FileName).ToString()

            Dim fPath As String = String.Empty
            'If LeaveID = 0 Then
            fPath = Server.MapPath("..\LeaveFiles\\" + FileName)
            If File.Exists(fPath) Then
                File.Delete(fPath)
                fuAttachFile.PostedFile.SaveAs(fPath)
            Else
                fuAttachFile.PostedFile.SaveAs(fPath)
            End If
            'Else
            'fPath = Server.MapPath("..\LeaveFiles\\" + FileName + FileExtension)
            'If File.Exists(fPath) Then
            'File.Delete(fPath)
            'fuAttachFile.PostedFile.SaveAs(fPath)
            'End If
            'End If
        End If
        'End If

        '--------If User Want Comfirmation Message When Leave Balance Insufficiant--------'
        'If hdnLeaveSufficient.Value = "True" Then
        '    mpeRejectPopup.Show()
        'Else
        If (ValidateEmployeeLeaveRequest(dtpFromDate.SelectedDate, dtpToDate.SelectedDate, EmployeeFilterUC.EmployeeId, ddlLeaveType.SelectedValue, ErrMessage) = False) Then
            If (ErrMessage <> String.Empty) Then
                CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                Return
            End If

        Else
            SaveEmpLeave()
        End If
        '--------If User Want Comfirmation Message When Leave Balance Insufficiant--------'
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'ddlEmployee.SelectedIndex = -1
        ClearAll()
        Dim dtClear As New DataTable
        grdEmpLeaves.DataSource = dtClear
        grdEmpLeaves.DataBind()
        EmployeeFilterUC.ClearValues()
    End Sub

    Protected Sub grdEmpLeaves_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdEmpLeaves.NeedDataSource
        grdEmpLeaves.DataSource = EmpDt

    End Sub
    Function ValidateEmployeeLeaveRequest(ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal EmployeeId As Integer, ByVal LeaveType As Integer, ByRef ErrorMessage As String) As Boolean


        If IsExists(FromDate, ToDate, FK_LeaveTypeId, EmployeeId, LeaveID) = True Then
            If SessionVariables.CultureInfo = "en-US" Then
                ErrorMessage = "Leave Record Already Exists Between The Date Range"
            Else
                ErrorMessage = "الاجازة موجودة مسبقاً في حدود التاريخ"
            End If
            Return False

        ElseIf IsExistsRequest(FromDate, ToDate, LeaveID, EmployeeId) = True Then
            If SessionVariables.CultureInfo = "en-US" Then
                ErrorMessage = "Leave Request Already Exists Between The Date Range"
            Else
                ErrorMessage = "طلب الاجازة موجودة مسبقاً في حدود التاريخ"
            End If
            Return False

            Return False

            'ElseIf Not LeaveRequestValidation(EmployeeId, FromDate, ToDate, objLeavesTypes, LeaveType, LeaveId, ErrorMessage, OffAndHolidayDays, LeaveTotalBalance) Then
            '    Return False
        Else
            Return True
        End If

    End Function


    Protected Sub grdEmpLeaves_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdEmpLeaves.SelectedIndexChanged
        LeaveID = Convert.ToInt32(DirectCast(grdEmpLeaves.SelectedItems(0), GridDataItem).GetDataKeyValue("LeaveId").ToString())
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objEmp_Leaves = New Emp_Leaves
        With objEmp_Leaves
            .LeaveId = LeaveID
            .GetByPK()
            ddlLeaveType.SelectedValue = .FK_LeaveTypeId
            ValidateLeaveSufficient()
            dtpFromDate.SelectedDate = .FromDate
            dtpToDate.SelectedDate = .ToDate
            dtpRequestDate.SelectedDate = .RequestDate
            txtRemarks.Text = .Remarks
            FileExtension = .AttachedFile
            fuAttachFile.Visible = True
            lnbLeaveFile.Visible = True
            lnbRemove.Visible = True
            Dim fPath As String = "..\LeaveFiles\" + LeaveID.ToString() + .AttachedFile
            Dim fPathExist As String = String.Empty
            fPathExist = Server.MapPath("..\LeaveFiles\\" + LeaveID.ToString() + .AttachedFile)
            'fPath = Server.MapPath("..\LeaveFiles\\" + LeaveID.ToString() + .AttachedFile)


            btnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)

            If String.IsNullOrEmpty(.AttachedFile) Then
                lnbLeaveFile.Visible = False
                lnbRemove.Visible = False
                lblNoAttachedFile.Visible = True
            ElseIf File.Exists(fPathExist) Then
                lnbLeaveFile.HRef = fPath
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

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1
        Dim intleaveDays As Double
        Dim intleavetype As Integer
        objEmp_LeavesRequest = New Emp_LeavesRequest
        objRECALC_REQUEST = New RECALC_REQUEST
        Dim temp_str_date As String
        Dim err2 As Integer

        For Each Item As GridDataItem In grdEmpLeaves.Items
            Dim cb As CheckBox = DirectCast(Item.FindControl("chk"), CheckBox)
            If cb.Checked Then

                ' Get Permission Id from hidden label
                Dim LevelId As Integer = Item.GetDataKeyValue("LeaveId")

                ' Get Employee Id from hidden label
                Dim EmployeeId As Integer = Item.GetDataKeyValue("FK_EmployeeId")

                ' Delete current Leave and Leave Request
                objEmp_Leaves = New Emp_Leaves()
                objEmp_Leaves.LeaveId = LevelId
                objEmp_Leaves.GetByPK()

                Dim LeaveRequestId As Integer = objEmp_Leaves.LeaveRequestId

                intleaveDays = objEmp_Leaves.Days
                intleavetype = objEmp_Leaves.FK_LeaveTypeId
                errNum = objEmp_Leaves.Delete()

                If errNum = 0 Then
                    objEmp_LeavesRequest.LeaveId = LeaveRequestId
                    objEmp_LeavesRequest.Delete()
                End If

                If (errNum = 0) Then
                    'Delete from balance
                    objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
                    objEmp_Leaves_BalanceHistory.FK_EmployeeId = EmployeeId
                    objEmp_Leaves_BalanceHistory.FK_LeaveId = intleavetype
                    objEmp_Leaves_BalanceHistory.FK_EmpLeaveId = LevelId

                    objEmp_Leaves_BalanceHistory.GetLastBalance()

                    objEmp_Leaves_BalanceHistory.BalanceDate = Date.Now
                    objEmp_Leaves_BalanceHistory.Balance = intleaveDays
                    objEmp_Leaves_BalanceHistory.TotalBalance = objEmp_Leaves_BalanceHistory.TotalBalance + intleaveDays
                    objEmp_Leaves_BalanceHistory.Remarks = "Delete Leave Data - Rollback Balance"
                    objEmp_Leaves_BalanceHistory.CREATED_DATE = Date.Now
                    objEmp_Leaves_BalanceHistory.CREATED_BY = SessionVariables.LoginUser.ID
                    objEmp_Leaves_BalanceHistory.Add()

                    Dim dteFrom As DateTime = Convert.ToDateTime(Item.GetDataKeyValue("FromDate"))
                    Dim dteTo As DateTime = Convert.ToDateTime(Item.GetDataKeyValue("ToDate"))

                    objAPP_Settings = New APP_Settings()
                    objAPP_Settings = objAPP_Settings.GetByPK()

                    If objAPP_Settings.ApprovalRecalMethod = 1 Then
                        While dteFrom <= dteTo
                            If Not dteFrom = Date.Now.AddDays(1).ToShortDateString() Then
                                temp_str_date = DateToString(dteFrom)
                                objRECALC_REQUEST = New RECALC_REQUEST()
                                objRECALC_REQUEST.EMP_NO = EmployeeFilterUC.EmployeeId
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
                                .Fk_EmployeeId = EmployeeFilterUC.EmployeeId
                                .FromDate = dteFrom
                                .ToDate = dteTo
                                .ImmediatelyStart = True
                                .RecalStatus = 0
                                .CREATED_BY = SessionVariables.LoginUser.ID
                                .Remarks = "Delete Employee Leave - SYSTEM"
                                err2 = .Add
                            End With
                        End If
                    End If
                End If
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillEmpLeaveGrid()
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")

        End If
        ClearAll()
        EmployeeFilterUC.ClearValues()
    End Sub

    Protected Sub grdEmpLeaves_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles grdEmpLeaves.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("RequestDate").ToString())) And (Not item.GetDataKeyValue("RequestDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("RequestDate").ToString()
                item("RequestDate").Text = fromDate.ToString("dd/MM/yyyy")
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromDate").ToString())) And (Not item.GetDataKeyValue("FromDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("FromDate").ToString()
                item("FromDate").Text = fromDate.ToString("dd/MM/yyyy")
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ToDate").ToString())) And (Not item.GetDataKeyValue("ToDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("ToDate").ToString()
                item("ToDate").Text = fromDate.ToString("dd/MM/yyyy")
            End If

            If Lang = CtlCommon.Lang.AR Then
                item("LeaveName").Text = DirectCast(item.FindControl("hdnLeaveArabicType"), HiddenField).Value
            End If

        End If
    End Sub

    Protected Sub btnGet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGet.Click
        objEmp_Leaves = New Emp_Leaves
        With objEmp_Leaves
            If EmployeeFilterUC.EmployeeId <> 0 Then
                .FK_EmployeeId = EmployeeFilterUC.EmployeeId
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PleaseSelectEmployee", CultureInfo), "info")
                Return
            End If
            .FromDate = dtpFromDateSearch.SelectedDate
            .ToDate = dtpToDateSearch.SelectedDate
            EmpDt = .GetAllLeavesByEmployee()

            grdEmpLeaves.DataSource = EmpDt
            grdEmpLeaves.DataBind()
        End With
    End Sub

    Protected Sub lnbRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnbRemove.Click
        If Not (LeaveID = 0) Then
            Dim fileName As String = LeaveID.ToString()
            Dim fPath As String = String.Empty
            fPath = Server.MapPath("..\LeaveFiles\\" + fileName + FileExtension)

            If File.Exists(fPath) Then
                File.Delete(fPath)
                lnbLeaveFile.Visible = False
                lnbRemove.Visible = False
                lblNoAttachedFile.Visible = True
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            End If
        End If
    End Sub

    Protected Sub ddlLeaveType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlLeaveType.SelectedIndexChanged
        ValidateLeaveSufficient()
        FK_LeaveTypeId = ddlLeaveType.SelectedValue
    End Sub

    Protected Sub btnConfirm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConfirm.Click
        SaveEmpLeave()
    End Sub

#End Region

#Region "Methods"

    Function IsExists(ByVal Fromdate As Date, ByVal Todate As Date, ByVal FK_LeaveTypeId As Integer, ByVal EmployeeId As Integer, ByVal LeaveId As Integer) As Boolean
        Dim EmpDt As DataTable = New DataTable
        Dim objEmp_LeavesRequest As New Emp_LeavesRequest
        objEmp_Leaves = New Emp_Leaves

        objEmp_Leaves.FK_EmployeeId = EmployeeId
        objEmp_Leaves.FromDate = Fromdate
        objEmp_Leaves.ToDate = Todate
        objEmp_Leaves.LeaveId = LeaveId
        EmpDt = objEmp_Leaves.GetAllLeavesByEmployee()

        'to check if duplicate data in leave request table
        If EmpDt IsNot Nothing Then
            If EmpDt.Rows.Count = 0 Then
                objEmp_LeavesRequest.FK_EmployeeId = EmployeeId
                objEmp_LeavesRequest.FK_LeaveTypeId = FK_LeaveTypeId
                EmpDt = objEmp_LeavesRequest.GetByEmployee()
            End If
        End If

        Dim status As Boolean
        For Each dr As DataRow In EmpDt.Rows
            If FK_LeaveTypeId = dr("LeaveId") Then
                Continue For
            End If
            If Fromdate >= dr("FromDate") And Fromdate <= dr("ToDate") Or Todate >= dr("FromDate") And Todate <= dr("ToDate") Then
                status = True
            End If
        Next
        Return status
    End Function

    Function IsExistsRequest(ByVal Fromdate As Date, ByVal Todate As Date, ByVal LeaveID As Integer, ByVal EmployeeId As Integer) As Boolean
        Dim EmpDt As DataTable = New DataTable
        Dim objEmp_LeavesRequest As New Emp_LeavesRequest

        'to check if duplicate data in leave request table

        objEmp_LeavesRequest.FK_EmployeeId = EmployeeId
        EmpDt = objEmp_LeavesRequest.GetByEmployee()

        Dim status As Boolean
        Dim requestStatus As Integer
        For Each dr As DataRow In EmpDt.Rows
            requestStatus = Convert.ToInt32(dr("FK_StatusId"))
            If LeaveID = dr("LeaveId") Then
                Continue For
            End If

            If (Fromdate >= dr("FromDate") And Fromdate <= dr("ToDate")) Or (Todate >= dr("FromDate") And Todate <= dr("ToDate")) Then
                If (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByGeneralManager)) _
                    Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByHumanResource)) _
                    Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedbyManager)) Then
                    Continue For
                Else
                    status = True
                End If
            End If
        Next
        Return status
    End Function




    'Function IsExists(ByVal Fromdate As Date, ByVal Todate As Date, ByVal LeaveID As Integer) As Boolean
    '    Dim status As Boolean
    '    For Each dr As DataRow In EmpDt.Rows
    '        If LeaveID = dr("LeaveId") Then
    '            Continue For
    '        End If
    '        If Fromdate >= dr("FromDate") And Fromdate <= dr("ToDate") Or Todate >= dr("FromDate") And Todate <= dr("ToDate") Then
    '            status = True
    '        End If

    '    Next
    '    Return status
    'End Function

    Sub ClearAll()
        txtRemarks.Text = String.Empty
        ddlLeaveType.SelectedIndex = -1
        dtpFromDate.Clear()
        dtpToDate.Clear()
        dtpRequestDate.SelectedDate = Date.Today
        'chkHalfDay.Checked = False
        LeaveID = 0
        fuAttachFile.Visible = True
        lnbLeaveFile.Visible = False
        lnbRemove.Visible = False
        FillEmpLeaveGrid()
        btnSave.Text = ResourceManager.GetString("btnSave", CultureInfo)
        lblNoAttachedFile.Visible = False
    End Sub

    Sub FillEmpLeaveGrid()
        objEmp_Leaves = New Emp_Leaves
        With objEmp_Leaves
            If EmployeeFilterUC.EmployeeId <> 0 Then
                .FK_EmployeeId = EmployeeFilterUC.EmployeeId
            End If
            .FromDate = DateSerial(Today.Year, Today.Month, 1)
            .ToDate = DateSerial(Today.Year, Today.Month + 1, 0)
            EmpDt = .GetAllLeavesByEmployee()

            grdEmpLeaves.DataSource = EmpDt
            grdEmpLeaves.DataBind()
        End With
    End Sub

    Sub FillDropDown()
        objProjectCommon = New ProjectCommon()
        Dim dt As DataTable = Nothing
        dt = Nothing
        objLeavesTypes = New LeavesTypes()
        dt = objLeavesTypes.GetAllForDDL
        ProjectCommon.FillRadComboBox(ddlLeaveType, dt, "LeaveName", _
                                     "LeaveArabicName", "LeaveId")

    End Sub

    Sub FillEmployee(Optional ByVal type As Integer = 0)
        Dim dt As DataTable
        If type = 0 Then
            objOrgCompany = New OrgCompany
            With objOrgCompany
                .CompanyId = EmployeeFilterUC.CompanyId
                dt = .GetEmployeesByOrgCompany()
            End With
        Else
            objOrgEntity = New OrgEntity()
            With objOrgEntity
                .EntityId = EmployeeFilterUC.EntityId
                dt = .GetEmployeesByOrgEntity
            End With

        End If
        If dt.Rows.Count > 0 Then
            'ProjectCommon.FillRadComboBox(ddlEmployee, dt, "EmployeeName", "EmployeeName", "EmployeeId")
        Else
            'ddlEmployee.Items.Clear()
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

    Private Sub SaveEmpLeave()

        Dim EmpLeaveTotalBalance As Double = 0
        Dim OffAndHolidayDays As Integer = 0
        Dim ErrorMessage As String = String.Empty
        objEmp_Leaves = New Emp_Leaves
        objLeavesTypes = New LeavesTypes
        objRECALC_REQUEST = New RECALC_REQUEST
        Dim temp_str_date As String
        Dim err2 As Integer

        If btnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo) Then 'ID: M01 || Date: 18-04-2023 || By: Yahia shalan || Description: If user wants to update his leave or vacation system should rollback the balance to get the original balance for the user before updating the balance'
            Dim errNum As Integer = -1
            Dim intleaveDays As Double
            Dim intleavetype As Integer
            objEmp_LeavesRequest = New Emp_LeavesRequest
            objRECALC_REQUEST = New RECALC_REQUEST

            For Each Item As GridDataItem In grdEmpLeaves.Items
                Dim LevelId As Integer = Item.GetDataKeyValue("LeaveId")
                Dim EmployeeId As Integer = Item.GetDataKeyValue("FK_EmployeeId")

                ' Delete current Leave and Leave Request
                objEmp_Leaves = New Emp_Leaves()
                objEmp_Leaves.LeaveId = LevelId
                objEmp_Leaves.GetByPK()

                Dim LeaveRequestId As Integer = objEmp_Leaves.LeaveRequestId

                intleaveDays = objEmp_Leaves.Days
                intleavetype = objEmp_Leaves.FK_LeaveTypeId

                'If errNum = 0 Then
                objEmp_LeavesRequest.LeaveId = LeaveRequestId
                objEmp_LeavesRequest.Delete()
                'End If

                'If (errNum = 0) Then
                'Delete from balance
                objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
                objEmp_Leaves_BalanceHistory.FK_EmployeeId = EmployeeId
                objEmp_Leaves_BalanceHistory.FK_LeaveId = intleavetype
                objEmp_Leaves_BalanceHistory.FK_EmpLeaveId = LevelId

                objEmp_Leaves_BalanceHistory.GetLastBalance()

                objEmp_Leaves_BalanceHistory.BalanceDate = Date.Now
                objEmp_Leaves_BalanceHistory.Balance = intleaveDays
                objEmp_Leaves_BalanceHistory.TotalBalance = objEmp_Leaves_BalanceHistory.TotalBalance + intleaveDays
                objEmp_Leaves_BalanceHistory.Remarks = "Delete Leave Data - Rollback Balance"
                objEmp_Leaves_BalanceHistory.CREATED_DATE = Date.Now
                objEmp_Leaves_BalanceHistory.CREATED_BY = SessionVariables.LoginUser.ID
                objEmp_Leaves_BalanceHistory.Add()
            Next
        End If

        If (objEmp_Leaves.ValidateEmployeeLeave(objLeavesTypes, dtpFromDate.SelectedDate, dtpToDate.SelectedDate, ddlLeaveType.SelectedValue, EmployeeFilterUC.EmployeeId,
                                                 ErrorMessage, OffAndHolidayDays, LeaveID, EmpLeaveTotalBalance) = False) Then
            If (ErrorMessage <> String.Empty) Then
                CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
            End If

        Else

            objEmp_Leaves.LeaveId = LeaveID
            Dim fileUploadExtension As String = Nothing
            If fuAttachFile.HasFile Then
                fileUploadExtension = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                If String.IsNullOrEmpty(fileUploadExtension) Then
                    fileUploadExtension = FileExtension
                End If
            End If


            objEmp_Leaves.AddLeaveAllProcess(EmployeeFilterUC.EmployeeId, OffAndHolidayDays, dtpFromDate.SelectedDate, dtpToDate.SelectedDate, dtpFromDate.DbSelectedDate,
            dtpToDate.DbSelectedDate, dtpRequestDate.DbSelectedDate, txtRemarks.Text, ddlLeaveType.SelectedValue, EmpLeaveTotalBalance, fileUploadExtension, ErrorMessage, Nothing, False)

            CtlCommon.ShowMessage(Me.Page, ErrorMessage, "success")

            'If LeaveID = 0 Then
            Dim fPath As String = String.Empty
            fPath = Server.MapPath("..\LeaveFiles\\" + objEmp_Leaves.LeaveId.ToString() + FileExtension)
            Dim fPathExist As String = String.Empty
            fPathExist = Server.MapPath("..\LeaveFiles\\" + FileName)

            If File.Exists(fPathExist) Then
                File.Delete(fPath)
                Rename(fPathExist, fPath)

                'My.Computer.FileSystem.RenameFile(fPathExist, objEmp_Leaves.LeaveId.ToString() + FileExtension)
            End If

            'End If

            'If (LeaveID = 0) Then
            '    Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
            '    Dim fileName As String = objEmp_Leaves.LeaveId.ToString()
            '    Dim fPath As String = String.Empty
            '    fPath = Server.MapPath("..\LeaveFiles\\" + fileName + extention)
            '    fuAttachFile.PostedFile.SaveAs(fPath)
            'Else
            '    Dim fileName As String = objEmp_Leaves.LeaveId.ToString()
            '    Dim fPath As String = String.Empty
            '    fPath = Server.MapPath("..\LeaveFiles\\" + fileName + FileExtension)
            '    If File.Exists(fPath) Then
            '        Dim extention As String = FileExtension
            '        File.Delete(fPath)
            '        fPath = Server.MapPath("..\LeaveFiles\\" + fileName + extention)
            '        fuAttachFile.PostedFile.SaveAs(fPath)
            '    Else
            '        Dim extention As String = FileExtension
            '        Dim fileNameExist As String = objEmp_Leaves.LeaveId.ToString()
            '        Dim fPathExist As String = String.Empty
            '        fPathExist = Server.MapPath("..\LeaveFiles\\" + fileNameExist + extention)
            '        fuAttachFile.PostedFile.SaveAs(fPathExist)
            '    End If
            'End If

            Dim dteFrom As DateTime = dtpFromDate.SelectedDate
            Dim dteTo As DateTime = dtpToDate.SelectedDate

            objAPP_Settings = New APP_Settings()
            objAPP_Settings = objAPP_Settings.GetByPK()

            If objAPP_Settings.ApprovalRecalMethod = 1 Then
                While dteFrom <= dteTo
                    If Not dteFrom = Date.Now.AddDays(1).ToShortDateString() Then
                        temp_str_date = DateToString(dteFrom)
                        objRECALC_REQUEST = New RECALC_REQUEST()
                        objRECALC_REQUEST.EMP_NO = EmployeeFilterUC.EmployeeId
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
                        .Fk_EmployeeId = EmployeeFilterUC.EmployeeId
                        .FromDate = dteFrom
                        .ToDate = dteTo
                        .ImmediatelyStart = True
                        .RecalStatus = 0
                        .CREATED_BY = SessionVariables.LoginUser.ID
                        .Remarks = "Employee Leave - SYSTEM"
                        err2 = .Add
                    End With
                End If
            End If


            FillEmpLeaveGrid()
            ClearAll()
            If IsViolationsCorrection = True Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType, Guid.NewGuid.ToString, "RefreshPage()", True)
                'Response.Redirect("../DailyTasks/HR_EmployeeViolations.aspx")
            End If
        End If

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

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", grdEmpLeaves.Skin))
    End Function

    Protected Sub grdEmpLeaves_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdEmpLeaves.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

#End Region

End Class
