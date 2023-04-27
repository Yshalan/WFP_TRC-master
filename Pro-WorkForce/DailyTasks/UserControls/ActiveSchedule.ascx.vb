Imports TA.Lookup
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.Employees
Imports System.Data
Imports SmartV.DB

Partial Class Admin_UserControls_ActiveSchedule
    Inherits System.Web.UI.UserControl

#Region "Page Events"

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnget.Click
        FillGrid()
    End Sub

    Protected Sub dgrdViewSchedule_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs) Handles dgrdAschedule.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            FillGrid()
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdAschedule.Skin))
    End Function

    Protected Sub dgrdAschedule_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles dgrdAschedule.SortCommand
        FillGrid()
    End Sub

#End Region

#Region "Methods"

    Private Sub FillGrid()
        Dim objEmp_WorkSchedule As New Emp_WorkSchedule
        Dim dt As New DataTable
        dt = objEmp_WorkSchedule.GetActiveSchedule(TextBox1.Text)
        dgrdAschedule.DataSource = dt
        dgrdAschedule.DataBind()

    End Sub

#End Region

End Class
