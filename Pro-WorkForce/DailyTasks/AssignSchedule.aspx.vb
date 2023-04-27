Imports TA.OrgCompany
Imports SmartV.UTILITIES
Imports TA.Lookup
Imports System.Data
Imports TA.Employees
Imports TA.Definitions

Partial Class Admin_AssignSchedule
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

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

        showHide(chckTemporary.Checked)

        If Not Page.IsPostBack Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")

            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            PageHeader1.HeaderText = ResourceManager.GetString("AssignEmpSchedule", CultureInfo)
            rfvScheduletype.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)

            Me.dtpFromdate.SelectedDate = Today
            Me.dtpEndDate.SelectedDate = Today
            'FillSchedule()

            EmployeeFilterUC.CompanyRequiredFieldValidationGroup = btnSave.ValidationGroup
        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim errornum As Integer
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If RadCmbBxSchedules.SelectedValue <> -1 Then
            Dim arr As String()
            arr = RadCmbBxSchedules.SelectedValue.Split(",")
            Dim flag As Boolean = False

            For Each item As ListItem In cblEmpList.Items
                If item.Selected = True Then
                    flag = True
                    Dim objEmp_WorkSchedule As New Emp_WorkSchedule
                    objEmp_WorkSchedule.FK_EmployeeId = item.Value
                    objEmp_WorkSchedule.FK_ScheduleId = RadCmbBxSchedules.SelectedValue.ToString().Split(",")(0)
                    objEmp_WorkSchedule.ScheduleType = RadCmbBxScheduletype.SelectedValue
                    objEmp_WorkSchedule.FromDate = dtpFromdate.SelectedDate
                    objEmp_WorkSchedule.ToDate = IIf(dtpEndDate.SelectedDate Is Nothing Or Not chckTemporary.Checked, DateTime.MinValue, dtpEndDate.SelectedDate)
                    objEmp_WorkSchedule.IsTemporary = chckTemporary.Checked

                    errornum = objEmp_WorkSchedule.AssignSchedule()

                End If
            Next


            If errornum = 0 Then

                If flag = False Then
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("EmployeeSelect", CultureInfo), "info")
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                    ClearAll()
                End If
            ElseIf errornum = -99 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("DateExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")

            End If

        End If

    End Sub

    Private Sub showHide(ByVal status As Boolean, Optional ByVal order As Integer = 0)
        pnlEndDate.Visible = status
    End Sub

#End Region

#Region "Methods"

    Protected Sub RadCmbBxScheduletype_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxScheduletype.SelectedIndexChanged
        FillSchedule()
    End Sub

    Public Sub FillEmployee()

        cblEmpList.Items.Clear()
        cblEmpList.Text = String.Empty
        hlViewEmployee.Visible = False

        If EmployeeFilterUC.CompanyId <> 0 Then
            Dim objEmployee As New Employee
            objEmployee.FK_CompanyId = EmployeeFilterUC.CompanyId
            If (EmployeeFilterUC.EntityId <> 0) Then
                objEmployee.FK_EntityId = EmployeeFilterUC.EntityId
            Else
                objEmployee.FK_EntityId = -1
            End If
            Dim dt As DataTable = objEmployee.GetEmpByCompany

            If (dt IsNot Nothing) Then
                For Each row As DataRow In dt.Rows
                    cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"

                Next
            End If
        End If
    End Sub

    Private Sub FillSchedule()
        Dim objWorkSchedule As New WorkSchedule()
        If (RadCmbBxScheduletype.SelectedValue = 1) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(1))
        ElseIf (RadCmbBxScheduletype.SelectedValue = 2) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(2))
        ElseIf (RadCmbBxScheduletype.SelectedValue = 3) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(3))
        End If


    End Sub

    Private Sub ClearAll()
        ' Clear the controls
        EmployeeFilterUC.ClearValues()
        RadCmbBxSchedules.SelectedValue = -1
        RadCmbBxScheduletype.SelectedValue = -1
        chckTemporary.Checked = False
        cblEmpList.Items.Clear()
        pnlEndDate.Visible = False
        dtpFromdate.SelectedDate = Today
        dtpEndDate.SelectedDate = Today

    End Sub

    Public Sub CompanyChanged()
        EmployeeFilterUC.FillEntity()
        FillEmployee()
    End Sub

    Private Function GetNavigateURL(ByVal CompanyId As String, ByVal EntityId As String) As String
        Dim res As String = "javascript:open_window('../Reports/EmployeeInfo.aspx?company={0}&entity={1}','',700,400)"

        Return String.Format(res, CompanyId, IIf(String.IsNullOrEmpty(EntityId), "-1", EntityId))
    End Function

#End Region

End Class