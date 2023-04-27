Imports TA.Employees
Imports SmartV.UTILITIES
Imports System.Data
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports Telerik.Web.UI
Imports TA.Lookup
Imports TA.Admin
Imports System.Web.UI
Imports TA.Definitions

Partial Class EmpLeave_ListAll
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_Leaves As Emp_Leaves
    Private objOrgEntity As OrgEntity
    Private objEmployee As Employee
    Private objOrgCompany As OrgCompany
    Private objLeavesTypes As LeavesTypes
    Private objProjectCommon As New ProjectCommon
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim lang As CtlCommon.Lang

#End Region

#Region "Properties"

    Private Property LeaveID() As Integer
        Get
            Return ViewState("LeaveID")
        End Get
        Set(ByVal value As Integer)
            ViewState("LeaveID") = value
        End Set
    End Property

    Private Property EmployeeID() As Integer
        Get
            Return ViewState("EmployeeID")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeID") = value
        End Set
    End Property

    Private Property EmpDt() As DataTable
        Get
            Return ViewState("EmpDt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("EmpDt") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            dtpFromSearch.SelectedDate = DateSerial(Today.Year, Today.Month, 1)
            dtToSearch.SelectedDate = DateSerial(Today.Year, Today.Month + 1, 0)
            FillEmpLeaveGrid(dtpFromSearch.SelectedDate, dtToSearch.SelectedDate)
            dtpRequestDate.SelectedDate = Date.Today
            FillDropDown()

        End If
    End Sub

    Protected Sub ddlCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlCompany.SelectedIndexChanged
        If ddlCompany.SelectedIndex > -1 Then
            FillEntity()
            FillEmployee()
        End If

    End Sub

    Protected Sub ddlEntity_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlEntity.SelectedIndexChanged
        If ddlEntity.SelectedIndex > -1 Then
            FillEmployee(1)
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If IsExists(dtpFromDate.SelectedDate, dtpToDate.SelectedDate, LeaveID, CInt(ddlEmployee.SelectedValue)) = True Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("LeaveExist", CultureInfo), "info")
            Exit Sub
        End If
        Dim err As Integer = -1
        Dim strMessage As String
        strMessage = ResourceManager.GetString("ErrorSave", CultureInfo)
        objEmp_Leaves = New Emp_Leaves
        With objEmp_Leaves
            .FK_EmployeeId = ddlEmployee.SelectedValue
            .FK_LeaveTypeId = ddlLeaveType.SelectedValue
            .FromDate = dtpFromDate.DbSelectedDate
            .ToDate = dtpToDate.DbSelectedDate
            .RequestDate = dtpRequestDate.DbSelectedDate
            .Remarks = txtRemarks.Text
            .IsHalfDay = chkHalfDay.Checked
            .CREATED_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            If LeaveID = 0 Then
                err = .Add()
                LeaveID = .LeaveId
                strMessage = ResourceManager.GetString("SaveSuccessfully", CultureInfo)
            Else
                .LeaveId = LeaveID
                err = .Update
                strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
            End If

            If err = 0 Then
                CtlCommon.ShowMessage(Me.Page, strMessage, "success")
                ClearAll()
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        End With
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ddlEmployee.SelectedIndex = -1
        ClearAll()
    End Sub

    Protected Sub grdEmpLeaves_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdEmpLeaves.NeedDataSource
        grdEmpLeaves.DataSource = EmpDt

    End Sub

    Protected Sub grdEmpLeaves_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdEmpLeaves.SelectedIndexChanged
        LeaveID = Convert.ToInt32(DirectCast(grdEmpLeaves.SelectedItems(0), GridDataItem).GetDataKeyValue("LeaveID").ToString())
        objEmp_Leaves = New Emp_Leaves
        Dim dt As DataTable = Nothing
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        With objEmp_Leaves
            .LeaveId = LeaveID
            .GetByPK()
            ddlLeaveType.SelectedValue = .FK_LeaveTypeId
            dtpFromDate.SelectedDate = .FromDate
            dtpToDate.SelectedDate = .ToDate
            dtpRequestDate.SelectedDate = .RequestDate
            txtRemarks.Text = .Remarks
            chkHalfDay.Checked = .IsHalfDay
            btnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)
            EmployeeID = .FK_EmployeeId
            dt = .GetCompanyAndEntity

        End With
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            ddlCompany.SelectedValue = dt.Rows(0)(1)
            FillEntity()
            ddlEntity.SelectedValue = dt.Rows(0)(2)
            FillEmployee()
            ddlEmployee.SelectedValue = EmployeeID
        End If
    End Sub

    Protected Sub btnApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply.Click
        FillEmpLeaveGrid(dtpFromSearch.DbSelectedDate, dtToSearch.DbSelectedDate)
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1
        Dim strBuilder As New StringBuilder
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        For Each row As GridDataItem In grdEmpLeaves.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objEmp_Leaves = New Emp_Leaves
                objEmp_Leaves.LeaveId = Convert.ToInt32(row.GetDataKeyValue("LeaveId").ToString())
                errNum = objEmp_Leaves.Delete()
                If errNum = 0 Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
                End If
            End If
        Next
        ClearAll()
    End Sub

