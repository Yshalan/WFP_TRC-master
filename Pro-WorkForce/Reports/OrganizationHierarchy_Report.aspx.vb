Imports SmartV.UTILITIES
Imports System.Data
Imports System.Web.Services
Imports System.Configuration
Imports System.Data.SqlClient
Imports TA.Admin

Partial Class Reports_OrganizationHierarchy_Report
    Inherits System.Web.UI.Page

#Region "Class Variables"
    Private Lang As CtlCommon.Lang
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objOrgCompany As New OrgCompany
#End Region
#Region "Page Events"
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
            Page.MasterPageFile = "~/default/ArabicMaster.master"
        Else
            Lang = CtlCommon.Lang.EN
            Page.MasterPageFile = "~/default/NewMaster.master"
        End If

        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            Page.UICulture = SessionVariables.CultureInfo
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If

        If Not Page.IsPostBack Then
            lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "الهيكل التنظيمي للمؤسسة", "Organization Hierarchy")

            objOrgCompany = New OrgCompany
            Dim dt As DataTable
            With objOrgCompany
                dt = .GetAll
                If dt.Rows.Count = 1 Then
                    lblCompany.Visible = False
                    radcmbCompany.Visible = False
                Else
                    FillCompany()
                End If
            End With

        End If
    End Sub
    Private Sub FillCompany()
        objOrgCompany = New OrgCompany
        With objOrgCompany
            CtlCommon.FillTelerikDropDownList(radcmbCompany, .GetAll, Lang)
        End With
    End Sub
    Protected Sub btnPrint_Click(sender As Object, e As System.EventArgs) Handles btnPrint.Click
        Dim CompanyId As Integer
        If radcmbCompany.SelectedValue = "" Then
            CompanyId = 0
        Else
            CompanyId = radcmbCompany.SelectedValue
        End If

        Context.Response.Write("<script> language='javascript'>window.open('OrgChart.aspx?CompanyId=" & CompanyId & "','_newtab');</script>")

        'Response.Redirect("OrgChart.aspx?CompanyId=" & CompanyId)
    End Sub
#End Region

End Class
