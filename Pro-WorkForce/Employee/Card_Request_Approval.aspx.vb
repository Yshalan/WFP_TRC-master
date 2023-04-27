Imports Telerik.Web.UI
Imports SmartV.UTILITIES
Imports TA.Card_Request
Imports System.Data

Partial Class Employee_Card_Request_Approval
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objCard_Request As Card_Request
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Public strLang As String
    Public MsgLang As String

#End Region

#Region "Properties"

    Private Enum CardRequestStatus

        Pending = 1
        RejectedBy2ndManager = 13
        RejectedbyManager = 4
        ApprovedByManager = 2
        ApprovedBy2ndManager = 12
        UnderPrinting = 14
        Printed = 15
        RejectedbyHR = 5
        RejectedbyGM = 7
        ApprovedbyHR = 3
        ApprovedbyGM = 6


    End Enum

    Public Property CardRequestId() As Integer
        Get
            Return ViewState("CardRequestId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CardRequestId") = value
        End Set
    End Property

    Public Property ReasonId() As Integer
        Get
            Return ViewState("ReasonId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ReasonId") = value
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

#End Region

#Region "Page Events"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else

            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                strLang = "ar"
                MsgLang = "ar"
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                Lang = CtlCommon.Lang.EN
                strLang = "en"
                MsgLang = "en"
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
            FillStatus()
            FillGrid()
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("CardRequestApproval", CultureInfo)
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdCardRequests.Skin))
    End Function

    Protected Sub dgrdCardRequests_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdCardRequests.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            Dim strStatus As String
            Dim strReason As String
            item = e.Item

            If Lang = CtlCommon.Lang.AR Then
                Select Case item.GetDataKeyValue("ReasonId")
                    Case 1
                        strReason = "جديد"
                    Case 2
                        strReason = "تجديد"
                    Case 3
                        strReason = "بدل تالف"
                    Case 4
                        strReason = "بدل فاقد"
                    Case 5
                        strReason = "تحديث بيانات"
                    Case 6
                        strReason = "اخرى"
                End Select
            Else

                Select Case item.GetDataKeyValue("ReasonId")
                    Case 1
                        strReason = "New"
                    Case 2
                        strReason = "Renew"
                    Case 3
                        strReason = "Damaged"
                    Case 4
                        strReason = "Replacement"
                    Case 5
                        strReason = "Update Information"
                    Case 6
                        strReason = "Other"
                End Select
            End If

            If Not item("ReasonId").Text = -1 Then
                item("ReasonId").Text = strReason
            End If

            If Lang = CtlCommon.Lang.AR Then
                Select Case item.GetDataKeyValue("Status")
                    Case 1
                        strStatus = "قيد الاعتماد"
                    Case 14
                        strStatus = "قيد الطباعة"
                    Case 15
                        strStatus = "تمت الطباعة"
                    Case 4
                        strStatus = "مرفوض"
                    Case 2
                        strStatus = "موافقة من قبل المدير"
                    Case 9
                        strStatus = "موافق من قبل المدير الأول"
                End Select
            Else

                Select Case item.GetDataKeyValue("Status")
                    Case 1
                        strStatus = "Pending"
                    Case 14
                        strStatus = "UnderPrinting"
                    Case 15
                        strStatus = "Printed"
                    Case 4
                        strStatus = "Rejected"
                    Case 2
                        strStatus = "Approved By Manager"
                    Case 9
                        strStatus = "Approved By 1st Manager"

                End Select
            End If

            item("Status").Text = strStatus

            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
            End If

            'If Not ddlStatus.SelectedValue = Convert.ToInt32(CardRequestStatus.Pending).ToString Then
            '    If Not ddlStatus.SelectedValue = -1 Then
            '        dgrdCardRequests.Columns(6).Visible = False
            '        dgrdCardRequests.Columns(7).Visible = False
            '    Else
            '        dgrdCardRequests.Columns(6).Visible = True
            '        dgrdCardRequests.Columns(7).Visible = True
            '    End If
            'End If
        End If
    End Sub

    Protected Sub lnkReject_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim NxtApproval As Integer
        Dim CardApproval As Integer
        Dim Str As String
        CardRequestId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("CardRequestId").ToString())
        NxtApproval = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("NextApprovalStatus").ToString())
        CardApproval = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("CardApproval").ToString())

        Dim txt As TextBox = TryCast((DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).FindControl("txtReason")), TextBox)
        Str = txt.Text

        If Str = String.Empty Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("InsertRemarks", CultureInfo), "error")
        Else
            RejectCardRequest(NxtApproval, CardApproval, Str)
        End If


    End Sub

    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim NxtApproval As Integer
        Dim CardApproval As Integer

        CardRequestId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("CardRequestId").ToString())
        NxtApproval = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("NextApprovalStatus").ToString())
        CardApproval = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("CardApproval").ToString())

        AcceptCardRequest(NxtApproval, CardApproval)
    End Sub

#End Region

