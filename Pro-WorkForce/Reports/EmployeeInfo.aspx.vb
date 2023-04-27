Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports SmartV.UTILITIES
Imports TA.Employees

Partial Class Reports_EmployeeInfo
    Inherits System.Web.UI.Page
    Dim cryRpt As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Company As String = Request.QueryString("company")
        Dim Entity As String = Request.QueryString("entity")
        BindReport(Company, Entity)


    End Sub

    Public Sub BindReport(ByVal CompanyId As String, ByVal EntityId As String)
        cryRpt = New ReportDocument
        Dim MainRPTDataSource As DataTable
        Dim RPTName As String
        Dim objEmployee As New Employee()

        RPTName = "~/Reports/rptEmpInfo.rpt"

        objEmployee.FK_CompanyId = CompanyId
        objEmployee.FK_EntityId = EntityId

        MainRPTDataSource = objEmployee.GetEmpByCompany()

        cryRpt.Load(Server.MapPath(RPTName))
        cryRpt.SetDataSource(MainRPTDataSource)

        crvCertificate.ReportSource = cryRpt
        cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "ExportedReport")

    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cryRpt.Close()
        cryRpt.Dispose()

    End Sub
End Class
