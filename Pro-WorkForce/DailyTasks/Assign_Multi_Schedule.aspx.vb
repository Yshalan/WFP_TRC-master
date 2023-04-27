Imports SmartV.UTILITIES
Imports SmartV.UTILITIES.CtlCommon
Imports System.Globalization
Imports System.Resources

Partial Class DailyTasks_Assign_Multi_Schedule
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
#End Region

#Region "Page Events"

    Protected Sub DailyTasks_Assign_Multi_Schedule_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")

            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                img_ar.Visible = True
                img_en.Visible = False

            Else
                Lang = CtlCommon.Lang.EN
                img_en.Visible = True
                img_ar.Visible = False
            End If
        End If
        Assign_Company.HeaderText = ResourceManager.GetString("Assign_Schedule", CultureInfo)
    End Sub

    Protected Sub DailyTasks_Assign_Multi_Schedule_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
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

#End Region

End Class
