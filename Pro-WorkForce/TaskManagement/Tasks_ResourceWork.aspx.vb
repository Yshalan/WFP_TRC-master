Imports System.Data
Imports TA.Definitions
Imports SmartV.UTILITIES
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports TA.Security
Imports TA.Admin
Imports TA.Employees
Imports TA.TaskManagement

Partial Class Definitions_Tasks_ResourceWork
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objTasks_ResourcesWork As TA.TaskManagement.Project_Tasks_ResourcesWork
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

    Private objProjects As Projects
    Private objVersion As SmartV.Version.version
    Private objOrgCompany As OrgCompany
    Private objAPP_Settings As APP_Settings
    Private objSYSUsers As SYSUsers
    Private objEmployee As Employee
    Private objProject_Resources As Project_Resources
    Private objEmp_Designation As Emp_Designation

#End Region



    Private Property ResourceWorkId() As Integer
        Get
            Return ViewState("ResourceWorkId")

        End Get
        Set(ByVal value As Integer)
            ViewState("ResourceWorkId") = value
        End Set
    End Property

#Region "Page Events"

    Protected Sub Definitions_Tasks_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

        End If
        PageHeader1.HeaderText = ResourceManager.GetString("DefineResourceWork", CultureInfo)
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

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

#End Region

#Region "Methods"

    Private Sub FillGridTasksResources(TaskId As Integer)
        objTasks_ResourcesWork = New TA.TaskManagement.Project_Tasks_ResourcesWork
        Dim dt As DataTable
        With objTasks_ResourcesWork
            dt = .GetAllEmployeeTask(EmployeeFilter1.EmployeeId, TaskId)
            dgrdResourceWork.DataSource = dt
            dgrdResourceWork.DataBind()
        End With


        Dim objTask As New Project_tasks
        objTask.TaskId = TaskId
        objTask.GetByPK()
        dtpPlannedStartDate.SelectedDate = IIf(objTask.PlannedStartDate = DateTime.MinValue, Nothing, objTask.PlannedStartDate)
        dtpPlannedEndDate.SelectedDate = IIf(objTask.PlannedEndDate = DateTime.MinValue, Nothing, objTask.PlannedEndDate)
        dtpActualStartDate.SelectedDate = IIf(objTask.ActualStartDate = DateTime.MinValue, Nothing, objTask.ActualStartDate)
        dtpActualEndDate.SelectedDate = IIf(objTask.ActualEndDate = DateTime.MinValue, Nothing, objTask.ActualEndDate)
        txtDescription.Text = objTask.TaskDescription
        Select Case objTask.Priority
            Case 1
                txtPriority.Text = "Low"
            Case 2
                txtPriority.Text = "Medium"
            Case 3
                txtPriority.Text = "High"
            Case Else
        End Select
        txtStatus.Text = IIf(objTask.IsCompleted, "Completed", "Not Completed")
        txtTotalcompletionpercentage.Text = objTask.Totalcompletionpercentage * 100

    End Sub

