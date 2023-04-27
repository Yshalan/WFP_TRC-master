Imports TA.Employees
Imports System.Data
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.Lookup
Imports TA.Definitions
Imports TA.SelfServices
Imports System.IO
Imports TA.Security
Imports SmartV.UTILITIES.ProjectCommon
Imports Telerik.Web.UI.Calendar
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports TA.Admin
Imports TA.DailyTasks
Imports TA_Announcements

Partial Class DailyTasks_HR_EmployeeViolations
    Inherits System.Web.UI.Page

#Region "Class Variables"

    'Private objEmp_LeavesRequest As Emp_LeavesRequest
    'Private objEmp_PermissionsRequest As Emp_PermissionsRequest
    'Private objProjectCommon As New ProjectCommon
    'Private objRequestStatus As New RequestStatus
    'Private objLeavesTypes As LeavesTypes
    Private Lang As CtlCommon.Lang
    'Private objEmp_Leaves As Emp_Leaves
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Shared SortExepression As String
    Shared dtCurrent As DataTable
    Private objEmployeeViolations As EmployeeViolations
    'Private objEmp_Permissions As Emp_Permissions
    'Private objPermissionsTypes As PermissionsTypes
    'Private objEmp_WorkSchedule As Emp_WorkSchedule
    'Private objAppSettings As APP_Settings
    'Private objRECALC_REQUEST As RECALC_REQUEST
    'Private objEmployee_Manager As Employee_Manager

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

    Public Property ViolationDate() As DateTime
        Get
            Return ViewState("ViolationDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("ViolationDate") = value
        End Set
    End Property



#End Region

#Region "PageEvents"

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

    Private Sub getPageTitle()
        Dim objSysForms As New SYSForms
        Dim dt As New DataTable
        dt = objSysForms.GetByPK(964)
        If SessionVariables.CultureInfo = "ar-JO" Then
            PageHeader1.HeaderText = dt.Rows(0)("Desc_Ar")
        Else
            PageHeader1.HeaderText = dt.Rows(0)("Desc_En")

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Dim browserType As String = Request.Browser.Type
            Dim browserVersion As String = Request.Browser.Version

            If browserType.Contains("IE") Then

            End If
            mvEmpViolations.SetActiveView(viewEmpViolationsList)
            getPageTitle()
            'PageHeader1.HeaderText = ResourceManager.GetString("EmployeeViolations", CultureInfo)

            dteFromDate.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            dteToDate.SelectedDate = dd

            FillGridView()

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)


        End If

    End Sub

    Protected Sub dgrdEmpViolations_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdEmpViolations.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item
            If Lang = CtlCommon.Lang.AR Then
                'item("EntityName").Text = DirectCast(item.FindControl("hdnEntityArabicName"), HiddenField).Value
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeArabicName"), HiddenField).Value
            End If


            Dim strContactDuration As New StringBuilder
            Dim strContactType As New StringBuilder

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("M_DATE").ToString())) And (Not item.GetDataKeyValue("M_DATE").ToString() = "")) Then
                Dim violationDate As DateTime = item.GetDataKeyValue("M_DATE")
                item("M_DATE").Text = violationDate.ToShortDateString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("Status").ToString())) And (Not item.GetDataKeyValue("Status").ToString() = "")) Then
                If item.GetDataKeyValue("Status").ToString() = "A" Then
                    item("Type").Text = ResourceManager.GetString("Absent", CultureInfo)
                    item.FindControl("lnbSubmitPermission").Visible = False
                    item.FindControl("lnbSubmitLeave").Visible = True
                End If
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("Description").ToString())) And (Not item.GetDataKeyValue("Description").ToString() = "")) Then
                If item.GetDataKeyValue("Description").ToString().Contains("Delay") Then
                    Dim strDelay() As String = item.GetDataKeyValue("Description").ToString().Split(":")
                    item("Type").Text = ResourceManager.GetString("Delay", CultureInfo)
                    item.FindControl("lnbSubmitPermission").Visible = True
                    item.FindControl("lnbSubmitLeave").Visible = False

                    item("Delay").Text = strDelay(1).ToString() + ":" + strDelay(2).ToString()
                    item("Duration").Text = item.GetDataKeyValue("Delay").ToString()

                    strContactType.AppendLine(item("Type").Text & "<br/>")
                    strContactDuration.AppendLine(item.GetDataKeyValue("Duration").ToString() & "<br/>")
                End If
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("Description").ToString())) And (Not item.GetDataKeyValue("Description").ToString() = "")) Then
                If item.GetDataKeyValue("Description").ToString().Contains("Early Out") Then
                    Dim strEarlyOut() As String = item.GetDataKeyValue("Description").ToString().Split(":")
                    item("Type").Text = ResourceManager.GetString("EarlyOut", CultureInfo)
                    item.FindControl("lnbSubmitPermission").Visible = True
                    item.FindControl("lnbSubmitLeave").Visible = False
                    item("EarlyOut").Text = strEarlyOut(1).ToString() + ":" + strEarlyOut(2).ToString()
                    item("Duration").Text = item("EarlyOut").Text
                    If Not String.IsNullOrEmpty(strContactType.ToString()) AndAlso Not String.IsNullOrEmpty(strContactDuration.ToString()) Then
                        strContactType.AppendLine(item("Type").Text)
                        strContactDuration.AppendLine(item.GetDataKeyValue("Duration").ToString())

                        item("Type").Text = strContactType.ToString()
                        item("Duration").Text = strContactDuration.ToString()

                    End If
                End If
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("Description").ToString())) And (Not item.GetDataKeyValue("Description").ToString() = "")) Then
                If item.GetDataKeyValue("Description").ToString().Contains("Out Time") Then
                    Dim strOutTime() As String = item.GetDataKeyValue("Description").ToString().Split(":")
                    item("Type").Text = ResourceManager.GetString("OutTime", CultureInfo)
                    item.FindControl("lnbSubmitPermission").Visible = True
                    item.FindControl("lnbSubmitLeave").Visible = False

                    item("OutTime").Text = strOutTime(1).ToString() + ":" + strOutTime(2).ToString()
                    item("Duration").Text = item.GetDataKeyValue("OutTime").ToString()

                    If Not String.IsNullOrEmpty(strContactType.ToString()) AndAlso Not String.IsNullOrEmpty(strContactDuration.ToString()) Then
                        strContactType.AppendLine(item("Type").Text)
                        strContactDuration.AppendLine(item.GetDataKeyValue("Duration").ToString())

                        item("Type").Text = strContactType.ToString()
                        item("Duration").Text = strContactDuration.ToString()

                    End If
                End If
            End If


        End If



        For Each item As GridDataItem In dgrdEmpViolations.MasterTableView.Items


            Dim ViolationType As TableCell = item("Type")
            If item.GetDataKeyValue("Description").ToString().Contains("Delay") Then
                ViolationType.Style.Item(HtmlTextWriterStyle.Color) = "#000080"
            ElseIf item.GetDataKeyValue("Description").ToString().Contains("Early Out") Then
                ViolationType.Style.Item(HtmlTextWriterStyle.Color) = "#008000"
            ElseIf item.GetDataKeyValue("Status").ToString() = "A" Then
                ViolationType.Style.Item(HtmlTextWriterStyle.Color) = "#FF0000"
            End If

            item("Type").HorizontalAlign = HorizontalAlign.Center
            item("M_DATE").HorizontalAlign = HorizontalAlign.Center
            item("EmployeeNo").HorizontalAlign = HorizontalAlign.Center
            item("PermStart").HorizontalAlign = HorizontalAlign.Center
            item("PermEnd").HorizontalAlign = HorizontalAlign.Center
            item("Duration").HorizontalAlign = HorizontalAlign.Center




        Next

    End Sub

    Protected Sub dgrdEmpViolations_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmpViolations.NeedDataSource

        objEmployeeViolations = New EmployeeViolations
        With objEmployeeViolations
            '.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FromDate = dteFromDate.SelectedDate
            .ToDate = dteToDate.SelectedDate
            dtCurrent = .GetHR_EmployeeViolations()
        End With

        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdEmpViolations.DataSource = dv

    End Sub

    Protected Sub btnRetrieve_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRetrieve.Click
        FillGridView()
    End Sub

    Protected Sub lnbSubmitPermission_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim FromTime As DateTime
        Dim ToTime As DateTime
        EmployeeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("EmployeeId").ToString())
        ViolationDate = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("M_DATE").ToString())
        FromTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermStart").ToString())
        ToTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("PermEnd").ToString())

        Response.Redirect("../employee/Permissions.aspx?EmployeeId=" + EmployeeId.ToString() + "&ViolationDate=" + ViolationDate + "&FromTime=" + FromTime + "&ToTime=" + ToTime)

    End Sub

    Protected Sub lnbSubmitLeave_Click(ByVal sender As Object, ByVal e As EventArgs)

        EmployeeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("EmployeeId").ToString())
        ViolationDate = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("M_DATE").ToString())

        Response.Redirect("../employee/EmpLeave_New2.aspx?EmployeeId=" + EmployeeId.ToString() + "&ViolationDate=" + ViolationDate)
    End Sub




