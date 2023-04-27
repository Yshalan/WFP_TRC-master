Imports TA.Admin.OrgCompany
Imports SmartV.UTILITIES
Imports TA.Lookup
Imports System.Data
Imports TA.Employees
Imports TA.Definitions
Imports SmartV.UTILITIES.ProjectCommon

Partial Class Employee_EmployeeRoamers
    Inherits System.Web.UI.Page


#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objEmp_Roamers As Emp_Roamers
#End Region

#Region "Property"
    Public Property RoamerId() As Integer
        Get
            Return ViewState("RoamerId")
        End Get
        Set(ByVal value As Integer)
            ViewState("RoamerId") = value
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
    Public Property EntityId() As Integer
        Get
            Return ViewState("EntityId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EntityId") = value
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
    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property
    Public Property IsAdvanced() As Integer
        Get
            Return ViewState("IsAdvanced")
        End Get
        Set(ByVal value As Integer)
            ViewState("IsAdvanced") = value
        End Set
    End Property
    Public Property IsManager() As Integer
        Get
            Return ViewState("IsManager")
        End Get
        Set(ByVal value As Integer)
            ViewState("IsManager") = value
        End Set
    End Property
    Public Property FK_ManagerId() As Integer
        Get
            Return ViewState("FK_ManagerId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_ManagerId") = value
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

        showHide(chckTemporary.Checked)

        If Not Page.IsPostBack Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                PageHeader1.HeaderText = ResourceManager.GetString("EmployeeRoamers", CultureInfo)
            Else
                Lang = CtlCommon.Lang.EN
                PageHeader1.HeaderText = ResourceManager.GetString("EmployeeRoamers", CultureInfo)

            End If

            Me.dtpFromdate.SelectedDate = Today
            Me.dtpEndDate.SelectedDate = Today
            FillGrid()
            EmployeeFilterUC.CompanyRequiredFieldValidationGroup = btnSave.ValidationGroup
        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim errornum As Integer
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim flag As Boolean = False

        For Each item As ListItem In cblEmpList.Items
            If item.Selected = True Then
                flag = True
                Dim objEmp_Roamers As New Emp_Roamers()
                objEmp_Roamers.FK_EmployeeId = item.Value
                objEmp_Roamers.FromDate = dtpFromdate.SelectedDate
                objEmp_Roamers.ToDate = IIf(dtpEndDate.SelectedDate Is Nothing Or Not chckTemporary.Checked, DateTime.MinValue, dtpEndDate.SelectedDate)
                objEmp_Roamers.IsTemporary = chckTemporary.Checked
                
                If RoamerId <> 0 Then
                    objEmp_Roamers.RoamerId = RoamerId
                    objEmp_Roamers.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    errornum = objEmp_Roamers.Update
                Else
                    objEmp_Roamers.CREATED_BY = SessionVariables.LoginUser.FK_EmployeeId
                    objEmp_Roamers.CREATED_DATE = Now
                    errornum = objEmp_Roamers.Add()
                End If
            End If
        Next


        If errornum = 0 Then

            If flag = False Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("EmployeeSelect", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                FillGrid()
                ClearAll()
            End If
        ElseIf errornum = -99 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DateExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer
        Dim lWithCheck As Boolean
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdSchedule_Roamer.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intEmpWorkScheduleId As Integer = Convert.ToInt32(row.GetDataKeyValue("RoamerId").ToString())
                objEmp_Roamers = New Emp_Roamers()
                objEmp_Roamers.RoamerId = intEmpWorkScheduleId
                errNum += objEmp_Roamers.Delete()
                lWithCheck = True
            End If
        Next
        If (lWithCheck) Then
            If errNum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
                FillGrid()
                ClearAll()
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
            End If
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "info")
        End If

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ClearAll()
        ManageControls(True)
        EmployeeFilterUC.ManageControls(True)
        FillEmployee()
    End Sub

    Public Sub ManageControls(ByVal status As Boolean)
        EmployeeFilterUC.ManageControls(status)
        cblEmpList.Enabled = status
    End Sub

    Private Sub showHide(ByVal status As Boolean, Optional ByVal order As Integer = 0)
        pnlEndDate.Visible = status
    End Sub

    Protected Sub dgrdSchedule_Roamer_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdSchedule_Roamer.NeedDataSource
        objEmp_Roamers = New Emp_Roamers
        dgrdSchedule_Roamer.DataSource = objEmp_Roamers.Get_Emp_Roamers_Details()

    End Sub

    Protected Sub dgrdSchedule_Employee_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdSchedule_Roamer.SelectedIndexChanged
        Literal1.Visible = False
        Literal2.Visible = False
        objEmp_Roamers = New Emp_Roamers
        RoamerId = CInt(CType(dgrdSchedule_Roamer.SelectedItems(0), GridDataItem).GetDataKeyValue("RoamerId").ToString())
        CompanyId = CInt(CType(dgrdSchedule_Roamer.SelectedItems(0), GridDataItem).GetDataKeyValue("CompanyId").ToString())
        EntityId = CInt(CType(dgrdSchedule_Roamer.SelectedItems(0), GridDataItem).GetDataKeyValue("EntityId").ToString())
        EmployeeId = CInt(CType(dgrdSchedule_Roamer.SelectedItems(0), GridDataItem).GetDataKeyValue("EmployeeId").ToString())
        Dim EmpName As String = CType(dgrdSchedule_Roamer.SelectedItems(0), GridDataItem).GetDataKeyValue("EmployeeName").ToString()
        Dim EmpArName As String = CType(dgrdSchedule_Roamer.SelectedItems(0), GridDataItem).GetDataKeyValue("EmployeeArabicName").ToString()

        With objEmp_Roamers
            .RoamerId = RoamerId
            .GetByPK()
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
            EmployeeFilterUC.CompanyId = CompanyId
            EmployeeFilterUC.FillCompanies()
            cblEmpList.Items.Clear()
            If SessionVariables.CultureInfo = "ar-JO" Then
                'cblEmpList.Items.Add(EmpArName)
                cblEmpList.Items.Add(New ListItem(EmpArName.ToString(), EmployeeId.ToString()))
            Else
                cblEmpList.Items.Add(New ListItem(EmpName.ToString(), EmployeeId.ToString()))
                'cblEmpList.Items.Add(EmpName)
            End If
            cblEmpList.Items(0).Selected = True

            EmployeeFilterUC.SelectByID("Company")
            EmployeeFilterUC.EntityId = EntityId
            EmployeeFilterUC.FillEntity()
            EmployeeFilterUC.SelectByID("Entity")
        End With
        ManageControls(False)
        'End If
        'Next
    End Sub
