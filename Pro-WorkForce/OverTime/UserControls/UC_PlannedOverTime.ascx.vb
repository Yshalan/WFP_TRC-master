Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports System.Reflection
Imports System.Data
Imports TA.OverTime
Partial Class OverTime_UserControls_UC_PlannedOverTime
    Inherits System.Web.UI.UserControl
    Public Property SortDir() As String
        Get
            Return ViewState("SortDir")
        End Get
        Set(ByVal value As String)
            ViewState("SortDir") = value
        End Set
    End Property
    Public Property SortExep() As String
        Get
            Return ViewState("SortExep")
        End Get
        Set(ByVal value As String)
            ViewState("SortExep") = value
        End Set
    End Property
    Public Property SortExepression() As String
        Get
            Return ViewState("SortExepression")
        End Get
        Set(ByVal value As String)
            ViewState("SortExepression") = value
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
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            FillYear()
            FillMonth()
            FillGrid()
        End If
    End Sub
    Private Sub FillGrid()
        Try
            Dim obj As New Emp_OverTime_Master

            With obj
                .CREATED_BY = SessionVariables.LoginUser.ID
            End With
            dtCurrent = obj.GetAll()
            Dim dv As New DataView(dtCurrent)
            dv.Sort = SortExepression
            gdvMyApplications.DataSource = dv
            gdvMyApplications.DataBind()

            If Not dtCurrent Is Nothing Then
                If dtCurrent.Rows.Count > 0 Then
                    If SessionVariables.CultureInfo = "en-US" Then
                        LbRowCount.Text = "Search Result:" & dtCurrent.Rows.Count
                    Else
                        LbRowCount.Text = "نتيجة البحث " & dtCurrent.Rows.Count
                    End If
                Else
                    If SessionVariables.CultureInfo = "en-US" Then
                        LbRowCount.Text = "Search Result:" & 0
                    Else
                        LbRowCount.Text = "نتيجة البحث " & 0
                    End If
                End If
            Else
                If SessionVariables.CultureInfo = "en-US" Then
                    LbRowCount.Text = "Search Result:" & 0
                Else
                    LbRowCount.Text = "نتيجة البحث " & 0
                End If

            End If
            obj = Nothing

        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub FillYear()
        Try


            For i As Integer = DateTime.Now.Year - 5 To DateTime.Now.Year

                Dim lst As New RadComboBoxItem
                If SessionVariables.CultureInfo = "en-US" Then
                    lst = New RadComboBoxItem(i.ToString(), i.ToString())
                    cmbYear.Items.Add(lst)
                Else
                    lst = New RadComboBoxItem(i.ToString(), i.ToString())
                    cmbYear.Items.Add(lst)
                End If

                ' cmbYear.Items.Add(i.ToString())
            Next

            Dim LastMonthDate As DateTime = DateTime.Now.AddMonths(-1)

            cmbYear.Items.FindItemByValue(LastMonthDate.Year.ToString()).Selected = True

            'cmbYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = True  'set current year as 
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub FillMonth()
        Try

            Dim LastMonthDate As DateTime = DateTime.Now.AddMonths(-1)

            Dim itm1 As New Telerik.Web.UI.RadComboBoxItem

            itm1.Value = 1
            itm1.Text = Resources.Labels.Jan

            cmbMonth.Items.Add(itm1)

            Dim itm2 As New Telerik.Web.UI.RadComboBoxItem
            itm2.Value = 2
            itm2.Text = Resources.Labels.Feb

            cmbMonth.Items.Add(itm2)

            Dim itm3 As New Telerik.Web.UI.RadComboBoxItem
            itm3.Value = 3
            itm3.Text = Resources.Labels.Mar

            cmbMonth.Items.Add(itm3)

            Dim itm4 As New Telerik.Web.UI.RadComboBoxItem
            itm4.Value = 4
            itm4.Text = Resources.Labels.Apr

            cmbMonth.Items.Add(itm4)

            Dim itm5 As New Telerik.Web.UI.RadComboBoxItem
            itm5.Value = 5
            itm5.Text = Resources.Labels.May

            cmbMonth.Items.Add(itm5)

            Dim itm6 As New Telerik.Web.UI.RadComboBoxItem
            itm6.Value = 6
            itm6.Text = Resources.Labels.Jun

            cmbMonth.Items.Add(itm6)

            Dim itm7 As New Telerik.Web.UI.RadComboBoxItem
            itm7.Value = 7
            itm7.Text = Resources.Labels.Jul

            cmbMonth.Items.Add(itm7)

            Dim itm8 As New Telerik.Web.UI.RadComboBoxItem
            itm8.Value = 8
            itm8.Text = Resources.Labels.Aug

            cmbMonth.Items.Add(itm8)

            Dim itm9 As New Telerik.Web.UI.RadComboBoxItem
            itm9.Value = 9
            itm9.Text = Resources.Labels.Sep

            cmbMonth.Items.Add(itm9)

            Dim itm10 As New Telerik.Web.UI.RadComboBoxItem
            itm10.Value = 10
            itm10.Text = Resources.Labels.Oct

            cmbMonth.Items.Add(itm10)

            Dim itm11 As New Telerik.Web.UI.RadComboBoxItem
            itm11.Value = 11
            itm11.Text = Resources.Labels.Nov

            cmbMonth.Items.Add(itm11)

            Dim itm12 As New Telerik.Web.UI.RadComboBoxItem
            itm12.Value = 12
            itm12.Text = Resources.Labels.Dec

            cmbMonth.Items.Add(itm12)

            'cmbMonth.SelectedValue = LastMonthDate.Month
            cmbMonth.SelectedValue = DateTime.Now.Month
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim obj As New Emp_OverTime_Master
            Dim err As Integer

            If cmbMonth.SelectedValue = -1 Then
                CtlCommon.ShowMessage(Me.Page, "Please Select Month", "info")
                Exit Sub
            End If
            If cmbYear.SelectedValue = -1 Then
                CtlCommon.ShowMessage(Me.Page, "Please Select Year", "info")
                Exit Sub
            End If

            If EmployeeFilter1.EmployeeId <= 0 Then
                CtlCommon.ShowMessage(Me.Page, "Please Select an Emplyee", "info")
                Exit Sub
            End If


            With obj

                If IsNumeric(txtPlannedOTNormal_HH.Text.Trim()) And IsNumeric(txtPlannedOTNormal_MM.Text.Trim()) Then
                    .Planned_OT_Normal = (txtPlannedOTNormal_HH.Text.Trim() * 60) + txtPlannedOTNormal_MM.Text.Trim()
                Else
                    .Planned_OT_Normal = 0
                End If


                If IsNumeric(txtPlannedOTRest_HH.Text.Trim()) And IsNumeric(txtPlannedOTRest_MM.Text.Trim()) Then
                    .Planned_OT_Rest = (txtPlannedOTRest_HH.Text.Trim() * 60) + txtPlannedOTRest_MM.Text.Trim()
                Else
                    .Planned_OT_Rest = 0
                End If

              
                .OverTimeMonth = cmbMonth.SelectedValue
                .OverTimeYear = cmbYear.SelectedValue
                .FK_EmployeeID = EmployeeFilter1.EmployeeId
                .CREATED_BY = SessionVariables.LoginUser.ID

                err = .AddPlannedOT()

            End With

            If err = 0 Then
                FillGrid()
                CtlCommon.ShowMessage(Me.Page, "Saved Successfully", "success")
                Exit Sub
            Else
                CtlCommon.ShowMessage(Me.Page, "Error while Saving", "error")
                Exit Sub

            End If

        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim err As Integer
            Dim obj As New Emp_OverTime_Master
            For Each row As GridViewRow In gdvMyApplications.Rows
                Dim cb As CheckBox = DirectCast(row.FindControl("chk"), CheckBox)
                If cb.Checked Then
                    Dim intMasterID As Integer = Convert.ToInt32(DirectCast(row.FindControl("lblOT_MasterID"), Label).Text)
                    obj.OT_MasterID = intMasterID
                    err = obj.Delete()
                End If
            Next
            If err = 0 Then
                FillGrid()
                CtlCommon.ShowMessage(Me.Page, "Deleted Successfully", "success")
            ElseIf err = -10 Then
                CtlCommon.ShowMessage(Me.Page, "Sorry No permission to delete as there is pending OverTime Approval Process", "info")
            Else
                CtlCommon.ShowMessage(Me.Page, "Error while Deleting", "error")
            End If
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
End Class
