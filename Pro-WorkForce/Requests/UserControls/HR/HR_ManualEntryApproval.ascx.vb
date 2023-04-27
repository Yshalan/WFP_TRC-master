Imports SmartV.UTILITIES
Imports TA.Employees
Imports TA.Admin
Imports Telerik.Web.UI
Imports TA.Definitions
Imports TA.SelfServices
Imports System.IO
Imports System.Data
Imports TA.DailyTasks

Partial Class Requests_UserControls_HR_HR_ManualEntryApproval
    Inherits System.Web.UI.UserControl

#Region "Class Variable"
    Private objEmp_MoveRequest As Emp_MoveRequest
    Public MsgLang As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Shared SortExepression As String
    Shared dtCurrent As DataTable
    Private Lang As CtlCommon.Lang
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objREADER_KEYS As READER_KEYS
    Private objEmp_Move As Emp_Move
    Private objAPP_Settings As APP_Settings
    Private objRecalculateRequest As RecalculateRequest

#End Region

#Region "Properties"

    Private Enum RequestStatus
        Pending = 1
        ApprovedByDM = 2
        ApprovedByHR = 3
        RejectedByDM = 4
        RejectedByHR = 5
        ApprovedByGM = 6
        RejectedByGM = 7
    End Enum

    Public Property MoveRequestId() As Integer
        Get
            Return ViewState("MoveRequestId")
        End Get
        Set(ByVal value As Integer)
            ViewState("MoveRequestId") = value
        End Set
    End Property

    Public Property LeaveApproval() As Integer
        Get
            Return ViewState("LeaveApproval")
        End Get
        Set(ByVal value As Integer)
            ViewState("LeaveApproval") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
            MsgLang = "ar"
        Else
            Lang = CtlCommon.Lang.EN
            MsgLang = "en"
        End If
        If Not IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                MsgLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                MsgLang = "en"
            End If

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

            lblManualEntryRequests.Text = ResourceManager.GetString("HRManualEntryHeader", CultureInfo)
            Page.Title = "Work Force Pro : :" + ResourceManager.GetString("HRManualEntryHeader", CultureInfo)

            ShowHideControls()
            FillGridView()
        End If
       
    End Sub

    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        objEmp_MoveRequest = New Emp_MoveRequest
        objRECALC_REQUEST = New RECALC_REQUEST
        objEmp_Move = New Emp_Move
        objAPP_Settings = New APP_Settings
        objAPP_Settings = objAPP_Settings.GetByPK()

        If (objAPP_Settings.LeaveApproval = 2 Or objAPP_Settings.LeaveApproval = 3) Then
            Dim MoveDate As DateTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("MoveDate"))
            Dim MoveTime As DateTime = CDate(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("MoveTime"))
            Dim EmployeeId As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_EmployeeId"))
            Dim AttachedFile As String = DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("AttachedFile").ToString()
            Dim MoveRequestId As String = DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("MoveRequestId")
            Dim Remarks As String = String.Empty

            If (DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Remarks").ToString() <> "") Then
                Remarks = CStr(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("Remarks").ToString())
            End If
            Dim ErrorMessage As String = String.Empty

            With objEmp_MoveRequest
                .MoveRequestId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("MoveRequestId"))
                .GetByPK()
                .Status = Convert.ToInt32(RequestStatus.ApprovedByHR).ToString
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .FK_HREmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                Err = .UpdateManualRequestStatus()

                objEmp_Move.AddManualEntryAllProcess(.FK_EmployeeId, .Type, .MoveDate, .MoveTime, .FK_ReasonId,
                                                    .Remarks, .Reader, .M_DATE_NUM, .M_TIME_NUM, .Status,
                                                    .SYS_Date, .IsManual, .CREATED_BY, .CREATED_DATE, .LAST_UPDATE_BY,
                                                    .LAST_UPDATE_DATE, .IsFromMobile, .MobileCoordinates, .IsRejected, .AttachedFile, False)
                strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                If (File.Exists(Server.MapPath("~\ManualEntryRequestFiles\\" + MoveRequestId.ToString() + AttachedFile))) Then
                    My.Computer.FileSystem.RenameFile(Server.MapPath("~\ManualEntryRequestFiles\\" + MoveRequestId.ToString() + AttachedFile), _
                                                      (objEmp_MoveRequest.MoveRequestId.ToString() + AttachedFile).ToString())

                    If (File.Exists(Server.MapPath("~\ManualEntryRequestFiles\\" + objEmp_MoveRequest.MoveRequestId.ToString() + AttachedFile))) Then
                        Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("~\ManualEntryRequestFiles\\" + objEmp_MoveRequest.MoveRequestId.ToString() + AttachedFile))
                        CopyFile.CopyTo(Server.MapPath("~\ManualEntryFiles\\" + objEmp_MoveRequest.MoveRequestId.ToString() + AttachedFile))
                    End If

                End If

                If Err = 0 Then
                    FillGridView()
                    Dim err2 As Integer = -1
                    If objAPP_Settings.ApprovalRecalMethod = 1 Then
                        objRECALC_REQUEST.EMP_NO = EmployeeId
                        objRECALC_REQUEST.VALID_FROM_NUM = DateToString(MoveDate)
                        objRECALC_REQUEST.RECALCULATE()
                        FillGridView()
                    Else
                        If Not MoveDate > Date.Today Then
                            objRecalculateRequest = New RecalculateRequest
                            With objRecalculateRequest
                                .Fk_EmployeeId = EmployeeId
                                .FromDate = MoveDate
                                .ToDate = MoveDate
                                .ImmediatelyStart = True
                                .RecalStatus = 0
                                .CREATED_BY = SessionVariables.LoginUser.ID
                                .Remarks = "Manual Entry Request Approval - SYSTEM"
                                err2 = .Add
                            End With
                        End If
                    End If
                    CtlCommon.ShowMessage(Me.Page, strMessage, "success")
                    FillGridView()
                Else
                    CtlCommon.ShowMessage(Me.Page, strMessage = ResourceManager.GetString("ErrorSave", CultureInfo), "error")
                End If
            End With

        ElseIf objAPP_Settings.LeaveApproval = 4 Then
            With objEmp_MoveRequest
                .Status = Convert.ToInt32(RequestStatus.ApprovedByHR).ToString
                .MoveRequestId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("MoveRequestId"))
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .FK_HREmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                Err = .UpdateManualRequestStatus()

                If Err = 0 Then
                    FillGridView()
                    strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
                    CtlCommon.ShowMessage(Me.Page, strMessage, "success")
                Else
                    CtlCommon.ShowMessage(Me.Page, strMessage = ResourceManager.GetString("ErrorSave", CultureInfo), "error")
                End If
            End With
        End If



    End Sub

    Protected Sub lnkReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReject.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim strMessage As String = String.Empty
        objEmp_MoveRequest = New Emp_MoveRequest
        Dim Err As Integer = 0
        With objEmp_MoveRequest
            .MoveRequestId = MoveRequestId
            .GetByPK()
            .Status = RequestStatus.RejectedByHR
            .RejectedReason = txtRejectedReason.Text.Trim(",")
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            Err = .UpdateManualRequestStatus()

            strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

            If Err = 0 Then
                FillGridView()
                CtlCommon.ShowMessage(Me.Page, strMessage, "success")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If
        End With
    End Sub

    Protected Sub dgrdManualEntryRequest_OnRowCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles dgrdManualEntryRequest.ItemCommand
        If (e.CommandName = "reject") Then
            txtRejectedReason.Text = String.Empty
            MoveRequestId = e.CommandArgument
            mpeRejectPopupManual.Show()
        ElseIf e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdManualEntryRequest_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdManualEntryRequest.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item
            Dim MoveRequestId As Integer = item.GetDataKeyValue("MoveRequestId")

            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
                item("StatusName").Text = DirectCast(item.FindControl("hdnStatusNameAr"), HiddenField).Value
                item("ReasonName").Text = DirectCast(item.FindControl("hdnReasonAr"), HiddenField).Value
            End If

            Dim AttachedFile As String = item.GetDataKeyValue("AttachedFile").ToString()
            If AttachedFile = "" Or AttachedFile = "&nbsp;" Then
                DirectCast(e.Item.FindControl("lnbView"), LinkButton).Visible = False
            Else
                DirectCast(e.Item.FindControl("lnbView"), LinkButton).Visible = True
            End If

            Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            scriptManager__1.RegisterPostBackControl(DirectCast(e.Item.FindControl("lnbView"), LinkButton))

        End If
    End Sub

    Protected Sub dgrdManualEntryRequest_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdManualEntryRequest.NeedDataSource

        objEmp_MoveRequest = New Emp_MoveRequest()
        objEmp_MoveRequest.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
        objEmp_MoveRequest.Status = RequestStatus.ApprovedByDM
        dtCurrent = objEmp_MoveRequest.GetByHR()
        If Not (dtCurrent Is Nothing) Then
            Dim dv As New DataView(dtCurrent)
            dv.Sort = SortExepression
            dgrdManualEntryRequest.DataSource = dv
        End If

    End Sub

    Protected Sub lnkDownloadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        objEmp_MoveRequest = New Emp_MoveRequest
        With objEmp_MoveRequest
            .MoveRequestId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("MoveRequestId").ToString())
            .GetByPK()
            Dim FileName As String = .MoveRequestId.ToString() + .AttachedFile
            Dim FilePath As String = Server.MapPath("..\\ManualEntryRequestFiles\") & FileName
            CtlCommon.Open_Download_File(FileName, FilePath)

        End With
    End Sub

#End Region

#Region "Methods"

    Public Sub FillGridView()
        Try
            Dim dtManualRequet As New DataTable
            objEmp_MoveRequest = New Emp_MoveRequest
            objEmp_MoveRequest.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            dtManualRequet = objEmp_MoveRequest.GetByHR()
            dgrdManualEntryRequest.DataSource = dtManualRequet
            dgrdManualEntryRequest.DataBind()

            If Not dtManualRequet Is Nothing Then
                If dtManualRequet.Rows.Count > 0 Then
                    If Not lblRequestNo.Text.Contains(":") Then
                        lblRequestNo.Text += " : " + (dtManualRequet.Rows.Count).ToString()
                    Else
                        Dim strArr() As String = lblRequestNo.Text.Split(":")
                        lblRequestNo.Text = strArr(0) + " : " + (dtManualRequet.Rows.Count).ToString()
                    End If
                Else
                    lblRequestNo.Visible = False
                    divrdbProceed.Visible = False
                    divbtnProceed.Visible = False
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

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

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdManualEntryRequest.Skin))
    End Function

