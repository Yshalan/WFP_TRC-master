Imports System.Data
Imports TA.Definitions
Imports SmartV.DB
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.Admin
Imports TA.Security
Imports SmartV.UTILITIES.ProjectCommon

Partial Class Admin_WorkSchedule
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objWeekDays As New WeekDays
    Private objWorkSchedule_NormalTime As WorkSchedule_NormalTime
    Private objWorkSchedule As WorkSchedule
    Private objApp_Settings As APP_Settings
    Private objWorkSchedule_Flexible As WorkSchedule_Flexible
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Properties"

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

    Public Property IsDefault() As Boolean
        Get
            Return ViewState("DtCurrent")
        End Get
        Set(ByVal value As Boolean)
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
                objNormalschedule.IsGraceTAPolicy = False
            End If
            objNormalschedule.CreateDTWeekDays()
            objNormalschedule.clear_DataGrid_Controls()
            FillGrid()
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("WorkSchedule", CultureInfo)
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

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Sub dgrdWorkSchedule_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdWorkSchedule.NeedDataSource
        objWorkSchedule = New WorkSchedule
        dgrdWorkSchedule.DataSource = objWorkSchedule.GetNormal_Flexible()
    End Sub

    Protected Sub dgrdWorkSchedule_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdWorkSchedule.SelectedIndexChanged
        ScheduleID = CInt(CType(dgrdWorkSchedule.SelectedItems(0), GridDataItem).GetDataKeyValue("ScheduleId").ToString())
        objNormalschedule.ClearAll()
        objNormalschedule.clear_DataGrid_Controls()

        objNormalschedule.ScheduleID = ScheduleID
        objNormalschedule.EnabledDisabeldScheduleType = False
        objWorkSchedule = New WorkSchedule
        With objWorkSchedule
            .ScheduleId = ScheduleID
            .GetByPK()
            If .IsRamadanSch Then
                objNormalschedule.ShowAddRamadanSchedule = True
            End If
            objNormalschedule.ScheduleID = ScheduleID
            objNormalschedule.filldata()
        End With
    End Sub

    Protected Sub dgrdWorkSchedule_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

#End Region

#Region "Page Methods"

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdWorkSchedule.Skin))
    End Function

    Public Sub FillGrid()
        Try
            Dim dt As DataTable
            objWorkSchedule = New WorkSchedule
            dt = objWorkSchedule.GetNormal_Flexible()
            dgrdWorkSchedule.DataSource = dt
            dgrdWorkSchedule.DataBind()

            If objApp_Settings.IsGraceTAPolicy Then
                dgrdWorkSchedule.Columns(4).Visible = False
                dgrdWorkSchedule.Columns(5).Visible = False
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Normal Schedule"

    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click
        SaveSchedule()

    End Sub

    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnSave.Click
        CheckDefaultSchedule()
        If IsDefault = False Then
            SaveSchedule()
            objNormalschedule.ShowAddRamadanSchedule = True
        End If
    End Sub

    Protected Sub SaveSchedule()
        objNormalschedule.SaveSchedule()
        FillGrid()


       

    End Sub

    Protected Sub CheckDefaultSchedule()
        objWorkSchedule = New WorkSchedule
        If objNormalschedule.CheckIsDefault Then
            objWorkSchedule.ScheduleId = ScheduleID
            Dim err As Integer = 0
            err = objWorkSchedule.CheckDefaultSchedule()
            If err = -99 Then
                IsDefault = True
                mpeSave.Show()
            Else
                IsDefault = False
            End If
        Else
            IsDefault = False
        End If
    End Sub

    Protected Sub ibtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnDelete.Click
        Dim err As Integer = -1
        Dim error1 As Integer = -1
        Dim ParentScheduleId As Integer
        Dim counter As Integer = 0
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdWorkSchedule.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                counter = counter + 1
                Dim strCode As String = row.GetDataKeyValue("ScheduleName").ToString()
                Dim intScheduleID As Integer = Convert.ToInt32(row.GetDataKeyValue("ScheduleId").ToString())
                objWorkSchedule = New WorkSchedule
                objWorkSchedule.ScheduleId = intScheduleID
                ParentScheduleId = intScheduleID
                
                err = objWorkSchedule.Delete()
                If err = 0 Then
                    error1 = 0
                    objWorkSchedule.ParentSchId = ParentScheduleId
                    objWorkSchedule.GetByParentId()
                    If (objWorkSchedule.ScheduleId >= 0) Then
                        error1 = objWorkSchedule.Delete()
                    End If


                End If
            End If
        Next
        If counter = 0 Then
            CtlCommon.ShowMessage(Me.Page, IIf(Lang = CtlCommon.Lang.AR, "الرجاء اختيار الجداول المراد حذفها", " Please Select schedules to delete"), "info")
            Return
        End If
        If err = 0 And error1 = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGrid()
            objNormalschedule.ClearAll()
            objNormalschedule.clear_DataGrid_Controls()
            dgrdWorkSchedule.SelectedIndexes.Clear()
        ElseIf err = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord"), "error")
        Else
            If error1 = 0 Then
                CtlCommon.ShowMessage(Me.Page, IIf(Lang = CtlCommon.Lang.AR, "الرجاء حذف جدول رمضان المرتبط بهذا الجدول", " Please delete the related ramadan schedule"), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        End If
    End Sub

    Protected Sub ibtnRest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnRest.Click
        objNormalschedule.ClearAll()
        objNormalschedule.clear_DataGrid_Controls()
        dgrdWorkSchedule.SelectedIndexes.Clear()
    End Sub

#End Region

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

    'Protected Sub dgrdWeekTime_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdWeekTime.ItemDataBound
    '    Dim chkOffDays As CheckBox = CType(e.Item.FindControl("chkOffDays"), CheckBox)
    '    'MsgBox(chkOffDays.ID.ToString)
    'End Sub


    'Public Sub checkboxClicked1(ByVal sender As Object, ByVal e As GridItemEventArgs)
    '    Dim HeaderItem As GridHeaderItem
    '    Dim DateItem As GridDataItem
    '    Dim chkbx As New CheckBox()
    '    If TypeOf e.Item Is GridHeaderItem Then
    '        HeaderItem = DirectCast(e.Item, GridHeaderItem)
    '        chkbx = DirectCast(HeaderItem("chkOffDays").Controls(0), CheckBox)
    '    End If
    '    If TypeOf e.Item Is GridDataItem Then
    '        DateItem = DirectCast(e.Item, GridDataItem)
    '        chkbx = DirectCast(DateItem("chkOffDays").Controls(0), CheckBox)
    '    End If
    '    MsgBox("1")
    'End Sub
    'Protected Sub lnkScheduleName_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim gvr As GridViewRow = CType(sender, LinkButton).Parent.Parent


    'End Sub
    
End Class
