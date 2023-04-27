Imports TA.Employees
Imports SmartV.UTILITIES
Imports System.Data
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports Telerik.Web.UI
Imports TA.Lookup
Imports TA.Admin
Imports System.Web.UI
Imports TA.Definitions

Partial Class Emp_UserControls_EmpLeave_List
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private objEmp_Leaves As Emp_Leaves
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

#Region "Methods"

    Sub FillEmpLeaveGrid(ByVal FromDate As Date, ByVal ToDate As Date)
        objEmp_Leaves = New Emp_Leaves
        With objEmp_Leaves
            .FK_EmployeeId = EmployeeID
            .FromDate = FromDate
            .ToDate = ToDate
            EmpDt = .GetAllLeavesByEmployee()
            grdEmpLeaves.DataSource = EmpDt
            grdEmpLeaves.DataBind()
        End With
    End Sub

    Public Sub LoadData()
        FillEmpLeaveGrid(dtpFromDate.DbSelectedDate, dtpToDate.DbSelectedDate)
    End Sub

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            dtpFromDate.SelectedDate = DateSerial(Today.Year, Today.Month, 1)
            dtpToDate.SelectedDate = DateSerial(Today.Year, Today.Month + 1, 0)
            If SessionVariables.CultureInfo = "ar-JO" Then
                PageHeader1.HeaderText = "اجازات الموظفين"
            Else
                PageHeader1.HeaderText = "Employee Leaves"
            End If
        End If
    End Sub

    Protected Sub grdEmpLeaves_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdEmpLeaves.NeedDataSource
        grdEmpLeaves.DataSource = EmpDt

    End Sub

    Protected Sub btnApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply.Click
        FillEmpLeaveGrid(dtpFromDate.DbSelectedDate, dtpToDate.DbSelectedDate)
    End Sub

#End Region

End Class
