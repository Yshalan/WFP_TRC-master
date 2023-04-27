
Imports System.Data
Imports System.Globalization
Imports SmartV.UTILITIES
Imports TA.Admin
Imports TA.Definitions
Imports TA.Employees
Imports TA.FeedBack
Imports TA.Security
Imports Telerik.Web.UI.DatePickerPopupDirection

Partial Class FeedBack_SurveyAssignments
    Inherits System.Web.UI.Page
#Region "Class Variables"

    Private objEmployee As Employee
    Private objVersion As SmartV.Version.version
    Private objOrgCompany As OrgCompany
    Private objAPP_Settings As APP_Settings
    Private objSYSUsers As SYSUsers
    Private objEmp_logicalGroup As Emp_logicalGroup
    Private objFeedback_Survey As Feedback_Survey
    Private objFeedBack_SurveyAssignment As Feedback_SurveyAssignment


    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Public Properties"
    Public Property AssignmentId() As Integer
        Get
            Return ViewState("AssignmentId")
        End Get
        Set(ByVal value As Integer)
            ViewState("AssignmentId") = value
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

    Private Sub FeedBack_SurveyAssignment_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            FillLogicalGroup()
            FillSurvey()

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
            SetRadDateTimePickerPeoperties()
            dtpFromdate.DbSelectedDate = DateTime.Now
            dtpToDate.DbSelectedDate = DateTime.Now

            dtpLGFromDate.DbSelectedDate = DateTime.Now
            dtpLGToDate.DbSelectedDate = DateTime.Now

            PageHeader1.HeaderText = ResourceManager.GetString("SurveyAssignment", CultureInfo)

            If Lang = CtlCommon.Lang.AR Then
                dtpFromdate.Culture = New CultureInfo("ar-EG", False)
                dtpToDate.Culture = New CultureInfo("ar-EG", False)
                dtpLGFromDate.Culture = New CultureInfo("ar-EG", False)
                dtpLGToDate.Culture = New CultureInfo("ar-EG", False)
            Else
                dtpFromdate.Culture = New CultureInfo("en-US", False)
                dtpToDate.Culture = New CultureInfo("en-US", False)
                dtpLGFromDate.Culture = New CultureInfo("en-US", False)
                dtpLGToDate.Culture = New CultureInfo("en-US", False)

            End If

        End If

        btnEmpDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdEmpSurvey.ClientID + "')")
        btnLGDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdLGSurvey.ClientID + "')")

    End Sub

    Protected Sub RadCmbBxCompanies_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompanies.SelectedIndexChanged
        If Not RadCmbBxCompanies.SelectedValue = -1 Then
            CompanyChanged()
        Else
            MultiEmployeeFilterUC.EmployeeID = 0
            CompanyChanged()
        End If

    End Sub

    Protected Sub dgrdEmpSurvey_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmpSurvey.Skin))
    End Function

    Protected Sub dgrdLGSurvey_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid1" Then
            RadFilter2.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar2_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon1() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdLGSurvey.Skin))
    End Function

    Private Sub btnEmpSave_Click(sender As Object, e As EventArgs) Handles btnEmpSave.Click
        Dim err As Integer = -1
        Dim flag As Boolean = False

        For Each item As ListItem In cblEmpList.Items
            If item.Selected Then
                flag = True
                objFeedBack_SurveyAssignment = New Feedback_SurveyAssignment
                With objFeedBack_SurveyAssignment
                    .FK_EmployeeId = item.Value
                    .FK_SurveyId = radcmbxSurvey.SelectedValue
                    .FromDate = dtpFromdate.DbSelectedDate
                    .ToDate = dtpToDate.DbSelectedDate

                    If AssignmentId = 0 Then
                        .CREATED_BY = SessionVariables.LoginUser.ID
                        err = .Add
                    Else
                        .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                        .AssignmentId = AssignmentId
                        err = .Update
                    End If
                End With
            End If
        Next

        If flag = False Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("EmployeeSelect", CultureInfo), "info")
            Exit Sub
        End If

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ClearEmployeeFields()
            FillEmpGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If

    End Sub

    Private Sub btnEmpClear_Click(sender As Object, e As EventArgs) Handles btnEmpClear.Click
        ClearEmployeeFields()
    End Sub

    Private Sub btnEmpDelete_Click(sender As Object, e As EventArgs) Handles btnEmpDelete.Click
        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdEmpSurvey.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objFeedBack_SurveyAssignment = New Feedback_SurveyAssignment
                objFeedBack_SurveyAssignment.AssignmentId = Convert.ToInt32(row.GetDataKeyValue("AssignmentId").ToString())
                errNum = objFeedBack_SurveyAssignment.Delete()
            End If
        Next

        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillEmpGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        End If
        ClearEmployeeFields()
    End Sub

    Private Sub dgrdEmpSurvey_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdEmpSurvey.NeedDataSource
        objFeedBack_SurveyAssignment = New Feedback_SurveyAssignment
        With objFeedBack_SurveyAssignment
            dgrdEmpSurvey.DataSource = .GetAll_ForEmp
        End With
    End Sub

    Private Sub dgrdEmpSurvey_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdEmpSurvey.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item
            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeArabicName"), HiddenField).Value
                item("SurveyName").Text = DirectCast(item.FindControl("hdnSurveyArabicName"), HiddenField).Value
            End If

        End If
    End Sub

    Private Sub btnLGSave_Click(sender As Object, e As EventArgs) Handles btnLGSave.Click
        Dim err As Integer = -1

        objFeedBack_SurveyAssignment = New Feedback_SurveyAssignment
        With objFeedBack_SurveyAssignment
            .FK_LogicalGroupId = ddlLogicalGroup.SelectedValue
            .FK_SurveyId = radcmbxLGSurvey.SelectedValue
            .FromDate = dtpLGFromDate.DbSelectedDate
            .ToDate = dtpLGToDate.DbSelectedDate

            If AssignmentId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add
            Else
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .AssignmentId = AssignmentId
                err = .Update
            End If
        End With


        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ClearLogicalGroupFields()
            FillLogicalGroupGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Private Sub btnLGClear_Click(sender As Object, e As EventArgs) Handles btnLGClear.Click
        ClearLogicalGroupFields()
    End Sub

    Private Sub btnLGDelete_Click(sender As Object, e As EventArgs) Handles btnLGDelete.Click
        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdLGSurvey.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objFeedBack_SurveyAssignment = New Feedback_SurveyAssignment
                objFeedBack_SurveyAssignment.AssignmentId = Convert.ToInt32(row.GetDataKeyValue("AssignmentId").ToString())
                errNum = objFeedBack_SurveyAssignment.Delete()

            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillLogicalGroupGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        End If
        ClearLogicalGroupFields()
    End Sub

    Private Sub dgrdLGSurvey_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdLGSurvey.NeedDataSource
        objFeedBack_SurveyAssignment = New Feedback_SurveyAssignment
        With objFeedBack_SurveyAssignment
            dgrdLGSurvey.DataSource = .GetAll_ForLogicalGroup
        End With
    End Sub

    Private Sub dgrdLGSurvey_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdLGSurvey.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item
            If Lang = CtlCommon.Lang.AR Then
                item("GroupName").Text = DirectCast(item.FindControl("hdnGroupArabicName"), HiddenField).Value
                item("SurveyName").Text = DirectCast(item.FindControl("hdnLGSurveyArabicName"), HiddenField).Value
            End If

        End If
    End Sub

