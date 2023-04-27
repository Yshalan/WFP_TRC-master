Imports TA.Definitions
Imports SmartV.UTILITIES
Imports System.Data
Imports TA.Employees
Imports TA.TaskManagement

Partial Class Definitions_TasksPopUp
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Protected dir, textalign As String

    Private objProject_tasks As Project_tasks
    Private objProject_Tasks_Resources As Project_Tasks_Resources
    Private objProject_Tasks_predecessor As Project_Tasks_predecessor
    Private objProject_Tasks_ResourcesWork As Project_Tasks_ResourcesWork
    Private objEmployee As Employee

#End Region

#Region "Public Properties"

    Public Property TaskId() As Integer
        Get
            Return ViewState("TaskId")
        End Get
        Set(ByVal value As Integer)
            ViewState("TaskId") = value
        End Set
    End Property

    Public Property ParentTaskId() As Integer
        Get
            Return ViewState("ParentTaskId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ParentTaskId") = value
        End Set
    End Property

    Public Property ProjectId() As Integer
        Get
            Return ViewState("ProjectId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ProjectId") = value
        End Set
    End Property

    Public Property dtResources() As DataTable
        Get
            Return ViewState("dtResources")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtResources") = value
        End Set
    End Property

    Public Property dtPredecessor() As DataTable
        Get
            Return ViewState("dtPredecessor")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtPredecessor") = value
        End Set
    End Property

    Public Property ClickType() As String
        Get
            Return ViewState("ClickType")
        End Get
        Set(ByVal value As String)
            ViewState("ClickType") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Definitions_TasksPopUp_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            SessionVariables.CultureInfo = "en-US"
            dir = "ltr"
            textalign = "left"
        Else
            If SessionVariables.CultureInfo = "en-US" Then
                SessionVariables.CultureInfo = "en-US"
                dir = "ltr"
                textalign = "left"
            Else
                SessionVariables.CultureInfo = "ar-JO"
                dir = "rtl"
                textalign = "right"
            End If
        End If
        Page.Culture = SessionVariables.CultureInfo
        Page.UICulture = SessionVariables.CultureInfo
    End Sub

    Protected Sub Definitions_TasksPopUp_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            If (SessionVariables.CultureInfo Is Nothing) Then
                SessionVariables.CultureInfo = "en-US"
            Else
                If SessionVariables.CultureInfo = "en-US" Then
                    SessionVariables.CultureInfo = "en-US"
                Else
                    SessionVariables.CultureInfo = "ar-JO"
                End If
            End If

            ProjectId = Page.Request.QueryString("ProjectId")
            ClickType = Page.Request.QueryString("ClickType")

            FillProiorityddl()
            FillPredecessorddl()
            FillRelatedTaskddl()
            FillddlParent()
            Create_dtResources()
            Create_dtPredecessor()

            If ClickType = "Edit" Then
                TaskId = Page.Request.QueryString("TaskId")
                FillControls()
            ElseIf ClickType = "Child" Then
                ParentTaskId = Page.Request.QueryString("ParentId")
                objProject_tasks = New Project_tasks
                With objProject_tasks
                    .TaskId = ParentTaskId
                    .GetByPK()
                    dtpPlannedStart.SelectedDate = .PlannedStartDate
                    dtpPlannedEnd.SelectedDate = .PlannedEndDate
                End With


            End If

            Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            scriptManager__1.RegisterPostBackControl(btnSave)

        End If

    End Sub

    Protected Sub btnAddResource_Click(sender As Object, e As EventArgs) Handles btnAddResource.Click
        Dim dr As DataRow
        objEmployee = New Employee

        With objEmployee
            .EmployeeId = EmployeeFilter.EmployeeId
            .GetByPK()

            dr = dtResources.NewRow
            dr("FK_EmployeeId") = .EmployeeId
            dr("EmployeeNo") = .EmployeeNo
            dr("EmployeeName") = .EmployeeName
            dr("EmployeeArabicName") = .EmployeeArabicName
            dr("ProjectInvolvement") = txtInvolvement.Text
            dtResources.Rows.Add(dr)
        End With
        FillResourcesGrid()

    End Sub

    Protected Sub btnRemoveResource_Click(sender As Object, e As EventArgs) Handles btnRemoveResource.Click
        Dim err As Integer = -1
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdResources.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intFK_EmployeeId As Integer = Convert.ToInt32(row.GetDataKeyValue("FK_EmployeeId").ToString())
                dtResources.Rows.Remove(dtResources.Select("FK_EmployeeId = " & intFK_EmployeeId)(0))
            End If
        Next
        FillResourcesGrid()
    End Sub

    Protected Sub btnAddPredecessors_Click(sender As Object, e As EventArgs) Handles btnAddPredecessors.Click
        objProject_Tasks_predecessor = New Project_Tasks_predecessor
        objProject_tasks = New Project_tasks

        Dim dr As DataRow

        For Each item In dtPredecessor.Rows
            If item("TaskId") = radcmbxRelatedTask.SelectedValue Then
                CtlCommon.ShowMessage(Me.Page, "Related Task Already Exist")
                Exit Sub
            End If
        Next
        objProject_tasks.TaskId = radcmbxRelatedTask.SelectedValue
        objProject_tasks.GetByPK()

        dr = dtPredecessor.NewRow
        dr("TaskId") = radcmbxRelatedTask.SelectedValue
        dr("TaskName") = objProject_tasks.TaskName
        dr("RelationType") = radcmbxPredecessors.SelectedValue
        dr("TaskSequence") = objProject_tasks.TaskSequence

        dtPredecessor.Rows.Add(dr)
        FillPredecessorGrid()
    End Sub

    Protected Sub btnRemovePredecessors_Click(sender As Object, e As EventArgs) Handles btnRemovePredecessors.Click
        Dim err As Integer = -1
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdPredeccessors.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intTaskId As Integer = Convert.ToInt32(row.GetDataKeyValue("TaskId").ToString())
                dtPredecessor.Rows.Remove(dtPredecessor.Select("TaskId = " & intTaskId)(0))
            End If
        Next
        FillPredecessorGrid()
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        objProject_tasks = New Project_tasks
        Dim err As Integer = -1
        With objProject_tasks
            .FK_ProjectId = ProjectId
            .TaskName = txtTaskName.Text
            .TaskDescription = txtTaskDesc.Text
            .PlannedStartDate = dtpPlannedStart.DbSelectedDate
            .PlannedEndDate = dtpPlannedEnd.DbSelectedDate
            .Priority = RadCmbBxProiority.SelectedValue
            If radnumCompletePercentage.Text = String.Empty Then
                radnumCompletePercentage.Text = 0
            End If
            .Totalcompletionpercentage = Convert.ToInt32(radnumCompletePercentage.Text) / 100
            .TaskSequence = "0"

            If ClickType = "Child" Then
                radcmbxParent.SelectedValue = ParentTaskId
                .FK_ParentTaskId = ParentTaskId
            Else
                .FK_ParentTaskId = radcmbxParent.SelectedValue
            End If

            If TaskId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add
                TaskId = .TaskId
            Else
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .TaskId = TaskId
                err = .Update
            End If
        End With

        If err = 0 Then
            If dtResources.Rows.Count > 0 Then
                objProject_Tasks_Resources = New Project_Tasks_Resources
                With objProject_Tasks_Resources
                    For Each item In dtResources.Rows
                        .FK_TaskId = TaskId
                        .FK_EmployeeId = item("FK_EmployeeId")
                        .Involvmentpercentage = item("ProjectInvolvement")
                        .CREATED_BY = SessionVariables.LoginUser.ID
                        .Add()
                    Next
                End With
            End If
        End If

        If err = 0 Then
            If dtPredecessor.Rows.Count > 0 Then
                objProject_Tasks_predecessor = New Project_Tasks_predecessor
                With objProject_Tasks_predecessor
                    For Each item In dtPredecessor.Rows
                        .FK_TaskId = TaskId
                        .FK_predecessorTask = item("TaskId")
                        .RelationType = item("RelationType")
                        .CREATED_BY = SessionVariables.LoginUser.ID
                        .Add()
                    Next
                End With
            End If
        End If
        If err = 0 Then
            'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "close", "CloseAndRefresh();", True)
        Else
            'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "Error")
        End If


    End Sub

    Protected Sub btnRetrive_Click(sender As Object, e As EventArgs) Handles btnRetrive.Click
        Dim NotIn As Boolean = False
        Dim TaskSequence As Integer
        objProject_tasks = New Project_tasks
        If (txtRetriveSequence.Text <> String.Empty) Then

            For Index As Integer = 0 To radcmbxParent.Items.Count - 1
                If radcmbxParent.Items(Index).Text.Split(" - ")(0).ToLower = txtRetriveSequence.Text.ToLower Then
                    radcmbxParent.SelectedValue = radcmbxParent.Items(Index).Value
                    TaskSequence = radcmbxParent.SelectedValue
                    objProject_tasks.TaskId = TaskId
                    objProject_tasks.TaskSequence = TaskSequence
                    objProject_tasks.GetByTaskId_Sequence()
                    NotIn = True
                End If
            Next

        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillRelatedTaskddl()
        objProject_tasks = New Project_tasks
        Dim dt As DataTable
        objProject_tasks.FK_ProjectId = ProjectId
        dt = objProject_tasks.Get_by_FK_ProjectId
        'If dt.Rows.Count > 0 Then
        ProjectCommon.FillRadComboBox(radcmbxRelatedTask, dt, "TaskName", "TaskName")
        'End If

    End Sub

    Private Sub FillProiorityddl()
        Dim item1 As New RadComboBoxItem
        Dim item2 As New RadComboBoxItem
        Dim item3 As New RadComboBoxItem

        item1.Text = "Low"
        item1.Value = "1"

        item2.Text = "Medium"
        item2.Value = "2"

        item3.Text = "High"
        item3.Value = "3"

        RadCmbBxProiority.Items.Add(item1)
        RadCmbBxProiority.Items.Add(item2)
        RadCmbBxProiority.Items.Add(item3)

    End Sub

    Private Sub FillResourcesGrid()
        dgrdResources.DataSource = dtResources
        dgrdResources.DataBind()

    End Sub

    Private Sub FillPredecessorddl()
        Dim item1 As New RadComboBoxItem
        Dim item2 As New RadComboBoxItem
        Dim item3 As New RadComboBoxItem
        Dim item4 As New RadComboBoxItem
        item1.Text = "Start to Start"
        item1.Value = "SS"

        item2.Text = "Start to Finish"
        item2.Value = "SF"

        item3.Text = "Finish to Start"
        item3.Value = "FS"

        item4.Text = "Finish to Finish"
        item4.Value = "FF"

        radcmbxPredecessors.Items.Add(item3)
        radcmbxPredecessors.Items.Add(item1)
        radcmbxPredecessors.Items.Add(item2)
        radcmbxPredecessors.Items.Add(item4)
    End Sub

    Private Sub FillPredecessorGrid()
        dgrdPredeccessors.DataSource = dtPredecessor
        dgrdPredeccessors.DataBind()

    End Sub

    Private Sub Create_dtResources()
        dtResources = New DataTable

        dtResources.Columns.Add("FK_EmployeeId")
        dtResources.Columns.Add("EmployeeNo")
        dtResources.Columns.Add("EmployeeName")
        dtResources.Columns.Add("EmployeeArabicName")
        dtResources.Columns.Add("ProjectInvolvement")

    End Sub

    Private Sub Create_dtPredecessor()
        dtPredecessor = New DataTable

        dtPredecessor.Columns.Add("TaskId")
        dtPredecessor.Columns.Add("TaskName")
        dtPredecessor.Columns.Add("RelationType")
        dtPredecessor.Columns.Add("TaskSequence")

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdResources.Skin))
    End Function

    Protected Function GetFilterIcon1() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdPredeccessors.Skin))
    End Function

    Private Sub FillControls()
        objProject_tasks = New Project_tasks
        With objProject_tasks
            .TaskId = TaskId
            .GetByPK()
            txtTaskName.Text = .TaskName
            txtTaskDesc.Text = .TaskDescription
            dtpPlannedStart.SelectedDate = .PlannedStartDate
            dtpPlannedEnd.SelectedDate = .PlannedEndDate
            lblActualStartVal.Text = IIf(.ActualStartDate = DateTime.MinValue, Nothing, .ActualStartDate.ToString("dd/MM/yyyy HH:mm"))
            lblActualEndVal.Text = IIf(.ActualEndDate = DateTime.MinValue, Nothing, .ActualEndDate.ToString("dd/MM/yyyy HH:mm"))
            RadCmbBxProiority.SelectedValue = .Priority
            radnumCompletePercentage.Text = (.Totalcompletionpercentage) * 100
            If Not .FK_ParentTaskId = 0 Then
                radcmbxParent.SelectedValue = .FK_ParentTaskId
            End If
        End With


        objProject_Tasks_Resources = New Project_Tasks_Resources
        With objProject_Tasks_Resources
            .FK_TaskId = TaskId
            dtResources = .Get_By_FK_TaskId()
            FillResourcesGrid()
        End With

        objProject_Tasks_predecessor = New Project_Tasks_predecessor
        With objProject_Tasks_predecessor
            .FK_TaskId = TaskId
            dtPredecessor = .Get_By_FK_TaskId
            FillPredecessorGrid()
        End With
    End Sub

    Private Sub FillddlParent()
        objProject_tasks = New Project_tasks
        Dim dt As DataTable
        With objProject_tasks
            .FK_ProjectId = ProjectId
            dt = .Get_by_FK_ProjectId()
            If (dt IsNot Nothing) Then
                If (dt.Rows.Count > 0) Then
                    Dim dtSource As New DataTable
                    dtSource.Columns.Add("TaskId")
                    dtSource.Columns.Add("TaskName")

                    Dim drRow As DataRow
                    drRow = dtSource.NewRow()

                    drRow(1) = ResourceManager.GetString("PleaseSelect", CultureInfo)
                    dtSource.Rows.Add(drRow)
                    For Item As Integer = 0 To dt.Rows.Count - 1
                        Dim drSource As DataRow
                        drSource = dtSource.NewRow
                        Dim dcCell1 As New DataColumn
                        Dim dcCell2 As New DataColumn
                        dcCell1.ColumnName = "TaskId"
                        dcCell2.ColumnName = "TaskName"
                        dcCell1.DefaultValue = dt.Rows(Item)(0)
                        'Add Space before and after the Dash (-) for search Emp No that contains (-)
                        dcCell2.DefaultValue = dt.Rows(Item)("TaskSequence") + " - " + dt.Rows(Item)("TaskName")
                        drSource("TaskId") = dcCell1.DefaultValue
                        drSource("TaskName") = dcCell2.DefaultValue
                        dtSource.Rows.Add(drSource)
                    Next

                    With radcmbxParent
                        .DataSource = dtSource
                        .DataValueField = dtSource.Columns(0).ColumnName
                        .DataTextField = dtSource.Columns(1).ColumnName
                        .DataBind()
                    End With
                Else
                    ProjectCommon.FillRadComboBox(radcmbxParent, dt, "TaskName", "TaskName")
                End If
            Else
                ProjectCommon.FillRadComboBox(radcmbxParent, dt, "TaskName", "TaskName")
            End If
        End With

    End Sub

#End Region

    
End Class
