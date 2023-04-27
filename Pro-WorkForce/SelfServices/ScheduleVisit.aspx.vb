Imports TA.Employees
Imports System.Data
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.LookUp
Imports SmartV.UTILITIES.ProjectCommon
Imports Telerik.Web.UI.Calendar
Imports TA.Definitions
Imports TA.SelfServices
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports System.IO
Imports TA.Security
Imports TA.Admin
Imports TA.DailyTasks
Imports VMS
Partial Class SelfServices_ScheduleVisit
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Shared dtCurrent As DataTable
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim IsAdmin As Boolean = False

    Dim UserId As String = ""
    Private objVisitors As Visitors
    Private objVisitInfo As VisitInfo
    Private objEmp_Nationality As Emp_Nationality
    Private objVisitorVisit As VisitorVisit

#End Region

#Region "Public Properties"

    Public Property VisitorId() As Integer
        Get
            Return ViewState("VisitorId")
        End Get
        Set(ByVal value As Integer)
            ViewState("VisitorId") = value
        End Set
    End Property

    Public Property VisitId() As Integer
        Get
            Return ViewState("VisitId")
        End Get
        Set(ByVal value As Integer)
            ViewState("VisitId") = value
        End Set
    End Property

    Public Property AdditionalVisitorsdt() As DataTable
        Get
            Return ViewState("AdditionalVisitorsdt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("AdditionalVisitorsdt") = value
        End Set
    End Property

    Public Property AdditionalVisitorId() As Integer
        Get
            Return ViewState("AdditionalVisitorId")
        End Get
        Set(ByVal value As Integer)
            ViewState("AdditionalVisitorId") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub RadToolBar1_ButtonClick(sender As Object, e As RadToolBarEventArgs)

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

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", grdVisitDetails.Skin))
    End Function

    Protected Sub dgrdEmpPermissionRequest_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdVisitDetails.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdVwEmpPermissions_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVisitDetails.SelectedIndexChanged


        VisitorId = Convert.ToInt32(DirectCast(grdVisitDetails.SelectedItems(0), GridDataItem).GetDataKeyValue("VisitorId"))
        VisitId = Convert.ToInt32(DirectCast(grdVisitDetails.SelectedItems(0), GridDataItem).GetDataKeyValue("VisitId"))
        'FillControls()
        FillVisitDetails()
        ClearAdditionalVisitors()
        FillAdditionalVisitorsGrid()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                dtpVisitDate.Culture = New System.Globalization.CultureInfo("ar-EG", False)
                rdDOB.Culture = New System.Globalization.CultureInfo("ar-EG", False)
                rdEidExpiry.Culture = New System.Globalization.CultureInfo("ar-EG", False)
            Else
                Lang = CtlCommon.Lang.EN
                dtpVisitDate.Culture = New System.Globalization.CultureInfo("en-US", False)
                rdDOB.Culture = New System.Globalization.CultureInfo("en-US", False)
                rdEidExpiry.Culture = New System.Globalization.CultureInfo("en-US", False)
            End If
            PgScheduleVisit.HeaderText = ResourceManager.GetString("ScheduleVisit")
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

            dtpVisitDate.DbSelectedDate = Date.Today
            rdDOB.DbSelectedDate = Date.Today
            rdEidExpiry.DbSelectedDate = Date.Today
            RadTPfromTime.DbSelectedDate = DateTime.Now
            RadTPtoTime.DbSelectedDate = DateTime.Now

            FillNationality()
            LoadVisitDeatils()
            SetRadDateTimePickerPeoperties()
            PrepareAdditionalVisitorsdt()
        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Dim errNum As Integer = -1
        Dim DimVisitorId As Integer
        Dim DimVisitId As Integer
        For Each row As GridDataItem In grdVisitDetails.Items
            Dim cb As CheckBox = DirectCast(row.FindControl("chk"), CheckBox)
            If cb.Checked Then
                DimVisitorId = Convert.ToInt32(row.GetDataKeyValue("VisitorId"))
                DimVisitId = Convert.ToInt32(row.GetDataKeyValue("VisitId"))
                objVisitors = New Visitors
                objVisitInfo = New VisitInfo
                objVisitorVisit = New VisitorVisit
                objVisitInfo.VisitId = DimVisitId

                objVisitors.VisitorId = DimVisitorId
                objVisitors.FK_VisitId = DimVisitId

                objVisitorVisit.FK_VisitId = DimVisitId
                objVisitorVisit.FK_VisitorId = DimVisitorId
                objVisitInfo.Delete()
                objVisitors.DeleteByFK_VisitId()
                objVisitorVisit.DeleteByFK_VisitId()
            End If
        Next
        ClearAll()
        LoadVisitDeatils()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        objVisitors = New Visitors
        objVisitInfo = New VisitInfo
        Dim VisVisit As VisitorVisit = New VisitorVisit
        For Each row In AdditionalVisitorsdt.Rows
            With objVisitors
                .VisitorId = IIf(row("VisitorId") Is DBNull.Value, 0, row("VisitorId"))
                .VisitorName = row("VisitorName")
                .VisitorArabicName = row("VisitorArabicName")
                .Nationality = row("Nationality")
                .MobileNumber = row("MobileNumber")
                .OrganizationName = row("OrganizationName")
                .Gender = row("Gender")
                .IDNumber = row("IDNumber")
                .DOB = row("DOB")
                .EIDExpiryDate = row("EIDExpiryDate")
                .IsDeleted = False
                .Remarks = ""

                If VisitorId = 0 Then
                    .CREATED_BY = SessionVariables.LoginUser.ID
                    .Add()
                    row("VisitorId") = .VisitorId
                Else
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    .Update()
                End If
            End With
        Next

        With objVisitInfo
            Dim visitdate As DateTime = dtpVisitDate.SelectedDate
            Dim CheckinTIme As TimeSpan = RadTPfromTime.SelectedTime
            Dim ChecOutTIME As TimeSpan = RadTPtoTime.SelectedTime

            .ExpectedCheckInTime = New DateTime(visitdate.Year, visitdate.Month, visitdate.Day, CheckinTIme.Hours, CheckinTIme.Minutes, CheckinTIme.Seconds)
            .ExpectedCheckOutTime = New DateTime(visitdate.Year, visitdate.Month, visitdate.Day, ChecOutTIME.Hours, ChecOutTIME.Minutes, ChecOutTIME.Seconds)

            .FK_DepartmentId = 0
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId

            .ReasonOfVisit = ddlreason.SelectedValue
            .IsDeleted = False
            .Remarks = ""
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            If VisitId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                .Add()
            Else
                .VisitId = VisitId
                .Update()
            End If
        End With
        If VisitId = 0 Then
            For Each row In AdditionalVisitorsdt.Rows
                With VisVisit
                    .FK_VisitId = objVisitInfo.VisitId
                    .FK_VisitorId = row("VisitorId")
                    .Add()
                End With
            Next
        End If

        If objVisitors.VisitorId = 0 Then
            CtlCommon.ShowMessage(Page, "Visit Details Not Saved, There is an error.", "error")
        Else

            CtlCommon.ShowMessage(Page, "Visit Details Saved Successfully", "success")
            LoadVisitDeatils()
        End If
        ClearAll()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

#End Region

#Region "Methods"

    Private Sub LoadVisitDeatils()
        Dim Visit As Visitors = New Visitors
        Visit.CREATED_BY = SessionVariables.LoginUser.ID.ToString()
        grdVisitDetails.DataSource = Visit.GetAllVisitDetails()
        grdVisitDetails.DataBind()
    End Sub

    Private Sub FillControls()
        Dim Visit As Visitors = New Visitors
        Dim VisInfo As VisitInfo = New VisitInfo
        VisInfo.VisitId = VisitId
        Visit.VisitorId = VisitorId
        VisInfo.GetByPK()
        Visit.GetByPK()

        txtName.Text = Visit.VisitorName
        txtNameAr.Text = Visit.VisitorArabicName
        radcmbxNationality.SelectedValue = Visit.Nationality
        txtMobile.Text = Visit.MobileNumber
        txtOrganization.Text = Visit.OrganizationName
        ddlGender.SelectedValue = Visit.Gender
        txtEID.Text = Visit.IDNumber
        rdDOB.SelectedDate = Visit.DOB
        rdEidExpiry.SelectedDate = Visit.EIDExpiryDate
        dtpVisitDate.SelectedDate = VisInfo.ExpectedCheckInTime
        RadTPfromTime.SelectedTime = VisInfo.ExpectedCheckInTime.TimeOfDay
        RadTPtoTime.SelectedTime = VisInfo.ExpectedCheckOutTime.TimeOfDay
        ddlreason.SelectedValue = VisInfo.ReasonOfVisit
    End Sub

    Private Sub ClearAll()
        txtName.Text = String.Empty
        txtNameAr.Text = String.Empty
        radcmbxNationality.SelectedValue = -1
        txtMobile.Text = String.Empty
        txtOrganization.Text = String.Empty
        ddlGender.SelectedValue = -1
        txtEID.Text = String.Empty
        rdDOB.DbSelectedDate = DateTime.Now
        rdEidExpiry.DbSelectedDate = DateTime.Now
        dtpVisitDate.DbSelectedDate = DateTime.Now
        RadTPfromTime.DbSelectedDate = DateTime.Now
        RadTPtoTime.DbSelectedDate = DateTime.Now
        ddlreason.SelectedValue = -1
        VisitorId = 0
        PrepareAdditionalVisitorsdt()
        ClearServicedt()
    End Sub

    Private Sub SetRadDateTimePickerPeoperties()


        ' This function set properties for terlerik controls

        'Imports Telerik.Web.UI.DatePickerPopupDirection

        ' Set TimeView properties 
        Me.RadTPfromTime.TimeView.StartTime = New TimeSpan(0, 0, 0)
        Me.RadTPfromTime.TimeView.EndTime = New TimeSpan(23.5, 0, 0)
        Me.RadTPfromTime.TimeView.HeaderText = String.Empty
        Me.RadTPfromTime.TimeView.Interval = New TimeSpan(0, 30, 0)
        Me.RadTPfromTime.TimeView.Columns = 5



        ' Set Popup window properties
        Me.RadTPfromTime.PopupDirection = TopRight
        Me.RadTPfromTime.ShowPopupOnFocus = True
        Me.RadTPfromTime.TimeView.TimeFormat = "HH:mm"


        ' Set default value
        Me.RadTPfromTime.SelectedDate = Now

        ' Set TimeView properties 
        Me.RadTPtoTime.TimeView.StartTime = New TimeSpan(0, 0, 0)
        Me.RadTPtoTime.TimeView.EndTime = New TimeSpan(23.5, 0, 0)
        Me.RadTPtoTime.TimeView.Interval = New TimeSpan(0, 30, 0)
        Me.RadTPtoTime.TimeView.HeaderText = String.Empty
        Me.RadTPtoTime.TimeView.Columns = 5
        ' Set Popup window properties
        Me.RadTPtoTime.PopupDirection = TopRight
        Me.RadTPtoTime.ShowPopupOnFocus = True
        Me.RadTPtoTime.TimeView.TimeFormat = "HH:mm"
        ' Set default value
        Me.RadTPtoTime.SelectedDate = Now

    End Sub

    Private Sub FillNationality()
        Dim dt As DataTable
        objEmp_Nationality = New Emp_Nationality
        With objEmp_Nationality
            dt = .GetAll
            ProjectCommon.FillRadComboBox(radcmbxNationality, dt, "NationalityName", "NationalityArabicName")
        End With
    End Sub

    Private Sub FillVisitDetails()
        objVisitInfo = New VisitInfo
        With objVisitInfo
            .VisitId = VisitId
            .GetByPK()
            dtpVisitDate.SelectedDate = .ExpectedCheckInTime
            RadTPfromTime.SelectedTime = .ExpectedCheckInTime.TimeOfDay
            RadTPtoTime.SelectedTime = .ExpectedCheckOutTime.TimeOfDay
            ddlreason.SelectedValue = .ReasonOfVisit
        End With
    End Sub

#End Region


    Private Sub PrepareAdditionalVisitorsdt()

        AdditionalVisitorsdt = New DataTable
        Dim dc As DataColumn
        dc = New DataColumn
        dc.ColumnName = "RowVisitorId"
        dc.DataType = GetType(Integer)
        AdditionalVisitorsdt.Columns.Add(dc)
        AdditionalVisitorsdt.Columns("RowVisitorId").AutoIncrement = True
        AdditionalVisitorsdt.Columns("RowVisitorId").AutoIncrementSeed = 1
        AdditionalVisitorsdt.Columns("RowVisitorId").AutoIncrementStep = 1

        dc = New DataColumn
        dc.ColumnName = "VisitorId"
        dc.DataType = GetType(Integer)
        AdditionalVisitorsdt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "VisitorName"
        dc.DataType = GetType(String)
        AdditionalVisitorsdt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "VisitorArabicName"
        dc.DataType = GetType(String)
        AdditionalVisitorsdt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "Nationality"
        dc.DataType = GetType(Integer)
        AdditionalVisitorsdt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "MobileNumber"
        dc.DataType = GetType(String)
        AdditionalVisitorsdt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "OrganizationName"
        dc.DataType = GetType(String)
        AdditionalVisitorsdt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "Gender"
        dc.DataType = GetType(Integer)
        AdditionalVisitorsdt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "IDNumber"
        dc.DataType = GetType(String)
        AdditionalVisitorsdt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "DOB"
        dc.DataType = GetType(DateTime)
        AdditionalVisitorsdt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "EIDExpiryDate"
        dc.DataType = GetType(DateTime)
        AdditionalVisitorsdt.Columns.Add(dc)

        dgrdAdditionalVisitors.DataSource = AdditionalVisitorsdt
        dgrdAdditionalVisitors.DataBind()

    End Sub

    Protected Sub RadToolBar2_ButtonClick(sender As Object, e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon2() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdAdditionalVisitors.Skin))
    End Function

    Protected Sub dgrdAdditionalVisitors_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdAdditionalVisitors.ItemCommand
        If e.CommandName = "FilterRadGrid1" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Private Sub dgrdAdditionalVisitors_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdAdditionalVisitors.NeedDataSource
        dgrdAdditionalVisitors.DataSource = AdditionalVisitorsdt
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim dr As DataRow

        If AdditionalVisitorId = 0 Then
            dr = AdditionalVisitorsdt.NewRow
            dr("VisitorName") = txtName.Text
            dr("VisitorArabicName") = txtNameAr.Text
            dr("Nationality") = radcmbxNationality.SelectedValue
            dr("MobileNumber") = txtMobile.Text
            dr("OrganizationName") = txtOrganization.Text
            dr("Gender") = ddlGender.SelectedValue
            dr("IDNumber") = txtEID.Text
            dr("DOB") = IIf(rdDOB.DbSelectedDate Is Nothing, DBNull.Value, rdDOB.DbSelectedDate)
            dr("EIDExpiryDate") = IIf(rdEidExpiry.DbSelectedDate Is Nothing, DBNull.Value, rdEidExpiry.DbSelectedDate)
            AdditionalVisitorsdt.Rows.Add(dr)
            AdditionalVisitorsdt.AcceptChanges()
        Else
            If AdditionalVisitorsdt.Rows.Count > 0 Then
                dr = AdditionalVisitorsdt.Select("VisitorId= " & AdditionalVisitorId)(0)
                dr("VisitorName") = txtName.Text
                dr("VisitorArabicName") = txtNameAr.Text
                dr("Nationality") = radcmbxNationality.SelectedValue
                dr("MobileNumber") = txtMobile.Text
                dr("OrganizationName") = txtOrganization.Text
                dr("Gender") = ddlGender.SelectedValue
                dr("IDNumber") = txtEID.Text
                dr("DOB") = IIf(rdDOB.DbSelectedDate Is Nothing, DBNull.Value, rdDOB.DbSelectedDate)
                dr("EIDExpiryDate") = IIf(rdEidExpiry.DbSelectedDate Is Nothing, DBNull.Value, rdEidExpiry.DbSelectedDate)

            End If
        End If


        dgrdAdditionalVisitors.DataSource = AdditionalVisitorsdt
        dgrdAdditionalVisitors.DataBind()
        ClearAdditionalVisitors()
    End Sub

    Private Sub FillAdditionalVisitorControls()

        AdditionalVisitorId = CInt(CType(dgrdAdditionalVisitors.SelectedItems(0), GridDataItem).GetDataKeyValue("VisitorId").ToString())

        txtName.Text = CType(dgrdAdditionalVisitors.SelectedItems(0), GridDataItem).GetDataKeyValue("VisitorName").ToString()
        txtNameAr.Text = CType(dgrdAdditionalVisitors.SelectedItems(0), GridDataItem).GetDataKeyValue("VisitorArabicName").ToString()
        radcmbxNationality.SelectedValue = CInt(CType(dgrdAdditionalVisitors.SelectedItems(0), GridDataItem).GetDataKeyValue("Nationality").ToString())
        txtMobile.Text = CType(dgrdAdditionalVisitors.SelectedItems(0), GridDataItem).GetDataKeyValue("MobileNumber").ToString()
        txtOrganization.Text = CType(dgrdAdditionalVisitors.SelectedItems(0), GridDataItem).GetDataKeyValue("OrganizationName").ToString()
        ddlGender.SelectedValue = CInt(CType(dgrdAdditionalVisitors.SelectedItems(0), GridDataItem).GetDataKeyValue("Gender").ToString())
        txtEID.Text = CType(dgrdAdditionalVisitors.SelectedItems(0), GridDataItem).GetDataKeyValue("IDNumber").ToString()
        rdDOB.DbSelectedDate = CDate(CType(dgrdAdditionalVisitors.SelectedItems(0), GridDataItem).GetDataKeyValue("DOB").ToString())
        rdEidExpiry.DbSelectedDate = CDate(CType(dgrdAdditionalVisitors.SelectedItems(0), GridDataItem).GetDataKeyValue("EIDExpiryDate").ToString())

    End Sub

    Private Sub dgrdAdditionalVisitors_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdAdditionalVisitors.SelectedIndexChanged
        AdditionalVisitorId = Convert.ToInt32(DirectCast(dgrdAdditionalVisitors.SelectedItems(0), GridDataItem).GetDataKeyValue("RowVisitorId"))
        FillAdditionalVisitorControls()
    End Sub

    Private Sub ClearAdditionalVisitors()
        txtName.Text = String.Empty
        txtNameAr.Text = String.Empty
        radcmbxNationality.SelectedValue = -1
        txtMobile.Text = String.Empty
        txtOrganization.Text = String.Empty
        ddlGender.SelectedValue = -1
        txtEID.Text = String.Empty
        rdDOB.DbSelectedDate = DateTime.Now
        rdEidExpiry.DbSelectedDate = DateTime.Now
        AdditionalVisitorId = 0
    End Sub

    Private Sub btnClearAdditionalVistor_Click(sender As Object, e As EventArgs) Handles btnClearAdditionalVistor.Click
        ClearAdditionalVisitors()
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        For Each row As GridDataItem In dgrdAdditionalVisitors.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intRowVisitorId As Integer = Convert.ToInt32(row.GetDataKeyValue("RowVisitorId").ToString())
                AdditionalVisitorsdt.Rows.Remove(AdditionalVisitorsdt.Select("RowVisitorId = " & intRowVisitorId)(0))
            End If
        Next
        dgrdAdditionalVisitors.DataSource = AdditionalVisitorsdt
        dgrdAdditionalVisitors.DataBind()
    End Sub

    Private Sub ClearServicedt()
        dgrdAdditionalVisitors.DataSource = AdditionalVisitorsdt
        dgrdAdditionalVisitors.DataBind()

    End Sub

    Private Sub FillAdditionalVisitorsGrid()
        objVisitorVisit = New VisitorVisit
        With objVisitorVisit
            .FK_VisitId = VisitId
            AdditionalVisitorsdt = .Get_ByFK_VisitId

            Dim dc As DataColumn
            dc = New DataColumn
            dc.ColumnName = "RowVisitorId"
            dc.DataType = GetType(Integer)
            AdditionalVisitorsdt.Columns.Add(dc)
            For i As Integer = 0 To AdditionalVisitorsdt.Rows.Count - 1
                AdditionalVisitorsdt.Rows(i)("RowVisitorId") = i + 1
            Next
            AdditionalVisitorsdt.Columns("RowVisitorId").AutoIncrement = True
            AdditionalVisitorsdt.Columns("RowVisitorId").AutoIncrementSeed = 1
            AdditionalVisitorsdt.Columns("RowVisitorId").AutoIncrementStep = 1

            dgrdAdditionalVisitors.DataSource = AdditionalVisitorsdt
            dgrdAdditionalVisitors.DataBind()
        End With
    End Sub

End Class
