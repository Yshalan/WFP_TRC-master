Imports TA.OrgCompany
Imports SmartV.UTILITIES
Imports TA.Lookup
Imports System.Data
Imports TA.Employees
Imports TA.Definitions
Imports TA.Admin
Imports TA.Security
Imports TA.DailyTasks
Imports Telerik.Web.UI

Partial Class DailyTasks_UserControls_AssignSchedule_Company
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objSYSUsers As SYSUsers
    Private objVersion As SmartV.Version.version
    Private objSchedule_Company As Schedule_Company
    Dim objUserPrivileg_Entities As UserPrivileg_Entities
    Dim objUserPrivileg_Companies As UserPrivileg_Companies
    Dim objEmployee As Employee
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objRecalculateRequest As RecalculateRequest
    Private objAPP_Settings As APP_Settings

#End Region

#Region "Properties"
    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property
    Public Property EmpWorkScheduleId() As Integer
        Get
            Return ViewState("EmpWorkScheduleId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmpWorkScheduleId") = value
        End Set
    End Property
    Public Property CompanyId() As Integer
        Get
            Return ViewState("CompanyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CompanyId") = value
        End Set
    End Property
    Public Property ScheduleId() As Integer
        Get
            Return ViewState("ScheduleId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ScheduleId") = value
        End Set
    End Property
#End Region

