Imports TA.Employees
Imports SmartV.UTILITIES
Imports TA.Lookup
Imports System.Data
Imports TA.DailyTasks
Imports TA.Admin
Imports Telerik.Web.UI
Imports TA.Security
Imports System.Reflection

Partial Class DailyTasks_ManualEntryMassUpdate
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objTA_Reason As TA_Reason
    Private objEmpMove As Emp_Move
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objREADER_KEYS As READER_KEYS
    Private objEmployee As Employee
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objVersion As SmartV.Version.version
    Private objOrgCompany As OrgCompany
    Private objAPP_Settings As APP_Settings
    Private objSYSUsers As SYSUsers

#End Region

#Region "Properties"

    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Public Property EntityId() As Integer
        Get
            Return ViewState("EntityId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EntityId") = value
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

    Public Property IsEmployeeRequired() As Boolean
        Get
            Return ViewState("IsEmployeeRequired")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsEmployeeRequired") = value
        End Set
    End Property

    Public Property IsLevelRequired() As Boolean
        Get
            Return ViewState("IsLevelRequired")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsLevelRequired") = value
        End Set
    End Property

    Public Property tbl_id() As Long
        Get
            Return ViewState("tbl_id")
        End Get
        Set(ByVal value As Long)
            ViewState("tbl_id") = value
        End Set
    End Property

    Public Property bFillGrid() As Boolean
        Get
            Return ViewState("bFillGrid")
        End Get
        Set(ByVal value As Boolean)
            ViewState("bFillGrid") = value
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

    Public Property SortExepression() As String
        Get
            Return ViewState("SortExepression")
        End Get
        Set(ByVal value As String)
            ViewState("SortExepression") = value
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

    Public Property PageNo() As Integer
        Get
            Return ViewState("PageNo")
        End Get
        Set(ByVal value As Integer)
            ViewState("PageNo") = value
        End Set
    End Property

    Public Property Emp_RecordCount() As Integer
        Get
            Return ViewState("Emp_RecordCount")
        End Get
        Set(ByVal value As Integer)
            ViewState("Emp_RecordCount") = value
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
                txtremarks.Text = "الادخال اليدوي"
            Else
                Lang = CtlCommon.Lang.EN
                txtremarks.Text = "Manual Entry"
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            FillReasons()
            IsEmployeeRequired = True
            IsLevelRequired = False
            rmtToTime2.Text = Now.TimeOfDay.ToString()
            RadDatePicker1.SelectedDate = Now
            RadDatePicker1.MaxDate = Now

            'EmployeeFilterUC.IsEmployeeRequired = IsEmployeeRequired
            'EmployeeFilterUC.IsLevelRequired = IsLevelRequired
            PageHeader1.HeaderText = ResourceManager.GetString("ManualEntryMassUpdate", CultureInfo)

            If (objVersion.HasMultiCompany() = False) Then
                MultiEmployeeFilterUC.CompanyID = objVersion.GetCompanyId()
                MultiEmployeeFilterUC.FillList()
                objAPP_Settings = New APP_Settings
                With objAPP_Settings
                    .GetByPK()
                    If .FillCheckBoxList = 1 Then
                        FillEmployee()
                    End If
                End With

                RadCmbBxCompanies.Visible = False
                lblCompany.Visible = False
                rfvCompanies.Enabled = False
            Else
                FillCompany()
                Dim RadComboboxCompany As RadComboBox = DirectCast(MultiEmployeeFilterUC.FindControl("RadCmbBxCompany"), RadComboBox)
                Dim RadComboboxCompanyLabel As Label = DirectCast(MultiEmployeeFilterUC.FindControl("lblCompany"), Label)
                RadComboboxCompany.Visible = False
                RadComboboxCompanyLabel.Visible = False
            End If

        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrentControls.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not pnlEmployee.FindControl(row("AddBtnName")) Is Nothing Then
                        pnlEmployee.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not pnlEmployee.FindControl(row("DeleteBtnName")) Is Nothing Then
                        pnlEmployee.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not pnlEmployee.FindControl(row("EditBtnName")) Is Nothing Then
                        pnlEmployee.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not pnlEmployee.FindControl(row("PrintBtnName")) Is Nothing Then
                        pnlEmployee.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next


    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not ddlReason.SelectedValue = -1 Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            Dim temp_date As Date
            Dim temp_str_date As String
            Dim formats As String() = New String() {"HHmm"}
            Dim temp_time As DateTime
            Dim temp_str_time As String
            Dim err As Integer
            Dim flag As Boolean = False

            For Each item As ListItem In cblEmpList.Items
                If item.Selected Then
                    flag = True
                    objEmpMove = New Emp_Move
                    objRECALC_REQUEST = New RECALC_REQUEST
                    objREADER_KEYS = New READER_KEYS
                    objRECALC_REQUEST.EMP_NO = item.Value

                    objEmployee = New Employee
                    EmployeeId = item.Value
                    If objEmployee.GetEmpNo(item.Value) = False Then
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NumIsntForThisLevel", CultureInfo), "info")
                        Return
                    End If
                    objEmpMove.FK_EmployeeId = EmployeeId
                    objEmpMove.Remarks = txtremarks.Text
                    objEmpMove.Reader = String.Empty
                    objEmpMove.Status = 1
                    objEmpMove.IsManual = True
                    objEmpMove.CREATED_BY = SessionVariables.LoginUser.ID
                    objEmpMove.Remarks = txtremarks.Text
                    objREADER_KEYS.READER_KEY = ddlReason.SelectedValue
                    objREADER_KEYS.GetByPK()

                    objEmpMove.FK_ReasonId = objREADER_KEYS.CHANGE_TO
                    objEmpMove.Type = objREADER_KEYS.Type
                    If RadDatePicker1.SelectedDate IsNot Nothing Then
                        objEmpMove.MoveDate = RadDatePicker1.SelectedDate
                        temp_date = RadDatePicker1.SelectedDate
                        temp_str_date = DateToString(temp_date)
                        objEmpMove.M_DATE_NUM = temp_str_date
                        objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)
                    Else
                        objEmpMove.MoveDate = Nothing
                        objEmpMove.M_DATE_NUM = Nothing
                    End If
                    objEmpMove.IsRemoteWork = False
                    DateTime.TryParseExact(rmtToTime2.Text, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, temp_time)

                    objEmpMove.MoveTime = temp_time
                    temp_str_time = temp_time.Minute + temp_time.Hour * 60
                    objEmpMove.M_TIME_NUM = temp_str_time

                    If tbl_id = 0 Then
                        err = objEmpMove.Add()
                    Else
                        objEmpMove.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                        objEmpMove.MoveId = tbl_id
                        err = objEmpMove.Update()
                    End If
                    If err = 0 Then
                        Dim err2 As Integer
                        Dim count As Integer
                        While count < 5
                            err2 = objRECALC_REQUEST.RECALCULATE()
                            If err2 = 0 Then
                                Exit While
                            End If
                            count += 1
                        End While
                        FillGrid()
                    End If
                End If
            Next


            If err = 0 Then
                If flag = False Then
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("EmployeeSelect", CultureInfo), "info")
                Else
                    ClearAll()
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                End If

            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")

            End If
        End If
    End Sub

    'Protected Sub ibtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnDelete.Click
    '    CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
    '    Dim err As Integer = -99
    '    For Each item As GridDataItem In dgEmpAtt.Items
    '        Dim cb As CheckBox = DirectCast(item.FindControl("chk"), CheckBox)
    '        If cb.Checked Then
    '            Dim tbl_id As Integer = Convert.ToInt32(item("moveid").Text)
    '            objEmpMove = New Emp_Move
    '            objEmpMove.MoveId = tbl_id
    '            err = objEmpMove.Delete()
    '        End If
    '    Next

    '    Dim TempFromDate As Date
    '    Dim TempFromStr As String
    '    objRECALC_REQUEST = New RECALC_REQUEST
    '    objRECALC_REQUEST.EMP_NO = EmployeeFilterUC.EmployeeId

    '    If Not RadDatePicker1.SelectedDate Is Nothing Then
    '        TempFromDate = RadDatePicker1.SelectedDate
    '        TempFromStr = DateToString(TempFromDate)
    '    End If

    '    If err = 0 Then
    '        FillGrid()
    '        'ClearAll()
    '        ddlReason.SelectedIndex = 0
    '        rmtToTime2.Text = String.Empty
    '        If RadDatePicker1.SelectedDate <= Date.Now Then
    '            objRECALC_REQUEST.VALID_FROM_NUM = TempFromStr
    '            objRECALC_REQUEST.RECALCULATE()
    '        End If
    '        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo))
    '        FillGrid()
    '        ClearAll()
    '    ElseIf err = -99 Then
    '        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PleaseSelectFromList", CultureInfo))

    '    Else
    '        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo))
    '    End If
    'End Sub

    Protected Sub ibtnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnClear.Click
        ClearAll()
    End Sub

    'Protected Sub dgEmpAtt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgEmpAtt.SelectedIndexChanged

    '    tbl_id = CInt(CType(dgEmpAtt.SelectedItems(0), GridDataItem)("moveid").Text)
    '    objEmpMove = New Emp_Move
    '    objEmpMove.MoveId = tbl_id
    '    objEmpMove.GetByPK()

    '    'EmployeeFilterUC.GetEmployeeInfo(objEmpMove.FK_EmployeeId)
    '    EmployeeFilterUC.EmployeeId = objEmpMove.FK_EmployeeId
    '    txtremarks.Text = objEmpMove.Remarks

    '    If objEmpMove.MoveDate > RadDatePicker1.MinDate Then
    '        RadDatePicker1.SelectedDate = objEmpMove.MoveDate
    '    Else
    '        RadDatePicker1.Clear()
    '    End If
    '    rmtToTime2.Text = SetTimeFormat(objEmpMove.MoveTime)
    '    ddlReason.SelectedValue = objEmpMove.FK_ReasonId

    'End Sub

    'Protected Sub dgEmpAtt_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgEmpAtt.NeedDataSource
    '    dgEmpAtt.DataSource = Nothing
    '    objEmpMove = New Emp_Move
    '    If Not RadDatePicker1.SelectedDate Is Nothing Then
    '        objEmpMove.MoveDate = RadDatePicker1.SelectedDate
    '    Else
    '        objEmpMove.MoveDate = Nothing
    '    End If

    '    objEmpMove.FK_EmployeeId = EmployeeFilterUC.EmployeeId
    '    dtCurrent = objEmpMove.Getfilter()
    '    Dim dv As New DataView(dtCurrent)
    '    dv.Sort = SortExepression
    '    dgEmpAtt.DataSource = dv

    '    FillGrid()
    'End Sub

    'Protected Sub RadDatePicker1_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles RadDatePicker1.SelectedDateChanged
    '    If EmployeeFilterUC.EmployeeId <> 0 Then
    '        FillGrid()
    '    End If
    'End Sub

    Protected Sub RadCmbBxCompanies_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompanies.SelectedIndexChanged
        If Not RadCmbBxCompanies.SelectedValue = -1 Then
            CompanyChanged()
        Else
            MultiEmployeeFilterUC.EmployeeID = 0
            CompanyChanged()
        End If

    End Sub
