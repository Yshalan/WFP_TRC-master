Imports Telerik.Web.UI
Imports SmartV.UTILITIES
Imports TA.SelfServices
Imports System.Data
Imports System.IO
Imports TA.LookUp
Imports Telerik.Web.UI.Calendar
Imports Telerik.Web
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports TA.Definitions
Imports TA.Employees
Imports TA.Admin
Imports SmartV.UTILITIES.ProjectCommon
Imports TA.Security

Partial Class SelfServices_StudyPermissionRequest
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objEmp_Study_PermissionRequest As Emp_Study_PermissionRequest
    Shared dtCurrent As DataTable
    Private objRequestStatus As RequestStatus
    Dim objWeekDays As New WeekDays
    Private objWorkSchedule As WorkSchedule
    Private objWorkSchedule_Flexible As WorkSchedule_Flexible
    Private objWorkSchedule_NormalTime As WorkSchedule_NormalTime
    Private objWorkSchedule_Shifts As WorkSchedule_Shifts
    Private objEmp_WorkSchedule As Emp_WorkSchedule
    Private objEmp_Shifts As Emp_Shifts
    Private objAPP_Settings As APP_Settings
    Private objPermissionsTypes As PermissionsTypes
    Private objEmployee_Manager As Employee_Manager
    Private objSYSForms As SYSForms
    Private objSemesters As Semesters
    Private objEmp_University As Emp_University
    Private objStudyMajors As StudyMajors
    Private objStudySpecialization As StudySpecialization

#End Region

