Imports TA.Admin

Imports SmartV.UTILITIES
Imports System.Data


Partial Class test_MultiLevelDdlTestlt
    Inherits System.Web.UI.Page

    Private objorgCompany As OrgCompany

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Page.IsPostBack() <> True Then

            Dim objProjectCommon As ProjectCommon
            objProjectCommon = New ProjectCommon()

            Dim dt As DataTable = Nothing
            objorgCompany = New OrgCompany()
            dt = objorgCompany.GetAll()
            objProjectCommon.FillMultiLevelRadComboBox(RadCmbOrgCompany, dt, _
                                                       "CompanyId", "CompanyName", _
                                                       "CompanyArabicName", "FK_ParentId")




        End If

    End Sub
End Class
