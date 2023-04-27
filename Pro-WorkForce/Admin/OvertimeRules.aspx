<%@ Page Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="OvertimeRules.aspx.vb" Inherits="Admin_OvertimeRules"
    Title="Overtime Rules" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
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

        function ValidateTextboxFrom() {

            var tmpTime1 = $find("<%=FromTime.ClientID %>");

            txtValidate(tmpTime1, true);

        }
        function ValidateTextboxTo() {

            var tmpTime1 = $find("<%=ToTime.ClientID %>");

            txtValidate(tmpTime1, false);

        }

        function ValidateFromTo(x) {

            var tmpFromTime = $find("<%=FromTime.ClientID %>");
            var tmpToTime = $find("<%=ToTime.ClientID %>");
            var lang = '<%= MsgLang %>'
            var t1 = tmpFromTime._projectedValue + ":00";
            var t2 = tmpToTime._projectedValue + ":00";
            if (t1 == "00:00:00") {
                ShowMessage("From Time Cannot be left as 00:00");
                return false;
            }
            if (t2 == "00:00:00") {
                ShowMessage("To Time Cannot be left as 00:00");
                return false;
            }

            t1 = t1.split(/\D/);
            t2 = t2.split(/\D/);
            var x1 = Number(t1[0]) * 60 * 60 + Number(t1[1]) * 60 + Number(t1[2]);
            var x2 = Number(t2[0]) * 60 * 60 + Number(t2[1]) * 60 + Number(t2[2]);
            if (x2 > x1) {
                var s = x2 - x1;

                var m = Math.floor(s / 60); s = s % 60;
                var h = Math.floor(m / 60); m = m % 60;
                var d = Math.floor(h / 24); h = h % 24;
                if (h <= 9)
                    h = "0" + h;
                if (m <= 9)
                    m = "0" + m;


            }
            else {
                if (lang == "en") {
                    ShowMessage("To Time Should Be Greater Than Or Equal To From Time");
                }
                else {
                    ShowMessage("الى وقت يجب ان يكون اكبر من او يساوي من وقت");
                }
                return false;
            }
        }


        function hideValidatorCalloutTab() {
            try {
                if (AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout != null) {
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.hide();


                }
            }
            catch (err) {
            }
            return false;
        }

        function OpenOvertimeTypes() {
            oWindow = radopen("Overtime_Types_PopUp.aspx", "RadWindow1");

            //OpenlobiWindow('&nbsp;', '../Admin/Overtime_Types_PopUp.aspx');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab"
                        meta:resourcekey="TabContainer1Resource1">
                        <cc1:TabPanel ID="Tab1" runat="server" HeaderText="Overtime Rule" meta:resourcekey="Tab1Resource1">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:Label ID="lblEngname" runat="server" CssClass="Profiletitletxt" Text="English Name"
                                            meta:resourcekey="lblEngnameResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TxtRuleName" runat="server" meta:resourcekey="TxtRuleNameResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqRuleName" runat="server" ControlToValidate="TxtRuleName"
                                            Display="None" ErrorMessage="Please Enter a Rule Name" ValidationGroup="Grp1"
                                            meta:resourcekey="reqRuleNameResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ExtenderreqRuleName" runat="server" CssClass="AISCustomCalloutStyle"
                                            Enabled="True" TargetControlID="reqRuleName" WarningIconImageUrl="~/images/warning1.png">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:Label ID="lblRuleArName" runat="server" CssClass="Profiletitletxt" Text="Arabic Name"
                                            meta:resourcekey="lblRuleArNameResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtRuleArName" runat="server" meta:resourcekey="txtRuleArNameResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqRuleArName" runat="server" ControlToValidate="txtRuleArName"
                                            Display="None" ErrorMessage="Please Enter arabic rule Name" ValidationGroup="Grp1"
                                            meta:resourcekey="reqRuleArNameResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ExtenderreqRuleArName" runat="server" CssClass="AISCustomCalloutStyle"
                                            Enabled="True" TargetControlID="reqRuleArName" WarningIconImageUrl="~/images/warning1.png">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:Label ID="lblOvertimeEligibility" runat="server" CssClass="Profiletitletxt"
                                            Text="Overtime Eligibility" meta:resourcekey="lblOvertimeEligibilityResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-8">
                                        <asp:RadioButtonList ID="rdbOTEligibility" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="True" meta:resourcekey="rdbOTEligibilityResource1">
                                            <asp:ListItem Text="Overtime Allowed" Value="1" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                            <asp:ListItem Text="Overtime Not Allowed" Value="0" Selected="True" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div id="dvOtEligibility" runat="server" visible="False">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="Minimum Overtime Duration/Mins"
                                                meta:resourcekey="Label4Resource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <telerik:RadNumericTextBox ID="TxtMinOvertime" runat="server" Culture="English (United States)"
                                                MinValue="0" MaxValue="1440" DataType="System.Int32" Skin="Vista" LabelCssClass=""
                                                meta:resourcekey="TxtMinOvertimeResource1">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" GroupSizes="1" />
                                            </telerik:RadNumericTextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblMaxOverTime" runat="server" CssClass="Profiletitletxt" Text="Maximum Overtime Duration/Mins"
                                                meta:resourcekey="lblMaxOverTimeResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <telerik:RadNumericTextBox ID="txtMaxOvertime" runat="server" Culture="English (United States)"
                                                MinValue="0" MaxValue="1440" DataType="System.Int32" Skin="Vista" LabelCssClass=""
                                                meta:resourcekey="txtMaxOvertimeResource1">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" GroupSizes="1" />
                                            </telerik:RadNumericTextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblManageOvertime" CssClass="Profiletitletxt" runat="server" Text="Manage Overtime"
                                                meta:resourcekey="lblManageOvertimeResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:RadioButtonList ID="rblManageOvertime" runat="server" AutoPostBack="true">
                                                <%-- meta:resourcekey="rdbManageOvertimeResource8"--%>
                                                <asp:ListItem Text="Calculate After Schedule" Value="2" meta:resourcekey="ListItemResource15"></asp:ListItem>
                                                <asp:ListItem Text="Calculate Before Schedule" Value="1" meta:resourcekey="ListItemResource16"></asp:ListItem>
                                                <asp:ListItem Text="Calculate Before and After Schedule" Value="3" meta:resourcekey="ListItemResource17"></asp:ListItem>
                                                <asp:ListItem Text="Calculate Real Time In (Deduct Lost time)" Value="4" meta:resourcekey="CalculateRealtimeResource17"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnlDeduct" runat="server" Visible="False">
                                        <div class="row">
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-4">
                                                <asp:CheckBox ID="chkDeduct" runat="server" Text="Deduct From High Time" meta:resourcekey="lblDeductResource1" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblApprovalRequired" runat="server" CssClass="Profiletitletxt" Text="Approval Required To Be Considered"
                                                meta:resourcekey="lblApprovalRequiredResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:RadioButtonList ID="rdbApprovalReqd" runat="server" RepeatDirection="Horizontal"
                                                AutoPostBack="true" meta:resourcekey="rdbApprovalReqdResource1">
                                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0" Selected="True" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div id="trApprovalBy" runat="server">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:Label ID="lblApprovalBy" runat="server" CssClass="Profiletitletxt" Text="Overtime Rules Approval By"
                                                    meta:resourcekey="lblApprovalByResource1"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:RadioButtonList ID="rlstApproval" runat="server" RepeatDirection="Vertical">
                                                    <asp:ListItem Text="Direct Manager Only" Value="1" meta:resourcekey="rdbtnDirectmgrResource1"></asp:ListItem>
                                                    <asp:ListItem Text="Human Resource Only" Value="2" meta:resourcekey="rdbtnHROnlyResource1"></asp:ListItem>
                                                    <asp:ListItem Text="Both" Value="3" meta:resourcekey="rdbtnBothResource1"></asp:ListItem>
                                                    <asp:ListItem Text="Direct & Second Manager" Value="5" meta:resourcekey="rdbtnSecondResource1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:Label ID="lblApprovalAbove" runat="server" Text="Approval Required if Duration Exceed" meta:resourcekey="lblApprovalAboveResource1"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <telerik:RadNumericTextBox ID="txtRequiredDuration" MinValue="1" MaxValue="1440"
                                                    Skin="Vista" runat="server" Culture="en-US" LabelCssClass="">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:Label ID="lblMinutes" runat="server" Text="Minute(s)" meta:resourcekey="lblMinutesResource1"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3"></div>
                                        <div class="col-md-3">
                                            <asp:LinkButton ID="lnkOvertimeTypes" runat="server" Text="Overtime Types" OnClientClick="OpenOvertimeTypes();" meta:resourcekey="lnkOvertimeTypesResource1"></asp:LinkButton>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblNormalDay" runat="server" Text="Consider Working Day" meta:resourcekey="lblNormalDayResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <telerik:RadComboBox ID="RadComboBoxNormalDay" runat="server" MarkFirstMatch="true" ExpandDirection="Up"
                                                Width="210px" meta:resourcekey="RadComboBoxNormalDayResource1">
                                            </telerik:RadComboBox>

                                            <asp:RequiredFieldValidator ID="rfvNormalDay" runat="server" ControlToValidate="RadComboBoxNormalDay" InitialValue="--Please Select--"
                                                Display="None" ErrorMessage="Please Select Overtime Type" ValidationGroup="Grp1"
                                                meta:resourcekey="rfvNormalDayResource1">
                                            </asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="vceNormalDay" runat="server" CssClass="AISCustomCalloutStyle"
                                                TargetControlID="rfvNormalDay" WarningIconImageUrl="~/images/warning1.png"
                                                Enabled="True">
                                            </cc1:ValidatorCalloutExtender>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblOffDay" runat="server" CssClass="Profiletitletxt" Text="Consider Off Day"
                                                meta:resourcekey="lblOffDayResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <telerik:RadComboBox ID="RadComboBoxOffDay" runat="server" MarkFirstMatch="true" ExpandDirection="Up"
                                                Width="210px" meta:resourcekey="RadComboBoxOffDayResource1">
                                            </telerik:RadComboBox>

                                            <asp:RequiredFieldValidator ID="rfvOffDay" runat="server" ControlToValidate="RadComboBoxOffDay" InitialValue="--Please Select--"
                                                Display="None" ErrorMessage="Please Select Overtime Type" ValidationGroup="Grp1"
                                                meta:resourcekey="rfvOffDayResource1">
                                            </asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="vceOffDay" runat="server" CssClass="AISCustomCalloutStyle"
                                                TargetControlID="rfvOffDay" WarningIconImageUrl="~/images/warning1.png"
                                                Enabled="True">
                                            </cc1:ValidatorCalloutExtender>

                                        </div>
                                        <%-- <div class="col-md-3">
                                            <asp:RadioButtonList ID="rdbconsiderOffDay" runat="server" RepeatDirection="Horizontal"
                                                meta:resourcekey="rdbconsiderOffDayResource1">
                                                <asp:ListItem Text="High" Value="1" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                                <asp:ListItem Text="Low" Value="0" Selected="True" meta:resourcekey="ListItemResource6"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblPayRate1" runat="server" CssClass="Profiletitletxt" Text="Pay Rate"
                                                meta:resourcekey="lblPayRate1Resource1" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <telerik:RadNumericTextBox ID="txtPayRate1" runat="server" MinValue="0" Visible="false">
                                                <NumberFormat DecimalDigits="1" />
                                            </telerik:RadNumericTextBox>

                                        </div>--%>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text="Consider Holiday"
                                                meta:resourcekey="Label1Resource1"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <telerik:RadComboBox ID="RadComboBoxHoliday" runat="server" MarkFirstMatch="true" ExpandDirection="Up"
                                                Width="210px" meta:resourcekey="RadComboBoxHolidayResource1">
                                            </telerik:RadComboBox>

                                            <asp:RequiredFieldValidator ID="rfvHoliday" runat="server" ControlToValidate="RadComboBoxHoliday" InitialValue="--Please Select--"
                                                Display="None" ErrorMessage="Please Select Overtime Type" ValidationGroup="Grp1"
                                                meta:resourcekey="rfvHolidayResource1">
                                            </asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="vceHoliday" runat="server" CssClass="AISCustomCalloutStyle"
                                                TargetControlID="rfvHoliday" WarningIconImageUrl="~/images/warning1.png"
                                                Enabled="True">
                                            </cc1:ValidatorCalloutExtender>

                                        </div>
                                        <%--<div class="col-md-3">
                                            <asp:RadioButtonList ID="rdbConsiderHoliday" runat="server" RepeatDirection="Horizontal"
                                                meta:resourcekey="rdbConsiderHolidayResource1">
                                                <asp:ListItem Text="High" Value="1" meta:resourcekey="ListItemResource7"></asp:ListItem>
                                                <asp:ListItem Text="Low" Value="0" Selected="True" meta:resourcekey="ListItemResource8"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblPayRate2" runat="server" CssClass="Profiletitletxt" Text="Pay Rate"
                                                meta:resourcekey="lblPayRate2Resource1" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <telerik:RadNumericTextBox ID="txtPayRate2" runat="server" MinValue="0" Visible="false">
                                                <NumberFormat DecimalDigits="1" />
                                            </telerik:RadNumericTextBox>
                                        </div>--%>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblReligionHoliday" runat="server" CssClass="Profiletitletxt" Text="Consider Religion Holiday"
                                                meta:resourcekey="lblReligionHolidayResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <telerik:RadComboBox ID="RadComboBoxReligionHoliday" runat="server" MarkFirstMatch="true" ExpandDirection="Up"
                                                Width="210px" meta:resourcekey="RadComboBoxReligionHolidayResource1">
                                            </telerik:RadComboBox>

                                            <asp:RequiredFieldValidator ID="rfvReligionHoliday" runat="server" ControlToValidate="RadComboBoxReligionHoliday" InitialValue="--Please Select--"
                                                Display="None" ErrorMessage="Please Select Overtime Type" ValidationGroup="Grp1"
                                                meta:resourcekey="rfvReligionHolidayResource1">
                                            </asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="vceReligionHoliday" runat="server" CssClass="AISCustomCalloutStyle"
                                                TargetControlID="rfvReligionHoliday" WarningIconImageUrl="~/images/warning1.png"
                                                Enabled="True">
                                            </cc1:ValidatorCalloutExtender>
                                        </div>
                                    </div>
                                    <%--<tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" CssClass="Profiletitletxt" Text="Overtime Compensate Late Time"
                                                        meta:resourcekey="Label2Resource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdbOtCompLateTime" runat="server" RepeatDirection="Horizontal"
                                                        meta:resourcekey="rdbOtCompLateTimeResource1">
                                                        <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource9"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0" Selected="True" meta:resourcekey="ListItemResource10"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdbOTLeaveOrFinance" runat="server" meta:resourcekey="rdbOTLeaveOrFinanceResource1">
                                                        <asp:ListItem Text="Overtime To Be Added To Leave Balance " Value="1" Selected="True"
                                                            meta:resourcekey="ListItemResource11"></asp:ListItem>
                                                        <asp:ListItem Text="Overtime To Be Consider As Financial" Value="0" meta:resourcekey="ListItemResource12"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblHighRate" runat="server" CssClass="Profiletitletxt" Text="High Rate"
                                                        meta:resourcekey="lblHighRateResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="TxtHighRate" runat="server" Culture="English (United States)"
                                                        Skin="Vista" LabelCssClass="" meta:resourcekey="TxtHighRateResource1">
                                                        <NumberFormat DecimalDigits="2" GroupSeparator="" GroupSizes="1" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblLowRate" runat="server" CssClass="Profiletitletxt" Text="Low Rate"
                                                        meta:resourcekey="lblLowRateResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="TxtLowRate" runat="server" Culture="English (United States)"
                                                        Skin="Vista" LabelCssClass="" meta:resourcekey="TxtLowRateResource1">
                                                        <NumberFormat DecimalDigits="2" GroupSeparator="" GroupSizes="1" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>--%>
                                </div>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="tabHighTime" runat="server" HeaderText="High Time" meta:resourcekey="tabHighTimeResource1">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:Label ID="Label6" runat="server" CssClass="Profiletitletxt" Text="Consider Specific Time As High Overtime "
                                            meta:resourcekey="Label6Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:RadioButtonList ID="rdbHighHasTime" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="True" meta:resourcekey="rdbHighHasTimeResource1">
                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource13"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0" Selected="True" meta:resourcekey="ListItemResource14"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div id="dvHasTime" runat="server" visible="False">
                                    <div class="row">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label ID="Label7" runat="server" CssClass="Profiletitletxt" Text="From Time"
                                                meta:resourcekey="Label7Resource1"></asp:Label>
                                        </div>
                                        <div class="col-md-2">
                                            <telerik:RadMaskedTextBox ID="FromTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                                DisplayMask="##:##" Text='0000' LabelCssClass="" meta:resourcekey="FromTimeResource1">
                                                <ClientEvents OnBlur="ValidateTextboxFrom" />
                                            </telerik:RadMaskedTextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label ID="Label3" runat="server" CssClass="Profiletitletxt" Text="To Time" meta:resourcekey="Label3Resource1"></asp:Label>
                                        </div>
                                        <div class="col-md-2">
                                            <telerik:RadMaskedTextBox ID="ToTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                                DisplayMask="##:##" Text='0000' LabelCssClass="" meta:resourcekey="ToTimeResource1">
                                                <ClientEvents OnBlur="ValidateTextboxTo" />
                                            </telerik:RadMaskedTextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4"></div>
                                        <div class="col-md-2">
                                            <asp:Label ID="lblOvertimeTypeHighTime" runat="server" Text="Overtime Type" meta:resourcekey="lblOvertimeTypeHighTimeResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-2">
                                            <telerik:RadComboBox ID="RadComboBoxOvertimeTypeHighTime" runat="server" MarkFirstMatch="true" ExpandDirection="Up"
                                                Width="210px" meta:resourcekey="RadComboBoxOvertimeTypeHighTimeResource1">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button" meta:resourcekey="btnAddResource1" />
                                            <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="button" meta:resourcekey="btnRemoveResource1" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="table-responsive">
                                            <telerik:RadGrid ID="dgrdHighTime" runat="server" AllowPaging="True" PageSize="7"
                                                GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True"
                                                meta:resourcekey="dgrdHighTimeResource1">
                                                <SelectedItemStyle ForeColor="Maroon" />
                                                <MasterTableView IsFilterItemExpanded="False" AutoGenerateColumns="False" DataKeyNames="HighTimeId,FromTime,ToTime,FK_OvertimeTypeId">
                                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                    <Columns>
                                                        <telerik:GridTemplateColumn AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="HighTimeId" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="FromTime" HeaderText="From Time" meta:resourcekey="GridBoundColumnResource9" />
                                                        <telerik:GridBoundColumn DataField="ToTime" HeaderText="To Time" meta:resourcekey="GridBoundColumnResource10" />
                                                        <telerik:GridBoundColumn DataField="FK_RuleId" Visible="false" />
                                                    </Columns>
                                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                </MasterTableView>
                                                <GroupingSettings CaseSensitive="False" />
                                                <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="ibtnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="Grp1"
                        meta:resourcekey="ibtnSaveResource1" />
                    <asp:Button ID="ibtnDelete" runat="server" Text="Delete" CssClass="button" CausesValidation="False"
                        meta:resourcekey="ibtnDeleteResource1" OnClientClick="return ValidateDelete();" />
                    <asp:Button ID="ibtnRest" runat="server" Text="Clear" CssClass="button" CausesValidation="False"
                        meta:resourcekey="ibtnRestResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdOverTimeRules"
                        Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadGrid ID="dgrdOverTimeRules" runat="server" AllowSorting="True" AllowPaging="True"
                        PageSize="25" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        ShowFooter="True" OnItemCommand="dgrdOverTimeRules_ItemCommand" meta:resourcekey="dgrdOverTimeRulesResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False"
                            DataKeyNames="OvertimeRuleId,RuleName">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="RuleName" SortExpression="RuleName" HeaderText="English Name"
                                    meta:resourcekey="GridBoundColumnResource5" />
                                <telerik:GridBoundColumn DataField="RuleArabicName" SortExpression="RuleArabicName"
                                    HeaderText="Arabic Name" meta:resourcekey="GridBoundColumnResource6" />
                                <telerik:GridBoundColumn DataField="MinOvertime" SortExpression="MinOvertime" HeaderText="Min Over Time"
                                    meta:resourcekey="GridBoundColumnResource7" />
                                <telerik:GridBoundColumn DataField="OvertimeRuleId" SortExpression="OvertimeRuleId"
                                    AllowFiltering="false" Visible="false" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                            ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Behavior="Default"
        EnableShadow="True" InitialBehavior="None">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Animation="FlyIn" Behavior="Close, Move, Resize"
                Behaviors="Close, Move, Resize" EnableShadow="True" Height="450px" IconUrl="~/images/HeaderWhiteChrome.jpg"
                InitialBehavior="None" ShowContentDuringLoad="False" VisibleStatusbar="False"
                Skin="Windows7" Width="700px">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <script type="text/javascript">
        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdOverTimeRules.ClientID %>");
            var masterTable = grid.get_masterTableView();
            var value = false;
            for (var i = 0; i < masterTable.get_dataItems().length; i++) {
                var gridItemElement = masterTable.get_dataItems()[i].findElement("chk");
                if (gridItemElement.checked) {
                    value = true;
                }
            }
            if (value === false) {
                ShowMessage("<%= Resources.Strings.ErrorDeleteRecourd %>");
            }
            return value;
        }
    </script>
</asp:Content>
