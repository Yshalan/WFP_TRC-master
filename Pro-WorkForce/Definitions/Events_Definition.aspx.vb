Imports SmartV.UTILITIES
Imports System.Data
Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports TA.Security
Imports SmartV.UTILITIES.ProjectCommon
Imports TA.Forms
Imports TA.Definitions
Imports System.Resources
Imports TA.Employees

Partial Class Definitions_Events_Definition
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEvents As Events
    Private objEvents_Groups As Events_Groups
    Private objEvents_Employees As Events_Employees
    Private objEmp_logicalGroup As Emp_logicalGroup
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objEmployee As Employee
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))

#End Region

#Region "Properties"

    Private Property CompanyId() As Integer
        Get
            Return ViewState("CompanyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CompanyId") = value
        End Set
    End Property

    Private Property EntityId() As Integer
        Get
            Return ViewState("EntityId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EntityId") = value
        End Set
    End Property

    Private Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Private Property EventId() As Integer
        Get
            Return ViewState("EventId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EventId") = value
        End Set
    End Property

    Private Property GroupId() As Integer
        Get
            Return ViewState("GroupId")
        End Get
        Set(ByVal value As Integer)
            ViewState("GroupId") = value
        End Set
    End Property

    Private Property NoOfEmp() As Integer
        Get
            Return ViewState("NoOfEmp")
        End Get
        Set(ByVal value As Integer)
            ViewState("NoOfEmp") = value
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
            Else
                Lang = CtlCommon.Lang.EN
            End If

            'ClearEventControls()
            FillGridEvents()
            'FillEvents()
            fillLogicalGroup()
            dteStartDate.SelectedDate = Date.Today
            dteEndDate.SelectedDate = Date.Today
            PageHeader1.HeaderText = ResourceManager.GetString("Events_Definition", CultureInfo)
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

        btnDeleteEvent.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdEvents.ClientID + "');")
        btnDeleteGroup.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdEventGroups.ClientID + "');")
        btnDeleteResource.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdResources.ClientID + "');")
    End Sub

#End Region

#Region "Methods"

#Region "Tab Event"

    Private Sub FillGridEvents()
        objEvents = New Events
        With objEvents
            dgrdEvents.DataSource = .GetAll_Details
            dgrdEvents.DataBind()
        End With
    End Sub

    Private Sub FillEventControls()
        objEvents = New Events
        With objEvents
            .EventId = EventId
            .GetByPK()
            txtEventName.Text = .EventName
            txtDescription.Text = .EventDescription
            EmployeeId = .ResposiblePerson
            objEmp_Filter.GetEmployeeInfo(EmployeeId)
            objEmp_Filter.EmployeeId = EmployeeId
            dteStartDate.SelectedDate = .StartDate
            dteEndDate.SelectedDate = .EndDate
        End With
    End Sub

    Private Sub ClearEventControls()
        txtEventName.Text = String.Empty
        txtDescription.Text = String.Empty
        dteStartDate.SelectedDate = Date.Today
        dteEndDate.SelectedDate = Date.Today
        objEmp_Filter.ClearValues()
        EventId = 0
        dgrdEventGroups.DataBind()
        dgrdResources.DataBind()
        ClearGroupControls()
        ClearResource()
        tab_AssignResources.Visible = False
        tab_EventGroups.Visible = False
    End Sub

    Protected Sub btnSaveEvent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveEvent.Click
        Dim err As Integer = -1
        objEvents = New Events
        With objEvents
            .EventName = txtEventName.Text
            .EventDescription = txtDescription.Text
            .StartDate = dteStartDate.SelectedDate
            .EndDate = dteEndDate.SelectedDate
            .ResposiblePerson = objEmp_Filter.EmployeeId
            If EventId = 0 Then
                err = .Add()
            Else
                .EventId = EventId
                err = .Update()
            End If

            If err = 0 Then
                FillGridEvents()
                tab_AssignResources.Visible = True
                tab_EventGroups.Visible = True
                RadCmbBxEmployees.Items.Clear()
                RadCmbBxEmployees.Text = String.Empty
                FillGroupGrid()
                FillGridResources()
                FillEventGroups()
                lblNoOFDefinedEmp.Visible = False
                lblNoOfEmps.Visible = False
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        End With
    End Sub

    Protected Sub btnClearEvent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearEvent.Click
        ClearEventControls()
        FillGridEvents()
    End Sub

    Protected Sub btnDeleteEvent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteEvent.Click
        Dim errNum As Integer = -1
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdEvents.Items
            If DirectCast(row.FindControl("chk"), System.Web.UI.WebControls.CheckBox).Checked Then
                Dim strCode As String = row.GetDataKeyValue("EventName").ToString()
                EventId = Convert.ToInt32(row.GetDataKeyValue("EventId").ToString())
                objEvents = New Events
                objEvents.EventId = EventId
                errNum = objEvents.Delete()
                With strBuilder

                End With
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGridEvents()
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")

        End If
    End Sub

    Protected Sub dgrdEvents_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEvents.NeedDataSource
        objEvents = New Events
        With objEvents
            dgrdEvents.DataSource = .GetAll_Details
        End With
    End Sub

    Protected Sub dgrdEvents_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdEvents.SelectedIndexChanged
        EventId = CType(dgrdEvents.SelectedItems(0), GridDataItem).GetDataKeyValue("EventId").ToString()
        tab_AssignResources.Visible = True
        tab_EventGroups.Visible = True
        RadCmbBxEmployees.Items.Clear()
        RadCmbBxEmployees.Text = String.Empty
        FillEventControls()
        FillGroupGrid()
        FillGridResources()
        FillEventGroups()
        lblNoOFDefinedEmp.Visible = False
        lblNoOfEmps.Visible = False
    End Sub

    Protected Sub lnkBuildSchedule_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        EventId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("EventId").ToString())
        Response.Redirect("../Definitions/EventAdvancedSchedule.aspx?EventId=" & EventId)
    End Sub

