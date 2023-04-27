Imports TA.Employees
Imports System.Data
Imports SmartV.UTILITIES
Imports Telerik.Web
Imports Telerik.Web.UI
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports System.Web
Imports Telerik.Web.UI.Calendar
Imports SmartV.UTILITIES.ProjectCommon
Imports TA.LookUp
Imports TA.Definitions
Imports System.IO
Imports TA.Security
Imports TA.DailyTasks
Imports TA.Admin
Imports TA.SelfServices

Partial Class Emp_userControls_EmpPermissions
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

    Private Lang As CtlCommon.Lang

    Private objEmployee As Employee
    Private objPermissionsTypes As PermissionsTypes
    Private objEmpPermissions As Emp_Permissions

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
    Private objEmployee_Manager As Employee_Manager
    Private objRecalculateRequest As RecalculateRequest
    Private objSemesters As Semesters
    Private objEmp_University As Emp_University

#End Region

#Region "Properties"

    Public Property PermissionId() As Integer
        Get
            Return ViewState("PersmissionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("PersmissionId") = value
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
        Set(ByVal value As Integer)
            ViewState("LeaveTypeDuration") = value
        End Set
    End Property

    Public Property IsManager() As Integer
        Get
            Return ViewState("IsManager")
        End Get
        Set(ByVal value As Integer)
            ViewState("IsManager") = value
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

    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Public Property ViolationDate() As DateTime
        Get
            Return ViewState("ViolationDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("ViolationDate") = value
        End Set
    End Property
    Public Property FromTime() As DateTime
        Get
            Return ViewState("FromTime")
        End Get
        Set(ByVal value As DateTime)
            ViewState("FromTime") = value
        End Set
    End Property
    Public Property ToTime() As DateTime
        Get
            Return ViewState("ToTime")
        End Get
        Set(ByVal value As DateTime)
            ViewState("ToTime") = value
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

    Public Property IsRequest() As Integer
        Get
            Return ViewState("IsRequest")
        End Get
        Set(ByVal value As Integer)
            ViewState("IsRequest") = value
        End Set
    End Property

    Public Property DeletePermDate() As DateTime
        Get
            Return ViewState("DeletePermDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("DeletePermDate") = value
        End Set
    End Property

    Public Property DeletePermEndDate() As DateTime
        Get
            Return ViewState("DeletePermEndDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("DeletePermEndDate") = value
        End Set
    End Property

#End Region

#Region "Methods & Events"

    Public Sub FillDataHREmployeeViolations()
        objEmployee = New Employee


        If EmployeeId <> 0 Then
            ''fill Employee Filter
            EmployeeFilterUC.EmployeeId = EmployeeId
            EmployeeFilterUC.IsEntityClick = "True"
            EmployeeFilterUC.GetEmployeeInfo(EmployeeId)
            EmployeeFilterUC.EmployeeId = EmployeeId
            EmployeeFilterUC.EnabledDisbaledControls(False)
            tdOption.Visible = False
            trFullyDay.Visible = False
            rdlTimeOption.Enabled = False

            dtpPermissionDate.Enabled = False
            RadTPfromTime.Enabled = False
            RadTPtoTime.Enabled = False
            dtpPermissionDate.SelectedDate = ViolationDate
            RadTPfromTime.SelectedDate = Now.Date + FromTime.TimeOfDay
            RadTPtoTime.SelectedDate = Now.Date + ToTime.TimeOfDay()
            setTimeDifference()
            ''''


            ''fill grid 

            objEmpPermissions = New Emp_Permissions()
            objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
            objEmpPermissions.PermissionOption = PermissionType
            objEmpPermissions.PermDate = dtpFromDateSearch.SelectedDate
            objEmpPermissions.PermEndDate = dtpToDateSearch.SelectedDate
            dtCurrent = objEmpPermissions.GetAllInnerJoin()
            Dim dv As New DataView(dtCurrent)
            dv.Sort = SortExepression
            dgrdVwEmpPermissions.DataSource = dv
            dgrdVwEmpPermissions.DataBind()
            ''''

            ''hide clear & delete button
            btnClear.Visible = False
            btnDelete.Visible = False
            ''''

            IsViolationsCorrection = True

        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        ElseIf SessionVariables.CultureInfo = "en-US" Then
            Lang = CtlCommon.Lang.EN
        Else
            Lang = CtlCommon.Lang.AR
        End If

        If Page.IsPostBack <> True Then
            EmployeeFilterUC.IsManager = IsManager
            EmployeeFilterUC.FK_ManagerId = FK_ManagerId

            ValidationGroup = btnSave.ValidationGroup
            EmployeeFilterUC.IsEmployeeRequired = True
            EmployeeFilterUC.CompanyRequiredValidationGroup = btnSave.ValidationGroup
            EmployeeFilterUC.EmployeeRequiredValidationGroup = btnSave.ValidationGroup

            SetRadDateTimePickerPeoperties()
            ManageDisplayMode_ExceptionalCases()
            FillGridView()
            rmtFlexibileTime.TextWithLiterals = "0000"


            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

            userCtrlEmpPermHeader.HeaderText = ResourceManager.GetString("EmpPerm", CultureInfo)
            reqPermission.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            FillLists()
            btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" +
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
            FillDataHREmployeeViolations()

            ArcivingMonths_DateValidation()

            If PermissionType = PermissionOption.Study Then
                objAPP_Settings = New APP_Settings
                objAPP_Settings.GetByPK()
                If objAPP_Settings.EnableSemesterSelection_StudyPermission = True Then
                    dvSemesterSelection.Visible = True
                    dvSemesterText.Visible = False
                    FillSemesters()
                Else
                    dvSemesterSelection.Visible = False
                    dvSemesterText.Visible = True
                End If

                If objAPP_Settings.EnableUniversitySelection_StudyPermission = True Then
                    dvUniversity.Visible = True
                    FillUnversities()
                Else
                    dvUniversity.Visible = False
                End If

            End If
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

        If ((PermissionType = PermissionOption.Normal) And radBtnSpecificDays.Checked = True) Or (PermissionType = PermissionOption.Study) Then
            dtpStartDatePerm.AutoPostBack = True
            dtpStartDatePerm.ClientEvents.OnDateSelected = ""
        Else
            dtpStartDatePerm.AutoPostBack = False
            dtpStartDatePerm.ClientEvents.OnDateSelected = "DateSelected"
        End If

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

        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        If Not objAPP_Settings.HasFlexibleNursingPermission Then
            If rlstAllowed.Items.Count > 2 Then
                rlstAllowed.Items.RemoveAt(2)
            End If

        End If
    End Sub

    Private Sub ManageDisplayMode_ExceptionalCases()
        '  Current function handle the cases where the displayMode 
        '  associated with the in appropriate value for 
        '  the PermissionId
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim _Exist As Integer = 0
        _Exist = IS_Exist()
        If DisplayMode = DisplayModeEnum.View Or
            DisplayMode = DisplayModeEnum.Edit Then
            If _Exist = -1 Then
                ' If the display mode is View or edit.But, the 
                ' Id is not a valid one
                CtlCommon.ShowMessage(
                    Me.Page,
                    ResourceManager.GetString("ErrorPermitProcessing", CultureInfo), "error")
                DisplayMode = DisplayModeEnum.ViewAll
            End If
        ElseIf DisplayMode = DisplayModeEnum.ViewAddEdit Or
            DisplayModeEnum.ViewAll = DisplayMode Then
            PermissionId = 0
        ElseIf DisplayMode() = DisplayModeEnum.Add Then
            If PermissionId > 0 Then
                If _Exist = 0 Then
                    DisplayMode = DisplayModeEnum.Edit
                Else
                    ' PermissionId>0 and does not matched a data base record
                    PermissionId = 0
                End If
            Else
                ' if PermissionId=0 or PermissionId<0
                PermissionId = 0
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
        objEmpPermissions = New Emp_Permissions()
        objEmpPermissions.PermissionId = PermissionId
        Dim _EXIT As Integer = 0
        If PermissionId <= 0 Then
            _EXIT = -1
        ElseIf objEmpPermissions.Find_Existing() = False Then
            _EXIT = -1
        End If
        Return _EXIT
    End Function

    Public Sub FillGridView()
        Try
            objEmpPermissions = New Emp_Permissions()
            objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
            objEmpPermissions.PermissionOption = PermissionType
            objEmpPermissions.PermDate = dtpFromDateSearch.SelectedDate
            objEmpPermissions.PermEndDate = dtpToDateSearch.SelectedDate
            dtCurrent = objEmpPermissions.GetAllInnerJoin()
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
        RadCmpPermissions.SelectedValue = -1
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


        If PermissionType = PermissionOption.Nursing Then
            trNursingFlexibleDurationPermission.Visible = True
            RadCmbFlixebleDuration.ClearSelection()
        End If
        If PermissionType = PermissionOption.Normal Then
            DisableEnableTimeView(Not chckFullDay.Checked)
            rdlTimeOption.Enabled = Not chckFullDay.Checked
            rlstAllowed.ClearSelection()
        End If
        dtpFromDateSearch.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
        Dim dd As New Date
        dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

        dtpToDateSearch.SelectedDate = dd

        txtStudyYear.Text = String.Empty
        txtSemester.Text = String.Empty
        radcmbxSemester.SelectedValue = -1
        If PermissionType = PermissionOption.Study Then
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            If objAPP_Settings.EnableSemesterSelection_StudyPermission = True Then
                dvSemesterSelection.Visible = True
                dvSemesterText.Visible = False
            Else
                dvSemesterSelection.Visible = False
                dvSemesterText.Visible = True
            End If

            If objAPP_Settings.EnableUniversitySelection_StudyPermission = True Then
                dvUniversity.Visible = True
                radcmbxUnversity.SelectedValue = -1
            Else
                dvUniversity.Visible = False
            End If

            rblEmp_GPAType.SelectedValue = 1
            txtEmp_GPA.Text = String.Empty

        End If

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

        objEmpPermissions = New Emp_Permissions()

        objEmpPermissions.PermissionId = PermissionId

        ' If the PermissionId is not a valid one , the below line will through 
        ' an exception
        If IsRequest = 1 Then
            Return
        Else
            objEmpPermissions.GetByPK()
        End If
        With objEmpPermissions
            ' Fill combo boxes
            'RadCmbEmployee.SelectedValue = .FK_EmployeeId
            EmployeeFilterUC.EmployeeId = .FK_EmployeeId
            RadCmpPermissions.SelectedValue = .FK_PermId
            ' Fill checkBoxes
            chckIsDividable.Checked = .IsDividable
            FileExtension = .AttachedFile

            'Dim FilePath As String = Server.MapPath("..\..\PermissionFiles\" + PermissionId.ToString() + .AttachedFile)

            'fuAttachFile.Visible = True
            'Dim FilePath As String = Server.MapPath("~/PermissionFiles")
            'FilePath = FilePath + "\" + PermissionId.ToString() + .AttachedFile
            'If File.Exists(FilePath) Then
            '    lnbLeaveFile.HRef = "..\..\PermissionFiles\" + PermissionId.ToString() + .AttachedFile
            '    lnbLeaveFile.Visible = True
            '    lnbRemove.Visible = True
            'End If

            'If String.IsNullOrEmpty(.AttachedFile) Then
            '    lnbLeaveFile.Visible = False
            '    lnbRemove.Visible = False
            '    lblNoAttachedFile.Visible = True
            'ElseIf File.Exists(FilePath) Then
            '    lnbLeaveFile.Visible = True
            '    lnbRemove.Visible = True
            '    lblNoAttachedFile.Visible = False
            'Else
            '    lnbLeaveFile.Visible = False
            '    lnbRemove.Visible = False
            '    lblNoAttachedFile.Visible = True
            'End If

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
                divAllowedTime.Visible = False
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
                divAllowedTime.Visible = True
                RadCmbFlixebleDuration.SelectedValue = .FlexibilePermissionDuration
                rlstAllowed.SelectedValue = .AllowedTime
            End If

            txtRemarks.Text = .Remark


            If (radBtnSpecificDays.Checked AndAlso PermissionType = PermissionOption.Normal) Or PermissionType = PermissionOption.Study Then
                chkWeekDays.ClearSelection()
                For Each item As ListItem In chkWeekDays.Items
                    item.Enabled = False
                Next

                Dim dtpStartDate As DateTime = .PermDate
                Dim dtpEndDate As DateTime = .PermEndDate

                Dim days As Integer = dtpEndDate.Subtract(dtpStartDate).Days + 1

                'if days less than week days to show only days selected
                'If days < 7 Then
                '    While dtpStartDate <= dtpEndDate
                '        Dim strDay As String = dtpStartDate.DayOfWeek.ToString()
                '        Select Case strDay
                '            Case "Monday"
                '                chkWeekDays.Items.FindByValue(2).Enabled = True
                '            Case "Tuesday"
                '                chkWeekDays.Items.FindByValue(3).Enabled = True
                '            Case "Wednesday"
                '                chkWeekDays.Items.FindByValue(4).Enabled = True
                '            Case "Thursday"
                '                chkWeekDays.Items.FindByValue(5).Enabled = True
                '            Case "Friday"
                '                chkWeekDays.Items.FindByValue(6).Enabled = True
                '            Case "Saturday"
                '                chkWeekDays.Items.FindByValue(7).Enabled = True
                '            Case "Sunday"
                '                chkWeekDays.Items.FindByValue(1).Enabled = True
                '        End Select
                '        dtpStartDate = dtpStartDate.AddDays(1)
                '    End While

                '    For Each item As ListItem In chkWeekDays.Items
                '        If item.Enabled = False Then
                '            item.Selected = False
                '        End If
                '    Next
                'Else
                '    For Each item As ListItem In chkWeekDays.Items
                '        item.Enabled = True
                '    Next
                'End If

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
            'If .StudyYear = 0 Then
            '    txtStudyYear.Text = String.Empty
            'Else
            '    txtStudyYear.Text = .StudyYear
            'End If


            'objAPP_Settings = New APP_Settings
            'objAPP_Settings.GetByPK()
            'If objAPP_Settings.EnableSemesterSelection_StudyPermission = True Then
            '    dvSemesterSelection.Visible = True
            '    dvSemesterText.Visible = False

            '    Dim item As RadComboBoxItem = radcmbxSemester.FindItemByText(.Semester)
            '    If Not item Is Nothing Then
            '        item.Selected = True
            '    End If
            'Else
            '    dvSemesterSelection.Visible = False
            '    dvSemesterText.Visible = True
            '    txtSemester.Text = .Semester
            'End If

            'If objAPP_Settings.EnableUniversitySelection_StudyPermission = True Then
            '    dvUniversity.Visible = True
            '    radcmbxUnversity.SelectedValue = .FK_UniversityId
            'Else
            '    dvUniversity.Visible = False
            'End If

            'rblEmp_GPAType.SelectedValue = .Emp_GPAType
            'txtEmp_GPA.Text = .Emp_GPA

        End With
    End Sub

    Private Sub ManageSomeControlsStatus(ByVal IsForPeriod As Boolean,
                                         ByVal PermDate As DateTime,
                                         ByVal PermEndDate As DateTime?,
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
            dtpStartDatePerm.SelectedDate = PermDate
            dtpEndDatePerm.SelectedDate =
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
            myDate =
                IIf(myDate > DateTime.MinValue, myDate, Nothing)
            Return (myDate)
        Else
            Return Nothing
        End If
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim found As Boolean = False
        objAPP_Settings = New APP_Settings
        'If hdnIsvalid.Value <> String.Empty Then
        '    If (hdnIsvalid.Value) Then

        '    Else
        '        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TimeRange", CultureInfo))
        '        Return
        '    End If
        'End If

        If (PermissionType = PermissionOption.Nursing) Then
            objEmployee = New Employee()
            'objEmployee = EmployeeFilterUC.GetEmployeeAllInfo()
            If (objEmployee.Gender = "m") Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NursingValidation", CultureInfo), "info")
                Return
            End If
        End If
        'For Check if Attachment is Manadetory
        If Not PermissionType = PermissionOption.Normal Then

            objEmpPermissions = New Emp_Permissions()
            objEmpPermissions.PermissionId = PermissionId
            If PermissionId > 0 Then
                objEmpPermissions.GetByPK()
            End If
            Dim FilePath As String = Server.MapPath("~/PermissionFiles")

            FilePath = FilePath + "\" + PermissionId.ToString() + objEmpPermissions.AttachedFile
            If Not File.Exists(FilePath) Then
                If fuAttachFile.HasFile = False Then
                    objAPP_Settings.GetByPK()
                    If objAPP_Settings.AttachmentIsMandatory = True Then
                        If Lang = CtlCommon.Lang.AR Then
                            CtlCommon.ShowMessage(Me.Page, "يرجى ارفاق ملف!", "info")
                        Else
                            CtlCommon.ShowMessage(Me.Page, "Please Attach File!", "info")
                        End If
                        Exit Sub
                    End If
                End If
            End If
        End If
        ''''''
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

    Protected Sub btnDelete_Click(ByVal sender As Object,
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
                Dim intPermissionId As Integer =
                Convert.ToInt32(row.GetDataKeyValue("PermissionId"))

                If ((Not row.GetDataKeyValue("FK_PermId").ToString() = "") And (Not String.IsNullOrEmpty(row.GetDataKeyValue("FK_PermId").ToString()))) Then
                    intPermTypeId =
                    Convert.ToInt32(row.GetDataKeyValue("FK_PermId"))
                End If
                objEmpPermissions = New Emp_Permissions
                With objEmpPermissions
                    .FK_EmployeeId = EmployeeId
                    .PermissionId = intPermissionId
                    IsRequest = .IsRequest
                End With
                'Get Leave Id
                objPermissionsTypes = New PermissionsTypes
                objPermissionsTypes.PermId = intPermTypeId
                objPermissionsTypes.GetByPK()
                LeaveID = objPermissionsTypes.FK_LeaveIdDeductBalance


                ' Delete current checked item
                objEmpPermissions = New Emp_Permissions()

                objEmpPermissions.PermissionId = intPermissionId

                If IsRequest = 0 Then
                    objEmpPermissions.GetByPK()

                    DeletePermDate = IIf(objEmpPermissions.PermDate = DateTime.MinValue, Convert.ToDateTime(row.GetDataKeyValue("PermDate")), objEmpPermissions.PermDate)
                    If Not objEmpPermissions.PermEndDate = Date.MinValue Or Not objEmpPermissions.PermEndDate Is Nothing Then
                        DeletePermEndDate = objEmpPermissions.PermEndDate
                    Else
                        DeletePermEndDate = objEmpPermissions.PermDate
                    End If

                End If

                If Not (objEmpPermissions.BalanceDays).ToString = "&nbsp;" Or Not (objEmpPermissions.BalanceDays).ToString = "0.0" Then
                    intPermDays = objEmpPermissions.BalanceDays
                End If
                If IsRequest = 0 Or (IsRequest = 1 And Not objEmpPermissions.PermissionId = Nothing) Then
                    errNum = objEmpPermissions.Delete()
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldNotDeletePermissionRequest", CultureInfo), "info")
                    Exit Sub
                End If


                If (errNum = 0) Then
                    If (LeaveID <> 0) Then
                        'Delete from balance
                        objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
                        objEmp_Leaves_BalanceHistory.FK_EmployeeId = EmployeeId
                        objEmp_Leaves_BalanceHistory.FK_LeaveId = LeaveID
                        objEmp_Leaves_BalanceHistory.FK_EmpPermId = intPermissionId


                        objEmp_Leaves_BalanceHistory.GetLastBalance()

                        objEmp_Leaves_BalanceHistory.BalanceDate = Date.Now
                        objEmp_Leaves_BalanceHistory.Balance = intPermDays
                        objEmp_Leaves_BalanceHistory.TotalBalance = objEmp_Leaves_BalanceHistory.TotalBalance + intPermDays
                        objEmp_Leaves_BalanceHistory.Remarks = "Delete Permission Data - Rollback Balance"
                        objEmp_Leaves_BalanceHistory.CREATED_DATE = Date.Now
                        objEmp_Leaves_BalanceHistory.CREATED_BY = SessionVariables.LoginUser.ID
                        objEmp_Leaves_BalanceHistory.AddBalance()
                    End If

                    Dim isForPeriod As Boolean = Convert.ToBoolean(row.GetDataKeyValue("IsForPeriod"))
                    objAPP_Settings = New APP_Settings()
                    objAPP_Settings = objAPP_Settings.GetByPK()

                    '----------------------Validate Employee Schedule------------'
                    objWorkSchedule = New WorkSchedule()
                    Dim intWorkScheduleType As Integer
                    Dim intWorkScheduleId As Integer

                    Dim ScheduleDate As DateTime
                    If radBtnOneDay.Checked Then
                        ScheduleDate = Convert.ToDateTime(row.GetDataKeyValue("PermDate"))
                    Else
                        ScheduleDate = Convert.ToDateTime(row.GetDataKeyValue("PermDate"))
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
                    objEmpPermissions = New Emp_Permissions
                    Dim dt As DataTable
                    Dim dt2 As DataTable
                    Dim PreviousPermDate As DateTime
                    Dim NextRestDate As DateTime
                    '----------------------Validate Employee Schedule------------'

                    If objAPP_Settings.ApprovalRecalMethod = 1 Then
                        If Not isForPeriod AndAlso PermissionType = PermissionOption.Normal Then
                            If intWorkScheduleType = 3 Then

                                objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
                                objEmpPermissions.PermDate = DeletePermDate
                                dt = objEmpPermissions.Get_Previous_Restday_Date()
                                If dt.Rows.Count > 0 Then
                                    PreviousPermDate = dt.Rows(0)("RestDate")
                                    objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
                                    objEmpPermissions.PreviousPermDate = PreviousPermDate
                                    dt2 = objEmpPermissions.Get_Next_Restday_Date()
                                    NextRestDate = dt2.Rows(0)("NextRestDate")
                                End If

                                If dt.Rows.Count > 0 Then
                                    temp_date = PreviousPermDate
                                    temp_str_date = DateToString(NextRestDate)
                                Else
                                    temp_date = Convert.ToDateTime(row.GetDataKeyValue("PermDate"))
                                    temp_str_date = DateToString(temp_date)
                                End If
                            Else
                                temp_date = Convert.ToDateTime(row.GetDataKeyValue("PermDate"))
                                temp_str_date = DateToString(temp_date)
                            End If
                            objRECALC_REQUEST.EMP_NO = EmployeeFilterUC.EmployeeId
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

                            Dim dteFrom As DateTime
                            Dim dteTo As DateTime
                            If intWorkScheduleType = 3 Then

                                objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
                                objEmpPermissions.PermDate = DeletePermDate
                                dt = objEmpPermissions.Get_Previous_Restday_Date()

                                If dt.Rows.Count > 0 Then
                                    PreviousPermDate = dt.Rows(0)("RestDate")
                                    objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
                                    objEmpPermissions.PreviousPermDate = PreviousPermDate
                                    dt2 = objEmpPermissions.Get_Next_Restday_Date()
                                    NextRestDate = dt2.Rows(0)("NextRestDate")
                                End If

                                If dt.Rows.Count > 0 Then
                                    dteFrom = PreviousPermDate
                                    dteTo = NextRestDate
                                Else
                                    dteFrom = Convert.ToDateTime(row.GetDataKeyValue("PermDate"))
                                    dteTo = Convert.ToDateTime(row.GetDataKeyValue("PermEndDate"))
                                End If
                            Else
                                dteFrom = Convert.ToDateTime(row.GetDataKeyValue("PermDate"))
                                If IsDBNull(row.GetDataKeyValue("PermEndDate")) = True Then
                                    dteTo = Convert.ToDateTime(row.GetDataKeyValue("PermDate"))
                                Else
                                    dteTo = Convert.ToDateTime(row.GetDataKeyValue("PermEndDate"))
                                End If
                            End If

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

                    Else
                        If Not isForPeriod AndAlso PermissionType = PermissionOption.Normal Then

                            If intWorkScheduleType = 3 Then

                                objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
                                objEmpPermissions.PermDate = DeletePermDate
                                dt = objEmpPermissions.Get_Previous_Restday_Date()

                                If dt.Rows.Count > 0 Then
                                    PreviousPermDate = dt.Rows(0)("RestDate")
                                    objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
                                    objEmpPermissions.PreviousPermDate = PreviousPermDate
                                    dt2 = objEmpPermissions.Get_Next_Restday_Date()
                                    NextRestDate = dt2.Rows(0)("NextRestDate")
                                    If NextRestDate > Date.Today Then
                                        NextRestDate = Date.Today
                                    End If
                                End If

                                If dt.Rows.Count > 0 Then
                                    temp_date = PreviousPermDate
                                    temp_str_date = DateToString(NextRestDate)
                                Else
                                    temp_date = Convert.ToDateTime(row.GetDataKeyValue("PermDate"))
                                    temp_str_date = DateToString(temp_date)
                                End If
                            Else
                                temp_date = Convert.ToDateTime(row.GetDataKeyValue("PermDate"))
                                temp_str_date = DateToString(temp_date)
                            End If

                            objRECALC_REQUEST.EMP_NO = EmployeeFilterUC.EmployeeId
                            objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)

                            objRecalculateRequest = New RecalculateRequest
                            With objRecalculateRequest
                                .Fk_EmployeeId = EmployeeFilterUC.EmployeeId
                                .FromDate = temp_date
                                If intWorkScheduleType = 3 Then
                                    .ToDate = NextRestDate
                                Else
                                    .ToDate = temp_date
                                End If

                                .ImmediatelyStart = True
                                .RecalStatus = 0
                                .CREATED_BY = SessionVariables.LoginUser.ID
                                .Remarks = "Delete Employee Permission - SYSTEM"
                                err2 = .Add
                            End With

                        Else

                            Dim dteFrom As DateTime
                            Dim dteTo As DateTime
                            If intWorkScheduleType = 3 Then

                                objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
                                objEmpPermissions.PermDate = DeletePermDate
                                dt = objEmpPermissions.Get_Previous_Restday_Date()

                                If dt.Rows.Count > 0 Then
                                    PreviousPermDate = dt.Rows(0)("RestDate")
                                    objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
                                    objEmpPermissions.PreviousPermDate = PreviousPermDate
                                    dt2 = objEmpPermissions.Get_Next_Restday_Date()
                                    NextRestDate = dt2.Rows(0)("NextRestDate")
                                    If NextRestDate > Date.Today Then
                                        NextRestDate = Date.Today
                                    End If
                                End If

                                If dt.Rows.Count > 0 Then
                                    dteFrom = PreviousPermDate
                                    dteTo = NextRestDate
                                Else
                                    dteFrom = Convert.ToDateTime(row.GetDataKeyValue("PermDate"))
                                    dteTo = Convert.ToDateTime(row.GetDataKeyValue("PermEndDate"))
                                End If
                            Else
                                dteFrom = Convert.ToDateTime(row.GetDataKeyValue("PermDate"))
                                dteTo = Convert.ToDateTime(row.GetDataKeyValue("PermEndDate"))
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
                                        .Remarks = "Delete Nursing Permission - SYSTEM"
                                    ElseIf PermissionType = PermissionOption.Study Then
                                        .Remarks = "Delete Study Permission - SYSTEM"
                                    Else
                                        .Remarks = "Delete Employee Permission - SYSTEM"
                                    End If

                                    err2 = .Add
                                End With
                            End If
                        End If

                    End If
                End If
            End If

        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            ClearAll()
            'EmployeeFilterUC.ClearValues()
            FillGridView()
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")

        End If
        ClearAll()
        'EmployeeFilterUC.ClearValues()
        'dgrdVwEmpPermissions.DataSource = Nothing
        'dgrdVwEmpPermissions.DataBind()
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object,
                                 ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
        Dim dtClear As New DataTable
        dgrdVwEmpPermissions.DataSource = dtClear
        dgrdVwEmpPermissions.DataBind()
        EmployeeFilterUC.ClearValues()

    End Sub

    'Protected Sub lnkEmployeeName_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    ' Get the PK from the hidden field
    '    PermissionId = Convert.ToInt32(DirectCast(dgrdVwEmpPermissions.SelectedItems(0), GridDataItem)("PermissionId").Text)
    '    FillControls()
    'End Sub

    Protected Sub dgrdVwEmpPermissions_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdVwEmpPermissions.SelectedIndexChanged
        dtpPermissionDate.Enabled = False
        lnbLeaveFile.Visible = False
        lnbLeaveFile.HRef = String.Empty

        PermissionId = Convert.ToInt32(DirectCast(dgrdVwEmpPermissions.SelectedItems(0), GridDataItem).GetDataKeyValue("PermissionId"))
        If (Not String.IsNullOrEmpty(DirectCast(dgrdVwEmpPermissions.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_LeaveId").ToString())) And (Not (DirectCast(dgrdVwEmpPermissions.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_LeaveId").ToString()) = "") Then
            LeaveID = Convert.ToInt32(DirectCast(dgrdVwEmpPermissions.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_LeaveId"))
        End If
        'IsRequest = Convert.ToInt32(DirectCast(dgrdVwEmpPermissions.SelectedItems(0), GridDataItem).GetDataKeyValue("IsRequest"))
        Dim FK_EmployeeId As Integer
        FK_EmployeeId = Convert.ToInt32(DirectCast(dgrdVwEmpPermissions.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_EmployeeId"))
        objEmpPermissions = New Emp_Permissions
        With objEmpPermissions
            .FK_EmployeeId = FK_EmployeeId
            .PermissionId = PermissionId
            IsRequest = .IsRequest
        End With
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
        If Not (PermissionId = 0) Then
            Dim fileName As String = PermissionId.ToString()
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
            objEmpPermissions = New Emp_Permissions()
            objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
            objEmpPermissions.PermissionOption = PermissionType
            objEmpPermissions.PermDate = dtpFromDateSearch.SelectedDate
            objEmpPermissions.PermEndDate = dtpToDateSearch.SelectedDate
            dtCurrent = objEmpPermissions.GetAllInnerJoin()
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

    Private Sub rblEmp_GPAType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblEmp_GPAType.SelectedIndexChanged
        If rblEmp_GPAType.SelectedValue = 1 Then
            txtEmp_GPA.MaxValue = 100
            txtEmp_GPA.MinValue = 0
            txtEmp_GPA.NumberFormat.DecimalDigits = 0
        Else
            txtEmp_GPA.MaxValue = 4
            txtEmp_GPA.MinValue = 0
            txtEmp_GPA.NumberFormat.DecimalDigits = 2
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
    Protected Sub dgrdVwEmpPermissions_DataBound(ByVal sender As Object,
                                                 ByVal e As System.EventArgs) Handles dgrdVwEmpPermissions.DataBound
        If Lang = CtlCommon.Lang.AR Then
            dgrdVwEmpPermissions.MasterTableView.GetColumn("Status").Visible = False
            dgrdVwEmpPermissions.MasterTableView.GetColumn("StatusAr").Visible = True
        Else
            dgrdVwEmpPermissions.MasterTableView.GetColumn("Status").Visible = True
            dgrdVwEmpPermissions.MasterTableView.GetColumn("StatusAr").Visible = False
        End If
        'SetArrowDirection()
    End Sub

    Protected Sub dgrdVwEmpPermissions_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdVwEmpPermissions.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromTime").ToString())) And (Not item.GetDataKeyValue("FromTime").ToString() = "")) Then
                Dim fromTime As DateTime = item.GetDataKeyValue("FromTime").ToString()
                item("FromTime").Text = fromTime.ToString("HH:mm")
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ToTime").ToString())) And (Not item.GetDataKeyValue("ToTime").ToString() = "")) Then
                Dim toTime As DateTime = item.GetDataKeyValue("ToTime")
                item("ToTime").Text = toTime.ToString("HH:mm")
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("PermDate").ToString())) And (Not item.GetDataKeyValue("PermDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("PermDate")
                item("PermDate").Text = fromDate.ToString("dd/MM/yyyy")
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("PermEndDate").ToString())) And (Not item.GetDataKeyValue("PermEndDate").ToString() = "")) Then
                Dim toDate As DateTime = item.GetDataKeyValue("PermEndDate")
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
        objEmpPermissions = New Emp_Permissions()
        objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
        objEmpPermissions.PermissionOption = PermissionType
        'objEmpPermissions.PermDate = DateSerial(Today.Year, Today.Month, 1)
        'objEmpPermissions.PermEndDate = DateSerial(Today.Year, Today.Month + 1, 0)
        objEmpPermissions.PermDate = dtpFromDateSearch.SelectedDate
        objEmpPermissions.PermEndDate = dtpToDateSearch.SelectedDate
        dtCurrent = objEmpPermissions.GetAllInnerJoin()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdVwEmpPermissions.DataSource = dv
    End Sub

#End Region

#Region "Mnage periodical option overhead"

    Protected Sub radBtnPeriod_CheckedChanged(ByVal sender As Object,
                                              ByVal e As System.EventArgs) Handles radBtnPeriod.CheckedChanged
        ShowHide(True)
    End Sub

    Protected Sub radBtnOneDay_CheckedChanged(ByVal sender As Object,
                                              ByVal e As System.EventArgs) Handles radBtnOneDay.CheckedChanged
        ShowHide(False)
    End Sub

    Protected Sub radBtnSpecificDays_CheckedChanged(ByVal sender As Object,
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
        objEmpPermissions = New Emp_Permissions()
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

        'If isFlixible = False AndAlso PermissionType = PermissionOption.Normal Then
        '    If Not CheckPermissionTypeDuration() Then
        '        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PermissionTypeDurationLimit"))
        '        Return -1
        '    End If
        'End If

        objPermissionsTypes = New PermissionsTypes
        objEmployee_Manager = New Employee_Manager

        With objPermissionsTypes
            .PermId = RadCmpPermissions.SelectedValue
            .GetByPK()
            Dim ErrMessage As String
            Dim IsManager As Boolean
            objEmployee_Manager.FK_ManagerId = EmployeeFilterUC.EmployeeId
            If objEmployee_Manager.IsManager = True Then
                IsManager = False
            Else
                IsManager = True
            End If
            If Not .ExcludeManagers_FromAfterBefore Then
                If IsManager = False Then
                    If Not objPermissionsTypes.AllowedAfterDays = Nothing Then
                        If dtpPermissionDate.SelectedDate > DateTime.Today.AddDays(.AllowedAfterDays) Then
                            If Lang = CtlCommon.Lang.AR Then
                                ErrMessage = "نوع المغادرة التي قمت باختيارها لايمكن طلبها لتاريخ بعد : " & .AllowedAfterDays & " أيام"
                                CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                            Else
                                ErrMessage = "The Selected Permission Type Not Allowed After : " & .AllowedAfterDays & " Day(s)"
                                CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                            End If

                            Return -1
                        End If
                    End If

                    If Not objPermissionsTypes.AllowedBeforeDays = Nothing Then
                        If dtpPermissionDate.SelectedDate < Date.Today.AddDays(.AllowedBeforeDays) Then
                            If Lang = CtlCommon.Lang.AR Then
                                ErrMessage = "نوع المغادرة التي قمت باختيارها لايمكن طلبها لتاريخ قبل : " & .AllowedBeforeDays + " أيام "
                                CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                            Else
                                ErrMessage = "The Selected Permission Type Not Allowed Before : " & .AllowedBeforeDays & " Day(s)"
                                CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                            End If

                            Return -1
                        End If
                    End If
                End If

                If IsPermissionExists(dtCurrent, True, dtpPermissionDate.SelectedDate, dtpPermissionDate.SelectedDate, RadTPtoTime.SelectedDate, RadTPtoTime.SelectedDate, PermissionId, dtpPermissionDate.SelectedDate) = True Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        ErrorMessage = " يوجد طلب مغادرة مسبقاً ضمن الفترة المطلوبة"
                        CtlCommon.ShowMessage(Me.Page, "يوجد طلب مغادرة مسبقاً ضمن الفترة المطلوبة!", "info")

                    Else
                        ErrorMessage = "Permission Record Already Exists Between The Date and Time Range"
                        CtlCommon.ShowMessage(Me.Page, "Permission Record Already Exists Between The Date and Time Range!", "info")

                    End If
                    Return -1
                End If
                If IsExists(dtpPermissionDate.SelectedDate, dtpPermissionDate.SelectedDate, EmployeeFilterUC.EmployeeId, 0) = True Then
                    If SessionVariables.CultureInfo = "en-US" Then
                        ErrorMessage = "Leave Record Already Exists In This Date"
                        CtlCommon.ShowMessage(Me.Page, "Leave Record Already Exists In This Date!", "info")

                    Else
                        ErrorMessage = "يوجد اجازة في هذا التاريخ"
                        CtlCommon.ShowMessage(Me.Page, "!يوجد اجازة في هذا التاريخ", "info")

                    End If
                    Return -1
                End If
            End If
        End With

        If PermissionType = PermissionOption.Normal Then
            If rdlTimeOption.SelectedValue = 0 Then
                'If (objEmpPermissions.ValidateEmployeePermission(EmployeeFilterUC.EmployeeId, PermissionId, dtCurrent, radBtnOneDay.Checked, dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate, tpFromTime,
                '                                                 tpToTime, dtpPermissionDate.SelectedDate, objPermissionsTypes, RadCmpPermissions.SelectedValue, chckFullDay.Checked, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
                '    If (ErrorMessage <> String.Empty) Then
                '        CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                '        Return -1
                '    End If
                'End If
            ElseIf rdlTimeOption.SelectedValue = 1 Then
                tpFromTime = DateTime.MinValue
                tpToTime = DateTime.MinValue
                If (objEmpPermissions.ValidateEmployeeFlexiblePermission(EmployeeFilterUC.EmployeeId, PermissionId, dtCurrent, radBtnOneDay.Checked,
                                                                        dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate,
                                                                         dtpPermissionDate.SelectedDate, tpFromTime, tpToTime, objPermissionsTypes, RadCmpPermissions.SelectedValue,
                                                                        chckFullDay.Checked, ErrorMessage, OffAndHolidayDays, strFlexibileDuration, EmpLeaveTotalBalance) = False) Then
                    If (ErrorMessage <> String.Empty) Then
                        CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                        Return -1
                    End If

                End If
            End If

        End If

        Dim days As String = String.Empty
        For i As Integer = 0 To chkWeekDays.Items.Count - 1
            If (chkWeekDays.Items(i).Selected) Then
                days &= chkWeekDays.Items(i).Value & ","
            End If
        Next

        If PermissionId = 0 Then
            If PermissionType = PermissionOption.Study Or PermissionType = PermissionOption.Nursing Then

                With objEmpPermissions
                    .FK_EmployeeId = EmployeeFilterUC.EmployeeId
                    .PermDate = dtpStartDatePerm.SelectedDate
                    .PermEndDate = dtpEndDatePerm.SelectedDate
                    .PermissionOption = PermissionType
                    .Days = days
                    If .Emp_Permissions_IsExistNursingStudy(ErrorMessage) = True Then
                        If (ErrorMessage <> String.Empty) Then
                            CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                            Return -1
                        End If
                    End If

                End With
            End If
        End If

        'If (chckFullDay.Checked) Then
        '    EmpLeaveTotalBalance = EmpLeaveTotalBalance - 1
        'End If



        ' Set data into object for Add / UpdateIf

        objEmpPermissions.PermissionId = PermissionId
        objEmpPermissions.LeaveId = LeaveID
        Dim fileUploadExtension As String
        If Not fuAttachFile.PostedFile Is Nothing Then
            fileUploadExtension = Path.GetExtension(fuAttachFile.PostedFile.FileName)
        End If

        If String.IsNullOrEmpty(fileUploadExtension) Then
            fileUploadExtension = FileExtension
        End If

        If Not PermissionType = PermissionOption.Nursing Then
            AllowedTime = Nothing
        Else
            objEmpPermissions.AllowedTime = rlstAllowed.SelectedValue
            AllowedTime = rlstAllowed.SelectedValue
        End If
        Dim StudyYear As Integer = 0
        If txtStudyYear.Text <> "" Then
            StudyYear = txtStudyYear.Text.Trim()
        Else
            StudyYear = 0
        End If

        Dim strSemester As String = ""
        Dim FK_UniversityId As Integer = Nothing
        Dim Emp_GPAType As Integer = Nothing
        Dim Emp_GPA As Decimal = Nothing

        If PermissionType = PermissionOption.Study Then
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            If objAPP_Settings.EnableSemesterSelection_StudyPermission = True Then
                If Not radcmbxSemester.SelectedValue = -1 Then
                    strSemester = radcmbxSemester.Text
                Else
                    strSemester = String.Empty
                End If
            Else
                If txtSemester.Text <> "" Then
                    objEmpPermissions.Semester = txtSemester.Text
                    strSemester = txtSemester.Text
                Else
                    objEmpPermissions.Semester = String.Empty
                    strSemester = String.Empty
                End If
            End If
            If Not radcmbxUnversity.SelectedValue = -1 Then
                FK_UniversityId = radcmbxUnversity.SelectedValue
            Else
                FK_UniversityId = Nothing
            End If
            Emp_GPAType = rblEmp_GPAType.SelectedValue
            Emp_GPA = txtEmp_GPA.Text

        End If


        objEmpPermissions.AddPermAllProcess(EmployeeFilterUC.EmployeeId, RadCmpPermissions.SelectedValue, chckIsDividable.Checked, isFlixible, radBtnSpecificDays.Checked,
                                         txtRemarks.Text, dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate, radBtnOneDay.Checked, tpFromTime,
                                         tpToTime, dtpPermissionDate.SelectedDate, PermissionDaysCount, OffAndHolidayDays, days, EmpLeaveTotalBalance, LeaveID,
                                         Integer.Parse(PermissionType), Integer.Parse(strFlexibileDuration), fileUploadExtension, chckFullDay.Checked, AllowedTime,
                                         ErrorMessage, StudyYear, strSemester, FK_UniversityId, Emp_GPAType, Emp_GPA)

        showResultMessage(Me.Page, ErrorMessage)
        If errNo = 0 Then
            If PermissionId = 0 Then
                If fileUploadExtension IsNot Nothing Then
                    Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                    Dim fileName As String = objEmpPermissions.PermissionId.ToString()
                    Dim fPath As String = String.Empty
                    fPath = Server.MapPath("..\PermissionFiles\\" + fileName + extention)
                    fuAttachFile.PostedFile.SaveAs(fPath)
                End If
            Else
                If fileUploadExtension IsNot Nothing Then
                    Dim fileName As String = objEmpPermissions.PermissionId.ToString()
                    Dim fPath As String = String.Empty
                    fPath = Server.MapPath("..\PermissionFiles\\" + fileName + FileExtension)
                    If File.Exists(fPath) Then
                        Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                        File.Delete(fPath)
                        fPath = Server.MapPath("..\PermissionFiles\\" + fileName + extention)
                        fuAttachFile.PostedFile.SaveAs(fPath)
                    Else
                        Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                        Dim fileNameExist As String = objEmpPermissions.PermissionId.ToString()
                        Dim fPathExist As String = String.Empty
                        fPathExist = Server.MapPath("..\PermissionFiles\\" + fileNameExist + extention)
                        fuAttachFile.PostedFile.SaveAs(fPathExist)
                    End If
                End If
            End If

            objRECALC_REQUEST = New RECALC_REQUEST
            objAPP_Settings = New APP_Settings()
            objAPP_Settings = objAPP_Settings.GetByPK()

            '----------------------Validate Employee Schedule------------'
            objWorkSchedule = New WorkSchedule()
            Dim intWorkScheduleType As Integer
            Dim intWorkScheduleId As Integer

            Dim ScheduleDate As DateTime
            If radBtnOneDay.Checked Then
                ScheduleDate = dtpPermissionDate.SelectedDate
            Else
                ScheduleDate = dtpStartDatePerm.SelectedDate
            End If

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
            objEmpPermissions = New Emp_Permissions
            Dim dt As DataTable
            Dim dt2 As DataTable
            Dim PreviousPermDate As DateTime
            Dim NextRestDate As DateTime

            '----------------------Validate Employee Schedule------------'
            If objAPP_Settings.ApprovalRecalMethod = 1 Then
                If radBtnOneDay.Checked AndAlso PermissionType = PermissionOption.Normal Then
                    If intWorkScheduleType = 3 Then

                        objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
                        objEmpPermissions.PermDate = dtpPermissionDate.SelectedDate
                        dt = objEmpPermissions.Get_Previous_Restday_Date()

                        If dt.Rows.Count > 0 Then
                            PreviousPermDate = dt.Rows(0)("RestDate")
                            objEmpPermissions.FK_EmployeeId = EmployeeId
                            objEmpPermissions.PreviousPermDate = PreviousPermDate
                            dt2 = objEmpPermissions.Get_Next_Restday_Date()
                            NextRestDate = dt2.Rows(0)("NextRestDate")
                            If NextRestDate > Date.Today Then
                                NextRestDate = Date.Today
                            End If
                        End If

                        If dt.Rows.Count > 0 Then
                            temp_date = PreviousPermDate
                            temp_str_date = DateToString(NextRestDate)
                        Else
                            temp_date = dtpPermissionDate.SelectedDate
                            temp_str_date = DateToString(temp_date)
                        End If
                    Else
                        temp_date = dtpPermissionDate.SelectedDate
                        temp_str_date = DateToString(temp_date)
                    End If

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
                    Dim dteFrom As DateTime
                    Dim dteTo As DateTime
                    If intWorkScheduleType = 3 Then

                        objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
                        objEmpPermissions.PermDate = dtpStartDatePerm.SelectedDate
                        dt = objEmpPermissions.Get_Previous_Restday_Date()

                        If dt.Rows.Count > 0 Then
                            PreviousPermDate = dt.Rows(0)("RestDate")
                            objEmpPermissions.FK_EmployeeId = EmployeeId
                            objEmpPermissions.PreviousPermDate = PreviousPermDate
                            dt2 = objEmpPermissions.Get_Next_Restday_Date()
                            NextRestDate = dt2.Rows(0)("NextRestDate")
                            If NextRestDate > Date.Today Then
                                NextRestDate = Date.Today
                            End If
                        End If

                        If dt.Rows.Count > 0 Then
                            dteFrom = PreviousPermDate
                            dteTo = NextRestDate
                        Else
                            dteFrom = dtpStartDatePerm.SelectedDate
                            dteTo = dtpEndDatePerm.SelectedDate
                        End If
                    Else
                        dteFrom = dtpStartDatePerm.SelectedDate
                        dteTo = dtpEndDatePerm.SelectedDate
                    End If

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
                If radBtnOneDay.Checked AndAlso PermissionType = PermissionOption.Normal Then
                    If intWorkScheduleType = 3 Then

                        objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
                        objEmpPermissions.PermDate = dtpPermissionDate.SelectedDate
                        dt = objEmpPermissions.Get_Previous_Restday_Date()

                        If dt.Rows.Count > 0 Then
                            PreviousPermDate = dt.Rows(0)("RestDate")
                            objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
                            objEmpPermissions.PreviousPermDate = PreviousPermDate
                            dt2 = objEmpPermissions.Get_Next_Restday_Date()
                            NextRestDate = dt2.Rows(0)("NextRestDate")
                            If NextRestDate > Date.Today Then
                                NextRestDate = Date.Today
                            End If
                        End If


                        If dt.Rows.Count > 0 Then
                            temp_date = PreviousPermDate
                            temp_str_date = DateToString(NextRestDate)
                        Else
                            temp_date = dtpPermissionDate.SelectedDate
                            temp_str_date = DateToString(temp_date)
                        End If
                    Else
                        temp_date = dtpPermissionDate.SelectedDate
                        temp_str_date = DateToString(temp_date)
                    End If

                    objRECALC_REQUEST.EMP_NO = EmployeeFilterUC.EmployeeId
                    objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)

                    objRecalculateRequest = New RecalculateRequest
                    With objRecalculateRequest
                        .Fk_EmployeeId = EmployeeFilterUC.EmployeeId
                        .FromDate = temp_date
                        If intWorkScheduleType = 3 Then
                            .ToDate = NextRestDate
                        Else
                            .ToDate = temp_date
                        End If
                        .ImmediatelyStart = True
                        .RecalStatus = 0
                        .CREATED_BY = SessionVariables.LoginUser.ID
                        .Remarks = "Employee Permission - SYSTEM"
                        err2 = .Add
                    End With

                Else
                    Dim dteFrom As DateTime
                    Dim dteTo As DateTime

                    If intWorkScheduleType = 3 Then
                        objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
                        objEmpPermissions.PermDate = dtpStartDatePerm.SelectedDate
                        dt = objEmpPermissions.Get_Previous_Restday_Date()

                        If dt.Rows.Count > 0 Then
                            PreviousPermDate = dt.Rows(0)("RestDate")
                            objEmpPermissions.FK_EmployeeId = EmployeeFilterUC.EmployeeId
                            objEmpPermissions.PreviousPermDate = PreviousPermDate
                            dt2 = objEmpPermissions.Get_Next_Restday_Date()
                            NextRestDate = dt2.Rows(0)("NextRestDate")
                            If NextRestDate > Date.Today Then
                                NextRestDate = Date.Today
                            End If
                        End If

                        If dt.Rows.Count > 0 Then
                            dteFrom = PreviousPermDate
                            dteTo = NextRestDate
                        Else
                            dteFrom = dtpStartDatePerm.SelectedDate
                            dteTo = dtpEndDatePerm.SelectedDate
                        End If
                    Else
                        dteFrom = dtpStartDatePerm.SelectedDate
                        dteTo = dtpEndDatePerm.SelectedDate
                    End If

                    If Not dteFrom > Date.Today Then
                        objRecalculateRequest = New RecalculateRequest
                        With objRecalculateRequest
                            .Fk_EmployeeId = EmployeeFilterUC.EmployeeId
                            .FromDate = dteFrom
                            .ToDate = dteTo
                            .ImmediatelyStart = True
                            .RecalStatus = 0
                            .CREATED_BY = SessionVariables.LoginUser.ID

                            If PermissionType = PermissionOption.Nursing Then
                                .Remarks = "Employee Nursing Permission - SYSTEM"
                            ElseIf PermissionType = PermissionOption.Study Then
                                .Remarks = "Employee Study Permission - SYSTEM"
                            Else
                                .Remarks = "Employee Permission - SYSTEM"
                            End If

                            err2 = .Add
                        End With
                    End If
                End If
            End If


            Select Case DisplayMode.ToString()
                Case "Add"
                    btnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)
                Case Else
                    FillGridView()
                    ClearAll()

                    If IsViolationsCorrection = True Then
                        ScriptManager.RegisterStartupScript(Page, Page.GetType, Guid.NewGuid.ToString, "RefreshPage()", True)
                        'Response.Redirect("../DailyTasks/HR_EmployeeViolations.aspx")
                    End If
            End Select

        End If

        HideShowControl()

    End Function

    Function updateOnly() As Integer
        Dim errNo As Integer = -1
        objEmpPermissions = New Emp_Permissions()
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

        If Not objEmpPermissions.PermissionOption = PermissionOption.Nursing Then
            AllowedTime = Nothing
        Else
            objEmpPermissions.AllowedTime = objEmpPermissions.AllowedTime
        End If

        Dim errorNum As Integer = -1

        If txtStudyYear.Text <> "" Then
            objEmpPermissions.StudyYear = txtStudyYear.Text.Trim()
        Else
            objEmpPermissions.StudyYear = 0
        End If

        If txtSemester.Text <> "" Then
            objEmpPermissions.Semester = txtSemester.Text
        Else
            objEmpPermissions.Semester = String.Empty
        End If

        objEmpPermissions.FillObjectData(EmployeeFilterUC.EmployeeId, RadCmpPermissions.SelectedValue, chckIsDividable.Checked, isFlixible, radBtnSpecificDays.Checked,
                                 txtRemarks.Text, dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate, radBtnOneDay.Checked, tpFromTime,
                                 tpToTime, dtpPermissionDate.SelectedDate, PermissionDaysCount, PermissionType, OffAndHolidayDays, days, Integer.Parse(strFlexibileDuration), FileExtension, chckFullDay.Checked, AllowedTime)
        If PermissionId > 0 Then
            ' Update a data base record , on update mode
            objEmpPermissions.PermissionId = PermissionId
            errNo = objEmpPermissions.Update()

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

                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <> "00:00") AndAlso
                            ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) <> "00:00") Then

                            If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Or
                                ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                Return True
                            Else
                                Return False
                            End If
                        Else
                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
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

                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <> "00:00") AndAlso
                                ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) <> "00:00") Then

                                If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Or
                                    ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                    returnPeriodValue = True
                                Else
                                    returnPeriodValue = False
                                End If
                            Else
                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
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

                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <> "00:00") AndAlso
                                    ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) <> "00:00") Then

                                    If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Or
                                        ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                        returnSpecificDaysValue = True
                                    Else
                                        returnSpecificDaysValue = False
                                    End If
                                Else
                                    If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
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

                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <> "00:00") AndAlso
                            ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) <> "00:00") Then

                            If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Or
                                ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                returnNursingValue = True
                            Else
                                returnNursingValue = False
                            End If
                        Else
                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
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

                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <> "00:00") AndAlso
                                ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) <> "00:00") Then

                                If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Or
                                    ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                    returnStudyValue = True
                                Else
                                    returnStudyValue = False
                                End If
                            Else
                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
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

            Dim dtDuration1 As Integer = (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration1").ToString().Insert(2, ":")).Hour * 60) +
                                            (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration1").ToString().Insert(2, ":")).Minute)
            Dim dtDuration2 As Integer = (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration2").ToString().Insert(2, ":")).Hour * 60) +
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

                        If (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") AndAlso
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") Then

                            If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Or
                                ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).AddMinutes(dtDuration2).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                Return True
                            Else
                                Return False
                            End If
                        Else
                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
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

                            If (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") AndAlso
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") Then

                                If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Or
                                    ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).AddMinutes(dtDuration2).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                    returnPeriodFlexValue = True
                                Else
                                    returnPeriodFlexValue = False
                                End If
                            Else
                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
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

                                If (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") AndAlso
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") Then

                                    If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Or
                                        ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).AddMinutes(dtDuration2).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                        returnSpecificFlexValue = True
                                    Else
                                        returnSpecificFlexValue = False
                                    End If
                                Else
                                    If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
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

                        If (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") AndAlso
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") Then

                            If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Or
                                ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).AddMinutes(dtDuration2).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                returnFlexNursingValue = True
                            Else
                                returnFlexNursingValue = False
                            End If
                        Else
                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
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

                            If (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") AndAlso
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") Then

                                If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Or
                                    ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).AddMinutes(dtDuration2).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                    returnStudyFlexValue = True
                                Else
                                    returnStudyFlexValue = False
                                End If
                            Else
                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
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
                    Dim objWorkSchedule_Shifts As New WorkSchedule_Shifts
                    objWorkSchedule_Shifts.FK_ScheduleId = intWorkScheduleId
                    dtShiftSchedule = objWorkSchedule_Shifts.GetByFKScheduleID()

                    'Commented To Include Overnight Shifts
                    'If ((Convert.ToDateTime(dtShiftSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                    '    (Convert.ToDateTime(dtShiftSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) Or _
                    '    (Convert.ToDateTime(dtShiftSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                    '    (Convert.ToDateTime(dtShiftSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                    returnShiftResult = True
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
                            If ((Convert.ToDateTime(dtShiftSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtShiftSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) Or
                                (Convert.ToDateTime(dtShiftSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And
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

                            If ((Convert.ToDateTime(dtShiftSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtShiftSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) Or
                                (Convert.ToDateTime(dtShiftSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And
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

                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And
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

                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And
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

                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And
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

                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And
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

                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And
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


                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And
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

                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And
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

                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                     (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                     (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And
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

                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And
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

                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And
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





    Public Function IsPermissionExists(ByVal dtCurrent As DataTable, ByVal IsPermissionOneDay As Boolean, ByVal Fromdate As Date?, ByVal Todate As Date?, ByVal FromTime As Date?, ByVal ToTime As Date?, ByVal PermissionId As Integer,
                                ByVal PermDate As DateTime) As Boolean

        Dim status As Boolean
        Dim requestStatus As Integer
        Dim containRequestStatus As Boolean = False
        Dim columns As DataColumnCollection = dtCurrent.Columns
        If columns.Contains("FK_StatusId") Then
            containRequestStatus = True
        End If

        For Each dr As DataRow In dtCurrent.Rows
            If PermissionId = dr("PermissionId") Then
                Continue For
            End If

            If containRequestStatus And Not IsDBNull(dr("FK_StatusId")) Then

                requestStatus = Convert.ToInt32(dr("FK_StatusId"))
                If (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByGeneralManager)) _
                        Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByHumanResource)) _
                        Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedbyManager)) Then
                    Continue For
                End If
            End If

            If (IsPermissionOneDay) Then
                FromTime = PermDate.AddHours(FromTime.Value.Hour).AddMinutes(FromTime.Value.Minute)
                ToTime = PermDate.AddHours(ToTime.Value.Hour).AddMinutes(ToTime.Value.Minute)
                If Fromdate >= dr("PermDate") Then
                    If (FromTime <> Nothing Or Not dr("FromTime") Is System.DBNull.Value) Then
                        If FromTime >= dr("FromTime") And FromTime < dr("ToTime") Or ToTime > dr("FromTime") And ToTime <= dr("ToTime") Then
                            If containRequestStatus And Not dr("FK_StatusId") Is System.DBNull.Value Then
                                requestStatus = Convert.ToInt32(dr("FK_StatusId"))
                                If (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByGeneralManager)) _
                                        Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByHumanResource)) _
                                        Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedbyManager)) Then
                                    Continue For
                                Else
                                    status = True
                                End If
                            Else
                                status = True
                            End If
                        End If
                    End If
                End If
            Else
                If (Not dr("PermEndDate") Is System.DBNull.Value) Then
                    If Fromdate >= dr("PermDate") And Fromdate <= dr("PermEndDate") Or Todate >= dr("PermDate") And Todate <= dr("PermEndDate") Then
                        If (FromTime IsNot Nothing And (Not IsDBNull(dr("FromTime")))) Then
                            If FromTime >= dr("FromTime") And FromTime < dr("ToTime") Or ToTime > dr("FromTime") And ToTime <= dr("ToTime") Then
                                If containRequestStatus Then
                                    requestStatus = Convert.ToInt32(dr("FK_StatusId"))
                                    If (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByGeneralManager)) _
                                            Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByHumanResource)) _
                                            Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedbyManager)) Then
                                        Continue For
                                    Else
                                        status = True
                                    End If
                                Else
                                    status = True
                                End If
                            End If
                        End If
                    End If
                Else
                    If Fromdate = dr("PermDate") Or Todate = dr("PermDate") Then
                        If (FromTime <> Nothing And Not dr("FromTime") Is System.DBNull.Value) Then
                            If FromTime >= dr("FromTime") And FromTime < dr("ToTime") Or ToTime > dr("FromTime") And ToTime <= dr("ToTime") Then
                                If containRequestStatus Then
                                    requestStatus = Convert.ToInt32(dr("FK_StatusId"))
                                    If (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByGeneralManager)) _
                                            Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByHumanResource)) _
                                            Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedbyManager)) Then
                                        Continue For
                                    Else
                                        status = True
                                    End If
                                Else
                                    status = True
                                End If
                            End If
                        End If
                    End If
                End If
            End If

        Next
        Return status
    End Function

    Private Function IsExists(ByVal Fromdate As Date, ByVal Todate As Date, ByVal EmployeeId As Integer, ByVal LeaveId As Integer) As Boolean
        Dim EmpDt As DataTable = New DataTable

        objEmp_Leaves = New Emp_Leaves

        objEmp_Leaves.FK_EmployeeId = EmployeeId
        objEmp_Leaves.FromDate = Fromdate
        objEmp_Leaves.ToDate = Todate
        objEmp_Leaves.LeaveId = LeaveId
        EmpDt = objEmp_Leaves.GetAllLeavesByEmployee()

        'to check if duplicate data in leave request table


        Dim status As Boolean
        For Each dr As DataRow In EmpDt.Rows

            If Fromdate >= dr("FromDate") And Fromdate <= dr("ToDate") Or Todate >= dr("FromDate") And Todate <= dr("ToDate") Then
                status = True
            End If
        Next
        Return status
    End Function

#End Region

#Region "Methods"

    Private Function Is_FullDay() As Boolean

        Return chckFullDay.Checked

    End Function

    Private Sub DisableEnableTimeView(ByVal status As Boolean)
        If DisplayMode.ToString() = "ViewAll" Or
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
            trSpecificTime.Visible = status
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
                    If SessionVariables.CultureInfo = "en-US" Then
                        'tdOption.Style("margin-right") = "-455px"
                    Else
                        'tdOption.Style("margin-left") = "-310px"
                    End If
                Else
                    If (browser.Contains("IE")) Then
                        If SessionVariables.CultureInfo = "en-US" Then
                            'tdOption.Style("margin-right") = "200px"
                        Else
                            'tdOption.Style("margin-left") = "300px"
                        End If


                    Else
                        If SessionVariables.CultureInfo = "en-US" Then
                            'tdOption.Style("margin-right") = "-440px"
                        Else
                            'tdOption.Style("margin-left") = "-310px"
                        End If
                    End If

                End If
                trWeekDays.Style("display") = "none"
                trType.Style("display") = "block"
                trNursingFlexibleDurationPermission.Visible = False
                divAllowedTime.Visible = False
                'trDifTime.Style("width") = "135px"
                If SessionVariables.CultureInfo = "en-US" Then
                    'tdWeekDays.Style("margin-right") = "-470px"
                    'tdWeekDays.Style("float") = "right"
                    'tdOption.Style("float") = "right"

                Else
                    'tdWeekDays.Style("margin-left") = "-390px"
                    'tdWeekDays.Style("float") = "left"
                    'tdOption.Style("float") = "left"

                End If

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
                    If SessionVariables.CultureInfo = "en-US" Then
                        'trDateFromTo.Style("padding-left") = "10px"
                        'trAttachedFile.Style("padding-left") = "10px"
                        'trRemarks.Style("padding-left") = "10px"
                        'trNursingFlexibleDurationPermission.Style("padding-left") = "10px"
                        'tdNursingFlexibleDurationPermission.Style("padding-left") = "10px"
                    Else
                        'trDateFromTo.Style("padding-right") = "10px"
                        'trAttachedFile.Style("padding-right") = "10px"
                        'trRemarks.Style("padding-right") = "10px"
                        'trNursingFlexibleDurationPermission.Style("padding-right") = "10px"
                        'tdNursingFlexibleDurationPermission.Style("padding-right") = "10px"
                    End If
                    tdDate.Style("width") = "125px"
                Else
                    If (browser.Contains("IE")) Then
                        If SessionVariables.CultureInfo = "en-US" Then
                            'trDateFromTo.Style("padding-left") = "10px"
                            'trAttachedFile.Style("padding-left") = "10px"
                            'trRemarks.Style("padding-left") = "10px"
                            'trNursingFlexibleDurationPermission.Style("padding-left") = "10px"
                            'tdNursingFlexibleDurationPermission.Style("padding-left") = "10px"
                        Else
                            'trDateFromTo.Style("padding-right") = "10px"
                            'trAttachedFile.Style("padding-right") = "10px"
                            'trRemarks.Style("padding-right") = "10px"
                            'trNursingFlexibleDurationPermission.Style("padding-right") = "10px"
                            'tdNursingFlexibleDurationPermission.Style("padding-right") = "10px"
                        End If
                    Else
                        If SessionVariables.CultureInfo = "en-US" Then
                            'trDateFromTo.Style("padding-left") = "10px"
                            'trAttachedFile.Style("padding-left") = "10px"
                            'trRemarks.Style("padding-left") = "10px"
                            'trNursingFlexibleDurationPermission.Style("padding-left") = "10px"
                            'tdNursingFlexibleDurationPermission.Style("padding-left") = "10px"
                        Else
                            'trDateFromTo.Style("padding-right") = "10px"
                            'trAttachedFile.Style("padding-right") = "10px"
                            'trRemarks.Style("padding-right") = "10px"
                            'trNursingFlexibleDurationPermission.Style("padding-right") = "10px"
                            'tdNursingFlexibleDurationPermission.Style("padding-right") = "10px"
                        End If
                    End If
                End If
                radBtnPeriod.Checked = True
                trWeekDays.Style("display") = "none"
                trTime.Style("display") = "none"
                trDifTime.Style("display") = "none"
                trPermType.Style("display") = "none"
                trType.Style("display") = "none"
                trNursingFlexibleDurationPermission.Visible = True
                divAllowedTime.Visible = True
                'trDateFromTo.Style("padding-left") = "25px"


                'tdDate.Style("padding-left") = "10px"
                PnlOneDayLeave.Visible = False
                pnlPeriodLeave.Visible = True
                reqPermission.Enabled = False
                ExtenderPermission.Enabled = False
                userCtrlEmpPermHeader.HeaderText = ResourceManager.GetString("EmpNursePerm", CultureInfo)
                'lblFromDate.Text = ResourceManager.GetString("BirthDate", CultureInfo)
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
                trSpecificTime.Visible = False
            Case PermissionOption.Study

                If (browser.Contains("Chrome")) Then

                    tdDate.Style("width") = "135px"
                    If SessionVariables.CultureInfo = "en-US" Then
                        'tdWeekDays.Style("margin-right") = "-515px"
                        'trTimeFromTo.Style("padding-left") = "20px"
                        'trDif.Style("padding-left") = "20px"
                        'trRemarks.Style("padding-left") = "20px"
                        'trAttachedFile.Style("padding-left") = "20px"
                        'trDateFromTo.Style("padding-left") = "20px"
                    Else
                        'tdWeekDays.Style("margin-left") = "-420px"
                        'trTimeFromTo.Style("padding-right") = "20px"
                        'trDif.Style("padding-right") = "20px"
                        'trRemarks.Style("padding-right") = "20px"
                        'trAttachedFile.Style("padding-right") = "20px"
                        'trDateFromTo.Style("padding-right") = "20px"
                        'tdWeekDays.Style("margin-right") = "-410px"
                    End If
                Else
                    If (browser.Contains("InternetExplorer")) Then

                        tdDate.Style("width") = "135px"
                        'tdDate.Style("padding-left") = "10px"

                        If SessionVariables.CultureInfo = "en-US" Then
                            'tdWeekDays.Style("margin-right") = "-535px"
                            'trTimeFromTo.Style("padding-left") = "45px"
                            'trDif.Style("padding-left") = "45px"
                            'trRemarks.Style("padding-left") = "45px"
                            'trAttachedFile.Style("padding-left") = "45px"
                            'trDateFromTo.Style("padding-left") = "45px"
                        Else
                            'tdWeekDays.Style("margin-left") = "-445px"
                            'trTimeFromTo.Style("padding-right") = "45px"
                            'trDif.Style("padding-right") = "45px"
                            'trRemarks.Style("padding-right") = "45px"
                            'trAttachedFile.Style("padding-right") = "45px"
                            'trDateFromTo.Style("padding-right") = "45px"
                        End If
                    Else
                        If (browser.Contains("IE")) Then
                            If SessionVariables.CultureInfo = "en-US" Then

                                'tdWeekDays.Style("margin-right") = "-50px"
                                'tdWeekDays.Style("margin-top") = "20px"
                                tdWeekDays.Style("width") = "100%"

                                'trTimeFromTo.Style("padding-left") = "55px"
                                'trDif.Style("padding-left") = "55px"
                                'trRemarks.Style("padding-left") = "55px"
                                'trAttachedFile.Style("padding-left") = "55px"
                                'trDateFromTo.Style("padding-left") = "55px"
                            Else
                                'tdWeekDays.Style("margin-left") = "-50px"
                                'tdWeekDays.Style("margin-top") = "15px"
                                tdWeekDays.Style("width") = "100%"

                                'trTimeFromTo.Style("padding-right") = "60px"
                                'trDif.Style("padding-right") = "62px"
                                'trRemarks.Style("padding-right") = "62px"
                                'trAttachedFile.Style("padding-right") = "62px"
                                'trDateFromTo.Style("padding-right") = "60px"
                            End If
                        Else
                            ''firefox



                            If SessionVariables.CultureInfo = "en-US" Then
                                'tdWeekDays.Style("margin-right") = "-490px"
                                'trDateFromTo.Style("padding-left") = "12px"
                                'trTimeFromTo.Style("padding-left") = "22px"
                                'trDif.Style("padding-left") = "12px"
                                'trRemarks.Style("padding-left") = "12px"
                                'trAttachedFile.Style("padding-left") = "12px"
                            Else
                                'tdWeekDays.Style("margin-left") = "-410px"
                                'trDateFromTo.Style("padding-right") = "12px"
                                'trTimeFromTo.Style("padding-right") = "12px"
                                'trDif.Style("padding-right") = "12px"
                                'trRemarks.Style("padding-right") = "12px"
                                'trAttachedFile.Style("padding-right") = "12px"
                            End If
                        End If

                    End If


                End If

                radBtnSpecificDays.Checked = True
                trWeekDays.Style("display") = "block"
                'trWeekDays.Style("padding-left") = "0px"
                trPermType.Style("display") = "none"
                trType.Style("display") = "none"
                trNursingFlexibleDurationPermission.Visible = False
                divAllowedTime.Visible = False

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
                lblStudyYear.Visible = True
                txtStudyYear.Visible = True
                lblSemester.Visible = True
                txtSemester.Visible = True
                userCtrlEmpPermHeader.HeaderText = ResourceManager.GetString("EmpStudyPerm", CultureInfo)
        End Select

    End Sub

    Protected Sub chckFullDay_CheckedChanged(ByVal sender As Object,
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

            'MsgBox(ex.Message)

        End Try
    End Function

    Private Sub setLocalizedTextField(ByVal comb As RadComboBox,
                             ByVal EnName As String, ByVal ArName As String)
        comb.DataTextField = IIf(SessionVariables.CultureInfo = "en-US",
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

        objEmpPermissions = New Emp_Permissions
        With objEmpPermissions
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
                If PermissionId = 0 Then
                    remainingBalanceCount += (Convert.ToDateTime(RadTPtoTime.SelectedDate.Value).Subtract(Convert.ToDateTime(RadTPfromTime.SelectedDate.Value))).TotalMinutes
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

    Private Sub ArcivingMonths_DateValidation()
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        Dim ArchivingMonths As Integer = objAPP_Settings.ArchivingMonths

        If Not ArchivingMonths = 0 Then
            ArchivingMonths = ArchivingMonths
            Dim NewDate As Date = DateAdd(DateInterval.Month, ArchivingMonths, Date.Today)
            dtpPermissionDate.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
            dtpStartDatePerm.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
            dtpEndDatePerm.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
            dtpFromDateSearch.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
            dtpToDateSearch.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
        End If

    End Sub

    Private Sub FillSemesters()
        objSemesters = New Semesters
        With objSemesters
            CtlCommon.FillTelerikDropDownList(radcmbxSemester, .GetAll, Lang)
        End With
    End Sub

    Private Sub FillUnversities()
        objEmp_University = New Emp_University
        With objEmp_University
            CtlCommon.FillTelerikDropDownList(radcmbxUnversity, .GetAll, Lang)
        End With
    End Sub

#End Region

#Region "Start & End date validation"

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

    'Protected Sub dtpEndDatePerm_SelectedDateChanged(ByVal sender As Object, _
    '                                            ByVal e As SelectedDateChangedEventArgs) _
    '                                            Handles dtpEndDatePerm.SelectedDateChanged




    '    If IS_Period() And dtpEndDatePerm.SelectedDate() IsNot Nothing Then


    '        StartEndDateComparison(dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate)


    '    End If


    'End Sub

#End Region

#Region "Handle Selected Time changed"

    Protected Sub RadTPtoTim_SelectedDateChanged(ByVal sender As Object,
                                                         ByVal e As SelectedDateChangedEventArgs) _
                                                         Handles RadTPtoTime.SelectedDateChanged
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        ' If (hdnIsvalid.Value) Then
        setTimeDifference()
        ' Else
        'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TimeRange", CultureInfo))

        'End If

    End Sub

    Protected Sub RadTPfromTime_SelectedDateChanged(ByVal sender As Object,
                                                             ByVal e As SelectedDateChangedEventArgs) _
                                                     Handles RadTPfromTime.SelectedDateChanged
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        'If (hdnIsvalid.Value) Then
        setTimeDifference()
        'Else
        'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TimeRange", CultureInfo))
        'End If

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
