Imports System.Data
Imports TA.DashBoard

Partial Class Default_DefaultAdmin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim objDashBoard As New DashBoard
        'objDashBoard.FK_CompanyId = 1
        'objDashBoard.FK_EntityId = 75
        'objDashBoard.DashDate = "8 / 4 / 2013"
        'Dim dtCAChart As DataTable = objDashBoard.GetAbsentDash()
        'Home3dDash1.dt1 = dtCAChart

        'dtCAChart = objDashBoard.GetDelayDash()
        'Home3dDash1.dt2 = dtCAChart

        'dtCAChart = objDashBoard.GetEarlyoutDash()
        'Home3dDash1.dt3 = dtCAChart

        'Home3dDash1.DoStairStackPie()
        'Home3dDash1.fill_chart(dtCAChart, "descrip", "number", "Attendees Dash board", 1)
    End Sub
End Class
