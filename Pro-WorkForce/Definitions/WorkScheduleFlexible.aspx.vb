Imports System.Data
Imports TA.Definitions
Imports SmartV.DB
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.Admin

Partial Class Admin_WorkScheduleFlexible
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objWeekDays As New WeekDays
    Private objWorkSchedule_Flexible As WorkSchedule_Flexible
    Private objWorkSchedule As WorkSchedule
    Private objApp_Settings As APP_Settings
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Public Properties"

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

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdWorkSchedule.Skin))
    End Function

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



            objApp_Settings = New APP_Settings()
            objApp_Settings.GetByPK()
            If objApp_Settings.IsGraceTAPolicy Then
                trGraceIn.Visible = False
                trGraceOut.Visible = False
            End If

            'CreateDTWeekDays()
            CtlCommon.FillRadGridView(dgrdWeekTimeShift1, objWeekDays.Flexible_GetAll)
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            clear_DataGrid_Controls()
            FillGrid()
            PageHeader1.HeaderText = ResourceManager.GetString("FlixebleSchedule", CultureInfo)
        End If
        ibtnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdWorkSchedule.ClientID + "')")
    End Sub

    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        For Each gvr As GridDataItem In dgrdWeekTimeShift1.Items
            If CBool(CType(gvr.FindControl("Shift2offDay"), CheckBox).Checked) = False Then

                If (CStr(CType(gvr.FindControl("rmtFromTime1"), RadMaskedTextBox).Text)) = "0000" Then
                    CtlCommon.ShowMessage(Me, ResourceManager.GetString("FrmTime1CantLeft", CultureInfo) + " " + CStr(CType(gvr.FindControl("lblDays"), Label).Text), "info")
                    Exit Sub
                End If
                If (CStr(CType(gvr.FindControl("rmtFromTime2"), RadMaskedTextBox).Text)) = "0000" Then
                    CtlCommon.ShowMessage(Me, ResourceManager.GetString("FrmTime2CantLeft", CultureInfo) + " " + CStr(CType(gvr.FindControl("lblDays"), Label).Text), "info")
                    Exit Sub
                End If

            End If
        Next
        objWorkSchedule = New WorkSchedule
        Dim err As Integer
        With objWorkSchedule
            .ScheduleType = 2
            .ScheduleName = txtScheduleEng.Text
            .ScheduleArabicName = txtScheduleAr.Text
            If (trGraceIn.Visible) Then
                .GraceIn = txtGraceIn.Text
            End If
            If (trGraceOut.Visible) Then
                .GraceOut = txtGraceOut.Text
            End If

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

            objWorkSchedule_Flexible = New WorkSchedule_Flexible
            objWorkSchedule_Flexible.FK_ScheduleId = ScheduleID

            Dim tmpDt As New DataTable("DT")

            tmpDt.Columns.Add("FK_ScheduleId") : tmpDt.Columns.Add("DayId") : tmpDt.Columns.Add("FromTime1") : tmpDt.Columns.Add("FromTime2") : tmpDt.Columns.Add("Duration1") : tmpDt.Columns.Add("FromTime3") : tmpDt.Columns.Add("FromTime4") : tmpDt.Columns.Add("Duration2") : tmpDt.Columns.Add("IsOffDay")

            For i As Integer = 0 To dgrdWeekTimeShift1.Items.Count - 1
                Dim gvr As GridDataItem = dgrdWeekTimeShift1.Items(i)
                Dim TmpROw As DataRow = tmpDt.NewRow

                TmpROw("FromTime1") = CStr(CType(gvr.FindControl("rmtFromTime1"), RadMaskedTextBox).Text)
                TmpROw("FromTime2") = CStr(CType(gvr.FindControl("rmtFromTime2"), RadMaskedTextBox).Text)
                Dim strDuration As String = CStr(CType(gvr.FindControl("rmtDuration1"), RadMaskedTextBox).Text)
                TmpROw("Duration1") = (Val(strDuration.Substring(0, 2)) * 60) + Val(strDuration.Substring(2, 2))

                TmpROw("FromTime3") = CStr(CType(gvr.FindControl("rmtFromTime3"), RadMaskedTextBox).Text)
                TmpROw("FromTime4") = CStr(CType(gvr.FindControl("rmtFromTime4"), RadMaskedTextBox).Text)
                strDuration = CStr(CType(gvr.FindControl("rmtDuration2"), RadMaskedTextBox).Text)
                TmpROw("Duration2") = (Val(strDuration.Substring(0, 2)) * 60) + Val(strDuration.Substring(2, 2))
                TmpROw("IsOffDay") = CType(gvr.FindControl("Shift2offDay"), CheckBox).Checked
                TmpROw("FK_ScheduleId") = ScheduleID
                TmpROw("DayId") = CInt(gvr.GetDataKeyValue("Dayid"))
                tmpDt.Rows.Add(TmpROw)
            Next
            err = objWorkSchedule_Flexible.Add(tmpDt, ScheduleID)
        End If

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGrid()
            ClearAll()
            clear_DataGrid_Controls()
        ElseIf err = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
        ElseIf err = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub ibtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnDelete.Click
        Dim err As Integer = 0
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdWorkSchedule.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then

                Dim intScheduleID As Integer = Convert.ToInt32(row.GetDataKeyValue("ScheduleId"))
                objWorkSchedule = New WorkSchedule
                objWorkSchedule.ScheduleId = intScheduleID
                err += objWorkSchedule.Delete()

            End If
        Next
        If err = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("CantDelete", CultureInfo), "error")
        End If
        FillGrid()
        ClearAll()
        clear_DataGrid_Controls()
    End Sub

    Protected Sub ibtnRest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnRest.Click
        ClearAll()
        clear_DataGrid_Controls()

    End Sub

    ' ************ COMMENTED BY ARUN ON 16/07/2012 *****************************
    ' for converting this to java Script



    'Protected Sub rmtFromTime1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim rmtFromTime1 As RadMaskedTextBox = DirectCast(sender, RadMaskedTextBox)
    '    If DirectCast(DirectCast(DirectCast(sender, RadMaskedTextBox).Parent.Parent, GridDataItem).FindControl("chkOffDays"), CheckBox).Checked = True Then
    '        rmtFromTime1.TextWithLiterals = "00:00"
    '    End If
    '    rmtFromTime1.TextWithLiterals = time_range_validate(rmtFromTime1.TextWithLiterals)
    'End Sub
    'Protected Sub rmtFromTime2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim rmtFromTime2 As RadMaskedTextBox = DirectCast(sender, RadMaskedTextBox)
    '    If DirectCast(DirectCast(DirectCast(sender, RadMaskedTextBox).Parent.Parent, GridDataItem).FindControl("chkOffDays"), CheckBox).Checked = True Then
    '        rmtFromTime2.TextWithLiterals = "00:00"
    '    End If
    '    rmtFromTime2.TextWithLiterals = time_range_validate(rmtFromTime2.TextWithLiterals)
    'End Sub
    'Protected Sub rmtToTime1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim rmtToTime1 As RadMaskedTextBox = DirectCast(sender, RadMaskedTextBox)
    '    If DirectCast(DirectCast(DirectCast(sender, RadMaskedTextBox).Parent.Parent, GridDataItem).FindControl("chkOffDays"), CheckBox).Checked = True Then
    '        rmtToTime1.TextWithLiterals = "00:00"
    '    End If
    '    rmtToTime1.TextWithLiterals = time_range_validate(rmtToTime1.TextWithLiterals)
    'End Sub
    'Protected Sub rmtToTime2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim rmtToTime2 As RadMaskedTextBox = DirectCast(sender, RadMaskedTextBox)
    '    If DirectCast(DirectCast(DirectCast(sender, RadMaskedTextBox).Parent.Parent, GridDataItem).FindControl("chkOffDays"), CheckBox).Checked = True Then
    '        rmtToTime2.TextWithLiterals = "00:00"
    '    End If
    '    rmtToTime2.TextWithLiterals = time_range_validate(rmtToTime2.TextWithLiterals)
    'End Sub

    '*******************************************************

    Protected Sub dgrdWorkSchedule_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdWorkSchedule.NeedDataSource
        objWorkSchedule = New WorkSchedule
        dgrdWorkSchedule.DataSource = objWorkSchedule.GetAllFlexible()
    End Sub

    Protected Sub dgrdWorkSchedule_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdWorkSchedule.SelectedIndexChanged
        ScheduleID = CInt(CType(dgrdWorkSchedule.SelectedItems(0), GridDataItem).GetDataKeyValue("ScheduleId"))
        objWorkSchedule = New WorkSchedule
        With objWorkSchedule
            .ScheduleId = ScheduleID
            .GetByPK()
            txtGraceIn.Text = .GraceIn
            txtScheduleEng.Text = .ScheduleName
            txtScheduleAr.Text = .ScheduleArabicName
            txtGraceOut.Text = .GraceOut

        End With
        FillWeekDays()

    End Sub

    Protected Sub dgrdWeekTimeShift1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdWeekTimeShift1.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim Itm As GridDataItem = e.Item
                Dim FromTime1 = DirectCast(Itm.FindControl("rmtFromTime1"), RadMaskedTextBox)
                Dim ToTime1 = DirectCast(Itm.FindControl("rmtToTime1"), RadMaskedTextBox)
                Dim FromTime2 = DirectCast(Itm.FindControl("rmtFromTime2"), RadMaskedTextBox)
                Dim ToTime2 = DirectCast(Itm.FindControl("rmtToTime2"), RadMaskedTextBox)
                Dim rmtDuration1 = DirectCast(Itm.FindControl("rmtDuration1"), RadMaskedTextBox)
                Dim chkIsoffDay = DirectCast(Itm.FindControl("Shift2offDay"), CheckBox)
                Dim tmpSTr As String = "'" & FromTime1.ClientID & "','" & ToTime1.ClientID & "','" & FromTime2.ClientID & "','" & ToTime2.ClientID & "','" & rmtDuration1.ClientID & "','" & chkIsoffDay.ClientID & "'"
                Dim enDay = DirectCast(Itm.FindControl("hdnEnDay"), HiddenField)
                Dim arDay = DirectCast(Itm.FindControl("hdnArDay"), HiddenField)
                Dim lblDay = DirectCast(Itm.FindControl("lblDays"), Label)
                If SessionVariables.CultureInfo = "ar-JO" Then
                    lblDay.Text = arDay.Value
                Else
                    lblDay.Text = enDay.Value
                End If

                FromTime1.Attributes.Add("onblur", "ValidateGridRowTest(" & tmpSTr & ");")
                ToTime1.Attributes.Add("onblur", "ValidateGridRowTest(" & tmpSTr & ");")
                FromTime2.Attributes.Add("onblur", "ValidateGridRowTest(" & tmpSTr & ");")
                ToTime2.Attributes.Add("onblur", "ValidateGridRowTest(" & tmpSTr & ");")
                rmtDuration1.Attributes.Add("onblur", "ValidateGridRowTest(" & tmpSTr & ");")
                Dim tmpstr2 As String = "'" & FromTime1.ClientID & "','" & ToTime1.ClientID & "','" & FromTime2.ClientID & "','" & ToTime2.ClientID & "','" & rmtDuration1.ClientID & "',"

                Try
                    ToTime1.Text = e.Item.DataItem("ToTime1")
                    ToTime2.Text = e.Item.DataItem("ToTime2")

                Catch ex As Exception

                End Try






                FromTime1 = DirectCast(Itm.FindControl("rmtFromTime3"), RadMaskedTextBox)
                ToTime1 = DirectCast(Itm.FindControl("rmtToTime3"), RadMaskedTextBox)
                FromTime2 = DirectCast(Itm.FindControl("rmtFromTime4"), RadMaskedTextBox)
                ToTime2 = DirectCast(Itm.FindControl("rmtToTime4"), RadMaskedTextBox)
                rmtDuration1 = DirectCast(Itm.FindControl("rmtDuration2"), RadMaskedTextBox)
                chkIsoffDay = DirectCast(Itm.FindControl("Shift2offDay"), CheckBox)
                tmpSTr = "'" & FromTime1.ClientID & "','" & ToTime1.ClientID & "','" & FromTime2.ClientID & "','" & ToTime2.ClientID & "','" & rmtDuration1.ClientID & "','" & chkIsoffDay.ClientID & "'"

                FromTime1.Attributes.Add("onblur", "ValidateGridRowTest(" & tmpSTr & ");")
                ToTime1.Attributes.Add("onblur", "ValidateGridRowTest(" & tmpSTr & ");")
                FromTime2.Attributes.Add("onblur", "ValidateGridRowTest(" & tmpSTr & ");")
                ToTime2.Attributes.Add("onblur", "ValidateGridRowTest(" & tmpSTr & ");")
                rmtDuration1.Attributes.Add("onblur", "ValidateGridRowTest(" & tmpSTr & ");")
                chkIsoffDay.Attributes.Add("onclick", "x(" & tmpstr2 & tmpSTr & ");")

                Try
                    ToTime1.Text = e.Item.DataItem("ToTime3")
                    ToTime2.Text = e.Item.DataItem("ToTime4")
                    chkIsoffDay.Checked = e.Item.DataItem("IsOffDay")
                Catch ex As Exception
                End Try




            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dgrdWeekTime_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdWeekTimeShift1.NeedDataSource
        objWorkSchedule_Flexible = New WorkSchedule_Flexible
        objWorkSchedule_Flexible.FK_ScheduleId = ScheduleID
        dgrdWeekTimeShift1.DataSource = objWorkSchedule_Flexible.GetAll()
    End Sub

