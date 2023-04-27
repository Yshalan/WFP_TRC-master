Imports SmartV.UTILITIES
Imports System.Data
Imports SmartV.Security
Imports SmartV.DB
Imports System.Resources
Imports System.Reflection
Imports System.Threading
Imports Telerik.Web.UI
Imports TA.Lookup
Imports TA.Employees

Partial Class UserControls_EmpWorkSchedule
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Dim objEmp_WorkSchedule As New Emp_WorkSchedule
    Dim objEmployee As New Employee
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

#End Region

#Region "Properties"

    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

#End Region

#Region "Page Items"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With objEmployee
                .EmployeeId = EmployeeId
                .GetByPK()
                CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
                If .Gender = "m" Then
                    PageHeader1.HeaderText = ResourceManager.GetString("EmpWorkScheduleMr", CultureInfo) + .EmployeeName
                Else
                    PageHeader1.HeaderText = ResourceManager.GetString("EmpWorkScheduleMrs", CultureInfo) + .EmployeeName
                End If
            End With

            dteStartDate.SelectedDate = New Date(Now.Year, 1, 1)
            dteEndDate.SelectedDate = New Date(Now.Year, 12, 31)
        End If
    End Sub

    Protected Sub dgrdActive_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdActive.ItemDataBound

        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then

            If (e.Item.Cells(5).Text) = "True" Then
                e.Item.BackColor = Drawing.Color.Coral
            End If
        End If
    End Sub

    Protected Sub dgrdActive_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdActive.NeedDataSource
        fillgrid()
    End Sub

    Protected Sub ibtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnSearch.Click
        dgrdActive.DataSource = Nothing
        dgrdActive.Rebind()
        fillgrid()
    End Sub

#End Region

#Region "Methods"

    Public Sub fillgrid()
        With objEmp_WorkSchedule

            .FK_EmployeeId = EmployeeId
            dgrdActive.DataSource = .GetAllByEmployeeandDateRange(dteStartDate.SelectedDate, dteEndDate.SelectedDate)
        End With
    End Sub

#End Region

End Class