#End Region

#Region "Methods"

    Function IsExists(ByVal Fromdate As Date, ByVal Todate As Date, ByVal LeaveID As Integer, ByVal EmployeeID As Integer) As Boolean
        Dim status As Boolean
        For Each dr As DataRow In EmpDt.Rows
            If LeaveID = dr("LeaveId") And EmployeeID = dr("FK_EmployeeId") Then
                Continue For
            End If
            If EmployeeID = dr("FK_EmployeeId") And (Fromdate >= dr("FromDate") And Fromdate <= dr("ToDate") Or Todate >= dr("FromDate") And Todate <= dr("ToDate")) Then
                status = True
            End If

        Next
        Return status
    End Function

    Sub ClearAll()
        txtRemarks.Text = String.Empty
        ddlLeaveType.SelectedIndex = -1
        dtpFromDate.Clear()
        dtpToDate.Clear()
        dtpRequestDate.SelectedDate = Date.Today
        chkHalfDay.Checked = False
        LeaveID = 0
        EmployeeID = 0
        dtpFromSearch.SelectedDate = DateSerial(Today.Year, Today.Month, 1)
        dtToSearch.SelectedDate = DateSerial(Today.Year, Today.Month + 1, 0)
        FillEmpLeaveGrid(dtpFromSearch.SelectedDate, dtToSearch.SelectedDate)
        btnSave.Text = "Save"
    End Sub

    Sub FillEmpLeaveGrid(ByVal FromDate As Date, ByVal ToDate As Date)
        objEmp_Leaves = New Emp_Leaves
        With objEmp_Leaves
            .FromDate = FromDate
            .ToDate = ToDate
            EmpDt = .GetAllLeavesLists
            grdEmpLeaves.DataSource = EmpDt
            grdEmpLeaves.DataBind()
        End With
    End Sub

    Sub FillDropDown()
        objProjectCommon = New ProjectCommon()
        Dim dt As DataTable = Nothing
        dt = Nothing
        objOrgCompany = New OrgCompany()
        dt = objOrgCompany.GetAll()
        objProjectCommon.FillMultiLevelRadComboBox(ddlCompany, dt, _
                                                   "CompanyId", "CompanyName", _
                                                   "CompanyArabicName", "FK_ParentId")

        dt = Nothing
        objLeavesTypes = New LeavesTypes()
        dt = objLeavesTypes.GetAllForDDL
        ProjectCommon.FillRadComboBox(ddlLeaveType, dt, "LeaveName", _
                                     "LeaveArabicName", "LeaveId")

    End Sub

    Private Sub FillEntity()
        If ddlCompany.SelectedValue <> -1 Then
            objProjectCommon = New ProjectCommon()
            objOrgEntity = New OrgEntity()
            objOrgEntity.FK_CompanyId = ddlCompany.SelectedValue
            Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()
            If dt.Rows.Count > 0 Then
                objProjectCommon.FillMultiLevelRadComboBox(ddlEntity, dt, "EntityId", _
                                                         "EntityName", "EntityArabicName", "FK_ParentId")
            Else
                ddlEntity.Items.Clear()
                ddlEntity.Text = String.Empty
                ddlEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem("--Please Select--", -1))
            End If
        End If

    End Sub

    Sub FillEmployee(Optional ByVal type As Integer = 0)
        Dim dt As DataTable
        If type = 0 Then
            objOrgCompany = New OrgCompany
            With objOrgCompany
                .CompanyId = ddlCompany.SelectedValue
                dt = .GetEmployeesByOrgCompany()
            End With
        Else
            objOrgEntity = New OrgEntity()
            With objOrgEntity
                .EntityId = ddlEntity.SelectedValue
                dt = .GetEmployeesByOrgEntity
            End With

        End If
        If dt.Rows.Count > 0 Then
            ProjectCommon.FillRadComboBox(ddlEmployee, dt, "EmployeeName", "EmployeeName", "EmployeeId")
        Else
            ddlEmployee.Items.Clear()
        End If
    End Sub


#End Region

End Class
