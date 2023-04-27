﻿Imports TA.Employees
Imports System.Data
Imports SmartV.UTILITIES
Imports Telerik.Web
Imports Telerik.Web.UI
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports System.Web
Imports Telerik.Web.UI.Calendar
Imports SmartV.UTILITIES.ProjectCommon
Imports TA.Lookup
Imports TA.Definitions
Imports System.IO
Imports TA.Security
Imports TA.DailyTasks
Imports TA.Admin

Partial Class Employee_UserControls_HR_PermissionRequest
    Inherits System.Web.UI.UserControl

#Region "Class Variables"
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

    Private Lang As New CtlCommon.Lang

    Private objEmployee As Employee
    Private objPermissionsTypes As PermissionsTypes
    Private objHR_PermissionRequest As HR_PermissionRequest
    Shared SortDir As String
    Shared SortExep As String
    Shared SortExepression As String
    Shared dtCurrent As DataTable
    Private objPermissionTypeOccurance As New PermissionTypeOccurance
    Private objPermissionTypeDuration As New PermissionTypeDuration
    Private objWorkSchedule_NormalTime As WorkSchedule_NormalTime
    Private objWorkSchedule_Flexible As WorkSchedule_Flexible
    Private objEmpWorkSchedule As Emp_WorkSchedule
    Private objWorkSchedule As WorkSchedule
    Private objEmp_Shifts As Emp_Shifts
    Private objHoliday As New Holiday
    Private objEmp_Leaves_BalanceHistory As New Emp_Leaves_BalanceHistory
    Private objEmp_Leaves As Emp_Leaves
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objWeekDays As New WeekDays
    Dim babyBirthDate As DateTime
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objVersion As SmartV.Version.version
    Dim objAPP_Settings As APP_Settings

#End Region

#Region "Properties"

    Public Property PermissionRequestId() As Integer
        Get
            Return ViewState("PermissionRequestId")
        End Get
        Set(ByVal value As Integer)
            ViewState("PermissionRequestId") = value
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

    Public Property dtOffDays() As DataTable
        Get
            Return ViewState("dtOffDays")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtOffDays") = value
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

    Public Property PermissionDaysCount() As Double
        Get
            Return ViewState("PermissionDaysCount")
        End Get
        Set(ByVal value As Double)
            ViewState("PermissionDaysCount") = value
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

    Public Property LeaveID() As Integer
        Get
            Return ViewState("LeaveID")
        End Get
        Set(ByVal value As Integer)
            ViewState("LeaveID") = value
        End Set
    End Property

    Public Property ValidationGroup() As String
        Get
            Return ViewState("ValidationGroup")
        End Get
        Set(ByVal value As String)
            ViewState("ValidationGroup") = value
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

    Public Property PermDate() As DateTime
        Get
            Return ViewState("PermDate")
        End Get
        Set(value As DateTime)
            ViewState("PermDate") = value
        End Set
    End Property

    Public Property PermEndDate() As DateTime
        Get
            Return ViewState("PermEndDate")
        End Get
        Set(value As DateTime)
            ViewState("PermEndDate") = value
        End Set
    End Property

    Public Property fileUploadExtension() As String
        Get
            Return ViewState("fileUploadExtension")
        End Get
        Set(value As String)
            ViewState("fileUploadExtension") = value
        End Set
    End Property
#End Region

