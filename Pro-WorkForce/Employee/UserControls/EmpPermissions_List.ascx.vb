Imports System.Data
Imports TA.Employees
Imports SmartV.UTILITIES
Imports TA.Lookup
Imports TA.Admin
Imports TA.Definitions

Partial Class Emp_UserControls_EmpPermissions_List
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private objEmp_Permissions As Emp_Permissions
    Private objOrgEntity As OrgEntity
    Private objEmployee As Employee
    Private objOrgCompany As OrgCompany
    Private objLeavesTypes As LeavesTypes
    Private objProjectCommon As New ProjectCommon

#End Region

#Region "Properties"

    Public Property EmployeeID() As Integer
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            dtpFromDate.SelectedDate = DateSerial(Today.Year, Today.Month, 1)
            dtpToDate.SelectedDate = DateSerial(Today.Year, Today.Month + 1, 0)
            If SessionVariables.CultureInfo = "ar-JO" Then
                PageHeader1.HeaderText = "مغادرات الموظفين"
            Else
                PageHeader1.HeaderText = "Employee Permissions"
            End If
        End If
    End Sub

    Protected Sub grdEmpLeaves_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdEmpPermissions.NeedDataSource
        grdEmpPermissions.DataSource = EmpDt
    End Sub

    Protected Sub btnApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply.Click
        FillEmpLeaveGrid(dtpFromDate.DbSelectedDate, dtpToDate.DbSelectedDate)
    End Sub

    Public Sub LoadData()
        FillEmpLeaveGrid(dtpFromDate.DbSelectedDate, dtpToDate.DbSelectedDate)
    End Sub

#End Region

#Region "Methods"

    Sub FillEmpLeaveGrid(ByVal FromDate As Date, ByVal ToDate As Date)
        objEmp_Permissions = New Emp_Permissions
        With objEmp_Permissions
            .FK_EmployeeId = EmployeeID
            .FromTime = FromDate
            .ToTime = ToDate
            EmpDt = .GetAllPermissionsByEmployee()
            grdEmpPermissions.DataSource = EmpDt
            grdEmpPermissions.DataBind()
        End With
    End Sub

#End Region

End Class
