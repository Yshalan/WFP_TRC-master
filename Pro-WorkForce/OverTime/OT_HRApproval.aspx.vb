Imports SmartV.UTILITIES
Imports SmartV.Version
Imports System.Data
Imports TA.Security
Partial Class OverTime_OT_HRApproval
    Inherits System.Web.UI.Page
    Private objVersion As version
    Private Lang As CtlCommon.Lang
    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
            Page.MasterPageFile = "~/default/ArabicMaster.master"
        Else
            Lang = CtlCommon.Lang.EN
            Page.MasterPageFile = "~/default/NewMaster.master"
        End If
        Page.UICulture = SessionVariables.CultureInfo

        Dim moduleID As Integer = SessionVariables.UserModuleId
        Dim objSysModules As New SYSModules
        With objSysModules
            .ModuleID = moduleID
            .GetByPK()

            If (SessionVariables.CultureInfo = "en-US") Then
                Page.Title = "Work Force Pro : :" + .EnglishName
            Else
                Page.Title = "Work Force Pro : :" + .ArabicName
            End If
        End With
    End Sub
End Class
