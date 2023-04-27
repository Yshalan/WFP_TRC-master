Imports Telerik.Web.UI
Imports TA_SchoolScheduling
Imports SmartV.UTILITIES
Imports System.Data
Imports System.Globalization

Partial Class SchoolScheduling_TeacherSchedule
    Inherits System.Web.UI.Page
#Region "Class Variables"
    Private objschoolSchedule As schoolSchedule
    Private objCourseTeachers As CourseTeachers
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Protected dir As String
    Public dtSchoolSchedule As New DataTable
#End Region

#Region "Properties"
    Public Property Lang() As CtlCommon.Lang
        Get
            Return ViewState("Lang")
        End Get
        Set(ByVal value As CtlCommon.Lang)
            ViewState("Lang") = value
        End Set
    End Property
#End Region

#Region "Page Events"
    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
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

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdTeacherSchedule.Skin))
    End Function

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        End If

        'dgrdClassSchoolScedule.ClientSettings.Scrolling.AllowScroll = True
        'dgrdClassSchoolScedule.ClientSettings.Scrolling.EnableVirtualScrollPaging = True
    End Sub

    Protected Sub dgrdTeacherSchedule_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdTeacherSchedule.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim Itm As GridDataItem = e.Item

            Dim EnDay = DirectCast(Itm.FindControl("hdnEnDay"), HiddenField)
            Dim ArDay = DirectCast(Itm.FindControl("hdnArDay"), HiddenField)


            Dim lblDay = DirectCast(Itm.FindControl("lblDay"), Label)
            Dim lblcourse1 = DirectCast(Itm.FindControl("lblCourse"), Label)

            If SessionVariables.CultureInfo = "ar-JO" Then
                lblDay.Text = ArDay.Value

            Else
                lblDay.Text = EnDay.Value
            End If

        End If
    End Sub



#End Region

#Region "Methods"

    Public Shared Function GetPageDirection() As String
        If SessionVariables.CultureInfo = "ar-JO" Then
            Return "rtl"
        Else
            Return "ltr"
        End If
    End Function
   
    Private Sub FillGrid()
        Dim dt As DataTable
        dt = New DataTable
        Dim dtweekday As DataTable

        objschoolSchedule = New schoolSchedule
        If EmployeeFilter.EmployeeId <> 0 Then
            dtweekday = objschoolSchedule.GetAll_WeekDays()
            objschoolSchedule = New schoolSchedule
            objschoolSchedule.FK_TeacherId = EmployeeFilter.EmployeeId
            Dim dtschedule As New DataTable
            dtschedule = objschoolSchedule.Fill_ByTeacherId
            Dim i As Integer
            For i = 1 To 8

                dtweekday.Columns.Add("Lesson" & i)

            Next
            dt = dtweekday.Clone
            For Each drow In dtschedule.Rows

                If SessionVariables.CultureInfo = "ar-JO" Then
                    dtweekday.Rows(drow("DayId") - 1)(drow("lesson") + 3) = "<strong>" & drow("ClassNameAr") & "</strong>" & "<br/>" & "<font color=""" & drow("color") & """><strong>" & drow("CourseNameAr") & "</strong></font>"
                Else
                    dtweekday.Rows(drow("DayId") - 1)(drow("lesson") + 3) = "<strong>" & drow("ClassName") & "</strong>" & "<br/>" & "<font color=""" & drow("color") & """><strong>" & drow("CourseName") & "</strong></font>"
                End If



            Next

            For Each row In dtweekday.Rows
                Dim dr As DataRow = dt.NewRow
                For z As Integer = 0 To dt.Columns.Count - 1
                    dr(z) = row(z)

                Next
                dt.Rows.Add(dr)
            Next
        End If

        dgrdTeacherSchedule.DataSource = dt
        dgrdTeacherSchedule.DataBind()
    End Sub



#End Region


   
    Protected Sub btnFilter_Click(sender As Object, e As System.EventArgs) Handles btnFilter.Click
        
        FillGrid()

    End Sub
End Class
