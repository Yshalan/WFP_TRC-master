Imports SmartV.UTILITIES
Imports TA.Definitions
Imports TA.Employees
Imports System.Data
Imports TA.Admin
Imports TA.LookUp
Partial Class SelfServices_UserControls_ScheduleInfo
    Inherits System.Web.UI.UserControl
#Region "Class Variables"

    Private objWorkSchedule As WorkSchedule
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objWorkSchedule_NormalTime As WorkSchedule_NormalTime
    Private objWorkSchedule_Shifts As WorkSchedule_Shifts
    Private objEmp_WorkSchedule As Emp_WorkSchedule
    Public Lang As String
    Public dawte As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Private objAPP_Settings As APP_Settings
    Private objEmployee As Employee
#End Region
#Region "Public Properties"

    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Private Property ScheduleType() As Integer
        Get
            Return ViewState("ScheduleType")
        End Get
        Set(ByVal value As Integer)
            ViewState("ScheduleType") = value
        End Set
    End Property

    Private Property ScheduleId() As Integer
        Get
            Return ViewState("ScheduleId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ScheduleId") = value
        End Set
    End Property

    Public Property VisibleInfoCount() As Integer
        Get
            Return ViewState("VisibleInfoCount")
        End Get
        Set(ByVal value As Integer)
            ViewState("VisibleInfoCount") = value
        End Set
    End Property

#End Region

    Public Sub FillScheduleInfo()
        Try
            If SessionVariables.CultureInfo = "en-US" Then
                Lang = "en"
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = "Ar"
            End If

            objEmployee = New Employee
            With objEmployee
                .EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                .GetByPK()
                lblEmpCardNoValue.Text = .EmployeeCardNo
            End With

            lblScheduleType.Visible = True
            lblScheduleTypeValue.Visible = True
            lblSchedule.Visible = True
            lblScheduleValue.Visible = True
            lblInTime.Visible = True
            lblTimeVal.Visible = True
            lblExpectOut.Visible = True
            lblExpectOutValue.Visible = True
            lblStatus.Visible = True
            lblStatusVal.Visible = True

            EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            If Not EmployeeId = Nothing Then

                objEmp_WorkSchedule = New Emp_WorkSchedule
                With objEmp_WorkSchedule
                    Dim dt As DataTable = Nothing
                    .FK_EmployeeId = EmployeeId
                    .M_Date = Now.Date
                    dt = .GetAll_ScheduleDeatils()
                    ScheduleType = dt.Rows(0)("ScheduleType")
                    If Not ScheduleType = Nothing Then
                        If Lang = "Ar" Then
                            lblStatusVal.Text = IIf(dt.Rows(0)("IsOffDay") = False, "يوم عمل", "يوم استراحة")
                        Else
                            lblStatusVal.Text = IIf(dt.Rows(0)("IsOffDay") = False, "Work Day", "Rest Day")
                        End If
                        If dt.Rows(0)("IsHoliday") = 1 Then
                            lblStatusVal.Text = IIf(Lang = "Ar", "عطلة رسمية", "Holiday")
                        End If


                        If Not IsDBNull(dt.Rows(0)("InTime")) Then
                            lblInTimeValue.Text = dt.Rows(0)("InTime")
                        End If

                        lblScheduleValue.Text = IIf(Lang = "Ar", dt.Rows(0)("ScheduleArabicName"), dt.Rows(0)("ScheduleName"))

                        If Not IsDBNull(dt.Rows(0)("ExpectedOutTime")) Then
                            lblExpectOutValue.Text = dt.Rows(0)("ExpectedOutTime")
                        End If
                        If IsDBNull(dt.Rows(0)("IsOffDay")) Then
                            lblExpectOut.Visible = False
                            lblExpectOutValue.Visible = False
                            lblTimeVal.Visible = False
                            lblInTime.Visible = False
                            lblInTimeValue.Visible = False
                        ElseIf dt.Rows(0)("IsOffDay") = True Then
                            lblExpectOut.Visible = False
                            lblExpectOutValue.Visible = False
                            lblTimeVal.Visible = False
                            lblInTime.Visible = False
                            lblInTimeValue.Visible = False
                        End If



                        If ScheduleType = 1 Then
                            lblScheduleTypeValue.Text = IIf(Lang = "Ar", "جدول عمل عادي", "Normal Schedule")
                            lblTimeVal.Text = "( " & dt.Rows(0)("SCH_START_TIME_STR") & "-" & dt.Rows(0)("SCH_END_TIME_STR") & " )"
                            lblScheduleValue.Visible = False
                        ElseIf ScheduleType = 2 Then
                            lblScheduleTypeValue.Text = IIf(Lang = "Ar", "جدول عمل مرن", "Flexible Schedule")

                            lblTimeVal.Text = "( " & dt.Rows(0)("SCH_START_TIME_STR") & "-" & dt.Rows(0)("SCH_END_TIME_STR") &
                              IIf(Lang = "Ar", " الوقت المرن ", " Flexible Time ") & dt.Rows(0)("FlixTime1") & " )"
                        ElseIf ScheduleType = 3 Then
                            lblScheduleTypeValue.Text = IIf(Lang = "Ar", "جدول عمل المناوبات", "Advanced Schedule")
                            lblTimeVal.Text = "(" & dt.Rows(0)("SCH_START_TIME_STR") & "-" & dt.Rows(0)("SCH_END_TIME_STR") & " ( " &
                                 IIf(Lang = "Ar", dt.Rows(0)("ShiftArabicName"), dt.Rows(0)("ShiftName")) & " )" & ")"
                        ElseIf ScheduleType = 4 Then
                            lblScheduleTypeValue.Text = IIf(Lang = "Ar", "جدول مناوبات المجموعات", "Group Advanced Schedule")
                            lblTimeVal.Text = "( " & IIf(Lang = "Ar", dt.Rows(0)("ShiftArabicName"), dt.Rows(0)("ShiftName")) & " ( " &
                                dt.Rows(0)("SCH_START_TIME_STR") & "-" & dt.Rows(0)("SCH_END_TIME_STR") & " )" & ")"
                        End If

                    Else
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("WorkScheduleNotExist", CultureInfo), "info")
                    End If
                End With
            End If
        Catch ex As Exception
            CtlCommon.CreateErrorLog("Schedule Info - self service home", ex.Message, "FillScheduleInfo")
        End Try

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If SessionVariables.CultureInfo = "en-US" Then
            Lang = "en"
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            Lang = "ar"
        End If

        If Not Page.IsPostBack Then
            FillScheduleInfo()
            VisibleInfo()
        End If
    End Sub

    Private Sub VisibleInfo()
        Dim RequestGridToAppear As String = ""
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            VisibleInfoCount = 0
            For Each i As String In .DurationTotalsToAppear.Split(",")
                If i = "1" Then
                    divInTime.Visible = True
                    VisibleInfoCount = VisibleInfoCount + 1
                ElseIf i = "2" Then
                    divScheduleType.Visible = True
                    VisibleInfoCount = VisibleInfoCount + 1
                ElseIf i = "3" Then
                    divSchedule.Visible = True
                    divStatus.Visible = True
                    VisibleInfoCount = VisibleInfoCount + 1
                ElseIf i = "4" Then
                    divExpectOut.Visible = True
                    VisibleInfoCount = VisibleInfoCount + 1
                ElseIf i = "19" Then
                    dvEmpCardNo.Visible = True
                    VisibleInfoCount = VisibleInfoCount + 1

                End If
            Next
        End With

    End Sub
End Class
