<%@ Page Title="TA App Integration Settings" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="App_IngrationSettings.aspx.vb" Inherits="Admin_App_IngrationSettings"
    UICulture="auto" meta:resourcekey="PageResource2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Integration Setting" />
    <div>
        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="" meta:resourcekey="TabContainer1Resource1">
            <cc1:TabPanel ID="tabCustomerInfo" runat="server" HeaderText="Settings"
                TabIndex="0" meta:resourcekey="tabCustomerInfoResource1">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblIntegrationType" runat="server" CssClass="Profiletitletxt" Text="Integration Type" meta:resourcekey="lblIntegrationTypeResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="radIntegrationType" runat="server" Skin="Vista" meta:resourcekey="radIntegrationTypeResource1">
                                <Items>
                                    <telerik:RadComboBoxItem Value="-1" Text="--Please Select--" runat="server" meta:resourcekey="RadComboBoxItemResource1" />
                                    <telerik:RadComboBoxItem Value="DOF" Text="DOF" runat="server" meta:resourcekey="RadComboBoxItemResource2" />
                                    <telerik:RadComboBoxItem Value="ADEC" Text="ADEC" runat="server" meta:resourcekey="RadComboBoxItemResource3" />
                                    <telerik:RadComboBoxItem Value="BAYANATI" Text="BAYANATI" runat="server" meta:resourcekey="RadComboBoxItemResource4" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblHasEmployee" runat="server" Text="Fetch Employee"
                                CssClass="Profiletitletxt" meta:resourcekey="lblHasEmployeeResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radHasEmployee" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radHasEmployeeResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblHasEmployeeLeave" runat="server" Text="Fetch Employee Leave"
                                CssClass="Profiletitletxt" meta:resourcekey="lblHasEmployeeLeaveResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radHasEmployeeLeave" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radHasEmployeeLeaveResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource4"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblHasLeaveAudit" runat="server" Text="Fetch Leave Audit"
                                CssClass="Profiletitletxt" meta:resourcekey="lblHasLeaveAuditResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radHasLeaveAudit" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radHasLeaveAuditResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource6"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblHasEmployeeSupervisor" runat="server" Text="Fetch Employee Supervisor"
                                CssClass="Profiletitletxt" meta:resourcekey="lblHasEmployeeSupervisorResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radHasEmployeeSupervisor" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radHasEmployeeSupervisorResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource7"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource8"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblHasGrade" runat="server" Text="Fetch Grade"
                                CssClass="Profiletitletxt" meta:resourcekey="lblHasGradeResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radHasGrade" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radHasGradeResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource9"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource10"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblHasDesignation" runat="server" Text="Fetch Designation"
                                CssClass="Profiletitletxt" meta:resourcekey="lblHasDesignationResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radHasDesignation" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radHasDesignationResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource11"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource12"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblHasNationality" runat="server" Text="Fetch Nationality"
                                CssClass="Profiletitletxt" meta:resourcekey="lblHasNationalityResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radHasNationality" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radHasNationalityResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource13"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource14"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblHasWorkLocation" runat="server" Text="Fetch Work Location"
                                CssClass="Profiletitletxt" meta:resourcekey="lblHasWorkLocationResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radHasWorkLocation" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radHasWorkLocationResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource15"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource16"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblHasOrganization" runat="server" Text="Fetch Organization"
                                CssClass="Profiletitletxt" meta:resourcekey="lblHasOrganizationResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radHasOrganization" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radHasOrganizationResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblHasLeaveTypes" runat="server" Text="Fetch Leave Types"
                                CssClass="Profiletitletxt" meta:resourcekey="lblHasLeaveTypesResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radHasLeaveTypes" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radHasLeaveTypesResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource19"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource20"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblHasEmployeeDelegate" runat="server" Text="Fetch Employee Delegate"
                                CssClass="Profiletitletxt" meta:resourcekey="lblHasEmployeeDelegateResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radHasEmployeeDelegate" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radHasEmployeeDelegateResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource21"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource22"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblHasHoliday" runat="server" Text="Fetch Holiday"
                                CssClass="Profiletitletxt" meta:resourcekey="lblHasHolidayResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radHasHoliday" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radHasHolidayResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource23"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource24"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblHasStudyLeave" runat="server" Text="Fetch Study Leave"
                                CssClass="Profiletitletxt" meta:resourcekey="lblHasStudyLeaveResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radHasStudyLeave" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radHasStudyLeaveResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource25"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource26"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblHasApproveErpViolation" runat="server" Text="Posting Approve ERP Violation"
                                CssClass="Profiletitletxt" meta:resourcekey="lblHasApproveErpViolationResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radHasApproveErpViolation" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radHasApproveErpViolationResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource27"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource28"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblIsPendingLeave" runat="server" Text="Fetch Pending Leave"
                                CssClass="Profiletitletxt" meta:resourcekey="lblIsPendingLeaveResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radIsPendingLeave" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radIsPendingLeaveResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource29"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource30"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblIsApproveLeave" runat="server" Text="Fetch Approve Leave"
                                CssClass="Profiletitletxt" meta:resourcekey="lblIsApproveLeaveResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radIsApproveLeave" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radIsApproveLeaveResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource31"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource32"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblIsTrainingLeave" runat="server" Text="Fetch Training Leave"
                                CssClass="Profiletitletxt" meta:resourcekey="lblIsTrainingLeaveResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radIsTrainingLeave" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radIsTrainingLeaveResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource33"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource34"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblIsExtraInfoLeave" runat="server" Text="Fetch Extra Info Leave"
                                CssClass="Profiletitletxt" meta:resourcekey="lblIsExtraInfoLeaveResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radIsExtraInfoLeave" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radIsExtraInfoLeaveResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource35"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource36"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblIsDutyLeave" runat="server" Text="Fetch Duty Leave"
                                CssClass="Profiletitletxt" meta:resourcekey="lblIsDutyLeaveResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radIsDutyLeave" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radIsDutyLeaveResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource37"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource38"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblRunTimesByMinutes" runat="server" CssClass="Profiletitletxt" Text="Run Times By Minutes (1-60)" meta:resourcekey="lblRunTimesByMinutesResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="txtRunTimesByMinutes" MinValue="1" MaxValue="60"
                                Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" DbValueFactor="1" LabelWidth="64px" meta:resourcekey="txtRunTimesByMinutesResource1">
                                <NegativeStyle Resize="None" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" ZeroPattern="n" />
                                <EmptyMessageStyle Resize="None" />
                                <ReadOnlyStyle Resize="None" />
                                <FocusedStyle Resize="None" />
                                <DisabledStyle Resize="None" />
                                <InvalidStyle Resize="None" />
                                <HoveredStyle Resize="None" />
                                <EnabledStyle Resize="None" />
                            </telerik:RadNumericTextBox>

                            <asp:RequiredFieldValidator ID="reqtxtRunTimesByMinutes" runat="server" ControlToValidate="txtRunTimesByMinutes"
                                Display="None" ErrorMessage="Please enter runtimes by minutes" ValidationGroup="ReligionGroup" meta:resourcekey="reqtxtRunTimesByMinutesResource1"></asp:RequiredFieldValidator>

                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" CssClass="AISCustomCalloutStyle"
                                TargetControlID="reqtxtRunTimesByMinutes" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblServiceURL" runat="server" CssClass="Profiletitletxt" Text="Service URL" meta:resourcekey="lblServiceURLResource1"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtServiceURL" runat="server" PlaceHolder="Please Enter Service URL" meta:resourcekey="txtServiceURLResource1"></asp:TextBox>

                            <asp:RequiredFieldValidator SkinID="RequiredFieldValidator1" ID="reqtxtServiceURL"
                                runat="server" ControlToValidate="txtServiceURL" Display="None" ErrorMessage="Please enter service url"
                                ValidationGroup="ReligionGroup" meta:resourcekey="reqtxtServiceURLResource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" CssClass="AISCustomCalloutStyle"
                                TargetControlID="reqtxtServiceURL" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblServiceUserName" runat="server" CssClass="Profiletitletxt" Text="Service User Name" meta:resourcekey="lblServiceUserNameResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtServiceUserName" runat="server" PlaceHolder="User Name" meta:resourcekey="txtServiceUserNameResource1"></asp:TextBox>

                            <asp:RequiredFieldValidator SkinID="RequiredFieldValidator1" ID="reqServiceUserName"
                                runat="server" ControlToValidate="txtServiceUserName" Display="None" ErrorMessage="Please enter service user name"
                                ValidationGroup="ReligionGroup" meta:resourcekey="reqServiceUserNameResource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                                TargetControlID="reqServiceUserName" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblServicePassword" runat="server" CssClass="Profiletitletxt" Text="Service Password" meta:resourcekey="lblServicePasswordResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtServicePassword" runat="server" PlaceHolder="Password" meta:resourcekey="txtServicePasswordResource1"></asp:TextBox>

                            <asp:RequiredFieldValidator SkinID="RequiredFieldValidator1" ID="reqServicePassword"
                                runat="server" ControlToValidate="txtServicePassword" Display="None" ErrorMessage="Please enter service password"
                                ValidationGroup="ReligionGroup" meta:resourcekey="reqServicePasswordResource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="AISCustomCalloutStyle"
                                TargetControlID="reqServicePassword" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblEmailErrorReceiver" runat="server" CssClass="Profiletitletxt" Text="Email Error Receiver" meta:resourcekey="lblEmailErrorReceiverResource1"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtEmailErrorReceiver" runat="server"
                                PlaceHolder="e.g. info@Smartv.ae" meta:resourcekey="txtEmailErrorReceiverResource1"></asp:TextBox>

                            <asp:RequiredFieldValidator SkinID="RequiredFieldValidator1" ID="reqEmailErrorReceiver"
                                runat="server" ControlToValidate="txtEmailErrorReceiver" Display="None" ErrorMessage="Please enter email error receiver"
                                ValidationGroup="ReligionGroup" meta:resourcekey="reqEmailErrorReceiverResource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" CssClass="AISCustomCalloutStyle"
                                TargetControlID="reqEmailErrorReceiver" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblEmailPortNumber" runat="server" CssClass="Profiletitletxt" Text="Email Port Number" meta:resourcekey="lblEmailPortNumberResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtEmailPortNumber" runat="server" PlaceHolder="110, 587, 25, 80" meta:resourcekey="txtEmailPortNumberResource1"></asp:TextBox>

                            <asp:RequiredFieldValidator SkinID="RequiredFieldValidator1" ID="reqEmailPortNumber"
                                runat="server" ControlToValidate="txtEmailPortNumber" Display="None" ErrorMessage="Please enter email port"
                                ValidationGroup="ReligionGroup" meta:resourcekey="reqEmailPortNumberResource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" CssClass="AISCustomCalloutStyle"
                                TargetControlID="reqEmailPortNumber" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblEmailEnableSsl" runat="server" Text="Email Enable Ssl"
                                CssClass="Profiletitletxt" meta:resourcekey="lblEmailEnableSslResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radEmailEnableSsl" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radEmailEnableSslResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource39"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource40"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblReprocessFirstSchedule" runat="server" CssClass="Profiletitletxt" Text="Reprocess First Schedule (0-23)" meta:resourcekey="lblReprocessFirstScheduleResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="radReprocessFirstSchedule" MinValue="0" MaxValue="23"
                                Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" DbValueFactor="1" LabelWidth="64px" meta:resourcekey="radReprocessFirstScheduleResource1">
                                <NegativeStyle Resize="None" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" ZeroPattern="n" />
                                <EmptyMessageStyle Resize="None" />
                                <ReadOnlyStyle Resize="None" />
                                <FocusedStyle Resize="None" />
                                <DisabledStyle Resize="None" />
                                <InvalidStyle Resize="None" />
                                <HoveredStyle Resize="None" />
                                <EnabledStyle Resize="None" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblReprocessSecondSchedule" runat="server" CssClass="Profiletitletxt" Text="Reprocess Second Schedule (0-23)" meta:resourcekey="lblReprocessSecondScheduleResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="radReprocessSecondSchedule" MinValue="0" MaxValue="23"
                                Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" DbValueFactor="1" LabelWidth="64px" meta:resourcekey="radReprocessSecondScheduleResource1">
                                <NegativeStyle Resize="None" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" ZeroPattern="n" />
                                <EmptyMessageStyle Resize="None" />
                                <ReadOnlyStyle Resize="None" />
                                <FocusedStyle Resize="None" />
                                <DisabledStyle Resize="None" />
                                <InvalidStyle Resize="None" />
                                <HoveredStyle Resize="None" />
                                <EnabledStyle Resize="None" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblReprocessThirdSchedule" runat="server" CssClass="Profiletitletxt" Text="Reprocess Third Schedule (0-23)" meta:resourcekey="lblReprocessThirdScheduleResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="radReprocessThirdSchedule" MinValue="0" MaxValue="23"
                                Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" DbValueFactor="1" LabelWidth="64px" meta:resourcekey="radReprocessThirdScheduleResource1">
                                <NegativeStyle Resize="None" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" ZeroPattern="n" />
                                <EmptyMessageStyle Resize="None" />
                                <ReadOnlyStyle Resize="None" />
                                <FocusedStyle Resize="None" />
                                <DisabledStyle Resize="None" />
                                <InvalidStyle Resize="None" />
                                <HoveredStyle Resize="None" />
                                <EnabledStyle Resize="None" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblRunTimesByHours" runat="server" CssClass="Profiletitletxt" Text="Bayanati Runtimes By Hours (0-23)" meta:resourcekey="lblRunTimesByHoursResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="txtRunTimesByHours" MinValue="0" MaxValue="23"
                                Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" DbValueFactor="1" LabelWidth="64px" meta:resourcekey="txtRunTimesByHoursResource1">
                                <NegativeStyle Resize="None" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" ZeroPattern="n" />
                                <EmptyMessageStyle Resize="None" />
                                <ReadOnlyStyle Resize="None" />
                                <FocusedStyle Resize="None" />
                                <DisabledStyle Resize="None" />
                                <InvalidStyle Resize="None" />
                                <HoveredStyle Resize="None" />
                                <EnabledStyle Resize="None" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblRunat" runat="server" CssClass="Profiletitletxt" Text="Bayanati Runat (0-23)" meta:resourcekey="lblRunatResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="txtRunat" MinValue="0" MaxValue="23"
                                Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" DbValueFactor="1" LabelWidth="64px" meta:resourcekey="txtRunatResource1">
                                <NegativeStyle Resize="None" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" ZeroPattern="n" />
                                <EmptyMessageStyle Resize="None" />
                                <ReadOnlyStyle Resize="None" />
                                <FocusedStyle Resize="None" />
                                <DisabledStyle Resize="None" />
                                <InvalidStyle Resize="None" />
                                <HoveredStyle Resize="None" />
                                <EnabledStyle Resize="None" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblRunat2" runat="server" CssClass="Profiletitletxt" Text="Bayanati Runat 2 (0-23)" meta:resourcekey="lblRunat2Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="txtRunat2" MinValue="0" MaxValue="23"
                                Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" DbValueFactor="1" LabelWidth="64px" meta:resourcekey="txtRunat2Resource1">
                                <NegativeStyle Resize="None" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" ZeroPattern="n" />
                                <EmptyMessageStyle Resize="None" />
                                <ReadOnlyStyle Resize="None" />
                                <FocusedStyle Resize="None" />
                                <DisabledStyle Resize="None" />
                                <InvalidStyle Resize="None" />
                                <HoveredStyle Resize="None" />
                                <EnabledStyle Resize="None" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblRecordPerPage" runat="server" CssClass="Profiletitletxt" Text="Bayanati Record Per Page" meta:resourcekey="lblRecordPerPageResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="txtRecordPerPage" MinValue="1" MaxValue="1000"
                                Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" DbValueFactor="1" LabelWidth="64px" meta:resourcekey="txtRecordPerPageResource1">
                                <NegativeStyle Resize="None" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" ZeroPattern="n" />
                                <EmptyMessageStyle Resize="None" />
                                <ReadOnlyStyle Resize="None" />
                                <FocusedStyle Resize="None" />
                                <DisabledStyle Resize="None" />
                                <InvalidStyle Resize="None" />
                                <HoveredStyle Resize="None" />
                                <EnabledStyle Resize="None" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblIsProduction" runat="server" Text="Bayanti (For Production)"
                                CssClass="Profiletitletxt" meta:resourcekey="lblIsProductionResource1" />
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="radIsProduction" runat="server" RepeatDirection="Horizontal" meta:resourcekey="radIsProductionResource1">
                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource41"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource42"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblEntityCode" runat="server" CssClass="Profiletitletxt" Text="Bayanati Entity Code" meta:resourcekey="lblEntityCodeResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtEntityCode" runat="server" meta:resourcekey="txtEntityCodeResource1"></asp:TextBox>
                        </div>
                    </div>

                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
    <center id="UpdatePanel1" runat="server">
        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="ReligionGroup" OnClick="btnSave_Click"
            CssClass="button" meta:resourcekey="btnSaveResource1"   />
    </center>
</asp:Content>
