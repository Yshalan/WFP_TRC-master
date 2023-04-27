Imports System.Data
Imports TA.Lookup
Imports SmartV.DB
Imports SmartV.UTILITIES
Imports TA.Definitions
Imports System.Web.UI.WebControls
Imports Telerik.Web.UI
Imports TA.Admin
Imports TA.Employees
Imports TA.Security

Partial Class Admin_Emp_Grade
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_Grade As Emp_Grade
    Private objApp_Settings As APP_Settings
    Private objLeaveTypes As LeavesTypes
    Private objArray As New ArrayList
    Dim Query As String = " WHERE LeaveId IN (0)"
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

#End Region

#Region "Properties"

    Public Property GradeId() As Integer
        Get
            Return ViewState("GradeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("GradeId") = value
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

    Public Property Lang() As CtlCommon.Lang
        Get
            Return ViewState("Lang")
        End Get
        Set(ByVal value As CtlCommon.Lang)
            ViewState("Lang") = value
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
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            objApp_Settings = New APP_Settings()
            objApp_Settings.GetByPK()
            If objApp_Settings.AnnualLeaveOption = 1 Or objApp_Settings.AnnualLeaveOption = 3 Then
                Tab2.Visible = False
                'trAnnualLeaveBalance.Visible = False
                'TxtAnnualLeaveBalance.Text = String.Empty
            End If
            If Not objApp_Settings.IsGradeTAException Then
                'lblTAException.Visible = False
                ChkIsTAException.Visible = False
            End If
            If Not objApp_Settings.IsGradeOvertimeRule Then
                trOverTimeRule.Visible = False
            End If

            LeaveTypeId = New ArrayList
            FillCombo()
            CtlCommon.FillTelerikDropDownList(ddlLeaveTypes, New LeavesTypes().GetAllForDDL(), Lang)
            PageHeader1.HeaderText = ResourceManager.GetString("DefGrade", CultureInfo)
        End If
        ibtnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdEmp_Grade.ClientID + "')")
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

    End Sub

    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objEmp_Grade = New Emp_Grade
        Dim err As Integer
        With objEmp_Grade
            'If TxtAnnualLeaveBalance.Text <> String.Empty Then
            '    .AnnualLeaveBalance = TxtAnnualLeaveBalance.Text
            'Else
            '    .AnnualLeaveBalance = 0
            'End If

            If (trOverTimeRule.Visible) Then
                If Not CmbOvertimeRule.SelectedValue = -1 Then
                    .FK_OvertimeRuleId = CmbOvertimeRule.SelectedValue
                Else
                    .FK_OvertimeRuleId = Nothing
                End If
            Else
                .FK_OvertimeRuleId = Nothing
            End If
            .GradeArabicName = TxtGradeName.Text.Trim
            .GradeCode = TxtGradeCode.Text.Trim
            .GradeName = TxtGradeName.Text.Trim
            .GradeArabicName = txtGradeArName.Text
            .IsTAException = ChkIsTAException.Checked
            .CREATED_BY = SessionVariables.LoginUser.ID 'SessionVariables.LoginUser.EmployeeID
            .CREATED_DATE = Now.Date
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID ' SessionVariables.LoginUser.EmployeeID
            .LAST_UPDATE_DATE = Now.Date
        End With
        If GradeId = 0 Then
            err = objEmp_Grade.Add()

            If err = 0 Then
                GradeId = objEmp_Grade.GradeId
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")

            ElseIf err = -1 Or err = -11 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If


        Else
            objEmp_Grade.GradeId = GradeId
            err = objEmp_Grade.Update()

            If err = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            ElseIf err = -1 Or err = -11 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If

        End If

        If err = 0 Then

            Try
                Dim objEmp_GradeLeavesTypes As New Emp_GradeLeavesTypes
                objEmp_GradeLeavesTypes.FK_GradeId = GradeId
                objEmp_GradeLeavesTypes.DeleteByFk()
                For Each intLeaveID As Integer In LeaveTypeId
                    objEmp_GradeLeavesTypes.FK_GradeId = GradeId
                    objEmp_GradeLeavesTypes.FK_LeaveId = intLeaveID
                    objEmp_GradeLeavesTypes.Add()
                Next
            Catch ex As Exception
            End Try
            ClearAll()
        End If

        If err = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
        ElseIf err = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo), "info")
        ElseIf err = -7 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
        ElseIf err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If

    End Sub

    Protected Sub ibtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnDelete.Click
        Dim err As Integer
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdEmp_Grade.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intGradeId As Integer = Convert.ToInt32(row.GetDataKeyValue("GradeId").ToString())
                Dim objEmp_GradeLeavesTypes As New Emp_GradeLeavesTypes
                objEmp_GradeLeavesTypes.FK_GradeId = GradeId
                objEmp_GradeLeavesTypes.DeleteByFk()
                objEmp_Grade = New Emp_Grade
                objEmp_Grade.GradeId = intGradeId
                objEmp_Grade.Delete()
              
            End If
        Next

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            ClearAll()
            FillGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If

    End Sub

    Protected Sub ibtnRest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnRest.Click
        ClearAll()
        FillGrid()
    End Sub

    Protected Sub dgrdEmp_Grade_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdEmp_Grade.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdEmp_Grade_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmp_Grade.NeedDataSource
        Try
            objEmp_Grade = New Emp_Grade()
            dgrdEmp_Grade.DataSource = objEmp_Grade.GetAll()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub dgrdEmp_Grade_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdEmp_Grade.SelectedIndexChanged
        GradeId = CInt(DirectCast(dgrdEmp_Grade.SelectedItems(0), GridDataItem).GetDataKeyValue("GradeId").ToString().Trim)
        objEmp_Grade = New Emp_Grade
        LeaveTypeId.Clear()
        With objEmp_Grade
            .GradeId = GradeId
            .GetByPK()
            'If (trAnnualLeaveBalance.Visible) Then
            '    TxtAnnualLeaveBalance.Text = .AnnualLeaveBalance
            'Else
            '    TxtAnnualLeaveBalance.Text = String.Empty
            'End If

            TxtGradeCode.Text = .GradeCode
            TxtGradeName.Text = .GradeName
            txtGradeArName.Text = .GradeArabicName
            ChkIsTAException.Checked = .IsTAException
            CmbOvertimeRule.SelectedValue = .FK_OvertimeRuleId
        End With

        Dim objEmp_GradeLeavesTypes As New Emp_GradeLeavesTypes
        objEmp_GradeLeavesTypes.FK_GradeId = GradeId
        Dim LeaveDt As New DataTable
        LeaveDt = objEmp_GradeLeavesTypes.SelectByFk
        For Each dr As DataRow In LeaveDt.Rows
            LeaveTypeId.Add(dr("FK_LeaveId").ToString())
        Next
        AddLeaveTypes()
        TabContainer1.ActiveTabIndex = 0
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If LeaveTypeId.Count > 0 And LeaveTypeId.Contains(ddlLeaveTypes.SelectedValue) Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("LeaveTypeExist", CultureInfo), "info")
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

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