#Region "Methods & Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack <> True Then

            ValidationGroup = btnSave.ValidationGroup
            EmployeeFilterUC.IsEmployeeRequired = True
            EmployeeFilterUC.CompanyRequiredValidationGroup = btnSave.ValidationGroup
            EmployeeFilterUC.EmployeeRequiredValidationGroup = btnSave.ValidationGroup

            SetRadDateTimePickerPeoperties()
            ManageDisplayMode_ExceptionalCases()
            'FillGridView()
            rmtFlexibileTime.TextWithLiterals = "0000"
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "en-US" Then
                Lang = CtlCommon.Lang.EN

            Else
                Lang = CtlCommon.Lang.AR
            End If

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            Page.UICulture = SessionVariables.CultureInfo
            userCtrlEmpPermHeader.HeaderText = ResourceManager.GetString("HR_PermissionRequest", CultureInfo)
            reqPermission.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            FillLists()
            btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + _
                                     dgrdVwEmpPermissions.ClientID + "');")
            ManageFunctionalities()

            dtpFromDateSearch.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            dtpToDateSearch.SelectedDate = dd

            CtlCommon.FillCheckBox(chkWeekDays, objWeekDays.GetAll())

            For i As Integer = 0 To chkWeekDays.Items.Count - 1
                chkWeekDays.Items(i).Text = String.Format("{0} {1}", " ", chkWeekDays.Items(i).Text)
            Next

            If (PermissionType = PermissionOption.Nursing) Then
                DisableEnableTimeControls(False)
                DisableEnableTimeView(False)
            End If

            rdlTimeOption.Items.FindByValue("0").Selected = True
            trFlixibleTime.Style("display") = "none"

            HideShowControl()

            'For Each item As ListItem In rdlTimeOption.Items
            '    item.Attributes.Add("onchange", "javascript:getSelectedListItem();")
            'Next

        End If
        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrentControls.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not updatePnlEmpPerm.FindControl(row("AddBtnName")) Is Nothing Then
                        updatePnlEmpPerm.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not updatePnlEmpPerm.FindControl(row("DeleteBtnName")) Is Nothing Then
                        updatePnlEmpPerm.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not updatePnlEmpPerm.FindControl(row("EditBtnName")) Is Nothing Then
                        updatePnlEmpPerm.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not updatePnlEmpPerm.FindControl(row("PrintBtnName")) Is Nothing Then
                        updatePnlEmpPerm.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

        ' You can put the suitable caption for the TimeView
        RadTPfromTime.TimeView.HeaderText = String.Empty
        RadTPtoTime.TimeView.HeaderText = String.Empty
        RadTPfromTime.TimeView.TimeFormat = "HH:mm"
        RadTPfromTime.TimeView.DataBind()
        RadTPtoTime.TimeView.TimeFormat = "HH:mm"
        RadTPtoTime.TimeView.DataBind()

        If (radBtnSpecificDays.Checked AndAlso PermissionType = PermissionOption.Normal) Or PermissionType = PermissionOption.Study Then

            For Each item As ListItem In chkWeekDays.Items
                item.Enabled = False
            Next

            Dim dtpStartDate As DateTime = dtpStartDatePerm.SelectedDate
            Dim dtpEndDate As DateTime = dtpEndDatePerm.SelectedDate

            Dim days As Integer = dtpEndDate.Subtract(dtpStartDate).Days + 1

            'if days less than week days to show only days selected
            If days < 7 Then
                While dtpStartDate <= dtpEndDate
                    Dim strDay As String = dtpStartDate.DayOfWeek.ToString()
                    Select Case strDay
                        Case "Monday"
                            chkWeekDays.Items.FindByValue(2).Enabled = True
                        Case "Tuesday"
                            chkWeekDays.Items.FindByValue(3).Enabled = True
                        Case "Wednesday"
                            chkWeekDays.Items.FindByValue(4).Enabled = True
                        Case "Thursday"
                            chkWeekDays.Items.FindByValue(5).Enabled = True
                        Case "Friday"
                            chkWeekDays.Items.FindByValue(6).Enabled = True
                        Case "Saturday"
                            chkWeekDays.Items.FindByValue(7).Enabled = True
                        Case "Sunday"
                            chkWeekDays.Items.FindByValue(1).Enabled = True
                    End Select
                    dtpStartDate = dtpStartDate.AddDays(1)
                End While

                For Each item As ListItem In chkWeekDays.Items
                    If item.Enabled = False Then
                        item.Selected = False
                    End If
                Next
            Else
                For Each item As ListItem In chkWeekDays.Items
                    item.Enabled = True
                Next
            End If

        End If
    End Sub

    Private Sub ManageDisplayMode_ExceptionalCases()
        '  Current function handle the cases where the displayMode 
        '  associated with the in appropriate value for 
        '  the PermissionRequestId
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim _Exist As Integer = 0
        _Exist = IS_Exist()
        If DisplayMode = DisplayModeEnum.View Or _
            DisplayMode = DisplayModeEnum.Edit Then
            If _Exist = -1 Then
                ' If the display mode is View or edit.But, the 
                ' Id is not a valid one
                CtlCommon.ShowMessage( _
                    Me.Page, _
                    ResourceManager.GetString("ErrorPermitProcessing", CultureInfo), "error")
                DisplayMode = DisplayModeEnum.ViewAll
            End If
        ElseIf DisplayMode = DisplayModeEnum.ViewAddEdit Or _
            DisplayModeEnum.ViewAll = DisplayMode Then
            PermissionRequestId = 0
        ElseIf DisplayMode() = DisplayModeEnum.Add Then
            If PermissionRequestId > 0 Then
                If _Exist = 0 Then
                    DisplayMode = DisplayModeEnum.Edit
                Else
                    ' PermissionRequestId>0 and does not matched a data base record
                    PermissionRequestId = 0
                End If
            Else
                ' if PermissionRequestId=0 or PermissionRequestId<0
                PermissionRequestId = 0
            End If
        End If
    End Sub

    Private Sub SetRadDateTimePickerPeoperties()


        ' This function set properties for terlerik controls

        'Imports Telerik.Web.UI.DatePickerPopupDirection

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

    Private Function IS_Exist() As Integer
        ' The View , and Edit modes require to have a valid Leave Id 
        objHR_PermissionRequest = New HR_PermissionRequest()
        objHR_PermissionRequest.PermissionRequestId = PermissionRequestId
        Dim _EXIT As Integer = 0
        If PermissionRequestId <= 0 Then
            _EXIT = -1
        ElseIf objHR_PermissionRequest.Find_Existing() = False Then
            _EXIT = -1
        End If
        Return _EXIT
    End Function

    Public Sub FillGridView()
        Try
            objHR_PermissionRequest = New HR_PermissionRequest()
            objHR_PermissionRequest.FK_EmployeeId = EmployeeFilterUC.EmployeeId
            objHR_PermissionRequest.PermissionOption = PermissionType
            objHR_PermissionRequest.PermDate = dtpFromDateSearch.SelectedDate
            objHR_PermissionRequest.PermEndDate = dtpToDateSearch.SelectedDate
            dtCurrent = objHR_PermissionRequest.GetAllInnerJoin()
            Dim dv As New DataView(dtCurrent)
            dv.Sort = SortExepression
            dgrdVwEmpPermissions.DataSource = dv
            dgrdVwEmpPermissions.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClearAll()
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
        PermissionRequestId = 0
        ' Reset mode to one day permission 
        ShowHide(False)
        'rdlPermissionOption.Items.FindByValue("1").Selected = True
        radBtnOneDay.Checked = True
        radBtnPeriod.Checked = False
        radBtnSpecificDays.Checked = False
        ' Remove sorting and sorting arrow 
        SortExepression = Nothing
        SortDir = Nothing
        For i As Integer = 0 To chkWeekDays.Items.Count - 1
            chkWeekDays.Items(i).Selected = False
        Next

        rdlTimeOption.Items.FindByValue("0").Selected = True
        rdlTimeOption.Items.FindByValue("1").Selected = False
        rmtFlexibileTime.TextWithLiterals = "0000"
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

        HideShowControl()
        'Dim dtClear As New DataTable
        'dgrdVwEmpPermissions.DataSource = dtClear
        'dgrdVwEmpPermissions.DataBind()

        'EmployeeFilterUC.ClearValues()

        lnbLeaveFile.Visible = False
        lnbRemove.Visible = False
        lnbLeaveFile.HRef = String.Empty
        lblNoAttachedFile.Visible = False
        fuAttachFile.Visible = True
        If PermissionType = PermissionOption.Normal Then
            DisableEnableTimeView(Not chckFullDay.Checked)
            rdlTimeOption.Enabled = Not chckFullDay.Checked
        End If

        If PermissionType = PermissionOption.Nursing Then
            trNursingFlexibleDurationPermission.Visible = True
            RadCmbFlixebleDuration.ClearSelection()
        End If

        dtpFromDateSearch.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
        Dim dd As New Date
        dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

        dtpToDateSearch.SelectedDate = dd

    End Sub

    Private Sub FillLists()
        ' Fill telerik combo boxes
        Dim dt As DataTable = Nothing
        ' Get the permissions
        objPermissionsTypes = New PermissionsTypes()
        dt = objPermissionsTypes.GetAll()
        If dt IsNot Nothing Then
            CtlCommon.FillTelerikDropDownList(RadCmpPermissions, dt, Lang)
        End If
        dt = Nothing
        ' Get the employees 
        objEmployee = New Employee()
        dt = objEmployee.GetAll()
        If dt IsNot Nothing Then

            'RadCmbEmployee.DataSource = dt
            'setLocalizedTextField(RadCmbEmployee, "EmployeeName", "EmployeeArabicName")
            'RadCmbEmployee.DataTextField = "EmployeeName"
            'RadCmbEmployee.DataValueField = "EmployeeId"
            'RadCmbEmployee.DataBind()
        End If
    End Sub

    Private Sub FillControls()

        objHR_PermissionRequest = New HR_PermissionRequest()
        objHR_PermissionRequest.PermissionRequestId = PermissionRequestId
        ' If the PermissionRequestId is not a valid one , the below line will through 
        ' an exception
        objHR_PermissionRequest.GetByPK()
        With objHR_PermissionRequest
            ' Fill combo boxes
            'RadCmbEmployee.SelectedValue = .FK_EmployeeId
            EmployeeFilterUC.EmployeeId = .FK_EmployeeId
            RadCmpPermissions.SelectedValue = .FK_PermId
            ' Fill checkBoxes
            chckIsDividable.Checked = .IsDividable
            FileExtension = .AttachedFile

            'Dim FilePath As String = Server.MapPath("..\..\PermissionFiles\" + PermissionId.ToString() + .AttachedFile)

            fuAttachFile.Visible = True
            Dim FilePath As String = Server.MapPath("~/HR_PermissionRequestFiles")
            FilePath = FilePath + "\" + PermissionRequestId.ToString() + .AttachedFile
            If File.Exists(FilePath) Then
                lnbLeaveFile.HRef = "..\..\HR_PermissionRequestFiles\" + PermissionRequestId.ToString() + .AttachedFile
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
                fileUploadExtension = .AttachedFile
                lblNoAttachedFile.Visible = False
            Else
                lnbLeaveFile.Visible = False
                lnbRemove.Visible = False
                lblNoAttachedFile.Visible = True
            End If

            'Else
            '    Exit Sub


            'End If

            If Not PermissionType = PermissionOption.Nursing Then
                If (.IsFlexible) Then
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
                trNursingFlexibleDurationPermission.Visible = False
                chckIsFlexible.Checked = .IsFlexible
                radBtnSpecificDays.Checked = .IsSpecificDays
                'chckSpecifiedDays.Checked = .IsSpecificDays
                ' Fill the time & date pickers
                RadTPfromTime.SelectedDate = .FromTime
                RadTPtoTime.SelectedDate = .ToTime
                txtDays.Text = .BalanceDays


            Else
                trSpecificTime.Style("display") = "none"
                trNursingFlexibleDurationPermission.Visible = True
                RadCmbFlixebleDuration.SelectedValue = .FlexibilePermissionDuration
            End If

            txtRemarks.Text = .Remark


            If (radBtnSpecificDays.Checked AndAlso PermissionType = PermissionOption.Normal) Or PermissionType = PermissionOption.Study Then
                chkWeekDays.ClearSelection()
                For Each item As ListItem In chkWeekDays.Items
                    item.Enabled = False
                Next
                dtpStartDatePerm.SelectedDate = .PermDate
                dtpEndDatePerm.SelectedDate = .PermEndDate

                Dim dtpStartDate As DateTime = .PermDate
                Dim dtpEndDate As DateTime = .PermEndDate

                Dim days As Integer = dtpEndDate.Subtract(dtpStartDate).Days + 1

                'if days less than week days to show only days selected
                If days < 7 Then
                    While dtpStartDate <= dtpEndDate
                        Dim strDay As String = dtpStartDate.DayOfWeek.ToString()
                        Select Case strDay
                            Case "Monday"
                                chkWeekDays.Items.FindByValue(2).Enabled = True
                            Case "Tuesday"
                                chkWeekDays.Items.FindByValue(3).Enabled = True
                            Case "Wednesday"
                                chkWeekDays.Items.FindByValue(4).Enabled = True
                            Case "Thursday"
                                chkWeekDays.Items.FindByValue(5).Enabled = True
                            Case "Friday"
                                chkWeekDays.Items.FindByValue(6).Enabled = True
                            Case "Saturday"
                                chkWeekDays.Items.FindByValue(7).Enabled = True
                            Case "Sunday"
                                chkWeekDays.Items.FindByValue(1).Enabled = True
                        End Select
                        dtpStartDate = dtpStartDate.AddDays(1)
                    End While

                    For Each item As ListItem In chkWeekDays.Items
                        If item.Enabled = False Then
                            item.Selected = False
                        End If
                    Next
                Else
                    For Each item As ListItem In chkWeekDays.Items
                        item.Enabled = True
                    Next
                End If

            End If

            If (Not String.IsNullOrEmpty(.Days)) Then
                If (.Days.Contains(",")) Then
                    Dim daysArr() As String = .Days.Split(",")
                    For j As Integer = 0 To daysArr.Length - 1
                        If (Not String.IsNullOrEmpty(daysArr(j))) Then
                            chkWeekDays.Items.FindByValue(daysArr(j)).Selected = True
                            chkWeekDays.Items.FindByValue(daysArr(j)).Enabled = True
                        End If
                    Next
                End If
            End If


            '''''''''''''''''''''''''''''''''''''''''''
            ' Enable / Disable time pickers according to the full day 
            ' value
            chckFullDay.Checked = .IsFullDay
            ManageSomeControlsStatus(.IsForPeriod, .PermDate, .PermEndDate, .IsFullDay, .IsFlexible)
            Me.dtpPermissionDate.SelectedDate = .PermDate
            Me.dtpEndDatePerm.SelectedDate = .PermEndDate
        End With
    End Sub

    Private Sub ManageSomeControlsStatus(ByVal IsForPeriod As Boolean, _
                                         ByVal PermDate As DateTime, _
                                         ByVal PermEndDate As DateTime?, _
                                         ByVal FullDay As Boolean, ByVal IsFlexible As Boolean)
        ' Manage the DatePickers according 
        ' to the permission type
        ShowHide(IsForPeriod)
        radBtnOneDay.Checked = Not IsForPeriod
        radBtnPeriod.Checked = IsForPeriod


        ' If the permission for full day , means to disable the TimeView(s)
        ' otherwise means to enable or keep it enable
        If FullDay = False Then
            DisableEnableTimeView(True)
            rdlTimeOption.Enabled = True
            If IsFlexible = True Then
                SetRadDateTimePickerPeoperties()
            End If
            If Not PermissionType = PermissionOption.Nursing Then
                setTimeDifference()
            End If
            trTime.Visible = True
            trDifTime.Visible = True
        Else
            DisableEnableTimeView(False)
            rdlTimeOption.Enabled = False
            txtTimeDifference.Text = String.Empty
            trTime.Visible = False
            trDifTime.Visible = False
        End If

        If IsForPeriod = False Then
            dtpPermissionDate.SelectedDate = PermDate
        Else
            Me.dtpStartDatePerm.SelectedDate = PermDate
            Me.dtpEndDatePerm.SelectedDate = _
                IIf(CheckDate(PermEndDate) = Nothing, Nothing, PermEndDate)
        End If
        ' Disable all controls in some display modes 
        If DisplayMode.ToString() = "ViewAll" Or DisplayMode.ToString() = "View" Then
            txtTimeDifference.Enabled = False
            RadTPfromTime.Enabled = False
            RadTPtoTime.Enabled = False
        End If
    End Sub

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

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim found As Boolean = False

        'If hdnIsvalid.Value <> String.Empty Then
        '    If (hdnIsvalid.Value) Then

        '    Else
        '        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TimeRange", CultureInfo))
        '        Return
        '    End If
        'End If

        If (PermissionType = PermissionOption.Nursing) Then
            objEmployee = New Employee()
            objEmployee = EmployeeFilterUC.GetEmployeeAllInfo()
            If (objEmployee.Gender = "m") Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NursingValidation", CultureInfo), "info")
                Return
            End If
        End If

        If (PermissionType = PermissionOption.Normal) Then
            objPermissionsTypes = New PermissionsTypes
            With objPermissionsTypes
                .PermId = RadCmpPermissions.SelectedValue
                .GetByPK()
                If .AttachmentIsMandatory = True Then
                    If lnbLeaveFile.Visible = False Then
                        If fuAttachFile.HasFile = False Then
                            If Lang = CtlCommon.Lang.AR Then
                                CtlCommon.ShowMessage(Me.Page, "يرجى ارفاق ملف!", "info")
                            Else
                                CtlCommon.ShowMessage(Me.Page, "Please Attach File!", "info")
                            End If
                            Exit Sub
                        End If
                    End If
                End If

                If .RemarksIsMandatory = True Then
                    If txtRemarks.Text = String.Empty Or txtRemarks.Text = "" Then
                        If Lang = CtlCommon.Lang.AR Then
                            CtlCommon.ShowMessage(Me.Page, "يرجى ادخال الملاحظات", "info")
                        Else
                            CtlCommon.ShowMessage(Me.Page, "Please Enter Remarks", "info")
                        End If
                        Exit Sub
                    End If
                End If
            End With
        End If

        'If dtpEndDatePerm.SelectedDate IsNot Nothing And IS_Period() Then
        '    If Not StartEndDateComparison(dtpStartDatePerm.SelectedDate, _
        '                                    dtpEndDatePerm.SelectedDate) Then
        '        Return
        '    End If
        'End If

        If radBtnSpecificDays.Checked Then
            For Each item As ListItem In chkWeekDays.Items
                If item.Selected Then
                    found = True
                    Exit For
                End If
            Next
        Else
            found = True
        End If

        If found Then
            Select Case DisplayMode.ToString
                Case "Add"
                    saveUpdate()
                Case "Edit"
                    updateOnly()
                Case "ViewAddEdit"
                    saveUpdate()
                Case Else
            End Select
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SelectAtLeastOneDay", CultureInfo), "info")
        End If
    End Sub

    Private Function IS_Period() As Boolean
        'Return rdlPermissionOption.Items.FindByValue("2").Selected
        Return radBtnPeriod.Checked
    End Function

    Protected Sub btnDelete_Click(ByVal sender As Object, _
                                  ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1
        Dim intPermTypeId As Integer
        Dim intPermDays As Double
        Dim EmployeeId As Integer

        objRECALC_REQUEST = New RECALC_REQUEST
        Dim temp_date As Date
        Dim temp_str_date As String
        Dim err2 As Integer

        For Each row As GridDataItem In dgrdVwEmpPermissions.Items
            Dim cb As CheckBox = DirectCast(row.FindControl("chkRow"), CheckBox)
            If cb.Checked Then

                EmployeeId = Convert.ToInt32(row.GetDataKeyValue("FK_EmployeeId"))
                Dim intPermissionRequestId As Integer = _
                Convert.ToInt32(row.GetDataKeyValue("PermissionRequestId"))

                If ((Not row.GetDataKeyValue("FK_PermId").ToString() = "") And (Not String.IsNullOrEmpty(row.GetDataKeyValue("FK_PermId").ToString()))) Then
                    intPermTypeId = _
                    Convert.ToInt32(row.GetDataKeyValue("FK_PermId"))
                End If

                'Get Leave Id
                objPermissionsTypes = New PermissionsTypes
                objPermissionsTypes.PermId = intPermTypeId
                objPermissionsTypes.GetByPK()
                LeaveID = objPermissionsTypes.FK_LeaveIdDeductBalance


                ' Delete current checked item
                objHR_PermissionRequest = New HR_PermissionRequest()
                objHR_PermissionRequest.PermissionRequestId = intPermissionRequestId
                objHR_PermissionRequest.GetByPK()
                If Not objHR_PermissionRequest.Days = "&nbsp;" Then
                    intPermDays = objHR_PermissionRequest.Days
                End If

                errNum = objHR_PermissionRequest.Delete()

                If (errNum = 0) Then
                    If (LeaveID <> 0) Then
                        'Delete from balance
                        objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
                        objEmp_Leaves_BalanceHistory.FK_EmployeeId = EmployeeId
                        objEmp_Leaves_BalanceHistory.FK_LeaveId = LeaveID
                        objEmp_Leaves_BalanceHistory.FK_EmpPermId = intPermissionRequestId


                        objEmp_Leaves_BalanceHistory.GetLastBalance()

                        objEmp_Leaves_BalanceHistory.BalanceDate = Date.Now
                        objEmp_Leaves_BalanceHistory.Balance = intPermDays
                        objEmp_Leaves_BalanceHistory.TotalBalance = objEmp_Leaves_BalanceHistory.TotalBalance + intPermDays
                        objEmp_Leaves_BalanceHistory.Remarks = "Delete Permission Data - Rollback Balance"
                        objEmp_Leaves_BalanceHistory.CREATED_DATE = Date.Now
                        objEmp_Leaves_BalanceHistory.CREATED_BY = "Username"
                        objEmp_Leaves_BalanceHistory.AddBalance()
                    End If

                    Dim isForPeriod As Boolean = Convert.ToBoolean(row.GetDataKeyValue("IsForPeriod"))

                    If Not isForPeriod AndAlso PermissionType = PermissionOption.Normal Then
                        temp_date = Convert.ToDateTime(row.GetDataKeyValue("PermDate"))
                        temp_str_date = DateToString(temp_date)
                        objRECALC_REQUEST.EMP_NO = EmployeeId
                        objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)

                        Dim count As Integer
                        While count < 5
                            err2 = objRECALC_REQUEST.RECALCULATE()
                            If err2 = 0 Then
                                Exit While
                            End If
                            count += 1
                        End While

                    Else

                        Dim dteFrom As DateTime = Convert.ToDateTime(row.GetDataKeyValue("PermDate"))
                        Dim dteTo As DateTime = Convert.ToDateTime(row.GetDataKeyValue("PermEndDate"))

                        While dteFrom <= dteTo
                            If Not dteFrom = Date.Now.AddDays(1).ToShortDateString() Then
                                temp_str_date = DateToString(dteFrom)
                                objRECALC_REQUEST = New RECALC_REQUEST()
                                objRECALC_REQUEST.EMP_NO = EmployeeId
                                objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)
                                err2 = objRECALC_REQUEST.RECALCULATE()
                                'If Not err2 = 0 Then
                                '    Exit While
                                'End If
                            End If
                            dteFrom = dteFrom.AddDays(1)
                        End While
                    End If

                End If
            End If

        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            ClearAll()
            EmployeeFilterUC.ClearValues()
            FillGridView()
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If
        ClearAll()
        EmployeeFilterUC.ClearValues()
        dgrdVwEmpPermissions.DataSource = Nothing
        dgrdVwEmpPermissions.DataBind()
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, _
                                 ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
        Dim dtClear As New DataTable
        dgrdVwEmpPermissions.DataSource = dtClear
        dgrdVwEmpPermissions.DataBind()
        EmployeeFilterUC.ClearValues()

    End Sub

    'Protected Sub lnkEmployeeName_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    ' Get the PK from the hidden field
    '    PermissionRequestId = Convert.ToInt32(DirectCast(dgrdVwEmpPermissions.SelectedItems(0), GridDataItem)("PermissionRequestId").Text)
    '    FillControls()
    'End Sub

    Protected Sub dgrdVwEmpPermissions_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdVwEmpPermissions.SelectedIndexChanged

        lnbLeaveFile.Visible = False
        lnbLeaveFile.HRef = String.Empty

        PermissionRequestId = Convert.ToInt32(DirectCast(dgrdVwEmpPermissions.SelectedItems(0), GridDataItem).GetDataKeyValue("PermissionRequestId"))
        If (Not String.IsNullOrEmpty(DirectCast(dgrdVwEmpPermissions.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_LeaveId").ToString())) And (Not (DirectCast(dgrdVwEmpPermissions.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_LeaveId").ToString()) = "") Then
            LeaveID = Convert.ToInt32(DirectCast(dgrdVwEmpPermissions.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_LeaveId"))
        End If

        FillControls()

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

    Protected Sub lnbRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnbRemove.Click
        If Not (PermissionRequestId = 0) Then
            Dim fileName As String = PermissionRequestId.ToString()
            Dim fPath As String = String.Empty
            fPath = Server.MapPath("..\PermissionFiles\\" + fileName + FileExtension)

            If File.Exists(fPath) Then
                File.Delete(fPath)
                lnbLeaveFile.Visible = False
                lnbRemove.Visible = False
                lblNoAttachedFile.Visible = True
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            End If
        End If
    End Sub

    Protected Sub btnGet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGet.Click
        Try
            objHR_PermissionRequest = New HR_PermissionRequest()
            objHR_PermissionRequest.FK_EmployeeId = EmployeeFilterUC.EmployeeId
            objHR_PermissionRequest.PermissionOption = PermissionType
            objHR_PermissionRequest.PermDate = dtpFromDateSearch.SelectedDate
            objHR_PermissionRequest.PermEndDate = dtpToDateSearch.SelectedDate
            dtCurrent = objHR_PermissionRequest.GetAllInnerJoin()
            Dim dv As New DataView(dtCurrent)
            dv.Sort = SortExepression
            dgrdVwEmpPermissions.DataSource = dv
            dgrdVwEmpPermissions.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub RadCmpPermissions_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadCmpPermissions.SelectedIndexChanged
        If Not RadCmpPermissions.SelectedValue = -1 Then
            objPermissionsTypes = New PermissionsTypes()
            With objPermissionsTypes
                .PermId = RadCmpPermissions.SelectedValue
                .GetByPK()
                If Not .FK_LeaveIdtoallowduration = 0 Then
                    LeaveTypeDuration = .DurationAllowedwithleave
                End If
            End With
        End If
    End Sub

#End Region

#Region "GridView Paging and sorting"
    'Protected Sub dgrdVwEmpPermissions_PageIndexChanging(ByVal sender As Object, _
    '                                                     ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dgrdVwEmpPermissions.PageIndexChanging
    '    Try
    '        dgrdVwEmpPermissions.SelectedIndex = -1
    '        dgrdVwEmpPermissions.PageIndex = e.NewPageIndex
    '        Dim dv As New DataView(dtCurrent)
    '        dv.Sort = SortExepression
    '        dgrdVwEmpPermissions.DataSource = dv
    '        dgrdVwEmpPermissions.DataBind()
    '        ManageFunctionalities()
    '        DisableEnableTimeView(Not Is_FullDay())
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Protected Sub dgrdVwEmpPermissions_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdVwEmpPermissions.DataBound

        'SetArrowDirection()
    End Sub

    Protected Sub dgrdVwEmpPermissions_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdVwEmpPermissions.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromTime").ToString())) And (Not item.GetDataKeyValue("FromTime").ToString() = "")) Then
                Dim fromTime As DateTime = item.GetDataKeyValue("FromTime")
                item("FromTime").Text = fromTime.ToString("HH:mm")
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ToTime").ToString())) And (Not item.GetDataKeyValue("ToTime").ToString() = "")) Then
                Dim toTime As DateTime = item.GetDataKeyValue("ToTime")
                item("ToTime").Text = toTime.ToString("HH:mm")
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("PermDate").ToString())) And (Not item.GetDataKeyValue("PermDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("PermDate")
                'item("PermDate").Text = fromDate.ToShortDateString
                item("PermDate").Text = fromDate.ToString("dd/MM/yyyy")
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("PermEndDate").ToString())) And (Not item.GetDataKeyValue("PermEndDate").ToString() = "")) Then
                Dim toDate As DateTime = item.GetDataKeyValue("PermEndDate")
                'item("PermEndDate").Text = toDate.ToShortDateString
                item("PermEndDate").Text = toDate.ToString("dd/MM/yyyy")
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

            'item("CustomerID").Text = "Telerik"
        End If
    End Sub

    'Protected Sub dgrdVwEmpPermissions_Sorting(ByVal sender As Object, _
    '                                           ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles dgrdVwEmpPermissions.Sorting
    '    Try
    '        If SortDir = "ASC" Then
    '            SortDir = "DESC"
    '        Else
    '            SortDir = "ASC"
    '        End If
    '        SortExepression = e.SortExpression & Space(1) & SortDir
    '        Dim dv As New DataView(dtCurrent)
    '        dv.Sort = SortExepression
    '        SortExep = e.SortExpression
    '        dgrdVwEmpPermissions.DataSource = dv
    '        dgrdVwEmpPermissions.DataBind()
    '        ManageFunctionalities()



    '        DisableEnableTimeView(Not Is_FullDay())




    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub DisableEnableTimeControls(ByVal status As Boolean)
        RadTPfromTime.Enabled = status
        RadTPtoTime.Enabled = status
        txtTimeDifference.Enabled = status
    End Sub

    'Private Sub SetArrowDirection()



    '    Dim img As System.Web.UI.WebControls.Image = New System.Web.UI.WebControls.Image()
    '    If Not SortDir = Nothing AndAlso Not SortDir = String.Empty Then
    '        If SortDir = "ASC" Then
    '            img.ImageUrl = "~/images/ar-down.png"
    '        Else
    '            img.ImageUrl = "~/images/ar-up.png"
    '        End If



    '        Select Case SortExep
    '            Case "EmployeeName"
    '                dgrdVwEmpPermissions.HeaderRow.Cells(1).Controls.Add(New LiteralControl(" "))
    '                dgrdVwEmpPermissions.HeaderRow.Cells(1).Controls.Add(img)
    '            Case "PermName"
    '                dgrdVwEmpPermissions.HeaderRow.Cells(2).Controls.Add(New LiteralControl(" "))
    '                dgrdVwEmpPermissions.HeaderRow.Cells(2).Controls.Add(img)
    '            Case "PermDate"
    '                dgrdVwEmpPermissions.HeaderRow.Cells(3).Controls.Add(New LiteralControl(" "))
    '                dgrdVwEmpPermissions.HeaderRow.Cells(3).Controls.Add(img)
    '            Case "IsFullDay"
    '                dgrdVwEmpPermissions.HeaderRow.Cells(4).Controls.Add(New LiteralControl(" "))
    '                dgrdVwEmpPermissions.HeaderRow.Cells(4).Controls.Add(img)


    '        End Select
    '    End If
    'End Sub

    Protected Sub dgrdVwEmpPermissions_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdVwEmpPermissions.NeedDataSource
        objHR_PermissionRequest = New HR_PermissionRequest()
        objHR_PermissionRequest.FK_EmployeeId = EmployeeFilterUC.EmployeeId
        objHR_PermissionRequest.PermissionOption = PermissionType
        'objHR_PermissionRequest.PermDate = DateSerial(Today.Year, Today.Month, 1)
        'objHR_PermissionRequest.PermEndDate = DateSerial(Today.Year, Today.Month + 1, 0)
        objHR_PermissionRequest.PermDate = dtpFromDateSearch.SelectedDate
        objHR_PermissionRequest.PermEndDate = dtpToDateSearch.SelectedDate
        dtCurrent = objHR_PermissionRequest.GetAllInnerJoin()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdVwEmpPermissions.DataSource = dv
    End Sub