#End Region

#Region "Methods"

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

    Private Sub ClearAll()
        ' Clear the controls
        EmployeeFilterUC.ClearValues()
        chckTemporary.Checked = False
        cblEmpList.Items.Clear()
        pnlEndDate.Visible = False
        dtpFromdate.SelectedDate = Today
        dtpEndDate.SelectedDate = Today
        RoamerId = 0
    End Sub

    Public Sub CompanyChanged()
        EmployeeFilterUC.FillEntity()
        FillEmployee()
    End Sub

    Private Function GetNavigateURL(ByVal CompanyId As String, ByVal EntityId As String) As String
        Dim res As String = "javascript:open_window('../Reports/EmployeeInfo.aspx?company={0}&entity={1}','',700,400)"

        Return String.Format(res, CompanyId, IIf(String.IsNullOrEmpty(EntityId), "-1", EntityId))
    End Function

    Private Sub FillGrid()
        objEmp_Roamers = New Emp_Roamers
        'If IsAdvanced = 1 Then
        '    If IsManager = 1 Then
        '        objEmp_WorkSchedule.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        '        dgrdSchedule_Employee.DataSource = objEmp_WorkSchedule.Get_Emp_Schedule_Details_Advanced_Mgr()
        '    Else
        '        dgrdSchedule_Employee.DataSource = objEmp_WorkSchedule.Get_Emp_Schedule_Details_Advanced

        '    End If
        'Else
        '    dgrdSchedule_Employee.DataSource = objEmp_WorkSchedule.Get_Emp_Schedule_Details
        'End If
        dgrdSchedule_Roamer.DataSource = objEmp_Roamers.Get_Emp_Roamers_Details()
        dgrdSchedule_Roamer.DataBind()
    End Sub
#End Region

End Class
