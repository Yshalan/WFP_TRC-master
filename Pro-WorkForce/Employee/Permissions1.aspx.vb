Imports SmartV.UTILITIES
Partial Class Emp_Permissions1
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
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
            SendParameter()
        End If
    End Sub

    Private Sub SendParameter()
        Dim EmployeeId As Integer = Request.QueryString("EmployeeId")
        If EmployeeId <> 0 Then
            EmpPermissions1.EmployeeId = Request.QueryString("EmployeeId")
            EmpPermissions1.ViolationDate = Request.QueryString("ViolationDate")
            EmpPermissions1.FromTime = Request.QueryString("FromTime")
            EmpPermissions1.ToTime = Request.QueryString("ToTime")
        End If
    End Sub

#End Region

End Class
