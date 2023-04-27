Imports SmartV.UTILITIES
Imports System.Data
Imports SmartV.Security
Imports SmartV.DB
Imports System.Resources
Imports System.Reflection
Imports System.Threading
Imports Telerik.Web.UI
Imports TA.Admin
Imports TA.LookUp
Imports SmartV.Version
Imports SmartV.UTILITIES.CtlCommon
Imports TA.Security
Imports System.Globalization
Imports TA.Employees
Imports SmartV.UTILITIES.ProjectCommon

Partial Class UserColntrols_DefineUsers
    Inherits System.Web.UI.UserControl

#Region "Class Variables"
    Public ShipDetails, ShipOwnerandLocalAgentDetails, LoadLinesInformation, NumberandCapacityofLifeSavingAppliances, TypeandNumberofFireFightingEquipment As String
    Public Add, Delete, PollutionPreventionTank As String
    Dim _DefUsers As SYSUsers
    Private objVersion As version
    Private objEmployee As Employee
    Protected dir, textalign As String
    Private Lang As CtlCommon.Lang
    Public MsgLang As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Public PasswordType As String
    Private objAPP_Settings As APP_Settings
#End Region

#Region "Properties"

    Public Property _Id() As Integer
        Get
            Return ViewState("ID")
        End Get
        Set(ByVal value As Integer)
            ViewState("ID") = value
        End Set
    End Property

    Public Property grpId() As Integer
        Get
            Return hdngrpID.Value
        End Get
        Set(ByVal value As Integer)
            hdngrpID.Value = value
        End Set
    End Property

    Public Property LocId() As Integer
        Get
            Return hdLocID.Value
        End Get
        Set(ByVal value As Integer)
            hdLocID.Value = value
        End Set
    End Property

    Private Property SortDir() As String
        Get
            Return hdnsortDir.Value
        End Get
        Set(ByVal value As String)
            hdnsortDir.Value = value
        End Set
    End Property

    Private Property SortExepression() As String
        Get
            Return hdnsortExp.Value
        End Get
        Set(ByVal value As String)
            hdnsortExp.Value = value
        End Set
    End Property

    Private Property data() As DataTable
        Get
            Return ViewState("dt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dt") = value
        End Set
    End Property

    Private Property password() As String
        Get
            Return ViewState("LoginPassword")
        End Get
        Set(ByVal value As String)
            ViewState("LoginPassword") = value
        End Set
    End Property

    Private Property Employee() As String
        Get
            Return ViewState("UserEmployeeID")
        End Get
        Set(ByVal value As String)
            ViewState("UserEmployeeID") = value
        End Set
    End Property

    Private Property SortExep() As String
        Get
            Return ViewState("sortExp")
        End Get
        Set(ByVal value As String)
            ViewState("sortExp") = value
        End Set
    End Property

    Private Property UserType() As String
        Get
            Return ViewState("UserType")
        End Get
        Set(ByVal value As String)
            ViewState("UserType") = value
        End Set
    End Property

    Private Property dcPassword() As String
        Get
            Return ViewState("dcPassword")
        End Get
        Set(ByVal value As String)
            ViewState("dcPassword") = value
        End Set
    End Property

    Public Property dtCurrentControls() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
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

    Public Property HasMobile() As Boolean
        Get
            Return ViewState("HasMobile")
        End Get
        Set(ByVal value As Boolean)
            ViewState("HasMobile") = value
        End Set
    End Property

    Public Property NoOfAllowedMobile() As Integer
        Get
            Return ViewState("NoOfAllowedMobile")
        End Get
        Set(ByVal value As Integer)
            ViewState("NoOfAllowedMobile") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (SessionVariables.CultureInfo Is Nothing) Then
            SessionVariables.CultureInfo = "en-US"
            dir = "ltr"
            textalign = "left"
        Else
            If SessionVariables.CultureInfo = "en-US" Then
                SessionVariables.CultureInfo = "en-US"
                dir = "ltr"
                textalign = "left"
                Lang = CtlCommon.Lang.EN
                MsgLang = "en"
            Else
                SessionVariables.CultureInfo = "ar-JO"
                dir = "rtl"
                textalign = "right"
                Lang = CtlCommon.Lang.AR
                MsgLang = "ar"
            End If
        End If



        If Not IsPostBack Then

            If (Lang = CtlCommon.Lang.EN) Then
                rfvCompanies.InitialValue = "--Please Select--"
                rfvEntiy.InitialValue = "--Please Select--"
            Else
                rfvCompanies.InitialValue = "--الرجاء الاختيار--"
                rfvEntiy.InitialValue = "--الرجاء الاختيار--"
            End If


            RowCompanies.Visible = False
            RowEntities.Visible = False

            rBtnLstSecurity.SelectedIndex = 0


            FillCompanies()
            LoadGroupDDL()

            LoadTypeDDL()
            LoadGrid()
            If (objVersion.HasMultiCompany() = False) Then
                rBtnLstSecurity.Items.RemoveAt(1)
                rBtnLstSecurity.Items(1).Attributes.Add("title", "test")
            Else
                rBtnLstSecurity.Items(2).Attributes.Add("title", "test")
            End If
            NoOfAllowedMobile = objVersion.GetNoOfAllowedMobile

            HasMobile = objVersion.HasMobileApplication()

            If HasMobile = False Then
                dvHasMobile.Visible = False
                dvMobileControls.Visible = False
                gvUsers.Columns(7).Visible = False
                gvUsers.Columns(8).Visible = False
                gvUsers.Columns(9).Visible = False
            End If

            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            If Not objAPP_Settings.PasswordType = Nothing Then
                PasswordType = objAPP_Settings.PasswordType
            End If
            If objAPP_Settings.PasswordType = 1 Then
                PwdValidation.Enabled = False
            Else
                PwdValidation.Enabled = True
            End If
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" & gvUsers.ClientID & "');")


        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrentControls.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not trControls.FindControl(row("AddBtnName")) Is Nothing Then
                        trControls.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not trControls.FindControl(row("DeleteBtnName")) Is Nothing Then
                        trControls.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not trControls.FindControl(row("EditBtnName")) Is Nothing Then
                        trControls.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not trControls.FindControl(row("PrintBtnName")) Is Nothing Then
                        trControls.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next


    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        _DefUsers = New SYSUsers
        Dim opRslt As Integer = -1
        Dim strEncrypt As String
        Dim objSecurity As New SmartV.UTILITIES.SmartSecurity
        'If txtPassword.Text.Trim = "1111111111" And _Id > 0 Then
        '    strEncrypt = ViewState("CurrentPass")

        'Else
        '    strEncrypt = objSecurity.strSmartEncrypt(txtPassword.Text.Trim, txtUserID.Text.Trim) 'objSecurity.strHashPassword(txtPassword.Text.Trim, txtUserID.Text.Trim)
        'End If
        If txtPassword.Text <> "" Then
            strEncrypt = objSecurity.strSmartEncrypt(txtPassword.Text.Trim, txtUserID.Text.Trim)

        Else
            strEncrypt = ""
        End If
        If Not dcPassword = Nothing Then
            strEncrypt = objSecurity.strSmartEncrypt(dcPassword, txtUserID.Text.Trim)
        End If

        With _DefUsers


            If _Id <> 0 Then
                .ID = _Id
            Else
                UserType = 1
            End If

            UserType = rblUserType.SelectedValue

            If UserType = 2 Then
                strEncrypt = ""
            End If
            .UsrID = txtUserID.Text.Trim
            .fullName = txtUserName.Text.Trim
            .GroupId = ddlGroup.SelectedValue
            '.LocID = ddlLocation.SelectedValue
            .LoginName = txtUserID.Text.Trim

            ''
            If chkActive.Checked Then
                .Active = 1
            Else
                .Active = 0
            End If
            .UserEmail = txtUserEmail.Text
            .JobDesc = txtJobDesc.Text
            .UserPhone = txtPhone.Text
            .Remarks = txtRemarks.Text
            .UserType = UserType
            If chkIsMobile.Checked = True Then
                .RegisteredUser = chkRegUser.Checked
                .RegisteredDevice = txtRegisteredDevice.Text
                .DeviceType = txtDeviceType.Text
                .IsMobile = chkIsMobile.Checked
                .IsSelfService = chkIsSelfService.Checked
                .HasSelfServiceReports = chkSelfServiceReports.Checked
                .HasMobilePunch = chkMobilePunch.Checked
            Else
                .RegisteredUser = False
                .RegisteredDevice = Nothing
                .DeviceType = Nothing
                .IsMobile = False
                .IsSelfService = False
                .HasSelfServiceReports = False
                .HasMobilePunch = False
            End If

            If Not String.IsNullOrEmpty(EmployeeFilterUC.hEmployeeId) Then
                If Not EmployeeFilterUC.hEmployeeId = 0 Then
                    .FK_EmployeeId = EmployeeFilterUC.hEmployeeId
                End If
            End If

            If Not String.IsNullOrEmpty(EmployeeFilterUC.EmployeeId) Then
                If Not EmployeeFilterUC.EmployeeId = 0 Then
                    .FK_EmployeeId = EmployeeFilterUC.EmployeeId
                End If
            End If
            ' ''
            .DefaultEmaiLang = rblDefaultEmailLang.SelectedValue
            .DefaultSMSLang = rblDefaultSMSLang.SelectedValue
            .DefaultSystemLang = rblDefaultAppLang.SelectedValue
            .UserStatus = rBtnLstSecurity.SelectedValue 'Security Level
            If Not NoOfAllowedMobile = -1 Then
                .NoOfAllowedMobile = NoOfAllowedMobile
            Else
                .NoOfAllowedMobile = Nothing
            End If
            .HasMobile = chkIsMobile.Checked
            .AllowAutoPunch = chkAllowAutoPunch.Checked
            If Not (strEncrypt = Nothing) Then .Password = strEncrypt Else .Password = ""
            '.Password = strEncrypt
            '.Status = chkActive.Checked
            '.PrefeardLang = ddlLanguage.SelectedValue
            .CREATED_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            opRslt = .save()
        End With
        If opRslt = 0 AndAlso _Id = 0 Then
            '    objSYS_Users_Security = New SYS_Users_Security
            '    'If (objVersion.HasMultiCompany()) Then
            '    '    If (rBtnLstSecurity.SelectedIndex = 1) Then
            '    '        objSYS_Users_Security.FK_CompanyId = RadCmbBxCompanies.SelectedValue
            '    '    ElseIf (rBtnLstSecurity.SelectedIndex = 2) Then
            '    '        objSYS_Users_Security.FK_CompanyId = RadCmbBxCompanies.SelectedValue
            '    '        objSYS_Users_Security.FK_EntityId = RadCmbBxEntity.SelectedValue
            '    '    End If
            '    'Else
            '    '    If (rBtnLstSecurity.SelectedIndex = 1) Then
            '    '        objSYS_Users_Security.FK_CompanyId = RadCmbBxCompanies.SelectedValue
            '    '        objSYS_Users_Security.FK_EntityId = RadCmbBxEntity.SelectedValue
            '    '    End If
            '    'End If

            '    objSYS_Users_Security.FK_UserId = _DefUsers.ID
            '    objSYS_Users_Security.FK_SecurityLevel = rBtnLstSecurity.SelectedValue
            '    objSYS_Users_Security.Add()

            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")

            ClearAll()
            EmployeeFilterUC.ClearValues()
        ElseIf opRslt = 0 AndAlso _Id > 0 Then

            'objSYS_Users_Security = New SYS_Users_Security
            ''If (objVersion.HasMultiCompany()) Then
            ''    If (rBtnLstSecurity.SelectedIndex = 1) Then
            ''        objSYS_Users_Security.FK_CompanyId = RadCmbBxCompanies.SelectedValue
            ''    ElseIf (rBtnLstSecurity.SelectedIndex = 2) Then
            ''        objSYS_Users_Security.FK_CompanyId = RadCmbBxCompanies.SelectedValue
            ''        objSYS_Users_Security.FK_EntityId = RadCmbBxEntity.SelectedValue
            ''    End If
            ''Else
            ''    If (rBtnLstSecurity.SelectedIndex = 1) Then
            ''        objSYS_Users_Security.FK_CompanyId = RadCmbBxCompanies.SelectedValue
            ''        objSYS_Users_Security.FK_EntityId = RadCmbBxEntity.SelectedValue
            ''    End If
            ''End If
            'objSYS_Users_Security.FK_UserId = _DefUsers.ID
            'objSYS_Users_Security.FK_SecurityLevel = rBtnLstSecurity.SelectedValue
            'objSYS_Users_Security.Update()

            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")

            ClearAll()
            EmployeeFilterUC.ClearValues()
        ElseIf opRslt = -11 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UserIDExist", CultureInfo), "info")
        ElseIf opRslt = -1 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        ElseIf opRslt = -13 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpAssigned", CultureInfo), "info")
        ElseIf opRslt = -14 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoOfMobileExceed", CultureInfo), "info")
        End If
        'Visiblility(False)
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
        EmployeeFilterUC.ClearValues()
    End Sub

    Protected Sub RadCmbBxCompanies_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompanies.SelectedIndexChanged
        If (objVersion.HasMultiCompany()) Then
            RowCompanies.Visible = True
        Else
            RowCompanies.Visible = False
            FillEntity()
        End If

        If (rBtnLstSecurity.SelectedIndex = 1) Then
            RowEntities.Visible = False
        ElseIf (rBtnLstSecurity.SelectedIndex = 2) Then
            FillEntity()
            RowEntities.Visible = True
        End If
    End Sub

    'Protected Sub rBtnLstSecurity_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rBtnLstSecurity.SelectedIndexChanged
    '    If (objVersion.HasMultiCompany()) Then
    '        If (rBtnLstSecurity.SelectedValue = 1) Then
    '            RowCompanies.Visible = False
    '            RowEntities.Visible = False
    '        ElseIf (rBtnLstSecurity.SelectedValue = 2) Then
    '            RadCmbBxCompanies.SelectedIndex = 0
    '            RowEntities.Visible = False
    '            RowCompanies.Visible = True
    '            RowCompanies.Style.Add("width", "100%")
    '        ElseIf (rBtnLstSecurity.SelectedValue = 3) Then
    '            RadCmbBxCompanies.SelectedIndex = 0
    '            RowCompanies.Visible = True
    '            RowEntities.Visible = True
    '            RowCompanies.Style.Add("width", "100%")

    '        End If
    '    Else
    '        If (rBtnLstSecurity.SelectedValue = 1) Then
    '            RowCompanies.Visible = False
    '            RowEntities.Visible = False
    '        ElseIf (rBtnLstSecurity.SelectedValue = 3) Then
    '            RowCompanies.Visible = False
    '            RowEntities.Visible = True
    '            RowCompanies.Style.Add("width", "100%")
    '        End If
    '    End If

    'End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Sub gvUsers_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvUsers.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
            LoadGrid()
        End If
    End Sub

    Protected Sub gvUsers_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvUsers.SelectedIndexChanged
        password = String.Empty
        _Id = 0
        CtlCommon.ClearCtlContent(New WebControl() {ddlGroup, txtConfirm, txtUserName, txtPassword, txtUserID, txtUserName, txtUserEmail, txtJobDesc, txtRemarks, txtPhone}, CtlCommon.OpMode.ResetIndex)
        chkActive.Checked = False
        rBtnLstSecurity.SelectedIndex = 0

        _DefUsers = New SYSUsers
        'objSYS_Users_Security = New SYS_Users_Security

        _Id = CInt(DirectCast(gvUsers.SelectedItems(0), GridDataItem).GetDataKeyValue("ID").ToString())
        'CInt(CType(gvUsers.SelectedItems(0), GridDataItem)("ID").Text)

        With _DefUsers
            .ID = _Id
            .GetUser()

            txtUserName.Text = .fullName
            If .GroupId > 0 Then
                ddlGroup.Items.FindItemByValue(.GroupId.ToString()).Selected = True
            End If
            txtUserID.Text = .UsrID
            txtUserEmail.Text = .UserEmail
            txtJobDesc.Text = .JobDesc
            txtPhone.Text = .UserPhone
            chkIsMobile.Checked = .IsMobile


            If HasMobile = True Then
                If .IsMobile = True Then
                    dvMobileControls.Visible = True
                    lnkClearMobileVal.Visible = True
                Else
                    dvMobileControls.Visible = False
                    lnkClearMobileVal.Visible = False
                End If
            Else
                dvMobileControls.Visible = False
                lnkClearMobileVal.Visible = False
            End If
            chkIsSelfService.Checked = .IsSelfService
            chkRegUser.Checked = .RegisteredUser
            txtRegisteredDevice.Text = .RegisteredDevice
            txtDeviceType.Text = .DeviceType
            chkAllowAutoPunch.Checked = .AllowAutoPunch
            chkSelfServiceReports.Checked = .HasSelfServiceReports
            chkMobilePunch.Checked = .HasMobilePunch
            If Not _DefUsers.UserStatus = 0 Then
                If .UserStatus = 3 Then
                    If Not version.HasMultiCompany = False Then
                        rBtnLstSecurity.SelectedValue = .UserStatus
                    End If
                Else
                    rBtnLstSecurity.SelectedValue = .UserStatus
                End If
            End If

            If (.Active) Then
                chkActive.Checked = True
            Else
                chkActive.Checked = False
            End If
            txtRemarks.Text = .Remarks

            Dim err As Integer = -1
            objEmployee = New Employee
            objEmployee.EmployeeId = .FK_EmployeeId
            err = objEmployee.GetExist_Employee()

            If err <> 0 Then
                EmployeeFilterUC.IsEntityClick = "True"
                EmployeeFilterUC.GetEmployeeInfo(.FK_EmployeeId)
            End If
            UserType = .UserType
            rblUserType.SelectedValue = UserType
            Dim objSecurity As New SmartV.UTILITIES.SmartSecurity

            If .Password <> "" Then
                dcPassword = objSecurity.strSmartDecrypt(.Password, txtUserID.Text.Trim)
                txtPassword.Attributes.Add("value", dcPassword)
                txtConfirm.Attributes.Add("value", dcPassword)
                password = .Password
            End If
            If .DefaultEmaiLang <> "" Then
                rblDefaultEmailLang.SelectedValue = .DefaultEmaiLang
            End If
            If .DefaultSMSLang <> "" Then
                rblDefaultSMSLang.SelectedValue = .DefaultSMSLang
            End If
            If .DefaultSystemLang <> "" Then
                rblDefaultAppLang.SelectedValue = .DefaultSystemLang
            End If
        End With
        'With objSYS_Users_Security
        '    .FK_UserId = _Id
        '    .GetByPK()
        '    If .FK_SecurityLevel > 0 Then
        '        'rBtnLstSecurity.Items.FindByValue(.FK_SecurityLevel).Selected = True

        '        'If (objVersion.HasMultiCompany()) Then
        '        '    If (rBtnLstSecurity.SelectedIndex = 1) Then
        '        '        RadCmbBxCompanies.Items.FindItemByValue(.FK_CompanyId).Selected = True
        '        '    ElseIf (rBtnLstSecurity.SelectedIndex = 2) Then
        '        '        RadCmbBxCompanies.Items.FindItemByValue(.FK_CompanyId).Selected = True
        '        '        RadCmbBxEntity.Items.FindItemByValue(.FK_EntityId).Selected = True
        '        '    End If
        '        'Else
        '        '    If (rBtnLstSecurity.SelectedIndex = 1) Then
        '        '        RadCmbBxCompanies.Items.FindItemByValue(.FK_CompanyId).Selected = True
        '        '        RadCmbBxEntity.Items.FindItemByValue(.FK_EntityId).Selected = True
        '        '    End If
        '        'End If
        '    End If

        'End With

        If UserType = 2 Then
            txtPassword.Attributes.Add("value", "")
            txtConfirm.Attributes.Add("value", "")
            txtPassword.Enabled = False
            txtConfirm.Enabled = False
            valReqPassword.Visible = False
            trPassword.Visible = False
            trConfirmPassword.Visible = False
            trChangePassword.Visible = False
        Else
            txtConfirm.Enabled = True
            txtPassword.Enabled = True
            valReqPassword.Visible = True
            trPassword.Visible = False
            trConfirmPassword.Visible = False
            trChangePassword.Visible = True
        End If

    End Sub

    Protected Sub gvUsers_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvUsers.NeedDataSource


        'Dim dv As New DataView(data)
        'dv.Sort = SortExepression
        'gvUsers.DataSource = dv

    End Sub

    Protected Sub lnbChangePassword_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnbChangePassword.Click
        mpeChangePwdPopup.Show()
    End Sub

    Protected Sub btnChangePassword_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChangePassword.Click
        Dim OldEncPass As String = Nothing
        Dim NewEncPass As String = Nothing
        Dim rslt As Integer = 0
        Dim objLoginUser As New SYSUsers
        Dim objSecurity As New SmartV.UTILITIES.SmartSecurity
        Dim user As New SYSUsers
        With objLoginUser

            .ID = _Id
            '.UsrID = 4
            .GetUser()

            If Not String.IsNullOrEmpty(txtOldPassowrd.Text) Then
                OldEncPass = objSecurity.strSmartEncrypt(txtOldPassowrd.Text.Trim, txtUserID.Text.Trim()) 'objSecurity.strHashPassword(txtOldPassowrd.Text.Trim, SessionVariables.Sys_LoginUser.UsrID) '.Password
            Else
                OldEncPass = Nothing
            End If

            NewEncPass = objSecurity.strSmartEncrypt(txtNewPassowrd.Text.Trim, txtUserID.Text.Trim()) 'objSecurity.strHashPassword(txtNewPassowrd.Text.Trim, SessionVariables.Sys_LoginUser.UsrID)
            .EncrptPassNew = NewEncPass
            .EncrptPassOld = OldEncPass
        End With
        Dim errNo As Integer
        Try
            errNo = objLoginUser.ChangeUserPassword()
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If errNo = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PassChangeSuccess", CultureInfo), "success")
                mpeChangePwdPopup.Hide()
                dcPassword = txtNewPassowrd.Text
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("WrongOldPass", CultureInfo), "error")
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In gvUsers.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim strCode As String = row.GetDataKeyValue("ID").ToString()
                Dim ID As Integer = Convert.ToInt32(row.GetDataKeyValue("ID"))
                _DefUsers = New SYSUsers
                _DefUsers.ID = ID
                _DefUsers = New SYSUsers
                _DefUsers.ID = ID
                '-----Delete Users Unless Admin------'
                If Not ID = 33 Then
                    errNum = _DefUsers.delete
                End If
                '-----Delete Users Unless Admin------'
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully"), "success")
            LoadGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete"), "error")

        End If
        ClearAll()
    End Sub

    Protected Sub rblUserType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblUserType.SelectedIndexChanged
        If rblUserType.SelectedValue = 2 Then
            txtPassword.Attributes.Add("value", "")
            txtConfirm.Attributes.Add("value", "")
            txtPassword.Enabled = False
            txtConfirm.Enabled = False
            valReqPassword.Visible = False
            trPassword.Visible = False
            trConfirmPassword.Visible = False
            trChangePassword.Visible = False

        Else
            If Not _Id = Nothing Or Not _Id = 0 Then
                If Not password = Nothing Then
                    trChangePassword.Visible = True
                    valReqPassword.Visible = True
                Else
                    txtConfirm.Enabled = True
                    txtPassword.Enabled = True
                    trPassword.Visible = True
                    trConfirmPassword.Visible = True
                    trChangePassword.Visible = False
                    valReqPassword.Visible = False
                End If
            Else
                txtConfirm.Enabled = True
                txtPassword.Enabled = True
                trPassword.Visible = True
                trConfirmPassword.Visible = True
            End If
        End If
    End Sub

    Protected Sub chkIsMobile_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsMobile.CheckedChanged
        If chkIsMobile.Checked = True Then
            dvMobileControls.Visible = True
        Else
            dvMobileControls.Visible = False
        End If
    End Sub

    Protected Sub gvUsers_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles gvUsers.ItemDataBound
        'If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
        '    Dim item As GridDataItem
        '    item = e.Item
        '    If objVersion.HasMobileApplication = True Then
        '        If Not item("RegisteredUser").Text = "&nbsp;" Then
        '            If item("RegisteredUser").Text = True Then
        '                item("RegisteredUser").Text = IIf(Lang = CtlCommon.Lang.AR, "نعم", "Yes")
        '            Else
        '                item("RegisteredUser").Text = IIf(Lang = CtlCommon.Lang.AR, "لا", "No")
        '            End If
        '        Else
        '            item("RegisteredUser").Text = IIf(Lang = CtlCommon.Lang.AR, "لا", "No")
        '        End If
        '    End If
        'End If
    End Sub

    Protected Sub lnkClearMobileVal_Click(sender As Object, e As EventArgs) Handles lnkClearMobileVal.Click
        _DefUsers = New SYSUsers
        Dim err As Integer = -1
        If Not _Id = 0 Then

            With _DefUsers
                .ID = _Id
                .RegisteredDevice = String.Empty
                .DeviceType = String.Empty
                err = .ClearDevice()
            End With
            If err = 0 Then
                txtRegisteredDevice.Text = String.Empty
                txtDeviceType.Text = String.Empty
                LoadGrid()
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ClearDevice"), "info")
            End If
        End If
    End Sub

