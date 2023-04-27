Imports System.Data
Imports TA.Employees
Imports SmartV.UTILITIES
Imports System.Web.UI.WebControls
Imports Telerik.Web.UI
Imports TA.Admin
Imports TA.Security
Imports TA_Announcements


Partial Class Admin_AnnouncementDetailsPopup
    Inherits System.Web.UI.Page



#Region "Class Variables"

    Private objAnnouncements As Announcements
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Properties"

    Private Property ID() As Integer
        Get
            Return ViewState("ID")
        End Get
        Set(ByVal value As Integer)
            ViewState("ID") = value
        End Set
    End Property


#End Region

#Region "PageEvents"



    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")

        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                lblEnglishContent.Visible = False

                lblEnglishTitle.Visible = False
            Else
                Lang = CtlCommon.Lang.EN
                lblArabicContent.Visible = False

                lblArabicTitle.Visible = False


            End If
            UserCtrlAnnouncementsDetails.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تفاصيل الاعلانات", "Announcement Details")
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If Request.QueryString("AnnouncementID") <> "" Then
                ID = Convert.ToInt32(Request.QueryString("AnnouncementID"))
                FillData()
            End If

        End If


    End Sub





#End Region

#Region "Methods"

    Private Sub FillData()
        objAnnouncements = New Announcements()
        objAnnouncements.ID = ID
        objAnnouncements.GetByPK()
        With objAnnouncements

            lblEnglishTitle.Text() = .Title_En
            lblArabicTitle.Text() = .Title_Ar
            lblEnglishContent.Text() = .Content_En
            lblArabicContent.Text() = .Content_Ar
            lblDay.Text() = .DayNo
            If SessionVariables.CultureInfo = "ar-JO" Then
                lblMonth.Text = .MonthArabic
            Else
                lblMonth.Text = .MonthEnglish
            End If


            Dim monthno As Integer = .AnnouncementDate.Month()
            'lblMonth.Text() = System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(monthno).ToString()
        End With
    End Sub


#End Region



End Class
