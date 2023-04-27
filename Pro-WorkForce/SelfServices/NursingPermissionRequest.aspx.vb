Imports TA.Definitions
Imports TA.Employees
Imports System.Data
Imports TA.Admin
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.SelfServices
Imports TA.Lookup
Imports System.Resources
Imports System.IO
Imports SmartV.UTILITIES.ProjectCommon
Imports TA.DailyTasks
Imports TA.Security

Partial Class SelfServices_NursingPermissionRequest
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_WorkSchedule As Emp_WorkSchedule
    Private objWorkSchedule As WorkSchedule
    Private objWorkSchedule_NormalTime As WorkSchedule_NormalTime
    Private objWorkSchedule_Flexible As WorkSchedule_Flexible
    Private objWorkSchedule_Shifts As WorkSchedule_Shifts
    Private objEmp_Shifts As Emp_Shifts
    Private objEmp_Nursing_Permission As Emp_Nursing_Permission
    Shared dtCurrent As DataTable
    Private objPermissionsTypes As PermissionsTypes
    Private objRequestStatus As RequestStatus
    Private Lang As CtlCommon.Lang
    Private objAPP_Settings As APP_Settings
    Dim babyBirthDate As DateTime
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Shared SortExepression As String
    Private objEmployee As Employee
    Private objEmployeeViolations As EmployeeViolations
    Private objSYSForms As SYSForms
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

    Public Property ApprovedRequired() As Boolean
        Get
            Return ViewState("ApprovedRequired")
        End Get
        Set(ByVal value As Boolean)
            ViewState("ApprovedRequired") = value
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

    Private Property IsFirstGrid() As String
        Get
            Return ViewState("IsFirstGrid")
        End Get
        Set(ByVal value As String)
            ViewState("IsFirstGrid") = value
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

#End Region

