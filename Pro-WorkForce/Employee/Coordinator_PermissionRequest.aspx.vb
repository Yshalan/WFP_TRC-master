Imports TA.Employees
Imports System.Data
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.LookUp
Imports SmartV.UTILITIES.ProjectCommon
Imports Telerik.Web.UI.Calendar
Imports TA.Definitions
Imports TA.SelfServices
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports System.IO
Imports TA.Security
Imports TA.Admin
Imports TA.DailyTasks

Partial Class Admin_LeavesRequest
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_PermissionsRequest As Emp_PermissionsRequest
    Private objProjectCommon As New ProjectCommon
    Private objRequestStatus As New RequestStatus
    Private objLeavesTypes As LeavesTypes
    Private Lang As CtlCommon.Lang
    Private objEmp_Leaves As Emp_Leaves
    Private objEmp_Permissions As Emp_Permissions
    Private objPermissionsTypes As PermissionsTypes
    Shared dtCurrent As DataTable
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Shared SortExepression As String
    Dim objAPP_Settings As APP_Settings
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objEmployee_Manager As Employee_Manager
    Private objEmployee As Employee

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

    Public Property PermissionId() As Integer
        Get
            Return ViewState("PersmissionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("PersmissionId") = value
        End Set
    End Property

    Public Property PermissionDaysCount() As Double
        Get
            Return ViewState("PermissionDaysCount")
        End Get
        Set(ByVal value As Double)
            ViewState("PermissionDaysCount") = value
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

    Public Property EmpLeaveTotalBalance() As Double
        Get
            Return ViewState("EmpLeaveTotalBalance")
        End Get
        Set(ByVal value As Double)
            ViewState("EmpLeaveTotalBalance") = value
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
            Return ViewState("dtCurrentControls")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrentControls") = value
        End Set
    End Property

    Public Property LeaveTypeDuration() As Integer
        Get
            Return ViewState("LeaveTypeDuration")
        End Get
        Set(value As Integer)
            ViewState("LeaveTypeDuration") = value
        End Set
    End Property

    Public Property ApprovedRequired() As Boolean
        Get
            Return ViewState("ApprovedRequired")
        End Get
        Set(value As Boolean)
            ViewState("ApprovedRequired") = value
        End Set
    End Property

    Public Property EmpPermissionRemainingBalance() As Decimal
        Get
            Return ViewState("RemainingBalance")
        End Get
        Set(value As Decimal)
            ViewState("RemainingBalance") = value
        End Set
    End Property

    Public Property EmpPermID() As String
        Get
            Return ViewState("EmpPermID")
        End Get
        Set(value As String)
            ViewState("EmpPermID") = value
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

#End Region

#Region "PageEvents"

    Protected Sub Page_PreRenderComplete(sender As Object, e As EventArgs) Handles Me.PreRenderComplete

        For Each row As DataRow In dtCurrentControls.Rows
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
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                dgrdEmpPermissionRequest.Columns(1).Visible = False
                dgrdEmpPermissionRequest.Columns(9).Visible = False
            Else
                Lang = CtlCommon.Lang.EN
                dgrdEmpPermissionRequest.Columns(2).Visible = False
                dgrdEmpPermissionRequest.Columns(10).Visible = False
            End If

            SetRadDateTimePickerPeoperties()

            FillPermissionTypes()
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            reqPermission.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            dtpPermissionDate.SelectedDate = Date.Today
            CoordinatorPermissionRequestHeader.HeaderText = ResourceManager.GetString("CoordinatorPermitRequest", CultureInfo)

            RadTPfromTime.TimeView.HeaderText = String.Empty
            RadTPtoTime.TimeView.HeaderText = String.Empty
            RadTPfromTime.TimeView.TimeFormat = "HH:mm"
            RadTPfromTime.TimeView.DataBind()
            RadTPtoTime.TimeView.TimeFormat = "HH:mm"
            RadTPtoTime.TimeView.DataBind()

            rmtFlexibileTime.TextWithLiterals = "0000"
            rdlTimeOption.Items.FindByValue("0").Selected = True
            trFlixibleTime.Style("display") = "none"

            dtpFromDateSearch.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            dtpToDateSearch.SelectedDate = dd

            FillGridView()

            ''FadiH::Edited Full Day from App_Settings
            objAPP_Settings = New APP_Settings()
            objAPP_Settings = objAPP_Settings.GetByPK()
            'ShowHideControls()


        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdEmpPermissionRequest.ClientID + "');")

        Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        scriptManager__1.RegisterPostBackControl(lnbLeaveFile)

        'If Session("FileUpload1") = Nothing And fuAttachFile.PostedFile Then
        '    Session("FileUpload1") = fuAttachFile
        '    Label1.Text = fuAttachFile.FileName
        'ElseIf Not Session("FileUpload1") = Nothing And Not fuAttachFile.HasFile Then
        '    fuAttachFile = Session("FileUpload1")
        'ElseIf fuAttachFile.HasFile Then
        '    Session("FileUpload1") = fuAttachFile
        'End If


        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrentControls.Rows
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

        Dim isFlixible As Boolean

        If radBtnPeriod.Checked Then
            If ValidateEndDate() = False Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("DateRange", CultureInfo), "info")
                Return
            End If

        End If

        If (RadTPtoTime.SelectedDate.Value.Hour - RadTPfromTime.SelectedDate.Value.Hour) < 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("TimeRange", CultureInfo), "info")
            Return
        End If

        Dim errNo As Integer
        EmpLeaveTotalBalance = 0
        OffAndHolidayDays = 0
        Dim ErrorMessage As String = String.Empty
        objEmp_Leaves = New Emp_Leaves
        objEmp_PermissionsRequest = New Emp_PermissionsRequest
        objLeavesTypes = New LeavesTypes
        objPermissionsTypes = New PermissionsTypes
        objEmp_Permissions = New Emp_Permissions

        objEmp_PermissionsRequest.FK_EmployeeId = EmployeeFilterUC.EmployeeId
        objEmp_PermissionsRequest.FK_PermId = RadCmpPermissions.SelectedValue
        objEmp_PermissionsRequest.PermDate = dtpPermissionDate.SelectedDate
        Dim fileUploadExtension As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)

        If SessionVariables.CultureInfo = "ar-JO" Then
            objEmp_PermissionsRequest.Lang = 0
        Else
            objEmp_PermissionsRequest.Lang = 1
        End If

        'Added by Kiran 28-Feb-2017 to exclude the validation on update
        'If PermissionId = 0 Then
        '    If (objEmp_PermissionsRequest.CheckAllowedPermissionTypeOccurance(ErrorMessage)) = False Then
        '        If (ErrorMessage <> String.Empty) Then
        '            CtlCommon.ShowMessage(Me.Page, ErrorMessage)
        '            Return
        '        End If
        '    End If
        'End If


        dtCurrent = objEmp_PermissionsRequest.GetByEmployee()
        Dim strFlexibileDuration As String = (CInt(rmtFlexibileTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtFlexibileTime.TextWithLiterals.Split(":")(1))
        If (rdlTimeOption.Items.FindByValue("0").Selected) Then
            isFlixible = False
        Else
            isFlixible = True
        End If

        'If isFlixible = False Then
        '    If Not CheckPermissionTypeDuration() Then
        '        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PermissionTypeDurationLimit"))
        '        Return
        '    End If
        'End If

        objPermissionsTypes = New PermissionsTypes

        objPermissionsTypes.PermId = RadCmpPermissions.SelectedValue
        objPermissionsTypes.GetByPK()
        objEmployee_Manager = New Employee_Manager


        With objPermissionsTypes
            Dim ErrMessage As String
            Dim IsManager As Boolean
            objEmployee_Manager.FK_ManagerId = EmployeeFilterUC.EmployeeId
            If objEmployee_Manager.IsManager = False Then
                IsManager = False
            Else
                IsManager = True
            End If
            If .ExcludeManagers_FromAfterBefore And IsManager Then

            Else


                If Not objPermissionsTypes.AllowedAfterDays = Nothing Then
                    If (IIf(radBtnPeriod.Checked, dtpStartDatePerm.SelectedDate, dtpPermissionDate.SelectedDate)) > DateTime.Today.AddDays(.AllowedAfterDays) Then
                        If Lang = CtlCommon.Lang.AR Then
                            ErrMessage = "نوع المغادرة التي قمت باختيارها لايمكن طلبها لتاريخ بعد : " & .AllowedAfterDays.ToString() & " أيام"
                            CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                        Else
                            ErrMessage = "The Selected Permission Type Not Allowed After : " & .AllowedAfterDays & " Day(s)"
                            CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                        End If

                        Return
                    End If
                End If

                If (Not objPermissionsTypes.AllowedBeforeDays = Nothing) Then
                    If IIf(radBtnPeriod.Checked, dtpEndDatePerm.SelectedDate, dtpPermissionDate.SelectedDate) <= DateTime.Today.AddDays(.AllowedBeforeDays * -1) Then
                        If Lang = CtlCommon.Lang.AR Then
                            ErrMessage = "نوع المغادرة التي قمت باختيارها لايمكن طلبها لتاريخ قبل : " & .AllowedBeforeDays.ToString() + " أيام "
                            CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                        Else
                            ErrMessage = "The Selected Permission Type Not Allowed Before : " & .AllowedBeforeDays & " Day(s)"
                            CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                        End If

                        Return
                    End If
                End If
            End If
        End With


        FillObjectData()

        If (objEmp_PermissionsRequest.IsAllowedToRequest(ErrorMessage)) = False Then
            If (ErrorMessage <> String.Empty) Then
                CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                Return
            End If
        End If
        If (objEmp_PermissionsRequest.HasNursingOrStudyPermission(RadCmpPermissions.SelectedValue, ErrorMessage)) = False Then
            If (ErrorMessage <> String.Empty) Then
                CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                Return
            End If
        End If
        If rdlTimeOption.SelectedValue = 0 Then
            If (objEmp_PermissionsRequest.ValidateEmployeePermission(EmployeeFilterUC.EmployeeId, PermissionId, dtCurrent, radBtnOneDay.Checked,
                                                            dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate, RadTPfromTime.SelectedDate,
                                                            RadTPtoTime.SelectedDate, dtpPermissionDate.SelectedDate, objPermissionsTypes, RadCmpPermissions.SelectedValue,
                                                            chckFullDay.Checked, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
                If (ErrorMessage <> String.Empty) Then
                    CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                    Return
                End If
            End If

            If chckFullDay.Checked = False Then
                If (objEmp_PermissionsRequest.CheckPermissionTimeInSideSchedule(RadTPfromTime.SelectedDate, RadTPtoTime.SelectedDate, ErrorMessage)) = False Then
                    If (ErrorMessage <> String.Empty) Then
                        CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                        Return
                    End If
                End If
            End If

        ElseIf rdlTimeOption.SelectedValue = 1 Then
            If (objEmp_PermissionsRequest.ValidateEmployeeFlexiblePermission(EmployeeFilterUC.EmployeeId, PermissionId, dtCurrent, radBtnOneDay.Checked,
                                                                    dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate,
                                                                    dtpPermissionDate.SelectedDate, objPermissionsTypes, RadCmpPermissions.SelectedValue,
                                                                    chckFullDay.Checked, ErrorMessage, OffAndHolidayDays, strFlexibileDuration, EmpLeaveTotalBalance) = False) Then
                If (ErrorMessage <> String.Empty) Then
                    CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                    Return
                End If

            End If
        End If

        'For Check if Remarks is Manadetory
        If txtRemarks.Text.Trim() = "" Then
            If objPermissionsTypes.RemarksIsMandatory = True Then
                If Lang = CtlCommon.Lang.AR Then
                    CtlCommon.ShowMessage(Me.Page, "يرجى ادخال الملاحظات", "info")
                Else
                    CtlCommon.ShowMessage(Me.Page, "Please Enter Remarks", "info")
                End If
                Exit Sub
            End If
        End If
        ''''''
        'For Check if Attachment is Manadetory
        If fuAttachFile.HasFile = False Then
            If objPermissionsTypes.AttachmentIsMandatory = True Then
                If Lang = CtlCommon.Lang.AR Then
                    CtlCommon.ShowMessage(Me.Page, "يرجى ارفاق ملف!", "info")
                Else
                    CtlCommon.ShowMessage(Me.Page, "Please Attach File!", "info")
                End If
                Exit Sub
            End If
        End If
        ''''''

        If PermissionId = 0 Then
            If Not fuAttachFile.PostedFile Is Nothing Then
                objEmp_PermissionsRequest.AttachedFile = Path.GetExtension(fuAttachFile.PostedFile.FileName)
            End If
            If ApprovedRequired Then
                objEmp_PermissionsRequest.FK_StatusId = Convert.ToInt32(CtlCommon.RequestStatusEnum.Pending)
                objEmp_PermissionsRequest.RequestedByCoordinator = True
                errNo = objEmp_PermissionsRequest.Add()
                PermissionId = objEmp_PermissionsRequest.PermissionId
            Else
                objEmp_PermissionsRequest.FK_StatusId = Convert.ToInt32(CtlCommon.RequestStatusEnum.AutomaticApproved)
                'If EmpPermissionRemainingBalance > 0 Then
                objEmp_PermissionsRequest.RequestedByCoordinator = True
                errNo = objEmp_PermissionsRequest.Add()
                PermissionId = objEmp_PermissionsRequest.PermissionId
                SaveEmployeePermission(ErrorMessage)
                'Else
                '    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("AutoApprovedPermissionBalance", CultureInfo))
                '    Return
                'End If
            End If
            If Not fileUploadExtension Is Nothing Then
                Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                Dim fileName As String = PermissionId.ToString()
                Dim fPath As String = String.Empty
                fPath = Server.MapPath("..\PermissionRequestFiles\\" + fileName + extention)
                fuAttachFile.PostedFile.SaveAs(fPath)
                'objEmp_PermissionsRequest.PermissionId = PermissionId
                'objEmp_PermissionsRequest.AttachedFile = extention
                'objEmp_PermissionsRequest.UpdateAttachment()
                If Not ApprovedRequired Then
                    If (File.Exists(Server.MapPath("..\PermissionRequestFiles\\" + PermissionId.ToString() + extention))) Then

                        Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("..\PermissionRequestFiles\\" + PermissionId.ToString() + extention))
                        CopyFile.CopyTo(Server.MapPath("..\PermissionFiles\\" + PermissionId.ToString() + extention))

                        Rename(Server.MapPath("..\PermissionFiles\\" + PermissionId.ToString() + extention),
                                                Server.MapPath("..\PermissionFiles\\" + EmpPermID + extention))
                    End If
                End If
            End If
            If errNo = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If

        Else
            objEmp_PermissionsRequest.PermissionId = PermissionId
            objEmp_PermissionsRequest.FK_StatusId = 1
            objEmp_PermissionsRequest.RequestedByCoordinator = True
            objEmp_PermissionsRequest.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            errNo = objEmp_PermissionsRequest.Update()

            If Not fileUploadExtension Is Nothing Then
                Dim fileName As String = PermissionId.ToString()
                Dim fPath As String = String.Empty
                fPath = Server.MapPath("..\PermissionRequestFiles\\" + fileName + FileExtension)
                If File.Exists(fPath) Then
                    Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                    File.Delete(fPath)
                    fPath = Server.MapPath("..\PermissionRequestFiles\\" + fileName + extention)
                    fuAttachFile.PostedFile.SaveAs(fPath)
                Else
                    File.Delete(fPath)
                    Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                    Dim fileNameExist As String = PermissionId.ToString()
                    Dim fPathExist As String = String.Empty
                    fPathExist = Server.MapPath("..\PermissionRequestFiles\\" + fileNameExist + extention)
                    fuAttachFile.PostedFile.SaveAs(fPathExist)
                End If
            End If

            If errNo = 0 Then
                If Lang = CtlCommon.Lang.AR Then
                    ScriptManager.RegisterStartupScript(Me, Page.GetType, "Redirect", "ShowMessageAndRedirect('تم إرسال الطلب الى المدير المباشر','../SelfServices/PermissionRequest.aspx');", True)
                Else
                    ScriptManager.RegisterStartupScript(Me, Page.GetType, "Redirect", "ShowMessageAndRedirect('The Request has been Sent to Direct Manager','../SelfServices/PermissionRequest.aspx');", True)
                End If
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        End If

        If errNo = 0 Then
            FillGridView()
            ClearAll()
        End If
    End Sub

    Protected Sub radBtnPeriod_CheckedChanged(ByVal sender As Object,
                                                  ByVal e As System.EventArgs) Handles radBtnPeriod.CheckedChanged
        ShowHide(True)
    End Sub

    Protected Sub radBtnOneDay_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radBtnOneDay.CheckedChanged
        ShowHide(False)
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Protected Sub dgrdEmpPermissionRequest_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdEmpPermissionRequest.SelectedIndexChanged
        PermissionId = Convert.ToInt32(DirectCast(dgrdEmpPermissionRequest.SelectedItems(0), GridDataItem).GetDataKeyValue("PermissionId"))
        FillControls()
    End Sub

    Protected Sub chckFullDay_CheckedChanged(ByVal sender As Object,
                                             ByVal e As System.EventArgs) _
                                             Handles chckFullDay.CheckedChanged
        DisableEnableTimeView(Not chckFullDay.Checked)
        rdlTimeOption.Enabled = Not chckFullDay.Checked
        rdlTimeOption.SelectedValue = 0
        trTime.Visible = Not chckFullDay.Checked
        trDifTime.Visible = Not chckFullDay.Checked
        trSpecificTime.Visible = Not chckFullDay.Checked
    End Sub

    Protected Sub btnGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGet.Click
        'objEmp_PermissionsRequest = New Emp_PermissionsRequest()
        'objEmp_PermissionsRequest.FK_EmployeeId = EmployeeFilterUC.EmployeeId
        'objEmp_PermissionsRequest.PermDate = dtpFromDateSearch.SelectedDate
        'objEmp_PermissionsRequest.PermEndDate = dtpToDateSearch.SelectedDate
        'objEmp_PermissionsRequest.FK_StatusId = 0
        'dgrdEmpPermissionRequest.DataSource = objEmp_PermissionsRequest.GetByStatusType()
        'dgrdEmpPermissionRequest.DataBind()
        FillGridView()
    End Sub

    Protected Sub dgrdEmpHR_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmpPermissionRequest.NeedDataSource
        objEmp_PermissionsRequest = New Emp_PermissionsRequest()
        objEmp_PermissionsRequest.FK_EmployeeId = EmployeeFilterUC.EmployeeId
        objEmp_PermissionsRequest.PermDate = DateSerial(Today.Year, Today.Month, 1)
        objEmp_PermissionsRequest.PermEndDate = DateSerial(Today.Year, Today.Month + 1, 0)
        dtCurrent = objEmp_PermissionsRequest.GetByEmployee()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdEmpPermissionRequest.DataSource = dv
    End Sub

    Protected Sub RadCmpPermissions_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmpPermissions.SelectedIndexChanged
        objEmp_Permissions = New Emp_Permissions()
        lblRemainingBalanceValue.Text = objEmp_Permissions.GetPermissionRemainingBalance(RadCmpPermissions.SelectedValue, EmployeeFilterUC.EmployeeId, radBtnOneDay.Checked, dtpPermissionDate.SelectedDate.Value, dtpStartDatePerm.SelectedDate.Value, dtpEndDatePerm.SelectedDate.Value)
        CheckPermissionApprovalRequired()
        objPermissionsTypes = New PermissionsTypes
        With objPermissionsTypes
            .PermId = RadCmpPermissions.SelectedValue
            .GetByPK()
            If .ShowRemainingBalance = False Then
                lblRemainingBalance.Visible = False
                lblRemainingBalanceHours.Visible = False
                lblRemainingBalanceValue.Visible = False
            Else
                lblRemainingBalance.Visible = True
                lblRemainingBalanceHours.Visible = True
                lblRemainingBalanceValue.Visible = True
            End If
            If Not .HasFlexiblePermission Then
                rdlTimeOption.Visible = False
                rdlTimeOption.SelectedValue = 0
                trSpecificTime.Style("display") = "block"
                trFlixibleTime.Style("display") = "none"

                reqFlexibiletime.Enabled = False
                ExtenderFlexibileTime.Enabled = False
                reqFromtime.Enabled = True
                ExtenderFromTime.Enabled = True
                reqToTime.Enabled = True
                ExtenderreqToTime.Enabled = True
                lblPeriodInterval.Visible = True
                txtTimeDifference.Visible = True
                CustomValidator1.Enabled = True
                CustomValidator2.Enabled = True

            Else
                rdlTimeOption.Visible = True
            End If
            If Not .HasPermissionForPeriod Then
                radBtnPeriod.Visible = False
                radBtnOneDay.Visible = False
                ShowHide(False)
            Else
                radBtnPeriod.Visible = True
                radBtnOneDay.Visible = True
            End If
            If .HasFullDayPermission Then
                trFullyDay.Visible = True
                DisableEnableTimeView(False)

            Else
                chckFullDay.Checked = False
                trFullyDay.Visible = False
                reqFromtime.Enabled = True
                ExtenderFromTime.Enabled = True
                reqToTime.Enabled = True
                rdlTimeOption.Enabled = True
                rdlTimeOption.SelectedValue = 0
                trTime.Visible = True
            End If
        End With
        'GetRemainingBalance()
    End Sub

    Protected Sub dtpPermissionDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dtpPermissionDate.SelectedDateChanged
        objEmp_Permissions = New Emp_Permissions()
        lblRemainingBalanceValue.Text = objEmp_Permissions.GetPermissionRemainingBalance(RadCmpPermissions.SelectedValue, EmployeeFilterUC.EmployeeId, radBtnOneDay.Checked, dtpPermissionDate.SelectedDate.Value, dtpStartDatePerm.SelectedDate.Value, dtpEndDatePerm.SelectedDate.Value)
        CheckPermissionApprovalRequired()
        'GetRemainingBalance()
    End Sub

    Protected Sub dtpStartDatePerm_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dtpStartDatePerm.SelectedDateChanged
        objEmp_Permissions = New Emp_Permissions()
        lblRemainingBalanceValue.Text = objEmp_Permissions.GetPermissionRemainingBalance(RadCmpPermissions.SelectedValue, EmployeeFilterUC.EmployeeId, radBtnOneDay.Checked, dtpPermissionDate.SelectedDate.Value, dtpStartDatePerm.SelectedDate.Value, dtpEndDatePerm.SelectedDate.Value)
        CheckPermissionApprovalRequired()
        'GetRemainingBalance()
    End Sub

    Protected Sub lnbRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnbRemove.Click
        If Not (PermissionId = 0) Then
            Dim fileName As String = PermissionId.ToString()
            Dim fPath As String = String.Empty
            fPath = Server.MapPath("..\PermissionRequestFiles\\" + fileName + FileExtension)

            If File.Exists(fPath) Then
                File.Delete(fPath)
                lnbLeaveFile.Visible = False
                lnbRemove.Visible = False
                lblNoAttachedFile.Visible = True
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully"), "success")
            End If
        End If
    End Sub

    Protected Sub lnkDownloadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        objEmp_PermissionsRequest = New Emp_PermissionsRequest
        With objEmp_PermissionsRequest
            .PermissionId = PermissionId
            .GetByPK()

            Dim FileName As String = PermissionId.ToString() + .AttachedFile
            Dim FilePath As String = Server.MapPath("..\\PermissionRequestFiles\") & FileName
            CtlCommon.Open_Download_File(FileName, FilePath)

        End With
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        For Each Item As GridDataItem In dgrdEmpPermissionRequest.Items
            Dim cb As CheckBox = DirectCast(Item.FindControl("chk"), CheckBox)
            If cb.Checked Then

                ' Get Permission Id from hidden label
                Dim PermissionIdgrd As Integer = Item.GetDataKeyValue("PermissionId")

                ' Get Employee Id from hidden label
                Dim Status As Integer = Convert.ToInt32(Item.GetDataKeyValue("FK_StatusId"))
                Dim AttachedFile As String
                If Not IsDBNull(Item.GetDataKeyValue("AttachedFile")) Then
                    AttachedFile = Item.GetDataKeyValue("AttachedFile")
                End If


                If (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.Pending) Or (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByGeneralManager)) Or
                    (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByHumanResource)) Or (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedbyManager))) Then
                    ' Delete current checked item
                    objEmp_PermissionsRequest = New Emp_PermissionsRequest
                    objEmp_PermissionsRequest.PermissionId = PermissionIdgrd
                    errNum = objEmp_PermissionsRequest.Delete()

                    If errNum = 0 Then
                        Dim fileName As String = PermissionIdgrd.ToString()
                        Dim fPath As String = String.Empty
                        fPath = Server.MapPath("..\PermissionRequestFiles\\" + fileName + AttachedFile)
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

    Protected Sub dgrdEmpPermissionRequest_ItemDataBind(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdEmpPermissionRequest.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("IsFullDay").ToString())) And (Not item.GetDataKeyValue("IsFullDay").ToString() = "")) Then
                Dim IsFullDay As Boolean = Convert.ToBoolean(item.GetDataKeyValue("IsFullDay"))
                If IsFullDay Then
                    item("FromTime").Text = String.Empty
                    item("ToTime").Text = String.Empty
                End If
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
                item("IsFlexible").Text = "(" + ResourceManager.GetString("Yes", CultureInfo) + ")" + "(" + strFlexibleDuration + ")"
            ElseIf strIsFlexible = "False" Then
                item("IsFlexible").Text = "(" + ResourceManager.GetString("No", CultureInfo) + ")"
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FK_StatusId").ToString())) And (Not item.GetDataKeyValue("FK_StatusId").ToString() = "")) Then
                If (Not item.GetDataKeyValue("FK_StatusId") = CtlCommon.RequestStatusEnum.RejectedByGeneralManager) And
                        (Not item.GetDataKeyValue("FK_StatusId") = CtlCommon.RequestStatusEnum.RejectedByHumanResource) And
                        (Not item.GetDataKeyValue("FK_StatusId") = CtlCommon.RequestStatusEnum.RejectedbyManager) Then

                    item("RejectionReason").Text = String.Empty

                End If
            End If

        End If
    End Sub

    Protected Sub rdlTimeOption_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If (rdlTimeOption.SelectedValue = "1") Then
            trSpecificTime.Style("display") = "none"
            trFlixibleTime.Style("display") = "block"

            reqFlexibiletime.Enabled = True
            ExtenderFlexibileTime.Enabled = True
            reqFromtime.Enabled = False
            ExtenderFromTime.Enabled = False
            reqToTime.Enabled = False
            ExtenderreqToTime.Enabled = False
            lblPeriodInterval.Visible = False
            txtTimeDifference.Visible = False
            CustomValidator1.Enabled = False
            CustomValidator2.Enabled = False
        Else
            trSpecificTime.Style("display") = "block"
            trFlixibleTime.Style("display") = "none"

            reqFlexibiletime.Enabled = False
            ExtenderFlexibileTime.Enabled = False
            reqFromtime.Enabled = True
            ExtenderFromTime.Enabled = True
            reqToTime.Enabled = True
            ExtenderreqToTime.Enabled = True
            lblPeriodInterval.Visible = True
            txtTimeDifference.Visible = True
            CustomValidator1.Enabled = True
            CustomValidator2.Enabled = True
        End If
    End Sub

