Imports TA.TaskManagement
Imports System.Data
Imports DayPilot.Web.Ui
Imports DayPilot.Web.Ui.Data
Imports DayPilot.Web.Ui.Enums
Imports DayPilot.Web.Ui.Enums.Gantt
Imports DayPilot.Web.Ui.Events.Gantt
Imports SmartV.UTILITIES
Imports System.Threading

Partial Class TaskManagement_UserControls_GanttChart
    Inherits System.Web.UI.UserControl

#Region "Class Variables"
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Public Lang As String

    Private objProject_tasks As Project_tasks
    Private objProject_Tasks_predecessor As Project_Tasks_predecessor

#End Region

#Region "Public Properties"

    Public Property ProjectId() As String
        Get
            Return ViewState("ProjectId")
        End Get
        Set(ByVal value As String)
            ViewState("ProjectId") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub TaskManagement_UserControls_GanttChart_Load(sender As Object, e As EventArgs) Handles Me.Load
        Gantt.DateTimeFormatInfo = New System.Globalization.DateTimeFormatInfo()

        If SessionVariables.CultureInfo = "en-US" Then
            CultureInfo = System.Globalization.CultureInfo.CurrentCulture
            Lang = CtlCommon.Lang.EN
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            CultureInfo = System.Globalization.CultureInfo.CurrentCulture
            Lang = CtlCommon.Lang.AR
        End If

        If Not Page.IsPostBack Then

            If SessionVariables.CultureInfo = "en-US" Then
                Lang = CtlCommon.Lang.EN
                CultureInfo = System.Globalization.CultureInfo.CurrentCulture
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                CultureInfo = System.Globalization.CultureInfo.CurrentCulture
            End If
            ProjectId = ProjectId
            LoadTasks()
            LoadLinks()

        End If

    End Sub

    Protected Sub Gantt_OnBeforeTaskRender(ByVal sender As Object, ByVal e As BeforeTaskRenderEventArgs)
        Dim daydiff As Integer
        Dim timediff As TimeSpan
        Dim inttimediff As Integer
        Dim strtimediff As String
        Dim formattedDuration As String

        daydiff = DateDiff(DateInterval.Day, e.Start, e.End)
        timediff = e.End - e.Start
        inttimediff = (timediff.Hours * 60) + timediff.Minutes
        strtimediff = CtlCommon.GetFullTimeString(inttimediff)
        formattedDuration = String.Format("dd({0}) - hh:mm({1})", daydiff, strtimediff)

        Dim duration As TimeSpan = e.End - e.Start
        'e.Row.Columns(1).Html = duration.ToString()
        e.Row.Columns(1).Html = formattedDuration
    End Sub

    Protected Sub Gantt_OnRowCreate(ByVal sender As Object, ByVal e As RowCreateEventArgs)

        Dim start As Date = Gantt.StartDate
        Dim [end] As Date = start.AddDays(1)
        Dim text As String = e.Text

        objProject_tasks = New Project_tasks
        Dim err As Integer = -1
        With objProject_tasks
            .FK_ProjectId = ProjectId
            .TaskName = text
            .TaskDescription = text
            .PlannedStartDate = start
            .PlannedEndDate = [end]
            .Priority = 1 '---Low
            .TaskSequence = "1"

            .CREATED_BY = SessionVariables.LoginUser.ID
            err = .Add

        End With
        LoadTasks()
        LoadLinks()
        Gantt.UpdateWithMessage("Task created.", CallBackUpdateType.Full)

    End Sub

    Protected Sub Gantt_OnCommand(ByVal sender As Object, ByVal e As CommandEventArgs)
        If e.Command = "refresh" Then
            LoadTasks()
            LoadLinks()
            Gantt.Update()
        End If

    End Sub

    Protected Sub Gantt_OnRowMenuClick(ByVal sender As Object, ByVal e As RowMenuClickEventArgs)
        Select Case e.Command
            Case "Delete"
                objProject_tasks = New Project_tasks
                Dim err As Integer = -1
                With objProject_tasks
                    .TaskId = e.Task.Id
                    .Delete()
                End With
                LoadTasks()
                LoadLinks()
                Gantt.Update()

                'Case "ToMilestone"
                '    Db.ToMilestone(e.Task.Id)
                '    LoadTasks()
                '    LoadLinks()
                '    Gantt.Update()
                'Case "ToTask"
                '    Db.ToTask(e.Task.Id)
                '    LoadTasks()
                '    LoadLinks()
                '    Gantt.Update()

        End Select
    End Sub

