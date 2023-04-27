Imports TA.Employees
Imports TA.Definitions
Imports SmartV.UTILITIES
Imports System.Data
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports Telerik.Web.UI
Imports TA.Lookup
Imports TA.Admin
Imports System.Web.UI
Imports SmartV.UTILITIES.ProjectCommon
Imports System.IO
Imports TA.Security
Imports TA.DailyTasks

Partial Class Employee_HR_LeaveRequest
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objHR_LeaveRequest As HR_LeaveRequest
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

#End Region

#Region "Page Events"

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
            PageHeader1.HeaderText = ResourceManager.GetString("HRLeaveRequest", CultureInfo)
            lblConfirmed.Text = ResourceManager.GetString("LeaveSufficient", CultureInfo)
            RequiredFieldValidator4.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            dtpRequestDate.SelectedDate = Date.Today
            FillDropDown()

            dtpFromDateSearch.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            dtpToDateSearch.SelectedDate = dd

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


        If hdnLeaveSufficient.Value = "True" Then
            mpeRejectPopup.Show()
        Else
            SaveEmpLeave()
        End If
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

    Protected Sub grdEmpLeaves_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdEmpLeaves.SelectedIndexChanged
        LeaveRequestID = Convert.ToInt32(DirectCast(grdEmpLeaves.SelectedItems(0), GridDataItem).GetDataKeyValue("LeaveRequestID").ToString())
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objHR_LeaveRequest = New HR_LeaveRequest
        With objHR_LeaveRequest
            .LeaveRequestId = LeaveRequestID
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
            Dim fPath As String = "..\HR_LeaveRequestFiles\" + LeaveRequestID.ToString() + .AttachedFile
            Dim fPathExist As String = String.Empty
            fPathExist = Server.MapPath("..\HR_LeaveRequestFiles\\" + LeaveRequestID.ToString() + .AttachedFile)
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

        objRECALC_REQUEST = New RECALC_REQUEST
        Dim temp_str_date As String
        Dim err2 As Integer

        For Each Item As GridDataItem In grdEmpLeaves.Items
            Dim cb As CheckBox = DirectCast(Item.FindControl("chk"), CheckBox)
            If cb.Checked Then

                ' Get Permission Id from hidden label
                Dim LeaveRequestID As Integer = Item.GetDataKeyValue("LeaveRequestID").ToString()

                ' Get Employee Id from hidden label
                Dim EmployeeId As Integer = Item.GetDataKeyValue("FK_EmployeeId").ToString()

                ' Delete current checked item
                objHR_LeaveRequest = New HR_LeaveRequest
                objHR_LeaveRequest.LeaveRequestId = LeaveRequestID
                objHR_LeaveRequest.GetByPK()
                intleaveDays = objHR_LeaveRequest.Days
                intleavetype = objHR_LeaveRequest.FK_LeaveTypeId
                errNum = objHR_LeaveRequest.Delete()

                If (errNum = 0) Then
                    'Delete from balance
                    objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
                    objEmp_Leaves_BalanceHistory.FK_EmployeeId = EmployeeId
                    objEmp_Leaves_BalanceHistory.FK_LeaveId = intleavetype
                    objEmp_Leaves_BalanceHistory.FK_EmpLeaveId = LeaveRequestID

                    objEmp_Leaves_BalanceHistory.GetLastBalance()

                    objEmp_Leaves_BalanceHistory.BalanceDate = Date.Now
                    objEmp_Leaves_BalanceHistory.Balance = intleaveDays
                    objEmp_Leaves_BalanceHistory.TotalBalance = objEmp_Leaves_BalanceHistory.TotalBalance + intleaveDays
                    objEmp_Leaves_BalanceHistory.Remarks = "Delete Leave Data - Rollback Balance"
                    objEmp_Leaves_BalanceHistory.CREATED_DATE = Date.Now
                    objEmp_Leaves_BalanceHistory.CREATED_BY = "Username"
                    objEmp_Leaves_BalanceHistory.Add()

                    Dim dteFrom As DateTime = Convert.ToDateTime(Item("FromDate").Text)
                    Dim dteTo As DateTime = Convert.ToDateTime(Item("ToDate").Text)

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
                        End If
                    End While
                End If
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully"), "success")
            FillEmpLeaveGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete"), "error")
        End If
        ClearAll()
        FillEmpLeaveGrid()
        EmployeeFilterUC.ClearValues()
    End Sub

    Protected Sub grdEmpLeaves_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles grdEmpLeaves.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("RequestDate").ToString())) And (Not item.GetDataKeyValue("RequestDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("RequestDate").ToString()
                item("RequestDate").Text = fromDate.ToShortDateString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromDate").ToString())) And (Not item.GetDataKeyValue("FromDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("FromDate").ToString()
                item("FromDate").Text = fromDate.ToShortDateString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ToDate").ToString())) And (Not item.GetDataKeyValue("ToDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("ToDate").ToString()
                item("ToDate").Text = fromDate.ToShortDateString()
            End If

            If item("IsRejected").Text = "&nbsp;" Then
                If Lang = CtlCommon.Lang.AR Then
                    item("IsRejected").Text = "قيد الدراسة"
                Else
                    item("IsRejected").Text = "Pending"
                End If

            ElseIf item("IsRejected").Text = "True" Then
                If Lang = CtlCommon.Lang.AR Then
                    item("IsRejected").Text = "مرفوض"
                Else
                    item("IsRejected").Text = "Rejected"
                End If
            Else
                If Lang = CtlCommon.Lang.AR Then
                    item("IsRejected").Text = "موافقة"
                Else
                    item("IsRejected").Text = "Approved"
                End If
            End If

            If Lang = CtlCommon.Lang.AR Then
                item("LeaveName").Text = DirectCast(item.FindControl("hdnLeaveArabicType"), HiddenField).Value
            End If

        End If
    End Sub

    Protected Sub btnGet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGet.Click
        objHR_LeaveRequest = New HR_LeaveRequest
        With objHR_LeaveRequest
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
        If Not (LeaveRequestID = 0) Then
            Dim fileName As String = LeaveRequestID.ToString()
            Dim fPath As String = String.Empty
            fPath = Server.MapPath("..\HR_LeaveFiles\\" + fileName + FileExtension)

            If File.Exists(fPath) Then
                File.Delete(fPath)
                lnbLeaveFile.Visible = False
                lnbRemove.Visible = False
                lblNoAttachedFile.Visible = True
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully"), "success")
            End If
        End If
    End Sub

    Protected Sub ddlLeaveType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlLeaveType.SelectedIndexChanged
        ValidateLeaveSufficient()
    End Sub

    Protected Sub btnConfirm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConfirm.Click
        SaveEmpLeave()
    End Sub

#End Region

#Region "Methods"

    Function IsExists(ByVal Fromdate As Date, ByVal Todate As Date, ByVal LeaveID As Integer) As Boolean
        Dim status As Boolean
        For Each dr As DataRow In EmpDt.Rows
            If LeaveID = dr("LeaveId") Then
                Continue For
            End If
            If Fromdate >= dr("FromDate") And Fromdate <= dr("ToDate") Or Todate >= dr("FromDate") And Todate <= dr("ToDate") Then
                status = True
            End If

        Next
        Return status
    End Function

    Sub ClearAll()
        txtRemarks.Text = String.Empty
        ddlLeaveType.SelectedIndex = -1
        dtpFromDate.Clear()
        dtpToDate.Clear()
        dtpRequestDate.SelectedDate = Date.Today
        'chkHalfDay.Checked = False
        LeaveRequestID = 0
        fuAttachFile.Visible = True
        lnbLeaveFile.Visible = False
        lnbRemove.Visible = False
        FillEmpLeaveGrid()
        btnSave.Text = ResourceManager.GetString("btnSave", CultureInfo)
        lblNoAttachedFile.Visible = False
    End Sub

    Sub FillEmpLeaveGrid()
        objHR_LeaveRequest = New HR_LeaveRequest
        With objHR_LeaveRequest
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
        objHR_LeaveRequest = New HR_LeaveRequest
        objLeavesTypes = New LeavesTypes
        objRECALC_REQUEST = New RECALC_REQUEST
        Dim temp_str_date As String
        Dim err2 As Integer
        Dim ErrMessage As String = String.Empty

        objLeavesTypes = New LeavesTypes
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


        

        If (objHR_LeaveRequest.ValidateEmployeeLeaveRequest(objLeavesTypes, dtpFromDate.SelectedDate, dtpToDate.SelectedDate, LeaveRequestID, EmployeeFilterUC.EmployeeId, _
                                                ddlLeaveType.SelectedValue, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
            If (ErrorMessage <> String.Empty) Then
                CtlCommon.ShowMessage(Me.Page, ErrorMessage)
            End If

        Else

            objHR_LeaveRequest.LeaveRequestId = LeaveRequestID
            Dim fileUploadExtension As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)

            If String.IsNullOrEmpty(fileUploadExtension) Then
                fileUploadExtension = FileExtension
            End If

            Dim err As Integer = -1
            Dim strMessage As String
            objHR_LeaveRequest = New HR_LeaveRequest

            Dim OffOrHolidayDaysCount As Integer
            OffOrHolidayDaysCount = 0

            If OffAndHolidayDays > 0 Then
                OffOrHolidayDaysCount = ((dtpToDate.SelectedDate - dtpFromDate.SelectedDate).Value.Days + 1) - OffAndHolidayDays
            Else
                OffOrHolidayDaysCount = (dtpToDate.SelectedDate - dtpFromDate.SelectedDate).Value.Days + 1
            End If

            With objHR_LeaveRequest
                .FK_EmployeeId = EmployeeFilterUC.EmployeeId
                .FK_LeaveTypeId = ddlLeaveType.SelectedValue
                .FromDate = dtpFromDate.DbSelectedDate
                .ToDate = dtpToDate.DbSelectedDate
                .RequestDate = dtpRequestDate.DbSelectedDate
                .Remarks = txtRemarks.Text
                .IsHalfDay = False ' chkHalfDay.Checked
                .AttachedFile = fileUploadExtension
                .Days = OffOrHolidayDaysCount
                objLeavesTypes = New LeavesTypes
                objLeavesTypes.LeaveId = ddlLeaveType.SelectedValue
                objLeavesTypes.GetByPK()




                'If fuAttachFile.HasFile Then
                '    .AttachedFile = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                'End If
                If LeaveRequestID = 0 Then
                    .CREATED_BY = SessionVariables.LoginUser.ID
                    err = .Add()
                    strMessage = ResourceManager.GetString("SaveSuccessfully", CultureInfo)
                Else
                    '.AttachedFile = FileExtension
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    .LeaveRequestId = LeaveRequestID
                    err = .Update()

                    strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
                End If

                If err = 0 Then
                    If fileUploadExtension IsNot Nothing Then
                        .AttachedFile = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                        Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                        Dim fileName As String = objHR_LeaveRequest.LeaveRequestId.ToString()
                        Dim fPath As String = String.Empty
                        fPath = Server.MapPath("..\HR_LeaveRequestFiles\\" + fileName + extention)
                        fuAttachFile.PostedFile.SaveAs(fPath)
                    Else
                        .AttachedFile = String.Empty
                    End If
                End If

                If err = 0 Then
                    CtlCommon.ShowMessage(Me.Page, strMessage, "success")
                    ClearAll()
                    FillEmpLeaveGrid()
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "info")
                End If
            End With
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