#Region "Page Events"
    'Protected Sub Page_PreRenderComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
    '    CType(pnlFilter.FindControl("lblCaption"), Label).Text = "Process Could take a few minutes."
    'End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If (objVersion.HasMultiCompany() = False) Then
                RadCmbBxCompany.SelectedValue = objVersion.GetCompanyId()
                RadCmbBxCompany_SelectedIndexChanged(Nothing, Nothing)
                lblCompany.Visible = False
                RadCmbBxCompany.Visible = False
            End If
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                Dim item1 As New RadComboBoxItem
                Dim item2 As New RadComboBoxItem
                Dim item3 As New RadComboBoxItem
                Dim item4 As New RadComboBoxItem
                Dim item5 As New RadComboBoxItem
                item1.Value = -1
                item1.Text = "--الرجاء الاختيار--"
                RadCmbBxScheduletype.Items.Add(item1)
                item2.Value = 1
                item2.Text = "عادي"
                RadCmbBxScheduletype.Items.Add(item2)
                item3.Value = 2
                item3.Text = "مرن"
                RadCmbBxScheduletype.Items.Add(item3)
                item4.Value = 3
                item4.Text = "مناوبات"
                RadCmbBxScheduletype.Items.Add(item4)
                item5.Value = 5
                item5.Text = "مفتوح"
                RadCmbBxScheduletype.Items.Add(item5)
                RFVCompany.InitialValue = "--الرجاء الاختيار--"
                rfvScheduletype.InitialValue = "--الرجاء الاختيار--"
            Else
                Lang = CtlCommon.Lang.EN

                Dim item1 As New RadComboBoxItem
                Dim item2 As New RadComboBoxItem
                Dim item3 As New RadComboBoxItem
                Dim item4 As New RadComboBoxItem
                Dim item5 As New RadComboBoxItem
                item1.Value = -1
                item1.Text = "--Please Select--"
                RadCmbBxScheduletype.Items.Add(item1)
                item2.Value = 1
                item2.Text = "Normal"
                RadCmbBxScheduletype.Items.Add(item2)
                item3.Value = 2
                item3.Text = "Flexible"
                RadCmbBxScheduletype.Items.Add(item3)
                item4.Value = 3
                item4.Text = "Advanced"
                RadCmbBxScheduletype.Items.Add(item4)
                item5.Value = 5
                item5.Text = "Open"
                RadCmbBxScheduletype.Items.Add(item5)
                RFVCompany.InitialValue = "--Please Select--"
                rfvScheduletype.InitialValue = "--Please Select--"
            End If
            Me.dtpFromdate.SelectedDate = Today
            Me.dtpEndDate.SelectedDate = Today
            FillCompany()
            FillGrid()
            'Assign_Company.HeaderText = ResourceManager.GetString("Assign_Company", CultureInfo)
            ArcivingMonths_DateValidation()
        End If
        If chckTemporary.Checked = True Then
            showHide(True)
        Else
            showHide(False)
        End If
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

    Protected Sub RadCmbBxScheduletype_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxScheduletype.SelectedIndexChanged
        FillSchedule()
    End Sub

    Private Sub showHide(ByVal status As Boolean, Optional ByVal order As Integer = 0)
        pnlEndDate.Visible = status
        CVDate.Visible = status
    End Sub

    Protected Sub RadCmbBxCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompany.SelectedIndexChanged

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If ValidateEndDate() = False Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DateRange", CultureInfo), "info")
            Return
        End If

        Dim errornum As Integer
        objRECALC_REQUEST = New RECALC_REQUEST
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

        If Not RadCmbBxSchedules.SelectedValue = Nothing Then
            If RadCmbBxSchedules.SelectedValue <> -1 Then
                Dim arr As String()
                arr = RadCmbBxSchedules.SelectedValue.Split(",")
                Dim objSchedule_Company As New Schedule_Company
                objSchedule_Company.FK_CompanyId = RadCmbBxCompany.SelectedValue
                objSchedule_Company.FK_ScheduleId = RadCmbBxSchedules.SelectedValue.ToString().Split(",")(0)
                objSchedule_Company.ScheduleType = RadCmbBxScheduletype.SelectedValue
                objSchedule_Company.FromDate = dtpFromdate.SelectedDate
                objSchedule_Company.ToDate = IIf(dtpEndDate.SelectedDate Is Nothing Or Not chckTemporary.Checked, DateTime.MinValue, dtpEndDate.SelectedDate)
                objSchedule_Company.IsTemporary = chckTemporary.Checked
                objSchedule_Company.CREATED_BY = SessionVariables.LoginUser.ID
                If EmpWorkScheduleId <> 0 Then
                    objSchedule_Company.EmpWorkScheduleId = EmpWorkScheduleId
                    objSchedule_Company.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    errornum = objSchedule_Company.Update
                Else
                    errornum = objSchedule_Company.AssignSchedule_Company()
                End If


            End If
        End If
        Dim errornum2 As Integer
        If errornum = 0 Then
            If Not dtpFromdate.SelectedDate > Date.Today Then
                objRecalculateRequest = New RecalculateRequest
                objEmployee = New Employee
                objRecalculateRequest.Fk_CompanyId = RadCmbBxCompany.SelectedValue
                objRecalculateRequest.ImmediatelyStart = True
                objRecalculateRequest.FromDate = dtpFromdate.SelectedDate
                objRecalculateRequest.Remarks = "Assign Schedule - SYSTEM"
                If chckTemporary.Checked = True Then
                    If Not dtpEndDate.SelectedDate Is Nothing Or Not dtpEndDate.SelectedDate > Date.Today Then
                        objRecalculateRequest.ToDate = dtpEndDate.SelectedDate
                    End If
                Else
                    objRecalculateRequest.ToDate = Date.Today
                End If
                objRecalculateRequest.CREATED_BY = SessionVariables.LoginUser.FK_EmployeeId
                errornum2 = objRecalculateRequest.Add()
            End If
        End If

        If errornum = 0 And errornum2 = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ClearAll()
        ElseIf errornum = -99 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DateExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")

        End If
        FillGrid()
    End Sub

    Protected Sub dgrdSchedule_Company_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdSchedule_Company.SelectedIndexChanged
        objSchedule_Company = New Schedule_Company
        EmpWorkScheduleId = CInt(CType(dgrdSchedule_Company.SelectedItems(0), GridDataItem).GetDataKeyValue("EmpWorkScheduleId").ToString())
        CompanyId = CInt(CType(dgrdSchedule_Company.SelectedItems(0), GridDataItem).GetDataKeyValue("CompanyId").ToString())
        ScheduleId = CInt(CType(dgrdSchedule_Company.SelectedItems(0), GridDataItem).GetDataKeyValue("ScheduleId").ToString())
        With objSchedule_Company
            .EmpWorkScheduleId = EmpWorkScheduleId
            .GetByPK()
            RadCmbBxCompany.SelectedValue = .FK_CompanyId
            RadCmbBxScheduletype.SelectedValue = .ScheduleType
            FillSchedule()
            RadCmbBxSchedules.SelectedValue = .FK_ScheduleId.ToString + "," + .ScheduleType.ToString
            dtpFromdate.SelectedDate = .FromDate
            chckTemporary.Checked = .IsTemporary
            showHide(chckTemporary.Checked)
            If chckTemporary.Checked = True Then
                If Not .ToDate = Nothing Then
                    showHide(chckTemporary.Checked)
                    dtpEndDate.SelectedDate = .ToDate
                Else
                    showHide(chckTemporary.Checked)
                End If
            End If
        End With
        ManageControls(False)
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Sub dgrdSchedule_Company_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdSchedule_Company.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdSchedule_Company_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdSchedule_Company.NeedDataSource
        Try
            objSchedule_Company = New Schedule_Company
            dgrdSchedule_Company.DataSource = objSchedule_Company.Get_Company_Schedule_Details

        Catch ex As Exception
        End Try
    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdSchedule_Company.Skin))
    End Function

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ClearAll()
        ManageControls(True)
    End Sub

