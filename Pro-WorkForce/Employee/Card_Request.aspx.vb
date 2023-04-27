Imports Telerik.Web.UI
Imports SmartV.UTILITIES
Imports TA.Card_Request
Imports System.Data
Imports TA.Security
Imports TA.Admin

Partial Class Employee_Card_Request
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objCard_Request As Card_Request
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objEmp_Designation As Emp_Designation
    Private objCardType As CardTypes

#End Region

#Region "Properties"

    Private Enum CardRequestStatus

        Pending = 1
        UnderPrinting = 2
        Printed = 3
        Rejected = 4

    End Enum

    Public Property CardRequestId() As Integer
        Get
            Return ViewState("CardRequestId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CardRequestId") = value
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

    Public Property ReasonId() As Integer
        Get
            Return ViewState("ReasonId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ReasonId") = value
        End Set
    End Property

    Public Property dtCardRequest() As DataTable
        Get
            Return ViewState("dtCardRequest")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCardRequest") = value
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
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        If Not EmployeeFilterUC.EmployeeId = 0 Then

            If EmployeeFilterUC.EmpDesignation = 0 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("NoDesignation", CultureInfo), "warning")
                ddlDesignation.ClearSelection()
                FillCardTypes(ddlDesignation.SelectedValue)
                ddlReason.ClearSelection()
            Else
                ddlDesignation.SelectedValue = EmployeeFilterUC.EmpDesignation.ToString()
                If ddlDesignation.SelectedValue <> "-1" Then
                    FillCardTypes(ddlDesignation.SelectedValue)
                End If
            End If

        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            FillReason()
            FillGrid()
            FillDesignations()
            EmployeeFilterUC.IsEmployeeRequired = True
            EmployeeFilterUC.CompanyRequiredValidationGroup = btnSave.ValidationGroup
            EmployeeFilterUC.EmployeeRequiredValidationGroup = btnSave.ValidationGroup
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("CardRequest", CultureInfo)
        End If
        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdCardRequests.ClientID + "');")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
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

    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        Dim err As Integer = -1
        objCard_Request = New Card_Request
        With objCard_Request
            '
            .FK_EmployeeId = EmployeeFilterUC.EmployeeId
            .ReasonId = ddlReason.SelectedValue
            If .ReasonId = 6 Then
                .OtherReason = txtOther.Text
            Else
                .OtherReason = ""
            End If

            .Status = 1
            .Remarks = String.Empty
            .CardType = ddlCardDesign.SelectedValue
            .CREATED_BY = SessionVariables.LoginUser.ID
            err = .Add()

        End With
        'objCard_Request = New Card_Request
        'With objCard_Request
        '    .FK_EmployeeId = EmployeeFilterUC.EmployeeId
        '    .ReasonId = ddlReason.SelectedValue
        '    .Remarks = String.Empty
        '    If ddlReason.SelectedValue = 6 Then
        '        .OtherReason = txtOther.Text
        '    Else
        '        .OtherReason = Nothing
        '    End If
        '    .Status = CardRequestStatus.Pending
        '    .CREATED_BY = SessionVariables.LoginUser.ID
        '    If CardRequestId = 0 Then
        '        err = .Add()
        '    Else
        '        .CardRequestId = CardRequestId
        '        .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
        '        err = .Update
        '    End If
        'End With

        If err = 0 Then
            If CardRequestId = 0 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            End If
            FillGrid()
            ClearAll()
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub dgrdCardRequests_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdCardRequests.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            Dim strStatus As String

            item = e.Item
            If Not item.GetDataKeyValue("ReasonId") = -1 Then
                ReasonId = item.GetDataKeyValue("ReasonId").ToString()
                item("ReasonId").Text = ddlReason.Items.Item(ReasonId).Text
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
                End Select
            End If

            item("Status").Text = strStatus

            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
            End If

        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdCardRequests.Skin))
    End Function

    Protected Sub ddlReason_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlReason.SelectedIndexChanged
        If Not ddlReason.SelectedValue = -1 Then
            If ddlReason.SelectedValue = 6 Then
                trOther.Visible = True
                rfvOther.Enabled = True
            Else
                trOther.Visible = False
                rfvOther.Enabled = False
            End If
        End If
    End Sub


    Protected Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdCardRequests.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                CardRequestId = Convert.ToInt32(row.GetDataKeyValue("CardRequestId").ToString())
                objCard_Request = New Card_Request
                objCard_Request.CardRequestId = CardRequestId
                errNum = objCard_Request.Delete()
                With strBuilder

                End With
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo))
            FillGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo))
        End If
        ClearAll()
    End Sub

    Protected Sub dgrdCardRequests_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dgrdCardRequests.SelectedIndexChanged
        FillControls()
    End Sub

