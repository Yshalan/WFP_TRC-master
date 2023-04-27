Imports Telerik.Web.UI
Imports SmartV.UTILITIES
Imports TA.DailyTasks
Imports System.Data
Imports SmartV.UTILITIES.ProjectCommon
Imports System.IO
Imports TA.Employees
Imports TA.Definitions
Imports TA.Admin
Partial Class DailyTasks_UserControls_HR_PermissionApproval
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private objHR_PermissionRequest As HR_PermissionRequest
    Private objEmp_Permissions As Emp_Permissions
    Private objPermissionsTypes As PermissionsTypes
    Private objRECALC_REQUEST As RECALC_REQUEST
    Dim objAPP_Settings As APP_Settings
    Private objEmployee As Employee
    Dim objWeekDays As New WeekDays
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Public MsgLang As String
    Public strLang As String
    Dim babyBirthDate As DateTime
    Private objRecalculateRequest As RecalculateRequest

#End Region
  
#Region "Properties"

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

    Public Property PermissionRequestId() As Integer
        Get
            Return ViewState("PermissionRequestId")
        End Get
        Set(ByVal value As Integer)
            ViewState("PermissionRequestId") = value
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


    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
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

    Public Property FileName() As String
        Get
            Return ViewState("FileName")
        End Get
        Set(ByVal value As String)
            ViewState("FileName") = value
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

    Public Property OffAndHolidayDays() As Integer
        Get
            Return ViewState("OffAndHolidayDays")
        End Get
        Set(ByVal value As Integer)
            ViewState("OffAndHolidayDays") = value
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

    Public Property PermissionId() As Integer
        Get
            Return ViewState("PersmissionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("PersmissionId") = value
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

    Public Property PermissionType() As PermissionOption
        Get
            Return ViewState("PermissionType")
        End Get
        Set(ByVal value As PermissionOption)
            ViewState("PermissionType") = value
        End Set
    End Property



#End Region
    
#Region "Page Events"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            'HideShowControl()
            ManageControls(False)
        End If
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If

        CtlCommon.FillCheckBox(chkWeekDays, objWeekDays.GetAll())

        For i As Integer = 0 To chkWeekDays.Items.Count - 1
            chkWeekDays.Items(i).Text = String.Format("{0} {1}", " ", chkWeekDays.Items(i).Text)
        Next
    End Sub

    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PermissionRequestId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("PermissionRequestId").Text)
        AcceptPermissionRequest()
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

    Protected Sub btnAccept_Click(sender As Object, e As System.EventArgs) Handles btnAccept.Click
        AcceptPermissionRequest()
    End Sub

    Protected Sub btnReject_Click(sender As Object, e As System.EventArgs) Handles btnReject.Click
        RejectPermissionRequest()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("../DailyTasks/HR_PermissionApproval.aspx")
    End Sub

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

#End Region

