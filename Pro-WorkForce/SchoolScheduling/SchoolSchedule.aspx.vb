Imports Telerik.Web.UI
Imports TA_SchoolScheduling
Imports SmartV.UTILITIES
Imports System.Data

Partial Class SchoolScheduling_SchoolSchedule
    Inherits System.Web.UI.Page
#Region "Class Variables"
    Private objschoolSchedule As schoolSchedule
    Private objClasses As Classes
    Dim CultureInfo As System.Globalization.CultureInfo
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
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdSchoolScedule.Skin))
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
            FillGrid()
        End If
        
        
    End Sub

    Protected Sub dgrdSchoolScedule_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdSchoolScedule.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim Itm As GridDataItem = e.Item

            Dim EnClass = DirectCast(Itm.FindControl("hdnEnClass"), HiddenField)
            Dim ArClass = DirectCast(Itm.FindControl("hdnArClass"), HiddenField)
            Dim lblClass = DirectCast(Itm.FindControl("lblClass"), Label)

            If SessionVariables.CultureInfo = "ar-JO" Then
                lblClass.Text = ArClass.Value
            Else
                lblClass.Text = EnClass.Value
            End If

        End If
    End Sub

    Protected Sub dgrdSchoolScedule_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdSchoolScedule.NeedDataSource
        objschoolSchedule = New schoolSchedule
        dgrdSchoolScedule.DataSource = objschoolSchedule.GetAll_ForGrid
    End Sub
#End Region

#Region "Methods"
    Private Sub FillGrid()
        Dim dt As DataTable
        dt = New DataTable
        Dim dtClass As DataTable

        objschoolSchedule = New schoolSchedule
        objClasses = New Classes
        dtClass = objClasses.GetAll()
        objschoolSchedule = New schoolSchedule
        'objschoolSchedule.FK_ClassId = RadCmbClass.SelectedValue
        Dim dtschedule As New DataTable
        dtschedule = objschoolSchedule.GetAll_ForGrid
        Dim weekday As Integer
        Dim i As Integer
        For weekday = 1 To 5


            For i = 1 To 8

                dtClass.Columns.Add(weekday & "-Lesson" & i)

            Next
        Next
        Dim arrdrowschedule() As DataRow
        Dim classindex
        '  dtClass.Columns.Add("Color")
        For classindex = 0 To dtClass.Rows.Count - 1
            arrdrowschedule = dtschedule.Select("FK_ClassId=" & dtClass(classindex)("ClassId"))
            Dim j As Integer
            For j = 0 To arrdrowschedule.Length - 1

                If SessionVariables.CultureInfo = "ar-JO" Then
                    dtClass.Rows(classindex)((arrdrowschedule(j)("DayId") * 8) + arrdrowschedule(j)("lesson") - 1) = "<p style=""color: #FFFFFF; background-color:" & arrdrowschedule(j)("Color") & """>" & arrdrowschedule(j)("EmployeeNo") & "<br/>" & arrdrowschedule(j)("CourseNameAr") & "</p>"
                Else
                    'dtClass.Rows(classindex)((arrdrowschedule(j)("DayId") * 8) + arrdrowschedule(j)("lesson") - 1) = arrdrowschedule(j)("EmployeeNo") & "<br/>" & arrdrowschedule(j)("CourseName")
                    dtClass.Rows(classindex)((arrdrowschedule(j)("DayId") * 8) + arrdrowschedule(j)("lesson") - 1) = "<p style=""color: #FFFFFF; background-color:" & arrdrowschedule(j)("Color") & """>" & arrdrowschedule(j)("EmployeeNo") & "<br/>" & arrdrowschedule(j)("CourseName") & "</p>"

                End If

                


            Next
            Next







        dgrdSchoolScedule.DataSource = dtClass
        dgrdSchoolScedule.DataBind()

        dgrdSchoolScedule.ClientSettings.Scrolling.AllowScroll = True
        dgrdSchoolScedule.ClientSettings.Scrolling.EnableVirtualScrollPaging = True
    End Sub

#End Region
End Class
