Imports System.Data
Imports Telerik.Web.UI
Imports TA.Employees
Imports SmartV.UTILITIES
Imports TA.Admin
Imports TA.Lookup
Imports TA.Definitions

Partial Class EmpWorkSchedule
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objOrgCompany As New OrgCompany
    Private objOrgEntity As New OrgEntity
    Private objWorkSchedule As New WorkSchedule
    Dim objEmp_WorkSchedule As New Emp_WorkSchedule
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Page Events"

    Protected Sub chckBxTemporary_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chckBxTemporary.CheckedChanged
        If chckBxTemporary.Checked Then
            manageTodate(True)
        Else
            manageTodate(False)
        End If

    End Sub

    Protected Sub RadComboBoxCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxCompany.SelectedIndexChanged
        ClearAll()
        fillEntity()
        fillemployees()
        FillSchedules()
    End Sub

    Protected Sub RadComboBoxWorkSchedules_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxWorkSchedules.SelectedIndexChanged
        RetrieveScheduleTypeName()
    End Sub

    Protected Sub RadComboBoxEntity_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxEntity.SelectedIndexChanged
        fillemployees()
        FillSchedules()
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
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            CtlCommon.FillTelerikDropDownList(RadComboBoxCompany, objOrgCompany.GetAllforddl, Lang)
            fillEntity()
            CtlCommon.FillTelerikDropDownList(RadComboBoxWorkSchedules, objWorkSchedule.GetAllFORDDL, Lang)
            manageTodate(False)
            'FillSchedules()
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim errno As Integer = -1
        Dim count As Integer = 0
        For Each lst As ListItem In chkLstEmployee.Items
            If lst.Selected Then
                count += 1
                Exit For
            End If
        Next
        If count = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpSelectLeast", CultureInfo) + " " + RadComboBoxCompany.SelectedItem.Text, "info")
            Exit Sub
        End If
        For Each lst As ListItem In chkLstEmployee.Items
            If lst.Selected Then
                With objEmp_WorkSchedule
                    .FK_EmployeeId = CInt(lst.Value)
                    .FK_ScheduleId = RadComboBoxWorkSchedules.SelectedValue
                    .FromDate = dteStartDate.SelectedDate
                    objWorkSchedule.ScheduleId() = RadComboBoxWorkSchedules.SelectedValue
                    .ScheduleType = objWorkSchedule.GetByPK().ScheduleType
                    .IsTemporary = chckBxTemporary.Checked
                    If chckBxTemporary.Checked Then
                        .ToDate = dteEndDate.SelectedDate
                    End If
                    errno = .Add()
                End With
            End If

        Next
        If errno = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
        ElseIf errno = -4 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("InsertDuplication", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If

    End Sub

#End Region

#Region "Methods"

    Public Sub ClearAll()
        'RadComboBoxCompany.SelectedValue = -1
        RadComboBoxEntity.SelectedValue = -1
        RadComboBoxWorkSchedules.SelectedValue = -1
        dteEndDate.Clear()
        dteStartDate.Clear()
        chkLstEmployee.Items.Clear()
        chckBxTemporary.Checked = False
        manageTodate(False)
    End Sub

    Public Sub fillemployees()

        If RadComboBoxEntity.SelectedValue > 0 Then
            With objOrgEntity
                .EntityId = RadComboBoxEntity.SelectedValue
                CtlCommon.FillCheckBox(chkLstEmployee, .GetEmployeesByOrgEntity)
            End With

        End If
        If RadComboBoxEntity.SelectedValue = 0 And RadComboBoxCompany.SelectedValue > 0 Then
            With objOrgCompany
                .CompanyId = RadComboBoxCompany.SelectedValue
                CtlCommon.FillCheckBox(chkLstEmployee, .GetEmployeesByOrgCompany)
            End With
        End If
    End Sub

    Public Sub manageTodate(ByVal Status As Boolean)
        ReqEndDate.Enabled = Status
        lblEndDate.Visible = Status
        dteEndDate.Visible = Status

    End Sub

    Public Sub fillEntity()
        objOrgEntity = New OrgEntity
        With objOrgEntity
            .FK_CompanyId = RadComboBoxCompany.SelectedValue
            CtlCommon.FillTelerikDropDownList(RadComboBoxEntity, objOrgEntity.SelectAllForDDLByCompany())
            RadComboBoxEntity.SelectedValue = 0
        End With
    End Sub

    Public Sub FillSchedules()
        Dim dt As DataTable = Nothing
        With objEmp_WorkSchedule
            dt = .GetAllByEmployeeCompany(RadComboBoxCompany.SelectedValue)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                RadComboBoxWorkSchedules.SelectedValue = dt.Rows(0)("FK_ScheduleId")
                dteStartDate.SelectedDate = dt.Rows(0)("FromDate")
                If dt.Rows(0)("IsTemporary") Then
                    chckBxTemporary.Checked = True
                    manageTodate(True)
                    dteEndDate.SelectedDate = dt.Rows(0)("ToDate")
                Else
                    chckBxTemporary.Checked = False
                    manageTodate(False)
                End If
                For Each dr As DataRow In dt.Rows
                    For Each lst As ListItem In chkLstEmployee.Items
                        If lst.Value = dr("FK_EmployeeId") Then
                            lst.Selected = True
                        End If
                    Next
                Next
            End If
            RetrieveScheduleTypeName()
        End With
    End Sub

    Public Sub RetrieveScheduleTypeName()
        If RadComboBoxWorkSchedules.SelectedValue > 0 Then
            With objWorkSchedule
                .ScheduleId() = RadComboBoxWorkSchedules.SelectedValue
                lblScheduleType.Text = .GetByPK().ScheduleTypeName
            End With
        End If

    End Sub

#End Region

End Class
