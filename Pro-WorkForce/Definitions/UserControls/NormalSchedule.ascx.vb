Imports System.Data
Imports SmartV.UTILITIES.CtlCommon
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports System.Resources
Imports System.Globalization
Imports TA.Definitions
Imports TA.Admin
Imports System.Web.UI.Control
Imports System.Web.UI.Page

Partial Class Definitions_UserControls_NormalSchedule
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private objWeekDays As New WeekDays
    Public objWorkSchedule_NormalTime As WorkSchedule_NormalTime
    Private objWorkSchedule As WorkSchedule
    Private objApp_Settings As APP_Settings
    Private objWorkSchedule_Flexible As WorkSchedule_Flexible
    Private objWorkSchedule_Open As WorkSchedule_Open
    Dim CultureInfo As System.Globalization.CultureInfo
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Public Shared FromTimeBuilder1 As New StringBuilder
    Public Shared FromTimeBuilder2 As New StringBuilder
    Public Shared ToTimeBuilder1 As New StringBuilder
    Public Shared ToTimeBuilder2 As New StringBuilder
    Public Shared flexBuilder1 As New StringBuilder
    Public Shared flexBuilder2 As New StringBuilder
    Public Shared DurationBuilder1 As New StringBuilder
    Public Shared DurationBuilder2 As New StringBuilder
    Public Shared CheckIsOffDayCtl As New StringBuilder
    Public Shared WorkHoursBuilder As New StringBuilder

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

    Public Property IsGraceTAPolicy() As Boolean
        Get
            Return ViewState("IsGraceTAPolicy")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsGraceTAPolicy") = value
        End Set
    End Property

    Public Property ShowAddRamadanSchedule() As Boolean
        Get
            Return btnAddRamadan.Visible
        End Get
        Set(ByVal value As Boolean)
            btnAddRamadan.Visible = value
        End Set
    End Property

    Public Property CheckIsDefault() As Boolean
        Get
            Return ChkIsDefault.Checked
        End Get
        Set(ByVal value As Boolean)
            ChkIsDefault.Checked = value
        End Set
    End Property

    Public Property RamadanScheduleID() As Integer
        Get
            Return ViewState("RamadanScheduleID")
        End Get
        Set(ByVal value As Integer)
            ViewState("RamadanScheduleID") = value
        End Set
    End Property

    Public Property EnabledDisabeldScheduleType() As Boolean
        Get
            Return RBLScheduleType.Enabled
        End Get
        Set(value As Boolean)
            RBLScheduleType.Enabled = value
        End Set
    End Property



