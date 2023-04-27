Imports System.Data
Imports TA.Employees
Imports SmartV.UTILITIES
Imports TA.Lookup
Imports TA.Admin
Imports TA.Definitions
Imports SmartV.UTILITIES.CtlCommon
Imports Telerik.Web.UI

Partial Class Employee_UserControls_EmpActiveSchedule
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private objWorkSchedule As New WorkSchedule
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
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

    Public Property EmployeeID() As Integer
        Get
            Return ViewState("EmployeeID")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeID") = value
        End Set
    End Property

    Public Property ScheduleDate() As DateTime
        Get
            Return ViewState("ScheduleDate")
        End Get
        Set(ByVal value As Date)
            ViewState("ScheduleDate") = value
        End Set
    End Property

    Public Property EmpWorkScheduleId() As Integer
        Get
            Return ViewState("EmpWorkScheduleId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmpWorkScheduleId") = value
        End Set
    End Property

    Public Property EmpActiveScheduleDT() As DataTable
        Get
            Return ViewState("EmpActiveScheduleDT")
        End Get
        Set(ByVal value As DataTable)
            ViewState("EmpActiveScheduleDT") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Employee_UserControls_EmpActiveSchedule_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        showHide(False)
        If Not Page.IsPostBack Then
            ScheduleDate = Date.Today
            dtpFromDate.SelectedDate = ScheduleDate
            dtpFromDate.Enabled = False
            If EmployeeID <> 0 Then
                fillCmbSchedule()
                fillGridSchedule()
            End If

            If SessionVariables.CultureInfo = "ar-JO" Then
                grdEmpSchedule.Columns(1).Visible = True
                grdEmpSchedule.Columns(0).Visible = False
            Else
                grdEmpSchedule.Columns(0).Visible = True
                grdEmpSchedule.Columns(1).Visible = False
            End If
        End If
        PageHeader1.HeaderText = ResourceManager.GetString("EmpActiveSchedule")

    End Sub

    Protected Sub grdEmpSchedule_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdEmpSchedule.PageIndexChanged
        grdEmpSchedule.CurrentPageIndex = e.NewPageIndex
        fillGridSchedule()
    End Sub

    Protected Sub grdEmpSchedule_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles grdEmpSchedule.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromDate").ToString())) And (Not item.GetDataKeyValue("FromDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("FromDate").ToString()
                item("FromDate").Text = fromDate.ToShortDateString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ToDate").ToString())) And (Not item.GetDataKeyValue("ToDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("ToDate").ToString()
                item("ToDate").Text = fromDate.ToShortDateString()
            End If
        End If

    End Sub

    Protected Sub grdEmpSchedule_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdEmpSchedule.NeedDataSource

        grdEmpSchedule.DataSource = EmpActiveScheduleDT

    End Sub

#End Region

#Region "Methods"

    Public Sub fillCmbSchedule()
        Dim objEmp_WorkSchedule As New Emp_WorkSchedule
        Dim objWorkSchedule As New WorkSchedule
        objWorkSchedule.EmployeeId = EmployeeID
        objWorkSchedule.GetActive_SchedulebyEmpId_row(Date.Today)

        'Dim dtCurrentSche As New DataTable
        'dtCurrentSche = objWorkSchedule.GetActiveSchedulebyEmpId(Date.Today)

        'If SessionVariables.CultureInfo = "ar-JO" Then
        '    Lang = CtlCommon.Lang.AR
        'Else
        '    Lang = CtlCommon.Lang.EN
        'End If
        'CtlCommon.FillTelerikDropDownList(RadCmbSchedule, dtCurrentSche, Lang)
        'If RadCmbSchedule.Items.Count > 0 Then
        '    RadCmbSchedule.SelectedIndex = 1
        If SessionVariables.CultureInfo = "ar-JO" Then
            lblActiveSchedule.Text = objWorkSchedule.ScheduleArabicName
        Else
            lblActiveSchedule.Text = objWorkSchedule.ScheduleName
        End If

        Try
            'dtpFromDate.SelectedDate = dtCurrentSche.Rows(1)("FromDate")
            dtpFromDate.DbSelectedDate = objWorkSchedule.FromDate
        Catch ex As Exception

        End Try

        'End If
    End Sub

    Public Sub fillGridSchedule()

        Dim objWorkSchedule As New WorkSchedule
        Dim objEmp_WorkSchedule As New Emp_WorkSchedule
        objWorkSchedule.EmployeeId = EmployeeID
        EmpActiveScheduleDT = objWorkSchedule.GetEmployeeActiveScheduleByEmpId(New Date(1900, 1, 1))
        grdEmpSchedule.DataSource = EmpActiveScheduleDT
        grdEmpSchedule.DataBind()

    End Sub

    Public Sub EnableControls(ByVal Status As Boolean)
        'RadCmbSchedule.Enabled = Status
    End Sub

    'Protected Sub grdEmpSchedule_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdEmpSchedule.SelectedIndexChanged
    '    Dim objEmp_WorkSchedule As New Emp_WorkSchedule
    '    EmpWorkScheduleId = CInt(CType(grdEmpSchedule.SelectedItems(0), GridDataItem)("EmpWorkScheduleId").Text)
    '    With objEmp_WorkSchedule
    '        .EmpWorkScheduleId = EmpWorkScheduleId
    '        .GetByPK()
    '        RadCmbSchedule.SelectedValue = .FK_ScheduleId
    '        dtpFromDate.SelectedDate = .FromDate
    '        If Not .ToDate = Nothing Then
    '            showHide(True)
    '            dtpToDate.SelectedDate = .ToDate
    '        Else
    '            showHide(False)
    '        End If
    '    End With
    'End Sub

    Private Sub showHide(ByVal status As Boolean, Optional ByVal order As Integer = 0)
        If order = 0 Then
            PnlOTEnddate.Visible = status
        Else
            PnlOTEnddate.Visible = status
        End If

    End Sub

#End Region

End Class
