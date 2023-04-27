Imports TA.Definitions
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.DailyTasks
Imports TA.Security
Imports SmartV.Version

Partial Class LogicalGroup
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_LogicalGroup As Emp_logicalGroup
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Public MsgLang As String
    Private objVersion As version

#End Region

#Region "Properties"

    Private Property GroupId() As Integer
        Get
            Return ViewState("ReligionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ReligionId") = value
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

    Public Property HasMobile() As Boolean
        Get
            Return ViewState("HasMobile")
        End Get
        Set(ByVal value As Boolean)
            ViewState("HasMobile") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub dgrdVwGroup_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

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
                MsgLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                MsgLang = "en"
            End If

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            Dim objTaPolicy As New TAPolicy()
            CtlCommon.FillTelerikDropDownList(RadCmbBxPolicy, objTaPolicy.GetAll, Lang)
            FillGridView()
            reqPolicyname.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            Emp_LogicGroup.HeaderText = ResourceManager.GetString("LogicGroup", CultureInfo)

            HasMobile = objVersion.HasMobileApplication()
            If HasMobile = True Then
                dvAllowPunchOutSideLocation.Visible = True
            Else
                dvAllowPunchOutSideLocation.Visible = False
                chkAllowPunchOutSideLocation.Checked = False
            End If

        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdVwGroup.ClientID + "');")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not pnlEmpReligion.FindControl(row("AddBtnName")) Is Nothing Then
                        pnlEmpReligion.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not pnlEmpReligion.FindControl(row("DeleteBtnName")) Is Nothing Then
                        pnlEmpReligion.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not pnlEmpReligion.FindControl(row("EditBtnName")) Is Nothing Then
                        pnlEmpReligion.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not pnlEmpReligion.FindControl(row("PrintBtnName")) Is Nothing Then
                        pnlEmpReligion.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub dgrdVwGroup_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdVwGroup.NeedDataSource
        objEmp_LogicalGroup = New Emp_logicalGroup()
        dgrdVwGroup.DataSource = objEmp_LogicalGroup.GetAll()
    End Sub

    Protected Sub dgrdVwGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdVwGroup.SelectedIndexChanged
        GroupId = Convert.ToInt32(DirectCast(dgrdVwGroup.SelectedItems(0), GridDataItem).GetDataKeyValue("GroupId").ToString())
        FillControls()
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object,
                                 ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
        FillGridView()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objEmp_LogicalGroup = New Emp_logicalGroup
        Dim errorNum As Integer = -1
        ' Set data into object for Add / Update
        With objEmp_LogicalGroup

            .GroupName = txtGroupName.Text.Trim()
            .GroupArabicName = txtGroupArabicName.Text.Trim()
            .FK_TAPolicyId = RadCmbBxPolicy.SelectedValue
            .Active = True
            If HasMobile Then
                .AllowPunchOutSideLocation = chkAllowPunchOutSideLocation.Checked
            Else
                .AllowPunchOutSideLocation = False
            End If


        End With
        If GroupId = 0 Then
            ' Do add operation 
            errorNum = objEmp_LogicalGroup.Add()
            If errorNum = 0 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ElseIf errorNum = -5 Then
                CtlCommon.ShowMessage(Me, ResourceManager.GetString("NameExist", CultureInfo), "info")

            ElseIf errorNum = -6 Then
                CtlCommon.ShowMessage(Me, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        Else
            ' Do update operations
            objEmp_LogicalGroup.GroupId = GroupId
            errorNum = objEmp_LogicalGroup.Update()

            If errorNum = 0 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            ElseIf errorNum = -5 Then
                CtlCommon.ShowMessage(Me, ResourceManager.GetString("NameExist", CultureInfo), "info")
            ElseIf errorNum = -6 Then
                CtlCommon.ShowMessage(Me, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "success")
            End If
        End If
        If errorNum = 0 Then
            FillGridView()
            ClearAll()
        Else
        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdVwGroup.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim strCode As String = row.GetDataKeyValue("GroupName").ToString()
                objEmp_LogicalGroup = New Emp_logicalGroup()
                objEmp_LogicalGroup.GroupId = Convert.ToInt32(row.GetDataKeyValue("GroupId").ToString())
                errNum = objEmp_LogicalGroup.Delete()

            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGridView()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
            'showResult(CodeResultMessage.CodeDeleteFail)

        End If



        ClearAll()
    End Sub

#End Region

#Region "Methods"

    Private Sub FillControls()
        ' Get the data by the PK and display on the controls
        objEmp_LogicalGroup = New Emp_logicalGroup
        objEmp_LogicalGroup.GroupId = GroupId
        objEmp_LogicalGroup.GetByPK()
        With objEmp_LogicalGroup
            txtGroupName.Text = .GroupName
            txtGroupArabicName.Text = .GroupArabicName
            'chckBoxActive.Checked = .Active
            RadCmbBxPolicy.SelectedValue = .FK_TAPolicyId
            chkAllowPunchOutSideLocation.Checked = .AllowPunchOutSideLocation
        End With
    End Sub

    Private Sub ClearAll()
        ' Clear the cntrols
        txtGroupName.Text = String.Empty
        txtGroupArabicName.Text = String.Empty
        txtGroupName.Text = String.Empty
        'chckBoxActive.Checked = False
        RadCmbBxPolicy.SelectedIndex = -1
        chkAllowPunchOutSideLocation.Checked = False
        ' Reset to next add operation
        GroupId = 0
        ' Remove sorting and sorting arrow

    End Sub

    Private Sub FillGridView()
        Try

            objEmp_LogicalGroup = New Emp_logicalGroup()
            dgrdVwGroup.DataSource = objEmp_LogicalGroup.GetAll()
            dgrdVwGroup.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdVwGroup.Skin))
    End Function

#End Region

End Class