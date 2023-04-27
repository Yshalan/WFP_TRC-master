Imports System.Data
Imports TA.LookUp
Imports TA.Definitions
Imports SmartV.DB
Imports SmartV.UTILITIES
Imports TA.Employees
Imports Telerik.Web.UI
Imports TA.Admin
Imports TA.Security

Partial Class Admin_Emp_Designation
    Inherits System.Web.UI.Page

#Region "Class Variables"
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objEmp_Designation As Emp_Designation
    Private objLeaveTypes As LeavesTypes
    Private objArray As New ArrayList
    Private objApp_Settings As APP_Settings
    Dim Query As String = " WHERE LeaveId IN (0)"
    Private Lang As CtlCommon.Lang

#End Region

#Region "Properties"

    Private Property DesignationId() As Integer
        Get
            Return ViewState("DesignationId")
        End Get
        Set(ByVal value As Integer)
            ViewState("DesignationId") = value
        End Set
    End Property

    Private Property LeaveTypeId() As ArrayList
        Get
            Return ViewState("LeaveTypeId")
        End Get
        Set(ByVal value As ArrayList)
            ViewState("LeaveTypeId") = value
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

#Region "Page events"

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

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")

            End If

            LeaveTypeId = New ArrayList
            'ClearAll()
            FillCombo()
            CtlCommon.FillTelerikDropDownList(ddlLeaveTypes, New LeavesTypes().GetAllForDDL())
            PageHeader1.HeaderText = ResourceManager.GetString("DesignationHeader", CultureInfo)
        End If

        ibtnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdEmp_Designation.ClientID + "')")
        btnRemove.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdLeavesTypes.ClientID + "')")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("AddBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("EditBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("PrintBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next


        objApp_Settings = New APP_Settings()
        objApp_Settings.GetByPK()
        If objApp_Settings.AnnualLeaveOption = 1 Or objApp_Settings.AnnualLeaveOption = 2 Then
            Tab2.Visible = False
            'trAnnualLeaveBalance.Visible = False
            'TxtAnnualLeaveBalance.Text = String.Empty
            dgrdEmp_Designation.Columns(4).Visible = False
        End If
        If objApp_Settings.IsGradeTAException Then
            'lblTaException.Visible = False
            ChkIsTAException.Visible = False
        End If
        If objApp_Settings.IsGradeOvertimeRule Then
            trOverTimeRule.Visible = False
        End If
    End Sub

    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objEmp_Designation = New Emp_Designation
        Dim err As Integer
        With objEmp_Designation
            '.AnnualLeaveBalance = Val(TxtAnnualLeaveBalance.Text.Trim)

            If (trOverTimeRule.Visible) Then
                If Not CmbOvertimeRule.SelectedValue = -1 Then
                    .FK_OvertimeRuleId = CmbOvertimeRule.SelectedValue
                Else
                    .FK_OvertimeRuleId = Nothing
                End If
            Else
                .FK_OvertimeRuleId = Nothing
            End If

            .DesignationArabicName = TxtDesignationName.Text.Trim
            .DesignationCode = TxtDesignationCode.Text.Trim
            .DesignationName = TxtDesignationName.Text.Trim
            .DesignationArabicName = txtArabicName.Text
            .IsTAException = ChkIsTAException.Checked
            .CREATED_BY = SessionVariables.LoginUser.ID
            .CREATED_DATE = Now.Date
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_DATE = Date.Now
        End With
        If DesignationId = 0 Then
            err = objEmp_Designation.Add()
            If err = 0 Then
                DesignationId = objEmp_Designation.DesignationId
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ElseIf err = -1 Or err = -11 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        Else
            objEmp_Designation.DesignationId = DesignationId
            err = objEmp_Designation.Update()
            If err = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            ElseIf err = -1 Or err = -11 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If
        End If

        If err = 0 Then
            Try
                Dim objEmp_DesignationLeavesTypes As New Emp_DesignationLeavesTypes
                objEmp_DesignationLeavesTypes.FK_DesignationId = DesignationId
                objEmp_DesignationLeavesTypes.DeleteByFk()
                For Each intLeaveID As Integer In LeaveTypeId
                    objEmp_DesignationLeavesTypes.FK_DesignationId = DesignationId
                    objEmp_DesignationLeavesTypes.FK_LeaveId = intLeaveID
                    objEmp_DesignationLeavesTypes.Add()
                Next
                ClearAll()
            Catch ex As Exception
            End Try
        End If
        If err = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
        ElseIf err = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo), "info")
        ElseIf err = -7 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
        End If
    End Sub

    Protected Sub ibtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnDelete.Click
        Dim err As Integer
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdEmp_Designation.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intDesignationId As Integer = Convert.ToInt32(row.GetDataKeyValue("DesignationId").ToString())
                Dim objEmp_DesignationLeavesTypes As New Emp_DesignationLeavesTypes
                objEmp_DesignationLeavesTypes.FK_DesignationId = intDesignationId
                objEmp_DesignationLeavesTypes.DeleteByFk()
                objEmp_Designation = New Emp_Designation
                objEmp_Designation.DesignationId = intDesignationId
                err = objEmp_Designation.Delete()
            End If
        Next
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If

        ClearAll()
    End Sub

    Protected Sub dgrdEmp_Designation_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmp_Designation.NeedDataSource
        dgrdEmp_Designation.DataSource = New Emp_Designation().GetAll()
    End Sub

    Protected Sub dgrdEmp_Designation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdEmp_Designation.SelectedIndexChanged
        DesignationId = Convert.ToInt32(DirectCast(dgrdEmp_Designation.SelectedItems(0), GridDataItem).GetDataKeyValue("DesignationId").ToString())
        LeaveTypeId.Clear()
        objEmp_Designation = New Emp_Designation
        With objEmp_Designation
            .DesignationId = DesignationId
            .GetByPK()
            'If (trAnnualLeaveBalance.Visible) Then
            '    TxtAnnualLeaveBalance.Text = .AnnualLeaveBalance
            'Else
            '    TxtAnnualLeaveBalance.Text = String.Empty
            'End If

            TxtDesignationCode.Text = .DesignationCode
            TxtDesignationName.Text = .DesignationName
            txtArabicName.Text = .DesignationArabicName
            ChkIsTAException.Checked = .IsTAException
            CmbOvertimeRule.SelectedValue = .FK_OvertimeRuleId
        End With
        Dim objEmp_DesignationLeavesTypes As New Emp_DesignationLeavesTypes
        objEmp_DesignationLeavesTypes.FK_DesignationId = DesignationId
        Dim LeaveDt As New DataTable
        LeaveDt = objEmp_DesignationLeavesTypes.SelectByFk
        For Each dr As DataRow In LeaveDt.Rows
            LeaveTypeId.Add(dr("FK_LeaveId").ToString())
        Next
        AddLeaveTypes()
        TabContainer1.ActiveTabIndex = 0
    End Sub

    Protected Sub ibtnRest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnRest.Click
        ClearAll()
        FillGrid()
    End Sub

    Protected Sub dgrdEmp_Designation_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If LeaveTypeId.Count > 0 And LeaveTypeId.Contains(ddlLeaveTypes.SelectedValue) Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("LeaveTypeSelected", CultureInfo), "info")

            Exit Sub
        End If
        LeaveTypeId.Add(ddlLeaveTypes.SelectedValue)
        AddLeaveTypes()
        ddlLeaveTypes.SelectedIndex = -1
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click

        'For Each row As GridDataItem In dgrdLeavesTypes.SelectedItems
        '    Dim intLeaveTypeId As Integer = Convert.ToInt32(row("LeaveID").Text)
        '    LeaveTypeId.Remove(intLeaveTypeId.ToString())
        '    AddLeaveTypes()
        'Next
        For Each row As GridDataItem In dgrdLeavesTypes.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intLeaveTypeId As Integer = Convert.ToInt32(row.GetDataKeyValue("LeaveID").ToString())
                LeaveTypeId.Remove(intLeaveTypeId.ToString())
                AddLeaveTypes()
            End If

        Next
    End Sub