#End Region

#Region "Proceed All"

    Protected Sub btnProceed_Click(sender As Object, e As System.EventArgs) Handles btnProceed.Click
        Dim checked As Boolean
        Dim ErrNo As Integer = 1
        Dim Msg As String = ""
        Dim Count As Integer = 0
        If rdbProceed.SelectedValue = "" Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Page, " الرجاء اختيار عملية معينة", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please select one Process", "info")
            End If
            Exit Sub
        End If
        If rdbProceed.SelectedValue = 2 And txtRejectAllReason.Text = "" Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Page, "الرجاء ادخال سبب الرفض", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please Insert Rejection Reason", "info")
            End If
            Exit Sub
        End If

        For Each item As GridDataItem In dgrdManualEntryRequest.Items
            If DirectCast(item.FindControl("chk"), CheckBox).Checked = True Then
                If rdbProceed.SelectedValue = 1 Then
                    AcceptAll(item, ErrNo, Msg)
                ElseIf rdbProceed.SelectedValue = 2 Then
                    RejectAll(item, ErrNo, Msg)
                End If
                Count = Count + 1
            End If
        Next

        If Count > 0 Then


            If ErrNo = 0 Then
                FillGridView()
                txtRejectAllReason.Text = String.Empty
                rdbProceed.SelectedIndex = "-1"
                divRejectAllReason.Visible = False
                CtlCommon.ShowMessage(Me.Page, Msg, "success")
            ElseIf ErrNo = 1 Then

            Else

                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")

            End If
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Page, " الرجاء اختيار طلب مغادرة واحد على الاقل", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please select one Permission Request at least", "info")
            End If

        End If
    End Sub

    Private Sub AcceptAll(ByVal item As GridDataItem, ByRef ErrNo As Integer, ByRef Msg As String)
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        objEmp_MoveRequest = New Emp_MoveRequest
        objRECALC_REQUEST = New RECALC_REQUEST
        objEmp_Move = New Emp_Move
        objAPP_Settings = New APP_Settings
        objAPP_Settings = objAPP_Settings.GetByPK()

        If (objAPP_Settings.LeaveApproval = 2 Or objAPP_Settings.LeaveApproval = 3) Then
            Dim MoveDate As DateTime = CDate(item.GetDataKeyValue("MoveDate"))
            Dim MoveTime As DateTime = CDate(item.GetDataKeyValue("MoveTime"))
            Dim EmployeeId As Integer = CInt(item.GetDataKeyValue("FK_EmployeeId"))
            Dim AttachedFile As String = item.GetDataKeyValue("AttachedFile").ToString()
            Dim MoveRequestId As String = item.GetDataKeyValue("MoveRequestId")
            Dim Remarks As String = String.Empty

            If (item.GetDataKeyValue("Remarks").ToString() <> "") Then
                Remarks = CStr(item.GetDataKeyValue("Remarks").ToString())
            End If
            Dim ErrorMessage As String = String.Empty

            With objEmp_MoveRequest
                .Status = Convert.ToInt32(RequestStatus.ApprovedByHR).ToString
                .MoveRequestId = CInt(item.GetDataKeyValue("MoveRequestId"))
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .FK_HREmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                Err = .UpdateManualRequestStatus()
                .GetByPK()
                objEmp_Move.AddManualEntryAllProcess(.FK_EmployeeId, .Type, .MoveDate, .MoveTime, .FK_ReasonId,
                                                    .Remarks, .Reader, .M_DATE_NUM, .M_TIME_NUM, .Status,
                                                    .SYS_Date, .IsManual, .CREATED_BY, .CREATED_DATE, .LAST_UPDATE_BY,
                                                    .LAST_UPDATE_DATE, .IsFromMobile, .MobileCoordinates, .IsRejected, .AttachedFile, False)
                strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                If (File.Exists(Server.MapPath("~\ManualEntryRequestFiles\\" + MoveRequestId.ToString() + AttachedFile))) Then
                    'My.Computer.FileSystem.RenameFile(Server.MapPath("~\ManualEntryRequestFiles\\" + MoveRequestId.ToString() + AttachedFile), _
                    '                                  (objEmp_MoveRequest.MoveRequestId.ToString() + AttachedFile).ToString())

                    If (File.Exists(Server.MapPath("~\ManualEntryRequestFiles\\" + objEmp_MoveRequest.MoveRequestId.ToString() + AttachedFile))) Then
                        Dim CopyFile As FileInfo = New FileInfo(Server.MapPath("~\ManualEntryRequestFiles\\" + objEmp_MoveRequest.MoveRequestId.ToString() + AttachedFile))
                        CopyFile.CopyTo(Server.MapPath("~\ManualEntryFiles\\" + objEmp_MoveRequest.MoveRequestId.ToString() + AttachedFile))
                    End If

                End If

                Dim err2 As Integer = -1
                If objAPP_Settings.ApprovalRecalMethod = 1 Then
                    objRECALC_REQUEST.EMP_NO = EmployeeId
                    objRECALC_REQUEST.VALID_FROM_NUM = DateToString(MoveDate)
                    objRECALC_REQUEST.RECALCULATE()
                Else
                    If Not MoveDate > Date.Today Then
                        objRecalculateRequest = New RecalculateRequest
                        With objRecalculateRequest
                            .Fk_EmployeeId = EmployeeId
                            .FromDate = MoveDate
                            .ToDate = MoveDate
                            .ImmediatelyStart = True
                            .RecalStatus = 0
                            .CREATED_BY = SessionVariables.LoginUser.ID
                            .Remarks = "Manual Entry Request Approval - SYSTEM"
                            err2 = .Add
                        End With
                    End If
                End If
            End With

        ElseIf objAPP_Settings.LeaveApproval = 4 Then
            With objEmp_MoveRequest
                .Status = Convert.ToInt32(RequestStatus.ApprovedByHR).ToString
                .MoveRequestId = CInt(item.GetDataKeyValue("MoveRequestId"))
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .FK_HREmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                Err = .UpdateManualRequestStatus()

                If Err = 0 Then
                    'FillGridView()
                    strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
                    '    CtlCommon.ShowMessage(Me.Page, strMessage)
                    'Else
                    '    CtlCommon.ShowMessage(Me.Page, strMessage = ResourceManager.GetString("ErrorSave", CultureInfo))
                End If
            End With
        End If



        ErrNo = Err
        Msg = strMessage
        item = Nothing
    End Sub

    Private Sub RejectAll(ByVal item As GridDataItem, ByRef ErrNo As Integer, ByRef Msg As String)
         CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim strMessage As String = String.Empty
        objEmp_MoveRequest = New Emp_MoveRequest
        Dim Err As Integer = 0
        With objEmp_MoveRequest
            .MoveRequestId = CInt(item.GetDataKeyValue("MoveRequestId"))
            .GetByPK()
            .Status = RequestStatus.RejectedByHR
            .RejectedReason = txtRejectAllReason.Text.Trim(",")
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            Err = .UpdateManualRequestStatus()

            strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
            ErrNo = Err
            Msg = strMessage
            'If Err = 0 Then
            '    FillGridView()
            '    CtlCommon.ShowMessage(Me.Page, strMessage)
            'Else
            '    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo))
            'End If
        End With
    End Sub

    Protected Sub rdbProceed_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdbProceed.SelectedIndexChanged
        If rdbProceed.SelectedValue = 2 Then
            divRejectAllReason.Visible = True
        Else
            divRejectAllReason.Visible = False
        End If
    End Sub

    Private Sub ShowHideControls()
        objAPP_Settings = New APP_Settings

        With objAPP_Settings
            .GetByPK()
            If .HasMultiApproval Then
                divrdbProceed.Visible = True
                divbtnProceed.Visible = True
            Else
                divrdbProceed.Visible = False
                divbtnProceed.Visible = False
            End If
        End With
    End Sub
#End Region

End Class
