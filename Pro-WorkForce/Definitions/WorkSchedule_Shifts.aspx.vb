Imports System.Data
Imports TA.Definitions
Imports SmartV.DB
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.Admin
Imports TA.Security
Imports SmartV.UTILITIES.ProjectCommon

Partial Class Admin_WorkSchedule_Shifts
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objWeekDays As New WeekDays
    Private objWorkSchedule_Shifts As WorkSchedule_Shifts
    Private objWorkSchedule As WorkSchedule
    Private objApp_Settings As APP_Settings
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Public Properties"

    Public Property ShiftId() As Integer
        Get
            Return ViewState("ShiftId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ShiftId") = value
        End Set
    End Property

    Public Property ScheduleID() As Integer
        Get
            Return ViewState("ScheduleID")
        End Get
        Set(ByVal value As Integer)
            ViewState("ScheduleID") = value
        End Set
    End Property

    Public Property DayID() As Integer
        Get
            Return ViewState("DayID")
        End Get
        Set(ByVal value As Integer)
            ViewState("DayID") = value
        End Set
    End Property

    Public Property DtWeeks() As DataTable
        Get
            Return ViewState("DtWeeks")
        End Get
        Set(ByVal value As DataTable)
            ViewState("DtWeeks") = value
        End Set
    End Property

    Public Property DtCurrent() As DataTable
        Get
            Return ViewState("DtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("DtCurrent") = value
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
    'Protected Sub lnkScheduleName_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim gvr As GridViewRow = CType(sender, LinkButton).Parent.Parent


    'End Sub

    Protected Sub dgrdWorkSchedule_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

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
            objApp_Settings = New APP_Settings()
            objApp_Settings.GetByPK()
            If objApp_Settings.IsGraceTAPolicy Then
                trGraceIn.Visible = False
                trGraceOut.Visible = False
            End If

            FillGrid()
            PageHeader1.HeaderText = ResourceManager.GetString("ShiftSchedule", CultureInfo)
        End If

        ibtnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdWorkSchedule.ClientID + "')")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrentControls.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not trControls.FindControl(row("AddBtnName")) Is Nothing Then
                        trControls.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not trControls.FindControl(row("DeleteBtnName")) Is Nothing Then
                        trControls.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not trControls.FindControl(row("EditBtnName")) Is Nothing Then
                        trControls.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not trControls.FindControl(row("PrintBtnName")) Is Nothing Then
                        trControls.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnSave.Click

        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objWorkSchedule = New WorkSchedule
        Dim err As Integer
        With objWorkSchedule
            .ScheduleName = txtScheduleEng.Text.Trim()
            .ScheduleArabicName = txtScheduleAr.Text.Trim()
            If (trGraceIn.Visible) Then
                .GraceIn = txtGraceIn.Text
            End If
            If (trGraceOut.Visible) Then
                .GraceOut = txtGraceOut.Text
            End If
            If txtMinimumAllowTime.Text = "" Then
                .MinimumAllowTime = "0"
            Else
                .MinimumAllowTime = txtMinimumAllowTime.Text
            End If

            If chkConsiderAtShiftEnd.Checked = True Then
                .ConsiderShiftScheduleAtEnd = True
            Else
                .ConsiderShiftScheduleAtEnd = False
            End If

            .IsActive = chkIsActive.Checked

            .ScheduleType = 3
            If ScheduleID = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add()
                ScheduleID = .ScheduleId
            Else
                .ScheduleId = ScheduleID
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                err = .Update()
            End If
        End With

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGrid()
            ClearAll()
        ElseIf err = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
        ElseIf err = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub ibtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnDelete.Click
        Dim err As Integer

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdWorkSchedule.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intScheduleID As Integer = Convert.ToInt32(row.GetDataKeyValue("ScheduleId").ToString())
                objWorkSchedule_Shifts = New WorkSchedule_Shifts()
                objWorkSchedule_Shifts.FK_ScheduleId = intScheduleID
                err += objWorkSchedule_Shifts.DeleteByFKSchedleID()

                objWorkSchedule = New WorkSchedule
                objWorkSchedule.ScheduleId = intScheduleID
                err += objWorkSchedule.Delete()

            End If
        Next

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGrid()
            ClearAll()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If

    End Sub

    Protected Sub ibtnRest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnRest.Click
        ClearAll()

    End Sub

    Protected Sub dgrdWorkSchedule_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdWorkSchedule.NeedDataSource
        objWorkSchedule = New WorkSchedule
        objWorkSchedule.ScheduleType = 3
        dgrdWorkSchedule.DataSource = objWorkSchedule.GetAllByType()
    End Sub

    Protected Sub dgrdWorkSchedule_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdWorkSchedule.SelectedIndexChanged
        ClearShiftControls()
        ScheduleID = CInt(CType(dgrdWorkSchedule.SelectedItems(0), GridDataItem).GetDataKeyValue("ScheduleId").ToString())
        objWorkSchedule = New WorkSchedule
        With objWorkSchedule
            .ScheduleId = ScheduleID
            .GetByPK()
            txtGraceIn.Text = .GraceIn
            txtScheduleEng.Text = .ScheduleName
            txtScheduleAr.Text = .ScheduleArabicName
            txtGraceOut.Text = .GraceOut
            txtMinimumAllowTime.Text = .MinimumAllowTime
            If .ConsiderShiftScheduleAtEnd = True Then
                chkConsiderAtShiftEnd.Checked = True
            Else
                chkConsiderAtShiftEnd.Checked = False
            End If
            chkIsActive.Checked = .IsActive
        End With

        objWorkSchedule_Shifts = New WorkSchedule_Shifts()
        objWorkSchedule_Shifts.FK_ScheduleId = ScheduleID
        grdShift.DataSource = objWorkSchedule_Shifts.GetByFKScheduleID()
        grdShift.DataBind()
    End Sub

    Protected Sub grdShift_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdShift.RowCommand
        If e.CommandName = "ShiftEdit" Then
            ShiftId = grdShift.DataKeys(e.CommandArgument)("ShiftId").ToString()
            objWorkSchedule_Shifts = New WorkSchedule_Shifts()
            objWorkSchedule_Shifts.ShiftId = ShiftId
            objWorkSchedule_Shifts.GetByPK()

            ScheduleID = objWorkSchedule_Shifts.FK_ScheduleId
            txtTKColor.Text = objWorkSchedule_Shifts.Color
            txtTKColor.Style.Item(HtmlTextWriterStyle.BackgroundColor) = objWorkSchedule_Shifts.Color
            txtTKColor.Style.Item(HtmlTextWriterStyle.Color) = objWorkSchedule_Shifts.Color
            txtFromTime1.Text = objWorkSchedule_Shifts.FromTime1
            txtToTime1.Text = objWorkSchedule_Shifts.ToTime1
            txtFromTime2.Text = objWorkSchedule_Shifts.FromTime2
            txtToTime2.Text = objWorkSchedule_Shifts.ToTime2
            txtShiftName.Text = objWorkSchedule_Shifts.ShiftArabicName
            txtShiftName.Text = objWorkSchedule_Shifts.ShiftName
            txtShiftCode.Text = objWorkSchedule_Shifts.ShiftCode
            chkIsOffDay.Checked = objWorkSchedule_Shifts.IsOffDay

            If chkIsOffDay.Checked = True Then
                txtFromTime1.Enabled = False
                txtFromTime2.Enabled = False
                txtToTime1.Enabled = False
                txtToTime2.Enabled = False
                radFlex1.Enabled = False
                radFlex2.Enabled = False
                txtTKColor.Text = "#ccc"
                txtTKColor.Style.Item(HtmlTextWriterStyle.BackgroundColor) = "#ccc"
                txtTKColor.Style.Item(HtmlTextWriterStyle.Color) = "#ccc"
            Else
                txtFromTime1.Enabled = True
                txtFromTime2.Enabled = True
                txtToTime1.Enabled = True
                txtToTime2.Enabled = True
                radFlex1.Enabled = True
                radFlex2.Enabled = True
                txtTKColor.Text = "#FFFFFF"
                txtTKColor.Style.Item(HtmlTextWriterStyle.BackgroundColor) = "#FFFFFF"
                txtTKColor.Style.Item(HtmlTextWriterStyle.Color) = "#FFFFFF"
            End If


            If Not objWorkSchedule_Shifts.FlexTime1 = 0 Or Not objWorkSchedule_Shifts.FlexTime1 = Nothing Then
                radFlex1.Text = CtlCommon.GetFullTimeString(objWorkSchedule_Shifts.FlexTime1)
            Else
                radFlex1.Text = "0000"
            End If
            If Not objWorkSchedule_Shifts.FlexTime2 = 0 Or Not objWorkSchedule_Shifts.FlexTime2 = Nothing Then
                radFlex2.Text = CtlCommon.GetFullTimeString(objWorkSchedule_Shifts.FlexTime2)
            Else
                radFlex2.Text = "0000"
            End If

        End If
    End Sub

    Protected Sub grdShift_ItemDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles grdShift.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblShiftColor As Label = DirectCast(e.Row.FindControl("lblShiftColor"), Label)

            lblShiftColor.Style.Item(HtmlTextWriterStyle.BackgroundColor) = e.Row.DataItem("Color").ToString()

        End If
    End Sub

    Protected Sub btnSaveShift_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveShift.Click

        Dim err = 0
        objWorkSchedule_Shifts = New WorkSchedule_Shifts
        If ShiftId <> 0 Then
            objWorkSchedule_Shifts.ShiftId = ShiftId
        End If
        If (ScheduleID = 0) Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SavScheduleShiftDetail", CultureInfo))
            TabContainer1.ActiveTabIndex = 0
            ClearAll()
        End If
        objWorkSchedule_Shifts.FK_ScheduleId = ScheduleID
        objWorkSchedule_Shifts.Color = IIf(txtTKColor.Text.Trim() = "", "#FFFFFF", txtTKColor.Text)
        objWorkSchedule_Shifts.FromTime1 = txtFromTime1.Text.Replace(":", "")
        objWorkSchedule_Shifts.ToTime1 = txtToTime1.Text.Replace(":", "")
        objWorkSchedule_Shifts.FromTime2 = txtFromTime2.Text.Replace(":", "")
        objWorkSchedule_Shifts.ToTime2 = txtToTime2.Text.Replace(":", "")
        objWorkSchedule_Shifts.ShiftArabicName = txtShiftName.Text
        objWorkSchedule_Shifts.ShiftName = txtShiftName.Text
        objWorkSchedule_Shifts.ShiftCode = txtShiftCode.Text
        objWorkSchedule_Shifts.CREATED_BY = ""
        objWorkSchedule_Shifts.CREATED_DATE = DateTime.Now
        objWorkSchedule_Shifts.LAST_UPDATE_BY = ""
        objWorkSchedule_Shifts.LAST_UPDATE_DATE = DateTime.Now
        objWorkSchedule_Shifts.FlexTime1 = (radFlex1.Text.Substring(0, 2) * 60) + (radFlex1.Text.Substring(2, 2))
        objWorkSchedule_Shifts.FlexTime2 = (radFlex2.Text.Substring(0, 2) * 60) + (radFlex2.Text.Substring(2, 2))
        objWorkSchedule_Shifts.IsOffDay = chkIsOffDay.Checked
        If ShiftId = 0 Then
            err = objWorkSchedule_Shifts.Add()
        Else
            err = objWorkSchedule_Shifts.Update()
        End If

        If err = 0 Then

            grdShift.DataSource = objWorkSchedule_Shifts.GetByFKScheduleID()
            grdShift.DataBind()
            ClearShiftControls()
        End If
    End Sub

    Protected Sub btnClearShift_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearShift.Click
        ClearShiftControls()
    End Sub

    Protected Sub btnDeleteShift_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteShift.Click
        Dim err As Integer
        objWorkSchedule_Shifts = New WorkSchedule_Shifts
        objWorkSchedule_Shifts.ShiftId = ShiftId
        err = objWorkSchedule_Shifts.Delete()
        If err = 0 Then
            ClearShiftControls()
            grdShift_Bind()
        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub FillGrid()
        objWorkSchedule = New WorkSchedule
        objWorkSchedule.ScheduleType = 3
        dgrdWorkSchedule.DataSource = objWorkSchedule.GetAllByType()
        dgrdWorkSchedule.DataBind()

        If (trGraceIn.Visible = False And trGraceOut.Visible = False) Then
            dgrdWorkSchedule.Columns(3).Visible = False
            dgrdWorkSchedule.Columns(4).Visible = False
        End If

    End Sub

    Public Sub FillgrdShift()
        objWorkSchedule_Shifts = New WorkSchedule_Shifts
        objWorkSchedule_Shifts.FK_ScheduleId = ScheduleID
        grdShift.DataSource = objWorkSchedule_Shifts.GetByFKScheduleID()
        grdShift.DataBind()
    End Sub

    Public Sub ClearAll()
        txtGraceIn.Text = String.Empty
        txtGraceOut.Text = String.Empty
        txtScheduleAr.Text = String.Empty
        txtScheduleEng.Text = String.Empty
        ScheduleID = 0
        DayID = 0
        dgrdWorkSchedule.SelectedIndexes.Clear()
        grdShift.DataSource = Nothing
        grdShift.DataBind()
        ClearShiftControls()
        txtMinimumAllowTime.Text = ""
        chkConsiderAtShiftEnd.Checked = False
        chkIsActive.Checked = False
    End Sub

    Public Function validate_Time(ByVal var_Time As String) As String
        Dim temp_arry As Array
        Dim time_string As String = ""

        temp_arry = Split(var_Time, ":")
        If temp_arry(0) < 10 Then
            time_string = "0" + temp_arry(0)
        End If
        Dim temp_value As String = temp_arry(1)
        If temp_value.Length < 2 Then
            If temp_arry(1) < 10 Then
                time_string += ":" + "0" + temp_arry(1)
            Else
                time_string += ":" + temp_arry(1)
            End If
        Else
            time_string += ":" + temp_arry(1)
        End If

        Return time_string


    End Function

    Public Function time_range_validate(ByVal var_time As String) As String
        Dim temp_arry As Array
        Dim time_string As String = ""
        temp_arry = Split(var_time, ":")
        If Not temp_arry(0) = Nothing Then
            If temp_arry(0) > 23 Then
                temp_arry(0) = "00"
            End If
            time_string = temp_arry(0)
        Else
            time_string = "00"
        End If
        If Not temp_arry(1) = Nothing Then

            If temp_arry(1) > 59 Then
                time_string = Val(time_string) + 1
                If Val(time_string) >= 10 Then
                    time_string += ":" + "00"
                    time_string = time_range_validate(time_string)
                Else
                    time_string = "0" + time_string + ":" + "00"
                End If
            Else
                time_string += ":" + temp_arry(1)
            End If
        Else
            time_string += ":" + "00"
        End If

        Return time_string
    End Function

    Protected Sub grdShift_Bind()
        objWorkSchedule_Shifts = New WorkSchedule_Shifts
        objWorkSchedule_Shifts.FK_ScheduleId = ScheduleID
        grdShift.DataSource = objWorkSchedule_Shifts.GetByFKScheduleID()
        grdShift.DataBind()
    End Sub

    Private Sub ClearShiftControls()
        ShiftId = 0
        txtTKColor.Text = "#FFFFFF"
        txtTKColor.Style.Item(HtmlTextWriterStyle.BackgroundColor) = "#FFFFFF"
        txtTKColor.Style.Item(HtmlTextWriterStyle.Color) = "#FFFFFF"
        txtFromTime1.Text = "0000"
        txtToTime1.Text = "0000"
        txtFromTime2.Text = "0000"
        txtToTime2.Text = "0000"
        txtShiftName.Text = String.Empty
        txtShiftCode.Text = String.Empty
        radFlex1.Text = "0000"
        radFlex2.Text = "0000"
        chkIsOffDay.Checked = False
        txtFromTime1.Enabled = True
        txtFromTime2.Enabled = True
        txtToTime1.Enabled = True
        txtToTime2.Enabled = True
        radFlex1.Enabled = True
        radFlex2.Enabled = True
    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdWorkSchedule.Skin))
    End Function

    Private Sub chkIsOffDay_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsOffDay.CheckedChanged
        If chkIsOffDay.Checked = True Then
            txtFromTime1.Enabled = False
            txtFromTime2.Enabled = False
            txtToTime1.Enabled = False
            txtToTime2.Enabled = False
            radFlex1.Enabled = False
            radFlex2.Enabled = False
            txtTKColor.Text = "#ccc"
            txtTKColor.Style.Item(HtmlTextWriterStyle.BackgroundColor) = "#ccc"
            txtTKColor.Style.Item(HtmlTextWriterStyle.Color) = "#ccc"
        Else
            txtFromTime1.Enabled = True
            txtFromTime2.Enabled = True
            txtToTime1.Enabled = True
            txtToTime2.Enabled = True
            radFlex1.Enabled = True
            radFlex2.Enabled = True
            txtTKColor.Text = "#FFFFFF"
            txtTKColor.Style.Item(HtmlTextWriterStyle.BackgroundColor) = "#FFFFFF"
            txtTKColor.Style.Item(HtmlTextWriterStyle.Color) = "#FFFFFF"
        End If
    End Sub

#End Region

End Class
