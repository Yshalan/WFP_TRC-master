Imports System.Data
Imports System.Web.UI.DataVisualization.Charting
Imports TA.Employees

Partial Class Emp_UserControls_Emp_DashBoard
    Inherits System.Web.UI.UserControl

#Region "Methods"

    Public Sub fill_chart(ByVal dtCAChart As DataTable, ByVal xmember As String, ByVal ymemeber As String, ByVal Title As String)

        Try

            chrtbie.Series("Default").ToolTip = "#PERCENT"
            chrtbie.Series("Default").LegendToolTip = "#LEGENDTEXT"
            chrtbie.Series("Default").PostBackValue = "#INDEX"
            chrtbie.Series("Default").LegendPostBackValue = "#INDEX"


            chrtbie.DataSource = dtCAChart
            chrtbie.Series("Default").XValueMember = xmember
            chrtbie.Series("Default").YValueMembers = ymemeber
            chrtbie.DataBind()
            ' Set Doughnut chart type
            chrtbie.Series("Default").ChartType = SeriesChartType.Pie
            ' Set labels style
            chrtbie.Series("Default")("PieLabelStyle") = "Disabled"
            ' Set Doughnut radius percentage
            chrtbie.Series("Default")("DoughnutRadius") = "30"
            ' Explode data point 
            chrtbie.Series("Default").Points(0)("Exploded") = "True"
            ' Enable 3D
            chrtbie.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True
            ' Disable the Legend
            chrtbie.Legends(0).Enabled = True

            chrtbie.Palette = ChartColorPalette.BrightPastel

            chrtbie.Titles.Add(Title)

        Catch ex As Exception

        End Try
    End Sub

#End Region
    
End Class