#Region "Methods"

    Private Sub FillGrid()
        objCard_Request = New Card_Request
        With objCard_Request
            .Status = 1
            'ddlStatus.SelectedValue
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            dtCurrent = .GetAll_CardRequest()
            '.GetAll_Inner()
        End With
        dgrdCardRequests.DataSource = dtCurrent
        dgrdCardRequests.DataBind()
    End Sub

    Private Sub AcceptCardRequest(ByVal NxtApproval As Integer, ByVal CardApproval As Integer)
        objCard_Request = New Card_Request
        Dim err As Integer = -1

        If (CardApproval = 1 And NxtApproval = 2) Then

            With objCard_Request
                .CardRequestId = CardRequestId
                .Status = NxtApproval
                .Remarks = String.Empty
                err = .UpdateRequestStatus()
            End With
            If err = 0 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
                FillGrid()
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If

        ElseIf (CardApproval = 3 Or CardApproval = 4 Or (CardApproval = 1 And NxtApproval = 9)) Then
            With objCard_Request
                .CardRequestId = CardRequestId
                .Status = NxtApproval
                .Remarks = String.Empty
                err = .UpdateRequestStatus()
            End With
            If err = 0 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
                FillGrid()
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        End If

        
    End Sub

    Private Sub RejectCardRequest(ByVal NxtApproval As Integer, ByVal CardApproval As Integer, ByVal Remarks As String)
        objCard_Request = New Card_Request
        Dim err As Integer = -1
        With objCard_Request

            .CardRequestId = CardRequestId

            If NxtApproval = 9 Then
                .Status = CardRequestStatus.RejectedbyManager
            ElseIf NxtApproval = 2 Then
                .Status = CardRequestStatus.RejectedBy2ndManager

            Else
                .Status = CardRequestStatus.RejectedbyHR

            End If

            .Remarks = Remarks
            err = .UpdateRequestStatus()
        End With
        If err = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            FillGrid()
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Private Sub FillStatus()
        'If SessionVariables.CultureInfo = "ar-JO" Then
        '    Dim item1 As New RadComboBoxItem
        '    Dim item2 As New RadComboBoxItem
        '    Dim item3 As New RadComboBoxItem
        '    Dim item4 As New RadComboBoxItem
        '    Dim item5 As New RadComboBoxItem

        '    item1.Value = -1
        '    item1.Text = "--الرجاء الاختيار--"
        '    ddlStatus.Items.Add(item1)
        '    item2.Value = 1
        '    item2.Text = "قيد الاعتماد"
        '    ddlStatus.Items.Add(item2)
        '    item3.Value = 2
        '    item3.Text = "قيد الطباعة"
        '    ddlStatus.Items.Add(item3)
        '    item4.Value = 3
        '    item4.Text = "تمت الطباعة"
        '    ddlStatus.Items.Add(item4)
        '    item5.Value = 4
        '    item5.Text = "مرفوض"
        '    ddlStatus.Items.Add(item5)

        'Else
        '    Dim item1 As New RadComboBoxItem
        '    Dim item2 As New RadComboBoxItem
        '    Dim item3 As New RadComboBoxItem
        '    Dim item4 As New RadComboBoxItem
        '    Dim item5 As New RadComboBoxItem

        '    item1.Value = -1
        '    item1.Text = "--Please Select--"
        '    ddlStatus.Items.Add(item1)
        '    item2.Value = 1
        '    item2.Text = "Pending"
        '    ddlStatus.Items.Add(item2)
        '    item3.Value = 2
        '    item3.Text = "Approved By Manager"
        '    ddlStatus.Items.Add(item3)
        '    item4.Value = 15
        '    item4.Text = "Printed"
        '    ddlStatus.Items.Add(item4)

        '    item5.Value = 15
        '    item5.Text = "Printed"
        '    ddlStatus.Items.Add(item5)

        '    item6.Value = 4
        '    item6.Text = "Rejected by Manager"
        '    ddlStatus.Items.Add(item6)

        'End If
        'ddlStatus.SelectedIndex = 1
    End Sub

#End Region

    'Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlStatus.SelectedIndexChanged
    '    'If Not ddlStatus.SelectedValue = -1 Then
    '    objCard_Request = New Card_Request
    '    With objCard_Request
    '        .Status = 1
    '        'ddlStatus.SelectedValue
    '        .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
    '        dtCurrent = .GetAll_CardRequest()
    '        'dtCurrent = .GetAll_Inner()
    '    End With
    '    dgrdCardRequests.DataSource = dtCurrent
    '    dgrdCardRequests.DataBind()
    '    'Else
    '    '    'objCard_Request = New Card_Request
    '    '    'With objCard_Request
    '    '    '    .Status = 1
    '    '    '    dtCurrent = .GetAll_Inner()
    '    '    'End With
    '    '    dgrdCardRequests.DataSource = Nothing
    '    '    dgrdCardRequests.DataBind()
    '    'End If
    'End Sub
End Class
