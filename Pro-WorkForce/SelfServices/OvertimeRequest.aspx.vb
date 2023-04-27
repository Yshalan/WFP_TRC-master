
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Admin
Imports TA.Employees
Imports TA.LookUp
Imports TA.Security

Partial Class SelfServices_OvertimeRequest
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_Overtime As Emp_Overtime
    Private objAPP_Settings As APP_Settings
    Private objRequestStatus As RequestStatus

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Public Properties"

    Public Property EmpOverTimeId As Integer
        Get
            Return ViewState("EmpOverTimeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmpOverTimeId") = value
        End Set
    End Property

    Public Property dtCurrentControls() As DataTable
        Get
            Return ViewState("dtCurrentControls")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrentControls") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdOvertime.Skin))
    End Function

    Protected Sub dgrdOvertime_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdOvertime.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub Page_PreRenderComplete(sender As Object, e As EventArgs) Handles Me.PreRenderComplete

        For Each row As DataRow In dtCurrentControls.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not update1.FindControl(row("AddBtnName")) Is Nothing Then
                        update1.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not update1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        update1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not update1.FindControl(row("EditBtnName")) Is Nothing Then
                        update1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not update1.FindControl(row("PrintBtnName")) Is Nothing Then
                        update1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

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

    Private Sub SelfServices_OvertimeRequest_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then


            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                dgrdOvertime.Columns(5).Visible = False
            Else
                Lang = CtlCommon.Lang.EN
                dgrdOvertime.Columns(6).Visible = False
            End If

            RadTPFromTime.TimeView.HeaderText = String.Empty
            RadTPToTime.TimeView.HeaderText = String.Empty
            RadTPFromTime.TimeView.TimeFormat = "HH:mm"
            RadTPFromTime.TimeView.DataBind()
            RadTPToTime.TimeView.TimeFormat = "HH:mm"
            RadTPToTime.TimeView.DataBind()

            dtpFromDateSearch.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            dtpToDateSearch.SelectedDate = dd

            FillGridView()
            FillStatus()

            PageHeader1.HeaderText = ResourceManager.GetString("EmpOvertimeRequest")

        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrentControls.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not update1.FindControl(row("AddBtnName")) Is Nothing Then
                        update1.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not update1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        update1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not update1.FindControl(row("EditBtnName")) Is Nothing Then
                        update1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not update1.FindControl(row("PrintBtnName")) Is Nothing Then
                        update1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Private Sub dgrdOvertime_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdOvertime.NeedDataSource
        objEmp_Overtime = New Emp_Overtime
        With objEmp_Overtime

            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FromDateTime = DateSerial(Today.Year, Today.Month, 1)
            .ToDateTime = DateSerial(Today.Year, Today.Month + 1, 0)
            dgrdOvertime.DataSource = .GetByEmployee

        End With
    End Sub

    Private Sub btnGet_Click(sender As Object, e As EventArgs) Handles btnGet.Click
        objEmp_Overtime = New Emp_Overtime
        With objEmp_Overtime

            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FromDateTime = dtpFromDateSearch.DbSelectedDate
            .ToDateTime = dtpToDateSearch.DbSelectedDate
            .ProcessStatus = ddlStatus.SelectedValue
            dgrdOvertime.DataSource = .GetByEmployee
            dgrdOvertime.DataBind()

        End With
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        objEmp_Overtime = New Emp_Overtime
        Dim err As Integer = -1
        With objEmp_Overtime
            .EmpOverTimeId = EmpOverTimeId
            .ProcessStatus = 1
            .Emp_Remarks = txtRemarks.Text
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            err = .UpdateRequest()
        End With

        If err = 0 Then
            If Lang = CtlCommon.Lang.AR Then
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Redirect", "ShowMessageAndRedirect('تم إرسال الطلب الى المدير المباشر','../SelfServices/OvertimeRequest.aspx');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Redirect", "ShowMessageAndRedirect('The Request has been Sent to Direct Manager','../SelfServices/OvertimeRequest.aspx');", True)
            End If
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("../SelfServices/OvertimeRequest.aspx?Id=1")
    End Sub

    Private Sub dgrdOvertime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdOvertime.SelectedIndexChanged
        EmpOverTimeId = Convert.ToInt32(DirectCast(dgrdOvertime.SelectedItems(0), GridDataItem).GetDataKeyValue("EmpOverTimeId"))
        FillControls()
        mvEmpOvertimeRequest.SetActiveView(viewOvertimeForm)
        ControlsStatus(False)
    End Sub

    Private Sub dgrdOvertime_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdOvertime.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim item As GridDataItem
            item = e.Item

            Dim lnkLeaveForm As LinkButton = DirectCast(item.FindControl("lnkRequestOvertime"), LinkButton)


            If Not item("IsFinallyApproved").Text = "&nbsp;" Then
                If item("IsFinallyApproved").Text = "True" Then
                    lnkLeaveForm.Visible = False
                Else
                    lnkLeaveForm.Visible = True
                End If
            Else
                lnkLeaveForm.Visible = True
            End If

            If item("ProcessStatus").Text = "&nbsp;" Then
                lnkLeaveForm.Visible = True
            Else
                lnkLeaveForm.Visible = False
            End If

        End If
    End Sub

    Protected Sub lnkRequestOvertime_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        EmpOverTimeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("EmpOverTimeId"))
        FillControls()
        mvEmpOvertimeRequest.SetActiveView(viewOvertimeForm)
    End Sub

#End Region

#Region "Methods"

    Private Sub FillGridView()
        objEmp_Overtime = New Emp_Overtime
        With objEmp_Overtime

            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FromDateTime = DateSerial(Today.Year, Today.Month, 1)
            .ToDateTime = DateSerial(Today.Year, Today.Month + 1, 0)
            dgrdOvertime.DataSource = .GetByEmployee
            dgrdOvertime.DataBind()

        End With
    End Sub

    Private Sub FillControls()
        objEmp_Overtime = New Emp_Overtime
        With objEmp_Overtime
            .EmpOverTimeId = EmpOverTimeId
            .GetByPK()
            dtpOvertimeDate.DbSelectedDate = .FromDateTime
            RadTPFromTime.SelectedDate = .FromDateTime
            RadTPToTime.SelectedDate = .ToDateTime
            txtRemarks.Text = .Emp_Remarks
        End With
    End Sub

    Private Sub ClearAll()
        dtpOvertimeDate.DbSelectedDate = Date.Today
        RadTPFromTime.SelectedDate = Date.Now
        RadTPToTime.SelectedDate = Date.Now
        EmpOverTimeId = 0
    End Sub

    Private Sub FillStatus()
        objRequestStatus = New RequestStatus
        Dim dt As DataTable = Nothing
        dt = Nothing
        dt = objRequestStatus.GetAll
        ProjectCommon.FillRadComboBox(ddlStatus, dt, "StatusName",
                                     "StatusNameArabic", "StatusId", Lang)
    End Sub

    Private Sub ControlsStatus(ByVal Status As Boolean)
        dtpOvertimeDate.Enabled = Status
        RadTPFromTime.Enabled = Status
        RadTPToTime.Enabled = Status
        txtRemarks.Enabled = Status
        btnSave.Visible = Status
    End Sub


#End Region


End Class
