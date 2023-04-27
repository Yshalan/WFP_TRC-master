Imports TA.Employees
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.Definitions
Imports TA.Security
Imports System.Data
Imports TA.LookUp
Imports TA.Admin

Partial Class Admin_EmployeesLeaveBalance
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objAPP_Settings As APP_Settings
    Dim objLeaveType As LeavesTypes
    Dim objEmpLeave As Emp_Leaves
    Private Lang As CtlCommon.Lang
    Private objEmp_Leaves As Emp_Leaves
    Private objEmp_Leaves_BalanceHistory As New Emp_Leaves_BalanceHistory
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Shared SortExepression As String
    Private objEmployee As Employee
    Private objEmp_GradeLeavesTypes As Emp_GradeLeavesTypes


#End Region

#Region "Properties"

    Public Property LeaveId() As Integer
        Get
            Return ViewState("LeaveId")
        End Get
        Set(ByVal value As Integer)
            ViewState("LeaveId") = value
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

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property
    ''
    Public Property BalanceID() As Integer
        Get
            Return ViewState("BalanceID")
        End Get
        Set(ByVal value As Integer)
            ViewState("BalanceID") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            mvEmpLastBalance.SetActiveView(viewEmpLastBalance)
            rbtnlstBalance.SelectedIndex = 0
            'EmployeeFilterUC.CompanyRequiredFieldValidationGroup = btnGet.ValidationGroup
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                'rfvLeaveType.InitialValue = "--الرجاء الاختيار--"
            Else
                Lang = CtlCommon.Lang.EN
                'rfvLeaveType.InitialValue = "--Please Select--"
            End If
            PageHeader1.HeaderText = ResourceManager.GetString("EmpLeaveBalance", CultureInfo)

            FillLeaveType()
            EmployeeFilterUC.IsEmployeeRequired = True
            EmployeeFilterUC.EmployeeRequiredValidationGroup = btnGet.ValidationGroup
        End If

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

    Protected Sub btnGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGet.Click
        FillGrid()
    End Sub

    Protected Sub btn_Click(ByVal sender As Object, ByVal e As EventArgs)
        mvEmpLastBalance.SetActiveView(viewEditEmpBalance)
        Dim gvr As GridDataItem = CType(sender, LinkButton).Parent.Parent
        BalanceID = DirectCast(gvr.FindControl("hdnBalanceID"), HiddenField).Value
        txtEmpNo.Text = dgrdLastBalance.Items(gvr.ItemIndex).GetDataKeyValue("EmployeeNo").ToString()
        txtEmployeeName.Text = dgrdLastBalance.Items(gvr.ItemIndex).GetDataKeyValue("EmployeeName").ToString()
        txtLeaveType.Text = dgrdLastBalance.Items(gvr.ItemIndex).GetDataKeyValue("LeaveName").ToString()
        txtCurrentBalance.Text = dgrdLastBalance.Items(gvr.ItemIndex).GetDataKeyValue("TotalBalance").ToString()
        LeaveId = dgrdLastBalance.Items(gvr.ItemIndex).GetDataKeyValue("LeaveId").ToString()
        EmployeeId = dgrdLastBalance.Items(gvr.ItemIndex).GetDataKeyValue("FK_EmployeeId").ToString()
        mvEmpLastBalance.SetActiveView(viewEditEmpBalance)
    End Sub

    Protected Sub btn_ClickLnk(ByVal sender As Object, ByVal e As EventArgs)
        'mvEmpLastBalance.SetActiveView(viewEditEmpBalance)
        objEmpLeave = New Emp_Leaves
        objLeaveType = New LeavesTypes
        objEmpLeave.FK_CompanyId = EmployeeFilterUC.CompanyId
        If Not EmployeeFilterUC.EmployeeId = Nothing Then
            objEmpLeave.FK_EntityId = EmployeeFilterUC.EntityId
            objEmpLeave.FK_EmployeeId = EmployeeFilterUC.EmployeeId
            If (RadCmbBxLeaveType.SelectedValue <> -1) Then
                objEmpLeave.FK_LeaveTypeId = RadCmbBxLeaveType.SelectedValue

                objLeaveType.LeaveId = RadCmbBxLeaveType.SelectedValue
                objLeaveType.GetByPK()
                If Not IsInGrade(objLeaveType.LeaveId, objLeaveType.IsSpecificGrade, EmployeeFilterUC.EmployeeId) Then
                    If SessionVariables.CultureInfo = "en-US" Then
                        CtlCommon.ShowMessage(Me.Page, "Employee Grade Is Not Eligible For This Leave Type", "info")
                    Else
                        CtlCommon.ShowMessage(Me.Page, "الدرجة الوظيفية للموظف لاتخوله لهذا النوع من الاجازة", "info")
                    End If
                    Exit Sub
                End If
                Dim ErrorMsg As String = ""

                If Not CheckLeaveAllowedGender(EmployeeFilterUC.EmployeeId, objLeaveType.AllowedGender, ErrorMsg) Then
                    CtlCommon.ShowMessage(Me.Page, ErrorMsg, "info")
                    Exit Sub
                End If

                If Not CheckLeaveAllowedEmployeeType(EmployeeId, objLeaveType.AllowForSpecificEmployeeType, objLeaveType.FK_EmployeeTypeId, ErrorMsg) Then
                    CtlCommon.ShowMessage(Me.Page, ErrorMsg, "info")
                    Exit Sub
                End If

                objEmpLeave.GetLastBalanceForEmployee_info()
                With objEmpLeave
                    'If Not objEmpLeave Is Nothing Then
                    If Not .EmployeeNo = 0 Then
                        BalanceID = 0
                        txtEmpNo.Text = .EmployeeNo
                        If Lang = CtlCommon.Lang.AR Then
                            txtEmployeeName.Text = .EmployeeArabicName
                            txtLeaveType.Text = .LeaveArabicName
                        Else
                            txtEmployeeName.Text = .EmployeeName
                            txtLeaveType.Text = .LeaveName
                        End If
                        txtCurrentBalance.Text = .TotalBalance
                        LeaveId = .LeaveId
                        EmployeeId = .FK_EmployeeId
                        mvEmpLastBalance.SetActiveView(viewEditEmpBalance)
                        'End If
                    Else
                        LeaveId = RadCmbBxLeaveType.SelectedValue
                        objLeaveType.LeaveId = RadCmbBxLeaveType.SelectedValue
                        objLeaveType.GetByPK()

                        objEmployee = New Employee
                        EmployeeId = EmployeeFilterUC.EmployeeId
                        objEmployee.EmployeeId = EmployeeFilterUC.EmployeeId
                        objEmployee.GetByPK()

                        mvEmpLastBalance.SetActiveView(viewEditEmpBalance)
                        txtEmpNo.Text = EmployeeFilterUC.EmpNo
                        If Lang = CtlCommon.Lang.AR Then
                            txtEmployeeName.Text = objEmployee.EmployeeArabicName
                            txtLeaveType.Text = objLeaveType.LeaveArabicName
                        Else
                            txtEmployeeName.Text = objEmployee.EmployeeName
                            txtLeaveType.Text = objLeaveType.LeaveName
                        End If
                        txtCurrentBalance.Text = 0

                    End If
                End With
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SelectLeaveType", CultureInfo), "info")
            End If
        End If
    End Sub

    Public Sub txtAmount_TextChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles txtAmount.TextChanged
        If Not String.IsNullOrEmpty(txtAmount.Text.Trim()) Then
            If (rbtnlstBalance.SelectedValue = 1) Then
                txtNewBalance.Text = CDbl(txtCurrentBalance.Text) + CDbl(txtAmount.Text)
            Else
                txtNewBalance.Text = CDbl(txtCurrentBalance.Text) - CDbl(txtAmount.Text)
            End If
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PleaseEnterAmount", CultureInfo), "info")
        End If

    End Sub

    Private Sub rmtHoursBalance_TextChanged(sender As Object, e As EventArgs) Handles rmtHoursBalance.TextChanged
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
        End With
        If Not rmtHoursBalance.TextWithLiterals = ":" Then
            If (rbtnlstBalance.SelectedValue = 1) Then
                Dim strHoursBalance As String = (CInt(rmtHoursBalance.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtHoursBalance.TextWithLiterals.Split(":")(1))
                txtNewBalance.Text = CDbl(txtCurrentBalance.Text) + (CDbl(strHoursBalance) / objAPP_Settings.DaysMinutes)
            Else
                Dim strHoursBalance As String = (CInt(rmtHoursBalance.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtHoursBalance.TextWithLiterals.Split(":")(1))
                txtNewBalance.Text = CDbl(txtCurrentBalance.Text) - (CDbl(strHoursBalance) / objAPP_Settings.DaysMinutes)
            End If
        End If

    End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
        Dim CreatedBy As String = String.Empty
        If SessionVariables.LoginUser IsNot Nothing Then
            CreatedBy = SessionVariables.LoginUser.ID
        End If

        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
        End With

        Dim ErrorNo As Integer
        With objEmp_Leaves_BalanceHistory
            objEmp_Leaves_BalanceHistory.FK_EmployeeId = EmployeeId
            objEmp_Leaves_BalanceHistory.FK_LeaveId = LeaveId
            objEmp_Leaves_BalanceHistory.BalanceDate = Date.Now
            If rblBalanceType.SelectedValue = 1 Then
                objEmp_Leaves_BalanceHistory.Balance = CDbl(txtAmount.Text)
            Else
                objEmp_Leaves_BalanceHistory.Balance = CDbl((CInt(rmtHoursBalance.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtHoursBalance.TextWithLiterals.Split(":")(1))) / objAPP_Settings.DaysMinutes
            End If
            objEmp_Leaves_BalanceHistory.TotalBalance = CDbl(txtNewBalance.Text)
            objEmp_Leaves_BalanceHistory.FK_EmpLeaveId = 0
            If Not txtRemarks.Text = String.Empty Then
                objEmp_Leaves_BalanceHistory.Remarks = txtRemarks.Text
            Else
                objEmp_Leaves_BalanceHistory.Remarks = "Manual Entry"
            End If
            objEmp_Leaves_BalanceHistory.CREATED_DATE = Date.Now
            objEmp_Leaves_BalanceHistory.CREATED_BY = CreatedBy
            'If BalanceID = 0 Then
            ErrorNo = objEmp_Leaves_BalanceHistory.Add()
            'Else
            '    .BalanceId = BalanceID
            '    ErrorNo = .Update()
            'End If

            If (ErrorNo = 0) Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
                btnGet_Click(Nothing, Nothing)
                mvEmpLastBalance.SetActiveView(viewEmpLastBalance)
                BalanceID = 0
                ClearAll()
                FillGrid()
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If
        End With
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        ClearAll()
        mvEmpLastBalance.SetActiveView(viewEmpLastBalance)
    End Sub

    Protected Sub rbtnlstBalance_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnlstBalance.SelectedIndexChanged
        If Not String.IsNullOrEmpty(txtAmount.Text.Trim()) Then
            If (rbtnlstBalance.SelectedValue = 1) Then
                txtNewBalance.Text = CDbl(txtCurrentBalance.Text) + CDbl(txtAmount.Text)
            Else
                txtNewBalance.Text = CDbl(txtCurrentBalance.Text) - CDbl(txtAmount.Text)
            End If
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PleaseEnterAmount", CultureInfo), "info")
        End If

    End Sub

    Protected Sub dgrdLastBalance_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdLastBalance.NeedDataSource
        '    If Not RadCmbBxLeaveType.SelectedValue = -1 Then
        '        objEmpLeave = New Emp_Leaves
        '        objEmpLeave.FK_CompanyId = EmployeeFilterUC.CompanyId
        '        If (EmployeeFilterUC.EntityId <> 0 And EmployeeFilterUC.EntityId <> -1) Then
        '            objEmpLeave.FK_EntityId = EmployeeFilterUC.EntityId
        '        Else
        '            objEmpLeave.FK_EntityId = 0
        '        End If
        '        If (RadCmbBxLeaveType.SelectedValue <> -1) Then
        '            objEmpLeave.FK_LeaveTypeId = RadCmbBxLeaveType.SelectedValue
        '        Else
        '            objEmpLeave.FK_LeaveTypeId = 0
        '        End If

        '        dtCurrent = objEmpLeave.GetLastBalanceForEmployees()

        '        Dim dv As New DataView(dtCurrent)
        '        dv.Sort = SortExepression
        '        dgrdLastBalance.DataSource = dv
        '    End If
        'If Not RadCmbBxLeaveType.SelectedValue = -1 Then
        objEmpLeave = New Emp_Leaves
        objEmpLeave.FK_CompanyId = EmployeeFilterUC.CompanyId
        If Not EmployeeFilterUC.EmployeeId = Nothing Then
            objEmpLeave.FK_EntityId = EmployeeFilterUC.EntityId
            objEmpLeave.FK_EmployeeId = EmployeeFilterUC.EmployeeId
        Else
            If (EmployeeFilterUC.EntityId <> 0 And EmployeeFilterUC.EntityId <> -1) Then
                objEmpLeave.FK_EntityId = EmployeeFilterUC.EntityId
            Else
                objEmpLeave.FK_EntityId = 0
            End If
        End If

        If (RadCmbBxLeaveType.SelectedValue <> -1) Then
            objEmpLeave.FK_LeaveTypeId = RadCmbBxLeaveType.SelectedValue
        Else
            objEmpLeave.FK_LeaveTypeId = 0
        End If
        dgrdLastBalance.DataSource = objEmpLeave.GetLastBalanceForEmployees()
        'End If
    End Sub

    Protected Sub dgrdLastBalance_ItemdataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdLastBalance.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            If SessionVariables.CultureInfo = "ar-JO" Then
                item("EmployeeName").Text = item.GetDataKeyValue("EmployeeArabicName").ToString()
                item("LeaveName").Text = item.GetDataKeyValue("LeaveArabicName").ToString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("TotalBalance").ToString())) And (Not item.GetDataKeyValue("TotalBalance").ToString() = "")) Then
                item("TotalBalance").Text = Decimal.Round(Convert.ToDecimal(item.GetDataKeyValue("TotalBalance").ToString()), 3).ToString()
            End If

        End If
    End Sub

    Private Sub rblBalanceType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblBalanceType.SelectedIndexChanged
        If rblBalanceType.SelectedValue = 1 Then
            dvDaysBalance.Visible = True
            dvHoursBalance.Visible = False
            rfvHoursBalance.Enabled = False
            rmtHoursBalance.TextWithLiterals = "0000"
        Else
            dvDaysBalance.Visible = False
            RegularExpressionValidator1.Enabled = False
            dvHoursBalance.Visible = True
            txtAmount.Text = String.Empty
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillGrid()
        objEmpLeave = New Emp_Leaves
        objEmpLeave.FK_CompanyId = EmployeeFilterUC.CompanyId
        If Not EmployeeFilterUC.EmployeeId = Nothing Then
            objEmpLeave.FK_EntityId = EmployeeFilterUC.EntityId
            objEmpLeave.FK_EmployeeId = EmployeeFilterUC.EmployeeId
        Else
            If (EmployeeFilterUC.EntityId <> 0 And EmployeeFilterUC.EntityId <> -1) Then
                objEmpLeave.FK_EntityId = EmployeeFilterUC.EntityId
            Else
                objEmpLeave.FK_EntityId = 0
            End If
        End If

        If (RadCmbBxLeaveType.SelectedValue <> -1) Then
            objEmpLeave.FK_LeaveTypeId = RadCmbBxLeaveType.SelectedValue
        Else
            objEmpLeave.FK_LeaveTypeId = 0
        End If
        'Dim dv As New DataView(objEmpLeave.GetLastBalanceForEmployees())
        'If Lang = CtlCommon.Lang.AR Then
        '    dv.Sort = "EmployeeArabicName"
        'Else
        '    dv.Sort = "EmployeeName"
        'End If
        'dgrdLastBalance.DataSource = dv
        dgrdLastBalance.DataSource = objEmpLeave.GetLastBalanceForEmployees()
        dgrdLastBalance.DataBind()
    End Sub

    Private Sub FillLeaveType()
        objLeaveType = New LeavesTypes
        CtlCommon.FillTelerikDropDownList(RadCmbBxLeaveType, objLeaveType.GetAll(), Lang)
    End Sub

    Function IsInGrade(ByVal FK_LeaveTypeId As Integer, ByVal IsSpecificGrade As Boolean, ByVal FK_EmployeeId As Integer) As Boolean
        objEmp_GradeLeavesTypes = New Emp_GradeLeavesTypes
        objEmployee = New Employee
        Dim dt As DataTable
        If IsSpecificGrade = True Then
            objEmp_GradeLeavesTypes.FK_LeaveId = FK_LeaveTypeId
            objEmployee.EmployeeId = FK_EmployeeId
            objEmployee.GetByPK()
            dt = objEmp_GradeLeavesTypes.GetAllByFK_LeaveId()

            Dim status As Boolean = False
            For Each dr As DataRow In dt.Rows
                If objEmployee.FK_Grade = dr("FK_GradeId") Then
                    Return True
                Else
                    status = False
                End If
            Next
            Return status
        Else
            Return True
        End If

    End Function

    Function CheckLeaveAllowedGender(ByVal EmployeeId As Integer, ByVal AllowedGender As Integer, ByRef ErrorMsg As String) As Boolean
        objEmployee = New Employee
        With objEmployee
            .EmployeeId = EmployeeId
            .GetByPK()
            If AllowedGender = 1 And .Gender = "f" Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    ErrorMsg = "نوع الاجازة الذي قمت باختياره مسموح للذكور فقط"
                Else
                    ErrorMsg = "The Selected Leave Type Allowed For Males Only"
                End If
                Return False
            ElseIf AllowedGender = 2 And .Gender = "m" Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    ErrorMsg = "نوع الاجازة الذي قمت باختياره مسموح للاناث فقط"
                Else
                    ErrorMsg = "The Selected Leave Type Allowed For Females Only"
                End If
                Return False
            End If
        End With
        Return True
    End Function

    Function CheckLeaveAllowedEmployeeType(ByVal EmployeeId As Integer, ByVal AllowForSpecificEmployeeType As Boolean, ByVal FK_EmployeeTypeId As Integer, ByRef ErrorMsg As String) As Boolean
        objEmployee = New Employee
        With objEmployee
            .EmployeeId = EmployeeId
            .GetByPK()
            If AllowForSpecificEmployeeType = True Then
                If FK_EmployeeTypeId <> .FK_LogicalGroup Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        ErrorMsg = "نوع الاجازة الذي قمت باختياره غير مسموح للمجموعة الوظيفية للموظف"
                    Else
                        ErrorMsg = "The Selected Leave Type Not Allowed For The Selected Employee Logical Group"
                    End If
                    Return False
                End If
            End If
        End With
        Return True
    End Function

    Private Sub ClearAll()
        txtEmployeeName.Text = String.Empty
        txtEmpNo.Text = String.Empty
        txtLeaveType.Text = String.Empty
        txtCurrentBalance.Text = String.Empty
        txtRemarks.Text = "Manual Entry"
        rbtnlstBalance.SelectedValue = 1
        txtAmount.Text = String.Empty
        txtNewBalance.Text = String.Empty
        rblBalanceType.SelectedValue = 1
        rmtHoursBalance.TextWithLiterals = "0000"
        BalanceID = 0
    End Sub
#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdLastBalance.Skin))
    End Function

    Protected Sub dgrdLastBalance_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdLastBalance.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub


#End Region

End Class
