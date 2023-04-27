Imports Telerik.Web.UI
Imports SmartV.UTILITIES
Imports System.Web.UI.WebControls
Imports TA_SchoolScheduling
Imports System.Data
Imports TA.Security
Imports TA_SchoolScheduling.Classes
Imports TA.Employees


Partial Class SchoolScheduling_Course
    Inherits System.Web.UI.Page

#Region "Class Variables"
    Private objCourse As Course
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
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

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

    Public Property CourseId() As Integer
        Get
            Return ViewState("CourseId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CourseId") = value
        End Set
    End Property
    Public Property CoursesWeeklyCount() As Integer
        Get
            Return ViewState("CoursesWeeklyCount")
        End Get
        Set(ByVal value As Integer)
            ViewState("CoursesWeeklyCount") = value
        End Set
    End Property
#End Region

#Region "Page events"

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

        End If

    End Sub

    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        btnGenerate.Enabled = False
        ' GenerateSchedulebyclass()
        GenerateSchedueByteacher()

    End Sub

#End Region

#Region "Methods"

    'Private Sub GenerateSchedulebyclass()




    '    Dim objclass As New TA_SchoolScheduling.Classes


    '    Dim classdt As DataTable
    '    classdt = objclass.GetAll()
    '    Dim classid As Integer
    '    For Each row As DataRow In classdt.Rows
    '        classid = row("ClassId")
    '        getClassTeachers(classid)
    '    Next

    '    showResult(ProjectCommon.CodeResultMessage.CodeSaveSucess)

    'End Sub

    Private Sub GenerateSchedueByteacher()
        Dim objteacher As New Employee
        Dim dtteacher As DataTable
        Dim teacherId As Integer
        Dim Breaks As Integer()

        dtteacher = objteacher.GetAll()
        For Each row As DataRow In dtteacher.Rows
            teacherId = row("EmployeeId")
            Breaks = getBreakDaily(teacherId)
            getTeacherClasses(teacherId, Breaks)
        Next
        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully"), "success")
    End Sub
    Private Sub getTeacherClasses(ByVal tTeacherId As Integer, ByVal Breaks As Integer())
        Dim objTeacherClasses As New TA_SchoolScheduling.TeacherClasses
        Dim dtTeacherClasses As DataTable
        Dim tClassId As Integer
        Dim tCourseId As Integer
        Dim tWeeklyCount As Integer
        Dim i As Integer
        objTeacherClasses.FK_EmployeeId = tTeacherId
        dtTeacherClasses = objTeacherClasses.GetAllByEmployee()
        For Each trow As DataRow In dtTeacherClasses.Rows
            tCourseId = trow("FK_CourseId")
            tTeacherId = trow("FK_EmployeeId")
            tWeeklyCount = trow("weeklyCount")
            tClassId = trow("FK_ClassId")
            saveOnEmptylesson(tClassId, tCourseId, tTeacherId, tWeeklyCount, Breaks)
        Next

    End Sub

    'Private Sub getClassTeachers(ByVal tClassId As Integer)
    '    Dim objTeacherClasses As New TA_SchoolScheduling.TeacherClasses
    '    Dim dtTeacherClasses As DataTable
    '    Dim tTeacherId As Integer
    '    Dim tCourseId As Integer
    '    Dim tWeeklyCount As Integer
    '    Dim i As Integer
    '    objTeacherClasses.FK_ClassId = tClassId
    '    dtTeacherClasses = objTeacherClasses.GetAllByClassId()

    '    For Each trow As DataRow In dtTeacherClasses.Rows
    '        tCourseId = trow("FK_CourseId")
    '        tTeacherId = trow("FK_EmployeeId")
    '        tWeeklyCount = trow("weeklyCount")

    '        saveOnEmptylesson(tClassId, tCourseId, tTeacherId, tWeeklyCount)
    '    Next

    'End Sub
    Public Function RandomNumber(ByVal teacherId As Integer, ByVal MaxNumber As Integer, Optional ByVal MinNumber As Integer = 0) As Integer

        'initialize random number generator
        'Dim r As New Random(System.DateTime.Now.Millisecond)
        Dim r As New Random(teacherId)
        'if passed incorrect arguments, swap them
        'can also throw exception or return 0

        If MinNumber > MaxNumber Then
            Dim t As Integer = MinNumber
            MinNumber = MaxNumber
            MaxNumber = t
        End If

        Return r.Next(MinNumber, MaxNumber)

    End Function


    Private Function getBreakDaily(ByVal teacherID As Integer) As Integer()
        Dim breaklesson As Integer
        Dim breaklesson1(4) As Integer

        breaklesson = RandomNumber(teacherID, 5, 1)
        Dim i As Integer
        For i = 0 To 4
            breaklesson1(i) = (breaklesson + 1 + i) Mod 5
            If breaklesson1(i) = 0 Then
                breaklesson1(i) = 5
            End If
        Next
        'breaklesson = RandomNumber(teacherID + 1, 6, 1)
        'For i = 5 To 9
        '    breaklesson1(i) = (breaklesson + 1 + i) Mod 6
        '    If breaklesson1(i) = 0 Then
        '        breaklesson1(i) = 6
        '    End If
        'Next


        Return breaklesson1

    End Function
    Private Sub saveOnEmptylesson(ByVal lclassid As Integer, ByVal lCourseId As Integer, ByVal lTeacherId As Integer, ByVal weeklyCount As Integer, ByVal Breaks As Integer())
        Dim objschoolSchedule As TA_SchoolScheduling.schoolSchedule

        Dim dayId As Integer
        Dim LessonId As Integer
        Dim counter As Integer = 0
        Dim ClassWeeklyCount As Integer
        Dim internalcount As Integer


        Dim ObjTeacherClasses As New TeacherClasses
        ObjTeacherClasses.FK_ClassId = lclassid
        ClassWeeklyCount = ObjTeacherClasses.GetClassWeeklyCount().Rows(0)(0)
        Dim dailyCount As Integer = ClassWeeklyCount / 5
        Dim RemainingDailyCount As Integer = ClassWeeklyCount Mod 5


        For lLessonId = 1 To dailyCount + 1

            If lLessonId = dailyCount + 1 And RemainingDailyCount = 0 Then
                RemainingDailyCount = RemainingDailyCount - 1
                Continue For
            End If


            For ldayId = 1 To 5
                If lLessonId = Breaks(ldayId - 1) Then ''  Or lLessonId = Breaks((ldayId * 2) - 1)

                    lblresult.Text = lblresult.Text & lTeacherId & ":<br/>"
                    lblresult.Text = lblresult.Text & ldayId & "--" & lLessonId & ":<br/>"
                    Continue For
                End If

                If ((lLessonId > dailyCount) And (ldayId > ClassWeeklyCount Mod 5)) Then
                    Continue For
                End If
                objschoolSchedule = New TA_SchoolScheduling.schoolSchedule
                With objschoolSchedule
                    .FK_ClassId = lclassid
                    .FK_CourseId = lCourseId
                    .FK_TeacherId = lTeacherId
                    .DayId = ldayId
                    .lesson = lLessonId

                    If counter < weeklyCount Then
                        internalcount = .CheckIfEmpty(RBLsequential.SelectedValue, RBLdistributed.SelectedValue, IIf(txtMaxLessons.Text.Trim = "", 0, CInt(txtMaxLessons.Text)))
                        If internalcount = 0 Then
                            counter = counter + 1
                        End If
                    Else
                        Exit For
                        Exit For
                    End If

                End With
            Next

        Next
        If counter < weeklyCount Then


            For lLessonId = 1 To dailyCount + 1

                If lLessonId = dailyCount + 1 And RemainingDailyCount = 0 Then
                    RemainingDailyCount = RemainingDailyCount - 1
                    Continue For
                End If


                For ldayId = 1 To 5


                    If ((lLessonId > dailyCount) And (ldayId > ClassWeeklyCount Mod 5)) Then
                        Continue For
                    End If
                    objschoolSchedule = New TA_SchoolScheduling.schoolSchedule
                    With objschoolSchedule
                        .FK_ClassId = lclassid
                        .FK_CourseId = lCourseId
                        .FK_TeacherId = lTeacherId
                        .DayId = ldayId
                        .lesson = lLessonId

                        If counter < weeklyCount Then
                            internalcount = .CheckIfEmpty(RBLsequential.SelectedValue, RBLdistributed.SelectedValue, IIf(txtMaxLessons.Text.Trim = "", 0, CInt(txtMaxLessons.Text)))
                            If internalcount = 0 Then
                                counter = counter + 1
                            End If
                        Else
                            Exit For
                            Exit For
                        End If

                    End With
                Next

            Next

        End If

        If counter < weeklyCount Then






            For lLessonId = 1 To dailyCount + 1

                If lLessonId = dailyCount + 1 And RemainingDailyCount = 0 Then
                    RemainingDailyCount = RemainingDailyCount - 1
                    Continue For
                End If


                For ldayId = 1 To 5


                    If ((lLessonId > dailyCount) And (ldayId > ClassWeeklyCount Mod 5)) Then
                        Continue For
                    End If
                    objschoolSchedule = New TA_SchoolScheduling.schoolSchedule
                    With objschoolSchedule
                        .FK_ClassId = lclassid
                        .FK_CourseId = lCourseId
                        .FK_TeacherId = lTeacherId
                        .DayId = ldayId
                        .lesson = lLessonId

                        If counter < weeklyCount Then
                            internalcount = .CheckIfEmpty(0, 0, IIf(txtMaxLessons.Text.Trim = "", 0, 10))
                            If internalcount = 0 Then
                                counter = counter + 1
                            End If
                        Else
                            Exit For
                            Exit For
                        End If

                    End With
                Next

            Next

        End If




    End Sub



#End Region


End Class
