Imports SmartV.UTILITIES
Imports SmartV.UTILITIES.CtlCommon
Imports System.Globalization
Imports Telerik.Web.UI
Imports System.Data
Imports TA.Definitions
Imports TA.Admin
Imports System.Web.UI.Control
Imports System.Web.UI.Page

Partial Class Definitions_UserControls_FlexibleSchedule
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private objWeekDays As New WeekDays
    Private objWorkSchedule_NormalTime As WorkSchedule_NormalTime
    Private objWorkSchedule As WorkSchedule
    Private objApp_Settings As APP_Settings
    Private objWorkSchedule_Flexible As WorkSchedule_Flexible
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Public Shared FromTimeBuilder1 As New StringBuilder
    Public Shared FromTimeBuilder2 As New StringBuilder
    Public Shared FromTimeBuilder3 As New StringBuilder
    Public Shared FromTimeBuilder4 As New StringBuilder
    Public Shared ToTimeBuilder1 As New StringBuilder
    Public Shared ToTimeBuilder2 As New StringBuilder
    Public Shared ToTimeBuilder3 As New StringBuilder
    Public Shared ToTimeBuilder4 As New StringBuilder
    Public Shared DurationBuilder1 As New StringBuilder
    Public Shared DurationBuilder2 As New StringBuilder

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

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If SessionVariables.CultureInfo = "ar-JO" Then
            lblShift1.Text = "مناوبة 2"
            lblShift2.Text = "مناوبة 1"
        End If
        If Not Page.IsPostBack Then
            CtlCommon.FillRadGridView(dgrdWeekTimeShift1, objWeekDays.Flexible_GetAll())
            clear_DataGrid_Controls_Flx()
            objApp_Settings = New APP_Settings()
            objApp_Settings.GetByPK()
            If objApp_Settings.IsGraceTAPolicy Then
                trGraceIn.Visible = False
                trGraceOut.Visible = False
            End If
        End If
    End Sub

#End Region

