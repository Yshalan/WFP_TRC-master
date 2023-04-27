Imports System.Data
Imports TA.DashBoard
Imports SmartV.UTILITIES
Imports TA.Security


Partial Class Admin_DashBoard
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objversion As SmartV.Version.version

#End Region

#Region "Properties"

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
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
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            UserSecurityFilter1.IsEntityPostBack = False
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")

            ElseIf SessionVariables.CultureInfo = "ar-JO" Then

                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            PageHeader1.HeaderText = ResourceManager.GetString("DashBoard", CultureInfo)

            'Emp_DashBoard1.Visible = False
            rdpDate.SelectedDate = Date.Today
            rdpToDate.SelectedDate = Date.Today
            Dim dashid As Integer = Request.QueryString("dashid")
            Dim objDashBoard As New DashBoard
            Dim Headertext As String
            Dim dtCAChart As DataTable
            If objversion.HasMultiCompany Then
                UserSecurityFilter1.GetEntity()
            End If
            If UserSecurityFilter1.CompanyId > 0 Then
                objDashBoard.FK_CompanyId = UserSecurityFilter1.CompanyId
            Else
                objDashBoard.FK_CompanyId = Nothing
            End If
            If UserSecurityFilter1.EntityId > 0 Then
                objDashBoard.FK_EntityId = UserSecurityFilter1.EntityId
            Else
                objDashBoard.FK_EntityId = Nothing
            End If
            If rdpDate.SelectedDate.HasValue Then
                objDashBoard.FromDate = rdpDate.SelectedDate
            Else
                rdpDate.SelectedDate = Date.Today
            End If

            If rdpToDate.SelectedDate.HasValue Then
                objDashBoard.ToDate = rdpToDate.SelectedDate
            Else
                rdpToDate.SelectedDate = Date.Today
            End If
            objDashBoard.Lang = Lang
            If SessionVariables.LoginUser.UserStatus = 1 Then


                Select Case dashid
                    Case 1
                        dtCAChart = objDashBoard.GetAttendDash()

                        Headertext = ResourceManager.GetString("AttendDash", CultureInfo)

                        PageHeader1.HeaderText = Headertext
                        Emp_DashBoard1.fill_chart(dtCAChart, "descrip", "number", Headertext)
                    Case 2
                        dtCAChart = objDashBoard.GetAbsentDash
                        Headertext = ResourceManager.GetString("AbsentDash", CultureInfo)
                        PageHeader1.HeaderText = Headertext
                        Emp_DashBoard1.fill_chart(dtCAChart, "descrip", "number", Headertext)
                    Case 3
                        dtCAChart = objDashBoard.GetDelayDash
                        Headertext = ResourceManager.GetString("DelayDash", CultureInfo)
                        PageHeader1.HeaderText = Headertext
                        Emp_DashBoard1.fill_chart(dtCAChart, "descrip", "number", Headertext)
                    Case 4
                        dtCAChart = objDashBoard.GetEarlyoutDash
                        Headertext = ResourceManager.GetString("EarlyDash", CultureInfo)
                        PageHeader1.HeaderText = Headertext
                        Emp_DashBoard1.fill_chart(dtCAChart, "descrip", "number", Headertext)
                    Case 5
                        dtCAChart = objDashBoard.GetLeaveDash
                        Headertext = ResourceManager.GetString("LeavesDash", CultureInfo)
                        PageHeader1.HeaderText = Headertext
                        Emp_DashBoard1.fill_chart(dtCAChart, "descrip", "number", Headertext)
                    Case 6
                        dtCAChart = objDashBoard.GetSummaryDash
                        Headertext = ResourceManager.GetString("SummaryDash", CultureInfo)
                        PageHeader1.HeaderText = Headertext
                        Emp_DashBoard1.fill_chart(dtCAChart, "descrip", "number", Headertext)
                    Case 7
                        dtCAChart = objDashBoard.GetDailyTransactionsDash
                        Headertext = ResourceManager.GetString("DailyTransactions", CultureInfo)
                        PageHeader1.HeaderText = Headertext
                        Emp_DashBoard1.fill_chart(dtCAChart, "descrip", "number", Headertext)
                End Select
            End If
        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not pnlFilter.FindControl(row("AddBtnName")) Is Nothing Then
                        pnlFilter.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not pnlFilter.FindControl(row("DeleteBtnName")) Is Nothing Then
                        pnlFilter.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not pnlFilter.FindControl(row("EditBtnName")) Is Nothing Then
                        pnlFilter.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not pnlFilter.FindControl(row("PrintBtnName")) Is Nothing Then
                        pnlFilter.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next


    End Sub

    Protected Sub btnRetrive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRetrive.Click

        Dim dashid As Integer = Request.QueryString("dashid")
        Dim objDashBoard As New DashBoard
        If UserSecurityFilter1.CompanyId > 0 Then
            objDashBoard.FK_CompanyId = UserSecurityFilter1.CompanyId
        Else
            objDashBoard.FK_CompanyId = Nothing
        End If
        If UserSecurityFilter1.EntityId > 0 Then
            objDashBoard.FK_EntityId = UserSecurityFilter1.EntityId
        Else
            objDashBoard.FK_EntityId = Nothing
        End If
        If rdpDate.SelectedDate.HasValue Then
            objDashBoard.FromDate = rdpDate.SelectedDate
        Else
            rdpDate.SelectedDate = Date.Today
        End If

        If rdpToDate.SelectedDate.HasValue Then
            objDashBoard.ToDate = rdpToDate.SelectedDate
        Else
            rdpToDate.SelectedDate = Date.Today
        End If

        'If rdpToDate.SelectedDate.HasValue Then
        '    If Not StartEndDateComparison(rdpDate.SelectedDate, _
        '                                    rdpToDate.SelectedDate) Then
        '        Return
        '    End If
        'End If

        objDashBoard.Lang = Lang

        Select Case dashid
            Case 1
                Dim dtCAChart As DataTable = objDashBoard.GetAttendDash()
                Emp_DashBoard1.fill_chart(dtCAChart, "descrip", "number", ResourceManager.GetString("AttendDash", CultureInfo))
                If dtCAChart.Rows.Count = 0 Then
                    Emp_DashBoard1.Visible = False
                    lbldash.Visible = True
                Else
                    Emp_DashBoard1.Visible = True
                    lbldash.Visible = False
                End If
            Case 2
                Dim dtCAChart As DataTable = objDashBoard.GetAbsentDash()
                Emp_DashBoard1.fill_chart(dtCAChart, "descrip", "number", ResourceManager.GetString("AbsentDash", CultureInfo))
                If dtCAChart.Rows.Count = 0 Then
                    Emp_DashBoard1.Visible = False
                    lbldash.Visible = True
                Else
                    Emp_DashBoard1.Visible = True
                    lbldash.Visible = False
                End If
            Case 3
                Dim dtCAChart As DataTable = objDashBoard.GetDelayDash()
                Emp_DashBoard1.fill_chart(dtCAChart, "descrip", "number", ResourceManager.GetString("DelayDash", CultureInfo))
                If dtCAChart.Rows.Count = 0 Then
                    Emp_DashBoard1.Visible = False
                    lbldash.Visible = True
                Else
                    Emp_DashBoard1.Visible = True
                    lbldash.Visible = False
                End If
            Case 4
                Dim dtCAChart As DataTable = objDashBoard.GetEarlyoutDash()
                Emp_DashBoard1.fill_chart(dtCAChart, "descrip", "number", ResourceManager.GetString("EarlyDash", CultureInfo))
                If dtCAChart.Rows.Count = 0 Then
                    Emp_DashBoard1.Visible = False
                    lbldash.Visible = True
                Else
                    Emp_DashBoard1.Visible = True
                    lbldash.Visible = False
                End If
            Case 5
                Dim dtCAChart As DataTable = objDashBoard.GetLeaveDash()
                Emp_DashBoard1.fill_chart(dtCAChart, "descrip", "number", ResourceManager.GetString("LeavesDash", CultureInfo))
                If dtCAChart.Rows.Count = 0 Then
                    Emp_DashBoard1.Visible = False
                    lbldash.Visible = True
                Else
                    Emp_DashBoard1.Visible = True
                    lbldash.Visible = False
                End If
            Case 6
                Dim dtCAChart As DataTable = objDashBoard.GetSummaryDash()
                Emp_DashBoard1.fill_chart(dtCAChart, "descrip", "number", ResourceManager.GetString("SummaryDash", CultureInfo))
                If dtCAChart.Rows.Count = 0 Then
                    Emp_DashBoard1.Visible = False
                    lbldash.Visible = True
                Else
                    Emp_DashBoard1.Visible = True
                    lbldash.Visible = False
                End If
            Case 7
                Dim dtCAChart As DataTable = objDashBoard.GetDailyTransactionsDash()

                If Not dtCAChart Is Nothing Then
                    For Each row As DataRow In dtCAChart.Rows
                        row("descrip") = row("descrip") + ": " + row("number").ToString()
                    Next
                End If

                Emp_DashBoard1.fill_chart(dtCAChart, "descrip", "number", ResourceManager.GetString("DailyTransactions", CultureInfo))
                If dtCAChart.Rows.Count = 0 Then
                    Emp_DashBoard1.Visible = False
                    lbldash.Visible = True
                Else
                    Emp_DashBoard1.Visible = True
                    lbldash.Visible = False
                End If

        End Select

        'Emp_DashBoard1.Visible = True
    End Sub

#End Region

#Region "Methods"

    Private Function StartEndDateComparison(ByVal dateStartdate As DateTime, _
                                           ByVal dateEndDate As DateTime) As Boolean
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If IsDate(rdpDate.SelectedDate) And IsDate(rdpToDate.SelectedDate) Then
            ' Get the start and end date from the calenders
            dateStartdate = New DateTime(rdpDate.SelectedDate.Value.Year, _
                                         rdpDate.SelectedDate.Value.Month, _
                                         rdpDate.SelectedDate.Value.Day)
            dateEndDate = New DateTime(rdpToDate.SelectedDate.Value.Year, _
                                       rdpToDate.SelectedDate.Value.Month, _
                                       rdpToDate.SelectedDate.Value.Day)
            Dim result As Integer = DateTime.Compare(dateEndDate, dateStartdate)
            If result < 0 Then
                ' show message and set focus on end date picker
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DateRangeEarly", CultureInfo))

                rdpToDate.Focus()
                Return False
            ElseIf result = 0 Then
                ' Show message and focus on the end date picker
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EndEqualStart", CultureInfo))

                rdpToDate.Focus()
                Return False
            Else
                ' Do nothing
                Return True
            End If
        End If
    End Function

#End Region

End Class
