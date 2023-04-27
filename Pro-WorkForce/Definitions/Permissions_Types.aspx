<%@ Page Title="Define Type of Permissions" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="Permissions_Types.aspx.vb" Inherits="Emp_Permissions_Types"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/MultiEmployeeFilter.ascx" TagName="MultiEmployeeFilter"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
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
        function ValidateTextboxFrom() {

            var tmpTime1 = $find("<%=radAllowedTime.ClientID %>");

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

        function CheckBoxListSelectCompany(state) {
            var chkBoxList = document.getElementById("<%= cblCompanies.ClientID%>");
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }

        function CheckBoxListSelectEntity(state) {
            var chkBoxList = document.getElementById("<%= cblEntities.ClientID %>");
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }
    </script>
    <%--  TO BE ADDED IN FUTURE WORK FOR SPECIFIC ENTITIES
           function CheckBoxListSelect(state) {
                    var chkBoxList = document.getElementById("<%= cblEmpList.ClientID %>");
                    var chkBoxCount = chkBoxList.getElementsByTagName("input");
                    for (var i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = state;
                    }
                    return false;
                }

                function CheckBoxListSelectEntity(state) {
                    var chkBoxList = document.getElementById("<%= cblEntities.ClientID %>");
                    var chkBoxCount = chkBoxList.getElementsByTagName("input");
                    for (var i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = state;
                    }
                    return false;
                }--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlPermissionTypes" runat="server">
        <ContentTemplate>

            <uc1:PageHeader ID="UserCtrlPermTypes" HeaderText="Permission Types" runat="server" />
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab"
                meta:resourcekey="TabContainer1Resource1">
                <cc1:TabPanel ID="Tab1" runat="server" HeaderText="Leave" meta:resourcekey="Tab1Resource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblName" runat="server" CssClass="Profiletitletxt" Text="English name"
                                    meta:resourcekey="lblNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtName" runat="server" meta:resourcekey="txtNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqPermissionName" runat="server" ControlToValidate="txtName"
                                    Display="None" ErrorMessage="Please enter permission english name" ValidationGroup="PermTypesGroup"
                                    meta:resourcekey="reqPermissionNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ExtenderPermissionName" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="reqPermissionName" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblArabicname" runat="server" CssClass="Profiletitletxt" Text="Arabic name"
                                    meta:resourcekey="lblArabicnameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtArabicName" runat="server" meta:resourcekey="txtArabicNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqArabicName" runat="server" ControlToValidate="txtArabicName"
                                    Display="None" ErrorMessage="Please enter permission arabic name" ValidationGroup="PermTypesGroup"
                                    meta:resourcekey="reqArabicNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ExtenderreqArabicName" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="reqArabicName" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblMinDuration" runat="server" CssClass="Profiletitletxt" Text="Minimum duration"
                                    meta:resourcekey="lblMinDurationResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtRadMinDuration" MinValue="0" MaxValue="999999999"
                                    runat="server" Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtRadMinDurationResource1">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="lblMinutes" runat="server" Text="Minutes"
                                    meta:resourcekey="lblMinutesResource1"></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="reqRadMinDuration" runat="server" ErrorMessage="Please enter minimum duration"
                                ValidationGroup="PermTypesGroup" ControlToValidate="txtRadMinDuration" Display="None"
                                meta:resourcekey="reqRadMinDurationResource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ExtenderRadMinDuration" TargetControlID="reqRadMinDuration"
                                runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblMaxDuration" runat="server" CssClass="Profiletitletxt" Text="Maximum duration"
                                    meta:resourcekey="lblMaxDurationResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtRadMaxDuration" MinValue="0" MaxValue="999999999"
                                    runat="server" Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtRadMaxDurationResource1">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />

                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="lblMinutes2" runat="server" Text="Minutes"
                                    meta:resourcekey="lblMinutes2Resource1"></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="reqRadMaxDuration" runat="server" ErrorMessage="Please enter maximum duration"
                                ValidationGroup="PermTypesGroup" ControlToValidate="txtRadMaxDuration" Display="None"
                                meta:resourcekey="reqRadMaxDurationResource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ExtendereqRadMaxDuration" TargetControlID="reqRadMaxDuration"
                                runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chckIsConsiderInWork" runat="server" Text="Consider in work"
                                    meta:resourcekey="lblIsConsiderInWorkResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkDeductFromOvertime" runat="server" Text="Deduct Balance From Overtime" AutoPostBack="true"
                                    meta:resourcekey="chkDeductFromOvertimeResource1" />
                            </div>
                        </div>
                        <div class="row" id="dvDeductMonthlyBalance" runat="server">
                            <div class="col-md-4">
                                <asp:Label ID="lblMonthlyBalance" runat="server" CssClass="Profiletitletxt" Text="Monthly balance"
                                    meta:resourcekey="lblMonthlyBalanceResource1"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <telerik:RadNumericTextBox ID="txtRadHourMonthlyBalance" MinValue="0" MaxValue="240"
                                    runat="server" Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtRadMonthlyBalanceResource1">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lblInHours" runat="server" Text="HH" meta:resourcekey="lblInHoursResource1"></asp:Label>
                                <asp:RequiredFieldValidator ID="reqHoursMonthlyBalance" runat="server" ErrorMessage="Please enter hour monthly balance"
                                    ValidationGroup="PermTypesGroup" ControlToValidate="txtRadHourMonthlyBalance"
                                    Display="None" meta:resourcekey="reqHoursMonthlyBalanceResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ExtenderHoursMonthlyBalance" TargetControlID="reqHoursMonthlyBalance"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    runat="server" Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lbl" runat="server" CssClass="Profiletitletxt" Text=":" meta:resourcekey="lblResource1"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <telerik:RadNumericTextBox ID="txtRadMonthlyBalance" MinValue="0" MaxValue="59" runat="server"
                                    Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtRadMonthlyBalanceResource1">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lblInMins" runat="server" Text="MM" meta:resourcekey="lblInMinsResource1"></asp:Label>
                                <asp:RequiredFieldValidator ID="reqMonthlyBalance" runat="server" ErrorMessage="Please enter minut monthly balance"
                                    ValidationGroup="PermTypesGroup" ControlToValidate="txtRadMonthlyBalance" Display="None"
                                    meta:resourcekey="reqMonthlyBalanceResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ExtenderMonthlyBalance" TargetControlID="reqMonthlyBalance"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    runat="server" Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row" id="dvDeductLeaveBalance" runat="server">
                            <div class="col-md-4">
                                <asp:Label ID="lblLeaveTypes" runat="server" CssClass="Profiletitletxt" Text="Deduct balance type"
                                    meta:resourcekey="lblLeaveTypesResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="cmbBxRadLeaveTypes" runat="server" MarkFirstMatch="True"
                                    Skin="Vista" meta:resourcekey="cmbBxRadLeaveTypesResource1" ExpandDirection="Up">
                                </telerik:RadComboBox>
                                <%-- <asp:RequiredFieldValidator ID="reqRadLeaveTypes" runat="server" ErrorMessage="Please select leave type"
                                                                    ValidationGroup="PermTypesGroup" ControlToValidate="cmbBxRadLeaveTypes" InitialValue="--Please Select--"
                                                                    Display="None" meta:resourcekey="reqRadLeaveTypesResource1"></asp:RequiredFieldValidator>
                                                                <cc1:ValidatorCalloutExtender ID="ExtenderRadLeaveTypes" TargetControlID="reqRadLeaveTypes"
                                                                    runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                                                    Enabled="True">
                                                                </cc1:ValidatorCalloutExtender>--%>
                            </div>
                        </div>
                        <div class="row" id="dvDeductOvertime" runat="server" visible="false">
                            <div class="col-md-4">
                                <asp:Label ID="lblOvertimeBalanceDays" runat="server" Text="Overtime Balance In The Last"
                                    meta:resourcekey="lblOvertimeBalanceDaysResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtOvertimeBalanceDays" MinValue="0" runat="server"
                                    Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtOvertimeBalanceDaysResource1">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="rfvOvertimeBalanceDays" runat="server" ErrorMessage="Please Insert Overtime Balance In The Last Day(s)"
                                    ValidationGroup="PermTypesGroup" ControlToValidate="txtOvertimeBalanceDays"
                                    Display="None" meta:resourcekey="rfvOvertimeBalanceDaysResource1" Enabled="false"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="rfvOvertimeBalanceDays"
                                    runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lblOvertimeDays" runat="server" Text="Days"
                                    meta:resourcekey="lblOvertimeDaysResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chckApprovalRequired" runat="server" Text="Approval required" AutoPostBack="true"
                                    meta:resourcekey="lblApprovalRequiredResource1" />
                            </div>
                        </div>
                        <div id="dvAutoApprove" runat="server" visible="false">
                            <div class="row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-4">
                                    <asp:CheckBox ID="chkAutoApprove" runat="server" Text="Automatic Approve" AutoPostBack="true"
                                        meta:resourcekey="chkAutoApproveResource1" />
                                </div>
                            </div>
                            <div id="dvAutoApproveAfter" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:Label ID="lblAutoApproveAfter" runat="server" Text="Automatic Approve After" meta:resourcekey="lblAutoApproveAfterResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadNumericTextBox ID="radnumAutoApproveAfter" MinValue="1" MaxValue="30"
                                            runat="server" Culture="English (United States)" LabelCssClass="" meta:resourcekey="radnumAutoApproveAfterResource1">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="rfvAutoApproveAfter" runat="server" ErrorMessage="Please Insert Automatic Approve After No. Of Day(s)"
                                            ValidationGroup="PermTypesGroup" ControlToValidate="radnumAutoApproveAfter"
                                            Display="None" meta:resourcekey="rfvAutoApproveAfterResource1" Enabled="false"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceAutoApproveAfter" TargetControlID="rfvAutoApproveAfter"
                                            runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="lblDays" runat="server" Text="Day(s)" meta:resourcekey="lblDaysResource1"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:Label ID="lblAutoApprovePolicy" runat="server" Text="Auto Approve Policy" meta:resourcekey="lblAutoApprovePolicyResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:CheckBoxList ID="chkAutoApprovePolicy" runat="server" RepeatDirection="Vertical">
                                            <asp:ListItem Value="1" Text="Direct Manager" meta:resourcekey="ListItemDMResource1" />
                                            <asp:ListItem Value="2" Text="Human Resource" meta:resourcekey="ListItemHRResource1" />
                                            <asp:ListItem Value="3" Text="General Manager" meta:resourcekey="ListItemGMResource1" />
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="trPermissionApproval" runat="server">
                            <div class="col-md-4">
                                <asp:Label ID="lblPermissionApproval" runat="server" CssClass="Profiletitletxt" Text="Permissions Approval"
                                    meta:resourcekey="lblPermissionApprovalResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:RadioButtonList ID="rlstApproval" runat="server" AutoPostBack="true"
                                    meta:resourcekey="rlstApprovalResource1" RepeatDirection="Vertical">
                                    <asp:ListItem Text="Direct Manager Only" Value="1" meta:resourcekey="rdbtnDirectmgrResource1"
                                        Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Human Resource Only" Value="2" meta:resourcekey="rdbtnHROnlyResource1"></asp:ListItem>
                                    <asp:ListItem Text="Both" Value="3" meta:resourcekey="rdbtnBothResource1"></asp:ListItem>
                                    <asp:ListItem Text="DM,HR,GM" Value="4" meta:resourcekey="rdbtnDMHRGMResource1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblPermissionRequestManagerLevelRequired" runat="server" Text="Permission Request Manager Level Required"
                                    meta:resourcekey="lblPermissionRequestManagerLevelRequiredResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="radcmbLevels" Filter="Contains" MarkFirstMatch="true" Skin="Vista"
                                    runat="server">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblTaReason" runat="server" CssClass="Profiletitletxt" Text="Attendance reason"
                                    meta:resourcekey="lblTaReasonResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="cmbBxTaReason" runat="server" MarkFirstMatch="True" Skin="Vista"
                                    meta:resourcekey="cmbBxTaReasonResource1">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="reqcmbBxTaReason" runat="server" ErrorMessage="Please select TA reason"
                                    ValidationGroup="PermTypesGroup" ControlToValidate="cmbBxTaReason" InitialValue="--Please Select--"
                                    Display="None" meta:resourcekey="reqcmbBxTaReasonResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ExtendercmbBxTaReason" TargetControlID="reqcmbBxTaReason"
                                    runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row" style="display: none">
                            <div class="col-md-4">
                                <asp:Label ID="lblPermissionType" runat="server" Text="Permission Type to Allowed Duration"
                                    CssClass="Profiletitletxt" meta:resourcekey="lblPermissionTypeResource1" />
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="RadCmpPermissions" runat="server" AppendDataBoundItems="True"
                                    MarkFirstMatch="True" AutoPostBack="true" CausesValidation="false" Skin="Vista"
                                    meta:resourcekey="ddlLeaveTypeResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblPermissionTypeDuration" runat="server" Text="Duration Allowed if has study permission per month"
                                    CssClass="Profiletitletxt" meta:resourcekey="lblPermissionTypeDuration" />
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtRadLeaveTypeDuration" MinValue="0" MaxValue="999999999"
                                    runat="server" Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtRadMinDurationResource1">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lblMinute3" runat="server" Text="Minutes"
                                    meta:resourcekey="lblMinutesResource1"></asp:Label>
                                <asp:RequiredFieldValidator ID="reqLeaveTypeDuration" runat="server" ErrorMessage="Please enter duration for this leave type"
                                    ValidationGroup="PermTypesGroup" ControlToValidate="txtRadLeaveTypeDuration"
                                    Display="None" meta:resourcekey="reqLeaveTypeDurationResource1" />
                                <cc1:ValidatorCalloutExtender ID="ExtenderLeaveTypeDuration" TargetControlID="reqLeaveTypeDuration"
                                    runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkAllowedAfter" runat="server" AutoPostBack="true" Text="Allowed after specific time" meta:resourcekey="lblAllowedAfterResource1" />
                            </div>
                        </div>
                        <div class="row" id="trAllowedAfterTime" runat="server" visible="false">
                            <div class="col-md-4">
                                <asp:Label ID="lblAllowedAfterTime" runat="server" Text="Allowed After" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblAllowedAfterTimeResource1" />
                            </div>
                            <div class="col-md-4">
                                <telerik:RadMaskedTextBox ID="radAllowedTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    CssClass="RadMaskedTextBox" DisplayMask="##:##" Text='0000' LabelCssClass="">
                                    <ClientEvents OnBlur="ValidateTextboxFrom" />
                                </telerik:RadMaskedTextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="lblhint" runat="server" Text="Hours : Minutes" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblhintResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkAllowedBefore" runat="server" AutoPostBack="true" Text="Allowed Before Specific Time" meta:resourcekey="lblAllowedBeforeResource1" />
                            </div>
                        </div>
                        <div class="row" id="trAllowedBeforeTime" runat="server" visible="false">
                            <div class="col-md-4">
                                <asp:Label ID="lblAllowedBeforeTime" runat="server" Text="Allowed Before" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblAllowedBeforeTimeResource1" />
                            </div>
                            <div class="col-md-4">
                                <telerik:RadMaskedTextBox ID="radAllowedTimeBefore" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    CssClass="RadMaskedTextBox" DisplayMask="##:##" Text='0000' LabelCssClass="">
                                    <ClientEvents OnBlur="ValidateTextboxFrom" />
                                </telerik:RadMaskedTextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="lblhintBefore" runat="server" Text="Hours : Minutes" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblhintResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkCompleteWHRS" runat="server" Text="Should Complete 50% Of Work Hours"
                                    meta:resourcekey="lblCompleteWHRSResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkAllowedManagers" runat="server" Text="Allowed For Managers Only"
                                    meta:resourcekey="lblAllowedManagersResource1" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkAllowedForSelfService" runat="server" Text="Allowed For Self Service"
                                    meta:resourcekey="lblAllowedForSelfServiceResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkConvertToLeave_ExceedDuration" runat="server" Text="Convert Permission To Leave If Exceed Permission Duration"
                                    meta:resourcekey="chkConvertToLeave_ExceedDurationResource1" />
                            </div>
                        </div>
                        <div class="row" id="dvAnnualLeaveId_ToDeductPermission" runat="server" visible="false">
                            <div class="col-md-4">
                                <asp:Label ID="lblAnnualLeaveId_ToDeductPermission" runat="server" Text="Leave Type"
                                    meta:resourcekey="lblAnnualLeaveId_ToDeductPermissionResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="radcmbxAnnualLeaveId" runat="server" MarkFirstMatch="True" Skin="Vista" ExpandDirection="Up">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblAllowedAfterDays" runat="server" Text="Allowed After" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblAllowedAfterDaysResource1" />
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="radtxtAllowedAfterDays" MaxValue="99999" Skin="Vista" MinValue="0"
                                    runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="radtxtAllowedAfterDaysResource1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lbldays3" runat="server" Text="Days" CssClass="Profiletitletxt" meta:resourcekey="lblDaysResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblAllowedBeforeDays" runat="server" Text="Allowed Before" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblAllowedBeforeDaysResource1" />
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="radtxtAllowedBeforeDays" MaxValue="99999" Skin="Vista" MinValue="0"
                                    runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="radtxtAllowedBeforeDaysResource1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lbldays2" runat="server" Text="Days" CssClass="Profiletitletxt" meta:resourcekey="lblDaysResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkExcludeManagers" runat="server" Text="Exclude Managers From Before & After"
                                    meta:resourcekey="lblExcludeManagersResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkAllowForSpecificEmployeeType" runat="server" AutoPostBack="true" Text="Allowed For Specific Employee Type" meta:resourcekey="lblAllowForSpecificEmployeeTypeResource1" />
                            </div>
                        </div>
                        <div class="row" id="trAllowForSpecificEmployeeType" runat="server" visible="false">
                            <div class="col-md-4">
                                <asp:Label ID="lblEmployeeType" runat="server" Text="Employee Type" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblEmployeeTypeResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="RadCmbBxEmployeeType" AllowCustomText="false" MarkFirstMatch="true"
                                    Skin="Vista" runat="server" CausesValidation="false" AutoPostBack="true">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rfvEmployeeType" runat="server" ControlToValidate="RadCmbBxEmployeeType"
                                    InitialValue="--Please Select--" Display="None" ErrorMessage="Please Select Employee Type"
                                    meta:resourcekey="rfvEmployeeTypeResource1" ValidationGroup="grpSave_entity"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvEmployeeType" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkNotAllowedWhenHasStudyorNursing" runat="server" Text="Not Allowed When Has Study or Nursing Permission"
                                    meta:resourcekey="lblNotAllowedWhenHasStudyorNursingResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkShowRemainingBalance" runat="server" Text="Show Remaining Balance"
                                    meta:resourcekey="lblShowRemainingBalanceResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkMustHaveTransaction" runat="server" Text="Employee Must Have In Transaction In Order to Request Permission"
                                    meta:resourcekey="chkMustHaveTransactionResource1" />

                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkValidateDelayPermissions" runat="server" Text="Delay Permission Limitation" AutoPostBack="true"
                                    meta:resourcekey="chkValidateDelayPermissionsResource1" />
                            </div>
                        </div>
                        <div class="row" id="dvDelayPermissionValidation" runat="server" visible="false">
                            <div class="col-md-4">
                                <asp:Label ID="lblDelayPermissionValidation" runat="server" Text="Allowed No. Of Delay Permission Per Month"
                                    meta:resourcekey="lblDelayPermissionValidationResource1"></asp:Label>

                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="radnumDelayPermissionValidation" runat="server" MinValue="0"
                                    Culture="English (United States)" LabelCssClass="">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkPerm_NotificationException" runat="server" Text="Notification Exception"
                                    ToolTip="By Checking This Option Email and SMS Notifications Will Be Disabled For The Defined Type"
                                    meta:resourcekey="chkPerm_NotificationExceptionResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblAllowWhenInSufficient" runat="server" Text="Allow When Balance InSufficient"
                                    CssClass="Profiletitletxt" meta:resourcekey="lblAllowWhenInSufficientResource1" />
                            </div>
                            <div class="col-md-4">
                                <asp:RadioButtonList ID="rblAllowWhenInSufficient" runat="server" RepeatDirection="Vertical"
                                    meta:resourcekey="rblAllowWhenInSufficientResource1">
                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                    </asp:ListItem>
                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                    </asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblHasPermissionTimeControls" runat="server" Text="Has Time Controls Permission"
                                    CssClass="Profiletitletxt" meta:resourcekey="lblHasPermissionTimeControlsResource1" />
                            </div>
                            <div class="col-md-4">
                                <asp:RadioButtonList ID="rblHasPermissionTimeControls" runat="server" RepeatDirection="Vertical"
                                    meta:resourcekey="rblHasPermissionTimeControlsResource1">
                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                    </asp:ListItem>
                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                    </asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblHasFullDayPermission" runat="server" Text="Has Full Day Permission"
                                    CssClass="Profiletitletxt" meta:resourcekey="lblHasFullDayPermissionResource1" />
                            </div>
                            <div class="col-md-4">
                                <asp:RadioButtonList ID="rdlFullDayPermission" runat="server" RepeatDirection="Vertical"
                                    meta:resourcekey="rdlFullDayPermissionResource1">
                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                    </asp:ListItem>
                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                    </asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblHasPermissionForPeriod" runat="server" Text="Has Permission For Period"
                                    CssClass="Profiletitletxt" meta:resourcekey="lblHasPermissionForPeriodResource1" />
                            </div>
                            <div class="col-md-4">
                                <asp:RadioButtonList ID="rdlHasPermissionForPeriod" runat="server" RepeatDirection="Vertical"
                                    meta:resourcekey="rdlHasPermissionForPeriodResource1">
                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                    </asp:ListItem>
                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                    </asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblHasFlexiblePermission" runat="server" Text="Has Flexible Permission"
                                    CssClass="Profiletitletxt" meta:resourcekey="lblHasFlexiblePermissionResource1" />
                            </div>
                            <div class="col-md-4">
                                <asp:RadioButtonList ID="rdlHasFlexiblePermission" runat="server" RepeatDirection="Vertical"
                                    meta:resourcekey="rdlHasFlexiblePermissionResource1">
                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                    </asp:ListItem>
                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                    </asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblConsiderRequestWithinBalance" runat="server" Text="Consider Request Within Balance"
                                    CssClass="Profiletitletxt" meta:resourcekey="lblConsiderRequestWithinBalanceResource1" />
                            </div>
                            <div class="col-md-4">
                                <asp:RadioButtonList ID="rdlConsiderRequestWithinBalance" runat="server" RepeatDirection="Vertical"
                                    meta:resourcekey="rdlConsiderRequestWithinBalanceResource1">
                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                    </asp:ListItem>
                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                    </asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblAttachmentIsMandatory" runat="server" Text="Attachment Is Mandatory"
                                    CssClass="Profiletitletxt" meta:resourcekey="lblAttachmentIsMandatoryResource1" />
                            </div>
                            <div class="col-md-4">
                                <asp:RadioButtonList ID="rdlAttachmentIsMandatory" runat="server" RepeatDirection="Horizontal"
                                    meta:resourcekey="rdlAttachmentIsMandatoryResource1">
                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                    </asp:ListItem>
                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                    </asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblRemarksIsMandatory" runat="server" Text="Remarks Is Mandatory"
                                    CssClass="Profiletitletxt" meta:resourcekey="lblRemarksIsMandatoryResource1" />
                            </div>
                            <div class="col-md-4">
                                <asp:RadioButtonList ID="rdlRemarksIsMandatory" runat="server" RepeatDirection="Horizontal"
                                    meta:resourcekey="rdlRemarksIsMandatoryResource1">
                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                    </asp:ListItem>
                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                    </asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblMinDurationSelfService" runat="server" Text="Minimum Duration Allowed In SelfService"
                                    meta:resourcekey="lblMinDurationSelfServiceResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="radcmbMinDurationSelfService" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="-1" Text="--Please Select--" meta:resourcekey="RadComboBoxItem1Resource1" />
                                        <telerik:RadComboBoxItem Value="10" Text="10 Minutes" meta:resourcekey="RadComboBoxItem2Resource1" />
                                        <telerik:RadComboBoxItem Value="15" Text="15 Minutes" meta:resourcekey="RadComboBoxItem3Resource1" />
                                        <telerik:RadComboBoxItem Value="20" Text="20 Minutes" meta:resourcekey="RadComboBoxItem4Resource1" />
                                        <telerik:RadComboBoxItem Value="30" Text="30 Minutes" meta:resourcekey="RadComboBoxItem5Resource1" />
                                        <telerik:RadComboBoxItem Value="60" Text="1 Hour" meta:resourcekey="RadComboBoxItem6Resource1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblGeneralGuide" runat="server" Text="General Guide" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblGeneralGuideResource1" />
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtGeneralGuide" runat="server" TextMode="MultiLine" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblGeneralGuideAr" runat="server" Text="Arabic General Guide" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblGeneralGuideArResource1" />
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtGeneralGuideAr" runat="server" TextMode="MultiLine" />
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="Tab2" runat="server" HeaderText="Allowed Occurance" meta:resourcekey="Tab2Resource1">
                    <ContentTemplate>

                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblDuration" runat="server" CssClass="Profiletitletxt" Text="Duration"
                                    meta:resourcekey="lblDurationResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="RadCmbBxDuration" AutoPostBack="True" MarkFirstMatch="True"
                                    Skin="Vista" runat="server" meta:resourcekey="RadCmbBxDurationResource1">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rfvDuration" InitialValue="--Please Select--" runat="server"
                                    ControlToValidate="RadCmbBxDuration" Display="None" ErrorMessage="Please Select Duration"
                                    ValidationGroup="AllowedOccurance" meta:resourcekey="rfvDurationResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceDuration" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvDuration" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblMaximumOccur" runat="server" CssClass="Profiletitletxt" Text="Maximum Allowed"
                                    meta:resourcekey="lblMaximumOccurResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtMaximimOccur" runat="server" MinValue="0" MaxValue="9999"
                                    Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtMaximimOccurResource1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="rfvMaximumOccur" runat="server" ControlToValidate="txtMaximimOccur"
                                    Display="None" ErrorMessage="Please enter maximum allowed amount" ValidationGroup="AllowedOccurance"
                                    meta:resourcekey="rfvMaximumOccurResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvMaximumOccur" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSaveAllowedOccurance" runat="server" CssClass="button" Text="Save"
                                    ValidationGroup="AllowedOccurance" meta:resourcekey="btnSaveAllowedOccuranceResource1" />
                                <asp:Button ID="btnDeleteMaximumAllowed" OnClientClick="return AllowedOccuranceValidateDelete();" runat="server" CausesValidation="False"
                                    CssClass="button" Text="Remove" meta:resourcekey="btnDeleteMaximumAllowedResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel2" meta:resourcekey="RadAjaxLoadingPanel2Resource1" />
                                <telerik:RadGrid ID="dgrdMaximumAllowed" runat="server" PageSize="15"
                                    GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True"
                                    meta:resourcekey="dgrdMaximumAllowedResource1">
                                    <SelectedItemStyle ForeColor="Maroon" />
                                    <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="MaximumAllowedId,MaximumOccur,FK_DurationId">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                                UniqueName="TemplateColumn">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="MaximumOccur" SortExpression="MaximumOccur" HeaderText="Maximum Allowed"
                                                UniqueName="MaximumOccur" meta:resourcekey="GridBoundColumnResource1" />
                                            <telerik:GridBoundColumn DataField="MaximumAllowedId" SortExpression="MaximumAllowedId"
                                                Visible="False" UniqueName="MaximumAllowedId" meta:resourcekey="GridBoundColumnResource2" />
                                            <telerik:GridBoundColumn DataField="FK_PermId" SortExpression="FK_PermId" Visible="False"
                                                UniqueName="FK_PermId" meta:resourcekey="GridBoundColumnResource3" />
                                            <telerik:GridBoundColumn DataField="FK_DurationId" SortExpression="FK_DurationId"
                                                Visible="False" UniqueName="FK_DurationId" meta:resourcekey="GridBoundColumnResource4" />
                                            <telerik:GridBoundColumn DataField="DurationTypeName" SortExpression="DurationTypeName"
                                                HeaderText="Duration Type" UniqueName="DurationType" meta:resourcekey="GridBoundColumnResource5" />
                                        </Columns>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                    </MasterTableView>
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                        EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="Tab3" runat="server" HeaderText="Allowed Duration" meta:resourcekey="Tab3Resource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblAllowedDuration" runat="server" CssClass="Profiletitletxt" Text="Duration"
                                    meta:resourcekey="lblAllowedDurationResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="RadCmbBxAllowedDuration" AutoPostBack="True" MarkFirstMatch="True"
                                    Skin="Vista" runat="server" meta:resourcekey="RadCmbBxAllowedDurationResource1">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rfvAllowedDuration" InitialValue="--Please Select--"
                                    runat="server" ControlToValidate="RadCmbBxAllowedDuration" Display="None" ErrorMessage="Please Select Duration"
                                    ValidationGroup="AllowedDuration" meta:resourcekey="rfvAllowedDurationResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvAllowedDuration" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblMaximumDuration" runat="server" CssClass="Profiletitletxt" Text="Maximum Duration"
                                    meta:resourcekey="lblMaximumDurationResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtMaximumDuration" runat="server" MinValue="0" MaxValue="9999"
                                    Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtMaximumDurationResource1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="rfvMaximumDuration" runat="server" ControlToValidate="txtMaximumDuration"
                                    Display="None" ErrorMessage="Please enter maximum duration amount" ValidationGroup="AllowedDuration"
                                    meta:resourcekey="rfvMaximumDurationResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvMaximumDuration" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lblMinutes4" runat="server" CssClass="Profiletitletxt" Text="Minutes"
                                    meta:resourcekey="lblMinutesResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblMaximumRamadanDuration" runat="server" CssClass="Profiletitletxt" Text="Maximum Duration In Ramadan"
                                    meta:resourcekey="lblMaximumRamadanDurationResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtMaximumRamadanDuration" runat="server" MinValue="0" MaxValue="9999"
                                    Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtMaximumRamadanDurationResource1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="rfvMaximumRamadanDuration" runat="server" ControlToValidate="txtMaximumRamadanDuration"
                                    Display="None" ErrorMessage="Please enter maximum duration in Ramadan" ValidationGroup="AllowedDuration"
                                    meta:resourcekey="rfvMaximumRamadanDurationResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceMaximumRamadanDuration" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvMaximumRamadanDuration" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text="Minutes"
                                    meta:resourcekey="lblMinutesResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblMaximumDuration_WithStudyNursing" runat="server" CssClass="Profiletitletxt"
                                    Text="Maximum Duration With Nursing Or Study Permission"
                                    meta:resourcekey="lblMaximumDuration_WithStudyNursingResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtMaximumDuration_WithStudyNursing" runat="server" MinValue="0" MaxValue="9999"
                                    Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtMaximumDuration_WithStudyNursingResource1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="Label3" runat="server" CssClass="Profiletitletxt" Text="Minutes"
                                    meta:resourcekey="lblMinutesResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSaveAllowedDuration" runat="server" CssClass="button" Text="Save"
                                    ValidationGroup="AllowedDuration" meta:resourcekey="btnSaveAllowedDurationResource1" />
                                <asp:Button ID="btnRemoveAllowedDuration" OnClientClick="return AllowedDurationValidateDelete()" runat="server" CausesValidation="False"
                                    CssClass="button" Text="Remove" meta:resourcekey="btnRemoveAllowedDurationResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                                <telerik:RadGrid ID="dgrdAllowedDuration" runat="server" PageSize="15"
                                    GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True"
                                    meta:resourcekey="dgrdAllowedDurationResource1">
                                    <SelectedItemStyle ForeColor="Maroon" />
                                    <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="MaximumDurationId,MaximumDuration,FK_DurationId,MaximumRamadanDuration,MaximumDuration_WithStudyNursing">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource2"
                                                UniqueName="TemplateColumn">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="MaximumDuration" SortExpression="MaximumDuration"
                                                HeaderText="Maximum Duration" UniqueName="MaximumDuration" meta:resourcekey="GridBoundColumnResource6" />
                                            <telerik:GridBoundColumn DataField="MaximumRamadanDuration" SortExpression="MaximumRamadanDuration"
                                                HeaderText="Maximum Ramadan Duration" UniqueName="MaximumRamadanDuration" meta:resourcekey="GridBoundColumnResource17" />
                                            <telerik:GridBoundColumn DataField="MaximumDurationId" SortExpression="MaximumDurationId"
                                                Visible="False" UniqueName="MaximumDurationId" meta:resourcekey="GridBoundColumnResource7" />
                                            <telerik:GridBoundColumn DataField="FK_PermId" SortExpression="FK_PermId" Visible="False"
                                                UniqueName="FK_PermId" meta:resourcekey="GridBoundColumnResource8" />
                                            <telerik:GridBoundColumn DataField="FK_DurationId" SortExpression="FK_DurationId"
                                                Visible="False" UniqueName="FK_DurationId" meta:resourcekey="GridBoundColumnResource9" />
                                            <telerik:GridBoundColumn DataField="DurationTypeName" SortExpression="DurationTypeName"
                                                HeaderText="Duration Type" UniqueName="DurationTypeName" meta:resourcekey="GridBoundColumnResource10" />
                                        </Columns>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                    </MasterTableView>
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                        EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="Tab4" runat="server" HeaderText="Permission Units" meta:resourcekey="Tab4Resource1">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblPermissionUnits" runat="server" AutoPostBack="true">
                            <asp:ListItem Value="0" Text="All" Selected="True" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Specific Company" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Specific Entity" meta:resourcekey="ListItem3Resource1"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div class="row" id="divCompanies" runat="server" visible="False">
                            <div class="col-md-2">
                                <asp:Label ID="lblCompanies" runat="server" CssClass="Profiletitletxt" Text="List Of Companies"
                                    meta:resourcekey="lbllblCompaniesResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; margin-top: 5px; border-radius: 5px">
                                    <asp:CheckBoxList ID="cblCompanies" runat="server" Style="height: 26px" DataTextField="CompanyName"
                                        DataValueField="CompanyId" meta:resourcekey="cblCompaniesListResource1">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <%--<div class="row" id="divCompaniesSelect" runat="server" visible="False">--%>
                            <div id="divCompaniesSelect" runat="server" visible="False">
                                <%-- <div class="col-md-2">
                            </div>--%>
                                <div class="col-md-2">
                                    <a href="javascript:void(0)" onclick="CheckBoxListSelectCompany(true)" style="font-size: 8pt">
                                        <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                                    <a href="javascript:void(0)" onclick="CheckBoxListSelectCompany(false)" style="font-size: 8pt">
                                        <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:HyperLink ID="HyperLink2" runat="server" Visible="False" meta:resourcekey="hlViewEntityResource1"
                                    Text="View Companies "></asp:HyperLink>
                            </div>
                        </div>
                        <%--<div class="row" id="divEntities" runat="server" visible="False">--%>
                        <div id="divEntities" runat="server" visible="False">
                            <div class="col-md-2">
                                <asp:Label ID="lblEntities" runat="server" CssClass="Profiletitletxt" Text="List Of Entities"
                                    meta:resourcekey="lblEntitiesResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; margin-top: 5px; border-radius: 5px">
                                    <asp:CheckBoxList ID="cblEntities" runat="server" Style="height: 26px" DataTextField="EntityName"
                                        DataValueField="EntityId" meta:resourcekey="cblEntityListResource1">
                                    </asp:CheckBoxList>
                                </div>
                            </div>

                            <div class="row" id="divEntitiesSelect" runat="server" visible="False">

                                <div class="col-md-2">
                                    <a href="javascript:void(0)" onclick="CheckBoxListSelectEntity(true)" style="font-size: 8pt">
                                        <asp:Literal ID="Literal3" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                                    <%--</div>
                            <div class="col-md-2">--%>
                                    <a href="javascript:void(0)" onclick="CheckBoxListSelectEntity(false)" style="font-size: 8pt">
                                        <asp:Literal ID="Literal4" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:HyperLink ID="HyperLink1" runat="server" Visible="False" meta:resourcekey="hlViewEntityResource1"
                                    Text="View Entities "></asp:HyperLink>
                            </div>
                        </div>
                    </ContentTemplate>

                </cc1:TabPanel>
            </cc1:TabContainer>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="PermTypesGroup"
                        meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnDelete" OnClientClick="return ValidateDelete();" runat="server" CssClass="button" Text="Delete" CausesValidation="False"
                        meta:resourcekey="btnDeleteResource1" />
                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" CausesValidation="False"
                        meta:resourcekey="btnClearResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdVwPermissionTypes"
                        Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadGrid ID="dgrdVwPermissionTypes" runat="server" AllowSorting="True" AllowPaging="True"
                        PageSize="25" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        ShowFooter="True" OnItemCommand="dgrdVwPermissionTypes_ItemCommand" meta:resourcekey="dgrdVwPermissionTypesResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="PermId,PermName">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource3">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="PermName" SortExpression="PermName" HeaderText="English Name"
                                    UniqueName="PermName" meta:resourcekey="GridBoundColumnResource11" />
                                <telerik:GridBoundColumn DataField="PermArabicName" SortExpression="PermArabicName"
                                    HeaderText="Arabic Name" UniqueName="PermArabicName" meta:resourcekey="GridBoundColumnResource12" />
                                <telerik:GridBoundColumn DataField="MinDuration" SortExpression="MinDuration" HeaderText="Minimum Duration"
                                    UniqueName="MinDuration" meta:resourcekey="GridBoundColumnResource13" />
                                <telerik:GridBoundColumn DataField="MaxDuration" SortExpression="MaxDuration" HeaderText="Maximum Duration"
                                    UniqueName="MaxDuration" meta:resourcekey="GridBoundColumnResource14" />
                                <telerik:GridBoundColumn DataField="MonthlyBalance" SortExpression="MonthlyBalance"
                                    HeaderText="Monthly Balance" UniqueName="MonthlyBalance" meta:resourcekey="GridBoundColumnResource15" />
                                <telerik:GridBoundColumn DataField="PermId" SortExpression="PermId" Visible="False"
                                    AllowFiltering="False" UniqueName="PermId" meta:resourcekey="GridBoundColumnResource16" />
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
                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdVwPermissionTypes.ClientID %>");
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


        function AllowedOccuranceValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdMaximumAllowed.ClientID %>");
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

        function AllowedDurationValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdAllowedDuration.ClientID %>");
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