#End Region

#Region "Page Methods"

    Private Sub FillGridView()

        objEmployeeViolations = New EmployeeViolations
        With objEmployeeViolations
            '.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FromDate = dteFromDate.DbSelectedDate
            .ToDate = dteToDate.DbSelectedDate
            If EmployeeFilter1.EmployeeId <> 0 Then
                .FK_EmployeeId = EmployeeFilter1.EmployeeId
            Else
                .FK_EmployeeId = Nothing
            End If

            If Not EmployeeFilter1.CompanyId = 0 Then
                .CompanyId = EmployeeFilter1.CompanyId
            Else
                .CompanyId = Nothing
            End If
            If EmployeeFilter1.FilterType = "C" Then
                If Not EmployeeFilter1.EntityId = 0 Then
                    .EntityId = EmployeeFilter1.EntityId
                Else
                    .EntityId = Nothing
                End If
            ElseIf EmployeeFilter1.FilterType = "W" Then
                If Not EmployeeFilter1.EntityId = 0 Then
                    .WorkLocationId = EmployeeFilter1.EntityId
                Else
                    .WorkLocationId = Nothing
                End If
            ElseIf EmployeeFilter1.FilterType = "L" Then
                If Not EmployeeFilter1.EntityId = 0 Then
                    .LogicalGroupId = EmployeeFilter1.EntityId
                Else
                    .LogicalGroupId = Nothing
                End If
            End If



            dtCurrent = .GetHR_EmployeeViolations()
        End With

        For Each item As DataRow In dtCurrent.Rows
            Dim type As String = String.Empty
            Dim delay As String = String.Empty
            Dim OutTime As String = String.Empty
            Dim earlyOut As String = String.Empty
            Dim strStatus As String = String.Empty
            Dim intStatus As Integer = 0

            If (Not IsDBNull(item("Status"))) AndAlso (item("Status") = "A") Then
                type = ResourceManager.GetString("Absent", CultureInfo)
            ElseIf item("Description").Contains("Delay") Then
                type = ResourceManager.GetString("Delay", CultureInfo)
                Dim strDelay() As String = item("Description").Split(":")
                delay = strDelay(1).ToString() + ":" + strDelay(2).ToString()
            ElseIf item("Description").Contains("Early Out") Then
                type = ResourceManager.GetString("EarlyOut", CultureInfo)
                Dim strEarlyOut() As String = item("Description").Split(":")
                earlyOut = strEarlyOut(1).ToString() + ":" + strEarlyOut(2).ToString()
            ElseIf item("Description").Contains("Out Time") Then
                type = ResourceManager.GetString("OutTime", CultureInfo)
                Dim strOutTime() As String = item("Description").Split(":")
                OutTime = strOutTime(1).ToString() + ":" + strOutTime(2).ToString()
            End If
            '' ismail ---- to check if needed 
            'If IsDBNull(item("Status")) Then
            '    PermissionIsExist(item("M_DATE"), type, item("IN_TIME"), delay, earlyOut, strStatus)
            '    objAppSettings = New APP_Settings()
            '    With objAppSettings
            '        .GetByPK()
            '        If Not String.IsNullOrEmpty(strStatus) Then

            '            Select Case .LeaveApproval
            '                Case 1
            '                    intStatus = 2
            '                Case 2
            '                    intStatus = 3
            '                Case 3
            '                    intStatus = 3
            '                Case 4
            '                    intStatus = 6
            '                Case Else

            '            End Select

            '            If Convert.ToInt32(strStatus) = intStatus Then
            '                item.Delete()
            '            End If
            '        End If
            '    End With
            'End If
        Next




        dgrdEmpViolations.DataSource = dtCurrent
        dgrdEmpViolations.DataBind()

    End Sub

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmpViolations.Skin))
    End Function

    Protected Sub dgrdEmpViolations_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdEmpViolations.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

#End Region

End Class