#End Region

#Region "Methods"

    Public Sub LoadTasks()
        Dim dt As DataTable
        objProject_tasks = New Project_tasks
        With objProject_tasks
            Gantt.DataStartField = "start"
            Gantt.DataEndField = "end"
            Gantt.DataIdField = "id"
            Gantt.DataTextField = "name"
            Gantt.DataParentField = "parent_id"
            Gantt.DataMilestoneField = "milestone"
            Gantt.DataCompleteField = "complete"

            .FK_ProjectId = ProjectId
            If Lang = CtlCommon.Lang.AR Then
                .Lang = "AR"
            Else
                .Lang = "EN"
            End If
            dt = .Get_by_FK_ProjectId_Gantt

            Dim NDT = New DataTable
            NDT = dt.Clone()

            NDT.Columns(8).DataType = System.Type.GetType("System.String")
            NDT.Columns(9).DataType = System.Type.GetType("System.String")
            For Each row As DataRow In dt.Rows
                Dim dr As DataRow = NDT.NewRow
                For i As Integer = 0 To NDT.Columns.Count - 1
                    dr(i) = row(i)
                    If i = 8 Then
                        If Not dr(i) Is DBNull.Value Then
                            dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy HH:mm")
                        End If
                    ElseIf i = 9 Then
                        If Not dr(i) Is DBNull.Value Then
                            dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy HH:mm")
                        End If

                    End If
                Next
                NDT.Rows.Add(dr)
            Next

            Gantt.DataSource = NDT
            Gantt.DataBind()
        End With
    End Sub

    Public Sub LoadLinks()
        Dim dt As DataTable
        objProject_tasks = New Project_tasks
        With objProject_tasks
            .FK_ProjectId = ProjectId
            dt = .Get_Predecessors_by_FK_ProjectId
        End With

        For Each row In dt.Rows
            Dim tasklink = New Link(row("from").ToString, row("to").ToString)
            tasklink.Id = row("id").ToString
            If row("type").ToString = "FS" Then
                tasklink.Type = 2
            ElseIf row("type").ToString = "SF" Then
                tasklink.Type = 3
            ElseIf row("type").ToString = "FF" Then
                tasklink.Type = 1
            Else
                tasklink.Type = 0
            End If

            Gantt.Links.Add(tasklink)
        Next

    End Sub

    Private Sub LoadChildTasks()



    End Sub

    Public Sub GetGanttDate()
        objProject_tasks = New Project_tasks
        Dim dt As DataTable
        Dim GanttStartDate As DateTime
        Dim GanttEndDate As DateTime
        Dim GanttDays As Integer
        With objProject_tasks
            If Not ProjectId Is Nothing Then
                .FK_ProjectId = ProjectId
                dt = .Get_Calendar_Dates()
                GanttStartDate = dt.Rows(0)("PlannedStartDate").ToString()
                GanttEndDate = dt.Rows(0)("PlannedEndDate").ToString()
                If GanttStartDate = Nothing Then
                    GanttStartDate = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
                End If
                If GanttEndDate = Nothing Then
                    Dim dd As New Date
                    dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(GanttStartDate.Year, GanttStartDate.Month).ToString("00") + "/" + GanttStartDate.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
                    GanttEndDate = dd
                End If
                GanttDays = DateDiff(DateInterval.Day, GanttStartDate, GanttEndDate)
            End If
        End With

        Gantt.StartDate = Convert.ToDateTime(GanttStartDate, CultureInfo)
        Gantt.Days = GanttDays

    End Sub

#End Region

End Class
