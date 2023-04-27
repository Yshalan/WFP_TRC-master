<%@ Page Title="" Theme="SvTheme" Language="VB" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="EmailConfigurations.aspx.vb" Inherits="Admin_EmailConfigurations" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblSmtpServer" runat="server" Text="SMTP Server" CssClass="Profiletitletxt"
                        meta:resourcekey="lblSmtpServerResource1" />
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtSmtpServer" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvSmtpServer" runat="server" ControlToValidate="txtSmtpServer"
                        ErrorMessage="Please enter SMTP server" ValidationGroup="grpEmailConfig" Display="None"
                        meta:resourcekey="rfvSmtpServerResource1" />
                    <cc1:ValidatorCalloutExtender ID="vceSmtpServer" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvSmtpServer" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblSMTPUserName" runat="server" Text="SMTP User Name" CssClass="Profiletitletxt"
                        meta:resourcekey="lblSMTPUserNameResource1" />
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtSMTPUserName" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvSmtpUserName" runat="server" ControlToValidate="txtSMTPUserName"
                        ErrorMessage="Please enter SMTP User Name" ValidationGroup="grpEmailConfig" Display="None"
                        meta:resourcekey="rfvSmtpUserNameResource1" />
                    <cc1:ValidatorCalloutExtender ID="vceSmtpUserName" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvSmtpUserName" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblSMTPPssword" runat="server" Text="SMTP Password" CssClass="Profiletitletxt"
                        meta:resourcekey="lblSMTPPsswordResource1" />
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtSMTPPassword" runat="server" type="password" />
                    <asp:RequiredFieldValidator ID="rfvSmtpPassword" runat="server" ControlToValidate="txtSMTPPassword"
                        ErrorMessage="Please enter SMTP Password" ValidationGroup="grpEmailConfig" Display="None"
                        meta:resourcekey="rfvSmtpPasswordResource1" />
                    <cc1:ValidatorCalloutExtender ID="vceSmtpPassword" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvSmtpPassword" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblSMTPConfirmPssword" runat="server" Text="SMTP Confirm Password"
                        CssClass="Profiletitletxt" meta:resourcekey="lblSMTPConfirmPsswordResource1" />
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtSMTPConfirmPassword" runat="server" type="password" />
                    <asp:CompareValidator ID="valCompConfirm" runat="server" ControlToCompare="txtSMTPPassword"
                        ControlToValidate="txtSMTPConfirmPassword" Display="None" ErrorMessage="Please Confirm the Password"
                        ValidationGroup="grpEmailConfig" meta:resourcekey="valCompConfirmResource1" />
                    <cc1:ValidatorCalloutExtender ID="vceSmtpConfirmPassword" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="valCompConfirm" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblEmailfrom" runat="server" Text="Email From" CssClass="Profiletitletxt"
                        meta:resourcekey="lblEmailfromResource1" />
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtEmailFrom" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvEmailfrom" runat="server" ControlToValidate="txtEmailFrom"
                        ErrorMessage="Please enter Email from" ValidationGroup="grpEmailConfig" Display="None"
                        meta:resourcekey="rfvEmailfromResource1" />
                    <cc1:ValidatorCalloutExtender ID="vceEmailfrom" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvEmailfrom" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="regEmailFrom" runat="server" ControlToValidate="txtEmailFrom"
                        ErrorMessage="Please enter correct email" ValidationGroup="grpEmailConfig" Display="None"
                        meta:resourcekey="regEmailFromResource1" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    <cc1:ValidatorCalloutExtender ID="vceRegEmailfrom" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="regEmailFrom" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chkEnableEmailService" Text="Enable Email Service" runat="server"
                        AutoPostBack="true" meta:resourcekey="lblEnableEmailServiceResource1" />
                    <asp:CheckBox ID="chkHasEmailApproval" Text="Has Email Approval" runat="server" meta:resourcekey="lblHasEmailApprovalResource1" />
                    <asp:CheckBox ID="chkEnableSMSService" Text="Enable SMS Service" runat="server" meta:resourcekey="lblEnableSMSServiceResource1" />
                </div>
            </div>
            <hr />
            <div id="trEmailConfig" runat="server" visible="false" class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblEmailNotification" runat="server" CssClass="Profiletitletxt" Text="Email Notifications"
                        meta:resourcekey="lblEmailNotificationResource1"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:RadioButtonList ID="rdlEmailNotifications" runat="server" meta:resourcekey="rdlEmailNotificationsResource1"
                        RepeatDirection="Vertical">
                        <asp:ListItem Text="Send To HR Group" Value="1" meta:resourcekey="rdbtnSendToHRGroupResource1">
                        </asp:ListItem>
                        <asp:ListItem Text="Send To HR Group & HR Employee" Value="2" meta:resourcekey="rdbtnSendToHRGroupHREmployeeResource1">
                        </asp:ListItem>
                        <asp:ListItem Text="Send To HR Employee Only" Value="3" meta:resourcekey="rdbtnSendToHREmployeeOnlyResource1">
                        </asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblIncludeNotifications_CoordinatorTypes" runat="server" Text="Include Coordinator Types In Notifications"
                        meta:resourcekey="lblIncludeNotifications_CoordinatorTypesResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="rblIncludeNotifications_CoordinatorTypes" runat="server">
                        <asp:ListItem Value="1" Text="Yes" meta:resourcekey="ListItemYesResource1"></asp:ListItem>
                        <asp:ListItem Value="2" Text="No" Selected="True" meta:resourcekey="ListItemNoResource1"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row" id="dvMonthlyOrWeeklyNotificationSettings" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblSendManagerNotification" runat="server" Text="Send Notification To Manager"
                            CssClass="Profiletitletxt" meta:resourcekey="lblSendManagerNotificationResource1"
                            Font-Bold="True" />
                    </div>
                    <div class="col-md-8">
                        <asp:CheckBoxList ID="chklSendManagerNotification" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" meta:resourcekey="chklSendManagerNotificationResource1" RepeatColumns="2">
                            <asp:ListItem Text="Weekly" Value="2" meta:resourcekey="ListItemResource22">
                            </asp:ListItem>
                            <asp:ListItem Text="Monthly" Value="3" meta:resourcekey="ListItemResource23">
                            </asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="row" id="trMgrDaily" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblMgrDaily" runat="server" Text="Daily Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblDailyResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 16px;">
                        <asp:RadioButtonList ID="rblMgrDailyWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblMgrDailyMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="trMgrDetailed" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblMgrDetailed" runat="server" Text="Detailed Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblDetailedResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblMgrDetailedWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblMgrDetailedMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="trMgrSummary" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblMgrSummary" runat="server" Text="Summary Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblSummaryResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblMgrSummaryWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblMgrSummaryMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="trMgrAbsent" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblMgrAbsentWeekly" runat="server" Text="Absent Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblAbsentResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblMgrAbsentWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblMgrAbsentMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="trMgrViolation" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblMgrViolation" runat="server" Text="Violations Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblViolationResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblMgrViolationWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblMgrViolationMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="dvMgrEmpSummaryAttendance" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblMgrEmpSummaryAttendance" runat="server" Text="Employee Summary Attendance Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblMgrEmpSummaryAttendanceResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblMgrEmpSummaryAttendanceWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblMgrEmpSummaryAttendanceMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="dvMgrEmpDeduction" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblMgrEmpDeduction" runat="server" Text="Monthly Deduction Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblMgrEmpDeductionResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblMgrEmpDeductionWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblMgrEmpDeductionMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="dvMgrDeductionPerPolicy" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="Label1" runat="server" Text="Deduction Per Policy Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblMgrDeductionPerPolicyResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblMgrDeductionPerPolicyWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblMgrDeductionPerPolicyMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblSendHRNotification" runat="server" Text="Send To HR Notification"
                            CssClass="Profiletitletxt" meta:resourcekey="lblSendHRNotificationResource1" Font-Bold="True" />
                    </div>
                    <div class="col-md-8">
                        <asp:CheckBoxList ID="chklSendHRNotification" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" meta:resourcekey="chklSendHRNotificationResource1">
                            <%-- <asp:ListItem Text="Daily" Value="1" meta:resourcekey="ListItemResource21">
                                                    </asp:ListItem>--%>
                            <asp:ListItem Text="Weekly" Value="2" meta:resourcekey="ListItemResource22">
                            </asp:ListItem>
                            <asp:ListItem Text="Monthly" Value="3" meta:resourcekey="ListItemResource23">
                            </asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="row" id="trHRDaily" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblHRDaily" runat="server" Text="Daily Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblDailyResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblHRDailyWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblHRDailyMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="trHRDetailed" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblHRDetailed" runat="server" Text="Detailed Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblDetailedResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblHRDetailedWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblHRDetailedMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="trHRSummary" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblHRSummary" runat="server" Text="Summary Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblSummaryResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblHRSummaryWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblHRSummaryMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="trHRAbsent" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblHRAbsent" runat="server" Text="Absent Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblAbsentResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblHRAbsentWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblHRAbsentMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="trHRViolation" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblHRViolation" runat="server" Text="Violations Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblViolationResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblHRViolationWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblHRViolationMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="dvHrEmpDeduction" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblHrEmpDeduction" runat="server" Text="Monthly Deduction Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblHrEmpDeductionResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblHrEmpDeductionWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblHrEmpDeductionMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="dvHRDeductionPerPolicy" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblHRDeductionPerPolicy" runat="server" Text="Deduction Per Policy Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblHRDeductionPerPolicyResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblHRDeductionPerPolicyWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblHRDeductionPerPolicyMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblSendEntityMgrNotification" runat="server" Text="Send To Entity Manager Notification"
                            CssClass="Profiletitletxt" meta:resourcekey="lblSendEntityMgrNotificationResource1" Font-Bold="True" />
                    </div>
                    <div class="col-md-8">
                        <asp:CheckBoxList ID="chklSendEntityMgrNotification" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" meta:resourcekey="chklSendEntityMgrNotificationResource1">
                            <%-- <asp:ListItem Text="Daily" Value="1" meta:resourcekey="ListItemResource21">
                                                    </asp:ListItem>--%>
                            <asp:ListItem Text="Weekly" Value="2" meta:resourcekey="ListItemResource22">
                            </asp:ListItem>
                            <asp:ListItem Text="Monthly" Value="3" meta:resourcekey="ListItemResource23">
                            </asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="row" id="dvEntityMgrViolation" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblEntityMgrViolation" runat="server" Text="Violations Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblViolationResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblEntityMgrViolationWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblEntityMgrViolationMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="dvEntityManagerEmpDeduction" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblEntityManagerEmpDeduction" runat="server" Text="Monthly Deduction Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblEntityManagerEmpDeductionResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblEntityManagerEmpDeductionWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblEntityManagerEmpDeductionMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="dvEntityManagerMaxAbsent" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblEntityManagerMaxAbsent" runat="server" Text="Max Abesent Employee(s) Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblEntityManagerMaxAbsentResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblEntityManagerMaxAbsentWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblEntityManagerMaxAbsentMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="dvEntityManagerMaxDelay" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblEntityManagerMaxDelay" runat="server" Text="Max Delay Employee(s) Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblEntityManagerMaxDelayResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblEntityManagerMaxDelayWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblEntityManagerMaxDelayMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="dvEntityManagerDeductionPerPolicy" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblEntityManagerDeductionPerPolicy" runat="server" Text="Deduction Per Policy Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblEntityManagerDeductionPerPolicyResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblEntityManagerDeductionPerPolicyWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblEntityManagerDeductionPerPolicyMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblSendEmployeeNotification" runat="server" Text="Send To Employee Notification"
                            CssClass="Profiletitletxt" meta:resourcekey="lblSendEmployeeNotificationResource1" Font-Bold="True" />
                    </div>
                    <div class="col-md-8">
                        <asp:CheckBoxList ID="chklSendEmployeeNotification" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" meta:resourcekey="chklSendEmployeeNotificationResource1">
                            <%-- <asp:ListItem Text="Daily" Value="1" meta:resourcekey="ListItemResource21">
                                                    </asp:ListItem>--%>
                            <asp:ListItem Text="Weekly" Value="2" meta:resourcekey="ListItemResource22">
                            </asp:ListItem>
                            <asp:ListItem Text="Monthly" Value="3" meta:resourcekey="ListItemResource23">
                            </asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="row" id="dvEmployeeViolation" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="Label2" runat="server" Text="Violations Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblViolationResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblEmployeeViolationWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblEmployeeViolationMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="dvEmployeeAbsent" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblEmployeeAbsent" runat="server" Text="Absent Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblEmployeeAbsentResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblEmployeeAbsentWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblEmployeeAbsentMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="dvEmployeeDetailedAbsent" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblEmployeeDetailedAbsent" runat="server" Text="Detailed Absent Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblEmployeeDetailedAbsentResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblEmployeeDetailedAbsentWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblEmployeeDetailedAbsentMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="dvEmployeeLostTimeDetails" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblEmployeeLostTimeDetails" runat="server" Text="Lost Time Details Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblEmployeeLostTimeDetailsResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblEmployeeLostTimeDetailsWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblEmployeeLostTimeDetailsMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="dvEmployeeEmpDeduction" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblEmployeeEmpDeduction" runat="server" Text="Monthly Deduction Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblEmployeeEmpDeductionResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblEmployeeEmpDeductionWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblEmployeeEmpDeductionMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="dvEmployeeSummary" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblEmployeeSummary" runat="server" Text="Summary Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblEmployeeSummaryResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblEmployeeSummaryWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblEmployeeSummaryMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="dvEmployeeDeductionPerPolicy" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblEmployeeDeductionPerPolicy" runat="server" Text="Deduction Per Policy Report For" CssClass="Profiletitletxt"
                            meta:resourcekey="lblEmployeeDeductionPerPolicyResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="rblEmployeeDeductionPerPolicyWeekly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4" style="padding-right: 110px; padding: 1px;">
                        <asp:RadioButtonList ID="rblEmployeeDeductionPerPolicyMonthly" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="None" Selected="True" meta:resourcekey="ListItemNone"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Week" meta:resourcekey="ListItemWeek"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Month" meta:resourcekey="ListItemMonth"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Year" meta:resourcekey="ListItemYear"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <hr />
                <div class="row" id="trHREmailFormat" runat="server">
                    <div class="col-md-4">
                        <asp:Label ID="lblHREmailFormat" runat="server" Text="HR Report Format" CssClass="Profiletitletxt"
                            meta:resourcekey="lblHREmailFormatResource1" />
                    </div>
                    <div class="col-md-4">
                        <asp:RadioButtonList ID="cblHREmailFormat" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="PDF" Selected="True" meta:resourcekey="ListItemResource29"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Email Template" meta:resourcekey="ListItemResource30"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="dvDeductionPerPolicy_Value" runat="server" visible="false">
                    <div class="col-md-4">
                        <asp:Label ID="lblDeductionPerPolicy_Value" runat="server" Text="Deduction Report Per Policy Value To Be Considered"
                            meta:resourcekey="lblDeductionPerPolicy_ValueResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadNumericTextBox ID="txtDeductionPerPolicy_Value" MinValue="1" MaxValue="365"
                            Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="txtDeductionPerPolicy_ValueResource1">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="Label3" runat="server" Text="Hours" CssClass="Profiletitletxt" meta:resourcekey="HoursResource1" />
                    </div>
                </div>
                <div id="trMgrEmailFormat" runat="server" class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblMgrEmailFormat" runat="server" Text="Manager Report Format" CssClass="Profiletitletxt"
                            meta:resourcekey="lblMgrEmailFormatResource1" />
                    </div>
                    <div class="col-md-4" style="padding-left: 18px;">
                        <asp:RadioButtonList ID="cblMgrEmailFormat" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="PDF" Selected="True" meta:resourcekey="ListItemResource29"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Email Template" meta:resourcekey="ListItemResource30"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chkYearlyReportFromYearBegining" runat="server" Text="Yearly Report From Begining Of Year" meta:resourcekey="chkYearlyReportFromYearBeginingResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblDeductionEmailFormat" runat="server" Text="Approved Deduction Notification Format" CssClass="Profiletitletxt"
                        meta:resourcekey="lblDeductionEmailFormatResource1" />
                </div>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="rblDeductionEmailFormat" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Text="PDF" meta:resourcekey="ListItemResource29"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Email Template" Selected="True" meta:resourcekey="ListItemResource30"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:CheckBox ID="chkSendDaily_AbsentDelay_EntityMgr" runat="server" Text="Send Entity Manager(s) Daily Summary Report"
                        meta:resourcekey="chkSendDaily_AbsentDelay_EntityMgrResource1" />
                </div>
                <div class="col-md-4"></div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblEarlyOutNotifications" runat="server" CssClass="Profiletitletxt"
                        Text="Early Out Notification to be Sent After" meta:resourcekey="lblEarlyOutNotificationsResource1"
                        Visible="false"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadMaskedTextBox ID="rmtEarlyOutNotifications" runat="server" Mask="##:##"
                        TextWithLiterals="00:00" DisplayMask="##:##" Text='0000' LabelCssClass="" Visible="false"
                        meta:resourcekey="rmtEarlyOutNotificationsResource1">
                        <ClientEvents OnBlur="ValidatermtEarlyOutNotifications" />
                    </telerik:RadMaskedTextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblPermissionReminder" runat="server" CssClass="Profiletitletxt" Text="Reminder Permissions Requests For Manager after"
                        meta:resourcekey="lblPermissionReminderResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadNumericTextBox ID="radtxtPermissionReminder" MaxValue="99999" Skin="Vista"
                        runat="server" Culture="en-US" LabelCssClass="" Width="210px" meta:resourcekey="radtxtPermissionReminderManualResource1">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                    </telerik:RadNumericTextBox>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="Label12" runat="server" Text="Days" CssClass="Profiletitletxt" meta:resourcekey="lblDaysResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblReminderAbsentAfter" runat="server" CssClass="Profiletitletxt"
                        Text="Reminder Absent After" meta:resourcekey="lblReminderAbsentAfterResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadNumericTextBox ID="radReminderAbsentAfter" MaxValue="99999" Skin="Vista"
                        runat="server" Culture="en-US" LabelCssClass="" Width="210px" meta:resourcekey="radReminderAbsentAfterResource1">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                    </telerik:RadNumericTextBox>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="lblDays" runat="server" Text="Days" CssClass="Profiletitletxt" meta:resourcekey="lblDaysResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblIncompleteWorkingHrs" runat="server" Text="Notify If Not Complete Working Hours"
                        meta:resourcekey="lblIncompleteWorkingHrsResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadNumericTextBox ID="radnumIncompleteWorkingHrs" MaxValue="24" Skin="Vista" MinValue="1"
                        runat="server" Culture="en-US" LabelCssClass="" Width="210px" meta:resourcekey="radnumIncompleteWorkingHrsResource1">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                    </telerik:RadNumericTextBox>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="Label15" runat="server" Text="Hours" CssClass="Profiletitletxt" meta:resourcekey="HoursResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblHREmail" runat="server" Text="HR Group Email" CssClass="Profiletitletxt"
                        meta:resourcekey="lblHREmailResource1" />
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtHREmail" runat="server" meta:resourcekey="txtHREmailResource1" />
                    <asp:RegularExpressionValidator ID="revHREmail" ErrorMessage="Please enter correct email"
                        ControlToValidate="txtHREmail" runat="server" ValidationGroup="ReligionGroup"
                        Display="None" meta:resourcekey="revHREmailResource1" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="revHREmail" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <center id="Center1" runat="server">
                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vceRegEmailfrom"
                    CssClass="button" meta:resourcekey="btnSaveResource1" />
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ValidatermtEarlyOutNotifications() {

            var tmpTime1 = $find("<%=rmtEarlyOutNotifications.ClientID %>");

            txtValidate(tmpTime1, true);

        }
        function txtValidate(txt, IsFrom) {
            var strTime = String(txt._projectedValue);
            strTime = strTime.split(/\D/);

            if (strTime[0] == "") { strTime[0] = "00"; }
            if (strTime[1] == "") { strTime[1] = "00"; }
            if (strTime[1] > 59) {
                strTime[1] = "00";
                strTime[0] = String(Number(strTime[0]) + 1);
            }
            if (IsFrom) {
                if (strTime[0] > 23) {
                    strTime[0] = "00";
                }
            }
            else if (strTime[0] > 24) {
                strTime[0] = "24";
            }

            txt.set_value(strTime[0] + "" + strTime[1]);
            return false;
        }

    </script>
</asp:Content>
