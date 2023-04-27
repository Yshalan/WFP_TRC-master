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
Imports TA.SelfServices
Imports TA.Lookup

Partial Class SelfServices_UpdateTransactionsRequest
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objTA_Reason As TA_Reason
    Private objEmp_MoveUpdateRequest As Emp_MoveUpdateRequest
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objREADER_KEYS As READER_KEYS
    Private objEmployee As Employee
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objVersion As SmartV.Version.version
    Private objOrgCompany As OrgCompany
    Private objRequestStatus As New RequestStatus
    Private objAPP_Settings As APP_Settings
    Private objEmp_Move As Emp_Move

#End Region

#Region "Properties"

    Private Enum UpdateApplicationType
        UpdateTransaction = 1
        DeleteTransaction = 2
    End Enum

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

    Private Property IsFirstGrid() As String
        Get
            Return ViewState("IsFirstGrid")
        End Get
        Set(ByVal value As String)
            ViewState("IsFirstGrid") = value
        End Set
    End Property

    Public Property MoveId() As Integer
        Get
            Return ViewState("MoveId")
        End Get
        Set(ByVal value As Integer)
            ViewState("MoveId") = value
        End Set
    End Property

    Public Property UpdateTransactionType() As Integer
        Get
            Return ViewState("UpdateTransactionType")
        End Get
        Set(ByVal value As Integer)
            ViewState("UpdateTransactionType") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub SelfServices_UpdateTransactionsRequest_PreRenderComplete(sender As Object, e As EventArgs) Handles Me.PreRenderComplete

        For Each row As DataRow In dtCurrentControls.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not Update1.FindControl(row("AddBtnName")) Is Nothing Then
                        Update1.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not Update1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        Update1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not Update1.FindControl(row("EditBtnName")) Is Nothing Then
                        Update1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not Update1.FindControl(row("PrintBtnName")) Is Nothing Then
                        Update1.FindControl(row("PrintBtnName")).Visible = False
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                txtremarks.Text = "تحديث الحركة من الخدمة الذاتية"
            Else
                Lang = CtlCommon.Lang.EN
                txtremarks.Text = "Update Transaction From Self-Service"
            End If

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            FillReasons()
            FillStatus()
            FillGrid()
            FillTransactionsGrid()
            IsEmployeeRequired = True
            IsLevelRequired = False

            rmtToTime2.Text = Now.TimeOfDay.ToString()
            RadDatePicker1.SelectedDate = Now
            RadDatePicker1.MaxDate = Now

            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            If Not objAPP_Settings.AllowedBefore = Nothing Then
                'Dim intMaxDate As Integer
                'intMaxDate = Now.Day + objAPP_Settings.AllowedBefore
                RadDatePicker1.MinDate = Date.Now.AddDays(-objAPP_Settings.AllowedBefore)
            End If


            Emp_UpdateTransactionsRequest.HeaderText = ResourceManager.GetString("UpdateTransactionRequest", CultureInfo)
            'mvEmpLeaverequest.ActiveViewIndex = 0
            HideShowViews()
            dtpFromDateSearch.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            dtpToDateSearch.SelectedDate = dd

        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdManualEntryRequest.ClientID + "');")

        Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        scriptManager__1.RegisterPostBackControl(lnbLeaveFile)

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrentControls.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not Update1.FindControl(row("AddBtnName")) Is Nothing Then
                        Update1.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not Update1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        Update1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not Update1.FindControl(row("EditBtnName")) Is Nothing Then
                        Update1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not Update1.FindControl(row("PrintBtnName")) Is Nothing Then
                        Update1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        Dim ErrorMessage As String = String.Empty
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

        If Not fuAttachFile.PostedFile Is Nothing Then
            If fuAttachFile.HasFile Then
                FileExtension = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                FileName = Path.GetFileName(fuAttachFile.PostedFile.FileName).ToString()

                Dim fPath As String = String.Empty
                fPath = Server.MapPath("..\UpdateTransactionsRequestFiles\\" + FileName)
                If File.Exists(fPath) Then
                    File.Delete(fPath)
                    fuAttachFile.PostedFile.SaveAs(fPath)
                Else
                    fuAttachFile.PostedFile.SaveAs(fPath)
                End If
            End If
        End If
        objEmp_MoveUpdateRequest = New Emp_MoveUpdateRequest
        objRECALC_REQUEST = New RECALC_REQUEST
        objREADER_KEYS = New READER_KEYS
        objRECALC_REQUEST.EMP_NO = SessionVariables.LoginUser.FK_EmployeeId

        objEmployee = New Employee
        EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        If objEmployee.GetEmpNo(EmployeeId) = False Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NumIsntForThisLevel", CultureInfo), "info")
            Return
        End If
        objEmp_MoveUpdateRequest.FK_EmployeeId = EmployeeId
        objEmp_MoveUpdateRequest.Remarks = txtremarks.Text
        objEmp_MoveUpdateRequest.Reader = String.Empty
        objEmp_MoveUpdateRequest.Status = 1
        objEmp_MoveUpdateRequest.IsManual = True
        objEmp_MoveUpdateRequest.Remarks = txtremarks.Text
        objEmp_MoveUpdateRequest.MoveId = MoveId
        objEmp_MoveUpdateRequest.UpdateTransactionType = UpdateTransactionType
        objREADER_KEYS.READER_KEY = ddlReason.SelectedValue
        DateTime.TryParseExact(rmtToTime2.Text, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, temp_time)
        objEmp_MoveUpdateRequest.MoveTime = temp_time
        objREADER_KEYS.GetByPK()

        'For Check the Request if exists or not
        If (objEmp_MoveUpdateRequest.IfExists_EmpMoveRequest(EmployeeId, RadDatePicker1.SelectedDate, ddlReason.SelectedValue, temp_time, ErrorMessage) = False) Then
            If (ErrorMessage <> String.Empty) Then
                CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                Return
            End If
        End If
        ''


        If objAPP_Settings.ManualEntryNo <> Nothing Then
            If (objEmp_MoveUpdateRequest.ValidateManualEntryRequestPerDay(EmployeeId, RadDatePicker1.SelectedDate, ErrorMessage) = False) Then
                If (ErrorMessage <> String.Empty) Then
                    CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                    Return
                End If
            End If

        End If

        'For Check if have In/Out in Same Day
        If objAPP_Settings.AllowMoreOneManualEntry = False Then
            If (objEmp_MoveUpdateRequest.CheckHasInOrOut(EmployeeId, RadDatePicker1.SelectedDate, ddlReason.Text, ddlReason.SelectedValue, temp_time, ErrorMessage) = False) Then
                If (ErrorMessage <> String.Empty) Then
                    CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                    Return
                End If
            End If
        End If
        ''

        objEmp_MoveUpdateRequest.FK_ReasonId = objREADER_KEYS.CHANGE_TO
        objEmp_MoveUpdateRequest.Type = objREADER_KEYS.Type
        If RadDatePicker1.SelectedDate IsNot Nothing Then
            objEmp_MoveUpdateRequest.MoveDate = RadDatePicker1.SelectedDate
            temp_date = RadDatePicker1.SelectedDate
            temp_str_date = DateToString(temp_date)
            objEmp_MoveUpdateRequest.M_DATE_NUM = temp_str_date
            objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)
        Else
            objEmp_MoveUpdateRequest.MoveDate = Nothing
            objEmp_MoveUpdateRequest.M_DATE_NUM = Nothing
        End If
        DateTime.TryParseExact(rmtToTime2.Text, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, temp_time)

        objEmp_MoveUpdateRequest.MoveTime = temp_time
        temp_str_time = temp_time.Minute + temp_time.Hour * 60
        objEmp_MoveUpdateRequest.M_TIME_NUM = temp_str_time

        objEmp_MoveUpdateRequest.MoveRequestId = MoveRequestId
        If Not fuAttachFile.PostedFile Is Nothing Then
            Dim fileUploadExtension As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
            If String.IsNullOrEmpty(fileUploadExtension) Then
                fileUploadExtension = FileExtension
            End If
            objEmp_MoveUpdateRequest.AttachedFile = FileExtension
        End If

        If tbl_id = 0 Then
            objEmp_MoveUpdateRequest.CREATED_BY = SessionVariables.LoginUser.ID
            err = objEmp_MoveUpdateRequest.Add()
            MoveRequestId = objEmp_MoveUpdateRequest.MoveRequestId

        Else
            objEmp_MoveUpdateRequest.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            objEmp_MoveUpdateRequest.MoveRequestId = tbl_id
            MoveRequestId = tbl_id
            err = objEmp_MoveUpdateRequest.Update()
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
            If Not fuAttachFile.PostedFile Is Nothing Then
                Dim fPath As String = String.Empty
                fPath = Server.MapPath("..\UpdateTransactionsRequestFiles\\" + MoveRequestId.ToString() + FileExtension)
                Dim fPathExist As String = String.Empty
                fPathExist = Server.MapPath("..\UpdateTransactionsRequestFiles\\" + FileName)

                If File.Exists(fPathExist) Then
                    File.Delete(fPath)
                    Rename(fPathExist, fPath)

                End If
            End If
            ddlReason.SelectedValue = -1
            RadDatePicker1.SelectedDate = Now
            rmtToTime2.Text = Now.TimeOfDay.ToString()
            txtremarks.Text = "Update Transaction From Self-Service"
            'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo))
            If Lang = CtlCommon.Lang.AR Then
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Redirect", "ShowMessageAndRedirect('تم إرسال الطلب الى المدير المباشر','../SelfServices/UpdateTransactionsRequest.aspx');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Redirect", "ShowMessageAndRedirect('The Request has been Sent to Direct Manager','../SelfServices/UpdateTransactionsRequest.aspx');", True)
            End If
            FillGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")

        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        For Each Item As GridDataItem In dgrdManualEntryRequest.Items
            Dim cb As CheckBox = DirectCast(Item.FindControl("chk"), CheckBox)
            If cb.Checked Then

                ' Get Permission Id from hidden label
                Dim MoveRequestId As Integer = Item.GetDataKeyValue("MoveRequestId")

                ' Get Employee Id from hidden label
                Dim Status As Integer = Convert.ToInt32(Item.GetDataKeyValue("Status"))
                Dim AttachedFile As String = Item.GetDataKeyValue("AttachedFile").ToString()

                If (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.Pending) Or (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByGeneralManager)) Or _
                    (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByHumanResource)) Or (Status = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedbyManager))) Then
                    ' Delete current checked item
                    objEmp_MoveUpdateRequest = New Emp_MoveUpdateRequest
                    objEmp_MoveUpdateRequest.MoveRequestId = MoveRequestId
                    errNum = objEmp_MoveUpdateRequest.Delete()

                    If errNum = 0 Then
                        Dim fileName As String = MoveRequestId.ToString()
                        Dim fPath As String = String.Empty
                        fPath = Server.MapPath("..\UpdateTransactionsRequestFiles\\" + fileName + AttachedFile)
                        If File.Exists(fPath) Then
                            File.Delete(fPath)
                        End If
                    End If
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SelectPendingStatus"), "info")
                    Return
                End If
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully"), "success")
            FillGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete"), "error")
        End If
        ClearAll()
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Protected Sub lnbRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnbRemove.Click
        If Not (MoveRequestId = 0) Then
            Dim fileName As String = MoveRequestId.ToString()
            Dim fPath As String = String.Empty
            fPath = Server.MapPath("..\UpdateTransactionsRequestFiles\\" + fileName + FileExtension)

            If File.Exists(fPath) Then
                File.Delete(fPath)
                lnbLeaveFile.Visible = False
                lnbRemove.Visible = False
                lblNoAttachedFile.Visible = True
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully"), "success")
            End If
        End If
    End Sub

    Protected Sub dgrdManualEntryRequest_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdManualEntryRequest.NeedDataSource
        dgrdManualEntryRequest.DataSource = Nothing
        objEmp_MoveUpdateRequest = New Emp_MoveUpdateRequest
        If Not RadDatePicker1.SelectedDate Is Nothing Then
            objEmp_MoveUpdateRequest.MoveDate = RadDatePicker1.SelectedDate
        Else
            objEmp_MoveUpdateRequest.MoveDate = Nothing
        End If

        objEmp_MoveUpdateRequest.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        dtCurrent = objEmp_MoveUpdateRequest.Getfilter()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdManualEntryRequest.DataSource = dv

        'FillGrid()
    End Sub

    Protected Sub RadDatePicker1_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles RadDatePicker1.SelectedDateChanged
        If EmployeeId <> 0 Then
            FillGrid()
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("../SelfServices/UpdateTransactionsRequest.aspx?Id=1")
    End Sub

    Protected Sub dgrdManualEntryRequest_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdManualEntryRequest.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item
            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("Status").ToString())) And (Not item.GetDataKeyValue("Status").ToString() = "")) Then

                If ((item.GetDataKeyValue("Status") <> Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByGeneralManager)) And _
                    (item.GetDataKeyValue("Status") <> Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByHumanResource)) And _
                    (item.GetDataKeyValue("Status") <> Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedbyManager))) Then
                End If

            End If

            If Lang = CtlCommon.Lang.AR Then
                item("name").Text = item.GetDataKeyValue("EmployeeArabicName").ToString()
                item("StatusName").Text = item.GetDataKeyValue("StatusNameArabic").ToString()
                item("ReasonName").Text = item.GetDataKeyValue("ReasonArabicName").ToString()
            End If

        End If
    End Sub

    Protected Sub dgrdManualEntryRequest_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdManualEntryRequest.SelectedIndexChanged
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        mvEmpLeaverequest.SetActiveView(viewAddManualEntryRequest)
        MoveRequestId = Convert.ToInt32(DirectCast(dgrdManualEntryRequest.SelectedItems(0), GridDataItem).GetDataKeyValue("MoveRequestId"))
        objEmp_MoveUpdateRequest = New Emp_MoveUpdateRequest
        With objEmp_MoveUpdateRequest
            .MoveRequestId = MoveRequestId
            .GetByPK()
            ddlReason.SelectedValue = .FK_ReasonId
            RadDatePicker1.MinDate = .MoveDate
            RadDatePicker1.SelectedDate = .MoveDate
            rmtToTime2.Text = SetTimeFormat(objEmp_MoveUpdateRequest.MoveTime)
            txtremarks.Text = .Remarks
            FileExtension = .AttachedFile


            fuAttachFile.Visible = True
            lnbLeaveFile.Visible = True
            lnbRemove.Visible = True
            Dim fPath As String = "..\UpdateTransactionsRequestFiles\" + MoveRequestId.ToString() + .AttachedFile
            Dim fPathExist As String = String.Empty
            fPathExist = Server.MapPath("..\UpdateTransactionsRequestFiles\\" + MoveRequestId.ToString() + .AttachedFile)

            Select Case objEmp_MoveUpdateRequest.Status
                Case 1
                    ControlsStatus(False)
                Case 2
                    ControlsStatus(False)
                Case 3
                    ControlsStatus(False)
                Case 4
                    ControlsStatus(True)
            End Select

            If String.IsNullOrEmpty(.AttachedFile) Then
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

            btnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)
        End With
    End Sub

    Protected Sub lnkDownloadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        objEmp_MoveUpdateRequest = New Emp_MoveUpdateRequest
        With objEmp_MoveUpdateRequest
            .MoveRequestId = MoveRequestId
            .GetByPK()

            Dim FileName As String = MoveRequestId.ToString() + .AttachedFile
            Dim FilePath As String = Server.MapPath("..\\UpdateTransactionsRequestFiles\") & FileName
            CtlCommon.Open_Download_File(FileName, FilePath)

        End With
    End Sub

    Protected Sub lnkUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        MoveId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("MoveId").ToString())
        UpdateTransactionType = UpdateApplicationType.UpdateTransaction
        FillRequestForm_ByEmp_Move()
        mvEmpLeaverequest.ActiveViewIndex = 1
    End Sub

    Protected Sub dgrdTransactions_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdTransactions.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            'If Not item("Status").Text = "&nbsp;" Then
            '    If Lang = CtlCommon.Lang.AR Then
            '        DirectCast(item.FindControl("lnkUpdate"), LinkButton).Text = item("StatusNameArabic").Text
            '        DirectCast(item.FindControl("lnkUpdate"), LinkButton).Enabled = False
            '    Else
            '        DirectCast(item.FindControl("lnkUpdate"), LinkButton).Text = item("StatusName").Text
            '        DirectCast(item.FindControl("lnkUpdate"), LinkButton).Enabled = False
            '    End If
            'End If
            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = item.GetDataKeyValue("EmployeeArabicName").ToString()
                item("ReasonName").Text = item.GetDataKeyValue("ReasonArabicName").ToString()
            End If

        End If
    End Sub

    Protected Sub dgrdTransactions_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdTransactions.NeedDataSource
        objAPP_Settings = New APP_Settings
        objEmp_Move = New Emp_Move

        Dim dt As DataTable
        Dim ManualEntryAllowedBefore As String
        With objAPP_Settings
            .GetByPK()
            ManualEntryAllowedBefore = .AllowedBefore
        End With
        objEmp_Move.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        objEmp_Move.ManualEntryAllowedBefore = ManualEntryAllowedBefore

        dt = objEmp_Move.Get_ByEmp_DateDiff
        If Not dt Is Nothing Then
            If dt.Rows.Count > 0 Then
                dgrdTransactions.DataSource = dt
            End If
        End If
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        MoveId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("MoveId").ToString())
        UpdateTransactionType = UpdateApplicationType.DeleteTransaction
        FillRequestForm_ByEmp_MoveForDelete()
        mvEmpLeaverequest.ActiveViewIndex = 1
    End Sub

