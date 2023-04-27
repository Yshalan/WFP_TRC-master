Imports SmartV.UTILITIES
Imports TA.SelfServices
Imports System.Data
Imports System.Globalization
Imports System.Threading
Imports System.Drawing

Partial Class SelfServices_Manager_CalendarDetails
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Protected dir, textalign As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objEmployeeViolations As EmployeeViolations

#End Region

#Region "Public Properties"

    Public Property ApptType() As String
        Get
            Return ViewState("ApptType")
        End Get
        Set(ByVal value As String)
            ViewState("ApptType") = value
        End Set
    End Property

    Public Property FK_ManagerId() As Integer
        Get
            Return ViewState("FK_ManagerId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_ManagerId") = value
        End Set
    End Property

    Public Property ApptDate() As DateTime
        Get
            Return ViewState("ApptDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("ApptDate") = value
        End Set
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
                Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
            Else
                SessionVariables.CultureInfo = "ar-JO"
                dir = "rtl"
                textalign = "right"
                Lang = CtlCommon.Lang.AR
                Thread.CurrentThread.CurrentCulture = New CultureInfo("ar-EG")
            End If
        End If
    End Sub

    Protected Sub SelfServices_Manager_CalendarDetails_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ApptType = Request.QueryString("ApptType")
            ApptDate = Request.QueryString("ApptDate")
            FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            lblTypeVal.Text = ApptType
            lblDateVal.Text = (ApptDate).ToString("yyyy-MMMM-dd")
            lblDayVal.Text = WeekdayName(Weekday(Convert.ToDateTime(ApptDate)))
            FillEmployeeEvents()
            ' If ApptType = 0 Then
            If Lang = CtlCommon.Lang.AR Then
                dgrdCal_Details.Columns(5).Visible = False
            Else
                dgrdCal_Details.Columns(6).Visible = False
            End If
            'End If
        End If
    End Sub

    Protected Sub dgrdCal_Details_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdCal_Details.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            If item("IsFlexible").Text <> "&nbsp;" Then
                If item("IsFlexible").Text = "True" Then
                    If Lang = CtlCommon.Lang.AR Then
                        item("IsFlexible").Text = "مرن " & item("Details").Text
                    Else
                        item("IsFlexible").Text = "Flex " & item("Details").Text
                    End If

                End If
            End If
            If ApptType = "0" Then
                Dim ItemColor As System.Drawing.Color
                If Not item("ApptType").Text = "&nbsp;" Then
                    If item("ApptType").Text = "1" Then
                        ItemColor = Drawing.Color.FromArgb(75, 197, 122)
                    ElseIf item("ApptType").Text = "2" Then
                        ItemColor = Drawing.Color.FromArgb(159, 15, 25)
                    ElseIf item("ApptType").Text = "3" Then
                        ItemColor = Drawing.Color.FromArgb(151, 141, 117)
                    ElseIf item("ApptType").Text = "4" Then
                        ItemColor = Drawing.Color.FromArgb(151, 141, 117)
                    ElseIf item("ApptType").Text = "5" Then
                        ItemColor = Drawing.Color.FromArgb(226, 99, 103)
                    ElseIf item("ApptType").Text = "6" Then
                        ItemColor = Drawing.Color.FromArgb(140, 42, 143)
                    ElseIf item("ApptType").Text = "7" Then
                        ItemColor = Drawing.Color.FromArgb(213, 169, 46)
                    ElseIf item("ApptType").Text = "8" Then
                        ItemColor = Drawing.Color.FromArgb(65, 103, 176)
                    ElseIf item("ApptType").Text = "9" Then
                        ItemColor = Drawing.Color.FromArgb(113, 180, 212)
                    ElseIf item("ApptType").Text = "10" Then
                        ItemColor = Drawing.Color.FromArgb(219, 201, 146)
                    End If
                End If

                If Not item("ApptType").Text = "&nbsp;" Then
                    item("EmployeeNo").BackColor = ItemColor
                    item("EmployeeName").BackColor = ItemColor
                    item("EmployeeArabicName").BackColor = ItemColor
                    item("Details").BackColor = ItemColor
                    item("Cal_Subject").BackColor = ItemColor
                    item("Cal_SubjectAr").BackColor = ItemColor

                    item("EmployeeNo").ForeColor = Drawing.Color.White
                    item("EmployeeName").ForeColor = Drawing.Color.White
                    item("EmployeeArabicName").ForeColor = Drawing.Color.White
                    item("Details").ForeColor = Drawing.Color.White
                    item("Cal_Subject").ForeColor = Drawing.Color.White
                    item("Cal_SubjectAr").ForeColor = Drawing.Color.White
                End If
            End If
        End If
    End Sub

    Protected Sub dgrdCal_Details_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdCal_Details.NeedDataSource
        objEmployeeViolations = New EmployeeViolations
        Dim dt As DataTable

        If ApptType.Contains("Attendees") Or ApptType.Contains("الحضور") Then
            ApptType = 1
        ElseIf ApptType.Contains("Absent") Or ApptType.Contains("الغياب") Then
            ApptType = 2
        ElseIf ApptType.Contains("Missing In") Or ApptType.Contains("لايوجد دخول") Then
            ApptType = 3
        ElseIf ApptType.Contains("Missing Out") Or ApptType.Contains("لايوجد خروج") Then
            ApptType = 4
        ElseIf ApptType.Contains("Delay") Or ApptType.Contains("التأخير") Then
            ApptType = 5
        ElseIf ApptType.Contains("Early Out") Or ApptType.Contains("الخروج المبكر") Then
            ApptType = 6
        ElseIf ApptType.Contains("Permissions") Or ApptType.Contains("المغادرات") Then
            ApptType = 7
        ElseIf ApptType.Contains("Leaves") Or ApptType.Contains("الاجازات") Then
            ApptType = 8
        ElseIf ApptType.Contains("Leave Request") Or ApptType.Contains("طلب اجازة") Then
            ApptType = 9
        ElseIf ApptType.Contains("Permission Request") Or ApptType.Contains("طلب مغادرة") Then
            ApptType = 10
        End If

        With objEmployeeViolations
            .ApptType = ApptType
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FromDate = Convert.ToDateTime(ApptDate)
            dt = .GetManagerCalendarDetails()
        End With
        dgrdCal_Details.DataSource = dt
    End Sub

#End Region

#Region "Methods"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdCal_Details.Skin))
    End Function

    Private Sub FillEmployeeEvents()
        objEmployeeViolations = New EmployeeViolations
        Dim dt As DataTable

        If ApptType.Contains("Attendees") Or ApptType.Contains("الحضور") Then
            ApptType = 1
        ElseIf ApptType.Contains("Absent") Or ApptType.Contains("الغياب") Then
            ApptType = 2
        ElseIf ApptType.Contains("Missing In") Or ApptType.Contains("لايوجد دخول") Then
            ApptType = 3
        ElseIf ApptType.Contains("Missing Out") Or ApptType.Contains("لايوجد خروج") Then
            ApptType = 4
        ElseIf ApptType.Contains("Delay") Or ApptType.Contains("التأخير") Then
            ApptType = 5
        ElseIf ApptType.Contains("Early Out") Or ApptType.Contains("الخروج المبكر") Then
            ApptType = 6
        ElseIf ApptType.Contains("Permissions") Or ApptType.Contains("المغادرات") Then
            ApptType = 7
        ElseIf ApptType.Contains("Leaves") Or ApptType.Contains("الاجازات") Then
            ApptType = 8
        ElseIf ApptType.Contains("Leave Request") Or ApptType.Contains("طلب اجازة") Then
            ApptType = 9
        ElseIf ApptType.Contains("Permission Request") Or ApptType.Contains("طلب مغادرة") Then
            ApptType = 10
        Else
            ApptType = 0
        End If

        With objEmployeeViolations
            .ApptType = ApptType
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FromDate = Convert.ToDateTime(ApptDate)
            dt = .GetManagerCalendarDetails()
        End With
        dgrdCal_Details.DataSource = dt
        dgrdCal_Details.DataBind()
    End Sub

#End Region


End Class
