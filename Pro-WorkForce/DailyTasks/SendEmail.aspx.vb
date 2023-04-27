Imports SmartV.UTILITIES
Imports TA.Admin
Imports TA.LookUp
Imports System.Data
Imports Telerik.Web.UI
Imports TA.Employees
Imports System.IO
Imports System.Net.Mail

Partial Class Admin_EmployeeSearch
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Protected dir, textalign As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objEmployee As New Employee

#End Region

#Region "Properties"
    Public Property dtCurrent() As DataTable
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
        Get
            Return (ViewState("dtCurrent"))
        End Get
    End Property
    Public Property EmployeeId() As Integer
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
        Get
            Return (ViewState("EmployeeId"))
        End Get
    End Property
    Public Property CompanyId() As Integer
        Set(ByVal value As Integer)
            ViewState("CompanyId") = value
        End Set
        Get
            Return (ViewState("CompanyId"))
        End Get
    End Property
    Public Property EmployeeNo() As String
        Set(ByVal value As String)
            ViewState("EmployeeNo") = value
        End Set
        Get
            Return (ViewState("EmployeeNo"))
        End Get
    End Property
#End Region

#Region "Page Events"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If (SessionVariables.CultureInfo Is Nothing) Then
            SessionVariables.CultureInfo = "en-US"
            dir = "ltr"
            textalign = "left"
        Else
            If SessionVariables.CultureInfo = "en-US" Then
                SessionVariables.CultureInfo = "en-US"
                dir = "ltr"
                textalign = "left"
                Lang = CtlCommon.Lang.EN
            Else
                SessionVariables.CultureInfo = "ar-JO"
                dir = "rtl"
                textalign = "right"
                Lang = CtlCommon.Lang.AR
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")


            Else

                '    Lang = CtlCommon.Lang.EN
                '    lblSend.Text = "Please Enter Email Address to Send Notification"

                'End If
                'CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
                'PageHeader1.HeaderText = ResourceManager.GetString("EnterEmail", CultureInfo)
                'Me.Title = ResourceManager.GetString("EnterEmail", CultureInfo)

                ''If Not Request.QueryString("SourceControlId") Is Nothing Then
                ''    Session("SourceControl_Session") = Request.QueryString("SourceControlId")
                ''End If
            End If
        End If

    End Sub



#End Region


End Class
