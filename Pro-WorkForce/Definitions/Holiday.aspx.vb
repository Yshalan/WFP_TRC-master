Imports TA.Definitions
Imports System.Data
Imports TA.LookUp
Imports TA.DailyTasks
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports System.Globalization
Imports SmartV.Version
Imports TA.Security
Imports SmartV.UTILITIES.ProjectCommon
Imports TA.Admin

Partial Class Admin_Holiday
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objTAPolicy As TAPolicy
    Private objHoliday_Company As New Holiday_Company
    Private objHoliday_WorkLocation As New Holiday_WorkLocation
    Private objHoliday_Religion As New Holiday_Religion

    Private objHoliday_EmployeeType As New Holiday_EmployeeTypes
    Private objHoliday_LogicalGroup As New Holiday_logicalGroup

    Private objHoliday As New Holiday
    Private DtCompany As DataTable
    Private DtWorkLocation As DataTable
    Private DtReligion As DataTable

    Private DtEmployeeType As DataTable
    Private DtLogicalGroup As DataTable

    Private objVersion As version
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objCommonProject As New ProjectCommon()
    Private Lang As CtlCommon.Lang

    Dim objAPP_Settings As APP_Settings
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objRecalculateRequest As RecalculateRequest

#End Region

#Region "Properties"

    Public Property HolidayID() As Integer
        Get
            Return ViewState("HolidayID")
        End Get
        Set(ByVal value As Integer)
            ViewState("HolidayID") = value
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

#End Region