#End Region

#Region "Methods"

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

    Public Sub CompanyChanged()
        'EmployeeFilterUC.FillEntity()
        MultiEmployeeFilterUC.CompanyID = RadCmbBxCompanies.SelectedValue
        MultiEmployeeFilterUC.FillList()
        FillEmployee()
    End Sub

    Private Sub FillCompany()

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

    Private Sub FillLogicalGroup()
        objEmp_logicalGroup = New Emp_logicalGroup
        With objEmp_logicalGroup
            CtlCommon.FillTelerikDropDownList(ddlLogicalGroup, .GetAll, Lang)
        End With
    End Sub

    Private Sub SetRadDateTimePickerPeoperties()


        ' This function set properties for terlerik controls

        'Imports Telerik.Web.UI.DatePickerPopupDirection

        ' Set TimeView properties 
        Me.dtpFromdate.TimeView.StartTime = New TimeSpan(0, 0, 0)
        Me.dtpFromdate.TimeView.EndTime = New TimeSpan(23.5, 0, 0)
        Me.dtpFromdate.TimeView.HeaderText = String.Empty
        Me.dtpFromdate.TimeView.Interval = New TimeSpan(0, 30, 0)
        Me.dtpFromdate.TimeView.Columns = 5

        ' Set Popup window properties
        Me.dtpFromdate.PopupDirection = TopRight
        Me.dtpFromdate.ShowPopupOnFocus = True



        ' Set default value
        Me.dtpToDate.SelectedDate = Now

        ' Set TimeView properties 
        Me.dtpToDate.TimeView.StartTime = New TimeSpan(0, 0, 0)
        Me.dtpToDate.TimeView.EndTime = New TimeSpan(23.5, 0, 0)
        Me.dtpToDate.TimeView.Interval = New TimeSpan(0, 30, 0)
        Me.dtpToDate.TimeView.HeaderText = String.Empty
        Me.dtpToDate.TimeView.Columns = 5
        ' Set Popup window properties
        Me.dtpToDate.PopupDirection = TopRight
        Me.dtpToDate.ShowPopupOnFocus = True
        ' Set default value
        Me.dtpToDate.SelectedDate = Now

    End Sub

    Private Sub FillSurvey()
        objFeedback_Survey = New Feedback_Survey
        With objFeedback_Survey
            CtlCommon.FillTelerikDropDownList(radcmbxSurvey, .GetAll, Lang)
        End With
        With objFeedback_Survey
            CtlCommon.FillTelerikDropDownList(radcmbxLGSurvey, .GetAll, Lang)
        End With
    End Sub

    Private Sub FillEmpGrid()
        objFeedBack_SurveyAssignment = New Feedback_SurveyAssignment
        With objFeedBack_SurveyAssignment
            dgrdEmpSurvey.DataSource = .GetAll_ForEmp
            dgrdEmpSurvey.DataBind()
        End With

    End Sub

    Private Sub ClearAll()
        ClearEmployeeFields()
        ClearLogicalGroupFields()
    End Sub

    Private Sub ClearEmployeeFields()
        MultiEmployeeFilterUC.ClearValues()
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
        cblEmpList.Items.Clear()
        dtpFromdate.SelectedDate = DateTime.Now
        dtpToDate.SelectedDate = DateTime.Now
        radcmbxSurvey.SelectedValue = -1
        AssignmentId = 0
    End Sub

    Private Sub FillLogicalGroupGrid()
        objFeedBack_SurveyAssignment = New Feedback_SurveyAssignment
        With objFeedBack_SurveyAssignment
            dgrdLGSurvey.DataSource = .GetAll_ForLogicalGroup
            dgrdLGSurvey.DataBind()
        End With
    End Sub

    Private Sub ClearLogicalGroupFields()
        ddlLogicalGroup.SelectedValue = -1
        radcmbxLGSurvey.SelectedValue = -1
        dtpLGFromDate.SelectedDate = DateTime.Now
        dtpLGToDate.SelectedDate = DateTime.Now

    End Sub

#End Region


End Class
