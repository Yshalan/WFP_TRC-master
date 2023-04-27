Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports SmartV.UTILITIES
Imports TA.Employees
Partial Class Reports_ViewScheduleByCompany
    Inherits System.Web.UI.Page
    Dim cryRpt As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim company As String = Request.QueryString("company")
        Dim entity As String = Request.QueryString("entity")
        BindReport(company, entity)

    End Sub

    Public Sub BindReport(ByVal company As String, ByVal entity As String)
        cryRpt = New ReportDocument
        Dim MainRPTDataSource As DataTable
        Dim RPTName As String
        Dim objEmployee As New Employee()

        RPTName = "~/Reports/ViewSchedulebycompany.rpt"

        objEmployee.FK_CompanyId = company
        objEmployee.FK_EntityId = entity
        MainRPTDataSource = objEmployee.GetAllScheduleByCompany()

        cryRpt.Load(Server.MapPath(RPTName))
        cryRpt.SetDataSource(MainRPTDataSource)

        cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")

    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cryRpt.Close()
        cryRpt.Dispose()

    End Sub
End Class