#End Region

    Protected Sub RadTasks_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        FillGridTasksResources(Convert.ToInt32(RadTasks.SelectedValue))
    End Sub

    Protected Sub EmployeeFilter1_eventEmployeeSelect(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Dim objTasks As New Project_Tasks_ResourcesWork
        CtlCommon.FillTelerikDropDownList(RadTasks, objTasks.GetForDDL(EmployeeFilter1.EmployeeId), Lang)
    End Sub

    Protected Sub dgrdResourceWork_SelectedIndexChanged(sender As Object, e As EventArgs)
        ResourceWorkId = CInt(CType(dgrdResourceWork.SelectedItems(0), GridDataItem).GetDataKeyValue("ResourceWorkId").ToString())
        Dim obj As New Project_Tasks_ResourcesWork()
        obj.ResourceWorkId = Convert.ToInt32(ResourceWorkId)
        obj.GetByPK()
        dtpEndDateTime.SelectedDate = IIf(obj.EndDateTime = DateTime.MinValue, Nothing, obj.EndDateTime)
        dtpStartDateTime.SelectedDate = IIf(obj.StartDateTime = DateTime.MinValue, Nothing, obj.StartDateTime)
        txtRemarks.Text = obj.Remarks
        txtCompletionPercentage.Text = (obj.completionpercentage * 100)
    End Sub

    Public Sub ClearControls()
        dtpEndDateTime.SelectedDate = Nothing
        dtpStartDateTime.SelectedDate = Nothing
        txtRemarks.Text = ""
        txtCompletionPercentage.Text = ""
        ResourceWorkId = 0
    End Sub


    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdResourceWork.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim _ResourceWorkId As Integer = Convert.ToInt32(row.GetDataKeyValue("ResourceWorkId").ToString())
                objTasks_ResourcesWork = New Project_Tasks_ResourcesWork()
                objTasks_ResourcesWork.ResourceWorkId = _ResourceWorkId
                errNum = objTasks_ResourcesWork.Delete()
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGridTasksResources(Convert.ToInt32(RadTasks.SelectedValue))
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        End If
        ClearControls()

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)



        If (String.IsNullOrEmpty(RadTasks.SelectedValue)) Then
            If Lang = CtlCommon.Lang.AR Then
                CtlCommon.ShowMessage(Page, "الرجاء اختيار مهمات العمل.", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please Select Tasks.", "info")
            End If
            Return
        End If

        If (Convert.ToInt32(RadTasks.SelectedValue) <= 0) Then
            If Lang = CtlCommon.Lang.AR Then
                CtlCommon.ShowMessage(Page, "الرجاء اختيار مهمات العمل.", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please Select Tasks.", "info")
            End If
            Return
        End If


        If (String.IsNullOrEmpty(txtCompletionPercentage.Text)) Then
            If Lang = CtlCommon.Lang.AR Then
                CtlCommon.ShowMessage(Page, "الرجاء ادخال نسبة الإنجاز .", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please Enter Completion Percentage.", "info")
            End If
            Return
        End If

        If (dtpEndDateTime.DbSelectedDate Is Nothing) Then
            If Lang = CtlCommon.Lang.AR Then
                CtlCommon.ShowMessage(Page, "الرجاء اختيار التاريخ", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please Select End Date.", "info")
            End If
            Return
        End If

        If (dtpStartDateTime.DbSelectedDate Is Nothing) Then
            If Lang = CtlCommon.Lang.AR Then
                CtlCommon.ShowMessage(Page, "الرجاء اختيار تاريخ البداية.", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please Select Start Date.", "info")
            End If
            Return
        End If

        If (dtpStartDateTime.DbSelectedDate >= dtpEndDateTime.DbSelectedDate) Then
            If Lang = CtlCommon.Lang.AR Then
                CtlCommon.ShowMessage(Page, "التاريخ المتوقع وتاريخ الإنتهاء يجب ان يكون أكبر أو يساوي تاريخ البدئ المتوقع أ, تاريخ البدايه.", "info")
            Else
                CtlCommon.ShowMessage(Page, "Planned End Date Should be Greater Than or Equal to Planned Start Date.", "info")
            End If
            Return

        End If
        Try
            If (Convert.ToDouble(txtCompletionPercentage.Text) < 0 Or Convert.ToDouble(txtCompletionPercentage.Text) > 100) Then
                If Lang = CtlCommon.Lang.AR Then
                    CtlCommon.ShowMessage(Page, " الرجاء إدخال النسبة التي تم إنجازها.", "info")
                Else
                    CtlCommon.ShowMessage(Page, "Please Enter Valid Completion Percentage.", "info")
                End If
                Return
            End If
        Catch ex As Exception
            If Lang = CtlCommon.Lang.AR Then
                CtlCommon.ShowMessage(Page, "الرجاء إدخال النسبة التي تم إنجازها.", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please Enter Valid Completion Percentage.", "info")
            End If
            Return
        End Try



        Dim obj As New Project_Tasks_ResourcesWork()
        obj.StartDateTime = dtpStartDateTime.DbSelectedDate
        obj.EndDateTime = dtpEndDateTime.DbSelectedDate
        obj.Remarks = txtRemarks.Text
        obj.completionpercentage = Convert.ToDouble(txtCompletionPercentage.Text)
        obj.FK_TaskId = Convert.ToInt32(RadTasks.SelectedValue)
        obj.FK_EmployeeId = EmployeeFilter1.EmployeeId


        If (obj.StartDateTime > obj.EndDateTime) Then
            If Lang = CtlCommon.Lang.AR Then
                CtlCommon.ShowMessage(Page, "تاريخ او وقت البداية أكبر من تارخ أو وقت النهاية.", "info")
            Else
                CtlCommon.ShowMessage(Page, "Start Date Time greater than End Date Time.", "info")
            End If
            Return
        End If

        Dim errorNum As Integer = -1
        If (ResourceWorkId > 0) Then
            obj.ResourceWorkId = ResourceWorkId
            errorNum = obj.Update()
        Else
            errorNum = obj.Add()
        End If

        If errorNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGridTasksResources(Convert.ToInt32(RadTasks.SelectedValue))
            ClearControls()
        ElseIf errorNum = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("StartEndDateTimeExists", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If


    End Sub


End Class