#End Region

#Region "Mnage periodical option overhead"

    Protected Sub radBtnPeriod_CheckedChanged(ByVal sender As Object, _
                                              ByVal e As System.EventArgs) Handles radBtnPeriod.CheckedChanged
        ShowHide(True)
    End Sub

    Protected Sub radBtnOneDay_CheckedChanged(ByVal sender As Object, _
                                              ByVal e As System.EventArgs) Handles radBtnOneDay.CheckedChanged
        ShowHide(False)
    End Sub

    Protected Sub radBtnSpecificDays_CheckedChanged(ByVal sender As Object, _
                                              ByVal e As System.EventArgs) Handles radBtnSpecificDays.CheckedChanged
        ShowHide(True)
    End Sub

    Private Sub ShowHide(ByVal IsPeriod As Boolean)
        pnlPeriodLeave.Visible = IsPeriod
        PnlOneDayLeave.Visible = Not IsPeriod
        reqEndDate.Enabled = IsPeriod
        CVDate.Enabled = IsPeriod
        'radBtnOneDay.Checked = Not IsPeriod
        'radBtnPeriod.Checked = IsPeriod

        If (radBtnSpecificDays.Checked) Then
            trWeekDays.Style("display") = "block"
        Else
            trWeekDays.Style("display") = "none"
        End If

    End Sub