#End Region

#Region "Methods"

    Private Sub FillCompany()
        If SessionVariables.LoginUser IsNot Nothing Then

            objUserPrivileg_Companies = New UserPrivileg_Companies
            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()

            If Not objSYSUsers.FK_EmployeeId = Nothing Then
                objUserPrivileg_Companies.FK_EmployeeId = objSYSUsers.FK_EmployeeId
                objUserPrivileg_Companies.GetByEmpId()
                CompanyId = objUserPrivileg_Companies.FK_CompanyId
            End If

            If (objUserPrivileg_Companies.FK_CompanyId <> 0) Then
                FillCompanyForUserSecurity()
            Else
                Dim objOrgCompany As New OrgCompany
                CtlCommon.FillTelerikDropDownList(RadCmbBxCompany, objOrgCompany.GetAllforddl, Lang)
            End If
        End If
    End Sub

    Private Sub FillCompanyForUserSecurity()
        objUserPrivileg_Companies = New UserPrivileg_Companies
        Dim objOrgCompany As New OrgCompany
        objOrgCompany.CompanyId = CompanyId
        Dim CompanyInfo = objOrgCompany.GetByPK()

        Dim dtCompanyInfo As New DataTable
        Dim dcCompanyValue As New DataColumn
        dcCompanyValue.ColumnName = "Value"
        dcCompanyValue.DataType = GetType(Integer)

        Dim dcCompanyText As New DataColumn
        dcCompanyText.ColumnName = "Text"
        dcCompanyText.DataType = GetType(String)

        dtCompanyInfo.Columns.Add(dcCompanyValue)
        dtCompanyInfo.Columns.Add(dcCompanyText)
        Dim drCompanyRow = dtCompanyInfo.NewRow()
        drCompanyRow("Value") = CompanyInfo.CompanyId
        drCompanyRow("Text") = CompanyInfo.CompanyName
        dtCompanyInfo.Rows.Add(drCompanyRow)

        CtlCommon.FillTelerikDropDownList(RadCmbBxCompany, dtCompanyInfo, Lang)
        RadCmbBxCompany.SelectedIndex = 1
        RadCmbBxCompany.Enabled = False
    End Sub

    Private Sub FillGrid()
        objSchedule_Company = New Schedule_Company
        dgrdSchedule_Company.DataSource = objSchedule_Company.Get_Company_Schedule_Details
        dgrdSchedule_Company.DataBind()
    End Sub

    Private Sub FillSchedule()
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
            rfvSchedule.InitialValue = "--الرجاء الاختيار--"
        Else
            Lang = CtlCommon.Lang.EN
            rfvSchedule.InitialValue = "--Please Select--"
        End If

        Dim objWorkSchedule As New WorkSchedule()
        If (RadCmbBxScheduletype.SelectedValue = 1) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(1), Lang)
        ElseIf (RadCmbBxScheduletype.SelectedValue = 2) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(2), Lang)
        ElseIf (RadCmbBxScheduletype.SelectedValue = 3) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(3), Lang)
        ElseIf (RadCmbBxScheduletype.SelectedValue = 5) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(5), Lang)
        End If


    End Sub

    Private Sub ClearAll()
        ' Clear the controls

        If Not (objVersion.HasMultiCompany() = False) Then
            RadCmbBxCompany.SelectedValue = -1
        End If

        RadCmbBxSchedules.Text = String.Empty
        RadCmbBxSchedules.Items.Clear()
        RadCmbBxScheduletype.SelectedValue = -1
        chckTemporary.Checked = False
        pnlEndDate.Visible = False
        dtpFromdate.SelectedDate = Today
        dtpEndDate.SelectedDate = Today
        EmpWorkScheduleId = 0
        CompanyId = 0
        ScheduleId = 0
        ManageControls(True)
    End Sub

    Private Sub ManageControls(ByVal status As Boolean)
        RadCmbBxCompany.Enabled = status
        RadCmbBxSchedules.Enabled = status
        RadCmbBxScheduletype.Enabled = status

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

    Private Sub ArcivingMonths_DateValidation()
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        Dim ArchivingMonths As Integer = objAPP_Settings.ArchivingMonths

        If Not ArchivingMonths = 0 Then
            ArchivingMonths = ArchivingMonths
            Dim NewDate As Date = DateAdd(DateInterval.Month, ArchivingMonths, Date.Today)
            dtpFromdate.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
            dtpEndDate.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
        End If
    End Sub