#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If SessionVariables.CultureInfo = "ar-JO" Then
            lblShift1.Text = "مناوبة 2"
            lblShift2.Text = "مناوبة 1"
        End If
        If Not Page.IsPostBack Then
            FromTimeBuilder1 = New StringBuilder
            FromTimeBuilder2 = New StringBuilder
            ToTimeBuilder1 = New StringBuilder
            ToTimeBuilder2 = New StringBuilder
            flexBuilder1 = New StringBuilder
            flexBuilder2 = New StringBuilder
            WorkHoursBuilder = New StringBuilder
            CtlCommon.FillRadGridView(dgrdWeekTime, objWeekDays.GetAll())
            CtlCommon.FillRadGridView(dgrdRamadanTime, objWeekDays.GetAll())
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "FillText", "FillAllGridText('" + FromTimeBuilder1.ToString() + "');", True)
            clear_DataGrid_Controls()
            objApp_Settings = New APP_Settings()
            objApp_Settings.GetByPK()
            If objApp_Settings.IsGraceTAPolicy Then
                trGraceIn.Visible = False
                trGraceOut.Visible = False
            End If
            ShowHide()
        End If

    End Sub

    Protected Sub dgrdWeekTime_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdWeekTime.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim Itm As GridDataItem = e.Item
                Dim FromTime1 = DirectCast(Itm.FindControl("rmtFromTime1"), RadMaskedTextBox)
                Dim ToTime1 = DirectCast(Itm.FindControl("rmtToTime1"), RadMaskedTextBox)
                Dim FromTime2 = DirectCast(Itm.FindControl("rmtFromTime2"), RadMaskedTextBox)
                Dim ToTime2 = DirectCast(Itm.FindControl("rmtToTime2"), RadMaskedTextBox)
                Dim rmtDuration1 = DirectCast(Itm.FindControl("rmtDuration1"), RadMaskedTextBox)
                Dim rmtDuration2 = DirectCast(Itm.FindControl("rmtDuration2"), RadMaskedTextBox)
                Dim chkOffDays = DirectCast(Itm.FindControl("chkOffDays"), CheckBox)

                Dim flex1 = DirectCast(Itm.FindControl("Flex1"), RadMaskedTextBox)
                Dim flex2 = DirectCast(Itm.FindControl("Flex2"), RadMaskedTextBox)

                Dim enDay = DirectCast(Itm.FindControl("hdnEnDay"), HiddenField)
                Dim arDay = DirectCast(Itm.FindControl("hdnArDay"), HiddenField)
                Dim lblDay = DirectCast(Itm.FindControl("lblDays"), Label)
                Dim WorkHrs = DirectCast(Itm.FindControl("radWorkHrs"), RadMaskedTextBox)

                If SessionVariables.CultureInfo = "ar-JO" Then
                    lblDay.Text = arDay.Value
                Else
                    lblDay.Text = enDay.Value
                End If

                Dim tmpSTr As String = "'" & FromTime1.ClientID & "','" & _
                                            ToTime1.ClientID & "','" & _
                                            FromTime2.ClientID & "','" & _
                                            ToTime2.ClientID & "'," & _
                                            chkOffDays.ClientID & ",'" & _
                                            rmtDuration1.ClientID & "','" & _
                                            rmtDuration2.ClientID & "','" & _
                                            flex1.ClientID & "','" & _
                                            flex2.ClientID & "','" & _
                                            WorkHrs.ClientID & "'"

                chkOffDays.Attributes.Add("onclick", "ValidateGridRow(" & tmpSTr & ");")
                FromTime1.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")
                FromTime2.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")
                ToTime1.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")
                ToTime2.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")
                rmtDuration1.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")
                rmtDuration2.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")

                flex1.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")
                flex2.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")

                WorkHrs.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")

                FromTimeBuilder1.AppendFormat("{0} ,", FromTime1.ClientID)
                FromTimeBuilder2.AppendFormat("{0} ,", FromTime2.ClientID)
                ToTimeBuilder1.AppendFormat("{0} ,", ToTime1.ClientID)
                ToTimeBuilder2.AppendFormat("{0} ,", ToTime2.ClientID)

                flexBuilder1.AppendFormat("{0} ,", flex1.ClientID)
                flexBuilder2.AppendFormat("{0} ,", flex2.ClientID)

                DurationBuilder1.AppendFormat("{0} ,", rmtDuration1.ClientID)
                DurationBuilder2.AppendFormat("{0} ,", rmtDuration2.ClientID)

                WorkHoursBuilder.AppendFormat("{0} ,", WorkHrs.ClientID)

                Dim check As CheckBox = DirectCast(Itm.FindControl("chkOffDays"), CheckBox)
                If check.Checked Then
                    CheckIsOffDayCtl.AppendFormat("{0} ,", FromTime1.ClientID, "{1} ,", ToTime1.ClientID, "{2} ,", FromTime2.ClientID, "{3} ,", ToTime2.ClientID, _
                                                  "{4} ,", flex1.ClientID, "{5} ,", flex2.ClientID, "{6} ,", rmtDuration1.ClientID, "{7} ,", rmtDuration2.ClientID, _
                                                  "{8} ,", WorkHrs.ClientID)

                    hdnCheckIsOffDays.Value = CheckIsOffDayCtl.ToString()
                End If

                hdnFromTime1.Value = FromTimeBuilder1.ToString()
                hdnFromTime2.Value = FromTimeBuilder2.ToString()
                hdnToTime1.Value = ToTimeBuilder1.ToString()
                hdnToTime2.Value = ToTimeBuilder2.ToString()

                hdnflex1.Value = flexBuilder1.ToString()
                hdnflex2.Value = flexBuilder2.ToString()

                hdnDuration1.Value = DurationBuilder1.ToString()
                hdnDuration2.Value = DurationBuilder2.ToString()

                hdnWorkHrs.Value = WorkHoursBuilder.ToString()

                Try
                    Dim dateFromTime1 = e.Item.DataItem("FromTime1")
                    Dim dateTotime1 = e.Item.DataItem("ToTime1")

                    If Not dateFromTime1.ToString().Contains(":") Then
                        dateFromTime1 = dateFromTime1.ToString().Insert(2, ":")
                    End If

                    If Not dateTotime1.ToString().Contains(":") Then
                        dateTotime1 = dateTotime1.ToString().Insert(2, ":")
                    End If

                    Dim fromTime() = dateFromTime1.ToString().Split(":")
                    Dim ToTime() = dateTotime1.ToString().Split(":")
                    Dim TimeInSec As Integer = ((Val(ToTime(0)) * 3600) + (Val(ToTime(1)) * 60)) - ((Val(fromTime(0)) * 3600) + (Val(fromTime(1)) * 60))
                    If TimeInSec < 0 Then
                        TimeInSec = 86400 + TimeInSec

                    End If

                    Dim h As Integer = Math.Truncate(TimeInSec / 3600)
                    TimeInSec = (TimeInSec Mod 3600)
                    rmtDuration1.Text = h.ToString("00") & (TimeInSec / 60).ToString("00")


                    dateFromTime1 = e.Item.DataItem("FromTime2")
                    dateTotime1 = e.Item.DataItem("ToTime2")
                    fromTime = dateFromTime1.ToString().Split(":")
                    ToTime = dateTotime1.ToString().Split(":")
                    TimeInSec = ((Val(ToTime(0)) * 3600) + (Val(ToTime(1)) * 60)) - ((Val(fromTime(0)) * 3600) + (Val(fromTime(1)) * 60))
                    h = Math.Truncate(TimeInSec / 3600)

                    TimeInSec = (TimeInSec Mod 3600)
                    rmtDuration2.Text = h.ToString("00") & (TimeInSec / 60).ToString("00")

                    flex1.Text = CtlCommon.GetFullTimeString(flex1.Text)
                    flex2.Text = CtlCommon.GetFullTimeString(flex2.Text)
                    'WorkHrs.Text = CtlCommon.GetFullTimeString(WorkHrs.Text)
                Catch ex As Exception

                End Try

            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dgrdRamadanTime_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdRamadanTime.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim Itm As GridDataItem = e.Item
                Dim FromTime1 = DirectCast(Itm.FindControl("rmtFromTime1"), RadMaskedTextBox)
                Dim ToTime1 = DirectCast(Itm.FindControl("rmtToTime1"), RadMaskedTextBox)
                Dim FromTime2 = DirectCast(Itm.FindControl("rmtFromTime2"), RadMaskedTextBox)
                Dim ToTime2 = DirectCast(Itm.FindControl("rmtToTime2"), RadMaskedTextBox)
                Dim rmtDuration1 = DirectCast(Itm.FindControl("rmtDuration1"), RadMaskedTextBox)
                Dim rmtDuration2 = DirectCast(Itm.FindControl("rmtDuration2"), RadMaskedTextBox)
                Dim chkOffDays = DirectCast(Itm.FindControl("chkOffDays"), CheckBox)

                Dim flex1 = DirectCast(Itm.FindControl("flex1"), RadMaskedTextBox)
                Dim flex2 = DirectCast(Itm.FindControl("flex2"), RadMaskedTextBox)

                Dim enDay = DirectCast(Itm.FindControl("hdnEnDay"), HiddenField)
                Dim arDay = DirectCast(Itm.FindControl("hdnArDay"), HiddenField)
                Dim lblDay = DirectCast(Itm.FindControl("lblDays"), Label)

                Dim WorkHrs = DirectCast(Itm.FindControl("radWorkHrs"), RadMaskedTextBox)

                If SessionVariables.CultureInfo = "ar-JO" Then
                    lblDay.Text = arDay.Value
                Else
                    lblDay.Text = enDay.Value
                End If

                Dim tmpSTr As String = "'" & FromTime1.ClientID & "','" & _
                                            ToTime1.ClientID & "','" & _
                                            FromTime2.ClientID & "','" & _
                                            ToTime2.ClientID & "'," & _
                                            chkOffDays.ClientID & ",'" & _
                                            rmtDuration1.ClientID & "','" & _
                                            rmtDuration2.ClientID & "','" & _
                                            flex1.ClientID & "','" & _
                                            flex2.ClientID & "','" & _
                                            WorkHrs.ClientID & "'"


                chkOffDays.Attributes.Add("onclick", "ValidateGridRow(" & tmpSTr & ");")
                FromTime1.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")
                FromTime2.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")
                ToTime1.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")
                ToTime2.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")
                rmtDuration1.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")
                rmtDuration2.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")

                flex1.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")
                flex2.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")

                WorkHrs.Attributes.Add("onblur", "ValidateTextbox(" & tmpSTr & ");")



                FromTimeBuilder1.AppendFormat("{0} ,", FromTime1.ClientID)
                FromTimeBuilder2.AppendFormat("{0} ,", FromTime2.ClientID)
                ToTimeBuilder1.AppendFormat("{0} ,", ToTime1.ClientID)
                ToTimeBuilder2.AppendFormat("{0} ,", ToTime2.ClientID)

                flexBuilder1.AppendFormat("{0} ,", flex1.ClientID)
                flexBuilder2.AppendFormat("{0} ,", flex2.ClientID)

                DurationBuilder1.AppendFormat("{0} ,", rmtDuration1.ClientID)
                DurationBuilder2.AppendFormat("{0} ,", rmtDuration2.ClientID)

                WorkHoursBuilder.AppendFormat("{0} ,", WorkHrs.ClientID)

                hdnFromTime1.Value = FromTimeBuilder1.ToString()
                hdnFromTime2.Value = FromTimeBuilder2.ToString()
                hdnToTime1.Value = ToTimeBuilder1.ToString()
                hdnToTime2.Value = ToTimeBuilder2.ToString()

                hdnflex1.Value = flexBuilder1.ToString()
                hdnflex2.Value = flexBuilder2.ToString()

                hdnDuration1.Value = DurationBuilder1.ToString()
                hdnDuration2.Value = DurationBuilder2.ToString()
                hdnWorkHrs.Value = WorkHoursBuilder.ToString()
                Try
                    Dim dateFromTime1 = e.Item.DataItem("FromTime1")
                    Dim dateTotime1 = e.Item.DataItem("ToTime1")

                    If Not dateFromTime1.ToString().Contains(":") Then
                        dateFromTime1 = dateFromTime1.ToString().Insert(2, ":")
                    End If

                    If Not dateTotime1.ToString().Contains(":") Then
                        dateTotime1 = dateTotime1.ToString().Insert(2, ":")
                    End If

                    Dim fromTime() = dateFromTime1.ToString().Split(":")
                    Dim ToTime() = dateTotime1.ToString().Split(":")
                    Dim TimeInSec As Integer = ((Val(ToTime(0)) * 3600) + (Val(ToTime(1)) * 60)) - ((Val(fromTime(0)) * 3600) + (Val(fromTime(1)) * 60))
                    If TimeInSec < 0 Then
                        TimeInSec = 86400 + TimeInSec

                    End If

                    Dim h As Integer = Math.Truncate(TimeInSec / 3600)
                    TimeInSec = (TimeInSec Mod 3600)

                    rmtDuration1.Text = h.ToString("00") & (TimeInSec / 60).ToString("00")


                    dateFromTime1 = e.Item.DataItem("FromTime2")
                    dateTotime1 = e.Item.DataItem("ToTime2")
                    fromTime = dateFromTime1.ToString().Split(":")
                    ToTime = dateTotime1.ToString().Split(":")
                    TimeInSec = ((Val(ToTime(0)) * 3600) + (Val(ToTime(1)) * 60)) - ((Val(fromTime(0)) * 3600) + (Val(fromTime(1)) * 60))
                    h = Math.Truncate(TimeInSec / 3600)

                    TimeInSec = (TimeInSec Mod 3600)
                    rmtDuration2.Text = h.ToString("00") & (TimeInSec / 60).ToString("00")

                    flex1.Text = CtlCommon.GetFullTimeString(flex1.Text)
                    flex2.Text = CtlCommon.GetFullTimeString(flex2.Text)
                    'WorkHrs.Text = CtlCommon.GetFullTimeString(WorkHrs.Text)
                Catch ex As Exception

                End Try

            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dgrdWeekTime_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdWeekTime.NeedDataSource
        objWorkSchedule_NormalTime = New WorkSchedule_NormalTime
        objWorkSchedule_NormalTime.FK_ScheduleId = ScheduleID
        dgrdWeekTime.DataSource = objWorkSchedule_NormalTime.GetAll()
    End Sub

    Protected Sub btnAddRamadan_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddRamadan.Click

        mpeRamadanPopup.Show()
    End Sub

    Protected Sub btnSaveRamadan_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveRamadan.Click
        SaveRamadanSchedule()
    End Sub

    Protected Sub dgrdramadantime_selectedindexchanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdRamadanTime.SelectedIndexChanged
        mpeRamadanPopup.Show()
    End Sub

    Protected Sub RBLScheduleType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBLScheduleType.SelectedIndexChanged
        Try
            CtlCommon.FillRadGridView(dgrdWeekTime, objWeekDays.GetAll())
            CtlCommon.FillRadGridView(dgrdRamadanTime, objWeekDays.GetAll())

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "FillText", "FillAllGridText('" + FromTimeBuilder1.ToString() + "');", True)
            clear_DataGrid_Controls()

            'If RBLScheduleType.SelectedValue = "N" Then
            objApp_Settings = New APP_Settings()
            objApp_Settings.GetByPK()
            If objApp_Settings.IsGraceTAPolicy Then
                trGraceIn.Visible = False
                trGraceOut.Visible = False
                trRamadanGraceIn.Visible = False
                trRamadanGraceOut.Visible = False
            Else
                trGraceIn.Visible = True
                trGraceOut.Visible = True
                trRamadanGraceIn.Visible = True
                trRamadanGraceOut.Visible = True
            End If
            If RBLScheduleType.SelectedValue = "O" Then
                trGraceIn.Visible = False
                trGraceOut.Visible = False
                trRamadanGraceIn.Visible = False
                trRamadanGraceOut.Visible = False
            End If
            'ElseIf RBLScheduleType.SelectedValue = "F" Then
            '    trGraceIn.Visible = False
            '    trGraceOut.Visible = False
            'End If

            ShowHide()
        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Page Methods"

    Public Sub FillWeekDays()
        Try
            objWorkSchedule_NormalTime = New WorkSchedule_NormalTime
            objWorkSchedule_Flexible = New WorkSchedule_Flexible
            objWorkSchedule_Open = New WorkSchedule_Open

            If RBLScheduleType.SelectedValue = "N" Then

                objWorkSchedule_NormalTime.FK_ScheduleId = ScheduleID
                dgrdWeekTime.DataSource = objWorkSchedule_NormalTime.GetAll()
                dgrdWeekTime.DataBind()

            ElseIf RBLScheduleType.SelectedValue = "F" Then
                objWorkSchedule_Flexible.FK_ScheduleId = ScheduleID
                dgrdWeekTime.DataSource = objWorkSchedule_Flexible.GetAll
                dgrdWeekTime.DataBind()
            ElseIf RBLScheduleType.SelectedValue = "O" Then
                objWorkSchedule_Open.FK_ScheduleId = ScheduleID
                dgrdWeekTime.DataSource = objWorkSchedule_Open.GetAll
                dgrdWeekTime.DataBind()
            End If

        Catch ex As Exception
        End Try
    End Sub

    Public Sub FillRamadanWeekDays(ByVal RamScheduleID As Integer)
        Try
            objWorkSchedule_NormalTime = New WorkSchedule_NormalTime
            objWorkSchedule_Flexible = New WorkSchedule_Flexible
            objWorkSchedule_Open = New WorkSchedule_Open

            If RBLScheduleType.SelectedValue = "N" Then

                objWorkSchedule_NormalTime.FK_ScheduleId = RamScheduleID
                dgrdRamadanTime.DataSource = objWorkSchedule_NormalTime.GetAll()
                dgrdRamadanTime.DataBind()

            ElseIf RBLScheduleType.SelectedValue = "F" Then
                objWorkSchedule_Flexible.FK_ScheduleId = RamScheduleID
                dgrdRamadanTime.DataSource = objWorkSchedule_Flexible.GetAll
                dgrdRamadanTime.DataBind()
            ElseIf RBLScheduleType.SelectedValue = "O" Then
                objWorkSchedule_Open.FK_ScheduleId = RamScheduleID
                dgrdRamadanTime.DataSource = objWorkSchedule_Open.GetAll
                dgrdRamadanTime.DataBind()
            End If

            If RamadanScheduleID = 0 Then
                clear_RamadanDataGrid_Controls()
            End If

        Catch ex As Exception
        End Try
    End Sub

    Public Sub CreateDTWeekDays()
        DtWeeks = New DataTable
        Dim dc As DataColumn
        dc = New DataColumn
        dc.ColumnName = "DayId"
        DtWeeks.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "Days"
        DtWeeks.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "FromTime1"
        DtWeeks.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "ToTime1"
        DtWeeks.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "FromTime2"
        DtWeeks.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "ToTime2"
        DtWeeks.Columns.Add(dc)
        If RBLScheduleType.SelectedValue = "F" Then
            dc = New DataColumn
            dc.ColumnName = "Flex1"
            DtWeeks.Columns.Add(dc)
            dc = New DataColumn
            dc.ColumnName = "Flex2"
            DtWeeks.Columns.Add(dc)
        End If
        If RBLScheduleType.SelectedValue = "O" Then
            dc = New DataColumn
            dc.ColumnName = "WorkHrs"
            DtWeeks.Columns.Add(dc)
        End If

        dc = New DataColumn
        dc.ColumnName = "IsOffDay"
        DtWeeks.Columns.Add(dc)
    End Sub

    Public Sub ClearAll()
        txtGraceIn.Text = String.Empty
        txtGraceOut.Text = String.Empty
        txtRamadanGraceIn.Text = String.Empty
        txtRamadanGraceOut.Text = String.Empty
        txtScheduleAr.Text = String.Empty
        txtScheduleEng.Text = String.Empty
        RBLScheduleType.SelectedValue = "N"
        dgrdWeekTime.Columns(5).Visible = False
        dgrdWeekTime.Columns(9).Visible = False
        dgrdRamadanTime.Columns(5).Visible = False
        dgrdRamadanTime.Columns(9).Visible = False
        ScheduleID = 0
        DayID = 0
        RamadanScheduleID = 0
        ShowAddRamadanSchedule = False
        btnAddRamadan.Text = ResourceManager.GetString("AddRamadan", CultureInfo)
        ChkIsDefault.Checked = False
        clear_DataGrid_Controls()
        clear_RamadanDataGrid_Controls()
        EnabledDisabeldScheduleType = True
        'rblGraceInGender.SelectedValue = "a"
        'rblGraceOutGender.SelectedValue = "a"
        CtlCommon.FillRadGridView(dgrdWeekTime, objWeekDays.GetAll())
        CtlCommon.FillRadGridView(dgrdRamadanTime, objWeekDays.GetAll())
          chkIsActive.Checked=False
        ShowHide()
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
        For Each gvr As GridDataItem In dgrdWeekTime.Items
            CType(gvr.FindControl("rmtFromTime1"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtFromTime2"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtToTime1"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtToTime2"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtDuration1"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtDuration2"), RadMaskedTextBox).TextWithLiterals = "0000"

            CType(gvr.FindControl("flex1"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("flex2"), RadMaskedTextBox).TextWithLiterals = "0000"

            CType(gvr.FindControl("radWorkHrs"), RadMaskedTextBox).TextWithLiterals = "0000"

            CType(gvr.FindControl("chkOffDays"), CheckBox).Checked = False
        Next
        For Each gvr As GridDataItem In dgrdRamadanTime.Items
            CType(gvr.FindControl("rmtFromTime1"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtFromTime2"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtToTime1"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtToTime2"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtDuration1"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtDuration2"), RadMaskedTextBox).TextWithLiterals = "0000"

            CType(gvr.FindControl("flex1"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("flex2"), RadMaskedTextBox).TextWithLiterals = "0000"

            CType(gvr.FindControl("radWorkHrs"), RadMaskedTextBox).TextWithLiterals = "0000"

            CType(gvr.FindControl("chkOffDays"), CheckBox).Checked = False
        Next
        'CtlCommon.FillRadGridView(dgrdWeekTime, objWeekDays.GetAll())
    End Sub

    Public Sub clear_RamadanDataGrid_Controls()
        For Each gvr As GridDataItem In dgrdRamadanTime.Items
            CType(gvr.FindControl("rmtFromTime1"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtFromTime2"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtToTime1"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtToTime2"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtDuration1"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("rmtDuration2"), RadMaskedTextBox).TextWithLiterals = "0000"

            CType(gvr.FindControl("flex1"), RadMaskedTextBox).TextWithLiterals = "0000"
            CType(gvr.FindControl("flex2"), RadMaskedTextBox).TextWithLiterals = "0000"

            CType(gvr.FindControl("radWorkHrs"), RadMaskedTextBox).TextWithLiterals = "0000"

            CType(gvr.FindControl("chkOffDays"), CheckBox).Checked = False
        Next
    End Sub

    Public Function filldata() As String

        objApp_Settings = New APP_Settings()
        objApp_Settings.GetByPK()
        If objApp_Settings.IsGraceTAPolicy Then
            trGraceIn.Visible = False
            trGraceOut.Visible = False
            trRamadanGraceIn.Visible = False
            trRamadanGraceOut.Visible = False
        Else
            trGraceIn.Visible = True
            trGraceOut.Visible = True
            trRamadanGraceIn.Visible = True
            trRamadanGraceOut.Visible = True
        End If

        objWorkSchedule = New WorkSchedule
        objWorkSchedule.ScheduleId = ScheduleID
        objWorkSchedule.GetByPK()
        With objWorkSchedule
            .ScheduleId = ScheduleID
            .GetByPK()
            txtGraceIn.Text = .GraceIn
            txtScheduleEng.Text = .ScheduleName
            txtScheduleAr.Text = .ScheduleArabicName
            txtGraceOut.Text = .GraceOut
            ChkIsDefault.Checked = .IsDefault
            chkIsActive.Checked = .IsActive

            If .GraceInGender = "" Then
                rblGraceInGender.SelectedValue = "a"
            Else
                rblGraceInGender.SelectedValue = .GraceInGender
            End If

            If .GraceOutGender = "" Then
                rblGraceOutGender.SelectedValue = "a"
            Else
                rblGraceOutGender.SelectedValue = .GraceOutGender
            End If


            If .ScheduleType = 1 Then
                RBLScheduleType.SelectedValue = "N"
                dvScheduleHeader.Visible = True
                dgrdWeekTime.Columns(3).Visible = True
                dgrdWeekTime.Columns(4).Visible = True
                dgrdWeekTime.Columns(6).Visible = True
                dgrdWeekTime.Columns(7).Visible = True
                dgrdWeekTime.Columns(8).Visible = True


                dgrdWeekTime.Columns(5).Visible = False
                dgrdWeekTime.Columns(9).Visible = False
                dgrdWeekTime.Columns(10).Visible = False

            ElseIf .ScheduleType = 2 Then
                RBLScheduleType.SelectedValue = "F"
                dvScheduleHeader.Visible = True

                dgrdWeekTime.Columns(3).Visible = True
                dgrdWeekTime.Columns(4).Visible = True
                dgrdWeekTime.Columns(6).Visible = True
                dgrdWeekTime.Columns(7).Visible = True
                dgrdWeekTime.Columns(8).Visible = True


                dgrdWeekTime.Columns(5).Visible = True
                dgrdWeekTime.Columns(9).Visible = True
                dgrdWeekTime.Columns(10).Visible = False

            ElseIf .ScheduleType = 5 Then
                RBLScheduleType.SelectedValue = "O"
                dvScheduleHeader.Visible = False

                dgrdWeekTime.Columns(2).Visible = False
                dgrdWeekTime.Columns(3).Visible = False
                dgrdWeekTime.Columns(4).Visible = False
                dgrdWeekTime.Columns(5).Visible = False
                dgrdWeekTime.Columns(6).Visible = False
                dgrdWeekTime.Columns(7).Visible = False
                dgrdWeekTime.Columns(8).Visible = False
                dgrdWeekTime.Columns(9).Visible = False
                dgrdWeekTime.Columns(10).Visible = True
            End If

            FillWeekDays()

        End With

        objWorkSchedule = New WorkSchedule
        With objWorkSchedule
            .ParentSchId = ScheduleID
            .GetByParentId()
            txtRamadanGraceIn.Text = .GraceIn
            txtRamadanGraceOut.Text = .GraceOut
            If .ScheduleType = 1 Then
                dvRamadanHeader.Visible = True

                dgrdRamadanTime.Columns(3).Visible = True
                dgrdRamadanTime.Columns(4).Visible = True
                dgrdRamadanTime.Columns(6).Visible = True
                dgrdRamadanTime.Columns(7).Visible = True
                dgrdRamadanTime.Columns(8).Visible = True


                dgrdRamadanTime.Columns(5).Visible = False
                dgrdRamadanTime.Columns(9).Visible = False
                dgrdRamadanTime.Columns(10).Visible = False

            ElseIf .ScheduleType = 2 Then
                dvRamadanHeader.Visible = True

                dgrdRamadanTime.Columns(3).Visible = True
                dgrdRamadanTime.Columns(4).Visible = True
                dgrdRamadanTime.Columns(6).Visible = True
                dgrdRamadanTime.Columns(7).Visible = True
                dgrdRamadanTime.Columns(8).Visible = True


                dgrdRamadanTime.Columns(5).Visible = True
                dgrdRamadanTime.Columns(9).Visible = True
                dgrdRamadanTime.Columns(10).Visible = False
            ElseIf .ScheduleType = 5 Then
                trRamadanGraceIn.Visible = False
                trRamadanGraceOut.Visible = False

                dvRamadanHeader.Visible = False

                dgrdRamadanTime.Columns(2).Visible = False
                dgrdRamadanTime.Columns(3).Visible = False
                dgrdRamadanTime.Columns(4).Visible = False
                dgrdRamadanTime.Columns(5).Visible = False
                dgrdRamadanTime.Columns(6).Visible = False
                dgrdRamadanTime.Columns(7).Visible = False
                dgrdRamadanTime.Columns(8).Visible = False
                dgrdRamadanTime.Columns(9).Visible = False
                dgrdRamadanTime.Columns(10).Visible = True
            Else
                If RBLScheduleType.SelectedValue = "N" Then
                    dvRamadanHeader.Visible = True

                    dgrdRamadanTime.Columns(3).Visible = True
                    dgrdRamadanTime.Columns(4).Visible = True
                    dgrdRamadanTime.Columns(6).Visible = True
                    dgrdRamadanTime.Columns(7).Visible = True
                    dgrdRamadanTime.Columns(8).Visible = True


                    dgrdRamadanTime.Columns(5).Visible = False
                    dgrdRamadanTime.Columns(9).Visible = False
                    dgrdRamadanTime.Columns(10).Visible = False

                ElseIf RBLScheduleType.SelectedValue = "F" Then
                    dvRamadanHeader.Visible = True

                    dgrdRamadanTime.Columns(3).Visible = True
                    dgrdRamadanTime.Columns(4).Visible = True
                    dgrdRamadanTime.Columns(6).Visible = True
                    dgrdRamadanTime.Columns(7).Visible = True
                    dgrdRamadanTime.Columns(8).Visible = True


                    dgrdRamadanTime.Columns(5).Visible = True
                    dgrdRamadanTime.Columns(9).Visible = True
                    dgrdRamadanTime.Columns(10).Visible = False
                ElseIf RBLScheduleType.SelectedValue = "O" Then
                    trRamadanGraceIn.Visible = False
                    trRamadanGraceOut.Visible = False

                    dvRamadanHeader.Visible = False

                    dgrdRamadanTime.Columns(2).Visible = False
                    dgrdRamadanTime.Columns(3).Visible = False
                    dgrdRamadanTime.Columns(4).Visible = False
                    dgrdRamadanTime.Columns(5).Visible = False
                    dgrdRamadanTime.Columns(6).Visible = False
                    dgrdRamadanTime.Columns(7).Visible = False
                    dgrdRamadanTime.Columns(8).Visible = False
                    dgrdRamadanTime.Columns(9).Visible = False
                    dgrdRamadanTime.Columns(10).Visible = True
                End If

            End If


            btnCancel.Text = ResourceManager.GetString("Cancel", CultureInfo)

            If Not .ScheduleId = 0 Then
                RamadanScheduleID = .ScheduleId
                FillRamadanWeekDays(RamadanScheduleID)
                ShowAddRamadanSchedule = True
                btnAddRamadan.Text = ResourceManager.GetString("ViewRamadan", CultureInfo)
                btnSaveRamadan.Text = ResourceManager.GetString("btnUpdate", CultureInfo)
            Else
                FillRamadanWeekDays(ScheduleID)
                ShowAddRamadanSchedule = True
                btnAddRamadan.Text = ResourceManager.GetString("AddRamadan", CultureInfo)
                btnSaveRamadan.Text = ResourceManager.GetString("btnSave", CultureInfo)
            End If

        End With

    End Function

    Public Function SaveSchedule() As String
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        For Each gvr As GridDataItem In dgrdWeekTime.Items
            If CBool(CType(gvr.FindControl("chkOffDays"), CheckBox).Checked) = False Then

                If (CStr(CType(gvr.FindControl("rmtFromTime1"), RadMaskedTextBox).Text)) = "0000" And RBLScheduleType.SelectedValue <> "O" Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("FrmTime1CantLeft", CultureInfo) + CStr(CType(gvr.FindControl("lblDays"), Label).Text), "info")
                    Exit Function
                End If
                If (CStr(CType(gvr.FindControl("rmtToTime1"), RadMaskedTextBox).Text)) = "0000" And RBLScheduleType.SelectedValue <> "O" Then
                    If Not RBLScheduleType.SelectedValue = "O" Then
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ToTime1CantLeft", CultureInfo) + CStr(CType(gvr.FindControl("lblDays"), Label).Text), "info")
                        Exit Function
                    End If
                End If

                If Not RBLScheduleType.SelectedValue = "O" Then
                    If Not (CStr(CType(gvr.FindControl("rmtFromTime2"), RadMaskedTextBox).Text)) = "0000" Then
                        Dim fromTime() = CType(gvr.FindControl("rmtFromTime2"), RadMaskedTextBox).TextWithLiterals.Split(":")
                        Dim ToTime() = CType(gvr.FindControl("rmtToTime1"), RadMaskedTextBox).TextWithLiterals.Split(":")
                        Dim inSecToTime As Integer = ((Val(fromTime(0)) * 3600) + (Val(fromTime(1)) * 60))
                        Dim inSecfromTime As Integer = ((Val(ToTime(0)) * 3600) + (Val(ToTime(1)) * 60))
                        If (inSecfromTime) > (inSecToTime) Then
                            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("Shift2GreaterthanShift1", CultureInfo) + CStr(CType(gvr.FindControl("lblDays"), Label).Text), "info")
                            Exit Function
                        End If
                    End If
                End If
            End If
        Next


        objWorkSchedule = New WorkSchedule
        Dim err As Integer
        With objWorkSchedule
            objWorkSchedule.ScheduleId = ScheduleID
            .ScheduleName = txtScheduleEng.Text.Trim()
            .ScheduleArabicName = txtScheduleAr.Text.Trim()
            If (trGraceIn.Visible) Then
                .GraceIn = txtGraceIn.Text

            End If
            If (trGraceOut.Visible) Then
                .GraceOut = txtGraceOut.Text
            End If

            .IsDefault = ChkIsDefault.Checked

            If RBLScheduleType.SelectedValue = "N" Then
                .ScheduleType = 1
            ElseIf RBLScheduleType.SelectedValue = "F" Then
                .ScheduleType = 2
            ElseIf RBLScheduleType.SelectedValue = "O" Then
                .ScheduleType = 5
            End If


            .IsRamadanSch = False

            'If RamadanScheduleID = 0 Then
            '    .IsRamadanSch = False
            'Else
            '    .IsRamadanSch = True
            'End If


            .GraceInGender = rblGraceInGender.SelectedValue
            .GraceOutGender = rblGraceOutGender.SelectedValue
            .ConsiderShiftScheduleAtEnd = Nothing
            .IsActive = chkIsActive.Checked

            If .ScheduleId = 0 Then
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
            If RBLScheduleType.SelectedValue = "N" Then
                objWorkSchedule_NormalTime = New WorkSchedule_NormalTime
                objWorkSchedule_NormalTime.FK_ScheduleId = ScheduleID
                For Each gvr As GridDataItem In dgrdWeekTime.Items
                    With objWorkSchedule_NormalTime
                        .FK_ScheduleId = ScheduleID
                        .IsOffDay = CBool(CType(gvr.FindControl("chkOffDays"), CheckBox).Checked)
                        .DayId = CInt(gvr.GetDataKeyValue("Dayid"))
                        If .IsOffDay = False Then
                            .FromTime1 = CStr(CType(gvr.FindControl("rmtFromTime1"), RadMaskedTextBox).Text)
                            .FromTime2 = CStr(CType(gvr.FindControl("rmtFromTime2"), RadMaskedTextBox).Text)
                            .ToTime1 = CStr(CType(gvr.FindControl("rmtToTime1"), RadMaskedTextBox).Text)
                            .ToTime2 = CStr(CType(gvr.FindControl("rmtToTime2"), RadMaskedTextBox).Text)
                        Else
                            .FromTime1 = "0000"
                            .FromTime2 = "0000"
                            .ToTime1 = "0000"
                            .ToTime2 = "0000"
                        End If
                        .Add()
                    End With
                Next
            ElseIf RBLScheduleType.SelectedValue = "F" Then
                objWorkSchedule_Flexible = New WorkSchedule_Flexible
                objWorkSchedule_Flexible.FK_ScheduleId = ScheduleID
                For Each gvr As GridDataItem In dgrdWeekTime.Items
                    With objWorkSchedule_Flexible
                        .FK_ScheduleId = ScheduleID
                        .DayId = CInt(gvr.GetDataKeyValue("Dayid"))
                        .IsOffDay = CBool(CType(gvr.FindControl("chkOffDays"), CheckBox).Checked)

                        .FromTime1 = CStr(CType(gvr.FindControl("rmtFromTime1"), RadMaskedTextBox).Text)
                        .FromTime2 = CStr(CType(gvr.FindControl("rmtToTime1"), RadMaskedTextBox).Text)

                        .FromTime3 = CStr(CType(gvr.FindControl("rmtFromTime2"), RadMaskedTextBox).Text)
                        .FromTime4 = CStr(CType(gvr.FindControl("rmtToTime2"), RadMaskedTextBox).Text)

                        Dim strDuration1 As String = (CInt(CType(gvr.FindControl("flex1"), RadMaskedTextBox).TextWithLiterals.Split(":")(0)) * 60) _
                                                     + CInt(CType(gvr.FindControl("flex1"), RadMaskedTextBox).TextWithLiterals.Split(":")(1))
                        .Duration1 = strDuration1
                        Dim strDuration2 As String = (CInt(CType(gvr.FindControl("flex2"), RadMaskedTextBox).TextWithLiterals.Split(":")(0)) * 60) _
                                                     + CInt(CType(gvr.FindControl("flex2"), RadMaskedTextBox).TextWithLiterals.Split(":")(1))
                        .Duration2 = strDuration2

                        .Add()
                    End With
                    gvr = Nothing
                Next
            ElseIf RBLScheduleType.SelectedValue = "O" Then
                objWorkSchedule_Open = New WorkSchedule_Open
                With objWorkSchedule_Open
                    .FK_ScheduleId = ScheduleID

                    For Each gvr As GridDataItem In dgrdWeekTime.Items
                        With objWorkSchedule_Open
                            .FK_ScheduleId = ScheduleID
                            .DayId = CInt(gvr.GetDataKeyValue("Dayid"))
                            .IsOffDay = CBool(CType(gvr.FindControl("chkOffDays"), CheckBox).Checked)
                            .FromTime = "0000" 'CStr(CType(gvr.FindControl("rmtFromTime1"), RadMaskedTextBox).Text)
                            Dim strDuration1 As String = (CInt(CType(gvr.FindControl("flex1"), RadMaskedTextBox).TextWithLiterals.Split(":")(0)) * 60) _
                                                      + CInt(CType(gvr.FindControl("flex1"), RadMaskedTextBox).TextWithLiterals.Split(":")(1))
                            .FlexTime = strDuration1
                            .WorkHours = CStr(CType(gvr.FindControl("radWorkHrs"), RadMaskedTextBox).Text)
                            .Add()
                        End With
                        gvr = Nothing
                    Next
                End With
            End If
        End If
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            'ClearAll()
            'clear_DataGrid_Controls()
        ElseIf err = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
        ElseIf err = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Function

    Public Function SaveRamadanSchedule() As String
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        For Each gvr As GridDataItem In dgrdRamadanTime.Items
            If CBool(CType(gvr.FindControl("chkOffDays"), CheckBox).Checked) = False Then

                If (CStr(CType(gvr.FindControl("rmtFromTime1"), RadMaskedTextBox).Text)) = "0000" And RBLScheduleType.SelectedValue <> "O" Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("FrmTime1CantLeft", CultureInfo) + CStr(CType(gvr.FindControl("lblDays"), Label).Text), "info")
                    mpeRamadanPopup.Show()
                    Exit Function
                End If
                If (CStr(CType(gvr.FindControl("rmtToTime1"), RadMaskedTextBox).Text)) = "0000" And RBLScheduleType.SelectedValue <> "O" Then
                    If Not RBLScheduleType.SelectedValue = "O" Then
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ToTime1CantLeft", CultureInfo) + CStr(CType(gvr.FindControl("lblDays"), Label).Text), "info")
                        mpeRamadanPopup.Show()
                        Exit Function
                    End If
                End If

            End If
        Next
        objWorkSchedule = New WorkSchedule
        Dim err As Integer
        With objWorkSchedule
            objWorkSchedule.ParentSchId = ScheduleID
            .ScheduleName = "Ramadan " + txtScheduleEng.Text
            .ScheduleArabicName = "رمضان " + txtScheduleAr.Text
            .IsRamadanSch = True

            If (trRamadanGraceIn.Visible) Then
                If Not String.IsNullOrEmpty(txtGraceIn.Text) Then
                    .GraceIn = txtRamadanGraceIn.Text
                End If
            End If

            If (trRamadanGraceOut.Visible) Then
                If Not String.IsNullOrEmpty(txtGraceOut.Text) Then
                    .GraceOut = txtRamadanGraceOut.Text
                End If
            End If

            .IsDefault = False

            If RBLScheduleType.SelectedValue = "N" Then
                .ScheduleType = 1
            ElseIf RBLScheduleType.SelectedValue = "F" Then
                .ScheduleType = 2
            ElseIf RBLScheduleType.SelectedValue = "O" Then
                .ScheduleType = 5
            End If

            If RamadanScheduleID = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add()
                RamadanScheduleID = .ScheduleId
            Else
                .ScheduleId = RamadanScheduleID
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                err = .Update()
            End If
        End With
        If err = 0 Then
            If RBLScheduleType.SelectedValue = "N" Then
                objWorkSchedule_NormalTime = New WorkSchedule_NormalTime
                objWorkSchedule_NormalTime.FK_ScheduleId = ScheduleID
                For Each gvr As GridDataItem In dgrdRamadanTime.Items
                    With objWorkSchedule_NormalTime
                        .FK_ScheduleId = RamadanScheduleID
                        .DayId = CInt(gvr.GetDataKeyValue("Dayid"))
                        .FromTime1 = CStr(CType(gvr.FindControl("rmtFromTime1"), RadMaskedTextBox).Text)
                        .FromTime2 = CStr(CType(gvr.FindControl("rmtFromTime2"), RadMaskedTextBox).Text)
                        .ToTime1 = CStr(CType(gvr.FindControl("rmtToTime1"), RadMaskedTextBox).Text)
                        .ToTime2 = CStr(CType(gvr.FindControl("rmtToTime2"), RadMaskedTextBox).Text)
                        .IsOffDay = CBool(CType(gvr.FindControl("chkOffDays"), CheckBox).Checked)
                        .Add()
                    End With
                Next
            ElseIf RBLScheduleType.SelectedValue = "F" Then
                objWorkSchedule_Flexible = New WorkSchedule_Flexible
                objWorkSchedule_Flexible.FK_ScheduleId = ScheduleID



                For Each gvr As GridDataItem In dgrdRamadanTime.Items
                    With objWorkSchedule_Flexible
                        .FK_ScheduleId = RamadanScheduleID
                        .DayId = CInt(gvr.GetDataKeyValue("Dayid"))
                        .FromTime1 = CStr(CType(gvr.FindControl("rmtFromTime1"), RadMaskedTextBox).Text)
                        .FromTime2 = CStr(CType(gvr.FindControl("rmtToTime1"), RadMaskedTextBox).Text)
                        Dim strDuration1 As String = (CInt(CType(gvr.FindControl("flex1"), RadMaskedTextBox).TextWithLiterals.Split(":")(0)) * 60) _
                                                     + CInt(CType(gvr.FindControl("flex1"), RadMaskedTextBox).TextWithLiterals.Split(":")(1))
                        .Duration1 = strDuration1
                        .FromTime3 = CStr(CType(gvr.FindControl("rmtFromTime2"), RadMaskedTextBox).Text)
                        .FromTime4 = CStr(CType(gvr.FindControl("rmtToTime2"), RadMaskedTextBox).Text)
                        Dim strDuration2 As String = (CInt(CType(gvr.FindControl("flex2"), RadMaskedTextBox).TextWithLiterals.Split(":")(0)) * 60) _
                                                     + CInt(CType(gvr.FindControl("flex2"), RadMaskedTextBox).TextWithLiterals.Split(":")(1))
                        .Duration2 = strDuration2
                        .IsOffDay = CBool(CType(gvr.FindControl("chkOffDays"), CheckBox).Checked)
                        .Add()
                    End With
                Next
            ElseIf RBLScheduleType.SelectedValue = "O" Then
                objWorkSchedule_Open = New WorkSchedule_Open
                For Each gvr As GridDataItem In dgrdRamadanTime.Items
                    With objWorkSchedule_Open
                        .FK_ScheduleId = RamadanScheduleID
                        .DayId = CInt(gvr.GetDataKeyValue("Dayid"))
                        .IsOffDay = CBool(CType(gvr.FindControl("chkOffDays"), CheckBox).Checked)
                        .FromTime = CStr(CType(gvr.FindControl("rmtFromTime1"), RadMaskedTextBox).Text)
                        Dim strDuration1 As String = (CInt(CType(gvr.FindControl("flex1"), RadMaskedTextBox).TextWithLiterals.Split(":")(0)) * 60) _
                                                  + CInt(CType(gvr.FindControl("flex1"), RadMaskedTextBox).TextWithLiterals.Split(":")(1))
                        .FlexTime = strDuration1
                        .WorkHours = CStr(CType(gvr.FindControl("radWorkHrs"), RadMaskedTextBox).Text)
                        .Add()
                    End With
                    gvr = Nothing
                Next
            End If
        End If
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")

            btnAddRamadan.Text = ResourceManager.GetString("ViewRamadan", CultureInfo)
            ShowAddRamadanSchedule = True
        ElseIf err = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
            ShowAddRamadanSchedule = True
        ElseIf err = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
            ShowAddRamadanSchedule = True
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            ShowAddRamadanSchedule = True
        End If
    End Function

    Private Sub ShowHide()
        If RBLScheduleType.SelectedValue = "N" Then
            dvScheduleHeader.Visible = True

            dgrdWeekTime.Columns(3).Visible = True
            dgrdWeekTime.Columns(4).Visible = True
            dgrdWeekTime.Columns(6).Visible = True
            dgrdWeekTime.Columns(7).Visible = True
            dgrdWeekTime.Columns(8).Visible = True


            dgrdWeekTime.Columns(5).Visible = False
            dgrdWeekTime.Columns(9).Visible = False
            dgrdWeekTime.Columns(10).Visible = False

            dgrdRamadanTime.Columns(5).Visible = False
            dgrdRamadanTime.Columns(9).Visible = False

        ElseIf RBLScheduleType.SelectedValue = "F" Then
            dvScheduleHeader.Visible = True

            dgrdWeekTime.Columns(3).Visible = True
            dgrdWeekTime.Columns(4).Visible = True
            dgrdWeekTime.Columns(6).Visible = True
            dgrdWeekTime.Columns(7).Visible = True
            dgrdWeekTime.Columns(8).Visible = True


            dgrdWeekTime.Columns(5).Visible = True
            dgrdWeekTime.Columns(9).Visible = True
            dgrdWeekTime.Columns(10).Visible = False

        ElseIf RBLScheduleType.SelectedValue = "O" Then
            dvScheduleHeader.Visible = False


            dgrdWeekTime.Columns(2).Visible = False
            dgrdWeekTime.Columns(3).Visible = False
            dgrdWeekTime.Columns(4).Visible = False
            dgrdWeekTime.Columns(5).Visible = False
            dgrdWeekTime.Columns(6).Visible = False
            dgrdWeekTime.Columns(7).Visible = False
            dgrdWeekTime.Columns(8).Visible = False
            dgrdWeekTime.Columns(9).Visible = False
            dgrdWeekTime.Columns(10).Visible = True



            dgrdRamadanTime.Columns(2).Visible = False
            dgrdRamadanTime.Columns(3).Visible = False
            dgrdRamadanTime.Columns(4).Visible = False
            dgrdRamadanTime.Columns(5).Visible = False
            dgrdRamadanTime.Columns(6).Visible = False
            dgrdRamadanTime.Columns(7).Visible = False
            dgrdRamadanTime.Columns(8).Visible = False
            dgrdRamadanTime.Columns(9).Visible = False
            dgrdRamadanTime.Columns(10).Visible = True

        End If
        CreateDTWeekDays()
    End Sub

#End Region

#Region "FillData_Normal"
    'Public Function filldata_normal() As String
    '    objWorkSchedule = New WorkSchedule
    '    With objWorkSchedule
    '        .ScheduleId = ScheduleID
    '        .GetByPK()
    '        txtGraceIn.Text = .GraceIn
    '        txtScheduleEng.Text = .ScheduleName
    '        txtScheduleAr.Text = .ScheduleArabicName
    '        txtGraceOut.Text = .GraceOut
    '        ChkIsDefault.Checked = .IsDefault
    '        'RadScheduleType.SelectedValue = 1
    '        FillWeekDays()
    '    End With
    'End Function
#End Region

End Class