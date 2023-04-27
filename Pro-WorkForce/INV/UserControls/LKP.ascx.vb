Imports System.Data
Imports LKP
Imports SmartV.UTILITIES
Imports System.Globalization
Imports System.Resources
Imports SmartV.UTILITIES.CtlCommon
Imports SmartV.UTILITIES.ProjectCommon
Imports TA.Definitions
Imports System
Imports Telerik.Web.UI
Imports System.Text
Imports System.Windows.Forms
Imports System.Web.UI.WebControls
Imports System.Windows.Forms.Control
Imports TA.Security

Partial Class INV_UserControls_LKP
    Inherits System.Web.UI.UserControl


#Region "Class Variables"
    Private objinv As LKP_table
    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
#End Region

#Region "Properties"
    Public Property INKid() As Integer
        Get
            Return ViewState("INKid")
        End Get
        Set(value As Integer)
            ViewState("INKid") = value
        End Set
    End Property


    Public Property lkpTypeId() As Integer
        Get
            Return ViewState("lkpTypeId")
        End Get
        Set(value As Integer)
            ViewState("lkpTypeId") = value
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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdLKP.ClientID + "');")

            Dim formPath As String = Request.Url.AbsoluteUri
            Dim strArr() As String = formPath.Split("/")
            Dim objSysForms As New SYSForms
            dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
            For Each row As DataRow In dtCurrent.Rows
                If Not row("AllowAdd") = 1 Then
                    If Not IsDBNull(row("AddBtnName")) Then
                        If Not Page.FindControl(row("AddBtnName")) Is Nothing Then
                            Page.FindControl(row("AddBtnName")).Visible = False
                        End If
                    End If
                End If

                If Not row("AllowDelete") = 1 Then
                    If Not IsDBNull(row("DeleteBtnName")) Then
                        If Not Page.FindControl(row("DeleteBtnName")) Is Nothing Then
                            Page.FindControl(row("DeleteBtnName")).Visible = False
                        End If
                    End If
                End If

                If Not row("AllowSave") = 1 Then
                    If Not IsDBNull(row("EditBtnName")) Then
                        If Not Page.FindControl(row("EditBtnName")) Is Nothing Then
                            Page.FindControl(row("EditBtnName")).Visible = False
                        End If
                    End If
                End If

                If Not row("AllowPrint") = 1 Then
                    If Not IsDBNull(row("PrintBtnName")) Then
                        If Not Page.FindControl(row("PrintBtnName")) Is Nothing Then
                            Page.FindControl(row("PrintBtnName")).Visible = False
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Protected Sub dgrdLKP_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdLKP.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        clearall()
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click

        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdLKP.Items
            If DirectCast(row.FindControl("chk"), System.Web.UI.WebControls.CheckBox).Checked Then
                Dim lkpId As Integer = Convert.ToInt32(row("lkpId").Text)
                objinv = New LKP_table()
                objinv.lkpId = lkpId
                errNum = objinv.Delete()
                With strBuilder

                End With
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillData(lkpTypeId)
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "info")
            'showResult(CodeResultMessage.CodeDeleteFail)

        End If

        clearall()
    End Sub

    Protected Sub dgrdLKP_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdLKP.ItemDataBound
        If (e.Item.ItemType = Telerik.Web.UI.GridItemType.Item) Then
            Dim Itm As GridDataItem = e.Item
        End If
    End Sub

    Protected Sub dgrdLKP_PreRender(sender As Object, e As System.EventArgs) Handles dgrdLKP.PreRender
        objinv = New LKP_table()
        Dim Dr As DataRow
        Dr = objinv.CheckHasValue(lkpTypeId)
        For Each col As GridColumn In dgrdLKP.Columns
            If col.UniqueName = "LKPCode" Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    col.HeaderText = "الرمز"
                Else
                    col.HeaderText = "Code"
                End If
            End If
            If col.UniqueName = "LKPName" Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    col.HeaderText = " الأسم باللغة العربية"
                Else
                    col.HeaderText = "Arabic Name"
                End If
            End If
            If col.UniqueName = "LKPNameAr" Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    col.HeaderText = "الأسم بالانجليزية"
                Else
                    col.HeaderText = "English Name"
                End If

            End If
            If col.UniqueName = "LKPRemarks" Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    col.HeaderText = "الملاحظات بالانجليزية"
                Else
                    col.HeaderText = "English Remarks"
                End If

            End If
            If col.UniqueName = "LKPArabicRemarks" Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    col.HeaderText = "ملاحظات"
                Else
                    col.HeaderText = "Arabic Remarks"
                End If

            End If
            If col.UniqueName = "Other1" Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    col.HeaderText = Dr("Other1CaptionAr")
                Else
                    col.HeaderText = Dr("Other1Caption")
                End If
            End If
            If col.UniqueName = "Other2" Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    col.HeaderText = Dr("Other2CaptionAr")
                Else
                    col.HeaderText = Dr("Other2Caption")
                End If
            End If
            If col.UniqueName = "Other3" Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    col.HeaderText = Dr("Other3CaptionAr")
                Else
                    col.HeaderText = Dr("Other3Caption")
                End If
            End If
            If col.UniqueName = "Other4" Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    col.HeaderText = Dr("Other4CaptionAr")
                Else
                    col.HeaderText = Dr("Other4Caption")
                End If
            End If
            If col.UniqueName = "Other5" Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    col.HeaderText = Dr("Other5CaptionAr")
                Else
                    col.HeaderText = Dr("Other5Caption")
                End If
            End If
        Next
        dgrdLKP.Rebind()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        objinv = New LKP_table
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim errorNum As Integer = -1
        With objinv
            .lkpCode = txtCode.Text.Trim()
            .lkpName = txtName.Text.Trim
            .lkpNameAr = txtArName.Text.Trim
            .FK_lkpType = Request.QueryString("id")
            .Remarks = txtRemarks.Text.Trim
            .RemarksAR = txtArRemarks.Text.Trim
            .RemarksAR = txtArRemarks.Text.Trim
            .Other1 = txtOther1.Text.Trim
            .Other2 = txtOther2.Text.Trim
            .Other3 = txtOther3.Text.Trim
            .Other4 = txtOther4.Text.Trim
            .Other5 = txtOther5.Text.Trim
        End With
        If INKid = 0 Then
            ' Do add operation 
            errorNum = objinv.Add()
            If errorNum = 0 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ElseIf errorNum = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
            ElseIf errorNum = -6 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
            ElseIf errorNum = -7 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        Else
            ' Do update operations
            objinv.lkpId = INKid
            errorNum = objinv.Update()
            If errorNum = 0 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            ElseIf errorNum = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
            ElseIf errorNum = -6 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
            ElseIf errorNum = -7 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If
        End If
        If errorNum = 0 Then
            FillData(lkpTypeId)
            clearall()
        End If

    End Sub

    Protected Sub dgrdLKP_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dgrdLKP.SelectedIndexChanged
        If dgrdLKP.SelectedItems.Count = 1 Then
            INKid = DirectCast(dgrdLKP.SelectedItems(0), GridDataItem)("lkpId").Text.Trim
            FillControls()
            FillData(lkpTypeId)
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdLKP.Skin))
    End Function

    Protected Sub dgrdVwEsubNationality_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdLKP.NeedDataSource
        Try
            objinv = New LKP_table
            dgrdLKP.DataSource = objinv.GetAll()
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Method"



    Public Sub FillData(id As Integer)
        Try
            objinv = New LKP_table()
            lkpTypeId = id
            dgrdLKP.DataSource = objinv.GetByFK(lkpTypeId)
            dgrdLKP.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Public Sub ifhas(id As Integer)
        Try
            objinv = New LKP_table()
            lkpTypeId = id
            Dim Dr As DataRow

            If SessionVariables.CultureInfo = "ar-JO" Then
                lblArName.Text = "اسم الجنسية باللغة العربية"
                lblName.Text = "اسم الجنسية باللغة الانجليزية"
            Else
                lblArName.Text = "Arabic name"
                lblName.Text = "English name"
            End If

            Dr = objinv.CheckHasValue(lkpTypeId)
            If Dr("HasCode") = False Then
                PnCode.Visible = False
            Else
                If SessionVariables.CultureInfo = "ar-JO" Then
                    lblCode.Text = "الرمز"
                Else
                    lblCode.Text = "Code"
                End If
            End If
            If Dr("HasRemarks") = False Then
                PnRemarks.Visible = False
                PnArRemarks.Visible = False
            Else
                dgrdLKP.Columns.FindByUniqueName("LKPRemarks").Visible = True
                dgrdLKP.Columns.FindByUniqueName("LKPArabicRemarks").Visible = True
                If SessionVariables.CultureInfo = "ar-JO" Then
                    lblArRemarks.Text = "الملاحظات"
                    PnRemarks.Visible = False
                Else
                    lblRemarks.Text = "Remarks"
                    PnArRemarks.Visible = False
                End If


            End If
            If Dr("HasOther1") = False Then
                pnOther1.Visible = False
            Else
                dgrdLKP.Columns.FindByUniqueName("Other1").Visible = True
                If SessionVariables.CultureInfo = "ar-JO" Then
                    lblOther1.Text = Dr("Other1CaptionAr")
                Else
                    lblOther1.Text = Dr("Other1Caption")
                End If
            End If
            If Dr("HasOther2") = False Then
                pnOther2.Visible = False
            Else
                dgrdLKP.Columns.FindByUniqueName("Other2").Visible = True
                If SessionVariables.CultureInfo = "ar-JO" Then
                    lblOther2.Text = Dr("Other2CaptionAr")
                Else
                    lblOther2.Text = Dr("Other2Caption")
                End If
            End If
            If Dr("HasOther3") = False Then
                pnOther3.Visible = False
            Else
                dgrdLKP.Columns.FindByUniqueName("Other3").Visible = True
                If SessionVariables.CultureInfo = "ar-JO" Then
                    lblOther3.Text = Dr("Other3CaptionAr")
                Else
                    lblOther3.Text = Dr("Other3Caption")
                End If
            End If
            If Dr("HasOther4") = False Then
                pnOther4.Visible = False
            Else
                dgrdLKP.Columns.FindByUniqueName("Other4").Visible = True
                If SessionVariables.CultureInfo = "ar-JO" Then
                    lblOther4.Text = Dr("Other4CaptionAr")
                Else
                    lblOther4.Text = Dr("Other4Caption")
                End If
            End If
            If Dr("HasOther5") = False Then
                pnOther5.Visible = False
            Else
                dgrdLKP.Columns.FindByUniqueName("Other5").Visible = True
                If SessionVariables.CultureInfo = "ar-JO" Then
                    lblOther5.Text = Dr("Other5CaptionAr")
                Else
                    lblOther5.Text = Dr("Other5Caption")
                End If
            End If
            Page.Title = String.Format("{0:d}", Dr("Name").ToString())
            If SessionVariables.CultureInfo = "ar-JO" Then
                Title.Text = Dr("NameAr").ToString()
            Else
                Title.Text = Dr("Name").ToString()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillControls()
        objinv = New LKP_table()
        objinv.lkpId = INKid
        objinv.GetByPK()
        txtName.Text = objinv.lkpName
        txtArName.Text = objinv.lkpNameAr
        txtCode.Text = objinv.lkpCode
        txtArRemarks.Text = objinv.RemarksAR
        txtRemarks.Text = objinv.Remarks
        txtOther1.Text = objinv.Other1
        txtOther2.Text = objinv.Other2
        txtOther3.Text = objinv.Other3
        txtOther4.Text = objinv.Other4
        txtOther5.Text = objinv.Other5
    End Sub

    Private Sub clearall()
        txtArName.Text = String.Empty
        txtName.Text = String.Empty
        txtArRemarks.Text = String.Empty
        txtRemarks.Text = String.Empty
        txtCode.Text = String.Empty
        txtOther1.Text = String.Empty
        txtOther2.Text = String.Empty
        txtOther3.Text = String.Empty
        txtOther4.Text = String.Empty
        txtOther5.Text = String.Empty
        INKid = 0
    End Sub
#End Region

End Class