#Region "Properties"

    Enum DisplayModeEnum
        Add
        Edit
        View
        ViewAll
        ViewAddEdit
    End Enum

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

    Public Property DisplayMode() As DisplayModeEnum
        Get
            Return ViewState("DisplayMode")
        End Get
        Set(ByVal value As DisplayModeEnum)
            ViewState("DisplayMode") = value
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
            Return ViewState("dtCurrentControls")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrentControls") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub SelfServices_StudyPermissionRequest_PreRenderComplete(sender As Object, e As EventArgs) Handles Me.PreRenderComplete
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
            rmtFlexibileTime.TextWithLiterals = "0000"

            SetRadDateTimePickerPeoperties()
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "en-US" Then
                Lang = CtlCommon.Lang.EN
                dgrdEmpPermissionRequest.Columns(12).Visible = False
            Else
                Lang = CtlCommon.Lang.AR
                dgrdEmpPermissionRequest.Columns(11).Visible = False
            End If
            ManageControls(True)
            DisableEnableTimeView(True)
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

            EmpStudyPermRequestHeader.HeaderText = ResourceManager.GetString("Emp_StudyPermissionRequest", CultureInfo)

            btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdEmpPermissionRequest.ClientID + "');")

            dtpFromDateSearch.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            dtpToDateSearch.SelectedDate = dd

            HideShowViews()
            FillLeaveStatus()
            FillGridView()
            ShowHideControls()
            CtlCommon.FillCheckBox(chkWeekDays, objWeekDays.GetAll())

            For i As Integer = 0 To chkWeekDays.Items.Count - 1
                chkWeekDays.Items(i).Text = String.Format("{0} {1}", " ", chkWeekDays.Items(i).Text)
            Next

            rdlTimeOption.Items.FindByValue("0").Selected = True
            trFlixibleTime.Style("display") = "none"

            HideShowControl()

            dtpStartDatePerm.SelectedDate = Date.Today
            dtpEndDatePerm.SelectedDate = Date.Today

            ArcivingMonths_DateValidation()


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

            If objAPP_Settings.EnableMajorSelection_StudyPermission = True Then
                dvMajor_Specialization.Visible = True
                FillStudyMajors()
            Else
                dvMajor_Specialization.Visible = False
            End If
        End If

        ' You can put the suitable caption for the TimeView
        RadTPfromTime.TimeView.HeaderText = String.Empty
        RadTPtoTime.TimeView.HeaderText = String.Empty
        RadTPfromTime.TimeView.TimeFormat = "HH:mm"
        RadTPfromTime.TimeView.DataBind()
        RadTPtoTime.TimeView.TimeFormat = "HH:mm"
        RadTPtoTime.TimeView.DataBind()

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

    Protected Sub RadTPtoTim_SelectedDateChanged(ByVal sender As Object,
                                                           ByVal e As SelectedDateChangedEventArgs) _
                                                           Handles RadTPtoTime.SelectedDateChanged
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If (hdnIsvalid.Value) Then
            setTimeDifference()
        Else
            'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TimeRange", CultureInfo))

        End If

    End Sub

    Protected Sub RadTPfromTime_SelectedDateChanged(ByVal sender As Object,
                                                             ByVal e As SelectedDateChangedEventArgs) _
                                                     Handles RadTPfromTime.SelectedDateChanged
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If (hdnIsvalid.Value) Then
            setTimeDifference()
        Else
            'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TimeRange", CultureInfo))
        End If

    End Sub

    Protected Sub btnGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGet.Click
        objEmp_Study_PermissionRequest = New Emp_Study_PermissionRequest
        objEmp_Study_PermissionRequest.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        objEmp_Study_PermissionRequest.PermDate = dtpFromDateSearch.SelectedDate
        objEmp_Study_PermissionRequest.PermEndDate = dtpToDateSearch.SelectedDate
        objEmp_Study_PermissionRequest.FK_StatusId = ddlStatus.SelectedValue
        dgrdEmpPermissionRequest.DataSource = objEmp_Study_PermissionRequest.GetByStatusType()
        dgrdEmpPermissionRequest.DataBind()
    End Sub

    Protected Sub btnRequestPermission_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRequestPermission.Click
        ClearAll()
        mvEmpPermissionRequest.SetActiveView(viewAddPermissionRequest)
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


                If (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.Pending) Or (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByGeneralManager)) Or
                    (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByHumanResource)) Or (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedbyManager))) Then
                    ' Delete current checked item
                    objEmp_Study_PermissionRequest = New Emp_Study_PermissionRequest
                    objEmp_Study_PermissionRequest.PermissionRequestId = PermissionIdgrd
                    errNum = objEmp_Study_PermissionRequest.Delete()

                    If errNum = 0 Then
                        Dim fileName As String = PermissionIdgrd.ToString()
                        Dim fPath As String = String.Empty
                        fPath = Server.MapPath("..\StudyPermissionRequestFiles\\" + fileName + AttachedFile)
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

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Page.Validate("EmpPermissionGroup")
        If Page.IsValid Then


            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            Dim errNo As Integer
            objEmp_Study_PermissionRequest = New Emp_Study_PermissionRequest()
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()

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


            Dim fileUploadExtension As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)

            tpFromTime = RadTPfromTime.SelectedDate
            tpToTime = RadTPtoTime.SelectedDate
            strFlexibileDuration = (CInt(rmtFlexibileTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtFlexibileTime.TextWithLiterals.Split(":")(1))


            If (objEmp_Study_PermissionRequest.CheckMaxStudyDuration(tpFromTime, tpToTime, isFlixible, strFlexibileDuration, ErrorMessage) = False) Then
                If (ErrorMessage <> String.Empty) Then
                    CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                    Return
                End If
            End If


            If isFlixible = False Then

                If Not ValidatePermissionWorkingTime() Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("WorkScheduleTiemRange"), "info")
                    Return

                End If
            End If


            If Not objAPP_Settings.StudyAllowedAfterDays Is Nothing Then
                If (dtpStartDatePerm.SelectedDate) > DateTime.Today.AddDays(objAPP_Settings.StudyAllowedAfterDays) Then
                    If Lang = CtlCommon.Lang.AR Then
                        CtlCommon.ShowMessage(Me.Page, "نوع مغادرة الدراسة يمكن طلبها لتاريخ بعد : " & objAPP_Settings.StudyAllowedAfterDays.ToString() & " أيام", "info")
                    Else
                        CtlCommon.ShowMessage(Me.Page, "The Study Permission Allowed After : " & objAPP_Settings.StudyAllowedAfterDays.ToString & " Day(s)", "info")
                    End If
                    Return
                End If
            End If

            If (Not objAPP_Settings.StudyAllowedBeforeDays Is Nothing) Then
                If dtpEndDatePerm.SelectedDate <= DateTime.Today.AddDays(objAPP_Settings.StudyAllowedBeforeDays * -1) Then
                    If Lang = CtlCommon.Lang.AR Then
                        CtlCommon.ShowMessage(Me.Page, "نوع مغادرة الدراسة يمكن طلبها لتاريخ قبل : " & objAPP_Settings.StudyAllowedBeforeDays.ToString() + " أيام ", "info")
                    Else
                        CtlCommon.ShowMessage(Me.Page, "The Study Permission Allowed Before : " & objAPP_Settings.StudyAllowedBeforeDays.ToString & " Day(s)", "info")
                    End If
                    Return
                End If
            End If

            objPermissionsTypes = New PermissionsTypes
            objEmployee_Manager = New Employee_Manager


            Dim days As String = String.Empty
            Dim flag As Boolean = False
            For i As Integer = 0 To chkWeekDays.Items.Count - 1
                If (chkWeekDays.Items(i).Selected) Then
                    days &= chkWeekDays.Items(i).Value & ","
                    flag = True
                End If
            Next
            If flag = False Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("DaySelect", CultureInfo), "info")
                Return
            End If


            'For Check if Attachment is Manadetory
            If (fileUploadExtension Is Nothing) Or (fileUploadExtension = "") Then
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

            'Set data into object for Add / UpdateIf

            objEmp_Study_PermissionRequest.PermissionRequestId = PermissionId
            If Not fileUploadExtension Is Nothing Then
                If String.IsNullOrEmpty(fileUploadExtension) Then
                    fileUploadExtension = FileExtension
                End If
            End If


            If (objEmp_Study_PermissionRequest.ValidateEmployeePermission(SessionVariables.LoginUser.FK_EmployeeId, PermissionId, dtCurrent, dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate,
                                                                      objPermissionsTypes, tpFromTime, tpToTime, days, ErrorMessage) = False) Then
                If (ErrorMessage <> String.Empty) Then
                    CtlCommon.ShowMessage(Me.Page, ErrorMessage)
                    Return
                End If
            End If

            FillObjectData()
            'For Check if time inside Schedule
            If rdlTimeOption.SelectedValue = 0 Then
                If (objEmp_Study_PermissionRequest.CheckPermissionTimeInSideSchedule(RadTPfromTime.SelectedDate, RadTPtoTime.SelectedDate, ErrorMessage)) = False Then
                    If (ErrorMessage <> String.Empty) Then
                        CtlCommon.ShowMessage(Me.Page, ErrorMessage)
                        Return
                    End If
                End If
            End If
            ''''''
            If PermissionId = 0 Then
                If fuAttachFile.HasFile = True Then
                    If Not fuAttachFile.PostedFile Is Nothing Then
                        objEmp_Study_PermissionRequest.AttachedFile = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                    End If
                End If
                objEmp_Study_PermissionRequest.FK_StatusId = Convert.ToInt32(CtlCommon.RequestStatusEnum.Pending)
                errNo = objEmp_Study_PermissionRequest.Add()
                PermissionId = objEmp_Study_PermissionRequest.PermissionRequestId
            End If

            If fuAttachFile.HasFile = True Then
                If Not fuAttachFile.PostedFile Is Nothing Then
                    Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                    Dim fileName As String = PermissionId.ToString()
                    Dim fPath As String = String.Empty
                    fPath = Server.MapPath("..\StudyPermissionRequestFiles\\" + fileName + extention)
                    fuAttachFile.PostedFile.SaveAs(fPath)
                End If
            End If

            If errNo = 0 Then
                If Lang = CtlCommon.Lang.AR Then
                    ScriptManager.RegisterStartupScript(Me, Page.GetType, "Redirect", "ShowMessageAndRedirect('تم إرسال الطلب الى المدير المباشر','../SelfServices/StudyPermissionRequest.aspx');", True)
                Else
                    ScriptManager.RegisterStartupScript(Me, Page.GetType, "Redirect", "ShowMessageAndRedirect('The Request has been Sent to Direct Manager','../SelfServices/StudyPermissionRequest.aspx');", True)
                End If
                FillGridView()
                HideShowControl()
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
            FillGridView()
            HideShowControl()
        End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("../SelfServices/StudyPermissionRequest.aspx?Id=1")
        'mvEmpPermissionRequest.SetActiveView(viewPermissionRequests)
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Protected Sub lnkDownloadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        objEmp_Study_PermissionRequest = New Emp_Study_PermissionRequest
        With objEmp_Study_PermissionRequest
            .PermissionRequestId = PermissionId
            .GetByPK()

            Dim FileName As String = PermissionId.ToString() + .AttachedFile
            Dim FilePath As String = Server.MapPath("..\\StudyPermissionRequestFiles\") & FileName
            CtlCommon.Open_Download_File(FileName, FilePath)

        End With
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

    Private Sub radcmbxMajor_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radcmbxMajor.SelectedIndexChanged
        If Not radcmbxMajor.SelectedValue = -1 Then
            FillStudySpecialization()
        Else
            radcmbxSemester.SelectedValue = -1
            radcmbxSemester.Items.Clear()
        End If
    End Sub

#End Region

#Region "Methods"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmpPermissionRequest.Skin))
    End Function

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

    Private Sub FillLeaveStatus()
        objRequestStatus = New RequestStatus()
        Dim dt As DataTable = Nothing
        dt = Nothing
        dt = objRequestStatus.GetAll
        ProjectCommon.FillRadComboBox(ddlStatus, dt, "StatusName",
                                     "StatusNameArabic", "StatusId", Lang)
    End Sub

    Private Sub FillGridView()
        Try
            objEmp_Study_PermissionRequest = New Emp_Study_PermissionRequest
            objEmp_Study_PermissionRequest.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            objEmp_Study_PermissionRequest.PermDate = dtpFromDateSearch.SelectedDate
            objEmp_Study_PermissionRequest.PermEndDate = dtpToDateSearch.SelectedDate
            dtCurrent = objEmp_Study_PermissionRequest.GetByEmployee()
            dgrdEmpPermissionRequest.DataSource = dtCurrent
            dgrdEmpPermissionRequest.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillControls()

        objEmp_Study_PermissionRequest = New Emp_Study_PermissionRequest
        objEmp_Study_PermissionRequest.PermissionRequestId = PermissionId
        objEmp_Study_PermissionRequest.GetByPK()
        With objEmp_Study_PermissionRequest

            dtpFromDateSearch.SelectedDate = .PermDate
            dtpToDateSearch.SelectedDate = .PermEndDate

            FileExtension = .AttachedFile
            fuAttachFile.Visible = True
            Dim FilePath As String = Server.MapPath("~/StudyPermissionRequestFiles")
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

            RadTPfromTime.SelectedDate = .FromTime
            RadTPtoTime.SelectedDate = .ToTime

            txtRemarks.Text = .Remark
            chkWeekDays.ClearSelection()
            If (Not String.IsNullOrEmpty(.Days)) Then
                If (.Days.Contains(",")) Then
                    Dim daysArr() As String = .Days.Split(",")
                    For j As Integer = 0 To daysArr.Length - 1
                        If (Not String.IsNullOrEmpty(daysArr(j))) Then
                            chkWeekDays.Items.FindByValue(daysArr(j)).Selected = True
                        End If
                    Next
                End If
            End If
            If .StudyYear = 0 Then
                txtStudyYear.Text = String.Empty
            Else
                txtStudyYear.Text = .StudyYear
            End If
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            If objAPP_Settings.EnableSemesterSelection_StudyPermission = True Then
                dvSemesterSelection.Visible = True
                dvSemesterText.Visible = False
                Dim item As RadComboBoxItem = radcmbxSemester.FindItemByText(.Semester)
                If Not item Is Nothing Then
                    item.Selected = True
                End If
            Else
                dvSemesterSelection.Visible = False
                dvSemesterText.Visible = True
                txtSemester.Text = .Semester
            End If

            If objAPP_Settings.EnableUniversitySelection_StudyPermission = True Then
                dvUniversity.Visible = True
                radcmbxUnversity.SelectedValue = .FK_UniversityId
            Else
                dvUniversity.Visible = False
            End If

            If Not .Emp_GPAType Is Nothing Then
                rblEmp_GPAType.SelectedValue = .Emp_GPAType
            End If

            If Not .Emp_GPA Is Nothing Then
                txtEmp_GPA.Text = .Emp_GPA
            Else
                txtEmp_GPA.Text = String.Empty
            End If

            If Not .FK_MajorId Is Nothing Then
                radcmbxMajor.SelectedValue = .FK_MajorId
            End If

            If Not .FK_SpecializationId Is Nothing Then
                radcmbxSpecialization.SelectedValue = .FK_SpecializationId
            End If

        End With
    End Sub

    Private Sub HideShowControl()
        Dim browser As String = Request.Browser.Type
        If (browser.Contains("Chrome")) Then


            'If SessionVariables.CultureInfo = "en-US" Then
            '    tdWeekDays.Style("margin-right") = "-515px"
            '    trTimeFromTo.Style("padding-left") = "20px"
            '    trRemarks.Style("padding-left") = "20px"
            '    trAttachedFile.Style("padding-left") = "20px"
            '    trDateFromTo.Style("padding-left") = "20px"
            'Else
            '    tdWeekDays.Style("margin-left") = "-420px"
            '    trTimeFromTo.Style("padding-right") = "20px"
            '    trRemarks.Style("padding-right") = "20px"
            '    trAttachedFile.Style("padding-right") = "20px"
            '    trDateFromTo.Style("padding-right") = "20px"
            '    tdWeekDays.Style("margin-right") = "-410px"
            'End If
        Else
            If (browser.Contains("InternetExplorer")) Then

                'If SessionVariables.CultureInfo = "en-US" Then
                '    'tdWeekDays.Style("margin-right") = "-535px"
                '    'trTimeFromTo.Style("padding-left") = "45px"
                '    trRemarks.Style("padding-left") = "45px"
                '    trAttachedFile.Style("padding-left") = "45px"
                '    'trDateFromTo.Style("padding-left") = "45px"
                'Else
                '    tdWeekDays.Style("margin-left") = "-445px"
                '    trTimeFromTo.Style("padding-right") = "45px"
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
                    '    trRemarks.Style("padding-left") = "55px"
                    '    trAttachedFile.Style("padding-left") = "55px"
                    '    trDateFromTo.Style("padding-left") = "55px"
                    'Else
                    '    tdWeekDays.Style("margin-left") = "-50px"
                    '    tdWeekDays.Style("margin-top") = "15px"
                    '    tdWeekDays.Style("width") = "100%"

                    '    trTimeFromTo.Style("padding-right") = "60px"
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
                    '    trRemarks.Style("padding-left") = "12px"
                    '    trAttachedFile.Style("padding-left") = "12px"
                    'Else
                    '    tdWeekDays.Style("margin-left") = "-410px"
                    '    trDateFromTo.Style("padding-right") = "12px"
                    '    trTimeFromTo.Style("padding-right") = "12px"
                    '    trRemarks.Style("padding-right") = "12px"
                    '    trAttachedFile.Style("padding-right") = "12px"
                    'End If
                End If

            End If


        End If

        trWeekDays.Style("display") = "block"
        trWeekDays.Style("padding-left") = "0px"
        'If SessionVariables.CultureInfo = "en-US" Then
        '    tdWeekDays.Style("float") = "right"
        'Else
        '    tdWeekDays.Style("float") = "left"
        'End If

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
        'Me.RadTPfromTime.SelectedDate = Now
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            Dim DefaultStudyPermissionFromTime As New TimeSpan(0, .DefaultStudyPermissionFromTime, 0)
            Me.RadTPfromTime.SelectedDate = Now.Date + DefaultStudyPermissionFromTime

            Dim DefaultStudyPermissionToTime As New TimeSpan(0, .DefaultStudyPermissionToTime, 0)
            Me.RadTPtoTime.SelectedDate = Now.Date + DefaultStudyPermissionToTime

            rmtFlexibileTime.Text = CtlCommon.GetFullTimeString(.DefaultStudyPermissionFlexibleTime)
        End With

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
        'Me.RadTPtoTime.SelectedDate = Now




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

        ArcivingMonths_DateValidation()
    End Sub

    Private Sub DisableEnableTimeView(ByVal status As Boolean)

        ' Enable or disable time view
        RadTPfromTime.Enabled = status
        RadTPtoTime.Enabled = status
        reqFromtime.Enabled = status
        reqToTime.Enabled = status
        CustomValidator1.Enabled = status
        CustomValidator2.Enabled = status

    End Sub

    Sub ManageControls(ByVal Status As Boolean)

        ' Get Values from the combo boxes
        'RadCmbEmployee.Enabled = Status
        txtRemarks.Enabled = Status
        ' Get values from rad controls
        dtpStartDatePerm.Enabled = Status
        dtpEndDatePerm.Enabled = Status
        RadTPfromTime.Enabled = Status
        RadTPtoTime.Enabled = Status
        dtpStartDatePerm.Enabled = Status
        dtpEndDatePerm.Enabled = Status
        radcmbxUnversity.Enabled = Status
        txtStudyYear.Enabled = Status
        txtSemester.Enabled = Status
        radcmbxSemester.Enabled = Status
        rblEmp_GPAType.Enabled = Status
        txtEmp_GPA.Enabled = Status
    End Sub

    Sub RefreshControls(ByVal status As Boolean)
        ManageControls(True)
        btnSave.Text = GetLocalResourceObject("btnSaveResource1.Text")
        ' Toggle the status of the buttons
        btnDelete.Visible = status
        btnCancel.Visible = status
        btnSave.Visible = status
    End Sub

    Private Sub FillObjectData()
        With objEmp_Study_PermissionRequest
            Dim days As String = String.Empty
            For i As Integer = 0 To chkWeekDays.Items.Count - 1
                If (chkWeekDays.Items(i).Selected) Then
                    days &= chkWeekDays.Items(i).Value & ","
                End If
            Next
            .Days = days
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FK_PermId = -1
            .IsDividable = False
            .RejectionReason = String.Empty
            .IsFullDay = False
            .IsSpecificDays = True
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
            If rdlTimeOption.SelectedValue = 0 Then
                .FromTime = RadTPfromTime.SelectedDate
                .ToTime = RadTPtoTime.SelectedDate
            Else
                .FromTime = Date.Now
                .ToTime = Date.Now
                .IsFlexible = rdlTimeOption.SelectedValue
                Dim strFlexibileDuration As String = (CInt(rmtFlexibileTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtFlexibileTime.TextWithLiterals.Split(":")(1))
                .FlexibilePermissionDuration = Integer.Parse(strFlexibileDuration)

            End If

            .CREATED_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID

            If txtStudyYear.Text <> "" Then
                .StudyYear = txtStudyYear.Text.Trim()
            Else
                .StudyYear = 0
            End If


            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            If objAPP_Settings.EnableSemesterSelection_StudyPermission = True Then
                dvSemesterSelection.Visible = True
                dvSemesterText.Visible = False
                If Not radcmbxSemester.SelectedValue = -1 Then
                    .Semester = radcmbxSemester.Text
                Else
                    .Semester = String.Empty
                End If
            Else
                dvSemesterSelection.Visible = False
                dvSemesterText.Visible = True
                If txtSemester.Text <> "" Then

                    .Semester = txtSemester.Text
                Else

                    .Semester = String.Empty
                End If
            End If

            If objAPP_Settings.EnableUniversitySelection_StudyPermission = True Then
                If Not radcmbxUnversity.SelectedValue = -1 Then
                    .FK_UniversityId = radcmbxUnversity.SelectedValue
                Else
                    .FK_UniversityId = Nothing
                End If
            Else
                .FK_UniversityId = Nothing
            End If

            .Emp_GPAType = rblEmp_GPAType.SelectedValue

            If Not txtEmp_GPA.Text = String.Empty Then
                .Emp_GPA = Convert.ToDecimal(txtEmp_GPA.Text)
            Else
                .Emp_GPA = Nothing
            End If

            If Not radcmbxMajor.SelectedValue = -1 Then
                .FK_MajorId = radcmbxMajor.SelectedValue
            Else
                .FK_MajorId = Nothing
            End If
            If objAPP_Settings.EnableMajorSelection_StudyPermission = True Then
                If Not radcmbxSpecialization Is Nothing Then
                    If Not radcmbxSpecialization.SelectedValue = -1 Then
                        .FK_SpecializationId = radcmbxSpecialization.SelectedValue
                    Else
                        .FK_SpecializationId = Nothing
                    End If
                Else
                    .FK_SpecializationId = Nothing
                End If
            Else
                .FK_SpecializationId = Nothing
            End If


        End With
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

    Private Function ValidatePermissionWorkingTime() As Boolean
        objEmp_WorkSchedule = New Emp_WorkSchedule()
        objWorkSchedule = New WorkSchedule()

        Dim ScheduleDate As DateTime
        Dim intWorkScheduleType As Integer
        Dim intWorkScheduleId As Integer
        Dim dtEmpWorkSchedule As DataTable
        Dim found As Boolean = False
        Dim objApp_Settings As New APP_Settings
        Dim returnShiftResult As Boolean = False

        objWorkSchedule.EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        ScheduleDate = Now
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
        With objApp_Settings
            .GetByPK()

            If Not .StudyPermissionSchedule = 0 Then
                intWorkScheduleId = .StudyPermissionSchedule
                objWorkSchedule = New WorkSchedule()
                With objWorkSchedule
                    .ScheduleId = objApp_Settings.StudyPermissionSchedule
                    .GetByPK()
                    intWorkScheduleType = .ScheduleType
                End With
            End If


        End With
        If intWorkScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Normal) Then
            objWorkSchedule_NormalTime = New WorkSchedule_NormalTime()
            objWorkSchedule_NormalTime.FK_ScheduleId = intWorkScheduleId
            dtEmpWorkSchedule = objWorkSchedule_NormalTime.GetAll()
            Dim DayId As Integer
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
                            If Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm") > RadTPfromTime.SelectedDate.Value.ToString("HH:mm") Or
                                Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm") < RadTPtoTime.SelectedDate.Value.ToString("HH:mm") Then



                                returnStudyValue = False
                            Else
                                returnStudyValue = True
                            End If

                        End If
                    End If

                End If
            Next

            Return returnStudyValue
        ElseIf intWorkScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Flexible) Then
            objWorkSchedule_Flexible = New WorkSchedule_Flexible()
            objWorkSchedule_Flexible.FK_ScheduleId = intWorkScheduleId
            dtEmpWorkSchedule = objWorkSchedule_Flexible.GetAll()

            Dim dtDuration1 As Integer = (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration1").ToString().Insert(2, ":")).Hour * 60) +
                                            (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration1").ToString().Insert(2, ":")).Minute)
            Dim dtDuration2 As Integer = (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration2").ToString().Insert(2, ":")).Hour * 60) +
                                            (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration2").ToString().Insert(2, ":")).Minute)

            Dim DayId As Integer
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

        ElseIf intWorkScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Advance) Then
            Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
            Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate
            Dim dtShiftSchedule As DataTable
            While dteStartDate < dteEndDate
                objEmp_Shifts = New Emp_Shifts
                objEmp_Shifts.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
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

        ElseIf intWorkScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Flexible) Then
            objWorkSchedule_Flexible = New WorkSchedule_Flexible()
            objWorkSchedule_Flexible.FK_ScheduleId = intWorkScheduleId
            dtEmpWorkSchedule = objWorkSchedule_Flexible.GetAll()
            Dim DayId As Integer
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
        Return True
    End Function

    Private Sub HideShowViews()
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
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

    Private Sub ShowHideControls()
        objAPP_Settings = New APP_Settings

        With objAPP_Settings
            .GetByPK()
            IsFirstGrid = .IsFirstGrid
            If Not .HasFlexiblePermission Then
                rdlTimeOption.Visible = False
            End If
            If SessionVariables.CultureInfo = "en-US" Then
                lblGeneralGuide.Text = .StudyGeneralGuide
            Else
                lblGeneralGuide.Text = .StudyGeneralGuideAr
            End If
        End With

    End Sub

    Private Sub ClearAll()
        dtpStartDatePerm.SelectedDate = Date.Today
        dtpEndDatePerm.SelectedDate = Date.Today
        chkWeekDays.ClearSelection()
        rdlTimeOption.SelectedValue = 0
        ShowHideControls()
        HideShowControl()
        trFlixibleTime.Style("display") = "none"
        trSpecificTime.Style("display") = "block"
        lblPeriodInterval.Visible = True
        txtTimeDifference.Visible = True
        SetRadDateTimePickerPeoperties()
        txtTimeDifference.Text = String.Empty
        txtStudyYear.Text = String.Empty
        txtSemester.Text = String.Empty
        txtRemarks.Text = String.Empty
        radcmbxSemester.SelectedValue = -1

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

        txtEmp_GPA.MaxValue = 100
        txtEmp_GPA.MinValue = 0
        txtEmp_GPA.NumberFormat.DecimalDigits = 0

        dtpFromDateSearch.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
        Dim dd As New Date
        dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
        dtpToDateSearch.SelectedDate = dd

        ' chkWeekDays.Enabled = False
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

    Private Sub FillSemesters()
        objSemesters = New Semesters
        With objSemesters
            ProjectCommon.FillRadComboBox(radcmbxSemester, .GetAll, "SemesterName", "SemesterArabicName")
        End With
    End Sub

    Private Sub FillUnversities()
        objEmp_University = New Emp_University
        With objEmp_University
            ProjectCommon.FillRadComboBox(radcmbxUnversity, .GetAll, "UniversityName", "UniversityArabicName")
        End With
    End Sub

    Private Sub FillStudyMajors()
        objStudyMajors = New StudyMajors
        With objStudyMajors
            ProjectCommon.FillRadComboBox(radcmbxMajor, .GetAll, "MajorName", "MajorArabicName")
        End With
    End Sub

    Private Sub FillStudySpecialization()
        objStudySpecialization = New StudySpecialization
        With objStudySpecialization
            .FK_MajorId = radcmbxMajor.SelectedValue
            ProjectCommon.FillRadComboBox(radcmbxSpecialization, .GetAll_Inner, "SpecializationName", "SpecializationArabicName")
        End With
    End Sub


#End Region


End Class