#End Region

#Region "Methods"

    Sub FillCombo()
        Dim dt As New DataTable
        Dim objOverTime As New OvertimeRules()
        dt = objOverTime.GetAll
        If dt IsNot Nothing Then
            ProjectCommon.FillRadComboBox(CmbOvertimeRule, dt, "RuleName", "RuleArabicName")
        End If
    End Sub

    Public Sub FillGrid()
        Try
            objEmp_Grade = New Emp_Grade()
            dgrdEmp_Grade.DataSource = objEmp_Grade.GetAll()
            dgrdEmp_Grade.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ClearAll()
        LeaveTypeId.Clear()
        'TxtAnnualLeaveBalance.Text = String.Empty
        TxtGradeCode.Text = String.Empty
        TxtGradeName.Text = String.Empty
        txtGradeArName.Text = String.Empty
        ChkIsTAException.Checked = False
        CmbOvertimeRule.SelectedIndex = 0
        GradeId = 0
        FillGrid()
        RefillLeaveGrid()
    End Sub

    Public Sub RefillLeaveGrid()
        Try
            Dim dt As DataTable
            objLeaveTypes = New LeavesTypes
            dt = objLeaveTypes.GetAllByLeaveTypes(Query)
            Dim dv As New DataView(dt)
            dgrdLeavesTypes.DataSource = dv
            dgrdLeavesTypes.DataBind()
        Catch ex As Exception
        End Try
    End Sub

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

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmp_Grade.Skin))
    End Function

#End Region

End Class