#Region "Page events"

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
            Else
                Lang = CtlCommon.Lang.EN
            End If

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            rbtnAllCompanies.Checked = True
            rbtnAllReligions.Checked = True

            rbtnAllEmployeeTypes.Checked = True
            rbtnAllLogicalGroup.Checked = True

            If (objVersion.HasMultiCompany() = False) Then
                rbtnSpecificCompany.Visible = False
                rbtnAllCompanies.Text = GetLocalResourceObject("AllWorkLocations")
            End If
            Dim MinDate As New DateTimeOffset(Now.Year, 1, 1, 0, 0, 0, TimeSpan.Zero)
            Dim MaxDate As New DateTimeOffset(Now.Year, 12, 31, 0, 0, 0, TimeSpan.Zero)
            dteFromDay.MinDate = MinDate.Date
            dteFromDay.MaxDate = MaxDate.Date
            dteToDay.MinDate = MinDate.Date
            dteToDay.MaxDate = MaxDate.Date

            If Lang = CtlCommon.Lang.AR Then
                dteFromDay.Culture = New CultureInfo("ar-EG", False)
                dteToDay.Culture = New CultureInfo("ar-EG", False)
            Else
                dteFromDay.Culture = New CultureInfo("en-US", False)
                dteToDay.Culture = New CultureInfo("en-US", False)
            End If

            CtlCommon.FillCheckBox(chkLstCompany, objHoliday_Company.GetAll_OrgCompany)
            CtlCommon.FillCheckBox(chkLstWorkLocation, objHoliday_WorkLocation.GetAll_WorkLocations())
            CtlCommon.FillCheckBox(chkLstReligion, objHoliday_Religion.GetAll_Religion)
            CtlCommon.FillCheckBox(chkLstEmployeeType, objHoliday_EmployeeType.GetAll_EmployeeType())
            CtlCommon.FillCheckBox(chkLstLogicalGroup, objHoliday_LogicalGroup.GetAll_LogicalGroup())

            FillGrid()
            PageHeader1.HeaderText = ResourceManager.GetString("DefHoliday", CultureInfo)
        End If
        ShowControls()
        ibtnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdHoliday.ClientID + "')")
        'chkComApplicable.Attributes.Add("OnClick", "javascript:return showCompany()")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not trControls.FindControl(row("AddBtnName")) Is Nothing Then
                        trControls.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not trControls.FindControl(row("DeleteBtnName")) Is Nothing Then
                        trControls.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not trControls.FindControl(row("EditBtnName")) Is Nothing Then
                        trControls.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not trControls.FindControl(row("PrintBtnName")) Is Nothing Then
                        trControls.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

        If (rbtnSpecificWorkLocation.Checked) Then
            Dim flagWorkloc As Boolean = False
            For Each lst As ListItem In chkLstWorkLocation.Items
                If lst.Selected Then
                    flagWorkloc = True
                    Exit For
                End If
            Next

            If Not flagWorkloc Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("WorkLocationSelect", CultureInfo), "info")
                Return
            End If
        End If


        If rbtnSpecificReligions.Checked Then
            Dim flagReligion As Boolean = False
            For Each lst As ListItem In chkLstReligion.Items
                If lst.Selected Then
                    flagReligion = True
                    Exit For
                End If
            Next

            If Not flagReligion Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ReligionSelect", CultureInfo), "info")
                Return
            End If
        End If

        If rbtnSpecificEmployeeTypes.Checked Then
            Dim flagEmployeeType As Boolean = False
            For Each lst As ListItem In chkLstEmployeeType.Items
                If lst.Selected Then
                    flagEmployeeType = True
                    Exit For
                End If
            Next

            If Not flagEmployeeType Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmployeeTypeSelect", CultureInfo), "info")
                Return
            End If
        End If

        If rbtnSpecificLogicalGroup.Checked Then
            Dim flagLogicalGroupType As Boolean = False
            For Each lst As ListItem In chkLstLogicalGroup.Items
                If lst.Selected Then
                    flagLogicalGroupType = True
                    Exit For
                End If
            Next

            If Not flagLogicalGroupType Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("LogicalGroupSelect", CultureInfo), "info")
                Return
            End If
        End If



        If rbtnSpecificCompany.Checked Then
            Dim flagCompany As Boolean = False
            For Each lst As ListItem In chkLstCompany.Items
                If lst.Selected Then
                    flagCompany = True
                    Exit For
                End If
            Next

            If Not flagCompany Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SelectCompany", CultureInfo), "info")
                Return
            End If
        End If

        Dim err As Integer = -1
        Dim err2 As Integer = -1
        With objHoliday
            .HolidayName = txtHolidayEnglish.Text
            .HolidayArabicName = txtHolidayArabic.Text
            If (rbtnAllCompanies.Checked) Then
                .IsCompanyApplicable = False
            ElseIf (rbtnSpecificCompany.Checked) Then

                .IsCompanyApplicable = True
            Else
                .IsCompanyApplicable = False
            End If

            If (rbtnAllReligions.Checked) Then
                .IsReligion = False
            Else
                .IsReligion = True
            End If

            If (rbtnAllEmployeeTypes.Checked) Then
                .IsEmployeeType = False
            Else
                .IsEmployeeType = True
            End If


            If (rbtnSpecificWorkLocation.Checked) Then
                .IsWorkLocation = True
            Else
                .IsWorkLocation = False
            End If

            If (rbtnAllLogicalGroup.Checked) Then
                .IsLogicalGroup = False
            Else
                .IsLogicalGroup = True
            End If

            .isYearlyFixed = chkYearlyFixed.Checked
            If chkYearlyFixed.Checked Then
                .StartDay = dteFromDay.SelectedDate.Value.Day
                .StartMonth = dteFromDay.SelectedDate.Value.Month
                .StartYear = dteFromDay.SelectedDate.Value.Year
                .EndDay = dteToDay.SelectedDate.Value.Day
                .EndMonth = dteToDay.SelectedDate.Value.Month
                .EndYear = dteToDay.SelectedDate.Value.Year
            Else
                .StartDay = dteFromDate.SelectedDate.Value.Day
                .StartMonth = dteFromDate.SelectedDate.Value.Month
                .StartYear = dteFromDate.SelectedDate.Value.Year
                .EndDay = dteToDate.SelectedDate.Value.Day
                .EndMonth = dteToDate.SelectedDate.Value.Month
                .EndYear = dteToDate.SelectedDate.Value.Year
            End If

            .IsReligionRelated = chkIsReligionRelated.Checked

            If HolidayID = 0 Then
                '.CREATED_BY = SessionVariables.Sys_LoginUser.ID
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add()
                HolidayID = .HolidayId
            Else
                .HolidayId = HolidayID
                '.LAST_UPDATE_BY = SessionVariables.Sys_LoginUser.ID
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                err = .Update()
                If err = 0 Then
                    objHoliday_Company.HolidayId = HolidayID
                    objHoliday_Company.Delete()
                    objHoliday_Religion.HolidayId = HolidayID
                    objHoliday_Religion.Delete()

                    objHoliday_EmployeeType.FK_HolidayId = HolidayID
                    objHoliday_EmployeeType.Delete()

                    objHoliday_LogicalGroup.FK_HolidayId = HolidayID
                    objHoliday_LogicalGroup.Delete()

                    objHoliday_WorkLocation.HolidayId = HolidayID
                    objHoliday_WorkLocation.Delete()
                End If
            End If
        End With
        If err = 0 Then
            If rbtnSpecificCompany.Checked Then
                For Each lst As ListItem In chkLstCompany.Items
                    If lst.Selected Then
                        With objHoliday_Company
                            .CompanyId = CInt(lst.Value)
                            .HolidayId = HolidayID
                            .Add()
                        End With
                    End If

                Next
            End If
            If rbtnSpecificWorkLocation.Checked Then
                For Each lst As ListItem In chkLstWorkLocation.Items
                    If lst.Selected Then
                        With objHoliday_WorkLocation
                            .WorkLocationId = CInt(lst.Value)
                            .HolidayId = HolidayID
                            .Add()
                        End With
                    End If

                Next
            End If
            If rbtnSpecificReligions.Checked Then
                For Each lst As ListItem In chkLstReligion.Items
                    If lst.Selected Then
                        With objHoliday_Religion
                            .ReligionId = CInt(lst.Value)
                            .HolidayId = HolidayID
                            .Add()
                        End With
                    End If

                Next
            End If

            If rbtnSpecificEmployeeTypes.Checked Then
                For Each lst As ListItem In chkLstEmployeeType.Items
                    If lst.Selected Then
                        With objHoliday_EmployeeType
                            .FK_EmployeeTypeId = CInt(lst.Value)
                            .FK_HolidayId = HolidayID
                            .Add()
                        End With
                    End If

                Next
            End If

            If rbtnSpecificLogicalGroup.Checked Then
                For Each lst As ListItem In chkLstLogicalGroup.Items
                    If lst.Selected Then
                        With objHoliday_LogicalGroup
                            .FK_LogicalGroupId = CInt(lst.Value)
                            .FK_HolidayId = HolidayID
                            .Add()
                        End With
                    End If

                Next
            End If

            If chkYearlyFixed.Checked = False Then
                If dteFromDate.DbSelectedDate < Date.Today Then
                    objRECALC_REQUEST = New RECALC_REQUEST
                    objAPP_Settings = New APP_Settings()
                    objAPP_Settings = objAPP_Settings.GetByPK()

                    If objAPP_Settings.ApprovalRecalMethod = 2 Then
                        If rbtnSpecificCompany.Checked = True Then
                            For Each lst As ListItem In chkLstCompany.Items
                                If lst.Selected Then
                                    objRecalculateRequest = New RecalculateRequest
                                    With objRecalculateRequest
                                        .Fk_CompanyId = CInt(lst.Value)
                                        .FromDate = dteFromDate.SelectedDate
                                        If dteFromDate.DbSelectedDate > Date.Today Then
                                            .ToDate = Date.Today
                                        Else
                                            .ToDate = dteToDate.DbSelectedDate
                                        End If
                                        .ImmediatelyStart = True
                                        .RecalStatus = 0
                                        .CREATED_BY = SessionVariables.LoginUser.ID
                                        .Remarks = "Holiday Company - SYSTEM"
                                        err2 = .Add
                                    End With
                                End If
                            Next
                        End If

                        If rbtnSpecificWorkLocation.Checked = True Then
                            For Each lst As ListItem In chkLstWorkLocation.Items
                                If lst.Selected Then
                                    objRecalculateRequest = New RecalculateRequest
                                    With objRecalculateRequest
                                        .FK_WorkLocation = CInt(lst.Value)
                                        .FromDate = dteFromDate.SelectedDate
                                        If dteFromDate.DbSelectedDate > Date.Today Then
                                            .ToDate = Date.Today
                                        Else
                                            .ToDate = dteToDate.DbSelectedDate
                                        End If
                                        .ImmediatelyStart = True
                                        .RecalStatus = 0
                                        .CREATED_BY = SessionVariables.LoginUser.ID
                                        .Remarks = "Holiday WorkLocation - SYSTEM"
                                        err2 = .Add
                                    End With
                                End If
                            Next
                        End If

                        If rbtnSpecificLogicalGroup.Checked = True Then
                            For Each lst As ListItem In chkLstLogicalGroup.Items
                                If lst.Selected Then
                                    objRecalculateRequest = New RecalculateRequest
                                    With objRecalculateRequest
                                        .FK_LogicalGroupId = CInt(lst.Value)
                                        .FromDate = dteFromDate.SelectedDate
                                        If dteFromDate.DbSelectedDate > Date.Today Then
                                            .ToDate = Date.Today
                                        Else
                                            .ToDate = dteToDate.DbSelectedDate
                                        End If
                                        .ImmediatelyStart = True
                                        .RecalStatus = 0
                                        .CREATED_BY = SessionVariables.LoginUser.ID
                                        .Remarks = "Holiday Logical Group - SYSTEM"
                                        err2 = .Add
                                    End With
                                End If
                            Next
                        End If

                        objRecalculateRequest = New RecalculateRequest
                        With objRecalculateRequest

                            .FromDate = dteFromDate.SelectedDate
                            If dteFromDay.DbSelectedDate > Date.Today Then
                                .ToDate = Date.Today
                            Else
                                .ToDate = dteToDate.DbSelectedDate
                            End If
                            .ImmediatelyStart = True
                            .RecalStatus = 0
                            .CREATED_BY = SessionVariables.LoginUser.ID
                            .Remarks = "Holiday - SYSTEM"
                            err2 = .Add
                        End With

                    End If
                End If
            End If

            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGrid()
            ClearAll()
        ElseIf err = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo), "info")
        ElseIf err = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DateExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub ibtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnDelete.Click
        Dim err As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdHoliday.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intHolidayId As Integer = Convert.ToInt32(row.GetDataKeyValue("HolidayId").ToString())
                objHoliday.HolidayId = intHolidayId
                err = objHoliday.Delete()
            End If
        Next
        If err = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGrid()
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If
        ClearAll()
        FillGrid()
    End Sub

    Protected Sub dgdgrdHoliday_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdHoliday.ItemDataBound

        If (e.Item.ItemType = GridItemType.Item) Then

            Dim Itm As GridDataItem = e.Item

            'Dim FromTime1 = DirectCast(Itm.FindControl("rmtFromTime1"), RadMaskedTextBox)
            'Dim ToTime1 = DirectCast(Itm.FindControl("rmtToTime1"), RadMaskedTextBox)



        End If
    End Sub

    Protected Sub ibtnRest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnRest.Click
        ClearAll()
        FillGrid()
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Sub dgrdHoliday_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdHoliday.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdHoliday_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdHoliday.NeedDataSource
        Try
            objHoliday = New Holiday
            Dim dtHoliday = objHoliday.GetAll()
            If dtHoliday IsNot Nothing And dtHoliday.Rows.Count > 0 Then
                dtHoliday.Columns.Add("StartDate")
                dtHoliday.Columns.Add("EndDate")
                dtHoliday.Columns.Add("IsYearly")
                For Each drRow As DataRow In dtHoliday.Rows
                    If (drRow("IsYearlyFixed") = False) Then
                        drRow("StartDate") = drRow("StartDay") & "/" & drRow("StartMonth") & "/" & drRow("StartYear")
                        drRow("EndDate") = drRow("EndDay") & "/" & drRow("EndMonth") & "/" & drRow("EndYear")
                        drRow("IsYearly") = ""
                    Else
                        drRow("StartDate") = drRow("StartDay") & "-" & CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(drRow("StartMonth"))
                        drRow("EndDate") = drRow("EndDay") & "-" & CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(drRow("EndMonth"))
                        drRow("IsYearly") = "Yearly"
                    End If

                Next
            End If

            dgrdHoliday.DataSource = dtHoliday

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dgrdHoliday_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdHoliday.SelectedIndexChanged

        If dgrdHoliday.SelectedItems.Count = 1 Then

            Dim Startdate As Date
            Dim EndDate As Date

            HolidayID = CInt(CType(dgrdHoliday.SelectedItems(0), GridDataItem).GetDataKeyValue("HolidayId").ToString())
            With objHoliday
                .HolidayId = HolidayID
                .GetByPK()
                txtHolidayArabic.Text = .HolidayArabicName
                txtHolidayEnglish.Text = .HolidayName.ToString
                If (.IsCompanyApplicable) Then
                    rbtnSpecificCompany.Checked = .IsCompanyApplicable
                    rbtnAllCompanies.Checked = False
                    rbtnSpecificWorkLocation.Checked = False
                ElseIf (.IsWorkLocation) Then
                    rbtnSpecificCompany.Checked = False
                    rbtnAllCompanies.Checked = False
                    rbtnSpecificWorkLocation.Checked = True
                Else
                    rbtnSpecificWorkLocation.Checked = False
                    rbtnAllCompanies.Checked = True
                    rbtnSpecificCompany.Checked = False
                End If

                If (.IsReligion) Then
                    rbtnSpecificReligions.Checked = .IsReligion
                    rbtnAllReligions.Checked = False
                Else
                    rbtnAllReligions.Checked = True
                    rbtnSpecificReligions.Checked = False
                End If

                If (.IsEmployeeType) Then
                    rbtnSpecificEmployeeTypes.Checked = .IsEmployeeType
                    rbtnAllEmployeeTypes.Checked = False
                Else
                    rbtnAllEmployeeTypes.Checked = True
                    rbtnSpecificEmployeeTypes.Checked = False
                End If

                If (.IsLogicalGroup) Then
                    rbtnSpecificLogicalGroup.Checked = .IsLogicalGroup
                    rbtnAllLogicalGroup.Checked = False
                Else
                    rbtnAllLogicalGroup.Checked = True
                    rbtnSpecificLogicalGroup.Checked = False
                End If

                chkYearlyFixed.Checked = .isYearlyFixed

                Startdate = Date.ParseExact(.StartMonth.ToString("00") + "/" + .StartDay.ToString("00") + "/" + .StartYear.ToString("0000"), "MM/dd/yyyy", Nothing)
                ' Startdate = CDate(CStr(.StartMonth) + "/" + CStr(.StartDay) + "/" + CStr(.StartYear))
                'EndDate = CDate(CStr(.EndMonth) + "/" + CStr(.EndDay) + "/" + CStr(.EndYear))
                EndDate = Date.ParseExact(.EndMonth.ToString("00") + "/" + .EndDay.ToString("00") + "/" + .EndYear.ToString("0000"), "MM/dd/yyyy", Nothing)

                chkIsReligionRelated.Checked = .IsReligionRelated

            End With
            ShowControls()
            dteFromDay.MinDate = "1900-01-02"
            dteFromDay.MaxDate = "2900-01-01"
            dteToDay.MinDate = "1900-01-02"
            dteToDay.MaxDate = "2900-01-01"
            If chkYearlyFixed.Checked Then
                dteFromDay.SelectedDate = Startdate
                dteToDay.SelectedDate = EndDate
            Else
                dteFromDate.SelectedDate = Startdate
                dteToDate.SelectedDate = EndDate
            End If
            objHoliday_Company.HolidayId = HolidayID
            objHoliday_Religion.HolidayId = HolidayID
            objHoliday_EmployeeType.FK_HolidayId = HolidayID
            objHoliday_WorkLocation.HolidayId = HolidayID
            objHoliday_LogicalGroup.FK_HolidayId = HolidayID
            DtCompany = objHoliday_Company.GetByPK()
            DtWorkLocation = objHoliday_WorkLocation.GetByPK()
            DtReligion = objHoliday_Religion.GetByPK()
            DtEmployeeType = objHoliday_EmployeeType.GetByPK()
            DtLogicalGroup = objHoliday_LogicalGroup.GetByPK()
            ClearLists()
            If Not DtCompany Is Nothing And DtCompany.Rows.Count > 0 Then
                For Each dr As DataRow In DtCompany.Rows
                    For Each lst As ListItem In chkLstCompany.Items
                        If lst.Value = dr(1) Then
                            lst.Selected = True
                        End If
                    Next
                Next

            End If

            If Not DtReligion Is Nothing And DtReligion.Rows.Count > 0 Then
                For Each dr As DataRow In DtReligion.Rows
                    For Each lst As ListItem In chkLstReligion.Items
                        If lst.Value = dr(1) Then
                            lst.Selected = True
                        End If
                    Next
                Next
            End If

            If Not DtEmployeeType Is Nothing And DtEmployeeType.Rows.Count > 0 Then
                For Each dr As DataRow In DtEmployeeType.Rows
                    For Each lst As ListItem In chkLstEmployeeType.Items
                        If lst.Value = dr(1) Then
                            lst.Selected = True
                        End If
                    Next
                Next
            End If


            If Not DtLogicalGroup Is Nothing Then
                For Each dr As DataRow In DtLogicalGroup.Rows
                    For Each lst As ListItem In chkLstLogicalGroup.Items
                        If lst.Value = dr(1) Then
                            lst.Selected = True
                        End If
                    Next
                Next
            End If


            If Not DtWorkLocation Is Nothing And DtWorkLocation.Rows.Count > 0 Then
                For Each dr As DataRow In DtWorkLocation.Rows
                    For Each lst As ListItem In chkLstWorkLocation.Items
                        If lst.Value = dr(1) Then
                            lst.Selected = True
                        End If
                    Next
                Next

            End If

            ibtnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)
        End If
    End Sub