#End Region

#Region "Display Mode Functions"

    Sub ManageFunctionalities()
        Select Case DisplayMode.ToString
            Case "Add"
                Adddisplaymode()
            Case "Edit"
                ViewEditmode()
            Case "View"
                Viewdisplaymode()
            Case "ViewAll"
                ViewAllDisplaymode()
            Case "ViewAddEdit"
                ViewAddEditDisplaymode()
            Case Else
        End Select
    End Sub

    Public Sub LoadData()
        FillGridView()
        ManageFunctionalities()
    End Sub

    Sub ViewAddEditDisplaymode()
        RefreshControls(True)
    End Sub

    Sub Adddisplaymode()
        RefreshControls(True)
        dgrdVwEmpPermissions.Visible = False
        btnDelete.Visible = False
        btnClear.Visible = False
    End Sub

    Sub Viewdisplaymode()
        RefreshControls(True)
        ManageControls(False)
        btnDelete.Visible = False
        btnClear.Visible = False
        btnSave.Visible = False
        btnClear.Visible = False
        FillControls()
        dgrdVwEmpPermissions.Visible = False

        For Each row As GridViewRow In dgrdVwEmpPermissions.Items

            Dim lnb As LinkButton = DirectCast(row.FindControl("lnkEmployeeName"), LinkButton)
            lnb.Visible = True
            Dim cb As CheckBox = DirectCast(row.FindControl("chkRow"), CheckBox)
            cb.Visible = False
        Next
    End Sub

    Sub ViewAllDisplaymode()
        RefreshControls(True)
        ManageControls(False)
        btnDelete.Visible = False
        btnClear.Visible = False
        btnSave.Visible = False
        btnSave.Visible = False
        btnClear.Visible = False
        For Each row As GridViewRow In dgrdVwEmpPermissions.Items
            Dim lb As LinkButton = DirectCast(row.FindControl("lnkEmployeeName"), LinkButton)
            Dim cb As CheckBox = DirectCast(row.FindControl("chkRow"), CheckBox)
            cb.Visible = False
            lb.Enabled = True
        Next
    End Sub

    Sub ViewEditmode()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        RefreshControls(True)
        btnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)

        FillControls()
        dgrdVwEmpPermissions.Visible = False
        btnClear.Visible = False
        btnDelete.Visible = False
    End Sub

    Sub ManageControls(ByVal Status As Boolean)

        ' Get Values from the combo boxes
        'RadCmbEmployee.Enabled = Status
        EmployeeFilterUC.ddlEmployeeEnabled = Status
        RadCmpPermissions.Enabled = Status
        ' Get values from the check boxes
        chckIsDividable.Enabled = Status
        chckIsFlexible.Enabled = Status
        chckFullDay.Enabled = Status
        chckSpecifiedDays.Enabled = Status
        txtRemarks.Enabled = Status
        ' Get values from rad controls
        dtpStartDatePerm.Enabled = Status
        dtpEndDatePerm.Enabled = Status
        RadTPfromTime.Enabled = Status
        RadTPtoTime.Enabled = Status
        dtpStartDatePerm.Enabled = Status
        dtpEndDatePerm.Enabled = Status
        dtpPermissionDate.Enabled = Status
        radBtnOneDay.Enabled = Status
        radBtnPeriod.Enabled = Status
        ' Toggle the status of the check boxes at the GridView column
        For Each row As GridViewRow In dgrdVwEmpPermissions.Items
            DirectCast(row.FindControl("chkRow"), CheckBox).Enabled = Status

        Next
    End Sub

    Sub RefreshControls(ByVal status As Boolean)
        ManageControls(True)
        For Each row As GridViewRow In dgrdVwEmpPermissions.Items
            ' Show the select field of the GridView
            Dim lnb As LinkButton = DirectCast(row.FindControl("lnkEmployeeName"), LinkButton)
            lnb.Visible = True
            ' Hide check boxes at check box column
            Dim cb As CheckBox = DirectCast(row.FindControl("chkRow"), CheckBox)
            cb.Visible = True
        Next
        btnSave.Text = GetLocalResourceObject("btnSaveResource1.Text")
        ' Toggle the status of the buttons
        btnDelete.Visible = status
        btnClear.Visible = status
        btnSave.Visible = status
    End Sub

    Function saveUpdate() As Integer
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim errNo As Integer
        objHR_PermissionRequest = New HR_PermissionRequest()
        OffAndHolidayDays = 0
        EmpLeaveTotalBalance = 0
        Dim errorNum As Integer = -1
        Dim ErrorMessage As String = String.Empty
        Dim tpFromTime As DateTime?
        Dim tpToTime As DateTime?
        Dim isFlixible As Boolean
        Dim strFlexibileDuration As String

        If (rdlTimeOption.Items.FindByValue("0").Selected) Then
            isFlixible = False
        Else
            isFlixible = True
        End If

        If ((PermissionType = PermissionOption.Nursing)) Then
            tpFromTime = CType(Nothing, DateTime?)
            tpToTime = CType(Nothing, DateTime?)
            strFlexibileDuration = RadCmbFlixebleDuration.SelectedValue
            isFlixible = True
        Else
            tpFromTime = RadTPfromTime.SelectedDate
            tpToTime = RadTPtoTime.SelectedDate
            strFlexibileDuration = (CInt(rmtFlexibileTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtFlexibileTime.TextWithLiterals.Split(":")(1))
        End If

        If isFlixible = False Then
            If Not chckFullDay.Checked Then
                If Not ValidatePermissionWorkingTime() Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("WorkScheduleTiemRange"), "info")
                    Return -1
                End If
            End If
        End If

        If isFlixible = False AndAlso PermissionType = PermissionOption.Normal Then
            If Not CheckPermissionTypeDuration() Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PermissionTypeDurationLimit"), "info")
                Return -1
            End If
        End If

        If PermissionType = PermissionOption.Normal Then
            If rdlTimeOption.SelectedValue = 0 Then
                If (objHR_PermissionRequest.ValidateEmployeePermission(EmployeeFilterUC.EmployeeId, PermissionRequestId, dtCurrent, radBtnOneDay.Checked, dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate, tpFromTime, _
                                                                 tpToTime, dtpPermissionDate.SelectedDate, objPermissionsTypes, RadCmpPermissions.SelectedValue, chckFullDay.Checked, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
                    If (ErrorMessage <> String.Empty) Then
                        CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                        Return -1
                    End If
                End If
            End If
        End If

        Dim days As String = String.Empty

        If (chckFullDay.Checked) Then
            EmpLeaveTotalBalance = EmpLeaveTotalBalance - 1
        End If

        For i As Integer = 0 To chkWeekDays.Items.Count - 1
            If (chkWeekDays.Items(i).Selected) Then
                days &= chkWeekDays.Items(i).Value & ","
            End If
        Next

        ' Set data into object for Add / UpdateIf

        'objHR_PermissionRequest.PermissionRequestId = PermissionRequestId
        objHR_PermissionRequest.LeaveId = LeaveID

        fileUploadExtension = Path.GetExtension(fuAttachFile.PostedFile.FileName)
        If String.IsNullOrEmpty(fileUploadExtension) Then
            fileUploadExtension = FileExtension
        End If
        With objHR_PermissionRequest
            .FK_EmployeeId = EmployeeFilterUC.EmployeeId


        End With
        FillObjectData()

        If PermissionRequestId = 0 Then
            objHR_PermissionRequest.PermissionOption = PermissionType
            objHR_PermissionRequest.CREATED_BY = SessionVariables.LoginUser.ID
            errNo = objHR_PermissionRequest.Add()
            PermissionRequestId = objHR_PermissionRequest.PermissionRequestId

            If fileUploadExtension IsNot Nothing Then
                Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                Dim fileName As String = PermissionRequestId.ToString()
                Dim fPath As String = String.Empty
                fPath = Server.MapPath("..\HR_PermissionRequestFiles\\" + fileName + extention)
                fuAttachFile.PostedFile.SaveAs(fPath)
            End If

            If errNo = 0 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If

        Else
            objHR_PermissionRequest.PermissionOption = PermissionType
            objHR_PermissionRequest.PermissionRequestId = PermissionRequestId
            objHR_PermissionRequest.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            errNo = objHR_PermissionRequest.Update()

            Dim fileName As String = PermissionRequestId.ToString()
            Dim fPath As String = String.Empty
            fPath = Server.MapPath("..\HR_PermissionRequestFiles\\" + fileName + FileExtension)
            If File.Exists(fPath) Then
                Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                If fuAttachFile.HasFile = False Then
                    extention = fileUploadExtension
                End If
                File.Delete(fPath)
                fPath = Server.MapPath("..\HR_PermissionRequestFiles\\" + fileName + extention)
                fuAttachFile.PostedFile.SaveAs(fPath)
            Else
                Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                If fuAttachFile.HasFile = False Then
                    extention = fileUploadExtension
                End If
                Dim fileNameExist As String = PermissionRequestId.ToString()
                Dim fPathExist As String = String.Empty
                fPathExist = Server.MapPath("..\HR_PermissionRequestFiles\\" + fileNameExist + extention)
                fuAttachFile.PostedFile.SaveAs(fPathExist)
            End If

            If errNo = 0 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
                'ScriptManager.RegisterStartupScript(Page, Page.GetType, Guid.NewGuid.ToString, "RefreshPage()", True)
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If
        End If

        If errNo = 0 Then
            FillGridView()
            ClearAll()
        End If

        Select Case DisplayMode.ToString()
            Case "Add"
                btnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)
            Case Else
                FillGridView()
                ClearAll()
        End Select

        HideShowControl()

    End Function

    Function updateOnly() As Integer
        Dim errNo As Integer = -1
        objHR_PermissionRequest = New HR_PermissionRequest()
        Dim tpFromTime As DateTime
        Dim tpToTime As DateTime
        Dim isFlixible As Boolean
        Dim strFlexibileDuration As String = (Val(rmtFlexibileTime.Text.Substring(0, 2)) * 60) + Val(rmtFlexibileTime.Text.Substring(2, 2))

        If ((PermissionType = PermissionOption.Nursing) Or (chckFullDay.Checked)) Then
            tpFromTime = CType(Nothing, DateTime?)
            tpToTime = CType(Nothing, DateTime?)
        Else
            tpFromTime = RadTPfromTime.SelectedDate
            tpToTime = RadTPtoTime.SelectedDate
        End If

        Dim days As String = String.Empty

        For i As Integer = 0 To chkWeekDays.Items.Count - 1
            If (chkWeekDays.Items(i).Selected) Then
                days &= chkWeekDays.Items(i).Value & ","
            End If
        Next

        If (rdlTimeOption.Items.FindByValue("0").Selected) Then
            isFlixible = False
        Else
            isFlixible = True
        End If

        Dim errorNum As Integer = -1
        objHR_PermissionRequest.FillObjectData(EmployeeFilterUC.EmployeeId, RadCmpPermissions.SelectedValue, chckIsDividable.Checked, isFlixible, radBtnSpecificDays.Checked, _
                                 txtRemarks.Text, dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate, radBtnOneDay.Checked, tpFromTime, _
                                 tpToTime, dtpPermissionDate.SelectedDate, PermissionDaysCount, PermissionType, OffAndHolidayDays, days, Integer.Parse(strFlexibileDuration), FileExtension, chckFullDay.Checked)
        If PermissionRequestId > 0 Then
            ' Update a data base record , on update mode
            objHR_PermissionRequest.PermissionRequestId = PermissionRequestId
            errNo = objHR_PermissionRequest.Update()

            If errorNum = 0 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If

        End If

        HideShowControl()

        Return errNo
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

    Private Function ValidatePermissionWorkingTime() As Boolean
        objEmpWorkSchedule = New Emp_WorkSchedule()
        objWorkSchedule = New WorkSchedule()

        Dim ScheduleDate As DateTime
        If radBtnOneDay.Checked Then
            ScheduleDate = dtpPermissionDate.SelectedDate
        Else
            ScheduleDate = dtpStartDatePerm.SelectedDate
        End If

        Dim intWorkScheduleType As Integer
        Dim intWorkScheduleId As Integer
        Dim dtEmpWorkSchedule As DataTable
        Dim found As Boolean = False
        Dim objApp_Settings As New APP_Settings
        Dim returnShiftResult As Boolean = False

        objWorkSchedule.EmployeeId = EmployeeFilterUC.EmployeeId
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

        If intWorkScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Normal) Then

            objWorkSchedule_NormalTime = New WorkSchedule_NormalTime()
            objWorkSchedule_NormalTime.FK_ScheduleId = intWorkScheduleId
            dtEmpWorkSchedule = objWorkSchedule_NormalTime.GetAll()


            Dim DayId As Integer

            If PermissionType = PermissionOption.Normal Then
                If radBtnOneDay.Checked Then
                    Dim strDay As String = dtpPermissionDate.SelectedDate.Value.DayOfWeek.ToString()
                    Select Case strDay
                        Case "Monday"
                            DayId = 2
                        Case "Tuesday"
                            DayId = 3
                        Case "Wednesday"
                            DayId = 4
                        Case "Thursday"
                            DayId = 5
                        Case "Friday"
                            DayId = 6
                        Case "Saturday"
                            DayId = 7
                        Case "Sunday"
                            DayId = 1
                    End Select

                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()

                    For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                        If Convert.ToInt32(row("DayId")) = DayId Then
                            found = True
                            Exit For
                        End If
                    Next

                    If found Then

                    Else
                        Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                        Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <> "00:00") AndAlso _
                            ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) <> "00:00") Then

                            If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                Return True
                            Else
                                Return False
                            End If
                        Else
                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                Return True
                            Else
                                Return False
                            End If
                        End If
                    End If
                ElseIf radBtnPeriod.Checked Then
                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                    Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
                    Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate
                    Dim returnPeriodValue As Boolean = False

                    While dteStartDate < dteEndDate
                        Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                        Select Case strDay
                            Case "Monday"
                                DayId = 2
                            Case "Tuesday"
                                DayId = 3
                            Case "Wednesday"
                                DayId = 4
                            Case "Thursday"
                                DayId = 5
                            Case "Friday"
                                DayId = 6
                            Case "Saturday"
                                DayId = 7
                            Case "Sunday"
                                DayId = 1
                        End Select

                        For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                            If Convert.ToInt32(row("DayId")) = DayId Then
                                found = True
                                Exit For
                            End If
                        Next

                        If found Then
                        Else
                            Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                            Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <> "00:00") AndAlso _
                                ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) <> "00:00") Then

                                If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                    ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                    returnPeriodValue = True
                                Else
                                    returnPeriodValue = False
                                End If
                            Else
                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                    returnPeriodValue = True
                                Else
                                    returnPeriodValue = False
                                End If
                            End If
                        End If
                        dteStartDate = dteStartDate.AddDays(1)
                    End While

                    Return returnPeriodValue
                Else
                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                    Dim returnSpecificDaysValue As Boolean = False
                    For Each item As ListItem In chkWeekDays.Items
                        If item.Selected Then
                            DayId = Convert.ToInt32(item.Value)
                            For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                                If Convert.ToInt32(row("DayId")) = DayId Then
                                    found = True
                                    Exit For
                                End If
                            Next

                            If found Then
                            Else
                                Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                                Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <> "00:00") AndAlso _
                                    ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) <> "00:00") Then

                                    If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                        ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                        returnSpecificDaysValue = True
                                    Else
                                        returnSpecificDaysValue = False
                                    End If
                                Else
                                    If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                        returnSpecificDaysValue = True
                                    Else
                                        returnSpecificDaysValue = False
                                    End If
                                End If
                            End If

                        End If
                    Next
                    Return returnSpecificDaysValue
                End If
            ElseIf PermissionType = PermissionOption.Nursing Then
                Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
                Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate
                Dim returnNursingValue As Boolean = False

                While dteStartDate < dteEndDate
                    Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                    Select Case strDay
                        Case "Monday"
                            DayId = 2
                        Case "Tuesday"
                            DayId = 3
                        Case "Wednesday"
                            DayId = 4
                        Case "Thursday"
                            DayId = 5
                        Case "Friday"
                            DayId = 6
                        Case "Saturday"
                            DayId = 7
                        Case "Sunday"
                            DayId = 1
                    End Select

                    For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                        If Convert.ToInt32(row("DayId")) = DayId Then
                            found = True
                            Exit For
                        End If
                    Next

                    If found Then
                    Else
                        Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                        Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <> "00:00") AndAlso _
                            ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) <> "00:00") Then

                            If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                returnNursingValue = True
                            Else
                                returnNursingValue = False
                            End If
                        Else
                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                returnNursingValue = True
                            Else
                                returnNursingValue = False
                            End If
                        End If
                    End If
                    dteStartDate = dteStartDate.AddDays(1)
                End While

                Return returnNursingValue
            ElseIf PermissionType = PermissionOption.Study Then
                Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                Dim returnStudyValue As Boolean = False
                For Each item As ListItem In chkWeekDays.Items
                    If item.Selected Then
                        DayId = Convert.ToInt32(item.Value)
                        For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                            If Convert.ToInt32(row("DayId")) = DayId Then
                                found = True
                                Exit For
                            End If
                        Next

                        If found Then
                        Else
                            Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                            Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <> "00:00") AndAlso _
                                ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) <> "00:00") Then

                                If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                    ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                    returnStudyValue = True
                                Else
                                    returnStudyValue = False
                                End If
                            Else
                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                    returnStudyValue = True
                                Else
                                    returnStudyValue = False
                                End If
                            End If
                        End If

                    End If
                Next

                Return returnStudyValue
            End If
        ElseIf intWorkScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Flexible) Then
            objWorkSchedule_Flexible = New WorkSchedule_Flexible()
            objWorkSchedule_Flexible.FK_ScheduleId = intWorkScheduleId
            dtEmpWorkSchedule = objWorkSchedule_Flexible.GetAll()

            Dim dtDuration1 As Integer = (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration1").ToString().Insert(2, ":")).Hour * 60) + _
                                            (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration1").ToString().Insert(2, ":")).Minute)
            Dim dtDuration2 As Integer = (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration2").ToString().Insert(2, ":")).Hour * 60) + _
                                            (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration2").ToString().Insert(2, ":")).Minute)

            Dim DayId As Integer

            If PermissionType = PermissionOption.Normal Then
                If radBtnOneDay.Checked Then
                    Dim strDay As String = dtpPermissionDate.SelectedDate.Value.DayOfWeek.ToString()
                    Dim dtEmpWorkScheduleOffDay As New DataTable
                    Select Case strDay
                        Case "Monday"
                            DayId = 2
                        Case "Tuesday"
                            DayId = 3
                        Case "Wednesday"
                            DayId = 4
                        Case "Thursday"
                            DayId = 5
                        Case "Friday"
                            DayId = 6
                        Case "Saturday"
                            DayId = 7
                        Case "Sunday"
                            DayId = 1
                    End Select

                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    If Not drEmpWorkScheduleOffDay.Length = 0 Then
                        dtEmpWorkScheduleOffDay = drEmpWorkScheduleOffDay.CopyToDataTable()
                    End If

                    For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                        If Convert.ToInt32(row("DayId")) = DayId Then
                            found = True
                            Exit For
                        End If
                    Next

                    If found Then
                    Else
                        Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                        Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                        If (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") AndAlso _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") Then

                            If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).AddMinutes(dtDuration2).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                Return True
                            Else
                                Return False
                            End If
                        Else
                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then
                                Return True
                            Else
                                Return False
                            End If
                        End If
                    End If
                ElseIf radBtnPeriod.Checked Then
                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                    Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
                    Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate
                    Dim returnPeriodFlexValue As Boolean = False

                    While dteStartDate < dteEndDate
                        Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                        Select Case strDay
                            Case "Monday"
                                DayId = 2
                            Case "Tuesday"
                                DayId = 3
                            Case "Wednesday"
                                DayId = 4
                            Case "Thursday"
                                DayId = 5
                            Case "Friday"
                                DayId = 6
                            Case "Saturday"
                                DayId = 7
                            Case "Sunday"
                                DayId = 1
                        End Select

                        For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                            If Convert.ToInt32(row("DayId")) = DayId Then
                                found = True
                                Exit For
                            End If
                        Next

                        If found Then
                        Else
                            Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                            Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                            If (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") AndAlso _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") Then

                                If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                    ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).AddMinutes(dtDuration2).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                    returnPeriodFlexValue = True
                                Else
                                    returnPeriodFlexValue = False
                                End If
                            Else
                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then
                                    returnPeriodFlexValue = True
                                Else
                                    returnPeriodFlexValue = False
                                End If
                            End If
                        End If

                        dteStartDate = dteStartDate.AddDays(1)
                    End While

                    Return returnPeriodFlexValue
                Else
                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                    Dim returnSpecificFlexValue As Boolean = False

                    For Each item As ListItem In chkWeekDays.Items
                        If item.Selected Then
                            DayId = Convert.ToInt32(item.Value)
                            For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                                If Convert.ToInt32(row("DayId")) = DayId Then
                                    found = True
                                    Exit For
                                End If
                            Next

                            If found Then
                            Else
                                Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                                Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                                If (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") AndAlso _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") Then

                                    If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                        ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).AddMinutes(dtDuration2).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                        returnSpecificFlexValue = True
                                    Else
                                        returnSpecificFlexValue = False
                                    End If
                                Else
                                    If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then
                                        returnSpecificFlexValue = True
                                    Else
                                        returnSpecificFlexValue = False
                                    End If
                                End If
                            End If

                        End If
                    Next

                    Return returnSpecificFlexValue
                End If
            ElseIf PermissionType = PermissionOption.Nursing Then
                Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
                Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate
                Dim returnFlexNursingValue As Boolean = False

                While dteStartDate < dteEndDate
                    Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                    Select Case strDay
                        Case "Monday"
                            DayId = 2
                        Case "Tuesday"
                            DayId = 3
                        Case "Wednesday"
                            DayId = 4
                        Case "Thursday"
                            DayId = 5
                        Case "Friday"
                            DayId = 6
                        Case "Saturday"
                            DayId = 7
                        Case "Sunday"
                            DayId = 1
                    End Select

                    For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                        If Convert.ToInt32(row("DayId")) = DayId Then
                            found = True
                            Exit For
                        End If
                    Next

                    If found Then
                    Else
                        Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                        Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                        If (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") AndAlso _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") Then

                            If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).AddMinutes(dtDuration2).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                returnFlexNursingValue = True
                            Else
                                returnFlexNursingValue = False
                            End If
                        Else
                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then
                                returnFlexNursingValue = True
                            Else
                                returnFlexNursingValue = False
                            End If
                        End If
                    End If
                    dteStartDate = dteStartDate.AddDays(1)
                End While

                Return returnFlexNursingValue
            ElseIf PermissionType = PermissionOption.Study Then
                Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                Dim returnStudyFlexValue As Boolean = False
                For Each item As ListItem In chkWeekDays.Items
                    If item.Selected Then
                        DayId = Convert.ToInt32(item.Value)
                        For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                            If Convert.ToInt32(row("DayId")) = DayId Then
                                found = True
                                Exit For
                            End If
                        Next

                        If found Then
                        Else
                            Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                            Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                            If (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") AndAlso _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") Then

                                If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                    ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).AddMinutes(dtDuration2).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                    returnStudyFlexValue = True
                                Else
                                    returnStudyFlexValue = False
                                End If
                            Else
                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then
                                    returnStudyFlexValue = True
                                Else
                                    returnStudyFlexValue = False
                                End If
                            End If
                        End If

                    End If
                Next

                Return returnStudyFlexValue
            End If
        ElseIf intWorkScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Advance) Then
            Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
            Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate
            Dim dtShiftSchedule As DataTable

            If (radBtnOneDay.Checked) AndAlso (PermissionType = PermissionOption.Normal) Then
                objEmp_Shifts = New Emp_Shifts
                objEmp_Shifts.FK_EmployeeId = EmployeeFilterUC.EmployeeId
                objEmp_Shifts.WorkDate = dtpPermissionDate.SelectedDate
                Dim AdvancedSchedule = objEmp_Shifts.GetShiftsByDate()

                If (AdvancedSchedule IsNot Nothing And AdvancedSchedule.Rows.Count > 0) Then
                    returnShiftResult = True
                    'Dim objWorkSchedule_Shifts As New WorkSchedule_Shifts
                    'objWorkSchedule_Shifts.FK_ScheduleId = intWorkScheduleId
                    'dtShiftSchedule = objWorkSchedule_Shifts.GetByFKScheduleID()

                    'If ((Convert.ToDateTime(dtShiftSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                    '    (Convert.ToDateTime(dtShiftSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) Or _
                    '    (Convert.ToDateTime(dtShiftSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                    '    (Convert.ToDateTime(dtShiftSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                    '    returnShiftResult = True
                    'Else
                    '    returnShiftResult = False
                    'End If

                Else
                    'FadiH:: check event when no shift
                    With objApp_Settings
                        .GetByPK()
                        If .NoShiftShcedule = Convert.ToInt32(CtlCommon.NoShiftSchedule.ConsideritOffDay) Then
                            returnShiftResult = True
                        ElseIf .NoShiftShcedule = Convert.ToInt32(CtlCommon.NoShiftSchedule.ConsiderDefaultSchedule) Then
                            returnShiftResult = CheckDefaultScheduleWorkingTime()
                        End If
                    End With
                End If

                Return returnShiftResult

            ElseIf (radBtnPeriod.Checked) Or (PermissionType = PermissionOption.Nursing) Then
                While dteStartDate < dteEndDate
                    objEmp_Shifts = New Emp_Shifts
                    objEmp_Shifts.FK_EmployeeId = EmployeeFilterUC.EmployeeId
                    objEmp_Shifts.WorkDate = dteStartDate
                    Dim AdvancedSchedule = objEmp_Shifts.GetShiftsByDate()

                    If (AdvancedSchedule IsNot Nothing And AdvancedSchedule.Rows.Count > 0) Then
                        Dim objWorkSchedule_Shifts As New WorkSchedule_Shifts
                        objWorkSchedule_Shifts.FK_ScheduleId = intWorkScheduleId
                        dtShiftSchedule = objWorkSchedule_Shifts.GetByFKScheduleID()

                        If Not PermissionType = PermissionOption.Nursing Then
                            If ((Convert.ToDateTime(dtShiftSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtShiftSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) Or _
                                (Convert.ToDateTime(dtShiftSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtShiftSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                returnShiftResult = True
                            Else
                                returnShiftResult = False
                            End If
                        Else
                            returnShiftResult = True
                        End If
                    Else
                        'FadiH:: check event when no shift
                        With objApp_Settings
                            .GetByPK()
                            If .NoShiftShcedule = Convert.ToInt32(CtlCommon.NoShiftSchedule.ConsideritOffDay) Then
                                returnShiftResult = True
                            ElseIf .NoShiftShcedule = Convert.ToInt32(CtlCommon.NoShiftSchedule.ConsiderDefaultSchedule) Then
                                returnShiftResult = CheckDefaultScheduleWorkingTime()
                            End If
                        End With
                    End If

                    dteStartDate = dteStartDate.AddDays(1)
                End While

                Return returnShiftResult
            ElseIf (radBtnSpecificDays.Checked) Or (PermissionType = PermissionOption.Study) Then
                While dteStartDate < dteEndDate
                    objEmp_Shifts = New Emp_Shifts
                    objEmp_Shifts.FK_EmployeeId = EmployeeFilterUC.EmployeeId
                    objEmp_Shifts.WorkDate = dteStartDate
                    Dim AdvancedSchedule = objEmp_Shifts.GetShiftsByDate()
                    Dim foundDay As Boolean = False
                    Dim DayId As Integer

                    Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                    Select Case strDay
                        Case "Monday"
                            DayId = 2
                        Case "Tuesday"
                            DayId = 3
                        Case "Wednesday"
                            DayId = 4
                        Case "Thursday"
                            DayId = 5
                        Case "Friday"
                            DayId = 6
                        Case "Saturday"
                            DayId = 7
                        Case "Sunday"
                            DayId = 1
                    End Select

                    For Each item As ListItem In chkWeekDays.Items
                        If item.Selected Then
                            If item.Value = DayId Then
                                foundDay = True
                                Exit For
                            End If
                        End If
                    Next

                    If foundDay Then
                        If (AdvancedSchedule IsNot Nothing And AdvancedSchedule.Rows.Count > 0) Then
                            Dim objWorkSchedule_Shifts As New WorkSchedule_Shifts
                            objWorkSchedule_Shifts.FK_ScheduleId = intWorkScheduleId
                            dtShiftSchedule = objWorkSchedule_Shifts.GetByFKScheduleID()

                            If ((Convert.ToDateTime(dtShiftSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtShiftSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) Or _
                                (Convert.ToDateTime(dtShiftSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtShiftSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                returnShiftResult = True
                            Else
                                returnShiftResult = False
                            End If

                        Else
                            'FadiH:: check event when no shift
                            With objApp_Settings
                                .GetByPK()
                                If .NoShiftShcedule = Convert.ToInt32(CtlCommon.NoShiftSchedule.ConsideritOffDay) Then
                                    returnShiftResult = True
                                ElseIf .NoShiftShcedule = Convert.ToInt32(CtlCommon.NoShiftSchedule.ConsiderDefaultSchedule) Then
                                    returnShiftResult = CheckDefaultScheduleWorkingTime()
                                End If
                            End With
                        End If
                    End If

                    dteStartDate = dteStartDate.AddDays(1)
                End While

                Return returnShiftResult
            End If

        End If

        Return True

    End Function

    Function CheckDefaultScheduleWorkingTime() As Boolean
        objEmpWorkSchedule = New Emp_WorkSchedule()
        Dim intWorkScheduleType As Integer
        Dim intWorkScheduleId As Integer
        Dim dtEmpWorkSchedule As DataTable
        Dim found As Boolean = False

        objWorkSchedule = New WorkSchedule()
        With objWorkSchedule
            .GetByDefault()
            intWorkScheduleType = .ScheduleType
            intWorkScheduleId = .ScheduleId
        End With

        If intWorkScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Normal) Then

            objWorkSchedule_NormalTime = New WorkSchedule_NormalTime()
            objWorkSchedule_NormalTime.FK_ScheduleId = intWorkScheduleId
            dtEmpWorkSchedule = objWorkSchedule_NormalTime.GetAll()


            Dim DayId As Integer

            If PermissionType = PermissionOption.Normal Then
                If radBtnOneDay.Checked Then
                    Dim strDay As String = dtpPermissionDate.SelectedDate.Value.DayOfWeek.ToString()
                    Select Case strDay
                        Case "Monday"
                            DayId = 2
                        Case "Tuesday"
                            DayId = 3
                        Case "Wednesday"
                            DayId = 4
                        Case "Thursday"
                            DayId = 5
                        Case "Friday"
                            DayId = 6
                        Case "Saturday"
                            DayId = 7
                        Case "Sunday"
                            DayId = 1
                    End Select

                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()

                    For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                        If Convert.ToInt32(row("DayId")) = DayId Then
                            found = True
                            Exit For
                        End If
                    Next

                    If found Then

                    Else
                        Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                        Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                            Return True
                        Else
                            Return False
                        End If
                    End If
                ElseIf radBtnPeriod.Checked Then
                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                    Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
                    Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate

                    While dteStartDate < dteEndDate
                        Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                        Select Case strDay
                            Case "Monday"
                                DayId = 2
                            Case "Tuesday"
                                DayId = 3
                            Case "Wednesday"
                                DayId = 4
                            Case "Thursday"
                                DayId = 5
                            Case "Friday"
                                DayId = 6
                            Case "Saturday"
                                DayId = 7
                            Case "Sunday"
                                DayId = 1
                        End Select

                        For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                            If Convert.ToInt32(row("DayId")) = DayId Then
                                found = True
                                Exit For
                            End If
                        Next

                        If found Then
                        Else
                            Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                            Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                Return True
                            Else
                                Return False
                            End If
                        End If
                        dteStartDate.AddDays(1)
                    End While
                Else
                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                    For Each item As ListItem In chkWeekDays.Items
                        If item.Selected Then
                            DayId = Convert.ToInt32(item.Value)
                            For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                                If Convert.ToInt32(row("DayId")) = DayId Then
                                    found = True
                                    Exit For
                                End If
                            Next

                            If found Then
                            Else
                                Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                                Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                    Return True
                                Else
                                    Return False
                                End If
                            End If

                        End If
                    Next
                End If
            ElseIf PermissionType = PermissionOption.Nursing Then
                Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
                Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate

                While dteStartDate < dteEndDate
                    Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                    Select Case strDay
                        Case "Monday"
                            DayId = 2
                        Case "Tuesday"
                            DayId = 3
                        Case "Wednesday"
                            DayId = 4
                        Case "Thursday"
                            DayId = 5
                        Case "Friday"
                            DayId = 6
                        Case "Saturday"
                            DayId = 7
                        Case "Sunday"
                            DayId = 1
                    End Select

                    For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                        If Convert.ToInt32(row("DayId")) = DayId Then
                            found = True
                            Exit For
                        End If
                    Next

                    If found Then
                    Else
                        Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                        Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                            Return True
                        Else
                            Return False
                        End If
                    End If
                    dteStartDate.AddDays(1)
                End While
            ElseIf PermissionType = PermissionOption.Study Then
                Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                For Each item As ListItem In chkWeekDays.Items
                    If item.Selected Then
                        DayId = Convert.ToInt32(item.Value)
                        For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                            If Convert.ToInt32(row("DayId")) = DayId Then
                                found = True
                                Exit For
                            End If
                        Next

                        If found Then
                        Else
                            Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                            Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                Return True
                            Else
                                Return False
                            End If
                        End If

                    End If
                Next
            End If
        ElseIf intWorkScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Flexible) Then
            objWorkSchedule_Flexible = New WorkSchedule_Flexible()
            objWorkSchedule_Flexible.FK_ScheduleId = intWorkScheduleId
            dtEmpWorkSchedule = objWorkSchedule_Flexible.GetAll()


            Dim DayId As Integer

            If PermissionType = PermissionOption.Normal Then
                If radBtnOneDay.Checked Then
                    Dim strDay As String = dtpPermissionDate.SelectedDate.Value.DayOfWeek.ToString()
                    Select Case strDay
                        Case "Monday"
                            DayId = 2
                        Case "Tuesday"
                            DayId = 3
                        Case "Wednesday"
                            DayId = 4
                        Case "Thursday"
                            DayId = 5
                        Case "Friday"
                            DayId = 6
                        Case "Saturday"
                            DayId = 7
                        Case "Sunday"
                            DayId = 1
                    End Select

                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()

                    For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                        If Convert.ToInt32(row("DayId")) = DayId Then
                            found = True
                            Exit For
                        End If
                    Next

                    If found Then
                    Else
                        Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                        Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()


                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                            Return True
                        Else
                            Return False
                        End If
                    End If
                ElseIf radBtnPeriod.Checked Then
                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                    Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
                    Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate

                    While dteStartDate < dteEndDate
                        Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                        Select Case strDay
                            Case "Monday"
                                DayId = 2
                            Case "Tuesday"
                                DayId = 3
                            Case "Wednesday"
                                DayId = 4
                            Case "Thursday"
                                DayId = 5
                            Case "Friday"
                                DayId = 6
                            Case "Saturday"
                                DayId = 7
                            Case "Sunday"
                                DayId = 1
                        End Select

                        For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                            If Convert.ToInt32(row("DayId")) = DayId Then
                                found = True
                                Exit For
                            End If
                        Next

                        If found Then
                        Else
                            Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                            Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                Return True
                            Else
                                Return False
                            End If
                        End If
                        dteStartDate.AddDays(1)
                    End While
                Else
                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                    For Each item As ListItem In chkWeekDays.Items
                        If item.Selected Then
                            DayId = Convert.ToInt32(item.Value)
                            For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                                If Convert.ToInt32(row("DayId")) = DayId Then
                                    found = True
                                    Exit For
                                End If
                            Next

                            If found Then
                            Else
                                Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                                Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                     (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                     (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                     (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                    Return True
                                Else
                                    Return False
                                End If
                            End If

                        End If
                    Next
                End If
            ElseIf PermissionType = PermissionOption.Nursing Then
                Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
                Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate

                While dteStartDate < dteEndDate
                    Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                    Select Case strDay
                        Case "Monday"
                            DayId = 2
                        Case "Tuesday"
                            DayId = 3
                        Case "Wednesday"
                            DayId = 4
                        Case "Thursday"
                            DayId = 5
                        Case "Friday"
                            DayId = 6
                        Case "Saturday"
                            DayId = 7
                        Case "Sunday"
                            DayId = 1
                    End Select

                    For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                        If Convert.ToInt32(row("DayId")) = DayId Then
                            found = True
                            Exit For
                        End If
                    Next

                    If found Then
                    Else
                        Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                        Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                            Return True
                        Else
                            Return False
                        End If
                    End If
                    dteStartDate.AddDays(1)
                End While
            ElseIf PermissionType = PermissionOption.Study Then
                Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                For Each item As ListItem In chkWeekDays.Items
                    If item.Selected Then
                        DayId = Convert.ToInt32(item.Value)
                        For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                            If Convert.ToInt32(row("DayId")) = DayId Then
                                found = True
                                Exit For
                            End If
                        Next

                        If found Then
                        Else
                            Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                            Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                Return True
                            Else
                                Return False
                            End If
                        End If

                    End If
                Next
            End If
        End If

        Return True
    End Function

#End Region

#Region "Methods"

    Private Function Is_FullDay() As Boolean

        Return chckFullDay.Checked

    End Function

    Private Sub DisableEnableTimeView(ByVal status As Boolean)
        If DisplayMode.ToString() = "ViewAll" Or _
            DisplayMode.ToString() = "View" Then
            RadTPfromTime.Enabled = False
            RadTPtoTime.Enabled = False
            ' Read only text box , no matter to enable or disable
            ' txtTimeDifference.Enabled = False
        Else
            ' Enable or disable time view
            RadTPfromTime.Enabled = status
            RadTPtoTime.Enabled = status
            reqFromtime.Enabled = status
            reqToTime.Enabled = status
            CustomValidator1.Enabled = status
            CustomValidator2.Enabled = status
            If status = True Then
                ' Do nothing
                'RadTPfromTime.TimeView.HeaderText = "Start Time"
                'RadTPtoTime.TimeView.HeaderText = "End Time"
            Else
            End If

        End If
    End Sub

    Private Sub HideShowControl()
        Dim browser As String = Request.Browser.Type
        Select Case PermissionType
            Case PermissionOption.Normal
                If (browser.Contains("Chrome") Or browser.Contains("InternetExplorer")) Then
                    'If SessionVariables.CultureInfo = "en-US" Then
                    '    tdOption.Style("margin-right") = "-455px"
                    'Else
                    '    tdOption.Style("margin-left") = "-310px"
                    'End If
                Else
                    If (browser.Contains("IE")) Then
                        'If SessionVariables.CultureInfo = "en-US" Then
                        '    tdOption.Style("margin-right") = "200px"
                        'Else
                        '    tdOption.Style("margin-left") = "300px"
                        'End If


                    Else
                        'If SessionVariables.CultureInfo = "en-US" Then
                        '    tdOption.Style("margin-right") = "-440px"
                        'Else
                        '    tdOption.Style("margin-left") = "-310px"
                        'End If
                    End If

                End If
                trWeekDays.Style("display") = "none"
                trType.Style("display") = "block"
                trNursingFlexibleDurationPermission.Visible = False
                'trDifTime.Style("width") = "135px"
                'If SessionVariables.CultureInfo = "en-US" Then
                '    tdWeekDays.Style("margin-right") = "-470px"
                '    tdWeekDays.Style("float") = "right"
                '    tdOption.Style("float") = "right"

                'Else
                '    tdWeekDays.Style("margin-left") = "-390px"
                '    tdWeekDays.Style("float") = "left"
                '    tdOption.Style("float") = "left"

                'End If

                ''FadiH::Edited Full Day from App_Settings
                objAPP_Settings = New APP_Settings()
                objAPP_Settings = objAPP_Settings.GetByPK()
                If objAPP_Settings.HasFullDayPermission Then
                    trFullyDay.Visible = True
                Else
                    trFullyDay.Visible = False
                End If
                ' trTime.Style("display") = "block"
            Case PermissionOption.Nursing
                If (browser.Contains("InternetExplorer")) Then
                    'If SessionVariables.CultureInfo = "en-US" Then
                    '    trDateFromTo.Style("padding-left") = "10px"
                    '    trAttachedFile.Style("padding-left") = "10px"
                    '    trRemarks.Style("padding-left") = "10px"
                    '    trNursingFlexibleDurationPermission.Style("padding-left") = "10px"
                    '    tdNursingFlexibleDurationPermission.Style("padding-left") = "10px"
                    'Else
                    '    trDateFromTo.Style("padding-right") = "10px"
                    '    trAttachedFile.Style("padding-right") = "10px"
                    '    trRemarks.Style("padding-right") = "10px"
                    '    trNursingFlexibleDurationPermission.Style("padding-right") = "10px"
                    '    tdNursingFlexibleDurationPermission.Style("padding-right") = "10px"
                    'End If
                    'tdDate.Style("width") = "125px"
                Else
                    If (browser.Contains("IE")) Then
                        'If SessionVariables.CultureInfo = "en-US" Then
                        '    trDateFromTo.Style("padding-left") = "10px"
                        '    trAttachedFile.Style("padding-left") = "10px"
                        '    trRemarks.Style("padding-left") = "10px"
                        '    trNursingFlexibleDurationPermission.Style("padding-left") = "10px"
                        '    tdNursingFlexibleDurationPermission.Style("padding-left") = "10px"
                        'Else
                        '    trDateFromTo.Style("padding-right") = "10px"
                        '    trAttachedFile.Style("padding-right") = "10px"
                        '    trRemarks.Style("padding-right") = "10px"
                        '    trNursingFlexibleDurationPermission.Style("padding-right") = "10px"
                        '    tdNursingFlexibleDurationPermission.Style("padding-right") = "10px"
                        'End If
                    Else
                        'If SessionVariables.CultureInfo = "en-US" Then
                        '    trDateFromTo.Style("padding-left") = "10px"
                        '    trAttachedFile.Style("padding-left") = "10px"
                        '    trRemarks.Style("padding-left") = "10px"
                        '    trNursingFlexibleDurationPermission.Style("padding-left") = "10px"
                        '    tdNursingFlexibleDurationPermission.Style("padding-left") = "10px"
                        'Else
                        '    trDateFromTo.Style("padding-right") = "10px"
                        '    trAttachedFile.Style("padding-right") = "10px"
                        '    trRemarks.Style("padding-right") = "10px"
                        '    trNursingFlexibleDurationPermission.Style("padding-right") = "10px"
                        '    tdNursingFlexibleDurationPermission.Style("padding-right") = "10px"
                        'End If
                    End If
                End If
                radBtnPeriod.Checked = True
                trWeekDays.Style("display") = "none"
                trTime.Style("display") = "none"
                trDifTime.Style("display") = "none"
                trPermType.Style("display") = "none"
                trType.Style("display") = "none"
                trNursingFlexibleDurationPermission.Visible = True
                'trDateFromTo.Style("padding-left") = "25px"


                'tdDate.Style("padding-left") = "10px"
                PnlOneDayLeave.Visible = False
                pnlPeriodLeave.Visible = True
                reqPermission.Enabled = False
                ExtenderPermission.Enabled = False
                userCtrlEmpPermHeader.HeaderText = ResourceManager.GetString("EmpNursePerm", CultureInfo)
                lblFromDate.Text = ResourceManager.GetString("BirthDate", CultureInfo)
                babyBirthDate = dtpStartDatePerm.SelectedDate

                ''FadiH::Edited Nursing Days from App_Settings alternate Web config
                objAPP_Settings = New APP_Settings()
                objAPP_Settings = objAPP_Settings.GetByPK()
                Dim nursingDay As Integer = objAPP_Settings.NursingDays
                babyBirthDate = babyBirthDate.AddDays(nursingDay)
                dtpEndDatePerm.SelectedDate = babyBirthDate
                hdnNurdingDay.Value = nursingDay

                trFullyDay.Visible = False
                reqFromtime.Enabled = False
                reqToTime.Enabled = False
                ExtenderFromTime.Enabled = False
                ExtenderreqToTime.Enabled = False

            Case PermissionOption.Study

                If (browser.Contains("Chrome")) Then

                    'tdDate.Style("width") = "135px"
                    'If SessionVariables.CultureInfo = "en-US" Then
                    '    tdWeekDays.Style("margin-right") = "-515px"
                    '    trTimeFromTo.Style("padding-left") = "20px"
                    '    trDif.Style("padding-left") = "20px"
                    '    trRemarks.Style("padding-left") = "20px"
                    '    trAttachedFile.Style("padding-left") = "20px"
                    '    trDateFromTo.Style("padding-left") = "20px"
                    'Else
                    '    tdWeekDays.Style("margin-left") = "-420px"
                    '    trTimeFromTo.Style("padding-right") = "20px"
                    '    trDif.Style("padding-right") = "20px"
                    '    trRemarks.Style("padding-right") = "20px"
                    '    trAttachedFile.Style("padding-right") = "20px"
                    '    trDateFromTo.Style("padding-right") = "20px"
                    '    tdWeekDays.Style("margin-right") = "-410px"
                    'End If
                Else
                    If (browser.Contains("InternetExplorer")) Then

                        'tdDate.Style("width") = "135px"
                        'tdDate.Style("padding-left") = "10px"

                        'If SessionVariables.CultureInfo = "en-US" Then
                        '    tdWeekDays.Style("margin-right") = "-535px"
                        '    trTimeFromTo.Style("padding-left") = "45px"
                        '    trDif.Style("padding-left") = "45px"
                        '    trRemarks.Style("padding-left") = "45px"
                        '    trAttachedFile.Style("padding-left") = "45px"
                        '    trDateFromTo.Style("padding-left") = "45px"
                        'Else
                        '    tdWeekDays.Style("margin-left") = "-445px"
                        '    trTimeFromTo.Style("padding-right") = "45px"
                        '    trDif.Style("padding-right") = "45px"
                        '    trRemarks.Style("padding-right") = "45px"
                        '    trAttachedFile.Style("padding-right") = "45px"
                        '    trDateFromTo.Style("padding-right") = "45px"
                        'End If
                    Else
                        If (browser.Contains("IE")) Then
                            'If SessionVariables.CultureInfo = "en-US" Then

                            '    tdWeekDays.Style("margin-right") = "-50px"
                            '    tdWeekDays.Style("margin-top") = "20px"
                            '    tdWeekDays.Style("width") = "100%"

                            '    trTimeFromTo.Style("padding-left") = "55px"
                            '    trDif.Style("padding-left") = "55px"
                            '    trRemarks.Style("padding-left") = "55px"
                            '    trAttachedFile.Style("padding-left") = "55px"
                            '    trDateFromTo.Style("padding-left") = "55px"
                            'Else
                            '    tdWeekDays.Style("margin-left") = "-50px"
                            '    tdWeekDays.Style("margin-top") = "15px"
                            '    tdWeekDays.Style("width") = "100%"

                            '    trTimeFromTo.Style("padding-right") = "60px"
                            '    trDif.Style("padding-right") = "62px"
                            '    trRemarks.Style("padding-right") = "62px"
                            '    trAttachedFile.Style("padding-right") = "62px"
                            '    trDateFromTo.Style("padding-right") = "60px"
                            'End If
                        Else
                            ''firefox



                            'If SessionVariables.CultureInfo = "en-US" Then
                            '    tdWeekDays.Style("margin-right") = "-490px"
                            '    trDateFromTo.Style("padding-left") = "12px"
                            '    trTimeFromTo.Style("padding-left") = "22px"
                            '    trDif.Style("padding-left") = "12px"
                            '    trRemarks.Style("padding-left") = "12px"
                            '    trAttachedFile.Style("padding-left") = "12px"
                            'Else
                            '    tdWeekDays.Style("margin-left") = "-410px"
                            '    trDateFromTo.Style("padding-right") = "12px"
                            '    trTimeFromTo.Style("padding-right") = "12px"
                            '    trDif.Style("padding-right") = "12px"
                            '    trRemarks.Style("padding-right") = "12px"
                            '    trAttachedFile.Style("padding-right") = "12px"
                            'End If
                        End If

                    End If


                End If

                radBtnSpecificDays.Checked = True
                trWeekDays.Style("display") = "block"
                'trWeekDays.Style("padding-left") = "0px"
                trPermType.Style("display") = "none"
                trType.Style("display") = "none"
                trNursingFlexibleDurationPermission.Visible = False


                'tdDate.Style("padding-left") = "20px"
                If SessionVariables.CultureInfo = "en-US" Then
                    'tdWeekDays.Style("margin-right") = "-550px"
                    'tdWeekDays.Style("float") = "right"
                Else
                    'tdWeekDays.Style("margin-left") = "-423px"
                    'tdWeekDays.Style("float") = "left"
                End If
                reqPermission.Enabled = False
                ExtenderPermission.Enabled = False
                trFullyDay.Visible = False
                'trTime.Style("display") = "block"
                PnlOneDayLeave.Visible = False
                pnlPeriodLeave.Visible = True
                userCtrlEmpPermHeader.HeaderText = ResourceManager.GetString("EmpStudyPerm", CultureInfo)
        End Select

    End Sub

    Protected Sub chckFullDay_CheckedChanged(ByVal sender As Object, _
                                             ByVal e As System.EventArgs) _
                                             Handles chckFullDay.CheckedChanged
        DisableEnableTimeView(Not chckFullDay.Checked)
        rdlTimeOption.Enabled = Not chckFullDay.Checked
        rdlTimeOption.SelectedValue = 0
        If chckFullDay.Checked Then
            trTime.Visible = False
            trDifTime.Visible = False
        Else
            trTime.Visible = True
            trDifTime.Visible = True
        End If

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
                    If c.Minutes < 0 Then
                        hours = hours - 1
                    End If
                    '''''''''''''''''''''''''
                    minutes = c.Minutes() * -1
                    If minutes <> 0 Then
                        'minutes = 60 - minutes
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
                     minutes & " دقائق"
                        txtTimeDifference.Style("text-align") = "right"
                    Else
                        txtTimeDifference.Text = hours & " hours," & _
                         minutes & " minutes"

                    End If
                End If
            End If
        Catch ex As Exception

            MsgBox(ex.Message)

        End Try
    End Function

    Private Sub setLocalizedTextField(ByVal comb As RadComboBox, _
                             ByVal EnName As String, ByVal ArName As String)
        comb.DataTextField = IIf(SessionVariables.CultureInfo = "en-US", _
                                                   EnName, ArName)
    End Sub

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

        objHR_PermissionRequest = New HR_PermissionRequest
        With objHR_PermissionRequest
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
                If RadTPtoTime.SelectedDate.Value > RadTPfromTime.SelectedDate.Value Then
                    remainingBalanceCount += (Convert.ToDateTime(RadTPtoTime.SelectedDate.Value).Subtract(Convert.ToDateTime(RadTPfromTime.SelectedDate.Value))).TotalMinutes
                Else
                    remainingBalanceCount += (Convert.ToDateTime(RadTPtoTime.SelectedDate.Value).Subtract(Convert.ToDateTime(RadTPfromTime.SelectedDate.Value))).TotalMinutes
                    remainingBalanceCount = remainingBalanceCount + 1440
                End If

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

    Private Sub FillObjectData()
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        ' Set data into object for Add / Update
        With objHR_PermissionRequest
            ' Get Values from the combo boxes
            .FK_EmployeeId = EmployeeFilterUC.EmployeeId
            .FK_PermId = RadCmpPermissions.SelectedValue
            ' Get values from the check boxes
            .IsDividable = chckIsDividable.Checked

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
            If PermissionRequestId = 0 Then
                If fuAttachFile.HasFile Then
                    .AttachedFile = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                Else
                    .AttachedFile = String.Empty
                End If

            Else
                .AttachedFile = FileExtension
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

            Dim days As String = String.Empty
            For i As Integer = 0 To chkWeekDays.Items.Count - 1
                If (chkWeekDays.Items(i).Selected) Then
                    days &= chkWeekDays.Items(i).Value & ","
                End If
            Next

            If Not radBtnOneDay.Checked Then
                If radBtnSpecificDays.Checked Then
                    .IsSpecificDays = True
                    .Days = days
                Else
                    .IsSpecificDays = False
                    .Days = PermissionDaysCount
                End If
                .IsForPeriod = True
                .PermDate = dtpStartDatePerm.SelectedDate
                .PermEndDate = dtpEndDatePerm.SelectedDate

            Else
                .IsForPeriod = False
                .PermDate = dtpPermissionDate.SelectedDate
                '.PermEndDate = dtpEndDatePerm.SelectedDate
                .PermEndDate = dtpPermissionDate.SelectedDate
                .Days = PermissionDaysCount
            End If

           

            'If IS_Period() Then
            '    ' Periodically leave
            '    .IsForPeriod = True

            '    .PermDate = dtpStartDatePerm.SelectedDate
            '    .PermEndDate = IIf(CheckDate(dtpEndDatePerm.SelectedDate) = Nothing, _
            '                       DateTime.MinValue, dtpEndDatePerm.SelectedDate)

            'Else
            '    ' Non Periodically leave
            '    .IsForPeriod = False

            '    .PermDate = dtpPermissionDate.SelectedDate
            '    .PermEndDate = Nothing   'DateTime.MinValue
            'End If
        End With
    End Sub

#End Region

#Region "Start & End date validation"

    Private Function StartEndDateComparison(ByVal dateStartdate As DateTime, _
                                           ByVal dateEndDate As DateTime) As Boolean
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If IsDate(dtpStartDatePerm.SelectedDate) And IsDate(dtpEndDatePerm.SelectedDate) Then
            ' Get the start and end date from the calenders
            dateStartdate = New DateTime(dtpStartDatePerm.SelectedDate.Value.Year, _
                                         dtpStartDatePerm.SelectedDate.Value.Month, _
                                         dtpStartDatePerm.SelectedDate.Value.Day)
            dateEndDate = New DateTime(dtpEndDatePerm.SelectedDate.Value.Year, _
                                       dtpEndDatePerm.SelectedDate.Value.Month, _
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

    Protected Sub dtpEndDatePerm_SelectedDateChanged(ByVal sender As Object, _
                                                ByVal e As SelectedDateChangedEventArgs) _
                                                Handles dtpEndDatePerm.SelectedDateChanged




        If IS_Period() And dtpEndDatePerm.SelectedDate() IsNot Nothing Then


            StartEndDateComparison(dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate)


        End If


    End Sub

#End Region

#Region "Handle Selected Time changed"

    Protected Sub RadTPtoTim_SelectedDateChanged(ByVal sender As Object, _
                                                         ByVal e As SelectedDateChangedEventArgs) _
                                                         Handles RadTPtoTime.SelectedDateChanged
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        'If (hdnIsvalid.Value) Then
        setTimeDifference()
        'Else
        'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TimeRange", CultureInfo))

        ' End If

    End Sub

    Protected Sub RadTPfromTime_SelectedDateChanged(ByVal sender As Object, _
                                                             ByVal e As SelectedDateChangedEventArgs) _
                                                     Handles RadTPfromTime.SelectedDateChanged
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        'If (hdnIsvalid.Value) Then
        setTimeDifference()
        ' Else
        'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TimeRange", CultureInfo))
        ' End If

    End Sub

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
        End If
    End Sub

#End Region

End Class