#End Region

#Region "Methods"

    Sub FillGridView()
        Try
            objEmp_PermissionsRequest = New Emp_PermissionsRequest()
            objEmp_PermissionsRequest.FK_EmployeeId = EmployeeFilterUC.EmployeeId
            'objEmp_PermissionsRequest.PermDate = DateSerial(Today.Year, Today.Month, 1)
            'objEmp_PermissionsRequest.PermEndDate = DateSerial(Today.Year, Today.Month + 1, 0)

            objEmp_PermissionsRequest.PermDate = dtpFromDateSearch.SelectedDate
            objEmp_PermissionsRequest.PermEndDate = dtpToDateSearch.SelectedDate

            dtCurrent = objEmp_PermissionsRequest.GetByEmployee()
            dgrdEmpPermissionRequest.DataSource = dtCurrent
            dgrdEmpPermissionRequest.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillPermissionTypes()
        Dim dt As DataTable = Nothing
        ' Get the permissions
        objPermissionsTypes = New PermissionsTypes()
        dt = objPermissionsTypes.GetAll()
        If dt IsNot Nothing Then
            CtlCommon.FillTelerikDropDownList(RadCmpPermissions, dt, Lang)
        End If
        dt = Nothing
    End Sub

    Sub ClearAll()
        ' Clear Controls and reset to default values
        ' Reset combo boxes
        'RadCmbEmployee.SelectedIndex = 0
        RadCmpPermissions.SelectedIndex = 0
        ' Reset checkBoxes
        chckFullDay.Checked = False
        chckIsDividable.Checked = False
        chckIsFlexible.Checked = False
        chckSpecifiedDays.Checked = False
        ' Reset the time Views
        RadTPfromTime.SelectedDate = DateTime.Now
        RadTPtoTime.SelectedDate = DateTime.Now
        ' Default values for the periodical leave type
        dtpStartDatePerm.SelectedDate = Today.Date
        dtpEndDatePerm.SelectedDate = Today.Date
        ' Default value for a day leave type  
        dtpPermissionDate.SelectedDate = Today
        txtRemarks.Text = String.Empty
        txtDays.Text = String.Empty
        txtTimeDifference.Text = String.Empty
        ' Reset to prepare for next add 
        PermissionId = 0
        ' Reset mode to one day permission 
        pnlPeriodLeave.Visible = False
        PnlOneDayLeave.Visible = True
        radBtnOneDay.Checked = True
        radBtnPeriod.Checked = False
        radBtnOneDay.Checked = True
        ' Remove sorting and sorting arrow 
        lblGeneralGuide.Text = String.Empty

        lblRemainingBalance.Visible = False
        lblRemainingBalanceHours.Visible = False
        lblRemainingBalanceValue.Visible = False

        rmtFlexibileTime.TextWithLiterals = "0000"
        rdlTimeOption.Items.FindByValue("0").Selected = True
        rdlTimeOption.Items.FindByValue("1").Selected = False
        rdlTimeOption.Enabled = Not chckFullDay.Checked
        lnbRemove.Visible = False
        lnbLeaveFile.Visible = False

        EmployeeFilterUC.ClearValues()
        FillGridView()
        btnSave.Text = ResourceManager.GetString("btnSave", CultureInfo)
    End Sub

    Private Sub SetRadDateTimePickerPeoperties()

        ' Set TimeView properties 
        Me.RadTPfromTime.TimeView.StartTime = New TimeSpan(0, 0, 0)
        Me.RadTPfromTime.TimeView.EndTime = New TimeSpan(23.5, 0, 0)
        Me.RadTPfromTime.TimeView.HeaderText = String.Empty
        Me.RadTPfromTime.TimeView.Interval = New TimeSpan(0, 30, 0)
        Me.RadTPfromTime.TimeView.Columns = 5



        ' Set Popup window properties
        Me.RadTPfromTime.PopupDirection = TopRight
        Me.RadTPfromTime.ShowPopupOnFocus = True




        ' Set default value
        Me.RadTPfromTime.SelectedDate = Now

        ' Set TimeView properties 
        Me.RadTPtoTime.TimeView.StartTime = New TimeSpan(0, 0, 0)
        Me.RadTPtoTime.TimeView.EndTime = New TimeSpan(23.5, 0, 0)
        Me.RadTPtoTime.TimeView.Interval = New TimeSpan(0, 30, 0)
        Me.RadTPtoTime.TimeView.HeaderText = String.Empty
        Me.RadTPtoTime.TimeView.Columns = 5
        ' Set Popup window properties
        Me.RadTPtoTime.PopupDirection = TopRight
        Me.RadTPtoTime.ShowPopupOnFocus = True
        ' Set default value
        Me.RadTPtoTime.SelectedDate = Now


        ' Set Data input properties
        Me.dtpStartDatePerm.SelectedDate = Today.Date
        ' Set popup properties
        Me.dtpStartDatePerm.PopupDirection = TopRight
        Me.dtpStartDatePerm.ShowPopupOnFocus = True
        ' Set calender boundaries
        Me.dtpStartDatePerm.MinDate = New Date(2006, 1, 1)
        Me.dtpStartDatePerm.MaxDate = New Date(2040, 1, 1)



        ' Set Data input properties
        Me.dtpEndDatePerm.SelectedDate = Today.Date
        ' Set popup properties
        Me.dtpEndDatePerm.PopupDirection = TopRight
        Me.dtpEndDatePerm.ShowPopupOnFocus = True
        ' Set calender boundaries
        Me.dtpEndDatePerm.MinDate = New Date(2006, 1, 1)
        Me.dtpEndDatePerm.MaxDate = New Date(2040, 1, 1)



        ' Set Data input properties
        Me.dtpPermissionDate.SelectedDate = Today.Date
        ' Set Popup properties
        Me.dtpPermissionDate.PopupDirection = TopRight
        Me.dtpPermissionDate.ShowPopupOnFocus = True
        ' Set calender boundaries
        Me.dtpPermissionDate.MinDate = New Date(2006, 1, 1)
        Me.dtpPermissionDate.MaxDate = New Date(2040, 1, 1)

    End Sub

    Private Sub FillControls()
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

            If .IsFlexible Then
                rdlTimeOption.SelectedValue = 1
                trFlixibleTime.Style("display") = "block"
                trSpecificTime.Style("display") = "none"
                rmtFlexibileTime.Text = CtlCommon.GetFullTimeString(.FlexibilePermissionDuration)
                lblPeriodInterval.Visible = False
                txtTimeDifference.Visible = False
            Else
                rdlTimeOption.SelectedValue = 0
                trFlixibleTime.Style("display") = "none"
                trSpecificTime.Style("display") = "block"
                lblPeriodInterval.Visible = True
                txtTimeDifference.Visible = True
            End If
            ' Fill the time & date pickers
            RadTPfromTime.SelectedDate = .FromTime
            RadTPtoTime.SelectedDate = .ToTime
            txtRemarks.Text = .Remark
            txtDays.Text = .Days
            FileExtension = .AttachedFile
            Select Case objEmp_PermissionsRequest.FK_StatusId
                Case 1
                    ControlsStatus(True)
                Case 2
                    ControlsStatus(False)
                Case 3
                    ControlsStatus(False)
                Case 4
                    ControlsStatus(True)
                Case 8
                    ControlsStatus(False)
            End Select

            fuAttachFile.Visible = True
            Dim fPath As String = "..\PermissionRequestFiles\" + PermissionId.ToString() + .AttachedFile
            Dim FilePath As String = Server.MapPath("~/PermissionRequestFiles")
            FilePath = FilePath + "\" + PermissionId.ToString() + .AttachedFile

            If File.Exists(FilePath) Then
                lnbLeaveFile.Visible = True
                lnbRemove.Visible = True
            End If

            If String.IsNullOrEmpty(.AttachedFile) Then
                lnbLeaveFile.Visible = False
                lnbRemove.Visible = False
                lblNoAttachedFile.Visible = True
            ElseIf File.Exists(FilePath) Then
                lnbLeaveFile.Visible = True
                lnbRemove.Visible = True
                lblNoAttachedFile.Visible = False
            Else
                lnbLeaveFile.Visible = False
                lnbRemove.Visible = False
                lblNoAttachedFile.Visible = True
            End If

            '''''''''''''''''''''''''''''''''''''''''''
            ' Enable / Disable time pickers according to the full day 
            ' value
            chckFullDay.Checked = .IsFullDay
            ManageSomeControlsStatus(.IsForPeriod, .PermDate, .PermEndDate, .IsFullDay)
            btnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)
        End With
    End Sub

    Private Sub ControlsStatus(ByVal Status As Boolean)
        RadCmpPermissions.Enabled = Status
        radBtnOneDay.Enabled = Status
        dtpPermissionDate.Enabled = Status
        radBtnPeriod.Enabled = Status
        dtpStartDatePerm.Enabled = Status
        dtpEndDatePerm.Enabled = Status
        chckFullDay.Enabled = Status
        RadTPfromTime.Enabled = Status
        RadTPtoTime.Enabled = Status
        fuAttachFile.Enabled = True
        txtRemarks.Enabled = Status
        btnSave.Visible = True
        btnClear.Visible = Status
        rmtFlexibileTime.Enabled = Status
        rdlTimeOption.Enabled = Status

    End Sub

    Private Sub ManageSomeControlsStatus(ByVal IsForPeriod As Boolean,
                                         ByVal PermDate As DateTime,
                                         ByVal PermEndDate As DateTime?,
                                         ByVal FullDay As Boolean)
        ' Manage the DatePickers according 
        ' to the permission type
        ShowHide(IsForPeriod)
        If IsForPeriod = False Then
            dtpPermissionDate.SelectedDate = PermDate
        Else
            dtpStartDatePerm.SelectedDate = PermDate
            dtpEndDatePerm.SelectedDate =
                IIf(CheckDate(PermEndDate) = Nothing, Nothing, PermEndDate)
        End If
        ' If the permission for full day , means to disable the TimeView(s)
        ' otherwise means to enable or keep it enable
        If FullDay = False Then
            setTimeDifference()
            rdlTimeOption.Enabled = True
            trTime.Visible = True
            trDifTime.Visible = True
        Else
            txtTimeDifference.Text = String.Empty
            rdlTimeOption.Enabled = False
            trTime.Visible = False
            trDifTime.Visible = False
        End If
    End Sub

    Private Sub ShowHide(ByVal IsPeriod As Boolean)
        pnlPeriodLeave.Visible = IsPeriod
        PnlOneDayLeave.Visible = Not IsPeriod
        radBtnOneDay.Checked = Not IsPeriod
        radBtnPeriod.Checked = IsPeriod
    End Sub

    Private Sub DisableEnableTimeView(ByVal status As Boolean)
        'RadTPfromTime.Enabled = status
        'RadTPtoTime.Enabled = status
    End Sub

    Private Sub GetRemainingBalance()
        objEmp_PermissionsRequest = New Emp_PermissionsRequest()
        objPermissionsTypes = New PermissionsTypes()
        objEmp_Permissions = New Emp_Permissions()

        Dim empId As Integer = EmployeeFilterUC.EmployeeId
        Dim permId As Integer = RadCmpPermissions.SelectedValue
        'Dim permMonth As Integer = -1
        'Dim permYear As Integer = -1
        Dim remainingBalanceCount As Integer = 0
        Dim monthlyBalance As Integer = 0
        Dim hours As Integer = 0
        Dim minutes As Integer = 0

        'If radBtnOneDay.Checked Then
        '    permMonth = dtpPermissionDate.SelectedDate.Value.Month
        '    permYear = dtpPermissionDate.SelectedDate.Value.Year
        'ElseIf radBtnPeriod.Checked Then
        '    permMonth = dtpStartDatePerm.SelectedDate.Value.Month
        '    permYear = dtpStartDatePerm.SelectedDate.Value.Year
        'End If

        If (Not RadCmpPermissions.SelectedValue = "-1") Then
            objEmp_PermissionsRequest.FK_EmployeeId = empId
            objEmp_PermissionsRequest.FK_PermId = permId

            objEmp_Permissions.FK_EmployeeId = empId
            objEmp_Permissions.FK_PermId = permId

            If radBtnOneDay.Checked Then
                objEmp_Permissions.FromTime = DateSerial(dtpPermissionDate.SelectedDate.Value.Year, dtpPermissionDate.SelectedDate.Value.Month, 1)
                objEmp_Permissions.ToTime = DateSerial(dtpPermissionDate.SelectedDate.Value.Year, dtpPermissionDate.SelectedDate.Value.Month + 1, 0)
            Else
                objEmp_Permissions.FromTime = DateSerial(dtpStartDatePerm.SelectedDate.Value.Year, dtpStartDatePerm.SelectedDate.Value.Month, 1)
                objEmp_Permissions.ToTime = DateSerial(dtpEndDatePerm.SelectedDate.Value.Year, dtpEndDatePerm.SelectedDate.Value.Month + 1, 0)
            End If

            'Dim dtpermissionRemainingBalance As DataTable = objEmp_PermissionsRequest.GetAll_ByPermMonthAndType(permMonth)
            Dim dtPermissionRemainingBalance As DataTable = objEmp_Permissions.GetAllDurationByEmployee()

            With objPermissionsTypes
                .PermId = permId
                .GetByPK()
                ApprovedRequired = .ApprovalRequired
                monthlyBalance = .MonthlyBalance
                If SessionVariables.CultureInfo = "ar-JO" Then
                    dvGeneralGuide.InnerHtml = .GeneralGuideAr
                Else
                    dvGeneralGuide.InnerHtml = .GeneralGuide
                End If
                dvGeneralGuide.Visible = True
                lblGeneralGuide.Visible = True
                If Not .FK_LeaveIdtoallowduration = 0 Then
                    LeaveTypeDuration = .DurationAllowedwithleave
                End If
            End With

            For Each row As DataRow In dtPermissionRemainingBalance.Rows
                If (Not IsDBNull(row("ToTime"))) And (Not IsDBNull(row("FromTime"))) Then
                    remainingBalanceCount += (Convert.ToDateTime(row("ToTime")).Subtract(Convert.ToDateTime(row("FromTime")))).TotalMinutes
                End If
            Next
        Else
            dvGeneralGuide.Visible = False
            'labelGeneralGuide.Text = String.Empty
            lblGeneralGuide.Visible = False
        End If

        Dim dcmRemainingBalanceCount As Decimal = (monthlyBalance - remainingBalanceCount)
        If Not dcmRemainingBalanceCount < 0 Then
            EmpPermissionRemainingBalance = dcmRemainingBalanceCount
            hours = dcmRemainingBalanceCount \ 60
            minutes = dcmRemainingBalanceCount - (hours * 60)
        End If

        Dim timeElapsed As String = CType(hours, String) & ":" & CType(minutes, String)
        lblRemainingBalanceValue.Text = timeElapsed

        'lblRemainingBalanceValue.Text = (Decimal.Round(dcmRemainingBalanceCount, 2)).ToString()

    End Sub

    Private Function ValidateEndDate() As Boolean
        If dtpEndDatePerm.SelectedDate() IsNot Nothing Then
            Return StartEndDateComparison(dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate)
        End If
        Return True
    End Function

    Private Function StartEndDateComparison(ByVal dateStartdate As DateTime,
                                           ByVal dateEndDate As DateTime) As Boolean
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If IsDate(dtpStartDatePerm.SelectedDate) And IsDate(dtpEndDatePerm.SelectedDate) Then
            ' Get the start and end date from the calenders
            dateStartdate = New DateTime(dtpStartDatePerm.SelectedDate.Value.Year,
                                         dtpStartDatePerm.SelectedDate.Value.Month,
                                         dtpStartDatePerm.SelectedDate.Value.Day)
            dateEndDate = New DateTime(dtpEndDatePerm.SelectedDate.Value.Year,
                                       dtpEndDatePerm.SelectedDate.Value.Month,
                                       dtpEndDatePerm.SelectedDate.Value.Day)
            Dim result As Integer = DateTime.Compare(dateEndDate, dateStartdate)
            If result < 0 Then
                ' show message and set focus on end date picker
                'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DateRangeEarly", CultureInfo))

                dtpEndDatePerm.Focus()
                Return False
            ElseIf result = 0 Then
                ' Show message and focus on the end date picker
                'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EndEqualStart", CultureInfo))


                dtpEndDatePerm.Focus()
                Return False
            Else
                ' Do nothing
                Return True
            End If
        End If
    End Function

    Private Sub FillObjectData()

        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        ' Set data into object for Add / Update
        With objEmp_PermissionsRequest
            ' Get Values from the combo boxes
            .FK_EmployeeId = EmployeeFilterUC.EmployeeId
            .FK_PermId = RadCmpPermissions.SelectedValue
            ' Get values from the check boxes
            .IsDividable = chckIsDividable.Checked

            .RejectedReason = String.Empty

            If (rdlTimeOption.Items.FindByValue("0").Selected) Then
                .IsFlexible = False
            Else
                .IsFlexible = True
            End If

            .IsFullDay = chckFullDay.Checked
            .IsSpecificDays = chckSpecifiedDays.Checked
            .Remark = txtRemarks.Text
            ' Get values from rad controls
            .PermDate = dtpPermissionDate.SelectedDate
            If PermissionId = 0 Then
                If fuAttachFile.HasFile Then
                    .AttachedFile = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                Else
                    .AttachedFile = String.Empty
                End If

            Else
                If Not fuAttachFile.PostedFile Is Nothing Then
                    .AttachedFile = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                End If
            End If

            Dim strFlexibileDuration As String = (CInt(rmtFlexibileTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtFlexibileTime.TextWithLiterals.Split(":")(1))
            .FlexibilePermissionDuration = Integer.Parse(strFlexibileDuration)


            If (radBtnOneDay.Checked) Then
                Dim objFromtime As DateTime
                Dim objToTime As DateTime
                objFromtime = RadTPfromTime.SelectedDate
                objToTime = RadTPtoTime.SelectedDate

                Dim ts As TimeSpan
                ts = New TimeSpan(objFromtime.TimeOfDay.Hours, objFromtime.TimeOfDay.Minutes, objFromtime.TimeOfDay.Seconds)
                objFromtime = dtpPermissionDate.SelectedDate + ts

                ts = New TimeSpan(objToTime.TimeOfDay.Hours, objToTime.TimeOfDay.Minutes, objToTime.TimeOfDay.Seconds)
                objToTime = dtpPermissionDate.SelectedDate + ts

                .FromTime = objFromtime
                .ToTime = objToTime
            Else
                .FromTime = RadTPfromTime.SelectedDate
                .ToTime = RadTPtoTime.SelectedDate
            End If


            PermissionDaysCount = 0

            If OffAndHolidayDays > 0 Then
                If radBtnOneDay.Checked Then
                    PermissionDaysCount = 0
                Else
                    PermissionDaysCount = (((dtpEndDatePerm.SelectedDate - dtpStartDatePerm.SelectedDate).Value.Days + 1) - OffAndHolidayDays) * ((RadTPtoTime.SelectedDate - RadTPfromTime.SelectedDate).Value.TotalMinutes / objAPP_Settings.DaysMinutes)
                End If

            Else
                If radBtnOneDay.Checked Then
                    PermissionDaysCount = ((RadTPtoTime.SelectedDate - RadTPfromTime.SelectedDate).Value.TotalMinutes / objAPP_Settings.DaysMinutes)
                Else
                    PermissionDaysCount = (dtpEndDatePerm.SelectedDate - dtpStartDatePerm.SelectedDate).Value.Days + 1 * ((RadTPtoTime.SelectedDate - RadTPfromTime.SelectedDate).Value.TotalMinutes / objAPP_Settings.DaysMinutes)
                End If
            End If

            ' Set systematic values
            .CREATED_BY = SessionVariables.LoginUser.ID
            .Days = PermissionDaysCount

            If IS_Period() Then
                ' Periodically leave
                .IsForPeriod = True

                .PermDate = dtpStartDatePerm.SelectedDate
                .PermEndDate = IIf(CheckDate(dtpEndDatePerm.SelectedDate) = Nothing,
                                   DateTime.MinValue, dtpEndDatePerm.SelectedDate)

            Else
                ' Non Periodically leave
                .IsForPeriod = False

                .PermDate = dtpPermissionDate.SelectedDate
                .PermEndDate = DateTime.MinValue
            End If
        End With
    End Sub

    Private Function IS_Period() As Boolean
        Return radBtnPeriod.Checked
    End Function

    Private Function CheckDate(ByVal myDate As Object) As Object
        ' input : DateTime object
        ' Output : Nothing or a DateTime greater tha date time 
        ' minimum value
        ' Processing : Check the input if a valid date time to be used
        ' valid means greater than the minimum value and in valid format
        If IsDate(myDate) Then
            myDate =
                IIf(myDate > DateTime.MinValue, myDate, Nothing)
            Return (myDate)
        Else
            Return Nothing
        End If
    End Function

    Private Function CheckPermissionTypeDuration() As Boolean
        'If Not LeaveTypeDuration = 0 Then

        Dim permId As Integer = RadCmpPermissions.SelectedValue
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
            .FK_EmployeeId = EmployeeFilterUC.EmployeeId
            .FK_PermId = RadCmpPermissions.SelectedValue

            If radBtnOneDay.Checked Then
                .FromTime = DateSerial(dtpPermissionDate.SelectedDate.Value.Year, dtpPermissionDate.SelectedDate.Value.Month, 1)
                .ToTime = DateSerial(dtpPermissionDate.SelectedDate.Value.Year, dtpPermissionDate.SelectedDate.Value.Month + 1, 0)
            Else
                .FromTime = DateSerial(dtpStartDatePerm.SelectedDate.Value.Year, dtpStartDatePerm.SelectedDate.Value.Month, 1)
                .ToTime = DateSerial(dtpEndDatePerm.SelectedDate.Value.Year, dtpEndDatePerm.SelectedDate.Value.Month + 1, 0)
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

                remainingBalanceCount += (Convert.ToDateTime(RadTPtoTime.SelectedDate.Value).Subtract(Convert.ToDateTime(RadTPfromTime.SelectedDate.Value))).TotalMinutes
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

    Sub SaveEmployeePermission(ByVal ErrorMessage As String)
        objEmp_Permissions = New Emp_Permissions
        objRECALC_REQUEST = New RECALC_REQUEST
        Dim temp_date As Date
        Dim temp_str_date As String
        Dim err2 As Integer

        objEmp_Permissions.AddPermAllProcess(EmployeeFilterUC.EmployeeId, RadCmpPermissions.SelectedValue, objEmp_PermissionsRequest.IsDividable,
                                             objEmp_PermissionsRequest.IsFlexible, objEmp_PermissionsRequest.IsSpecificDays, objEmp_PermissionsRequest.Remark, objEmp_PermissionsRequest.PermDate,
                                             objEmp_PermissionsRequest.PermEndDate, objEmp_PermissionsRequest.IsForPeriod, objEmp_PermissionsRequest.FromTime, objEmp_PermissionsRequest.ToTime,
                                             objEmp_PermissionsRequest.PermDate, PermissionDaysCount, OffAndHolidayDays, objEmp_PermissionsRequest.Days,
                                             EmpLeaveTotalBalance, PermissionId, 1, 0, objEmp_PermissionsRequest.AttachedFile, objEmp_PermissionsRequest.IsFullDay, Nothing, ErrorMessage)

        If Not String.IsNullOrEmpty(ErrorMessage) Then
            EmpPermID = objEmp_Permissions.PermissionId.ToString()

            If objEmp_PermissionsRequest.IsForPeriod = False Then
                temp_date = objEmp_PermissionsRequest.PermDate
                temp_str_date = DateToString(temp_date)
                objRECALC_REQUEST.EMP_NO = EmployeeFilterUC.EmployeeId
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
                Dim dteFrom As DateTime = objEmp_PermissionsRequest.PermDate
                Dim dteTo As DateTime = objEmp_PermissionsRequest.PermEndDate

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
            End If
        Else
            CtlCommon.ShowMessage(Me.Page, ErrorMessage)
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

    Sub CheckPermissionApprovalRequired()
        objPermissionsTypes = New PermissionsTypes()

        If (Not RadCmpPermissions.SelectedValue = "-1") Then
            With objPermissionsTypes
                .PermId = RadCmpPermissions.SelectedValue
                .GetByPK()
                ApprovedRequired = .ApprovalRequired

                If SessionVariables.CultureInfo = "ar-JO" Then
                    dvGeneralGuide.InnerHtml = .GeneralGuideAr
                Else
                    dvGeneralGuide.InnerHtml = .GeneralGuide
                End If
                dvGeneralGuide.Visible = True
                lblGeneralGuide.Visible = True
                If Not .FK_LeaveIdtoallowduration = 0 Then
                    LeaveTypeDuration = .DurationAllowedwithleave
                End If

            End With
        Else
            dvGeneralGuide.Visible = False
            lblGeneralGuide.Visible = False
        End If
    End Sub

    Private Sub ShowHideControls()
        'objAPP_Settings = New APP_Settings

        'With objAPP_Settings
        '    .GetByPK()
        '    If Not .HasFlexiblePermission Then
        '        rdlTimeOption.Visible = False
        '    End If
        '    If Not .HasPermissionForPeriod Then
        '        radBtnPeriod.Visible = False
        '        radBtnOneDay.Visible = False
        '    End If
        '    If .HasFullDayPermission Then
        '        trFullyDay.Visible = True
        '    Else
        '        trFullyDay.Visible = False
        '    End If
        'End With

    End Sub

#End Region

#Region "Handle Selected Time changed"

    Protected Sub RadTPtoTim_SelectedDateChanged(ByVal sender As Object,
                                                         ByVal e As SelectedDateChangedEventArgs) _
                                                         Handles RadTPtoTime.SelectedDateChanged
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If (hdnIsvalid.Value) Then
            setTimeDifference()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TimeRange", CultureInfo))

        End If

    End Sub

    Protected Sub RadTPfromTime_SelectedDateChanged(ByVal sender As Object,
                                                             ByVal e As SelectedDateChangedEventArgs) _
                                                     Handles RadTPfromTime.SelectedDateChanged
        If (hdnIsvalid.Value) Then
            setTimeDifference()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TimeRange", CultureInfo))
        End If

    End Sub

    Private Function setTimeDifference() As TimeSpan
        Try
            Dim temp1 As DateTime = Nothing
            Dim temp2 As DateTime = Nothing

            temp1 = RadTPfromTime.SelectedDate.Value
            temp2 = RadTPtoTime.SelectedDate.Value
            Dim startTime As New DateTime(2011, 1, 1,
                                          temp1.Hour(), temp1.Minute(), temp1.Second)

            Dim endTime As New DateTime(2011, 1, 1,
                                          temp2.Hour(), temp2.Minute(), temp2.Second)


            Dim c As TimeSpan = (endTime.Subtract(startTime))
            Dim result As Integer =
                DateTime.Compare(endTime, startTime)

            Dim hours As Integer = 0
            Dim minutes As Integer = 0

            If result = 0 Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    txtTimeDifference.Text = 0 & " ساعات," &
                      0 & " دقائق"
                    txtTimeDifference.Style("text-align") = "right"
                Else
                    txtTimeDifference.Text = 0 & " hours," &
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
                    txtTimeDifference.Text = c.Hours() & " ساعات," &
                     TotalMinutes & " دقائق"
                    txtTimeDifference.Style("text-align") = "right"
                Else
                    txtTimeDifference.Text = c.Hours() & " hours," &
                     TotalMinutes & " minutes"

                End If

            ElseIf result < 0 Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    txtTimeDifference.Text = c.Hours() & " ساعات," &
                       Math.Ceiling(c.TotalMinutes()) & " دقائق"
                    txtTimeDifference.Style("text-align") = "right"
                Else
                    txtTimeDifference.Text = c.Hours() & " hours," &
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
                        txtTimeDifference.Text = hours & " ساعات," &
                          Math.Ceiling(c.TotalMinutes()) & " دقائق"
                        txtTimeDifference.Style("text-align") = "right"
                    Else
                        txtTimeDifference.Text = hours & " hours," &
                          Math.Ceiling(c.TotalMinutes()) & " minutes"

                    End If
                End If
            End If
        Catch ex As Exception

            MsgBox(ex.Message)

        End Try
    End Function

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmpPermissionRequest.Skin))
    End Function

    Protected Sub dgrdEmpPermissionRequest_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdEmpPermissionRequest.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

#End Region

End Class
