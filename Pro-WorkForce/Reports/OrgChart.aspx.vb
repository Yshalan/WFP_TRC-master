
Imports System.Data
Imports System.Web.Services
Imports System.Configuration
Imports System.Data.SqlClient
Imports TA.Reports

Partial Class Reports_OrgChart
    Inherits System.Web.UI.Page
    Public Shared objReport As New Report


    <WebMethod()> _
    Public Shared Function GetChartData() As List(Of Object)
        
        Dim dt As New DataTable
        dt = objReport.Get_OrganizationHierarchy()
        Dim chartData As New List(Of Object)()
        For Each row As DataRow In dt.Rows
            chartData.Add(New Object() {row("EntityId"), row("EntityName"), row("EntityArabicName"), row("FK_ParentId")})

        Next
        Return chartData
    End Function

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim CompanyId As String = Request.QueryString("CompanyId")
            'GetChartData(41)
            
            objReport.CompanyId = CInt(DirectCast(CompanyId, String))
        End If
    End Sub
End Class