#End Region

#Region "Methods"

    Sub FillCombo()
        Dim dt As New DataTable
        Dim objOverTime As New OvertimeRules()
        dt = objOverTime.GetAll
        ProjectCommon.FillRadComboBox(CmbOvertimeRule, dt, "RuleName", "RuleArabicName")
    End Sub

    Public Sub RefillLeaveGrid()
        Try
            Dim dt As DataTable
            objLeaveTypes = New LeavesTypes
            dt = objLeaveTypes.GetAllByLeaveTypes(Query)
            dgrdLeavesTypes.DataSource = dt
            dgrdLeavesTypes.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub FillGrid()
        Try
            Dim dt As DataTable
            objEmp_Designation = New Emp_Designation()
            dt = objEmp_Designation.GetAll()
            'CtlCommon.FillGridView(dgrdlkpCurrency, dt)
            dgrdEmp_Designation.DataSource = dt
            dgrdEmp_Designation.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ClearAll()
        LeaveTypeId.Clear()
        'TxtAnnualLeaveBalance.Text = String.Empty
        TxtDesignationCode.Text = String.Empty
        TxtDesignationName.Text = String.Empty
        txtArabicName.Text = String.Empty
        CmbOvertimeRule.SelectedIndex = 0
        ChkIsTAException.Checked = False
        FillGrid()
        DesignationId = 0
        RefillLeaveGrid()

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmp_Designation.Skin))
    End Function

    Sub AddLeaveTypes()
        Dim strIDs As String = ""
        Dim count As Integer = 1
        For Each intLeaveID As Integer In LeaveTypeId
            If count > 1 Then
                strIDs = strIDs & "," & intLeaveID.ToString()
            Else
                strIDs = intLeaveID.ToString()
            End If
            count += 1
            strIDs.Remove(strIDs.Length - 1, 1)
        Next

        If strIDs.Length > 0 Then
            Query = " WHERE LeaveId IN (" & strIDs & ")"
        Else
            Query = " WHERE LeaveId IN (0)"
        End If
        dgrdLeavesTypes.DataSource = New LeavesTypes().GetAllByLeaveTypes(Query)
        dgrdLeavesTypes.DataBind()
    End Sub

#End Region

    'Protected Sub lnkViewRuleDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkViewRuleDetails.Click
    '    If CmbOvertimeRule.SelectedIndex > 0 Then
    '        Dim strLast As String = "','window','height=' + (screen.height/2.5) + ', width=' + (screen.width/2.15) + ', toolbar=no, menubar=no,resizable=yes,location=no,scrollbars=yes,directories=no, status=no ,resizable=false' + mypos);"
    '        Dim Str As String = "var iX = (screen.width/4)-275; var iY = (screen.height/4)-150; var mypos = 'left='+iX+',top='+iY; var win = null; win = window.open('OvertimeRulesPopUp.aspx?RuleId=" + CmbOvertimeRule.SelectedValue
    '        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, Guid.NewGuid.ToString, Str & strLast, True)
    '    End If

    'End Sub
End Class