#End Region

#Region "Methods"

    Private Sub FillReasons()
        objTA_Reason = New TA_Reason()
        CtlCommon.FillTelerikDropDownList(ddlReason, objTA_Reason.GetNotIsScheduleTiming, Lang)
    End Sub

    Private Function SetTimeFormat(ByVal TimeToFormat As DateTime) As String
        Dim TimeM As String
        Dim TimeH As String

        If TimeToFormat.Hour.ToString.Length < 2 Then
            TimeH = "0" + TimeToFormat.Hour.ToString()
        Else
            TimeH = TimeToFormat.Hour.ToString()
        End If

        If TimeToFormat.Minute.ToString.Length < 2 Then
            TimeM = "0" + TimeToFormat.Minute.ToString()
        Else
            TimeM = TimeToFormat.Minute.ToString()
        End If

        Return TimeH + TimeM
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

    Public Sub FillGrid()

        For Each item As ListItem In cblEmpList.Items
            If item.Selected Then
                dgEmpAtt.DataSource = Nothing
                objEmpMove = New Emp_Move
                If Not RadDatePicker1.SelectedDate Is Nothing Then
                    objEmpMove.MoveDate = RadDatePicker1.SelectedDate
                Else
                    objEmpMove.MoveDate = Nothing
                End If

                objEmpMove.FK_EmployeeId = item.Value
                dtCurrent = objEmpMove.Getfilter()
                Dim dv As New DataView(dtCurrent)
                dv.Sort = SortExepression
                dgEmpAtt.DataSource = dv
                dgEmpAtt.DataBind()
            End If
        Next

    End Sub

    Public Sub ClearAll()
        'TxtEmpNo.Text = ""

        MultiEmployeeFilterUC.ClearValues()
        'ddlLevels.SelectedValue = 0
        'ddlEmpList.SelectedValue = "-1"
        ddlReason.SelectedValue = -1
        RadDatePicker1.SelectedDate = Now
        rmtToTime2.Text = Now.TimeOfDay.ToString()
        txtremarks.Text = "Manual Entry"
        dgEmpAtt.SelectedIndexes.Clear()
        rmtToTime2.Text = String.Empty
        ddlReason.SelectedIndex = -1
        cblEmpList.Items.Clear()
        tbl_id = 0
        If (objVersion.HasMultiCompany() = False) Then
            MultiEmployeeFilterUC.CompanyID = objVersion.GetCompanyId()
            MultiEmployeeFilterUC.FillList()
            FillEmployee()

            RadCmbBxCompanies.Visible = False
            lblCompany.Visible = False
            rfvCompanies.Enabled = False
        Else
            FillCompany()
        End If

    End Sub

    Public Sub ClearValues()
        EmployeeId = 0
        EntityId = 0
        CompanyId = 0
    End Sub

    'Public Sub CompanyChanged()
    '    EmployeeFilterUC.FillEntity()
    '    FillEmployee()
    '    FillGrid()
    'End Sub

    Public Sub FillEmployee()
        If PageNo = 0 Then
            PageNo = 1
        End If
        Repeater1.Visible = False
        cblEmpList.Items.Clear()
        cblEmpList.Text = String.Empty

        If MultiEmployeeFilterUC.CompanyID <> 0 Then
            Dim objEmployee As New Employee
            objEmployee.FK_CompanyId = MultiEmployeeFilterUC.CompanyID

            If (Not MultiEmployeeFilterUC.EntityID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "1" Then
                objEmployee.FK_EntityId = MultiEmployeeFilterUC.EntityID
            Else
                objEmployee.FK_EntityId = -1
            End If

            If (Not MultiEmployeeFilterUC.WorkGroupID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "2" Then
                objEmployee.FK_LogicalGroup = MultiEmployeeFilterUC.WorkGroupID
                objEmployee.FilterType = "L"
            Else
                objEmployee.FK_LogicalGroup = -1
            End If

            If (Not MultiEmployeeFilterUC.WorkLocationsID) AndAlso MultiEmployeeFilterUC.SearchType = "3" Then
                objEmployee.FK_WorkLocation = MultiEmployeeFilterUC.WorkLocationsID
                objEmployee.FilterType = "W"
            Else
                objEmployee.FK_WorkLocation = -1
            End If
            objEmployee.EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            objEmployee.Get_EmployeeRecordCount()
            Emp_RecordCount = objEmployee.Emp_RecordCount
            Dim dt As DataTable

            If MultiEmployeeFilterUC.EmployeeID = 0 Then
                objEmployee.PageNo = PageNo
                objEmployee.PageSize = 1000
                dt = objEmployee.GetEmpByCompany
            Else
                objEmployee.EmployeeId = MultiEmployeeFilterUC.EmployeeID
                dt = objEmployee.GetByEmpId()
            End If


            ' fill pager 
            'Dim pagefrom, pageto As Integer
            If Emp_RecordCount > 1000 And EmployeeId = 0 Then

                Dim dtpaging As New DataTable
                dtpaging.Columns.Add("pageNo")
                Dim index As Integer
                Dim empcount As Integer
                'empcount = dt.Rows.Count
                empcount = Emp_RecordCount
                For index = 0 To empcount Step 1000
                    Dim drpaging As DataRow
                    drpaging = dtpaging.NewRow
                    Dim dcCell3 As New DataColumn
                    dcCell3.ColumnName = "PageNo"
                    drpaging(0) = index / 1000 + 1
                    dtpaging.Rows.Add(drpaging)

                Next
                Repeater1.DataSource = dtpaging
                Repeater1.DataBind()
                For Each item2 As RepeaterItem In Repeater1.Items
                    Dim lnk As LinkButton = DirectCast(item2.FindControl("LinkButton1"), LinkButton)
                    If lnk.Text = PageNo Then
                        lnk.Attributes.Add("style", "color:Blue;font-weight:bold;text-decoration: underline")
                    End If
                Next
                'pagefrom = ((PageNo - 1) * 1000) + 1
                'pageto = PageNo * 1000
            End If


            If EmployeeId > 0 Then
                cblEmpList.DataSource = Nothing
                Repeater1.DataSource = Nothing
                Repeater1.DataBind()
            End If
            ' end fill pager


            If (dt IsNot Nothing) Then
                Dim dtEmployees = dt
                If (dtEmployees IsNot Nothing) Then
                    If (dtEmployees.Rows.Count > 0) Then
                        Repeater1.Visible = True
                        Dim dtSource As New DataTable
                        dtSource.Columns.Add("EmployeeId")
                        dtSource.Columns.Add("EmployeeName")
                        Dim drRow As DataRow
                        drRow = dtSource.NewRow()
                        For Item As Integer = 0 To dtEmployees.Rows.Count - 1
                            'If Item + 1 >= pagefrom And Item + 1 <= pageto Then


                            Dim drSource As DataRow
                            drSource = dtSource.NewRow
                            Dim dcCell1 As New DataColumn
                            Dim dcCell2 As New DataColumn
                            dcCell1.ColumnName = "EmployeeId"
                            dcCell2.ColumnName = "EmployeeName"
                            dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
                            dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
                            drSource("EmployeeId") = dcCell1.DefaultValue
                            drSource("EmployeeName") = dcCell2.DefaultValue
                            dtSource.Rows.Add(drSource)

                            'ElseIf MultiEmployeeFilterUC.EmployeeID > 0 Then

                            '    Dim drSource As DataRow
                            '    drSource = dtSource.NewRow
                            '    Dim dcCell1 As New DataColumn
                            '    Dim dcCell2 As New DataColumn
                            '    dcCell1.ColumnName = "EmployeeId"
                            '    dcCell2.ColumnName = "EmployeeName"
                            '    dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
                            '    dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
                            '    drSource("EmployeeId") = dcCell1.DefaultValue
                            '    drSource("EmployeeName") = dcCell2.DefaultValue
                            '    dtSource.Rows.Add(drSource)
                            'Else
                            '    Dim drSource As DataRow
                            '    drSource = dtSource.NewRow
                            '    Dim dcCell1 As New DataColumn
                            '    Dim dcCell2 As New DataColumn
                            '    dcCell1.ColumnName = "EmployeeId"
                            '    dcCell2.ColumnName = "EmployeeName"
                            '    dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
                            '    dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
                            '    drSource("EmployeeId") = dcCell1.DefaultValue
                            '    drSource("EmployeeName") = dcCell2.DefaultValue
                            '    dtSource.Rows.Add(drSource)
                            'End If
                        Next
                        Dim dv As New DataView(dtSource)
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            'dv.Sort = "EmployeeName"
                            For Each row As DataRowView In dv
                                If (Not MultiEmployeeFilterUC.EmployeeID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "4" Then
                                    If MultiEmployeeFilterUC.EmployeeID = row("EmployeeId") Then
                                        cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                                        Exit For
                                    End If
                                Else
                                    cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                                End If
                            Next
                        Else
                            For Each row As DataRowView In dv
                                If (Not MultiEmployeeFilterUC.EmployeeID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "4" Then
                                    If MultiEmployeeFilterUC.EmployeeID = row("EmployeeId") Then
                                        cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                                        Exit For
                                    End If
                                Else
                                    cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub FillCompany()
        'Dim dt As DataTable
        'objOrgCompany = New OrgCompany
        'dt = objOrgCompany.GetAllforddl
        'CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, dt, Lang)

        If SessionVariables.LoginUser IsNot Nothing Then
            objAPP_Settings = New APP_Settings
            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()
            objAPP_Settings.GetByPK()
            If objSYSUsers.UserStatus = 1 Then 'System User - Full 
                Dim objOrgCompany As New OrgCompany
                CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, objOrgCompany.GetAllforddl, Lang)
            ElseIf objSYSUsers.UserStatus = 2 Then
                If objAPP_Settings.ShowLGWithEntityPrivilege = False Then
                    FillCompanyForUserSecurity()
                Else
                    Dim objOrgCompany As New OrgCompany
                    CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, objOrgCompany.GetAllforddl, Lang)
                End If

            ElseIf objSYSUsers.UserStatus = 3 Then
                FillCompanyForUserSecurity()
            Else
                Dim objOrgCompany As New OrgCompany
                CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, objOrgCompany.GetAllforddl, Lang)
            End If
        End If
        If (objVersion.HasMultiCompany() = False) Then
            CompanyId = objVersion.GetCompanyId()
            RadCmbBxCompanies.SelectedValue = objVersion.GetCompanyId()
        End If

    End Sub

    Private Sub FillCompanyForUserSecurity()
        Try
            Dim objOrgCompany As New OrgCompany
            Dim dtCompanies As New DataTable

            objOrgCompany.FK_UserId = objSYSUsers.ID
            objOrgCompany.FilterType = "C"
            dtCompanies = objOrgCompany.GetAllforddl_ByUserId

            CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, dtCompanies, Lang)


        Catch ex As Exception

        End Try
    End Sub

    Public Sub CompanyChanged()
            'EmployeeFilterUC.FillEntity()
            MultiEmployeeFilterUC.CompanyID = RadCmbBxCompanies.SelectedValue
            MultiEmployeeFilterUC.FillList()
            FillEmployee()
    End Sub

    Public Sub EntityChanged()

        If Not MultiEmployeeFilterUC.CompanyID = Nothing Or Not MultiEmployeeFilterUC.CompanyID = -1 Then
            PageNo = 0
            FillEmployee()
        Else
            Repeater1.Visible = False
        End If

    End Sub

    Public Sub WorkGroupChanged()

        If Not MultiEmployeeFilterUC.CompanyID = Nothing Or Not MultiEmployeeFilterUC.CompanyID = -1 Then
            PageNo = 0
            FillEmployee()
        Else
            Repeater1.Visible = False
        End If

    End Sub

    Public Sub WorkLocationsChanged()

        If Not MultiEmployeeFilterUC.CompanyID = Nothing Or Not MultiEmployeeFilterUC.CompanyID = -1 Then
            PageNo = 0
            FillEmployee()
        Else
            Repeater1.Visible = False
        End If

    End Sub

    Protected Sub Repeater1_ItemCommand(source As Object, e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles Repeater1.ItemCommand

        Dim page As Integer = CInt(CType(e.CommandSource, LinkButton).Text)
        PageNo = page
        FillEmployee()

    End Sub

    Protected Sub dgEmpAtt_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgEmpAtt.NeedDataSource
        For Each item As ListItem In cblEmpList.Items
            If item.Selected Then
                dgEmpAtt.DataSource = Nothing
                objEmpMove = New Emp_Move
                If Not RadDatePicker1.SelectedDate Is Nothing Then
                    objEmpMove.MoveDate = RadDatePicker1.SelectedDate
                Else
                    objEmpMove.MoveDate = Nothing
                End If

                objEmpMove.FK_EmployeeId = item.Value
                dtCurrent = objEmpMove.Getfilter()
                Dim dv As New DataView(dtCurrent)
                dv.Sort = SortExepression
                dgEmpAtt.DataSource = dv
            End If
        Next
    End Sub

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgEmpAtt.Skin))
    End Function

    Protected Sub dgEmpAtt_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgEmpAtt.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

#End Region

    

    
End Class
