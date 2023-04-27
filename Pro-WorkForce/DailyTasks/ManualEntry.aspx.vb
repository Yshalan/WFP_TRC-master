Imports TA.Employees
Imports SmartV.UTILITIES
Imports TA.LookUp
Imports System.Data
Imports TA.DailyTasks
Imports TA.Admin
Imports Telerik.Web.UI
Imports TA.Security
Imports System.IO

Partial Class Admin_ManualEntry
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objTA_Reason As TA_Reason
    Private objEmpMove As Emp_Move
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objREADER_KEYS As READER_KEYS
    Private objEmployee As Employee
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objVersion As SmartV.Version.version
    Private objOrgCompany As OrgCompany
    Private objAPP_Settings As APP_Settings

#End Region

#Region "Properties"

    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Public Property EntityId() As Integer
        Get
            Return ViewState("EntityId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EntityId") = value
        End Set
    End Property

    Public Property CompanyId() As Integer
        Get
            Return ViewState("CompanyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CompanyId") = value
        End Set
    End Property

    Public Property IsEmployeeRequired() As Boolean
        Get
            Return ViewState("IsEmployeeRequired")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsEmployeeRequired") = value
        End Set
    End Property

    Public Property IsLevelRequired() As Boolean
        Get
            Return ViewState("IsLevelRequired")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsLevelRequired") = value
        End Set
    End Property

    Public Property tbl_id() As Long
        Get
            Return ViewState("tbl_id")
        End Get
        Set(ByVal value As Long)
            ViewState("tbl_id") = value
        End Set
    End Property

    Public Property bFillGrid() As Boolean
        Get
            Return ViewState("bFillGrid")
        End Get
        Set(ByVal value As Boolean)
            ViewState("bFillGrid") = value
        End Set
    End Property

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

    Public Property SortExepression() As String
        Get
            Return ViewState("SortExepression")
        End Get
        Set(ByVal value As String)
            ViewState("SortExepression") = value
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

    Public Property FileExtension() As String
        Get
            Return ViewState("FileExtension")
        End Get
        Set(ByVal value As String)
            ViewState("FileExtension") = value
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
                txtremarks.Text = "الادخال اليدوي"
            Else
                Lang = CtlCommon.Lang.EN
                txtremarks.Text = "Manual Entry"
            End If

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            FillReasons()
            IsEmployeeRequired = True
            IsLevelRequired = False
            rmtToTime2.Text = Now.TimeOfDay.ToString()
            RadDatePicker1.SelectedDate = Now
            RadDatePicker1.MaxDate = Now

            EmployeeFilterUC.IsEmployeeRequired = IsEmployeeRequired
            EmployeeFilterUC.IsLevelRequired = IsLevelRequired
            PageHeader1.HeaderText = ResourceManager.GetString("ManualEntry", CultureInfo)
            objAPP_Settings = New APP_Settings
            With objAPP_Settings
                .GetByPK()
                If .ShowEmployeeList = 0 Then
                    FillGrid()
                End If

            End With

            ArcivingMonths_DateValidation()
        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrentControls.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not pnlEmployee.FindControl(row("AddBtnName")) Is Nothing Then
                        pnlEmployee.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not pnlEmployee.FindControl(row("DeleteBtnName")) Is Nothing Then
                        pnlEmployee.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not pnlEmployee.FindControl(row("EditBtnName")) Is Nothing Then
                        pnlEmployee.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not pnlEmployee.FindControl(row("PrintBtnName")) Is Nothing Then
                        pnlEmployee.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next


    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not ddlReason.SelectedValue = -1 Then
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            Dim temp_date As Date
            Dim temp_str_date As String
            Dim formats As String() = New String() {"HHmm"}
            Dim temp_time As DateTime
            Dim temp_str_time As String
            Dim err As Integer

            objEmpMove = New Emp_Move
            objRECALC_REQUEST = New RECALC_REQUEST
            objREADER_KEYS = New READER_KEYS
            objRECALC_REQUEST.EMP_NO = EmployeeFilterUC.EmployeeId

            objEmployee = New Employee
            EmployeeId = EmployeeFilterUC.EmployeeId
            If objEmployee.GetEmpNo(EmployeeFilterUC.EmployeeId) = False Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NumIsntForThisLevel", CultureInfo), "info")
                Return
            End If

            If fuAttachFile.HasFile = False AndAlso FileExtension = Nothing Then
                If objAPP_Settings.AttachmentIsMandatoryManualEntryRequest Then
                    If Lang = CtlCommon.Lang.AR Then
                        CtlCommon.ShowMessage(Me.Page, "يرجى ارفاق ملف!", "info")
                    Else
                        CtlCommon.ShowMessage(Me.Page, "Please Attach File!", "info")
                    End If
                    Exit Sub
                End If
            End If

            Dim fileUploadExtension As String = ""
            If FileExtension = Nothing Then
                If Not fuAttachFile.PostedFile Is Nothing Then
                    fileUploadExtension = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                End If
            Else
                fileUploadExtension = FileExtension
            End If


            objEmpMove.FK_EmployeeId = EmployeeId
            objEmpMove.Remarks = txtremarks.Text
            objEmpMove.Reader = String.Empty
            objEmpMove.Status = 1
            objEmpMove.IsManual = True
            objEmpMove.CREATED_BY = SessionVariables.LoginUser.ID
            objEmpMove.Remarks = txtremarks.Text

            If FileExtension = Nothing Then
                If Not fuAttachFile.PostedFile Is Nothing Then
                    objEmpMove.AttachedFile = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                End If
            Else
                objEmpMove.AttachedFile = FileExtension
            End If


            objREADER_KEYS.READER_KEY = ddlReason.SelectedValue
            objREADER_KEYS.GetByPK()

            objEmpMove.FK_ReasonId = objREADER_KEYS.CHANGE_TO
            objEmpMove.Type = objREADER_KEYS.Type
            If RadDatePicker1.SelectedDate IsNot Nothing Then
                objEmpMove.MoveDate = RadDatePicker1.SelectedDate
                temp_date = RadDatePicker1.SelectedDate
                temp_str_date = DateToString(temp_date)
                objEmpMove.M_DATE_NUM = temp_str_date
                objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)
            Else
                objEmpMove.MoveDate = Nothing
                objEmpMove.M_DATE_NUM = Nothing
            End If
            objEmpMove.IsRemoteWork = False

            DateTime.TryParseExact(rmtToTime2.Text, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, temp_time)

            objEmpMove.MoveTime = temp_time
            temp_str_time = temp_time.Minute + temp_time.Hour * 60
            objEmpMove.M_TIME_NUM = temp_str_time

            If tbl_id = 0 Then
                err = objEmpMove.Add()
                tbl_id = objEmpMove.MoveId
            Else
                objEmpMove.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                objEmpMove.MoveId = tbl_id
                err = objEmpMove.Update()
            End If
            If err = 0 Then
                Dim err2 As Integer
                Dim count As Integer
                While count < 5
                    err2 = objRECALC_REQUEST.RECALCULATE()
                    If err2 = 0 Then
                        Exit While
                    End If
                    count += 1
                End While
                FillGrid()
            End If

            If err = 0 Then
                If FileExtension = Nothing Then
                    If Not fileUploadExtension Is Nothing Then
                        'If fuAttachFile.HasFile Then
                        objEmpMove.AttachedFile = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                        Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                        Dim fileName As String = tbl_id.ToString()
                        Dim fPath As String = String.Empty
                        fPath = Server.MapPath("..\ManualEntryFiles\\" + fileName + extention)
                        fuAttachFile.PostedFile.SaveAs(fPath)
                    Else
                        objEmpMove.AttachedFile = String.Empty
                    End If
                End If

            End If


            If err = 0 Then
                ddlReason.SelectedValue = -1
                'RadDatePicker1.SelectedDate = Now
                rmtToTime2.Text = Now.TimeOfDay.ToString()
                txtremarks.Text = "Manual Entry"
                tbl_id = 0
                lnbLeaveFile.Visible = False
                lnbRemove.Visible = False
                lblNoAttachedFile.Visible = False
                FileExtension = Nothing
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")

            End If
        End If
    End Sub

    Protected Sub ibtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnDelete.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim err As Integer = -99
        For Each item As GridDataItem In dgEmpAtt.Items
            Dim cb As CheckBox = DirectCast(item.FindControl("chk"), CheckBox)
            If cb.Checked Then
                Dim tbl_id As Integer = Convert.ToInt32(item.GetDataKeyValue("moveid").ToString())
                objEmpMove = New Emp_Move
                objEmpMove.MoveId = tbl_id
                objEmpMove.GetByPK()
                err = objEmpMove.Delete()
            End If
        Next

        Dim TempFromDate As Date
        Dim TempFromStr As String
        objRECALC_REQUEST = New RECALC_REQUEST
        objRECALC_REQUEST.EMP_NO = EmployeeFilterUC.EmployeeId

        If Not RadDatePicker1.SelectedDate Is Nothing Then
            TempFromDate = RadDatePicker1.SelectedDate
            TempFromStr = DateToString(TempFromDate)
        End If

        If err = 0 Then
            Dim fileName As String = tbl_id.ToString()
            Dim fPath As String = String.Empty
            fPath = Server.MapPath("..\ManualEntryFiles\\" + fileName + objEmpMove.AttachedFile)
            If File.Exists(fPath) Then
                File.Delete(fPath)
            End If
        End If


        If err = 0 Then
            FillGrid()
            'ClearAll()
            ddlReason.SelectedIndex = 0
            rmtToTime2.Text = String.Empty
            If RadDatePicker1.SelectedDate <= Date.Now Then
                objRECALC_REQUEST.VALID_FROM_NUM = TempFromStr
                objRECALC_REQUEST.RECALCULATE()
            End If
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGrid()
            ClearAll()
        ElseIf err = -99 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PleaseSelectFromList", CultureInfo), "info")

        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If
    End Sub

    Protected Sub ibtnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnClear.Click
        ClearAll()
    End Sub

    Protected Sub dgEmpAtt_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgEmpAtt.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            Dim strIsFromMobile As String = item.GetDataKeyValue("IsFromMobile").ToString()
            Dim strCoordinations As String = item.GetDataKeyValue("MobileCoordinates").ToString()
            If strIsFromMobile = "True" Then
                If Lang = CtlCommon.Lang.AR Then

                    item("MobileCoordinates").Text = "نعم" + "<br />" + strCoordinations
                Else
                    item("MobileCoordinates").Text = "Yes" + "<br />" + strCoordinations
                End If
            End If
            If Lang = CtlCommon.Lang.AR Then
                item("name").Text = item.GetDataKeyValue("EmployeeArabicName").ToString()
                item("ReasonName").Text = item.GetDataKeyValue("ReasonArabicName").ToString()
            End If
        End If
    End Sub

    Protected Sub dgEmpAtt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgEmpAtt.SelectedIndexChanged

        tbl_id = CInt(CType(dgEmpAtt.SelectedItems(0), GridDataItem).GetDataKeyValue("moveid").ToString())
        objEmpMove = New Emp_Move
        objEmpMove.MoveId = tbl_id
        objEmpMove.GetByPK()

        'EmployeeFilterUC.GetEmployeeInfo(objEmpMove.FK_EmployeeId)
        EmployeeFilterUC.EmployeeId = objEmpMove.FK_EmployeeId
        txtremarks.Text = objEmpMove.Remarks

        If objEmpMove.MoveDate > RadDatePicker1.MinDate Then
            RadDatePicker1.SelectedDate = objEmpMove.MoveDate
        Else
            RadDatePicker1.Clear()
        End If
        rmtToTime2.Text = SetTimeFormat(objEmpMove.MoveTime)
        ddlReason.SelectedValue = objEmpMove.FK_ReasonId
        FileExtension = objEmpMove.AttachedFile
        fuAttachFile.Visible = True
        lnbLeaveFile.Visible = True
        lnbRemove.Visible = True
        Dim fPath As String = "..\ManualEntryFiles\" + tbl_id.ToString() + objEmpMove.AttachedFile
        Dim fPathExist As String = String.Empty
        fPathExist = Server.MapPath("..\ManualEntryFiles\\" + tbl_id.ToString() + objEmpMove.AttachedFile)

        If String.IsNullOrEmpty(objEmpMove.AttachedFile) Then
            lnbLeaveFile.Visible = False
            lnbRemove.Visible = False
            lblNoAttachedFile.Visible = True
        ElseIf File.Exists(fPathExist) Then

            lnbLeaveFile.Visible = True
            lnbRemove.Visible = True
            lblNoAttachedFile.Visible = False
        Else
            lnbLeaveFile.Visible = False
            lnbRemove.Visible = False
            lblNoAttachedFile.Visible = True
        End If

    End Sub

    Protected Sub dgEmpAtt_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgEmpAtt.NeedDataSource
        dgEmpAtt.DataSource = Nothing
        objEmpMove = New Emp_Move
        If Not RadDatePicker1.SelectedDate Is Nothing Then
            objEmpMove.MoveDate = RadDatePicker1.SelectedDate
        Else
            objEmpMove.MoveDate = Nothing
        End If

        objEmpMove.FK_EmployeeId = EmployeeFilterUC.EmployeeId
        dtCurrent = objEmpMove.Getfilter()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgEmpAtt.DataSource = dv

        'FillGrid()
    End Sub

    Protected Sub RadDatePicker1_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles RadDatePicker1.SelectedDateChanged
        If EmployeeFilterUC.EmployeeId <> 0 Then
            FillGrid()
        End If
    End Sub

    Protected Sub lnkDownloadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        objEmpMove = New Emp_Move
        With objEmpMove
            .MoveId = tbl_id
            .GetByPK()

            Dim FileName As String = tbl_id.ToString() + .AttachedFile
            Dim FilePath As String = Server.MapPath("..\\ManualEntryFiles\") & FileName
            CtlCommon.Open_Download_File(FileName, FilePath)

        End With
    End Sub

    Protected Sub lnbRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnbRemove.Click
        If Not (tbl_id = 0) Then
            Dim fileName As String = tbl_id.ToString()
            Dim fPath As String = String.Empty
            fPath = Server.MapPath("..\ManualEntryFiles\\" + fileName + FileExtension)

            If File.Exists(fPath) Then
                File.Delete(fPath)
                lnbLeaveFile.Visible = False
                lnbRemove.Visible = False
                lblNoAttachedFile.Visible = True
                FileExtension = Nothing
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            End If
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillReasons()
        objTA_Reason = New TA_Reason()
        CtlCommon.FillTelerikDropDownList(ddlReason, objTA_Reason.GetNotIsScheduleTiming, Lang)
    End Sub

    Private Function SetTimeFormat(ByVal TimeToFormat As DateTime) As String
        Dim TimeM As String
        Dim TimeH As String

        If TimeToFormat.Hour.ToString.Length < 2 Then
            TimeH = "0" + TimeToFormat.Hour.ToString()
        Else
            TimeH = TimeToFormat.Hour.ToString()
        End If

        If TimeToFormat.Minute.ToString.Length < 2 Then
            TimeM = "0" + TimeToFormat.Minute.ToString()
        Else
            TimeM = TimeToFormat.Minute.ToString()
        End If

        Return TimeH + TimeM
    End Function

    Private Function DateToString(ByVal TempDate As Date) As String
        Dim tempDay As String
        Dim tempMonth As String

        If TempDate.Month.ToString.Length = 1 Then
            tempMonth = "0" + TempDate.Month.ToString
        Else
            tempMonth = TempDate.Month.ToString
        End If
        If TempDate.Day.ToString.Length = 1 Then
            tempDay = "0" + TempDate.Day.ToString
        Else
            tempDay = TempDate.Day.ToString
        End If
        Return TempDate.Year.ToString() + tempMonth + tempDay
    End Function

    Public Sub FillGrid()
        dgEmpAtt.DataSource = Nothing
        objEmpMove = New Emp_Move
        If Not RadDatePicker1.SelectedDate Is Nothing Then
            objEmpMove.MoveDate = RadDatePicker1.SelectedDate
        Else
            objEmpMove.MoveDate = Nothing
        End If
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            If .ShowEmployeeList = 0 Then
                Dim dt As DataTable
                dt = New DataTable
                objEmployee = New Employee
                With objEmployee
                    .FK_CompanyId = EmployeeFilterUC.CompanyId
                    .EmployeeNo = EmployeeFilterUC.EmpNo
                    .UserId = SessionVariables.LoginUser.ID
                    dt = .GetEmpByEmpNo()
                    If dt.Rows.Count > 0 Then
                        objEmpMove.FK_EmployeeId = dt.Rows(0)("EmployeeId")
                    End If

                End With
            Else
                objEmpMove.FK_EmployeeId = EmployeeFilterUC.EmployeeId
            End If

        End With

        dtCurrent = objEmpMove.Getfilter()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgEmpAtt.DataSource = dv
        dgEmpAtt.DataBind()

    End Sub

    Public Sub ClearAll()
        'TxtEmpNo.Text = ""
        'ddlLevels.SelectedValue = 0
        'ddlEmpList.SelectedValue = "-1"
        ddlReason.SelectedValue = -1
        RadDatePicker1.SelectedDate = Now
        rmtToTime2.Text = Now.TimeOfDay.ToString()
        txtremarks.Text = "Manual Entry"
        dgEmpAtt.SelectedIndexes.Clear()
        'rmtToTime2.Text = String.Empty
        ddlReason.SelectedIndex = -1

        fuAttachFile.Visible = True
        lnbLeaveFile.Visible = False
        lnbRemove.Visible = False
        lblNoAttachedFile.Visible = False
        FileExtension = Nothing

        tbl_id = 0
        EmployeeFilterUC.ClearValues()
        dgEmpAtt.DataSource = Nothing
        dgEmpAtt.DataBind()

    End Sub

    Public Sub ClearValues()
        EmployeeId = 0
        EntityId = 0
        CompanyId = 0
    End Sub

    Private Sub ArcivingMonths_DateValidation()
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        Dim ArchivingMonths As Integer = objAPP_Settings.ArchivingMonths

        If Not ArchivingMonths = 0 Then
            ArchivingMonths = ArchivingMonths
            Dim NewDate As Date = DateAdd(DateInterval.Month, ArchivingMonths, Date.Today)
            RadDatePicker1.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
        End If

    End Sub

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgEmpAtt.Skin))
    End Function

    Protected Sub dgEmpAtt_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgEmpAtt.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

#End Region

End Class