#End Region

#Region "Tab Groups"

    'Private Sub FillEvents()
    '    objEvents = New Events
    '    Dim dt As DataTable
    '    With objEvents
    '        dt = .GetAll
    '        CtlCommon.FillTelerikDropDownList(RadCmbBxEvent, dt, Lang)
    '    End With
    'End Sub

    Private Sub FillGroupGrid()
        objEvents_Groups = New Events_Groups
        With objEvents_Groups
            .FK_EventId = EventId
            dgrdEventGroups.DataSource = .GetAll_Details
            dgrdEventGroups.DataBind()
        End With
    End Sub

    Private Sub FillGroupControls()
        objEvents_Groups = New Events_Groups
        With objEvents_Groups
            .FK_EventId = EventId
            .FK_GroupId = GroupId
            .GetByPK()
            'RadCmbBxEvent.SelectedValue = EventId
            RadCmbBxGroup.SelectedValue = GroupId
            txtNoOfEmployees.Text = .NumberOfEmployees
        End With
    End Sub

    Private Sub fillLogicalGroup()
        objEmp_logicalGroup = New Emp_logicalGroup
        Dim dt As DataTable
        With objEmp_logicalGroup
            dt = .GetAll_ForDDL
            CtlCommon.FillTelerikDropDownList(RadCmbBxGroup, dt, Lang)
        End With
    End Sub

    Private Sub ClearGroupControls()
        'RadCmbBxEvent.SelectedValue = -1
        RadCmbBxGroup.SelectedValue = -1
        txtNoOfEmployees.Text = String.Empty
        GroupId = 0
        dgrdEventGroups.DataBind()
    End Sub

    Protected Sub btnSaveGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveGroup.Click
        objEvents_Groups = New Events_Groups
        Dim err As Integer = -1
        With objEvents_Groups
            .FK_EventId = EventId
            .FK_GroupId = RadCmbBxGroup.SelectedValue
            .NumberOfEmployees = txtNoOfEmployees.Text
            If Not GroupId = 0 AndAlso Not EventId = 0 Then
                err = .Update
            Else
                err = .Add()
            End If
            If err = 0 Then
                FillGroupGrid()
                FillEventGroups()
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")

            ElseIf err = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("GroupExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        End With
    End Sub

    Protected Sub btnClearGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearGroup.Click
        ClearGroupControls()
    End Sub

    Protected Sub btnDeleteGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteGroup.Click
        Dim errNum As Integer = -1
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdEventGroups.Items
            If DirectCast(row.FindControl("chk"), System.Web.UI.WebControls.CheckBox).Checked Then
                Dim strCode As String = row.GetDataKeyValue("GroupName").ToString()
                EventId = Convert.ToInt32(row.GetDataKeyValue("FK_EventId").ToString())
                GroupId = Convert.ToInt32(row.GetDataKeyValue("FK_GroupId").ToString())
                objEvents_Groups = New Events_Groups
                objEvents_Groups.FK_EventId = EventId
                objEvents_Groups.FK_GroupId = GroupId
                errNum = objEvents_Groups.Delete()
                With strBuilder

                End With
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGroupGrid()
            ClearGroupControls()
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")

        End If

    End Sub

    Protected Sub dgrdEventGroups_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdEventGroups.SelectedIndexChanged
        EventId = CType(dgrdEventGroups.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_EventId").ToString()
        GroupId = CType(dgrdEventGroups.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_GroupId").ToString()
        FillGroupControls()
    End Sub
#End Region

#Region "Tab Resources"

    Private Sub FillEventGroups()
        objEvents_Employees = New Events_Employees
        Dim dt As DataTable
        With objEvents_Employees
            .FK_EventId = EventId
            dt = .Get_EventGroups
            CtlCommon.FillTelerikDropDownList(RadCmbBxEventGroup, dt, Lang)
        End With
    End Sub

    Private Sub FillGridResources()
        objEvents_Employees = New Events_Employees
        With objEvents_Employees
            .FK_EventId = EventId
            If Not RadCmbBxEventGroup.SelectedValue = Nothing Then
                If Not RadCmbBxEventGroup.SelectedValue = -1 Then
                    .FK_GroupId = RadCmbBxEventGroup.SelectedValue
                Else
                    .FK_GroupId = 0
                End If
            End If
            dgrdResources.DataSource = .Get_All_Details
            dgrdResources.DataBind()
        End With
    End Sub

    Private Sub FillResourceControls()
        objEvents_Employees = New Events_Employees
        With objEvents_Employees
            .FK_EventId = EventId
            .FK_EmployeeId = EmployeeId
            .GetByPK()
            RadCmbBxEventGroup.SelectedValue = GroupId
            FillEmployeeDDL()
            RadCmbBxEmployees.SelectedValue = EmployeeId
        End With
    End Sub

    Private Sub FillEmployeeDDL()
        objEmployee = New Employee
        Dim dt As DataTable
        With objEmployee
            .FK_LogicalGroup = RadCmbBxEventGroup.SelectedValue
            .FK_EntityId = -1
            dt = .GetByLogicalGroup()
            CtlCommon.FillTelerikDropDownList(RadCmbBxEmployees, dt, Lang)
        End With
    End Sub

    Private Sub ClearResource()
        RadCmbBxEventGroup.SelectedValue = -1
        RadCmbBxEmployees.SelectedValue = -1
        dgrdResources.DataBind()
        lblNoOFDefinedEmp.Visible = False
        lblNoOfEmps.Visible = False
    End Sub

    Protected Sub btnSaveResource_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveResource.Click
        Dim err As Integer = -1
        objEvents_Employees = New Events_Employees
        objEvents_Groups = New Events_Groups
        objEvents_Groups.FK_EventId = EventId
        objEvents_Groups.FK_GroupId = RadCmbBxEventGroup.SelectedValue
        GroupId = RadCmbBxEventGroup.SelectedValue
        objEvents_Groups.GetByPK()
        NoOfEmp = objEvents_Groups.NumberOfEmployees

        With objEvents_Employees
            .FK_EventId = EventId
            .FK_EmployeeId = RadCmbBxEmployees.SelectedValue

        End With
        If Not dgrdResources.Items.Count = 0 Then
            For Each row As GridDataItem In dgrdResources.Items
                If Convert.ToInt32(row.GetDataKeyValue("GroupId").ToString()) = GroupId Then
                    If dgrdResources.Items.Count < NoOfEmp Then
                        err = objEvents_Employees.Add
                        Exit For
                    Else
                        err = -2
                    End If
                End If
            Next
        Else
            err = objEvents_Employees.Add
        End If
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGridResources()
            FillGroupGrid()
        ElseIf err = -2 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ExceedLimit", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub btnClearResource_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearResource.Click
        ClearResource()
    End Sub

    Protected Sub btnDeleteResource_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteResource.Click
        Dim errNum As Integer = -1
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdResources.Items
            If DirectCast(row.FindControl("chk"), System.Web.UI.WebControls.CheckBox).Checked Then
                Dim strCode As String = row.GetDataKeyValue("GroupName").ToString()
                GroupId = Convert.ToInt32(row.GetDataKeyValue("GroupId").ToString())
                EmployeeId = Convert.ToInt32(row.GetDataKeyValue("FK_EmployeeId").ToString())
                objEvents_Employees = New Events_Employees
                objEvents_Employees.FK_EmployeeId = EmployeeId
                objEvents_Employees.FK_EventId = EventId
                errNum = objEvents_Employees.Delete()
                With strBuilder

                End With
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            ClearResource()
            FillGridResources()
            FillGroupGrid()
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")

        End If
    End Sub

    Protected Sub dgrdResources_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdResources.SelectedIndexChanged
        EmployeeId = CType(dgrdResources.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_EmployeeId").ToString()
        GroupId = CType(dgrdResources.SelectedItems(0), GridDataItem).GetDataKeyValue("GroupId").ToString()
        FillResourceControls()
    End Sub

    Protected Sub RadCmbBxEventGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxEventGroup.SelectedIndexChanged
        GroupId = RadCmbBxEventGroup.SelectedValue
        If Not RadCmbBxEventGroup.SelectedValue = -1 Then
            objEvents_Groups = New Events_Groups
            With objEvents_Groups
                .FK_EventId = EventId
                .FK_GroupId = GroupId
                If Not EventId = 0 Then
                    .GetByPK()
                    lblNoOfEmps.Text = .NumberOfEmployees

                    lblNoOFDefinedEmp.Visible = True
                    lblNoOfEmps.Visible = True
                End If
            End With

        Else
            lblNoOFDefinedEmp.Visible = False
            lblNoOfEmps.Visible = False
        End If
        FillGridResources()
        FillEmployeeDDL()
    End Sub
#End Region

#End Region

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetEvent_FilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEvents.Skin))
    End Function

    Protected Sub dgrdEvents_OnItemCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles dgrdEvents.ItemCommand
        RadFilter_Event.FireApplyCommand()
        FillGridEvents()
    End Sub

End Class
