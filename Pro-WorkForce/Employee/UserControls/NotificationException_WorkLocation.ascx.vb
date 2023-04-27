Imports SmartV.UTILITIES
Imports TA.Admin
Imports TA.Definitions
Imports System.Data
Imports TA.Employees

Partial Class Employee_UserControls_NotificationEception_WorkLocation
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objVersion As SmartV.Version.version
    Private objOrgCompany As OrgCompany
    Private objAPP_Settings As APP_Settings
    Private objNotification_Exception As Notification_Exception
    Private objEmp_WorkLocation As Emp_WorkLocation

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

    Public Property FK_WorkLocationId() As Integer
        Get
            Return ViewState("FK_WorkLocationId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_WorkLocationId") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Employee_UserControls_NotificationEception_WorkLocation_Init(sender As Object, e As EventArgs) Handles Me.Init
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If
    End Sub

    Protected Sub Employee_UserControls_NotificationException_WorkLocation_Load(sender As Object, e As EventArgs) Handles Me.Load
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
                FillWorkLocation()
            Else
                FillCompany()
            End If

        End If

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        objNotification_Exception = New Notification_Exception
        Dim errornum As Integer = -1
        With objNotification_Exception
            .FK_WorkLocationId = RadCmbBxWorkLocation.SelectedValue
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
            .FK_EntityId = Nothing
            If NotificationExceptionId = 0 Then
                errornum = .Add
            Else
                .NotificationExceptionId = NotificationExceptionId
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
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
            FillWorkLocation()
        End If
    End Sub

    Private Sub FillWorkLocation()
        objEmp_WorkLocation = New Emp_WorkLocation
        With objEmp_WorkLocation
            .FK_CompanyId = CompanyId
            CtlCommon.FillTelerikDropDownList(RadCmbBxWorkLocation, .GetAllByCompany, Lang)
        End With
    End Sub

    Private Sub ClearAll()
        RadCmbBxCompanies.SelectedValue = -1
        RadCmbBxCompanies.Enabled = True
        RadCmbBxWorkLocation.Items.Clear()
        RadCmbBxWorkLocation.Text = String.Empty
        RadCmbBxWorkLocation.Enabled = True
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
        objEmp_WorkLocation = New Emp_WorkLocation
        objNotification_Exception = New Notification_Exception

        objEmp_WorkLocation.WorkLocationId = FK_WorkLocationId
        objEmp_WorkLocation.GetByPK()
        FillCompany()
        RadCmbBxCompanies.SelectedValue = objEmp_WorkLocation.FK_CompanyId
        CompanyId = objEmp_WorkLocation.FK_CompanyId
        FillWorkLocation()
        RadCmbBxWorkLocation.SelectedValue = FK_WorkLocationId
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
        RadCmbBxWorkLocation.Enabled = Status

    End Sub

#End Region

End Class