#Region "Page Events"

    Protected Sub SelfServices_NursingPermissionRequest_PreRenderComplete(sender As Object, e As EventArgs) Handles Me.PreRenderComplete

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
                'dgrdEmpPermissionRequest.Columns(1).Visible = False
                dgrdEmpPermissionRequest.Columns(11).Visible = False
            Else
                Lang = CtlCommon.Lang.EN
                'dgrdEmpPermissionRequest.Columns(2).Visible = False
                dgrdEmpPermissionRequest.Columns(12).Visible = False
            End If
            dtpFromDateSearch.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            dtpToDateSearch.SelectedDate = dd
            'mvEmpPermissionRequest.SetActiveView(viewPermissionRequests)
            HideShowViews()
            FillLeaveStatus()
            FillGridView()
            SetRadDateTimePickerPeoperties()
            HideShowControl()
            objAPP_Settings = New APP_Settings
            With objAPP_Settings
                .GetByPK()

                If SessionVariables.CultureInfo = "en-US" Then
                    lblGeneralGuide.Text = .NursingGeneralGuide
                Else
                    lblGeneralGuide.Text = .NursingGeneralGuideAr
                End If
            End With
            ArcivingMonths_DateValidation()
        End If

        Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        scriptManager__1.RegisterPostBackControl(lnbLeaveFile)

        Dim dtpStartDate As DateTime = dtpStartDatePerm.SelectedDate
        Dim dtpEndDate As DateTime = dtpEndDatePerm.SelectedDate
        EmpLeaveRequestHeader.HeaderText = ResourceManager.GetString("SelfNursePerm", CultureInfo)
        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdEmpPermissionRequest.ClientID + "');")

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

    Protected Sub dgrdEmpHR_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmpPermissionRequest.NeedDataSource
        objEmp_Nursing_Permission = New Emp_Nursing_Permission()
        objEmp_Nursing_Permission.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        objEmp_Nursing_Permission.PermDate = DateSerial(Today.Year, Today.Month, 1)
        objEmp_Nursing_Permission.PermEndDate = DateSerial(Today.Year, Today.Month + 1, 0)
        dtCurrent = objEmp_Nursing_Permission.GetByEmployee()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdEmpPermissionRequest.DataSource = dv
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim isFlixible As Boolean
        Dim strFlexibileDuration As String
        Dim ErrorMessage As String = String.Empty
        ApprovedRequired = True
        Dim errNo As Integer
        objAPP_Settings = New APP_Settings
        objEmp_Nursing_Permission = New Emp_Nursing_Permission
        objPermissionsTypes = New PermissionsTypes
        strFlexibileDuration = RadCmbFlixebleDuration.SelectedValue
        isFlixible = True

        objEmployee = New Employee()
        objEmployee.EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        objEmployee.GetByPK()
        If (objEmployee.Gender = "m") Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SelfNursingValidation", CultureInfo), "info")
            Return
        End If
        objAPP_Settings.GetByPK()


        'For Check if allowed in ramadan time
        If objAPP_Settings.AllowNursingInRamadan = False Then
            Dim dtAllowNursingInRamadan As DataTable
            Dim AllowedNursingInRamadan As Boolean
            With objEmp_Nursing_Permission
                .PermDate = dtpStartDatePerm.SelectedDate
                .PermEndDate = dtpEndDatePerm.SelectedDate
                dtAllowNursingInRamadan = .AllowedNursingInRamadan()
                AllowedNursingInRamadan = dtAllowNursingInRamadan.Rows(0)(0)
            End With
            If AllowedNursingInRamadan = False Then
                If Lang = CtlCommon.Lang.AR Then
                    CtlCommon.ShowMessage(Me.Page, "لا يمكنك تقديم مغادرة الرضاعه خلال فترة رمضان", "info")
                Else
                    CtlCommon.ShowMessage(Me.Page, "you can't request Nursing Permission during Ramadan Period ", "info")
                End If
                Exit Sub
            End If

        End If
        ''''


        'For Check if Attachment is Manadetory
        If fuAttachFile.HasFile = False Then
            If objAPP_Settings.AttachmentIsMandatory = True Then
                If Lang = CtlCommon.Lang.AR Then
                    CtlCommon.ShowMessage(Me.Page, "يرجى ارفاق ملف!", "info")
                Else
                    CtlCommon.ShowMessage(Me.Page, "Please Attach File!", "info")
                End If
                Exit Sub
            End If
        End If
        ''''''

        'For ChecK Maternity Leave for the employee for last 3 month
        If objAPP_Settings.NursingRequireMaternity = True Then
            Dim dtChKHasMaternityLeave As DataTable
            Dim HasMaternityLeave As Boolean
            With objEmp_Nursing_Permission
                .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                .FK_MaternityLeaveTypeId = objAPP_Settings.FK_MaternityLeaveTypeId
                dtChKHasMaternityLeave = .ChKHasMaternityLeave()
                HasMaternityLeave = dtChKHasMaternityLeave.Rows(0)(0)
            End With
            If HasMaternityLeave = False Then
                If Lang = CtlCommon.Lang.AR Then
                    CtlCommon.ShowMessage(Me.Page, "لايوجد إجازة امومة للموظفة في التاريخ الذي اخترته", "info")
                Else
                    CtlCommon.ShowMessage(Me.Page, "Employee Does Not Have Maternity Leave", "info")
                End If
                Exit Sub
            End If
        End If

        ''''''

        If objAPP_Settings.NursingPermissionAttend = True Then
            objEmployeeViolations = New EmployeeViolations
            objEmployeeViolations.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            objEmployeeViolations.M_Date = dtpStartDatePerm.SelectedDate
            objEmployeeViolations.GetEmpInTime()
            If objEmployeeViolations.M_Time = Nothing Then
                If Lang = CtlCommon.Lang.AR Then
                    CtlCommon.ShowMessage(Me.Page, "لايوجد حركة دخول للموظف في التاريخ الذي اخترته", "info")
                Else
                    CtlCommon.ShowMessage(Me.Page, "Employee Does Not Have In Transaction In the Selected From Date", "info")
                End If
                Return
            End If
        End If

        FillObjectData()


        If (objEmp_Nursing_Permission.ValidateEmployeePermission(ErrorMessage) = False) Then
            If (ErrorMessage <> String.Empty) Then
                CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                Return
            End If
        End If


        If PermissionId = 0 Then
            If Not fuAttachFile.PostedFile Is Nothing Then
                objEmp_Nursing_Permission.AttachedFile = Path.GetExtension(fuAttachFile.PostedFile.FileName)
            End If

            objEmp_Nursing_Permission.FK_StatusId = Convert.ToInt32(CtlCommon.RequestStatusEnum.Pending)
            errNo = objEmp_Nursing_Permission.Add()
            PermissionId = objEmp_Nursing_Permission.PermissionRequestId

        End If

        If Not fuAttachFile.PostedFile Is Nothing Then
            Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
            Dim fileName As String = PermissionId.ToString()
            Dim fPath As String = String.Empty
            fPath = Server.MapPath("..\NursingPermissionRequestFiles\\" + fileName + extention)
            fuAttachFile.PostedFile.SaveAs(fPath)

            If Not ApprovedRequired Then
                If (File.Exists(Server.MapPath("..\NursingPermissionRequestFiles\\" + PermissionId.ToString() + extention))) Then

                    Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("..\NursingPermissionRequestFiles\\" + PermissionId.ToString() + extention))
                    CopyFile.CopyTo(Server.MapPath("..\PermissionFiles\\" + PermissionId.ToString() + extention))

                    Rename(Server.MapPath("..\PermissionFiles\\" + PermissionId.ToString() + extention), _
                                            Server.MapPath("..\PermissionFiles\\" + PermissionId + extention))
                End If
            End If
        End If


        If errNo = 0 Then
            If Lang = CtlCommon.Lang.AR Then
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Redirect", "ShowMessageAndRedirect('تم إرسال الطلب الى المدير المباشر','../SelfServices/NursingPermissionRequest.aspx');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Redirect", "ShowMessageAndRedirect('The Request has been Sent to Direct Manager','../SelfServices/NursingPermissionRequest.aspx');", True)
            End If
            FillGridView()
            HideShowControl()
           
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
        FillGridView()
        HideShowControl()
    End Sub

    Protected Sub btnRequestPermission_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRequestPermission.Click
        ClearAll()
        mvEmpPermissionRequest.SetActiveView(viewAddPermissionRequest)
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("../SelfServices/NursingPermissionRequest.aspx?Id=1")
        'mvEmpPermissionRequest.SetActiveView(viewPermissionRequests)
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        For Each Item As GridDataItem In dgrdEmpPermissionRequest.Items
            Dim cb As CheckBox = DirectCast(Item.FindControl("chk"), CheckBox)
            If cb.Checked Then

                ' Get Permission Id from hidden label
                Dim PermissionIdgrd As Integer = Item.GetDataKeyValue("PermissionRequestId")

                ' Get Employee Id from hidden label
                Dim Status As Integer = Convert.ToInt32(Item.GetDataKeyValue("FK_StatusId"))
                Dim AttachedFile As String
                If Not IsDBNull(Item.GetDataKeyValue("AttachedFile")) Then
                    AttachedFile = Item.GetDataKeyValue("AttachedFile").ToString()
                End If


                If (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.Pending) Or (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByGeneralManager)) Or _
                    (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByHumanResource)) Or (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedbyManager))) Then
                    ' Delete current checked item
                    objEmp_Nursing_Permission = New Emp_Nursing_Permission
                    objEmp_Nursing_Permission.PermissionRequestId = PermissionIdgrd
                    errNum = objEmp_Nursing_Permission.Delete()

                    If errNum = 0 Then
                        Dim fileName As String = PermissionIdgrd.ToString()
                        Dim fPath As String = String.Empty
                        fPath = Server.MapPath("..\NursingPermissionRequestFiles\\" + fileName + AttachedFile)
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
        'ClearAll()
    End Sub

    Protected Sub dgrdEmpPermissionRequest_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdEmpPermissionRequest.SelectedIndexChanged
        PermissionId = Convert.ToInt32(DirectCast(dgrdEmpPermissionRequest.SelectedItems(0), GridDataItem).GetDataKeyValue("PermissionRequestId"))
        FillControls()
        mvEmpPermissionRequest.SetActiveView(viewAddPermissionRequest)
    End Sub

    Protected Sub btnGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGet.Click
        objEmp_Nursing_Permission = New Emp_Nursing_Permission
        objEmp_Nursing_Permission.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        objEmp_Nursing_Permission.PermDate = dtpFromDateSearch.SelectedDate
        objEmp_Nursing_Permission.PermEndDate = dtpToDateSearch.SelectedDate
        objEmp_Nursing_Permission.FK_StatusId = ddlStatus.SelectedValue
        dgrdEmpPermissionRequest.DataSource = objEmp_Nursing_Permission.GetByStatusType()
        dgrdEmpPermissionRequest.DataBind()
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Protected Sub lnkDownloadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        objEmp_Nursing_Permission = New Emp_Nursing_Permission
        With objEmp_Nursing_Permission
            .PermissionRequestId = PermissionId
            .GetByPK()

            Dim FileName As String = PermissionId.ToString() + .AttachedFile
            Dim FilePath As String = Server.MapPath("..\\NursingPermissionRequestFiles\") & FileName
            CtlCommon.Open_Download_File(FileName, FilePath)

        End With
    End Sub

#End Region

#Region "Methods"

    Private Function ValidatePermissionWorkingTime() As Boolean
        Dim ScheduleDate As DateTime
        Dim intWorkScheduleType As Integer
        Dim intWorkScheduleId As Integer
        Dim dtEmpWorkSchedule As DataTable
        Dim found As Boolean = False
        Dim objApp_Settings As New APP_Settings
        Dim returnShiftResult As Boolean = False

        objEmp_WorkSchedule = New Emp_WorkSchedule()
        objWorkSchedule = New WorkSchedule()
        objWorkSchedule.EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
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


            End While

            Return returnNursingValue

        ElseIf intWorkScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Flexible) Then
            objWorkSchedule_Flexible = New WorkSchedule_Flexible()
            objWorkSchedule_Flexible.FK_ScheduleId = intWorkScheduleId
            dtEmpWorkSchedule = objWorkSchedule_Flexible.GetAll()

            Dim dtDuration1 As Integer = (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration1").ToString().Insert(2, ":")).Hour * 60) + _
                                            (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration1").ToString().Insert(2, ":")).Minute)
            Dim dtDuration2 As Integer = (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration2").ToString().Insert(2, ":")).Hour * 60) + _
                                            (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration2").ToString().Insert(2, ":")).Minute)

            Dim DayId As Integer



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
                dteStartDate = dteStartDate.AddDays(1)
            End While

            Return returnFlexNursingValue

        ElseIf intWorkScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Advance) Then
            Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
            Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate
            Dim dtShiftSchedule As DataTable

            While dteStartDate < dteEndDate
                objEmp_Shifts = New Emp_Shifts
                objEmp_Shifts.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                objEmp_Shifts.WorkDate = dteStartDate
                Dim AdvancedSchedule = objEmp_Shifts.GetShiftsByDate()

                If (AdvancedSchedule IsNot Nothing And AdvancedSchedule.Rows.Count > 0) Then
                    objWorkSchedule_Shifts = New WorkSchedule_Shifts
                    objWorkSchedule_Shifts.FK_ScheduleId = intWorkScheduleId
                    dtShiftSchedule = objWorkSchedule_Shifts.GetByFKScheduleID()

                    returnShiftResult = True
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

        End If
        Return True

    End Function

    Function CheckDefaultScheduleWorkingTime() As Boolean
        objEmp_WorkSchedule = New Emp_WorkSchedule()
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
                dteStartDate.AddDays(1)
            End While


        ElseIf intWorkScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Flexible) Then
            objWorkSchedule_Flexible = New WorkSchedule_Flexible()
            objWorkSchedule_Flexible.FK_ScheduleId = intWorkScheduleId
            dtEmpWorkSchedule = objWorkSchedule_Flexible.GetAll()

            Dim DayId As Integer
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
                dteStartDate.AddDays(1)
            End While
        End If

        Return True
    End Function

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmpPermissionRequest.Skin))
    End Function

    Private Sub FillLeaveStatus()
        objRequestStatus = New RequestStatus()
        Dim dt As DataTable = Nothing
        dt = Nothing
        dt = objRequestStatus.GetAll
        ProjectCommon.FillRadComboBox(ddlStatus, dt, "StatusName", _
                                     "StatusNameArabic", "StatusId", Lang)
    End Sub

    Private Sub FillGridView()
        Try
            objEmp_Nursing_Permission = New Emp_Nursing_Permission()
            objEmp_Nursing_Permission.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            objEmp_Nursing_Permission.PermDate = dtpFromDateSearch.SelectedDate
            objEmp_Nursing_Permission.PermEndDate = dtpToDateSearch.SelectedDate
            dtCurrent = objEmp_Nursing_Permission.GetByEmployee()
            dgrdEmpPermissionRequest.DataSource = dtCurrent
            dgrdEmpPermissionRequest.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetRadDateTimePickerPeoperties()

        ' Set Data input properties
        Me.dtpStartDatePerm.SelectedDate = Today.Date
        ' Set popup properties
        Me.dtpStartDatePerm.PopupDirection = DatePickerPopupDirection.TopRight
        Me.dtpStartDatePerm.ShowPopupOnFocus = True
        ' Set calender boundaries
        Me.dtpStartDatePerm.MinDate = New Date(2006, 1, 1)
        Me.dtpStartDatePerm.MaxDate = New Date(2040, 1, 1)
        Me.dtpStartDatePerm.BorderWidth = 0
    End Sub

    Private Sub HideShowControl()
        Dim browser As String = Request.Browser.Type
        If (browser.Contains("InternetExplorer")) Then
            'If SessionVariables.CultureInfo = "en-US" Then
            '    trAttachedFile.Style("padding-left") = "10px"
            '    trRemarks.Style("padding-left") = "10px"
            '    tdNursingFlexibleDurationPermission.Style("padding-left") = "10px"
            'Else
            '    trAttachedFile.Style("padding-right") = "10px"
            '    trRemarks.Style("padding-right") = "10px"
            '    tdNursingFlexibleDurationPermission.Style("padding-right") = "10px"
            'End If

        Else
            If (browser.Contains("IE")) Then
                'If SessionVariables.CultureInfo = "en-US" Then

                '    trAttachedFile.Style("padding-left") = "10px"
                '    trRemarks.Style("padding-left") = "10px"

                '    tdNursingFlexibleDurationPermission.Style("padding-left") = "10px"
                'Else

                '    trAttachedFile.Style("padding-right") = "10px"
                '    trRemarks.Style("padding-right") = "10px"

                '    tdNursingFlexibleDurationPermission.Style("padding-right") = "10px"
                'End If
            Else
                'If SessionVariables.CultureInfo = "en-US" Then

                '    trAttachedFile.Style("padding-left") = "10px"
                '    trRemarks.Style("padding-left") = "10px"

                '    tdNursingFlexibleDurationPermission.Style("padding-left") = "10px"
                'Else

                '    trAttachedFile.Style("padding-right") = "10px"
                '    trRemarks.Style("padding-right") = "10px"

                '    tdNursingFlexibleDurationPermission.Style("padding-right") = "10px"
                'End If
            End If
        End If
        'lblFromDate.Text = ResourceManager.GetString("BirthDate", CultureInfo)
        babyBirthDate = dtpStartDatePerm.SelectedDate
        objAPP_Settings = New APP_Settings()
        objAPP_Settings = objAPP_Settings.GetByPK()
        Dim nursingDay As Integer = objAPP_Settings.NursingDays
        babyBirthDate = babyBirthDate.AddDays(nursingDay)
        dtpEndDatePerm.SelectedDate = babyBirthDate
        hdnNurdingDay.Value = nursingDay

        'Disable To Date and HasFlexibleNursingPermission

        dtpEndDatePerm.Enabled = False
        If Not objAPP_Settings.HasFlexibleNursingPermission Then
            rlstAllowed.Items.RemoveAt(2)
        End If
    End Sub

    Private Sub FillObjectData()
        With objEmp_Nursing_Permission
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FK_PermId = -1
            .IsDividable = False
            .RejectionReason = String.Empty
            .IsFlexible = True
            .IsFullDay = False
            .IsSpecificDays = False
            .Remark = txtRemarks.Text
            .PermDate = dtpStartDatePerm.SelectedDate
            If PermissionId = 0 Then
                If fuAttachFile.HasFile Then
                    .AttachedFile = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                Else
                    .AttachedFile = String.Empty
                End If
            Else
                .AttachedFile = FileExtension
            End If
            .IsForPeriod = True
            .PermDate = dtpStartDatePerm.SelectedDate
            .PermEndDate = dtpEndDatePerm.SelectedDate
            .FlexibilePermissionDuration = RadCmbFlixebleDuration.SelectedValue
            .CREATED_BY = SessionVariables.LoginUser.ID
            .AllowedTime = rlstAllowed.SelectedValue
        End With


    End Sub

    Private Sub FillControls()
        objEmp_Nursing_Permission = New Emp_Nursing_Permission()
        objEmp_Nursing_Permission.PermissionRequestId = PermissionId
        objEmp_Nursing_Permission.GetByPK()
        With objEmp_Nursing_Permission
            dtpStartDatePerm.SelectedDate = .PermDate
            dtpEndDatePerm.SelectedDate = .PermEndDate
            RadCmbFlixebleDuration.SelectedValue = .FlexibilePermissionDuration
            txtRemarks.Text = .Remark
            rlstAllowed.SelectedValue = .AllowedTime
            FileExtension = .AttachedFile
            Select Case objEmp_Nursing_Permission.FK_StatusId
                Case 1
                    ControlsStatus(False)
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
            Dim fPath As String = "..\NursingPermissionRequestFiles\" + PermissionId.ToString() + .AttachedFile
            Dim FilePath As String = Server.MapPath("~/NursingPermissionRequestFiles")
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
            btnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)
        End With
    End Sub

    Private Sub ControlsStatus(ByVal Status As Boolean)

        RadCmbFlixebleDuration.Enabled = Status
        dtpStartDatePerm.Enabled = Status
        dtpEndDatePerm.Enabled = Status
        fuAttachFile.Enabled = Status
        txtRemarks.Enabled = Status
        btnSave.Visible = Status
        rlstAllowed.Enabled = Status
    End Sub

    Private Sub ShowHideControls()
        objAPP_Settings = New APP_Settings

        With objAPP_Settings
            .GetByPK()
            If Not .HasFlexibleNursingPermission Then
                rlstAllowed.Items.RemoveAt(3)
            End If

        End With

    End Sub

    Private Sub HideShowViews()
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            IsFirstGrid = .IsFirstGrid
            If Request.QueryString("id") = 0 Then
                If .IsFirstGrid Then
                    mvEmpPermissionRequest.SetActiveView(viewPermissionRequests)
                Else
                    mvEmpPermissionRequest.SetActiveView(viewAddPermissionRequest)
                End If
            Else
                mvEmpPermissionRequest.SetActiveView(viewPermissionRequests)
            End If
        End With
    End Sub

    Private Sub ClearAll()
        dtpStartDatePerm.SelectedDate = Date.Today
        babyBirthDate = dtpStartDatePerm.SelectedDate
        objAPP_Settings = New APP_Settings()
        objAPP_Settings = objAPP_Settings.GetByPK()
        Dim nursingDay As Integer = objAPP_Settings.NursingDays
        babyBirthDate = babyBirthDate.AddDays(nursingDay)
        dtpEndDatePerm.SelectedDate = babyBirthDate

        RadCmbFlixebleDuration.SelectedValue = 60
        rlstAllowed.SelectedValue = 1
        txtRemarks.Text = String.Empty
        dtpFromDateSearch.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
        Dim dd As New Date
        dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
        dtpToDateSearch.SelectedDate = dd
        PermissionId = 0
    End Sub

    Private Sub ArcivingMonths_DateValidation()
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        Dim ArchivingMonths As Integer = objAPP_Settings.ArchivingMonths

        If Not ArchivingMonths = 0 Then
            ArchivingMonths = ArchivingMonths
            Dim NewDate As Date = DateAdd(DateInterval.Month, ArchivingMonths, Date.Today)
            dtpStartDatePerm.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
            dtpEndDatePerm.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
            dtpFromDateSearch.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
            dtpToDateSearch.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
        End If

    End Sub

#End Region

End Class
