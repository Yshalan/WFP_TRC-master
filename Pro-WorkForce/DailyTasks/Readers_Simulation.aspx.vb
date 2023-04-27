
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.DailyTasks
Imports TA.Employees

Partial Class DailyTasks_Readers_Simulation
    Inherits System.Web.UI.Page

    Private objEmployee As Employee
    Private objEmp_Move As Emp_Move
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Public Property EmployeeName() As String
        Get
            Return ViewState("EmployeeName")
        End Get
        Set(ByVal value As String)
            ViewState("EmployeeName") = value
        End Set
    End Property

    Private Sub DailyTasks_Readers_Simulation_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub btnCheckExistance_Click(sender As Object, e As EventArgs) Handles btnCheckExistance.Click
        CheckEmployeeNo()
    End Sub

    Private Sub btnIn_Click(sender As Object, e As EventArgs) Handles btnIn.Click
        CheckEmployeeNo()
        InsertTransaction(0)
    End Sub

    Private Sub btnOut_Click(sender As Object, e As EventArgs) Handles btnOut.Click
        CheckEmployeeNo()
        InsertTransaction(1)
    End Sub

    Private Sub btnOfficialOut_Click(sender As Object, e As EventArgs) Handles btnOfficialOut.Click
        CheckEmployeeNo()
        InsertTransaction(2)
    End Sub

    Private Sub btnSickOut_Click(sender As Object, e As EventArgs) Handles btnSickOut.Click
        CheckEmployeeNo()
        InsertTransaction(3)
    End Sub

    Private Sub InsertTransaction(ByVal TransactionType As Integer)
        If Not EmployeeId = 0 Then
            objEmp_Move = New Emp_Move
            Dim err As Integer = -1
            With objEmp_Move
                .FK_EmployeeId = EmployeeId
                .FK_ReasonId = TransactionType
                err = .Add_TransactionBySimulation()
            End With

            If err = 0 Then
                If TransactionType = 0 Then
                    CtlCommon.ShowMessage(Me.Page, "Welcome Mr. " & EmployeeName, "info")
                ElseIf TransactionType = 1 Then
                    CtlCommon.ShowMessage(Me.Page, "Good Bye Mr. " & EmployeeName, "info")
                ElseIf TransactionType = 2 Then
                    CtlCommon.ShowMessage(Me.Page, "Good Bye Mr. " & EmployeeName, "info")
                ElseIf TransactionType = 3 Then
                    CtlCommon.ShowMessage(Me.Page, "Good Bye Mr. " & EmployeeName, "info")
                End If
                ClearAll()
            Else
                CtlCommon.ShowMessage(Me.Page, "Error While Adding", "info")
            End If
        End If
    End Sub

    Private Sub CheckEmployeeNo()
        If Not txtEmployeeNo.Text = String.Empty Then
            objEmployee = New Employee
            With objEmployee
                .EmployeeNo = txtEmployeeNo.Text
                .GetRowByEmpNo()
                If Not .EmployeeNo Is Nothing Then
                    lblEmployeeNoStatus.Text = .EmployeeName
                    EmployeeId = .EmployeeId
                    EmployeeName = .EmployeeName
                    lblEmployeeNoStatus.ForeColor = Drawing.Color.Black
                Else
                    lblEmployeeNoStatus.ForeColor = Drawing.Color.Red
                    lblEmployeeNoStatus.Text = ResourceManager.GetString("EmployeeNoNotFound", CultureInfo)
                End If
            End With
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmployeeNoInsert", CultureInfo), "info")
        End If
    End Sub

    Private Sub ClearAll()
        lblEmployeeNoStatus.Text = String.Empty
        txtEmployeeNo.Text = String.Empty
        EmployeeId = 0
        EmployeeName = String.Empty
    End Sub
End Class