#Region "Methods"

    Private Sub RejectPermissionRequest()
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        objHR_PermissionRequest = New HR_PermissionRequest
        objHR_PermissionRequest.PermissionRequestId = PermissionRequestId
        objHR_PermissionRequest.IsRejected = True
        Err = objHR_PermissionRequest.Update_HR_Permission_RequestStatus
        If Err = 0 Then
            Response.Redirect("../DailyTasks/HR_PermissionApproval.aspx")
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
        End If
    End Sub

    Function AcceptPermissionRequest() As Integer
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim errNo As Integer
        objHR_PermissionRequest = New HR_PermissionRequest()
        objEmp_Permissions = New Emp_Permissions
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

        objHR_PermissionRequest = New HR_PermissionRequest
        objHR_PermissionRequest.PermissionRequestId = PermissionRequestId
        objHR_PermissionRequest.GetByPK()

        If ((objHR_PermissionRequest.PermissionOption = PermissionOption.Nursing)) Then
            tpFromTime = CType(Nothing, DateTime?)
            tpToTime = CType(Nothing, DateTime?)
            strFlexibileDuration = objHR_PermissionRequest.FlexibilePermissionDuration
            isFlixible = True
        Else
            tpFromTime = objHR_PermissionRequest.FromTime
            tpToTime = objHR_PermissionRequest.ToTime
            strFlexibileDuration = CtlCommon.GetFullTimeString(objHR_PermissionRequest.FlexibilePermissionDuration)
        End If

        If objHR_PermissionRequest.IsForPeriod = False Then
            If objHR_PermissionRequest.PermEndDate Is Nothing Then
                objHR_PermissionRequest.PermEndDate = objHR_PermissionRequest.PermDate
            End If
        End If


        If objHR_PermissionRequest.PermissionOption = PermissionOption.Normal Then
            If objHR_PermissionRequest.IsFlexible = False Then
                If (objHR_PermissionRequest.ValidateEmployeePermission(objHR_PermissionRequest.FK_EmployeeId, PermissionRequestId, dtCurrent, objHR_PermissionRequest.IsForPeriod, objHR_PermissionRequest.PermDate, objHR_PermissionRequest.PermEndDate, tpFromTime, _
                                                                 tpToTime, objHR_PermissionRequest.PermDate, objPermissionsTypes, objHR_PermissionRequest.FK_PermId, objHR_PermissionRequest.IsFullDay, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
                    If (ErrorMessage <> String.Empty) Then
                        CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                        Return -1
                    End If
                End If
            End If
        End If
        Dim days As String = String.Empty

        If objHR_PermissionRequest.IsForPeriod = False Then
            objHR_PermissionRequest.PermEndDate = Date.MinValue
        End If

        If (objHR_PermissionRequest.IsFullDay = True) Then
            EmpLeaveTotalBalance = EmpLeaveTotalBalance - 1
        End If

        objEmp_Permissions.Days = objHR_PermissionRequest.Days

        ' Set data into object for Add / UpdateIf

        objHR_PermissionRequest.PermissionRequestId = PermissionRequestId
        objHR_PermissionRequest.LeaveId = LeaveID

        Dim fileUploadExtension As String = objHR_PermissionRequest.AttachedFile
        If String.IsNullOrEmpty(fileUploadExtension) Then
            fileUploadExtension = FileExtension
        End If

        With objHR_PermissionRequest
            .FK_EmployeeId = objHR_PermissionRequest.FK_EmployeeId


        End With

        If Not objHR_PermissionRequest.PermissionOption = PermissionOption.Nursing Then
            AllowedTime = Nothing
        End If

        objEmp_Permissions.AddPermAllProcess(objHR_PermissionRequest.FK_EmployeeId, objHR_PermissionRequest.FK_PermId, objHR_PermissionRequest.IsDividable, isFlixible, objHR_PermissionRequest.IsSpecificDays, _
                                      objHR_PermissionRequest.Remark, objHR_PermissionRequest.PermDate, objHR_PermissionRequest.PermEndDate, objHR_PermissionRequest.IsForPeriod, tpFromTime, _
                                       tpToTime, objHR_PermissionRequest.PermDate, objHR_PermissionRequest.Days, OffAndHolidayDays, days, EmpLeaveTotalBalance, LeaveID, _
                                       Integer.Parse(objHR_PermissionRequest.PermissionOption), Integer.Parse(strFlexibileDuration), fileUploadExtension, objHR_PermissionRequest.IsFullDay, AllowedTime, ErrorMessage)

        showResultMessage(Me.Page, ErrorMessage)
        If errNo = 0 Then
            If PermissionId = 0 Then
                If fileUploadExtension IsNot Nothing Then
                    Dim extention As String = objHR_PermissionRequest.AttachedFile
                    Dim fileName As String = objEmp_Permissions.PermissionId.ToString()
                    Dim fPath As String = String.Empty
                    fPath = Server.MapPath("..\PermissionFiles\\" + fileName + extention)
                    If File.Exists(fPath) Then
                        File.Delete(fPath)
                        Rename(fPath, fPath)

                    End If

                End If
            Else
                If fileUploadExtension IsNot Nothing Then
                    Dim fileName As String = objEmp_Permissions.PermissionId.ToString()
                    Dim fPath As String = String.Empty
                    fPath = Server.MapPath("..\PermissionFiles\\" + fileName + FileExtension)
                    If File.Exists(fPath) Then
                        Dim extention As String = objHR_PermissionRequest.AttachedFile
                        File.Delete(fPath)
                        fPath = Server.MapPath("..\PermissionFiles\\" + fileName + extention)
                        If File.Exists(fPath) Then
                            File.Delete(fPath)
                            Rename(fPath, fPath)
                        End If
                    Else
                        Dim extention As String = objHR_PermissionRequest.AttachedFile
                        Dim fileNameExist As String = objEmp_Permissions.PermissionId.ToString()
                        Dim fPathExist As String = String.Empty
                        fPathExist = Server.MapPath("..\PermissionFiles\\" + fileNameExist + extention)
                        If File.Exists(fPath) Then
                            File.Delete(fPath)
                            Rename(fPath, fPath)
                        End If
                    End If
                End If
            End If

            objRECALC_REQUEST = New RECALC_REQUEST
            objAPP_Settings = New APP_Settings()
            objAPP_Settings = objAPP_Settings.GetByPK()

            If objAPP_Settings.ApprovalRecalMethod = 1 Then
                If radBtnOneDay.Checked AndAlso objHR_PermissionRequest.PermissionOption = PermissionOption.Normal Then
                    temp_date = objHR_PermissionRequest.PermDate
                    temp_str_date = DateToString(temp_date)
                    objRECALC_REQUEST.EMP_NO = objHR_PermissionRequest.FK_EmployeeId
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

                    Dim dteFrom As DateTime = objHR_PermissionRequest.PermDate
                    Dim dteTo As DateTime = objHR_PermissionRequest.PermEndDate

                    While dteFrom <= dteTo
                        If Not dteFrom = Date.Now.AddDays(1).ToShortDateString() Then
                            temp_str_date = DateToString(dteFrom)
                            objRECALC_REQUEST = New RECALC_REQUEST()
                            objRECALC_REQUEST.EMP_NO = objHR_PermissionRequest.FK_EmployeeId
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
                    temp_date = dtpPermissionDate.SelectedDate
                    temp_str_date = DateToString(temp_date)
                    objRECALC_REQUEST.EMP_NO = objHR_PermissionRequest.FK_EmployeeId
                    objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)

                    objRecalculateRequest = New RecalculateRequest
                    With objRecalculateRequest
                        .Fk_EmployeeId = objHR_PermissionRequest.FK_EmployeeId
                        .FromDate = temp_date
                        .ToDate = temp_date
                        .ImmediatelyStart = True
                        .RecalStatus = 0
                        .CREATED_BY = SessionVariables.LoginUser.ID
                        .Remarks = "HR Permission Approval - SYSTEM"
                        err2 = .Add
                    End With

                Else
                    Dim dteFrom As DateTime = dtpStartDatePerm.SelectedDate
                    Dim dteTo As DateTime = dtpEndDatePerm.SelectedDate
                    If Not dteFrom > Date.Today Then
                        objRecalculateRequest = New RecalculateRequest
                        With objRecalculateRequest
                            .Fk_EmployeeId = objHR_PermissionRequest.FK_EmployeeId
                            .FromDate = dteFrom
                            .ToDate = dteTo
                            .ImmediatelyStart = True
                            .RecalStatus = 0
                            .CREATED_BY = SessionVariables.LoginUser.ID

                            If PermissionType = PermissionOption.Nursing Then
                                .Remarks = "HR Nursing Permission Approval - SYSTEM"
                            ElseIf PermissionType = PermissionOption.Study Then
                                .Remarks = "HR Study Permission Approval - SYSTEM"
                            End If

                            err2 = .Add
                        End With
                    End If
                End If
            End If


            Response.Redirect("../DailyTasks/HR_PermissionApproval.aspx")


        End If

        'HideShowControl()

    End Function

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

    'Private Sub HideShowControl()
    '    Dim browser As String = Request.Browser.Type
    '    Select Case PermissionType
    '        Case PermissionOption.Normal
    '            If (browser.Contains("Chrome") Or browser.Contains("InternetExplorer")) Then
    '                If SessionVariables.CultureInfo = "en-US" Then
    '                    tdOption.Style("margin-right") = "-455px"
    '                Else
    '                    tdOption.Style("margin-left") = "-310px"
    '                End If
    '            Else
    '                If (browser.Contains("IE")) Then
    '                    If SessionVariables.CultureInfo = "en-US" Then
    '                        tdOption.Style("margin-right") = "200px"
    '                    Else
    '                        tdOption.Style("margin-left") = "300px"
    '                    End If


    '                Else
    '                    If SessionVariables.CultureInfo = "en-US" Then
    '                        tdOption.Style("margin-right") = "-440px"
    '                    Else
    '                        tdOption.Style("margin-left") = "-310px"
    '                    End If
    '                End If

    '            End If
    '            trWeekDays.Style("display") = "none"
    '            trType.Style("display") = "block"
    '            trNursingFlexibleDurationPermission.Visible = False
    '            trDifTime.Style("width") = "135px"
    '            If SessionVariables.CultureInfo = "en-US" Then
    '                tdWeekDays.Style("margin-right") = "-470px"
    '                tdWeekDays.Style("float") = "right"
    '                tdOption.Style("float") = "right"

    '            Else
    '                tdWeekDays.Style("margin-left") = "-390px"
    '                tdWeekDays.Style("float") = "left"
    '                tdOption.Style("float") = "left"

    '            End If

    '            ''FadiH::Edited Full Day from App_Settings
    '            objAPP_Settings = New APP_Settings()
    '            objAPP_Settings = objAPP_Settings.GetByPK()
    '            If objAPP_Settings.HasFullDayPermission Then
    '                trFullyDay.Visible = True
    '            Else
    '                trFullyDay.Visible = False
    '            End If
    '            ' trTime.Style("display") = "block"
    '        Case PermissionOption.Nursing
    '            If (browser.Contains("InternetExplorer")) Then
    '                If SessionVariables.CultureInfo = "en-US" Then
    '                    trDateFromTo.Style("padding-left") = "10px"
    '                    trAttachedFile.Style("padding-left") = "10px"
    '                    trRemarks.Style("padding-left") = "10px"
    '                    trNursingFlexibleDurationPermission.Style("padding-left") = "10px"
    '                    tdNursingFlexibleDurationPermission.Style("padding-left") = "10px"
    '                Else
    '                    trDateFromTo.Style("padding-right") = "10px"
    '                    trAttachedFile.Style("padding-right") = "10px"
    '                    trRemarks.Style("padding-right") = "10px"
    '                    trNursingFlexibleDurationPermission.Style("padding-right") = "10px"
    '                    tdNursingFlexibleDurationPermission.Style("padding-right") = "10px"
    '                End If
    '                tdDate.Style("width") = "125px"
    '            Else
    '                If (browser.Contains("IE")) Then
    '                    If SessionVariables.CultureInfo = "en-US" Then
    '                        trDateFromTo.Style("padding-left") = "10px"
    '                        trAttachedFile.Style("padding-left") = "10px"
    '                        trRemarks.Style("padding-left") = "10px"
    '                        trNursingFlexibleDurationPermission.Style("padding-left") = "10px"
    '                        tdNursingFlexibleDurationPermission.Style("padding-left") = "10px"
    '                    Else
    '                        trDateFromTo.Style("padding-right") = "10px"
    '                        trAttachedFile.Style("padding-right") = "10px"
    '                        trRemarks.Style("padding-right") = "10px"
    '                        trNursingFlexibleDurationPermission.Style("padding-right") = "10px"
    '                        tdNursingFlexibleDurationPermission.Style("padding-right") = "10px"
    '                    End If
    '                Else
    '                    If SessionVariables.CultureInfo = "en-US" Then
    '                        trDateFromTo.Style("padding-left") = "10px"
    '                        trAttachedFile.Style("padding-left") = "10px"
    '                        trRemarks.Style("padding-left") = "10px"
    '                        trNursingFlexibleDurationPermission.Style("padding-left") = "10px"
    '                        tdNursingFlexibleDurationPermission.Style("padding-left") = "10px"
    '                    Else
    '                        trDateFromTo.Style("padding-right") = "10px"
    '                        trAttachedFile.Style("padding-right") = "10px"
    '                        trRemarks.Style("padding-right") = "10px"
    '                        trNursingFlexibleDurationPermission.Style("padding-right") = "10px"
    '                        tdNursingFlexibleDurationPermission.Style("padding-right") = "10px"
    '                    End If
    '                End If
    '            End If
    '            radBtnPeriod.Checked = True
    '            trWeekDays.Style("display") = "none"
    '            trTime.Style("display") = "none"
    '            trDifTime.Style("display") = "none"
    '            trPermType.Style("display") = "none"
    '            trType.Style("display") = "none"
    '            trNursingFlexibleDurationPermission.Visible = True
    '            'trDateFromTo.Style("padding-left") = "25px"


    '            tdDate.Style("padding-left") = "10px"
    '            PnlOneDayLeave.Visible = False
    '            pnlPeriodLeave.Visible = True
    '            reqPermission.Enabled = False
    '            ExtenderPermission.Enabled = False
    '            'userCtrlEmpPermHeader.HeaderText = ResourceManager.GetString("EmpNursePerm", CultureInfo)
    '            lblFromDate.Text = ResourceManager.GetString("BirthDate", CultureInfo)
    '            babyBirthDate = dtpStartDatePerm.SelectedDate

    '            ''FadiH::Edited Nursing Days from App_Settings alternate Web config
    '            objAPP_Settings = New APP_Settings()
    '            objAPP_Settings = objAPP_Settings.GetByPK()
    '            Dim nursingDay As Integer = objAPP_Settings.NursingDays
    '            babyBirthDate = babyBirthDate.AddDays(nursingDay)
    '            dtpEndDatePerm.SelectedDate = babyBirthDate
    '            hdnNurdingDay.Value = nursingDay

    '            trFullyDay.Visible = False
    '            reqFromtime.Enabled = False
    '            reqToTime.Enabled = False
    '            ExtenderFromTime.Enabled = False
    '            ExtenderreqToTime.Enabled = False

    '        Case PermissionOption.Study

    '            If (browser.Contains("Chrome")) Then

    '                tdDate.Style("width") = "135px"
    '                If SessionVariables.CultureInfo = "en-US" Then
    '                    tdWeekDays.Style("margin-right") = "-515px"
    '                    trTimeFromTo.Style("padding-left") = "20px"
    '                    trDif.Style("padding-left") = "20px"
    '                    trRemarks.Style("padding-left") = "20px"
    '                    trAttachedFile.Style("padding-left") = "20px"
    '                    trDateFromTo.Style("padding-left") = "20px"
    '                Else
    '                    tdWeekDays.Style("margin-left") = "-420px"
    '                    trTimeFromTo.Style("padding-right") = "20px"
    '                    trDif.Style("padding-right") = "20px"
    '                    trRemarks.Style("padding-right") = "20px"
    '                    trAttachedFile.Style("padding-right") = "20px"
    '                    trDateFromTo.Style("padding-right") = "20px"
    '                    tdWeekDays.Style("margin-right") = "-410px"
    '                End If
    '            Else
    '                If (browser.Contains("InternetExplorer")) Then

    '                    tdDate.Style("width") = "135px"
    '                    'tdDate.Style("padding-left") = "10px"

    '                    If SessionVariables.CultureInfo = "en-US" Then
    '                        tdWeekDays.Style("margin-right") = "-535px"
    '                        trTimeFromTo.Style("padding-left") = "45px"
    '                        trDif.Style("padding-left") = "45px"
    '                        trRemarks.Style("padding-left") = "45px"
    '                        trAttachedFile.Style("padding-left") = "45px"
    '                        trDateFromTo.Style("padding-left") = "45px"
    '                    Else
    '                        tdWeekDays.Style("margin-left") = "-445px"
    '                        trTimeFromTo.Style("padding-right") = "45px"
    '                        trDif.Style("padding-right") = "45px"
    '                        trRemarks.Style("padding-right") = "45px"
    '                        trAttachedFile.Style("padding-right") = "45px"
    '                        trDateFromTo.Style("padding-right") = "45px"
    '                    End If
    '                Else
    '                    If (browser.Contains("IE")) Then
    '                        If SessionVariables.CultureInfo = "en-US" Then

    '                            tdWeekDays.Style("margin-right") = "-50px"
    '                            tdWeekDays.Style("margin-top") = "20px"
    '                            tdWeekDays.Style("width") = "100%"

    '                            trTimeFromTo.Style("padding-left") = "55px"
    '                            trDif.Style("padding-left") = "55px"
    '                            trRemarks.Style("padding-left") = "55px"
    '                            trAttachedFile.Style("padding-left") = "55px"
    '                            trDateFromTo.Style("padding-left") = "55px"
    '                        Else
    '                            tdWeekDays.Style("margin-left") = "-50px"
    '                            tdWeekDays.Style("margin-top") = "15px"
    '                            tdWeekDays.Style("width") = "100%"

    '                            trTimeFromTo.Style("padding-right") = "60px"
    '                            trDif.Style("padding-right") = "62px"
    '                            trRemarks.Style("padding-right") = "62px"
    '                            trAttachedFile.Style("padding-right") = "62px"
    '                            trDateFromTo.Style("padding-right") = "60px"
    '                        End If
    '                    Else
    '                        ''firefox



    '                        If SessionVariables.CultureInfo = "en-US" Then
    '                            tdWeekDays.Style("margin-right") = "-490px"
    '                            trDateFromTo.Style("padding-left") = "12px"
    '                            trTimeFromTo.Style("padding-left") = "22px"
    '                            trDif.Style("padding-left") = "12px"
    '                            trRemarks.Style("padding-left") = "12px"
    '                            trAttachedFile.Style("padding-left") = "12px"
    '                        Else
    '                            tdWeekDays.Style("margin-left") = "-410px"
    '                            trDateFromTo.Style("padding-right") = "12px"
    '                            trTimeFromTo.Style("padding-right") = "12px"
    '                            trDif.Style("padding-right") = "12px"
    '                            trRemarks.Style("padding-right") = "12px"
    '                            trAttachedFile.Style("padding-right") = "12px"
    '                        End If
    '                    End If

    '                End If


    '            End If

    '            radBtnSpecificDays.Checked = True
    '            trWeekDays.Style("display") = "block"
    '            trWeekDays.Style("padding-left") = "0px"
    '            trPermType.Style("display") = "none"
    '            trType.Style("display") = "none"
    '            trNursingFlexibleDurationPermission.Visible = False


    '            'tdDate.Style("padding-left") = "20px"
    '            If SessionVariables.CultureInfo = "en-US" Then
    '                'tdWeekDays.Style("margin-right") = "-550px"
    '                tdWeekDays.Style("float") = "right"
    '            Else
    '                'tdWeekDays.Style("margin-left") = "-423px"
    '                tdWeekDays.Style("float") = "left"
    '            End If
    '            reqPermission.Enabled = False
    '            ExtenderPermission.Enabled = False
    '            trFullyDay.Visible = False
    '            'trTime.Style("display") = "block"
    '            PnlOneDayLeave.Visible = False
    '            pnlPeriodLeave.Visible = True
    '            'userCtrlEmpPermHeader.HeaderText = ResourceManager.GetString("EmpStudyPerm", CultureInfo)
    '    End Select

    'End Sub

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

    Public Function FillControls() As Integer

        objHR_PermissionRequest = New HR_PermissionRequest()
        objHR_PermissionRequest.PermissionRequestId = PermissionRequestId
        FillLists()
        ' If the PermissionId is not a valid one , the below line will through 
        ' an exception
        objHR_PermissionRequest.GetByPK()
        With objHR_PermissionRequest
            ' Fill combo boxes
            'RadCmbEmployee.SelectedValue = .FK_EmployeeId
            EmployeeFilterUC.EmployeeId = .FK_EmployeeId
            EmployeeFilterUC.IsEntityClick = "True"
            EmployeeFilterUC.GetEmployeeInfo(.FK_EmployeeId)

            RadCmpPermissions.SelectedValue = .FK_PermId
            ' Fill checkBoxes
            chckIsDividable.Checked = .IsDividable
            FileExtension = .AttachedFile

            'Dim FilePath As String = Server.MapPath("..\..\PermissionFiles\" + PermissionId.ToString() + .AttachedFile)

            fuAttachFile.Visible = True
            Dim FilePath As String = Server.MapPath("~/PermissionFiles")
            FilePath = FilePath + "\" + PermissionId.ToString() + .AttachedFile
            If File.Exists(FilePath) Then
                lnbLeaveFile.HRef = "..\..\PermissionFiles\" + PermissionId.ToString() + .AttachedFile
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
                trNursingFlexibleDurationPermission.Visible = True
                RadCmbFlixebleDuration.SelectedValue = .FlexibilePermissionDuration
            End If

            txtRemarks.Text = .Remark

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

            '''''''''''''''''''''''''''''''''''''''''''
            ' Enable / Disable time pickers according to the full day 
            ' value
            chckFullDay.Checked = .IsFullDay
            ManageSomeControlsStatus(.IsForPeriod, .PermDate, .PermEndDate, .IsFullDay)
        End With
    End Function

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

    Private Sub ManageSomeControlsStatus(ByVal IsForPeriod As Boolean, _
                                         ByVal PermDate As DateTime, _
                                         ByVal PermEndDate As DateTime?, _
                                         ByVal FullDay As Boolean)
        ' Manage the DatePickers according 
        ' to the permission type
        ShowHide(IsForPeriod)
        radBtnOneDay.Checked = Not IsForPeriod
        radBtnPeriod.Checked = IsForPeriod

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
            DisableEnableTimeView(True)
            rdlTimeOption.Enabled = False
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
        ' Disable all controls in some display modes 
        If DisplayMode.ToString() = "ViewAll" Or DisplayMode.ToString() = "View" Then
            txtTimeDifference.Enabled = False
            RadTPfromTime.Enabled = False
            RadTPtoTime.Enabled = False
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
        rdlTimeOption.Enabled = Status
        fuAttachFile.Enabled = Status
        EmployeeFilterUC.EnabledDisbaledControls(False)
    End Sub

#End Region
   

End Class
