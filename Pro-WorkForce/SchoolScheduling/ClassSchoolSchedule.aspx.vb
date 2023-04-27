Imports Telerik.Web.UI
Imports TA_SchoolScheduling
Imports SmartV.UTILITIES
Imports System.Data
Imports System.Globalization

Partial Class SchoolScheduling_ClassSchoolSchedule
    Inherits System.Web.UI.Page

#Region "Class Variables"
    Private objschoolSchedule As schoolSchedule
    Private objClassGrade As ClassGrade
    Private objClasses As Classes
    Dim CultureInfo As System.Globalization.CultureInfo
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
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdClassSchoolScedule.Skin))
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

            FillClassGrade()

        End If

    End Sub

    Protected Sub dgrdClassSchoolScedule_DataBound(sender As Object, e As System.EventArgs) Handles dgrdClassSchoolScedule.DataBound


        'Dim value As String = ""


        'For Each item As GridDataItem In dgrdClassSchoolScedule.MasterTableView.Items
        '    Dim cell As TableCell = item("Color")
        '    If Not cell.Text = value Then
        '        value = cell.Text
        '        cell.Style.Item(HtmlTextWriterStyle.BackgroundColor) = value
        '        cell.Text = ""
        '    End If
        'Next
    End Sub

    Protected Sub dgrdClassSchoolScedule_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdClassSchoolScedule.ItemDataBound
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

    Protected Sub dgrdClassSchoolScedule_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdClassSchoolScedule.NeedDataSource

        If RadCmbClass.SelectedValue <> "" Then

            objschoolSchedule = New schoolSchedule
            dgrdClassSchoolScedule.DataSource = objschoolSchedule.Fill_ByClassId
        End If
    End Sub

    Protected Sub RadCmbGrade_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbGrade.SelectedIndexChanged
        RadCmbClass.Items.Clear()
        RadCmbClass.SelectedValue = -1

        FillClass()
    End Sub

    Protected Sub RadCmbClass_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbClass.SelectedIndexChanged
        FillGrid()
    End Sub
#End Region

#Region "Methods"
    Public Sub FillWeekDays()
        Try

            objschoolSchedule = New schoolSchedule

            dgrdClassSchoolScedule.DataSource = objschoolSchedule.GetAll_WeekDays()
            dgrdClassSchoolScedule.DataBind()


        Catch ex As Exception
        End Try
    End Sub

    Sub ClearAll()
        RadCmbClass.SelectedValue = -1
        RadCmbGrade.SelectedValue = -1

    End Sub
    Private Sub FillClassGrade()

        Dim dt As DataTable = Nothing
        objClassGrade = New ClassGrade()
        dt = objClassGrade.GetAll()
        CtlCommon.FillTelerikDropDownList(RadCmbGrade, dt, Lang)
    End Sub
    Private Sub FillClass()
        Dim dt As DataTable = Nothing
        dt = New DataTable
        objClasses = New Classes
        objClasses.FK_ClassGradeId = RadCmbGrade.SelectedValue
        dt = objClasses.GetAll_ByClassGradeId()
        CtlCommon.FillTelerikDropDownList(RadCmbClass, dt, Lang)
    End Sub


    Private Sub FillGrid()
        Dim dt As DataTable
        dt = New DataTable
        Dim dtweekday As DataTable

        objschoolSchedule = New schoolSchedule
        If RadCmbClass.SelectedValue <> "" Then
            dtweekday = objschoolSchedule.GetAll_WeekDays()
            objschoolSchedule = New schoolSchedule
            objschoolSchedule.FK_ClassId = RadCmbClass.SelectedValue
            Dim dtschedule As New DataTable
            dtschedule = objschoolSchedule.Fill_ByClassId
            Dim i As Integer
            For i = 1 To 8

                dtweekday.Columns.Add("Lesson" & i)

            Next
            dt = dtweekday.Clone
            For Each drow In dtschedule.Rows
                If SessionVariables.CultureInfo = "ar-JO" Then
                    dtweekday.Rows(drow("DayId") - 1)(drow("lesson") + 3) = "<font color=""" & drow("color") & """><strong>" & drow("CourseNameAr") & "</strong></font>" & "<br/>" & drow("EmployeeArabicName")
                Else
                    dtweekday.Rows(drow("DayId") - 1)(drow("lesson") + 3) = "<font color=""" & drow("color") & """><strong>" & drow("CourseName") & "</strong></font>" & "<br/>" & drow("EmployeeName")
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

        dgrdClassSchoolScedule.DataSource = dt
        dgrdClassSchoolScedule.DataBind()

        'dgrdClassSchoolScedule.ClientSettings.Scrolling.AllowScroll = True
        'dgrdClassSchoolScedule.ClientSettings.Scrolling.EnableVirtualScrollPaging = True
    End Sub



#End Region


 
End Class
