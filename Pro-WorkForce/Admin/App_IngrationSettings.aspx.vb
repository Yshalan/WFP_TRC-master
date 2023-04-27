Imports TA.Admin
Imports SmartV.UTILITIES

Partial Class Admin_App_IngrationSettings
    Inherits System.Web.UI.Page


#Region "Class Variables"

    Dim objAPP_Settings As New APP_Settings
    Dim objAPP_IntegrationSettings As New APP_IntegrationSettings

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objAppEmailConfiguration As App_EmailConfigurations

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")

            ElseIf SessionVariables.CultureInfo = "ar-JO" Then

                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            ' PageHeader1.HeaderText = ResourceManager.GetString("AppSettings", CultureInfo)
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            fillcontrols()
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        objAPP_IntegrationSettings = New APP_IntegrationSettings
        With objAPP_IntegrationSettings


            .HasEmployee = IIf(radHasEmployee.SelectedValue = 1, True, False)
            .HasEmployeeLeave = IIf(radHasEmployeeLeave.SelectedValue = 1, True, False)
            .HasLeaveAudit = IIf(radHasLeaveAudit.SelectedValue = 1, True, False)
            .HasEmployeeSupervisor = IIf(radHasEmployeeSupervisor.SelectedValue = 1, True, False)
            .HasGrade = IIf(radHasGrade.SelectedValue = 1, True, False)
            .HasDesignation = IIf(radHasDesignation.SelectedValue = 1, True, False)
            .HasNationality = IIf(radHasNationality.SelectedValue = 1, True, False)
            .HasWorkLocation = IIf(radHasWorkLocation.SelectedValue = 1, True, False)
            .HasOrganization = IIf(radHasOrganization.SelectedValue = 1, True, False)

            .HasLeaveTypes = IIf(radHasLeaveTypes.SelectedValue = 1, True, False)
            .HasHoliday = IIf(radHasHoliday.SelectedValue = 1, True, False)
            .HasEmployeeDelegate = IIf(radHasEmployeeDelegate.SelectedValue = 1, True, False)
            .HasStudyLeave = IIf(radHasStudyLeave.SelectedValue = 1, True, False)
            .HasApproveErpViolation = IIf(radHasApproveErpViolation.SelectedValue = 1, True, False)

            .ReprocessFirstSchedule = radReprocessFirstSchedule.Text
            .ReprocessSecondSchedule = radReprocessSecondSchedule.Text
            .ReprocessThirdSchedule = radReprocessThirdSchedule.Text

            .ServiceURL = txtServiceURL.Text
            .ServiceUserName = txtServiceUserName.Text
            .ServicePassword = txtServicePassword.Text
            .IntegrationType = radIntegrationType.SelectedValue
            .EmailErrorReceiver = txtEmailErrorReceiver.Text
            .EmailPortNumber = txtEmailPortNumber.Text


            .EmailEnableSsl = IIf(radEmailEnableSsl.SelectedValue = 1, True, False)
            .IsPendingLeave = IIf(radIsPendingLeave.SelectedValue = 1, True, False)
            .IsApproveLeave = IIf(radIsApproveLeave.SelectedValue = 1, True, False)
            .IsTrainingLeave = IIf(radIsTrainingLeave.SelectedValue = 1, True, False)
            .IsExtraInfoLeave = IIf(radIsExtraInfoLeave.SelectedValue = 1, True, False)
            .IsDutyLeave = IIf(radIsDutyLeave.SelectedValue = 1, True, False)


            .RunTimesByMinutes = txtRunTimesByMinutes.Text


            .RunTimesByHours = txtRunTimesByHours.Text
            .Runat = txtRunat.Text
            .Runat2 = txtRunat2.Text
            .EntityCode = txtEntityCode.Text
            .RecordPerPage = txtRecordPerPage.Text
            .IsProduction = IIf(radIsProduction.SelectedValue = 1, True, False)
            .Add()
        End With

    End Sub

    Sub fillcontrols()
        objAPP_IntegrationSettings = New APP_IntegrationSettings
        With objAPP_IntegrationSettings
            .GetAll()
            If objAPP_IntegrationSettings IsNot Nothing Then
                radHasEmployee.SelectedValue = IIf(.HasEmployee, 1, 2)
                radHasEmployeeLeave.SelectedValue = IIf(.HasEmployeeLeave, 1, 2)
                radHasLeaveAudit.SelectedValue = IIf(.HasLeaveAudit, 1, 2)
                radHasEmployeeSupervisor.SelectedValue = IIf(.HasEmployeeSupervisor, 1, 2)
                radHasGrade.SelectedValue = IIf(.HasGrade, 1, 2)
                radHasDesignation.SelectedValue = IIf(.HasDesignation, 1, 2)
                radHasNationality.SelectedValue = IIf(.HasNationality, 1, 2)
                radHasWorkLocation.SelectedValue = IIf(.HasWorkLocation, 1, 2)
                radHasOrganization.SelectedValue = IIf(.HasOrganization, 1, 2)
                radHasLeaveTypes.SelectedValue = IIf(.HasLeaveTypes, 1, 2)
                radHasHoliday.SelectedValue = IIf(.HasHoliday, 1, 2)
                radHasEmployeeDelegate.SelectedValue = IIf(.HasEmployeeDelegate, 1, 2)
                radHasStudyLeave.SelectedValue = IIf(.HasStudyLeave, 1, 2)
                radHasApproveErpViolation.SelectedValue = IIf(.HasApproveErpViolation, 1, 2)
                radReprocessFirstSchedule.Text = .ReprocessFirstSchedule
                radReprocessSecondSchedule.Text = .ReprocessSecondSchedule
                radReprocessThirdSchedule.Text = .ReprocessThirdSchedule
                txtServiceURL.Text = .ServiceURL
                txtServiceUserName.Text = .ServiceUserName
                txtServicePassword.Text = .ServicePassword
                radIntegrationType.SelectedValue = .IntegrationType
                txtEmailErrorReceiver.Text = .EmailErrorReceiver
                txtEmailPortNumber.Text = .EmailPortNumber
                radEmailEnableSsl.SelectedValue = IIf(.EmailEnableSsl, 1, 2)
                radIsPendingLeave.SelectedValue = IIf(.IsPendingLeave, 1, 2)
                radIsApproveLeave.SelectedValue = IIf(.IsApproveLeave, 1, 2)
                radIsTrainingLeave.SelectedValue = IIf(.IsTrainingLeave, 1, 2)
                radIsExtraInfoLeave.SelectedValue = IIf(.IsExtraInfoLeave, 1, 2)
                radIsDutyLeave.SelectedValue = IIf(.IsDutyLeave, 1, 2)
                txtRunTimesByMinutes.Text = .RunTimesByMinutes
                txtRunTimesByHours.Text = .RunTimesByHours
                txtRunat.Text = .Runat
                txtRunat2.Text = .Runat2
                txtEntityCode.Text = .EntityCode
                txtRecordPerPage.Text = .RecordPerPage
                radIsProduction.SelectedValue = IIf(.IsProduction, 1, 2)
            End If
        End With
    End Sub
#End Region

    
End Class