#End Region

#Region "Methods"

    Private Sub FillReason()
        If SessionVariables.CultureInfo = "ar-JO" Then
            Dim item1 As New RadComboBoxItem
            Dim item2 As New RadComboBoxItem
            Dim item3 As New RadComboBoxItem
            Dim item4 As New RadComboBoxItem
            Dim item5 As New RadComboBoxItem
            Dim item6 As New RadComboBoxItem
            Dim item7 As New RadComboBoxItem
            item1.Value = -1
            item1.Text = "--الرجاء الاختيار--"
            ddlReason.Items.Add(item1)
            item2.Value = 1
            item2.Text = "جديد"
            ddlReason.Items.Add(item2)
            item3.Value = 2
            item3.Text = "تجديد"
            ddlReason.Items.Add(item3)
            item4.Value = 3
            item4.Text = "بدل تالف"
            ddlReason.Items.Add(item4)
            item5.Value = 4
            item5.Text = "بدل فاقد"
            ddlReason.Items.Add(item5)
            item6.Value = 5
            item6.Text = "تحديث بيانات"
            ddlReason.Items.Add(item6)
            item7.Value = 6
            item7.Text = "اخرى"
            ddlReason.Items.Add(item7)
        Else
            Dim item1 As New RadComboBoxItem
            Dim item2 As New RadComboBoxItem
            Dim item3 As New RadComboBoxItem
            Dim item4 As New RadComboBoxItem
            Dim item5 As New RadComboBoxItem
            Dim item6 As New RadComboBoxItem
            Dim item7 As New RadComboBoxItem
            item1.Value = -1
            item1.Text = "--Please Select--"
            ddlReason.Items.Add(item1)
            item2.Value = 1
            item2.Text = "New"
            ddlReason.Items.Add(item2)
            item3.Value = 2
            item3.Text = "Renew"
            ddlReason.Items.Add(item3)
            item4.Value = 3
            item4.Text = "Damaged"
            ddlReason.Items.Add(item4)
            item5.Value = 4
            item5.Text = "Replacement"
            ddlReason.Items.Add(item5)
            item6.Value = 5
            item6.Text = "Update Information"
            ddlReason.Items.Add(item6)
            item7.Value = 6
            item7.Text = "Other"
            ddlReason.Items.Add(item7)
        End If
    End Sub

    Private Sub FillGrid()
        objCard_Request = New Card_Request
        With objCard_Request
            .Status = 1
            dtCurrent = .GetAll_Inner()
        End With
        dgrdCardRequests.DataSource = dtCurrent
        dgrdCardRequests.DataBind()

        dtCardRequest = dtCurrent

    End Sub

    Private Sub ClearAll()
        EmployeeFilterUC.ClearValues()
        ddlReason.SelectedValue = -1
        trOther.Visible = False
        txtOther.Text = String.Empty
        CardRequestId = 0
    End Sub

    Private Sub FillControls()
        objCard_Request = New Card_Request
        CardRequestId = DirectCast(dgrdCardRequests.SelectedItems(0), GridDataItem).GetDataKeyValue("CardRequestId").ToString().Trim
        With objCard_Request
            .CardRequestId = CardRequestId
            .GetByPK()
            EmployeeFilterUC.EmployeeId = .FK_EmployeeId
            EmployeeFilterUC.IsEntityClick = "True"
            EmployeeFilterUC.GetEmployeeInfo(.FK_EmployeeId)
            EmployeeFilterUC.EmployeeId = .FK_EmployeeId
            ddlReason.SelectedValue = .ReasonId
            If ReasonId = 6 Then
                trOther.Visible = True
                txtOther.Text = .OtherReason
                rfvOther.Enabled = True
            End If
        End With
    End Sub
    Private Sub FillCardTypes(ByVal Fk_designation As Integer)
        objCardType = New CardTypes
        With objCardType
            CtlCommon.FillTelerikDropDownList(ddlCardDesign, .GetAllByDesignation(Fk_designation), Lang)
        End With
    End Sub
    Private Sub FillDesignations()
        objEmp_Designation = New Emp_Designation
        With objEmp_Designation
            CtlCommon.FillTelerikDropDownList(ddlDesignation, .GetAll, Lang)
        End With



    End Sub
#End Region


    Protected Sub ddlDesignation_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        FillCardTypes(ddlDesignation.SelectedValue)
    End Sub

    Protected Sub dgrdCardRequests_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)

        dgrdCardRequests.DataSource = dtCardRequest

    End Sub
End Class
