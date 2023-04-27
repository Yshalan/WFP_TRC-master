Imports SmartV.Version
Imports System.Data
Imports SmartV.UTILITIES



Partial Class Default_HomePage
    Inherits System.Web.UI.Page
    Private objVersion As version

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lbtnAdmin_1_2_3.Visible = IsLinkVisible(lbtnAdmin_1_2_3.ID)
            lbtnDefinitions_1_2_3.Visible = IsLinkVisible(lbtnDefinitions_1_2_3.ID)
            lbtnEmployee_1_2_3.Visible = IsLinkVisible(lbtnEmployee_1_2_3.ID)
            lbtnDailyTasks_1_2_3.Visible = IsLinkVisible(lbtnDailyTasks_1_2_3.ID)
            lbtnSelfServices_2_3.Visible = IsLinkVisible(lbtnSelfServices_2_3.ID)
            lbtnRequests_2_3.Visible = IsLinkVisible(lbtnRequests_2_3.ID)
            lbtnDashBoard_1_2_3.Visible = IsLinkVisible(lbtnDashBoard_1_2_3.ID)
            lbtnReports_1_2_3.Visible = IsLinkVisible(lbtnReports_1_2_3.ID)
            lbtnSecurity_1_2_3.Visible = IsLinkVisible(lbtnSecurity_1_2_3.ID)

        End If
    End Sub

    Function IsLinkVisible(ByVal linkId As String) As Boolean
        Dim VersionType As Integer
        VersionType = objVersion.GetVersionType()
        Dim ModuleDT As DataTable
        Dim BuildMenu As New SmartV.Security.MENU.SYSMenuCreator
        ModuleDT = BuildMenu.BuildLeftMenu(SessionVariables.LoginUser.GroupId, 0, 0, 1, VersionType)
        Dim i As Integer
        For i = 0 To ModuleDT.Rows.Count - 1
            Dim str As String() = linkId.Split("_")
            For j = 1 To Str.Length - 1
                If (str(j) = VersionType) Then
                    Return True
                End If
            Next
        Next
        Return False

    End Function

    Protected Sub lbtnAdmin_1_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnAdmin_1_2_3.Click
        SessionVariables.UserModuleId = 1
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnDefinitions_1_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnDefinitions_1_2_3.Click
        SessionVariables.UserModuleId = 2
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnDailyTasks_1_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnDailyTasks_1_2_3.Click
        SessionVariables.UserModuleId = 3
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnEmployee_1_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnEmployee_1_2_3.Click
        SessionVariables.UserModuleId = 6
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnSelfServices_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnSelfServices_2_3.Click
        SessionVariables.UserModuleId = 8
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnRequests_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnRequests_2_3.Click
        SessionVariables.UserModuleId = 9
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnDashBoard_1_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnDashBoard_1_2_3.Click
        SessionVariables.UserModuleId = 7
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnReports_1_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnReports_1_2_3.Click
        SessionVariables.UserModuleId = 4
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnSecurity_1_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnSecurity_1_2_3.Click
        SessionVariables.UserModuleId = 5
        Response.Redirect("../Default/Inner.aspx")
    End Sub
End Class