#End Region

#Region "Methods"

    'Sub CreateDTWeekDays()
    '    DtWeeks = New DataTable
    '    Dim dc As DataColumn
    '    dc = New DataColumn
    '    dc.ColumnName = "DayId"
    '    DtWeeks.Columns.Add(dc)
    '    dc = New DataColumn
    '    dc.ColumnName = "Days"
    '    DtWeeks.Columns.Add(dc)
    '    dc = New DataColumn
    '    dc.ColumnName = "FromTime1"
    '    DtWeeks.Columns.Add(dc)
    '    dc = New DataColumn
    '    dc.ColumnName = "ToTime1"
    '    DtWeeks.Columns.Add(dc)
    '    dc = New DataColumn
    '    dc.ColumnName = "FromTime2"
    '    DtWeeks.Columns.Add(dc)
    '    dc = New DataColumn
    '    dc.ColumnName = "ToTime2"
    '    DtWeeks.Columns.Add(dc)
    '    'dc = New DataColumn
    '    'dc.ColumnName = "IsOffDay"
    '    'DtWeeks.Columns.Add(dc)
    'End Sub

    Public Sub FillGrid()
        Try
            Dim dt As DataTable
            objWorkSchedule = New WorkSchedule
            dt = objWorkSchedule.GetAllFlexible()
            dgrdWorkSchedule.DataSource = dt
            dgrdWorkSchedule.DataBind()

            If (trGraceIn.Visible = False And trGraceOut.Visible = False) Then
                dgrdWorkSchedule.Columns(4).Visible = False
                dgrdWorkSchedule.Columns(5).Visible = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub FillWeekDays()
        Try
            objWorkSchedule_Flexible = New WorkSchedule_Flexible
            objWorkSchedule_Flexible.FK_ScheduleId = ScheduleID
            dgrdWeekTimeShift1.DataSource = objWorkSchedule_Flexible.GetAll()
            dgrdWeekTimeShift1.DataBind()


        Catch ex As Exception
        End Try
    End Sub

    Public Sub ClearAll()
        txtGraceIn.Text = String.Empty
        txtGraceOut.Text = String.Empty
        txtScheduleAr.Text = String.Empty
        txtScheduleEng.Text = String.Empty

        ScheduleID = 0
        DayID = 0
        dgrdWorkSchedule.SelectedIndexes.Clear()

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

    Public Sub clear_DataGrid_Controls()
        For Each gvr As GridDataItem In dgrdWeekTimeShift1.Items
            CType(gvr.FindControl("rmtFromTime1"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtFromTime2"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtToTime1"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtToTime2"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtDuration1"), RadMaskedTextBox).TextWithLiterals = "0000"

            CType(gvr.FindControl("rmtFromTime3"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtFromTime4"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtToTime3"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtToTime4"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtDuration2"), RadMaskedTextBox).TextWithLiterals = "0000"

            CType(gvr.FindControl("Shift2offDay"), CheckBox).Checked = False
        Next


        'CtlCommon.FillRadGridView(dgrdWeekTime, objWeekDays.GetAll())
    End Sub

#End Region

End Class
