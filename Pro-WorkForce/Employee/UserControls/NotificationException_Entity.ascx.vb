Imports SmartV.UTILITIES
Imports TA.Admin
Imports TA.Definitions
Imports System.Data
Imports TA.Employees
Imports TA.Security

Partial Class Employee_UserControls_NotificationException_Entity
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objVersion As SmartV.Version.version
    Private objOrgCompany As OrgCompany
    Private objAPP_Settings As APP_Settings
    Private objNotification_Exception As Notification_Exception
    Private objOrgEntity As OrgEntity
    Private objSYSUsers As SYSUsers

#End Region

#Region "Public Properties"

    Public Property CompanyId() As Integer
        Get
            Return ViewState("CompanyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CompanyId") = value
        End Set
    End Property

    Public Property NotificationExceptionId() As Integer
        Get
            Return ViewState("NotificationExceptionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("NotificationExceptionId") = value
        End Set
    End Property

    Public Property FK_EntityId() As Integer
        Get
            Return ViewState("FK_EntityId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_EntityId") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Employee_UserControls_NotificationException_Entity_Init(sender As Object, e As EventArgs) Handles Me.Init
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If
    End Sub

    Protected Sub Employee_UserControls_NotificationException_LogicalGroup_Load(sender As Object, e As EventArgs) Handles Me.Load
        showHide(chckTemporary.Checked)
        If Not Page.IsPostBack Then
            Me.dtpFromdate.SelectedDate = Today
            Me.dtpEndDate.SelectedDate = Today

            If (objVersion.HasMultiCompany() = False) Then
                CompanyId = objVersion.GetCompanyId()
                RadCmbBxCompanies.SelectedValue = CompanyId
                RadCmbBxCompanies.Visible = False
                lblCompany.Visible = False
                rfvCompanies.Enabled = False
                FillEntity()
            Else
                FillCompany()
            End If

        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        objNotification_Exception = New Notification_Exception
        Dim errornum As Integer = -1
        With objNotification_Exception
            .FK_EntityId = RadCmbBxEntity.SelectedValue
            .FromDate = dtpFromdate.DbSelectedDate
            If chckTemporary.Checked Then
                .ToDate = dtpEndDate.DbSelectedDate
                pnlEndDate.Visible = True
            Else
                .ToDate = Nothing
                pnlEndDate.Visible = False
            End If
            .Reason = txtReason.Text
            .CREATED_BY = SessionVariables.LoginUser.ID
            .FK_EmployeeId = Nothing
            .FK_LogicalGroupId = Nothing
            .FK_WorkLocationId = Nothing
            If NotificationExceptionId = 0 Then
                errornum = .Add
            Else
                .NotificationExceptionId = NotificationExceptionId
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                errornum = .Update

            End If

        End With

        If errornum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ClearAll()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "close", "Refresh();", True)
        ElseIf errornum = -99 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DateExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

#End Region

#Region "Methods"

    Private Sub FillCompany()
        Dim dt As DataTable
        objOrgCompany = New OrgCompany
        dt = objOrgCompany.GetAllforddl
        CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, dt, Lang)
    End Sub

    Public Sub CompanyChanged()
        If Not RadCmbBxCompanies.SelectedValue = -1 Then
            CompanyId = RadCmbBxCompanies.SelectedValue
            FillEntity()
        End If
    End Sub

    Private Sub FillEntity()
        If RadCmbBxCompanies.SelectedValue <> -1 Then
            Dim objProjectCommon = New ProjectCommon()
            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()
            If objSYSUsers.UserStatus = 1 Then
                Dim objOrgEntity = New OrgEntity()
                objOrgEntity.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()

                objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId", _
                                                         "EntityName", "EntityArabicName", "FK_ParentId")
                RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
            ElseIf objSYSUsers.UserStatus = 2 Then
                Dim objOrgEntity = New OrgEntity()
                Dim dtEntity As New DataTable
                objOrgEntity.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                objOrgEntity.FK_UserId = objSYSUsers.ID
                dtEntity = objOrgEntity.GetAllEntityByCompanyAndByUserId
                If DTable.IsValidDataTable(dtEntity) Then
                    CtlCommon.FillTelerikDropDownList(RadCmbBxEntity, dtEntity, Lang)
                End If
            Else
                Dim objOrgEntity = New OrgEntity()
                objOrgEntity.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()

                objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId", _
                                                         "EntityName", "EntityArabicName", "FK_ParentId")
                RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
            End If
        End If
    End Sub

    Private Sub ClearAll()
        RadCmbBxCompanies.SelectedValue = -1
        RadCmbBxCompanies.Enabled = True
        RadCmbBxEntity.Items.Clear()
        RadCmbBxEntity.Text = String.Empty
        dtpEndDate.SelectedDate = Date.Today
        pnlEndDate.Visible = False
        dtpFromdate.SelectedDate = Date.Today
        chckTemporary.Checked = False
        txtReason.Text = String.Empty
    End Sub

    Private Sub showHide(ByVal status As Boolean, Optional ByVal order As Integer = 0)

        pnlEndDate.Visible = status

        If (status) Then
            rfvToDate.Enabled = True
            ValidatorCalloutExtender2.Enabled = True
        Else
            rfvToDate.Enabled = False
            ValidatorCalloutExtender2.Enabled = False
        End If

    End Sub

    Public Sub FillControls()
        objOrgEntity = New OrgEntity
        objNotification_Exception = New Notification_Exception
        objOrgEntity.EntityId = FK_EntityId
        objOrgEntity.GetByPK()
        FillCompany()
        RadCmbBxCompanies.SelectedValue = objOrgEntity.FK_CompanyId
        CompanyId = objOrgEntity.FK_CompanyId
        FillEntity()
        RadCmbBxEntity.SelectedValue = FK_EntityId
        objNotification_Exception.NotificationExceptionId = NotificationExceptionId
        objNotification_Exception.GetByPK()
        dtpFromdate.SelectedDate = objNotification_Exception.FromDate
        If Not objNotification_Exception.ToDate = Nothing Then
            dtpEndDate.SelectedDate = objNotification_Exception.ToDate
            chckTemporary.Checked = True
            pnlEndDate.Visible = True
        Else
            chckTemporary.Checked = False
            pnlEndDate.Visible = False
        End If
        txtReason.Text = objNotification_Exception.Reason
        ManipulateControls(False)

    End Sub

    Private Sub ManipulateControls(ByVal Status As Boolean)

        RadCmbBxCompanies.Enabled = Status
        RadCmbBxEntity.Enabled = Status

    End Sub
#End Region

End Class