#Region "Methods"

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

    Public Sub FillWeekDays_Flx()
        Try
            objWorkSchedule_Flexible = New WorkSchedule_Flexible
            objWorkSchedule_Flexible.FK_ScheduleId = ScheduleID
            dgrdWeekTimeShift1.DataSource = objWorkSchedule_Flexible.GetAll()
            dgrdWeekTimeShift1.DataBind()


        Catch ex As Exception
        End Try
    End Sub

    Public Sub ClearAll_Flx()
        txtGraceIn.Text = String.Empty
        txtGraceOut.Text = String.Empty
        txtScheduleAr.Text = String.Empty
        txtScheduleEng.Text = String.Empty

        ScheduleID = 0
        DayID = 0


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
                Dim tmpSTrchk As String = String.Empty
                Dim tmpSTr As String = "'" & FromTime1.ClientID & "','" & ToTime1.ClientID & "','" & FromTime2.ClientID & "','" & ToTime2.ClientID & "','" & rmtDuration1.ClientID & "','" & chkIsoffDay.ClientID & "'"
                Dim enDay = DirectCast(Itm.FindControl("hdnEnDay"), HiddenField)
                Dim arDay = DirectCast(Itm.FindControl("hdnArDay"), HiddenField)
                Dim lblDay = DirectCast(Itm.FindControl("lblDays"), Label)
                If SessionVariables.CultureInfo = "ar-JO" Then
                    lblDay.Text = arDay.Value
                Else
                    lblDay.Text = enDay.Value
                End If

                FromTime1.Attributes.Add("onblur", "FlexValidateGridRowTest(" & tmpSTr & ");")
                ToTime1.Attributes.Add("onblur", "FlexValidateGridRowTest(" & tmpSTr & ");")
                FromTime2.Attributes.Add("onblur", "FlexValidateGridRowTest(" & tmpSTr & ");")
                ToTime2.Attributes.Add("onblur", "FlexValidateGridRowTest(" & tmpSTr & ");")
                rmtDuration1.Attributes.Add("onblur", "FlexValidateGridRowTest(" & tmpSTr & ");")
                Dim tmpstr2 As String = "'" & FromTime1.ClientID & "','" & ToTime1.ClientID & "','" & FromTime2.ClientID & "','" & ToTime2.ClientID & "','" & rmtDuration1.ClientID & "',"

                FromTimeBuilder1.AppendFormat("{0} ,", FromTime1.ClientID)
                FromTimeBuilder2.AppendFormat("{0} ,", FromTime2.ClientID)
                ToTimeBuilder1.AppendFormat("{0} ,", ToTime1.ClientID)
                ToTimeBuilder2.AppendFormat("{0} ,", ToTime2.ClientID)
                DurationBuilder1.AppendFormat("{0} ,", rmtDuration1.ClientID)

                hdnFromTime1.Value = FromTimeBuilder1.ToString()
                hdnFromTime2.Value = FromTimeBuilder2.ToString()
                hdnToTime1.Value = ToTimeBuilder1.ToString()
                hdnToTime2.Value = ToTimeBuilder2.ToString()
                hdnDuration1.Value = DurationBuilder1.ToString()

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
                tmpSTrchk = "'" & FromTime1.ClientID & "','" & _
                                            ToTime1.ClientID & "','" & _
                                            FromTime2.ClientID & "','" & _
                                            ToTime2.ClientID & "'," & _
                                            chkIsoffDay.ClientID & ",'" & _
                                            rmtDuration1.ClientID & "'"

                FromTime1.Attributes.Add("onblur", "FlexValidateGridRowTest(" & tmpSTr & ");")
                ToTime1.Attributes.Add("onblur", "FlexValidateGridRowTest(" & tmpSTr & ");")
                FromTime2.Attributes.Add("onblur", "FlexValidateGridRowTest(" & tmpSTr & ");")
                ToTime2.Attributes.Add("onblur", "FlexValidateGridRowTest(" & tmpSTr & ");")
                rmtDuration1.Attributes.Add("onblur", "FlexValidateGridRowTest(" & tmpSTr & ");")
                chkIsoffDay.Attributes.Add("onclick", "x(" & tmpstr2 & tmpSTrchk & ");")

                FromTimeBuilder3.AppendFormat("{0} ,", FromTime1.ClientID)
                FromTimeBuilder4.AppendFormat("{0} ,", FromTime2.ClientID)
                ToTimeBuilder3.AppendFormat("{0} ,", ToTime1.ClientID)
                ToTimeBuilder4.AppendFormat("{0} ,", ToTime2.ClientID)
                DurationBuilder2.AppendFormat("{0} ,", rmtDuration1.ClientID)

                hdnFromTime3.Value = FromTimeBuilder3.ToString()
                hdnFromTime4.Value = FromTimeBuilder4.ToString()
                hdnToTime3.Value = ToTimeBuilder3.ToString()
                hdnToTime4.Value = ToTimeBuilder4.ToString()
                hdnDuration2.Value = DurationBuilder2.ToString()

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

    Public Sub clear_DataGrid_Controls_Flx()
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

    Public Function filldata_Flexible() As String
        objWorkSchedule = New WorkSchedule
        With objWorkSchedule
            .ScheduleId = ScheduleID
            .GetByPK()

            txtScheduleEng.Text = .ScheduleName
            txtScheduleAr.Text = .ScheduleArabicName
            txtGraceIn.Text = .GraceIn
            txtGraceOut.Text = .GraceOut
            FillWeekDays_Flx()
        End With
    End Function

    Public Function SaveFlexible_Schedule() As String
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        For Each gvr As GridDataItem In dgrdWeekTimeShift1.Items
            If CBool(CType(gvr.FindControl("Shift2offDay"), CheckBox).Checked) = False Then

                If (CStr(CType(gvr.FindControl("rmtFromTime1"), RadMaskedTextBox).Text)) = "0000" Then
                    CtlCommon.ShowMessage(Me.Parent.Page, ResourceManager.GetString("FrmTime1CantLeft", CultureInfo) + " " + CStr(CType(gvr.FindControl("lblDays"), Label).Text), "info")
                    Exit Function
                End If
                If (CStr(CType(gvr.FindControl("rmtFromTime2"), RadMaskedTextBox).Text)) = "0000" Then
                    CtlCommon.ShowMessage(Me.Parent.Page, ResourceManager.GetString("FrmTime2CantLeft", CultureInfo) + " " + CStr(CType(gvr.FindControl("lblDays"), Label).Text), "info")
                    Exit Function
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

            ClearAll_Flx()
            clear_DataGrid_Controls_Flx()
        ElseIf err = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
        ElseIf err = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Function

#End Region

End Class