#End Region

#Region "Page Methods"

    Private Sub LoadGrid()
        _DefUsers = New SYSUsers
        data = _DefUsers.GetAllUsers()
        gvUsers.DataSource = data
        gvUsers.DataBind()
    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", gvUsers.Skin))
    End Function

    Private Sub LoadGroupDDL()
        Dim objGroup As SYSGroups = New SYSGroups
        If SessionVariables.CultureInfo = "en-US" Then
            CtlCommon.FillTelerikDropDownList(ddlGroup, objGroup.GetAll(1), Lang)
        Else
            CtlCommon.FillTelerikDropDownList(ddlGroup, objGroup.GetAll(0), Lang)
        End If
    End Sub

    'Private Sub LoadLocationDDL()
    '    Dim objLoc As SYSLocation = New SYSLocation

    '    If SessionVariables.CultureInfo = "en-US" Then
    '        CtlCommon.FillTelerikDropDownList(ddlLocation, objLoc.GetAll(1), CtlCommon.Lang.EN)
    '    Else
    '        CtlCommon.FillTelerikDropDownList(ddlLocation, objLoc.GetAll(0), CtlCommon.Lang.AR)
    '    End If


    'End Sub

    Private Sub ClearAll()
        CtlCommon.ClearCtlContent(New WebControl() {ddlGroup, txtConfirm, txtUserName, txtPassword, txtUserID, txtUserName, txtUserEmail, txtJobDesc, txtRemarks, txtPhone}, CtlCommon.OpMode.ResetIndex)
        txtPassword.Attributes.Add("value", "")
        txtConfirm.Attributes.Add("value", "")
        rblUserType.SelectedValue = 1
        chkActive.Checked = False
        rBtnLstSecurity.SelectedIndex = 0
        LoadGrid()
        LoadGroupDDL()
        ''LoadLocationDDL()
        'LoadTypeDDL()

        CntrlVisible(True)
        'Visiblility(False)
        Me.ViewState.Remove("ID")

        '_DefUsers = New SYSUsers                      
        'With _DefUsers
        '    .ID = _Id
        'End With
        'If (objVersion.HasMultiCompany() = False) Then
        '    RowCompanies.Style("visibility") = "hidden"
        '    RowEntities.Style("visibility") = "hidden"
        '    rBtnLstSecurity.SelectedIndex = 0
        'Else
        RowCompanies.Visible = False
        RowEntities.Visible = False

        'End If
        rblDefaultEmailLang.SelectedValue = "AR"
        rblDefaultSMSLang.SelectedValue = "AR"
        rblDefaultAppLang.SelectedValue = "AR"
        trPassword.Visible = True
        trConfirmPassword.Visible = True
        txtPassword.Enabled = True
        txtConfirm.Enabled = True
        trChangePassword.Visible = False
        password = String.Empty
        _Id = 0
        chkIsMobile.Checked = False
        chkIsSelfService.Checked = False
        If HasMobile = True Then
            dvHasMobile.Visible = True
        Else
            dvHasMobile.Visible = False
        End If
        dvMobileControls.Visible = False
        chkRegUser.Checked = False
        txtRegisteredDevice.Text = String.Empty
        txtDeviceType.Text = String.Empty
        lnkClearMobileVal.Visible = False
        chkSelfServiceReports.Checked = False
        chkMobilePunch.Checked = False
        chkAllowAutoPunch.Checked = False
    End Sub

    'Protected Sub lnkEmployeeid_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    _DefUsers = New SYSUsers
    '    Dim encPass As String
    '    Dim objSecurity As New SmartSecurity


    '    Dim GVR As GridViewRow = CType(sender, LinkButton).Parent.Parent
    '    _Id = CType(gvUsers.Rows(GVR.RowIndex).FindControl("lblUserID"), Label).Text
    '    grpId = CType(gvUsers.Rows(GVR.RowIndex).FindControl("lblGrpID"), Label).Text
    '    'LocId = CType(gvUsers.Rows(GVR.RowIndex).FindControl("lblLocID"), Label).Text

    '    txtUserName.Text = CType(gvUsers.Rows(GVR.RowIndex).FindControl("lnkEmployeeid"), LinkButton).Text

    '    With _DefUsers
    '        .ID = _Id
    '        .GetUser()

    '        ViewState("CurrentPass") = .Password

    '        '' CtlCommon.ShowMessage(Me.Page, ViewState("CurrentPass"))
    '        ' Response.Write(ViewState("CurrentPass"))

    '        'encPass = objSecurity.strSmartDecrypt(.Password, .UsrID) 'objSecurity.strHashPassword(.Password, .UsrID)

    '        'Dim strEncrypt As String = objSecurity.strSmartEncrypt("n", .UsrID)
    '        Dim strDecrypt As String = objSecurity.strSmartDecrypt(.Password, .UsrID)
    '        txtPassword.Text = strDecrypt
    '        txtConfirm.Text = strDecrypt

    '        txtUserName.Text = CType(gvUsers.Rows(GVR.RowIndex).FindControl("lnkEmployeeid"), LinkButton).Text
    '        Employee = .FK_EmployeeId
    '        'EmployeeFilterUC.EmployeeId = .FK_EmployeeId
    '        EmployeeFilterUC.SetSelectedCompanyAndEmployee(.FK_CompanyId, .FK_EmployeeId)

    '        ddlGroup.SelectedValue = grpId
    '        'ddlLocation.SelectedValue = LocId
    '        txtUserID.Text = .UsrID
    '        'ddlType.SelectedValue = .UserType 'CType(gvUsers.Rows(GVR.RowIndex).FindControl("lblUserType"), Label).Text
    '        txtRemarks.Text = .Remarks
    '        txtUserEmail.Text = .UserEmail
    '        txtPhone.Text = .UserPhone
    '        txtJobDesc.Text = .JobDesc
    '        If .Active = 1 Then
    '            chkActive.Checked = True
    '        Else
    '            chkActive.Checked = False
    '        End If
    '    End With

    '    objSYS_Users_Security = New SYS_Users_Security
    '    objSYS_Users_Security.FK_UserId = _Id
    '    objSYS_Users_Security.GetByPK()

    '    If (objVersion.HasMultiCompany() = False) Then
    '        If (objSYS_Users_Security.FK_EntityId <> 0) Then
    '            RowCompanies.Visible = False
    '            RowEntities.Visible = True
    '            RadCmbBxCompanies.SelectedValue = objSYS_Users_Security.FK_CompanyId
    '            FillEntity()
    '            RadCmbBxEntity.SelectedValue = objSYS_Users_Security.FK_EntityId
    '            rBtnLstSecurity.SelectedIndex = 1

    '        Else
    '            RowCompanies.Visible = False
    '            RowEntities.Visible = False
    '            RadCmbBxCompanies.SelectedValue = objVersion.GetCompanyId()
    '            RadCmbBxCompanies_SelectedIndexChanged(Nothing, Nothing)
    '            rBtnLstSecurity.SelectedIndex = 0
    '            RadCmbBxEntity.DataSource = Nothing
    '            RadCmbBxEntity.DataBind()
    '            RadCmbBxCompanies.DataSource = Nothing
    '            RadCmbBxCompanies.DataBind()
    '        End If
    '    Else
    '        If (objSYS_Users_Security.FK_CompanyId <> 0 And objSYS_Users_Security.FK_EntityId <> 0) Then
    '            RowCompanies.Visible = True
    '            RowEntities.Visible = True
    '            RadCmbBxCompanies.SelectedValue = objSYS_Users_Security.FK_CompanyId
    '            FillEntity()
    '            RadCmbBxEntity.SelectedValue = objSYS_Users_Security.FK_EntityId
    '            rBtnLstSecurity.SelectedIndex = 2
    '        ElseIf (objSYS_Users_Security.FK_CompanyId <> 0 And objSYS_Users_Security.FK_EntityId = 0) Then
    '            RowCompanies.Visible = True
    '            RowEntities.Visible = False
    '            RadCmbBxEntity.DataSource = Nothing
    '            RadCmbBxEntity.DataBind()
    '            RadCmbBxCompanies.SelectedValue = objSYS_Users_Security.FK_CompanyId
    '            rBtnLstSecurity.SelectedIndex = 1
    '        ElseIf (objSYS_Users_Security.FK_CompanyId = 0 And objSYS_Users_Security.FK_EntityId = 0) Then
    '            RowCompanies.Visible = False
    '            RowEntities.Visible = False
    '            rBtnLstSecurity.SelectedIndex = 0
    '            RadCmbBxEntity.DataSource = Nothing
    '            RadCmbBxEntity.DataBind()
    '            RadCmbBxCompanies.DataSource = Nothing
    '            RadCmbBxCompanies.DataBind()
    '        End If
    '    End If

    '    ScriptManager.RegisterStartupScript(Page, Page.GetType, Guid.NewGuid.ToString, " Pass();", True)


    '    ' CntrlVisible(False)
    '    'Visiblility(True)
    'End Sub

    'Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    '    _DefUsers = New SYSUsers

    '    Dim username As String


    '    Dim opRslt As Integer = -1
    '    Dim StringBuilder As New StringBuilder


    '    For i As Integer = 0 To gvUsers.Rows.Count - 1
    '        If CType(gvUsers.Rows(i).FindControl("chkUser"), CheckBox).Checked = True Then

    '            With _DefUsers
    '                .ID = CInt(CType(gvUsers.Rows(i).FindControl("lblUserID"), Label).Text)
    '                username = CType(gvUsers.Rows(i).FindControl("lnkEmployeeid"), LinkButton).Text
    '                opRslt = .delete()
    '            End With

    '            If Not opRslt = 0 Then

    '                With StringBuilder
    '                    .Append("Failed delete employee user " & gvUsers.Rows(i).Cells(1).Text)
    '                    .Append("\n")
    '                End With
    '            Else

    '                ' Delete from User_Security
    '                objSYS_Users_Security = New SYS_Users_Security
    '                objSYS_Users_Security.FK_UserId = CInt(CType(gvUsers.Rows(i).FindControl("lblUserID"), Label).Text)
    '                objSYS_Users_Security.Delete()
    '                With StringBuilder

    '                    .Append("Delete  user:- " & username)
    '                    .Append("\n")
    '                End With
    '            End If
    '        End If
    '    Next

    '    If StringBuilder.Length = 0 Then
    '        With StringBuilder
    '            .Append("please select from list")
    '            .Append("\n")
    '        End With
    '    End If
    '    CtlCommon.ShowMessage(Me.Page, StringBuilder.ToString)
    '    StringBuilder = Nothing
    '    ClearAll()
    'End Sub

    Private Sub CntrlVisible(ByVal vbl As Boolean)

        txtConfirm.Visible = vbl
        txtPassword.Visible = vbl
        lblConfirm.Visible = vbl
        lblPassword.Visible = vbl

    End Sub

    'Private Sub Visiblility(ByVal vbl As Boolean)
    '    LnkChPasswd.Visible = vbl
    '    'If vbl = True Then
    '    '    lblPassword.Text = "Enter new Password"
    '    'Else
    '    '    lblPassword.Text = "Enter the Password"
    '    'End If

    'End Sub

    'Protected Sub gvUsers_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvUsers.PageIndexChanging
    '    gvUsers.SelectedIndex = -1
    '    gvUsers.PageIndex = e.NewPageIndex
    '    Dim dv As New DataView(data)
    '    dv.Sort = SortExepression
    '    gvUsers.DataSource = dv
    '    gvUsers.DataBind()
    '    CtlCommon.ClearCtlContent(New WebControl() {ddlGroup, txtConfirm, txtUserName, txtPassword, txtUserID, txtUserName}, CtlCommon.OpMode.ResetIndex)
    'End Sub

    'Protected Sub gvUsers_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvUsers.Sorting

    '    If SortDir = "ASC" Then
    '        SortDir = "DESC"
    '    Else
    '        SortDir = "ASC"
    '    End If
    '    SortExepression = e.SortExpression & Space(1) & SortDir
    '    Dim dv As New DataView(data)

    '    dv.Sort = SortExepression
    '    SortExep = e.SortExpression
    '    gvUsers.DataSource = dv
    '    gvUsers.DataBind()

    'End Sub
    'Protected Sub LnkChPasswd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkChPasswd.Click
    '    CntrlVisible(True)
    '    'Visiblility(True)

    'End Sub

    Protected Sub LoadTypeDDL()
        'Dim item As New RadComboBoxItem
        'If SessionVariables.CultureInfo = "en-US" Then
        '    item.Value = -1
        '    item.Text = "--Please Select--"
        '    ddlType.Items.Add(item)
        '    item = Nothing
        '    item = New RadComboBoxItem
        '    item.Value = 1
        '    item.Text = "Normal User"
        '    ddlType.Items.Add(item)
        '    item = Nothing
        '    item = New RadComboBoxItem
        '    item.Value = 2
        '    item.Text = "Admin"
        '    ddlType.Items.Add(item)
        '    item = Nothing
        '    item = New RadComboBoxItem
        '    item.Value = 3
        '    item.Text = "Super Admin"
        '    ddlType.Items.Add(item)

        'Else
        '    item.Value = -1
        '    item.Text = "--الرجاء الاختيار--"
        '    ddlType.Items.Add(item)
        '    item = Nothing
        '    item = New RadComboBoxItem
        '    item.Value = 1
        '    item.Text = "مستخدم"
        '    ddlType.Items.Add(item)
        '    item = Nothing
        '    item = New RadComboBoxItem
        '    item.Value = 2
        '    item.Text = "مسؤول النظام"
        '    ddlType.Items.Add(item)
        '    item = Nothing
        '    item = New RadComboBoxItem
        '    item.Value = 3
        '    item.Text = "المسؤول المختص"
        '    ddlType.Items.Add(item)
        'End If
    End Sub

    Private Sub FillCompanies()
        Dim objOrgCompany As New OrgCompany
        CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, objOrgCompany.GetAllforddl, Lang)

    End Sub

    Private Sub FillEntity()

        If RadCmbBxCompanies.SelectedValue <> -1 Then
            Dim objProjectCommon = New ProjectCommon()
            Dim objOrgEntity = New OrgEntity()
            objOrgEntity.FK_CompanyId = RadCmbBxCompanies.SelectedValue
            Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()
            If dt.Rows.Count > 0 Then
                objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId",
                                                         "EntityName", "EntityArabicName", "FK_ParentId")
            Else
                RadCmbBxEntity.Items.Clear()
                RadCmbBxEntity.Text = String.Empty
            End If
            If (Lang = CtlCommon.Lang.EN) Then
                RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem("--Please Select--", -1))
            Else
                RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem("--الرجاء الاختيار--", -1))
            End If

        End If

    End Sub

    Public Sub LoadGridByEmplotyeeID()
        _DefUsers = New SYSUsers
        data = _DefUsers.GetAllUSersByEmployeeID(EmployeeFilterUC.EmployeeId)
        CtlCommon.FillRadGridView(gvUsers, data)
    End Sub

#End Region

End Class
