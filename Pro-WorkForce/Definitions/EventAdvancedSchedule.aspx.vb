Imports System.Data
Imports TA.Lookup
Imports TA.Definitions
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports System.Globalization
Imports TA.Security
Imports TA.Employees
Imports TA.Forms

Partial Class Definitions_EventAdvancedSchedule_
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Public Lang As CtlCommon.Lang
    Public AddButtonValue As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objEvents_Employees_Schedule As Events_Employees_Schedule
    Dim objEmployee As Employee
    Dim objEventEmployee As Events_Employees
    Dim objEvents As Events
    Dim objEmpLeave As Emp_Leaves
    Public strLang As String

#End Region

#Region "Page Propereties"

    Public Property EventID() As Integer
        Get
            Return ViewState("EventID")
        End Get
        Set(ByVal value As Integer)
            ViewState("EventID") = value
        End Set
    End Property

    Public Property FromDate() As DateTime
        Get
            Return ViewState("FromDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("FromDate") = value
        End Set
    End Property

    Public Property ToDate() As DateTime
        Get
            Return ViewState("ToDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("ToDate") = value
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

    Public Property PageAction() As String
        Get
            Return ViewState("Action")
        End Get
        Set(ByVal value As String)
            ViewState("Action") = value
        End Set
    End Property

    Public Property CompareDate() As DateTime
        Get
            Return ViewState("ConpareDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("ConpareDate") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
                strLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
                strLang = "en"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If Not Request.QueryString("EventId") Is Nothing Then
                EventID = CInt(Request.QueryString("EventId"))
            End If

            PageHeader1.HeaderText = ResourceManager.GetString("EventAdvancedSchedule", CultureInfo)

            ClearAll()

            objEvents_Employees_Schedule = New Events_Employees_Schedule()
            objEvents_Employees_Schedule.FK_EventId = EventID
            Dim dtEmployees_Schedule As DataTable = objEvents_Employees_Schedule.GetAllByEventID()

            For Each row As DataRow In dtEmployees_Schedule.Rows
                objEmployee = New Employee()
                With objEmployee
                    Dim empID As Integer = CInt(row("FK_EmployeeId"))
                    .EmployeeId = empID
                    .GetByPK()
                    Dim empNo As String = .EmployeeNo
                    hdnSelectedEmployees.Value += "," + empNo.ToString()

                    objEmpLeave = New Emp_Leaves()
                    objEmpLeave.FK_EmployeeId = .EmployeeId
                    Dim dtempLeaves As DataTable = objEmpLeave.GetAllLeavesByEmployee()

                    For Each rowLeave As DataRow In dtempLeaves.Rows
                        Dim leaveFromDate As DateTime = Convert.ToDateTime(rowLeave("FromDate"))
                        Dim leaveToDate As DateTime = Convert.ToDateTime(rowLeave("ToDate"))

                        While leaveFromDate <= leaveToDate
                            hdnLeaveDays.Value += "," + leaveFromDate.Day.ToString()
                            hdnLeaveMonths.Value += "," + leaveFromDate.Month.ToString()
                            hdnLeaveEmployee.Value += "," + .EmployeeNo.ToString()
                            leaveFromDate = leaveFromDate.AddDays(1)
                        End While
                    Next

                End With
                hdnSelectedShift.Value += "," + row("Shift").ToString()
                hdnSavedDays.Value += "," + (Convert.ToDateTime(row("ScheduleDate")).Day).ToString()
                hdnSelectedEmployeesIDs.Value += "," + row("FK_EmployeeId").ToString()
            Next

            If (Not dtEmployees_Schedule Is Nothing) AndAlso dtEmployees_Schedule.Rows.Count > 0 Then
                PageAction = "Edit"
            Else
                PageAction = "Add"
            End If

            objEvents = New Events()
            With objEvents
                .EventId = EventID()
                .GetByPK()
                CompareDate = .StartDate
                FromDate = .StartDate
                ToDate = .EndDate
                lblEventName.Text += ": " + .EventName
                lblEventDate.Text += ": " + .StartDate.ToShortDateString() + " - " + .EndDate.ToShortDateString()
            End With

            While FromDate <= ToDate
                Dim dayOfWeek As String = FromDate.DayOfWeek
                hdnDays.Value += "," + FromDate.Day.ToString()
                hdnMonths.Value += "," + FromDate.Month.ToString()
                FromDate = FromDate.AddDays(1)
            End While

            Dim intTotalDays As Integer = ToDate.Subtract(FromDate).TotalDays

            'hdnDays.Value = intTotalDays

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Fill Table", "ClearScheduleGrid();", True)

            objEventEmployee = New Events_Employees()
            objEventEmployee.FK_EventId = EventID
            dtCurrent = objEventEmployee.Get_All_Details()

            For i As Integer = 0 To dtCurrent.Rows.Count - 1
                If Lang = CtlCommon.Lang.EN Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Fill Data Table" + i.ToString(), "scheduleGrid_AddEmp('" + dtCurrent.Rows(i)("GroupId").ToString() + "," + dtCurrent.Rows(i)("EmployeeNo").ToString() + "," + dtCurrent.Rows(i)("EmployeeName").ToString() + "," + dtCurrent.Rows(i)("GroupName").ToString() + "');", True)
                ElseIf Lang = CtlCommon.Lang.AR Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Fill Data Table" + i.ToString(), "scheduleGrid_AddEmp('" + dtCurrent.Rows(i)("GroupId").ToString() + "," + dtCurrent.Rows(i)("EmployeeNo").ToString() + "," + dtCurrent.Rows(i)("EmployeeArabicName").ToString() + "," + dtCurrent.Rows(i)("GroupArabicName").ToString() + "');", True)
                End If
            Next

            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Fill Data Table", "scheduleGrid_AddEmp('" + "1," + "109," + "Fadi Halboni" + "');", True)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Fill Data Table2", "scheduleGrid_AddEmp('" + "2," + "119," + "Fadi.H" + "');", True)
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        objEvents_Employees_Schedule = New Events_Employees_Schedule()
        Dim arrEmployeeNos() As String = hdnEmployeeNos.Value.Split(",")
        Dim arrGroupIds() As String = hdnGroupIDs.Value.Split(",")
        Dim arrShifts() As String = hdnShifts.Value.Split(",")
        Dim arrSelectedDays() As String = hdnSelectedDays.Value.Split(",")
        Dim intEmployeeID As Integer
        Dim dtEmployee As DataTable
        Dim errNo As Integer
        Dim CurrD As DateTime = CompareDate
        Dim isDeleted As Boolean = False


        For i As Integer = 1 To arrEmployeeNos.Length - 1
            CurrD = CompareDate
            objEvents_Employees_Schedule = New Events_Employees_Schedule()
            objEmployee = New Employee()

            objEmployee.EmployeeNo = arrEmployeeNos(i)
            objEmployee.UserId = SessionVariables.LoginUser.ID
            dtEmployee = objEmployee.GetEmpByEmpNo()
            intEmployeeID = dtEmployee.Rows(0)("EmployeeId")

            objEvents_Employees_Schedule.FK_EventId = EventID
            objEvents_Employees_Schedule.FK_EmployeeId = intEmployeeID
            objEvents_Employees_Schedule.Shift = arrShifts(i)

            While CurrD <= ToDate
                If CurrD.Day = CInt(arrSelectedDays(i)) Then
                    objEvents_Employees_Schedule.ScheduleDate = CurrD
                    Exit While
                End If

                CurrD = CurrD.AddDays(1)
            End While

            If PageAction = "Edit" Then
                If Not isDeleted Then
                    objEvents_Employees_Schedule.FK_EventId = EventID
                    objEvents_Employees_Schedule.DeleteByEventID()
                    isDeleted = True
                End If
            End If

            errNo += objEvents_Employees_Schedule.Add()
        Next

        If errNo = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ClearAll()
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If

    End Sub

#End Region

#Region "Methods"

    Sub ClearAll()

        hdnEmployeeNos.Value = String.Empty
        hdnGroupIDs.Value = String.Empty
        hdnShifts.Value = String.Empty
        hdnSelectedDays.Value = String.Empty
        hdnShiftACount.Value = 0
        hdnShiftBCount.Value = 0
        hdnShiftCCount.Value = 0
        PageAction = "Add"

        hdnSelectedEmployees.Value = String.Empty
        hdnSelectedShift.Value = String.Empty
        hdnSavedDays.Value = String.Empty

    End Sub

#End Region

End Class
