Imports Telerik.Web.UI
Imports TA.Employees
Imports SmartV.UTILITIES

Partial Class Admin_LeaveBalance
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Dim objEmp_Leaves_BalanceHistory As Emp_Leaves_BalanceHistory
    Dim objEmp_Leaves_BalanceExpired As Emp_Leaves_BalanceExpired
    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

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
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            TabContainer1.ActiveTabIndex = 0
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

#End Region

#Region "Methods"

    Public Sub FillGrid()
        objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
        dgrdBalance.DataSource = objEmp_Leaves_BalanceHistory.GetAll()
        dgrdBalance.DataBind()

        objEmp_Leaves_BalanceExpired = New Emp_Leaves_BalanceExpired
        dgrdBalanceExpired.DataSource = objEmp_Leaves_BalanceExpired.GetAll()
        dgrdBalanceExpired.DataBind()
    End Sub

    Public Sub FillExpiredBalance()

    End Sub

#End Region

End Class
