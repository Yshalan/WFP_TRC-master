Imports SmartV.UTILITIES
Imports TA.Admin

Partial Class Requests_GM_EmployeeRequests
    Inherits System.Web.UI.Page
#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Dim objAPP_Settings As New APP_Settings
#End Region

#Region "Page Events"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            GM_EmployeeRequestsHeader.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "طلبات الموظفين للمدير العام", "General Manager Employee Requests")
            FillUserControls()
            FillGridViews()
        End If
    End Sub

#End Region

#Region "Methods"
    Private Sub FillUserControls()
        Dim RequestGridToAppear As String = ""
        With objAPP_Settings
            .GetByPK()
            For Each i As String In .RequestGridToAppear.Split(",")
                If i = "1" Then
                    GM_PermissionApproval1.Visible = True
                ElseIf i = "2" Then
                    GM_StudyPermissionApproval1.Visible = True
                ElseIf i = "3" Then
                    GM_NursingPermissionApproval1.Visible = True
                ElseIf i = "4" Then
                    GM_ManualEntryApproval1.Visible = True
                    'ElseIf i = "5" Then
                    '    GM_OverTimeApproval1.Visible = True
                    'ElseIf i = "6" Then
                    '    GM_OverTimeApproval1.Visible = True
                ElseIf i = "7" Then
                    GM_LeaveApproval1.Visible = True
                ElseIf i = "8" Then
                    GM_UpdateTransactions1.Visible = True
                End If
            Next
        End With
    End Sub

    Private Sub FillGridViews()
        GM_PermissionApproval1.FillGridView()
        GM_StudyPermissionApproval1.FillGridView()
        GM_NursingPermissionApproval1.FillGridView()
        GM_ManualEntryApproval1.FillGridView()
        'GM_OverTimeApproval1.FillGridView()
        GM_LeaveApproval1.FillGridView()
        GM_UpdateTransactions1.FillGridView()
    End Sub

#End Region
End Class
