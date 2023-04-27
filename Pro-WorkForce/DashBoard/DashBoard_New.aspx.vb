Imports SmartV.UTILITIES
Imports System.Data
Imports TA.DashBoard
Imports TA.Admin

Partial Class DashBoard_DashBoard_New
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objversion As SmartV.Version.version
    Dim DrillDownDatalvl1 As String = ""
    Dim Str As String
    Dim STRsPLIT() As String
    Dim StrDrill As String
    Dim StrDrillsPLIT() As String
    Dim data As String = "" '"{name:   'الحضور',  y: 16.11}, {name:   'عدم الحضور',y: 83.89},"
    Dim drills As String = ""
    Private objOrgLevel As New OrgLevel
    Dim dtOrgLevel As New DataTable
    Private objOrgEntity As OrgEntity
    Public chartLang As String

#End Region

#Region "Properties"
    Public Property SubTitleText() As String
        Get
            Return ViewState("SubTitleText")
        End Get
        Set(ByVal value As String)
            ViewState("SubTitleText") = value
        End Set
    End Property

    Public Property ChartTitleText() As String
        Get
            Return ViewState("ChartTitleText")
        End Get
        Set(ByVal value As String)
            ViewState("ChartTitleText") = value
        End Set
    End Property

    Public Property dt() As DataTable
        Get
            Return ViewState("dt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dt") = value
        End Set
    End Property

    Public Property dtDrillDown() As DataTable
        Get
            Return ViewState("dtDrillDown")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtDrillDown") = value
        End Set
    End Property

    Public Property dashid() As Integer
        Get
            Return ViewState("dashid")
        End Get
        Set(ByVal value As Integer)
            ViewState("dashid") = value
        End Set
    End Property

    Public Property PageHeader() As String
        Get
            Return ViewState("PageHeader")
        End Get
        Set(ByVal value As String)
            ViewState("PageHeader") = value
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
                chartLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
                chartLang = "en"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            dashid = Request.QueryString("dashid")
            If Not Page.IsPostBack Then
                UserSecurityFilter1.IsEntityPostBack = False
                CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
                If (SessionVariables.CultureInfo Is Nothing) Then
                    Response.Redirect("~/default/Login.aspx")

                ElseIf SessionVariables.CultureInfo = "ar-JO" Then

                    Lang = CtlCommon.Lang.AR
                    chartLang = "ar"
                Else
                    Lang = CtlCommon.Lang.EN
                    chartLang = "en"
                End If

                rdpFromDate.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
                Dim dd As New Date
                dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

                rdpToDate.SelectedDate = dd
            End If

            ddlChartType.FindItemIndexByValue("Faces")

            FillHighCharts(dashid)
            objOrgLevel.FK_CompanyId = UserSecurityFilter1.CompanyId

            dtOrgLevel = objOrgLevel.GetAllByComapany()
            If ddlChartType.SelectedValue = "Faces" Then
                DivFaces.Visible = True
                divBorder.Visible = True
            Else
                fillCahrt()
                DivFaces.Visible = False
                divBorder.Visible = False
            End If
            If dashid = 8 Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    lblHappy.Text = "سعيد"
                    lblNeutral.Text = "عادي"
                    lblUnHappy.Text = "غير سعيد"
                End If
            Else
                Try
                    ddlChartType.Items.Remove(ddlChartType.FindItemIndexByValue("Faces"))
                Catch ex As Exception

                End Try


                'ddlChartType.SelectedValue = "pie"
                DivFaces.Visible = False
                divBorder.Visible = False
                'fillCahrt()

            End If
            If Not Page.IsPostBack Then
                fillCahrt()
            End If

            SubTitleText = FillChart_Info(UserSecurityFilter1.EntityId)
            JSONChartTitleText.Text = "<script charset='utf-8'>var Var_ChartTitleText ='" & ChartTitleText & "' ; </Script>"
            JSONChartSubTitleText.Text = "<script type='text/javascript' charset='utf-8'>var Var_ChartSubtitleText ='" & SubTitleText & "' ; </Script>"
            JSONChartType.Text = "<script type='text/javascript' charset='utf-8'>var Var_ChartType ='" & ddlChartType.SelectedValue & "' ; </Script>"
            JSONSeries.Text = "<script type='text/javascript' charset='utf-8'>var Var_Series = [{name:   'Overview',colorByPoint: true,data: [" & data & "]  }]; </Script>"

            If dashid = 6 Then
                JSONdrilldown.Text = "<script type='text/javascript' charset='utf-8'>var Var_drilldown ={ series: [] }; </Script>"
            Else
                JSONdrilldown.Text = "<script type='text/javascript' charset='utf-8'>var Var_drilldown ={}; </Script>"
            End If
            FillPageHeader(dashid)
            pageheader1.HeaderText = PageHeader

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnRetrive_Click(sender As Object, e As System.EventArgs) Handles btnRetrive.Click
        Dim dashid As Integer = Request.QueryString("dashid")
        FillHighCharts(dashid)

    End Sub

    Protected Sub ddlChartType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlChartType.SelectedIndexChanged
        If ddlChartType.SelectedValue = "Faces" Then
            DivFaces.Visible = True
            divBorder.Visible = True
        Else
            DivFaces.Visible = False
            divBorder.Visible = False
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub fillCahrt()
        If Not (dt Is Nothing) Then
            If Not IsDBNull(dt.Rows(0)("descrip")) Then



                For i As Integer = 0 To dt.Rows.Count - 1

                    data += "{name:   '"
                    Str = dt.Rows(i)("descrip")
                    STRsPLIT = Str.Split(":")
                    data += STRsPLIT(0)
                    data += "',  y: "
                    If dashid = 7 Then
                        data += dt.Rows(i)("number").ToString()
                    Else
                        data += STRsPLIT(1).Replace("%", "")
                    End If

                    If dashid = 6 Then
                        data += ",  drilldown: '"
                        data += LowercaseFirstLetter(STRsPLIT(0)).Replace(" ", "") + "'"
                        DrillDownCharts(LowercaseFirstLetter(STRsPLIT(0)).Replace(" ", ""))
                        FillSubTitleText(LowercaseFirstLetter(STRsPLIT(0)).Replace(" ", ""))


                        drills += DynamicfillDrillDown(0)
                        DrillDownDatalvl1 = ""
                        'For lvl As Integer = 0 To dtOrgLevel.Rows.Count - 1
                        '    drills += DynamicfillDrillDown(dtOrgLevel.Rows(lvl)("LevelId"))
                        '    DrillDownDatalvl1 = ""
                        'Next

                    End If
                    data += "},"
                Next

                ''''''''''''''
                If dashid = 6 Then
                    If Not dtDrillDown Is Nothing Then
                        If dtDrillDown.Rows.Count > 0 Then
                            Dim distinctDT As DataTable = dtDrillDown.DefaultView.ToTable(True, "FK_ParentId")
                            distinctDT = distinctDT.Select("FK_ParentId <> 0").CopyToDataTable()

                            For ParentId As Integer = 0 To distinctDT.Rows.Count - 1

                                drills += DynamicfillDrillDown(distinctDT.Rows(ParentId)("FK_ParentId"))
                                DrillDownDatalvl1 = ""
                            Next
                        End If
                    End If
                End If
            End If
        End If
        ''''''''''''''
    End Sub

    Private Function DynamicfillDrillDown(ByVal FK_ParentId As Integer) As String
        Try

            Dim dtdrillByLevel As New DataTable
            If Not dtDrillDown Is Nothing And dtDrillDown.Rows.Count > 0 Then


                If FK_ParentId = 0 Then
                    DrillDownDatalvl1 += "{id: '"
                    DrillDownDatalvl1 += LowercaseFirstLetter(STRsPLIT(0)).Replace(" ", "") 'fill vio (absent,...) as id
                    DrillDownDatalvl1 += "',name: '"
                    DrillDownDatalvl1 += STRsPLIT(0)
                    DrillDownDatalvl1 += "',data: ["

                Else
                    DrillDownDatalvl1 += "{id: '"
                    Dim row As DataRow = dtDrillDown.Select("FK_EntityId =" & FK_ParentId).FirstOrDefault()
                    Dim EntityName As String = ""

                    If Not row Is Nothing Then
                        EntityName = row.Item("EntityName")
                    End If



                    DrillDownDatalvl1 += LowercaseFirstLetter(EntityName).Replace(" ", "") 'fill vio (absent,...) as id
                    DrillDownDatalvl1 += "',name: '"
                    DrillDownDatalvl1 += EntityName
                    DrillDownDatalvl1 += "',data: ["

                End If





                dtdrillByLevel = dtDrillDown.Select("FK_ParentId =" & FK_ParentId).CopyToDataTable()
                For z As Integer = 0 To dtdrillByLevel.Rows.Count - 1
                    DrillDownDatalvl1 += "{ name :'"
                    DrillDownDatalvl1 += dtdrillByLevel.Rows(z)("EntityName") + "'," 'Fill Name
                    DrillDownDatalvl1 += "y:"
                    DrillDownDatalvl1 += dtdrillByLevel.Rows(z)("AbsetPerct").ToString()

                    DrillDownDatalvl1 += ", drilldown: '"
                    DrillDownDatalvl1 += LowercaseFirstLetter(dtdrillByLevel.Rows(z)("EntityName").Replace(" ", "")) + "'"
                    DrillDownDatalvl1 += "},"
                Next
                DrillDownDatalvl1 += "]},"
            End If
            Return DrillDownDatalvl1
        Catch ex As Exception
            '{id: 'absent',name: 'Absent',data: [{ name :'InformationTechnology',y:25.51, drilldown: 'informationTechnology'},{ name :'finance',y:10.28, drilldown: 'finance'},]},
            '{id: 'absent',name: 'Absent',data: [{ name :'QualityAssurance',y:11.25, drilldown: 'qualityAssurance'},{ name :'Development',y:10.88, drilldown: 'development'},]},
            '{id: 'absent',name: 'Absent',data: [{ name :'Enterprise',y:42.08, drilldown: 'enterprise'},]},
        End Try
    End Function

    Private Sub DrillDownCharts(ByVal DrillDownid As String)
        Dim objDashBoard As New DashBoard
        dtDrillDown = New DataTable

        objDashBoard.Lang = Lang


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
        If rdpFromDate.SelectedDate.HasValue Then
            objDashBoard.FromDate = rdpFromDate.SelectedDate
        Else
            rdpFromDate.SelectedDate = Date.Today
        End If

        If rdpToDate.SelectedDate.HasValue Then
            objDashBoard.ToDate = rdpToDate.SelectedDate
        Else
            rdpToDate.SelectedDate = Date.Today
        End If
        objDashBoard.Lang = Lang

        'dtDrillDown = objDashBoard.RetreivDrillDownData()
        If SessionVariables.LoginUser.UserStatus = 1 Then
            Select Case DrillDownid
                Case "noIssue"
                    objDashBoard.DrillDownType = 0
                    SubTitleText = ResourceManager.GetString("AttendDash", CultureInfo)
                Case "absent"
                    objDashBoard.DrillDownType = "A"
                    SubTitleText = ResourceManager.GetString("AbsentDash", CultureInfo)
                Case "delay"
                    objDashBoard.DrillDownType = "delay"
                    SubTitleText = ResourceManager.GetString("DelayDash", CultureInfo)

                Case "earlyOut"
                    objDashBoard.DrillDownType = "earlyOut"
                    SubTitleText = ResourceManager.GetString("EarlyDash", CultureInfo)
                Case "leave"
                    objDashBoard.DrillDownType = "L"
                    SubTitleText = ResourceManager.GetString("LeavesDash", CultureInfo)
                Case "holiday"
                    objDashBoard.DrillDownType = "H"
                    SubTitleText = "Holiday DashBoard"
                    'Case "noIssue"
                    '    objDashBoard.DrillDownType = 0
                    '    SubTitleText = ResourceManager.GetString("SummaryDash", CultureInfo)

            End Select
            dtDrillDown = objDashBoard.RetreivDrillDownData()
        End If

    End Sub

    Private Sub FillSubTitleText(ByVal SubTitleText As String)
        Select Case SubTitleText
            Case "attend"
                SubTitleText = ResourceManager.GetString("AttendDash", CultureInfo)
            Case "absent"
                SubTitleText = ResourceManager.GetString("AbsentDash", CultureInfo)
            Case "delay"
                SubTitleText = ResourceManager.GetString("DelayDash", CultureInfo)
            Case "earlyOut"
                SubTitleText = ResourceManager.GetString("EarlyDash", CultureInfo)
            Case "leave"
                SubTitleText = ResourceManager.GetString("LeavesDash", CultureInfo)
            Case "holiday"
                SubTitleText = "Holiday DashBoard"
                'Case "noIssue"
                '    objDashBoard.DrillDownType = 0
                '    SubTitleText = ResourceManager.GetString("SummaryDash", CultureInfo)

        End Select
    End Sub

    Private Sub FillHighCharts(ByVal dashid As Integer)

        Dim objDashBoard As New DashBoard
        Dim dtCAChart As New DataTable

        objDashBoard.Lang = Lang


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
        If rdpFromDate.SelectedDate.HasValue Then
            objDashBoard.FromDate = rdpFromDate.SelectedDate
        Else
            rdpFromDate.SelectedDate = Date.Today
        End If

        If rdpToDate.SelectedDate.HasValue Then
            objDashBoard.ToDate = rdpToDate.SelectedDate
        Else
            rdpToDate.SelectedDate = Date.Today
        End If
        objDashBoard.Lang = Lang
        If (SessionVariables.LoginUser.UserStatus = 1 Or SessionVariables.LoginUser.UserStatus = 3) And (UserSecurityFilter1.CompanyId <> 0) Then
            Select Case dashid
                Case 1
                    dtCAChart = objDashBoard.GetAttendDash()
                    If Not dtCAChart Is Nothing Then
                        dt = dtCAChart
                    End If
                    ChartTitleText = ResourceManager.GetString("AttendDash", CultureInfo)
                Case 2
                    dtCAChart = objDashBoard.GetAbsentDash()
                    If Not dtCAChart Is Nothing Then
                        dt = dtCAChart
                    End If
                    ChartTitleText = ResourceManager.GetString("AbsentDash", CultureInfo)
                Case 3
                    dtCAChart = objDashBoard.GetDelayDash()
                    If Not dtCAChart Is Nothing Then
                        dt = dtCAChart
                    End If
                    ChartTitleText = ResourceManager.GetString("DelayDash", CultureInfo)
                Case 4
                    dtCAChart = objDashBoard.GetEarlyoutDash()
                    If Not dtCAChart Is Nothing Then
                        dt = dtCAChart
                    End If
                    ChartTitleText = ResourceManager.GetString("EarlyDash", CultureInfo)
                Case 5
                    dtCAChart = objDashBoard.GetLeaveDash()
                    If Not dtCAChart Is Nothing Then
                        dt = dtCAChart
                    End If
                    ChartTitleText = ResourceManager.GetString("LeavesDash", CultureInfo)
                Case 6
                    dtCAChart = objDashBoard.GetSummaryDash()
                    If Not dtCAChart Is Nothing Then
                        dt = dtCAChart
                    End If
                    ChartTitleText = ResourceManager.GetString("SummaryDash", CultureInfo)
                Case 7
                    dtCAChart = objDashBoard.GetDailyTransactionsDash()
                    If Not dtCAChart Is Nothing Then
                        dt = dtCAChart
                    End If
                    ChartTitleText = ResourceManager.GetString("DailyTransactions", CultureInfo)
                Case 8
                    dtCAChart = objDashBoard.GetFeedbackDash()
                    If Not dtCAChart Is Nothing Then
                        dt = dtCAChart
                        If dt.Rows.Count > 0 Then
                            If Not dt Is Nothing Then
                                Dim descrip As String = ""
                                Dim Count As String = ""
                                If ddlChartType.SelectedValue = "Faces" Then
                                    For i As Integer = 0 To dt.Rows.Count - 1
                                        If Not dt.Rows(i)("descrip") Is DBNull.Value Then
                                            descrip = dt.Rows(i)("descrip")
                                            Count = dt.Rows(i)("number")
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                If descrip.Contains("سعيد") And Not descrip.Contains("غير") Then
                                                    descrip = descrip.Replace("سعيد:", "")
                                                    lblHappyValue.Text = Count & " (" & descrip & ")"
                                                End If
                                                If descrip.Contains("عادي") Then
                                                    descrip = descrip.Replace("عادي:", "")
                                                    lblNeutralValue.Text = Count & " (" & descrip & ")"
                                                End If
                                                If descrip.Contains("غير سعيد") Then
                                                    descrip = descrip.Replace("غير سعيد:", "")
                                                    lblUnHappyValue.Text = Count & " (" & descrip & ")"
                                                End If

                                            Else
                                                If descrip.Contains("Happy") And Not descrip.Contains("Un") Then
                                                    descrip = descrip.Replace("Happy:", "")
                                                    lblHappyValue.Text = Count & " (" & descrip & ")"
                                                End If
                                                If descrip.Contains("Neutral") Then
                                                    descrip = descrip.Replace("Neutral:", "")
                                                    lblNeutralValue.Text = Count & " (" & descrip & ")"
                                                End If
                                                If descrip.Contains("UnHappy") Then
                                                    descrip = descrip.Replace("UnHappy:", "")
                                                    lblUnHappyValue.Text = Count & " (" & descrip & ")"
                                                End If
                                            End If

                                        End If

                                    Next


                                End If
                            End If
                        End If
                        ChartTitleText = ResourceManager.GetString("DeviceFeedbackDashboard", CultureInfo)
                    End If
            End Select
        End If

    End Sub

    Function GetAfterString(value As String, a As String) As String
        ' Get index of argument and return substring after its position.
        Dim posA As Integer = value.LastIndexOf(a)
        If posA = -1 Then
            Return ""
        End If
        Dim adjustedPosA As Integer = posA + a.Length
        If adjustedPosA >= value.Length Then
            Return ""
        End If
        Return value.Substring(adjustedPosA)
    End Function

    Function LowercaseFirstLetter(ByVal val As String) As String
        ' Test for nothing or empty.
        If String.IsNullOrEmpty(val) Then
            Return val
        End If

        ' Convert to character array.
        Dim array() As Char = val.ToCharArray

        ' Uppercase first character.
        array(0) = Char.ToLower(array(0))

        ' Return new string.
        Return New String(array)
    End Function

    Private Function FillChart_Info(ByVal FK_EntityId As Integer) As String
        Dim text As String
        objOrgEntity = New OrgEntity
        objOrgEntity.EntityId = FK_EntityId
        objOrgEntity.GetByPK()

        If Lang = CtlCommon.Lang.AR Then
            text = objOrgEntity.EntityArabicName & " (" & rdpFromDate.SelectedDate & "-" & rdpToDate.SelectedDate & ")"
        Else
            text = objOrgEntity.EntityName & " (" & rdpFromDate.SelectedDate & "-" & rdpToDate.SelectedDate & ")"
        End If

        Return text
    End Function

    Private Sub FillPageHeader(ByVal dashid As Integer)
        Select Case dashid
            Case 1
                PageHeader = ResourceManager.GetString("AttendDash", CultureInfo)
            Case 2
                PageHeader = ResourceManager.GetString("AbsentDash", CultureInfo)
            Case 3
                PageHeader = ResourceManager.GetString("DelayDash", CultureInfo)
            Case 4
                PageHeader = ResourceManager.GetString("EarlyDash", CultureInfo)
            Case 5
                PageHeader = ResourceManager.GetString("LeavesDash", CultureInfo)
            Case 6
                PageHeader = ResourceManager.GetString("SummaryDash", CultureInfo)
            Case 7
                PageHeader = ResourceManager.GetString("DailyTransactions", CultureInfo)
            Case 8
                PageHeader = ResourceManager.GetString("DeviceFeedbackDashboard", CultureInfo)
        End Select
    End Sub

#End Region

End Class
