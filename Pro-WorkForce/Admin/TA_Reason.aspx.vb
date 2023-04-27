Imports System.Data
Imports TA.Employees
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.Admin
Imports TA.Security

Partial Class Emp_TA_Reason
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objTA_Reason As TA_Reason
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Properties"

    Private Property ReasonCode() As Integer
        Get
            Return ViewState("ReasonCode")
        End Get
        Set(ByVal value As Integer)
            ViewState("ReasonCode") = value
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

    Protected Sub dgrdVwTaReason_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdVwTaReason.Skin))
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

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            FillGridView()
            trIsInsideWork.Visible = False
            RadComboBoxType.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(IIf(Lang = CtlCommon.Lang.EN, ResourceManager.GetString("PleaseSelect", CultureInfo), ResourceManager.GetString("PleaseSelect", CultureInfo)), -1))
            rfvType.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            PageHeaderTaReason.HeaderText = ResourceManager.GetString("TAReason", CultureInfo)
            btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdVwTaReason.ClientID + "');")
        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not pnlTaReason.FindControl(row("AddBtnName")) Is Nothing Then
                        pnlTaReason.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not pnlTaReason.FindControl(row("DeleteBtnName")) Is Nothing Then
                        pnlTaReason.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not pnlTaReason.FindControl(row("EditBtnName")) Is Nothing Then
                        pnlTaReason.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not pnlTaReason.FindControl(row("PrintBtnName")) Is Nothing Then
                        pnlTaReason.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdVwTaReason.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objTA_Reason = New TA_Reason()
                objTA_Reason.ReasonCode = Convert.ToInt32(row.GetDataKeyValue("ReasonCode").ToString())
                errNum = objTA_Reason.Delete()

            End If
        Next

        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGridView()
            ClearAll()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        objTA_Reason = New TA_Reason()
        Dim errorNum As Integer = -1
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        ' Set data into object for Add / Update
        With objTA_Reason
            .ReasonCode = txtRadReasonCode.Text
            .ReasonArabicName = txtReasonArabicName.Text
            .ReasonName = txtReasonName.Text
            .IsScheduleTiming = chkIsScheduleTiming.Checked

            If chkIsScheduleTiming.Checked = True Then
                .Type = "I"
            Else
                .Type = RadComboBoxType.SelectedValue
            End If
            .IsInsideWork = chkIsInsideWork.Checked
            .IsFirstIn = chkFirstIn.Checked
            .IsLastOut = chkLastOut.Checked
        End With

        If ReasonCode = 0 Then
            objTA_Reason.ReasonCode = txtRadReasonCode.Text
            If objTA_Reason.IS_Exist() = True Then
                txtRadReasonCode.Focus()
                CtlCommon.ShowMessage(Me, ResourceManager.GetString("CodeExist", CultureInfo), "info")
                Return
            Else
                ' Do add operation 
                errorNum = objTA_Reason.Add()

                If errorNum = 0 Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                ElseIf errorNum = -5 Then
                    CtlCommon.ShowMessage(Me, ResourceManager.GetString("CodeExist", CultureInfo), "info")
                ElseIf errorNum = -6 Then
                    CtlCommon.ShowMessage(Me, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
                ElseIf errorNum = -7 Then
                    CtlCommon.ShowMessage(Me, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
                End If

            End If
        Else
            ' Do update operations
            objTA_Reason.ReasonCode = ReasonCode
            errorNum = objTA_Reason.Update()
            txtRadReasonCode.ReadOnly = False

            If errorNum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ElseIf errorNum = -6 Then
                CtlCommon.ShowMessage(Me, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
            ElseIf errorNum = -7 Then
                CtlCommon.ShowMessage(Me, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "success")
            End If
        End If

        If errorNum = 0 Then
            FillGridView()
            ClearAll()
        End If
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object,
                                 ByVal e As System.EventArgs) Handles btnClear.Click
        FillGridView()
        ClearAll()
    End Sub

    Protected Sub dgrdVwTaReason_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdVwTaReason.NeedDataSource
        objTA_Reason = New TA_Reason()
        dgrdVwTaReason.DataSource = objTA_Reason.GetAll()
    End Sub

    Protected Sub dgrdVwTaReason_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdVwTaReason.SelectedIndexChanged
        txtRadReasonCode.ReadOnly = True
        ReasonCode = Convert.ToInt32(DirectCast(dgrdVwTaReason.SelectedItems(0), GridDataItem).GetDataKeyValue("ReasonCode").ToString())
        FillControls()
    End Sub

    Protected Sub RadComboBoxType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxType.SelectedIndexChanged
        If (RadComboBoxType.SelectedIndex = 2) Then
            trIsInsideWork.Visible = True
            chkIsInsideWork.Checked = False
            chkLastOut.Checked = False

            dvFirstIn.Visible = False
            chkFirstIn.Checked = False

        ElseIf RadComboBoxType.SelectedIndex = 1 Then
            trIsInsideWork.Visible = False
            chkIsInsideWork.Checked = False
            chkLastOut.Checked = False

            dvFirstIn.Visible = True
            chkFirstIn.Checked = False
        End If
    End Sub

    Private Sub chkIsScheduleTiming_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsScheduleTiming.CheckedChanged
        If chkIsScheduleTiming.Checked = True Then
            dvType.Visible = False
            trIsInsideWork.Visible = False
            RadComboBoxType.SelectedValue = -1
        Else
            dvType.Visible = True
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillControls()
        ' This function will display the data of a specific record on the 
        ' form controls
        objTA_Reason = New TA_Reason()
        objTA_Reason.ReasonCode = ReasonCode
        objTA_Reason.GetByPK()
        With objTA_Reason
            txtRadReasonCode.Text = .ReasonCode
            txtReasonName.Text = .ReasonName
            txtReasonArabicName.Text = .ReasonArabicName
            chkIsScheduleTiming.Checked = .IsScheduleTiming

            If chkIsScheduleTiming.Checked = False Then
                dvType.Visible = True
                RadComboBoxType.SelectedValue = .Type
                If (.Type = "I") Then
                    trIsInsideWork.Visible = False
                    chkIsInsideWork.Checked = False
                    chkLastOut.Checked = False

                    dvFirstIn.Visible = True
                    chkFirstIn.Checked = .IsFirstIn
                Else
                    trIsInsideWork.Visible = True
                    chkIsInsideWork.Checked = .IsInsideWork
                    chkLastOut.Checked = .IsLastOut
                    dvFirstIn.Visible = False
                    chkFirstIn.Checked = False
                End If
            Else
                dvType.Visible = False
            End If


        End With
    End Sub

    Private Sub FillGridView()
        Try
            objTA_Reason = New TA_Reason()
            dgrdVwTaReason.DataSource = objTA_Reason.GetAll()
            dgrdVwTaReason.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ClearAll()

        ' Re allow the user to enter the reason code 
        txtRadReasonCode.ReadOnly = False

        ' Clear controls
        txtReasonArabicName.Text = String.Empty
        txtRadReasonCode.Text = String.Empty
        txtReasonName.Text = String.Empty
        RadComboBoxType.SelectedIndex = 0
        trIsInsideWork.Visible = False
        chkIsInsideWork.Checked = False
        chkLastOut.Checked = False
        dvFirstIn.Visible = False
        chkFirstIn.Checked = False
        ' Reset to prepare to the next add operation


        ReasonCode = 0




    End Sub

#End Region

End Class