#End Region

#Region "Methods"

    Private Sub ControlsStatus(ByVal Status As Boolean)
        RadDatePicker1.Enabled = Status
        ddlReason.Enabled = Status
        fuAttachFile.Enabled = Status
        txtremarks.Enabled = Status
        btnSave.Visible = Status
        btnClear.Visible = Status
        rmtToTime2.Enabled = Status
    End Sub

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
        objAPP_Settings = New APP_Settings
        Dim ManualEntryAllowedBefore As String
        With objAPP_Settings
            .GetByPK()
            ManualEntryAllowedBefore = .AllowedBefore
        End With
        dgrdManualEntryRequest.DataSource = Nothing
        objEmp_MoveUpdateRequest = New Emp_MoveUpdateRequest
        If Not RadDatePicker1.SelectedDate Is Nothing Then
            objEmp_MoveUpdateRequest.MoveDate = RadDatePicker1.SelectedDate
        Else
            objEmp_MoveUpdateRequest.MoveDate = Nothing
        End If
        If Not dtpFromDateSearch.SelectedDate Is Nothing Then
            objEmp_MoveUpdateRequest.FromDate = dtpFromDateSearch.SelectedDate
        Else
            objEmp_MoveUpdateRequest.FromDate = Nothing
        End If
        If Not dtpToDateSearch.SelectedDate Is Nothing Then
            objEmp_MoveUpdateRequest.ToDate = dtpToDateSearch.SelectedDate
        Else
            objEmp_MoveUpdateRequest.ToDate = Nothing
        End If
        objEmp_MoveUpdateRequest.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        dtCurrent = objEmp_MoveUpdateRequest.Getfilter()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdManualEntryRequest.DataSource = dv
        dgrdManualEntryRequest.DataBind()

    End Sub

    Private Sub FillTransactionsGrid()
        objAPP_Settings = New APP_Settings
        objEmp_Move = New Emp_Move

        Dim dt As DataTable
        Dim ManualEntryAllowedBefore As String
        With objAPP_Settings
            .GetByPK()
            ManualEntryAllowedBefore = .AllowedBefore
        End With
        objEmp_Move.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        objEmp_Move.ManualEntryAllowedBefore = ManualEntryAllowedBefore

        dt = objEmp_Move.Get_ByEmp_DateDiff
        If Not dt Is Nothing Then
            If dt.Rows.Count > 0 Then
                dgrdTransactions.DataSource = dt
                dgrdTransactions.DataBind()
            End If
        End If


    End Sub

    Protected Sub btnGet_Click(sender As Object, e As System.EventArgs) Handles btnGet.Click
        objEmp_MoveUpdateRequest = New Emp_MoveUpdateRequest()
        objEmp_MoveUpdateRequest.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        objEmp_MoveUpdateRequest.Status = ddlStatus.SelectedValue
        objEmp_MoveUpdateRequest.MoveDate = dtpFromDateSearch.SelectedDate
        objEmp_MoveUpdateRequest.ToDate = dtpToDateSearch.SelectedDate
        dgrdManualEntryRequest.DataSource = objEmp_MoveUpdateRequest.Getfilter()
        dgrdManualEntryRequest.DataBind()
    End Sub

    Public Sub ClearAll()
        'TxtEmpNo.Text = ""
        'ddlLevels.SelectedValue = 0
        'ddlEmpList.SelectedValue = "-1"
        ddlReason.SelectedValue = -1
        RadDatePicker1.SelectedDate = Now
        rmtToTime2.Text = Now.TimeOfDay.ToString()
        txtremarks.Text = ""
        dgrdManualEntryRequest.SelectedIndexes.Clear()
        'rmtToTime2.Text = String.Empty
        ddlReason.SelectedIndex = -1
        tbl_id = 0
        EmployeeId = 0
        'dgrdManualEntryRequest.DataSource = Nothing
        'dgrdManualEntryRequest.DataBind()
        UpdateTransactionType = 0
    End Sub

    Public Sub ClearValues()
        EmployeeId = 0
        EntityId = 0
        CompanyId = 0
    End Sub

    Private Sub FillStatus()
        objRequestStatus = New RequestStatus
        Dim dt As DataTable = Nothing
        dt = Nothing
        dt = objRequestStatus.GetAll
        ProjectCommon.FillRadComboBox(ddlStatus, dt, "StatusName", _
                                     "StatusNameArabic", "StatusId")
    End Sub

    Private Sub HideShowViews()
        'objAPP_Settings = New APP_Settings
        'With objAPP_Settings
        '    .GetByPK()
        '    IsFirstGrid = .IsFirstGrid
        '    If Request.QueryString("id") = 0 Then
        '        If .IsFirstGrid Then
        '            mvEmpLeaverequest.SetActiveView(viewManualEntryRequests)
        '        Else
        '            mvEmpLeaverequest.SetActiveView(viewAddManualEntryRequest)
        '        End If
        '    Else
        mvEmpLeaverequest.SetActiveView(viewManualEntryRequests)
        '    End If
        'End With
    End Sub

    Private Sub FillRequestForm_ByEmp_Move()
        objEmp_Move = New Emp_Move
        With objEmp_Move
            .MoveId = MoveId
            .GetByPK()

            RadDatePicker1.MinDate = .MoveDate
            RadDatePicker1.SelectedDate = .MoveDate
            ddlReason.SelectedValue = .FK_ReasonId
            If Lang = CtlCommon.Lang.AR Then
                txtremarks.Text = "طلب تعديل حركة من الخدمات الذاتية"
            Else
                txtremarks.Text = "Update Transaction Type From Self-Service"
            End If
            rmtToTime2.Text = SetTimeFormat(.MoveTime)

        End With
    End Sub

    Private Sub FillRequestForm_ByEmp_MoveForDelete()
        objEmp_Move = New Emp_Move
        With objEmp_Move
            .MoveId = MoveId
            .GetByPK()

            RadDatePicker1.MinDate = .MoveDate
            RadDatePicker1.SelectedDate = .MoveDate
            ddlReason.SelectedValue = .FK_ReasonId
            rmtToTime2.Text = SetTimeFormat(.MoveTime)
            If Lang = CtlCommon.Lang.AR Then
                txtremarks.Text = "طلب حذف حركة من الخدمات الذاتية"
            Else
                txtremarks.Text = "Delete Transaction From Self-Service"
            End If
            ddlReason.Enabled = False
        End With
    End Sub

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdManualEntryRequest.Skin))
    End Function

    Protected Sub dgrdManualEntryRequest_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdManualEntryRequest.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar2_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon1() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdTransactions.Skin))
    End Function

    Protected Sub dgrdTransactions_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdTransactions.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            TransactionsFilter.FireApplyCommand()
        End If
    End Sub

#End Region

End Class
