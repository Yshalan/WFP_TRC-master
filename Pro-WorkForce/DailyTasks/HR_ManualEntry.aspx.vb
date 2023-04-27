Imports TA.Definitions
Imports TA.Admin
Imports TA.DailyTasks
Imports TA.Employees
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports System.Data
Imports TA.Security
Imports System.IO
Imports SmartV.UTILITIES.ProjectCommon


Partial Class Definitions_HR_ManualEntry
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objTA_Reason As TA_Reason
    Private objHR_Emp_Move As HR_Emp_Move
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

    Public Property MoveRequestId() As Integer
        Get
            Return ViewState("MoveRequestId")
        End Get
        Set(ByVal value As Integer)
            ViewState("MoveRequestId") = value
        End Set
    End Property

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

    Public Property FileName() As String
        Get
            Return ViewState("FileName")
        End Get
        Set(ByVal value As String)
            ViewState("FileName") = value
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
            PageHeader1.HeaderText = ResourceManager.GetString("HRManualEntry", CultureInfo)

            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()

            If objAPP_Settings.AllowEditManualEntryRequestDate = True Then
                RadDatePicker1.Enabled = True
            Else
                RadDatePicker1.Enabled = False
            End If

            If objAPP_Settings.AllowEditManualEntryRequestTime = True Then
                rmtToTime2.Enabled = True
            Else
                rmtToTime2.Enabled = False
            End If

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
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim temp_date As Date
        Dim temp_str_date As String
        Dim formats As String() = New String() {"HHmm"}
        Dim temp_time As DateTime
        Dim temp_str_time As String
        Dim err As Integer

        If fuAttachFile.HasFile = False Then
            If objAPP_Settings.AttachmentIsMandatoryManualEntryRequest = True Then
                If Lang = CtlCommon.Lang.AR Then
                    CtlCommon.ShowMessage(Me.Page, "يرجى ارفاق ملف!", "info")
                Else
                    CtlCommon.ShowMessage(Me.Page, "Please Attach File!", "info")
                End If
                Exit Sub
            End If
        End If

        If fuAttachFile.HasFile Then
            FileExtension = Path.GetExtension(fuAttachFile.PostedFile.FileName)
            FileName = Path.GetFileName(fuAttachFile.PostedFile.FileName).ToString()

            Dim fPath As String = String.Empty
            fPath = Server.MapPath("..\HR_ManualEntryRequestFiles\\" + FileName)
            If File.Exists(fPath) Then
                File.Delete(fPath)
                fuAttachFile.PostedFile.SaveAs(fPath)
            Else
                fuAttachFile.PostedFile.SaveAs(fPath)
            End If
        End If

        objHR_Emp_Move = New HR_Emp_Move
        objRECALC_REQUEST = New RECALC_REQUEST
        objREADER_KEYS = New READER_KEYS
        objRECALC_REQUEST.EMP_NO = EmployeeFilterUC.EmployeeId

        objEmployee = New Employee
        EmployeeId = EmployeeFilterUC.EmployeeId
        If objEmployee.GetEmpNo(EmployeeFilterUC.EmployeeId) = False Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NumIsntForThisLevel", CultureInfo), "info")
            Return
        End If
        objHR_Emp_Move.FK_EmployeeId = EmployeeId
        objHR_Emp_Move.Remarks = txtremarks.Text
        objHR_Emp_Move.Reader = String.Empty
        objHR_Emp_Move.Status = 1
        objHR_Emp_Move.IsManual = True
        objHR_Emp_Move.Remarks = txtremarks.Text
        objREADER_KEYS.READER_KEY = ddlReason.SelectedValue
        objREADER_KEYS.GetByPK()

        objHR_Emp_Move.FK_ReasonId = objREADER_KEYS.CHANGE_TO
        objHR_Emp_Move.Type = objREADER_KEYS.Type
        If RadDatePicker1.SelectedDate IsNot Nothing Then
            objHR_Emp_Move.MoveDate = RadDatePicker1.SelectedDate
            temp_date = RadDatePicker1.SelectedDate
            temp_str_date = DateToString(temp_date)
            objHR_Emp_Move.M_DATE_NUM = temp_str_date
            objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)
        Else
            objHR_Emp_Move.MoveDate = Nothing
            objHR_Emp_Move.M_DATE_NUM = Nothing
        End If
        DateTime.TryParseExact(rmtToTime2.Text, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, temp_time)

        objHR_Emp_Move.MoveTime = temp_time
        temp_str_time = temp_time.Minute + temp_time.Hour * 60
        objHR_Emp_Move.M_TIME_NUM = temp_str_time

        objHR_Emp_Move.MoveRequestId = MoveRequestId
        Dim fileUploadExtension As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)

        If String.IsNullOrEmpty(fileUploadExtension) Then
            fileUploadExtension = FileExtension
        End If
        objHR_Emp_Move.AttachedFile = FileExtension
        If tbl_id = 0 Then
            objHR_Emp_Move.CREATED_BY = SessionVariables.LoginUser.ID
            err = objHR_Emp_Move.Add()
            MoveRequestId = objHR_Emp_Move.MoveRequestId

        Else
            objHR_Emp_Move.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            objHR_Emp_Move.MoveRequestId = tbl_id
            MoveRequestId = tbl_id
            err = objHR_Emp_Move.Update()
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

            Dim fPath As String = String.Empty
            fPath = Server.MapPath("..\HR_ManualEntryRequestFiles\\" + MoveRequestId.ToString() + FileExtension)
            Dim fPathExist As String = String.Empty
            fPathExist = Server.MapPath("..\HR_ManualEntryRequestFiles\\" + FileName)

            If File.Exists(fPathExist) Then
                File.Delete(fPath)
                Rename(fPathExist, fPath)

            End If

            ddlReason.SelectedValue = -1
            'RadDatePicker1.SelectedDate = Now
            rmtToTime2.Text = Now.TimeOfDay.ToString()
            txtremarks.Text = "Manual Entry"
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")

        End If
    End Sub

    Protected Sub ibtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnDelete.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim err As Integer = -99
        For Each item As GridDataItem In dgEmpAtt.Items
            Dim cb As CheckBox = DirectCast(item.FindControl("chk"), CheckBox)
            If cb.Checked Then
                Dim tbl_id As Integer = Convert.ToInt32(item.GetDataKeyValue("MoveRequestId").ToString())
                objHR_Emp_Move = New HR_Emp_Move
                objHR_Emp_Move.MoveRequestId = tbl_id
                err = objHR_Emp_Move.Delete()
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

            If item("IsRejected").Text = "&nbsp;" Then
                If Lang = CtlCommon.Lang.AR Then
                    item("IsRejected").Text = "قيد الدراسة"
                Else
                    item("IsRejected").Text = "Pending"
                End If

            ElseIf item("IsRejected").Text = "True" Then
                If Lang = CtlCommon.Lang.AR Then
                    item("IsRejected").Text = "مرفوض"
                Else
                    item("IsRejected").Text = "Rejected"
                End If
            Else
                If Lang = CtlCommon.Lang.AR Then
                    item("IsRejected").Text = "موافقة"
                Else
                    item("IsRejected").Text = "Approved"
                End If
            End If

        End If
    End Sub

    Protected Sub dgEmpAtt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgEmpAtt.SelectedIndexChanged

        tbl_id = CInt(CType(dgEmpAtt.SelectedItems(0), GridDataItem).GetDataKeyValue("MoveRequestId").ToString())
        objHR_Emp_Move = New HR_Emp_Move
        MoveRequestId = tbl_id
        objHR_Emp_Move.MoveRequestId = tbl_id
        objHR_Emp_Move.GetByPK()

        EmployeeFilterUC.EmployeeId = objHR_Emp_Move.FK_EmployeeId
        txtremarks.Text = objHR_Emp_Move.Remarks

        If objHR_Emp_Move.MoveDate > RadDatePicker1.MinDate Then
            RadDatePicker1.SelectedDate = objHR_Emp_Move.MoveDate
        Else
            RadDatePicker1.Clear()
        End If
        rmtToTime2.Text = SetTimeFormat(objHR_Emp_Move.MoveTime)
        ddlReason.SelectedValue = objHR_Emp_Move.FK_ReasonId

        FileExtension = objHR_Emp_Move.AttachedFile
        fuAttachFile.Visible = True
        lnbLeaveFile.Visible = True
        lnbRemove.Visible = True
        Dim fPath As String = "..\HR_ManualEntryRequestFiles\" + MoveRequestId.ToString() + objHR_Emp_Move.AttachedFile
        Dim fPathExist As String = String.Empty
        fPathExist = Server.MapPath("..\HR_ManualEntryRequestFiles\\" + MoveRequestId.ToString() + objHR_Emp_Move.AttachedFile)

        If String.IsNullOrEmpty(objHR_Emp_Move.AttachedFile) Then
            lnbLeaveFile.Visible = False
            lnbRemove.Visible = False
            lblNoAttachedFile.Visible = True
        ElseIf File.Exists(fPathExist) Then
            lnbLeaveFile.HRef = fPath
            lnbLeaveFile.Visible = True
            lnbRemove.Visible = True
            lblNoAttachedFile.Visible = False
        Else
            lnbLeaveFile.Visible = False
            lnbRemove.Visible = False
            lblNoAttachedFile.Visible = True
        End If

    End Sub

    Protected Sub lnbRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnbRemove.Click
        If Not (MoveRequestId = 0) Then
            Dim fileName As String = MoveRequestId.ToString()
            Dim fPath As String = String.Empty
            fPath = Server.MapPath("..\HR_ManualEntryRequestFiles\\" + fileName + FileExtension)

            If File.Exists(fPath) Then
                File.Delete(fPath)
                lnbLeaveFile.Visible = False
                lnbRemove.Visible = False
                lblNoAttachedFile.Visible = True
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully"), "success")
            End If
        End If
    End Sub

    Protected Sub dgEmpAtt_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgEmpAtt.NeedDataSource
        dgEmpAtt.DataSource = Nothing
        objHR_Emp_Move = New HR_Emp_Move
        If Not RadDatePicker1.SelectedDate Is Nothing Then
            objHR_Emp_Move.MoveDate = RadDatePicker1.SelectedDate
        Else
            objHR_Emp_Move.MoveDate = Nothing
        End If

        objHR_Emp_Move.FK_EmployeeId = EmployeeFilterUC.EmployeeId
        dtCurrent = objHR_Emp_Move.Getfilter()
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

#End Region

#Region "Methods"

    Private Sub FillReasons()
        objTA_Reason = New TA_Reason()
        CtlCommon.FillTelerikDropDownList(ddlReason, objTA_Reason.GetAll, Lang)
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
        objHR_Emp_Move = New HR_Emp_Move
        If Not RadDatePicker1.SelectedDate Is Nothing Then
            objHR_Emp_Move.MoveDate = RadDatePicker1.SelectedDate
        Else
            objHR_Emp_Move.MoveDate = Nothing
        End If

        objHR_Emp_Move.FK_EmployeeId = EmployeeFilterUC.EmployeeId
        dtCurrent = objHR_Emp_Move.Getfilter()
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