#End Region

#Region "Methods"

    Public Sub FillGrid()
        Try
            Dim dtHoliday = objHoliday.GetAll()
            If dtHoliday IsNot Nothing And dtHoliday.Rows.Count > 0 Then
                dtHoliday.Columns.Add("StartDate")
                dtHoliday.Columns.Add("EndDate")
                dtHoliday.Columns.Add("IsYearly")
                For Each drRow As DataRow In dtHoliday.Rows
                    If (drRow("IsYearlyFixed") = False) Then
                        drRow("StartDate") = Convert.ToDateTime(drRow("StartDay") & "/" & drRow("StartMonth") & "/" & drRow("StartYear")).ToString("dd/MM/yyyy")
                        drRow("EndDate") = Convert.ToDateTime(drRow("EndDay") & "/" & drRow("EndMonth") & "/" & drRow("EndYear")).ToString("dd/MM/yyyy")
                        drRow("IsYearly") = ""
                    Else
                        'drRow("StartDate") = drRow("StartDay") & "-" & CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(drRow("StartMonth"))
                        drRow("StartDate") = drRow("StartDay") & "-" & CultureInfo.GetCultureInfo("ar-EG").DateTimeFormat.GetMonthName(drRow("StartMonth"))

                        'drRow("EndDate") = drRow("EndDay") & "-" & CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(drRow("EndMonth"))
                        drRow("EndDate") = drRow("EndDay") & "-" & CultureInfo.GetCultureInfo("ar-EG").DateTimeFormat.GetMonthName(drRow("EndMonth"))
                        drRow("IsYearly") = "Yearly"
                    End If

                Next
            End If

            dgrdHoliday.DataSource = dtHoliday
            dgrdHoliday.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClearLists()
        For Each lst As ListItem In chkLstEmployeeType.Items
            lst.Selected = False
        Next
        For Each lst As ListItem In chkLstReligion.Items
            lst.Selected = False
        Next
        For Each lst As ListItem In chkLstCompany.Items
            lst.Selected = False
        Next
        For Each lst As ListItem In chkLstWorkLocation.Items
            lst.Selected = False
        Next
        For Each lst As ListItem In chkLstLogicalGroup.Items
            lst.Selected = False
        Next
    End Sub

    Public Sub ClearAll()
        txtHolidayEnglish.Text = String.Empty
        txtHolidayArabic.Text = String.Empty
        rbtnAllCompanies.Checked = True
        rbtnSpecificCompany.Checked = False
        rbtnAllReligions.Checked = True
        rbtnAllEmployeeTypes.Checked = True
        rbtnAllLogicalGroup.Checked = True

        rbtnSpecificLogicalGroup.Checked = False
        rbtnSpecificEmployeeTypes.Checked = False
        rbtnSpecificReligions.Checked = False
        rbtnSpecificWorkLocation.Checked = False
        chkYearlyFixed.Checked = False

        ClearLists()
        dteFromDate.Clear()
        dteFromDay.Clear()
        dteToDate.Clear()
        dteToDay.Clear()
        chkIsReligionRelated.Checked = False
        HolidayID = 0

        ShowControls()
    End Sub

    Private Sub ShowControls()
        If chkYearlyFixed.Checked Then
            dvDate.Attributes.CssStyle.Add("display", "none")
            dvDay.Attributes.CssStyle.Add("display", "block")
            ReqFVFromDate.Enabled = False
            ReqFVToDate.Enabled = False
            ReqFVFromDay.Enabled = True
            ReqFVToDay.Enabled = True
        Else
            dvDate.Attributes.CssStyle.Add("display", "block")
            dvDay.Attributes.CssStyle.Add("display", "none")
            ReqFVFromDate.Enabled = True
            ReqFVToDate.Enabled = True
            ReqFVFromDay.Enabled = False
            ReqFVToDay.Enabled = False
        End If
        If rbtnSpecificCompany.Checked Then
            dvCompany.Attributes.CssStyle.Add("display", "block")
        Else
            dvCompany.Attributes.CssStyle.Add("display", "none")
        End If
        If rbtnSpecificReligions.Checked Then
            dvReligion.Attributes.CssStyle.Add("display", "block")
        Else
            dvReligion.Attributes.CssStyle.Add("display", "none")
        End If

        If rbtnSpecificEmployeeTypes.Checked Then
            dvEmployeeType.Attributes.CssStyle.Add("display", "block")
        Else
            dvEmployeeType.Attributes.CssStyle.Add("display", "none")
        End If

        If rbtnSpecificLogicalGroup.Checked Then
            dvLogicalGroup.Attributes.CssStyle.Add("display", "block")
        Else
            dvLogicalGroup.Attributes.CssStyle.Add("display", "none")
        End If

        If rbtnSpecificWorkLocation.Checked Then
            dvWorkLocation.Attributes.CssStyle.Add("display", "block")
        Else
            dvWorkLocation.Attributes.CssStyle.Add("display", "none")
        End If
    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdHoliday.Skin))
    End Function

#End Region

End Class
