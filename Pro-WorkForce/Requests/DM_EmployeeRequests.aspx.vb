Imports SmartV.UTILITIES
Imports TA.Admin

Partial Class Requests_DM_EmployeeRequests
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
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            If Not objAPP_Settings.RequestGridToAppear = Nothing Then
                DM_EmployeeRequestsHeader.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "طلبات الموظفين للمدير المباشر", "Direct Manager Employee Requests")
            Else
                DM_EmployeeRequestsHeader.Visible = False
            End If

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
                    DM_PermissionApproval1.Visible = True
                ElseIf i = "2" Then
                    DM_StudyPermissionApproval1.Visible = True
                ElseIf i = "3" Then
                    DM_NursingPermissionApproval1.Visible = True
                ElseIf i = "4" Then
                    DM_ManualEntryApproval1.Visible = True
                ElseIf i = "5" Then
                    DM_OverTimeApproval1.Visible = True
                    'ElseIf i = "6" Then
                    '    DM_OverTimeApproval1.Visible = True
                ElseIf i = "7" Then
                    DM_LeaveApproval1.Visible = True
                ElseIf i = "8" Then
                    DM_UpdateTransactions1.Visible = True
                End If
            Next
        End With
    End Sub

    Private Sub FillGridViews()
        DM_PermissionApproval1.FillGridView()
        DM_StudyPermissionApproval1.FillGridView()
        DM_NursingPermissionApproval1.FillGridView()
        DM_ManualEntryApproval1.FillGridView()
        DM_OverTimeApproval1.FillGridView()
        DM_LeaveApproval1.FillGridView()
        DM_UpdateTransactions1.FillGridView()
    End Sub

#End Region

    Protected Sub tmrRefresh_Tick(sender As Object, e As EventArgs)

    End Sub
End Class