#End Region

#Region "Start & End date validation"

    Private Function ValidateEndDate() As Boolean
        If IS_Temporary() And dtpEndDate.SelectedDate() IsNot Nothing Then
            Return StartEndDateComparison(dtpFromdate.SelectedDate, dtpEndDate.SelectedDate)
        End If
        Return True
    End Function

    Private Function StartEndDateComparison(ByVal dateStartdate As DateTime, _
                                           ByVal dateEndDate As DateTime) As Boolean
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If IsDate(dtpFromdate.SelectedDate) And IsDate(dtpEndDate.SelectedDate) Then
            ' Get the start and end date from the calenders
            dateStartdate = New DateTime(dtpFromdate.SelectedDate.Value.Year, _
                                         dtpFromdate.SelectedDate.Value.Month, _
                                         dtpFromdate.SelectedDate.Value.Day)
            dateEndDate = New DateTime(dtpEndDate.SelectedDate.Value.Year, _
                                       dtpEndDate.SelectedDate.Value.Month, _
                                       dtpEndDate.SelectedDate.Value.Day)
            Dim result As Integer = DateTime.Compare(dateEndDate, dateStartdate)
            If result < 0 Then
                ' show message and set focus on end date picker
                'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DateRangeEarly", CultureInfo))

                dtpEndDate.Focus()
                Return False
                'ElseIf result = 0 Then
                '    dtpEndDate.Focus()
                '    Return False
            Else
                ' Do nothing
                Return True
            End If
        End If
    End Function

    Private Function IS_Temporary() As Boolean
        Return chckTemporary.Checked
    End Function

#End Region

End Class
