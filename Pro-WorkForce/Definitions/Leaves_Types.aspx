<%@ Page Title="Define Type of Leaves" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="Leaves_Types.aspx.vb" Inherits="Emp_Leaves_Types"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
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
        function CheckBoxListSelect(state) {
            var chkBoxList = document.getElementById("<%= cblGradeList.ClientID %>");
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }

        function CheckBoxGradeListSelect(state) {
            var chkBoxList = document.getElementById("<%= cblBalanceGradeList.ClientID%>");
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }

        function ResetPopUpDate() {
            var dtpdpEffectiveDate = document.getElementById("<%=dpEffectiveDate.ClientID%>");
            dpEffectiveDate.value = '';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlLeavesTypes" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" HeaderText="Leave Types" runat="server" />
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
                                <asp:RequiredFieldValidator ID="reqName" runat="server" ControlToValidate="txtName"
                                    Display="None" ErrorMessage="Please enter leave english name" ValidationGroup="LeavesGroups"
                                    meta:resourcekey="reqNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ExtenderName" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="reqName" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblArabicName" runat="server" CssClass="Profiletitletxt" Text="Arabic name"
                                    meta:resourcekey="lblArabicNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtArabicName" runat="server" meta:resourcekey="txtArabicNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqArabicName" runat="server" ControlToValidate="txtArabicName"
                                    Display="None" ErrorMessage="Please enter leave arabic name" ValidationGroup="LeavesGroups"
                                    meta:resourcekey="reqArabicNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ExtenderreqArabicName" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="reqArabicName" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblLeaveCode" runat="server" Text="Leave Code" meta:resourcekey="lblLeaveCodeResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtLeaveCode" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblConsiderBalance" runat="server" Text="Consider Balance By" meta:resourcekey="lblConsiderBalanceResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:RadioButtonList ID="rblConsiderBalance" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                    <asp:ListItem Value="0" Text="Leave Type" Selected="True" meta:resourcekey="ListItem1ConsiderBalanceResource1"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Grade" meta:resourcekey="ListItem2ConsiderBalanceResource1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div id="dvBalance" runat="server" visible="true">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblBalance" runat="server" CssClass="Profiletitletxt" Text="Balance (days)"
                                        meta:resourcekey="lblBalanceResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadNumericTextBox ID="txtRadBalance" runat="server" MinValue="0" MaxValue="365"
                                        Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtRadBalanceResource1">
                                        <%--<NumberFormat DecimalDigits="2" GroupSeparator="" />--%>
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="reqRadBalance" runat="server" ControlToValidate="txtRadBalance"
                                        Display="None" ErrorMessage="Please enter balance" ValidationGroup="LeavesGroups"
                                        meta:resourcekey="reqRadBalanceResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderreqRadBalance" runat="server" CssClass="AISCustomCalloutStyle"
                                        TargetControlID="reqRadBalance" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <asp:RadioButtonList ID="rdbMonthlyBalance" runat="server" RepeatDirection="Horizontal"
                                    meta:resourcekey="rdbMonthlyBalanceResource1">
                                    <asp:ListItem Text="Monthly " Value="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                    <asp:ListItem Text="Yearly" Value="False" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="ReqrdbMonthlyBalance" runat="server" ControlToValidate="rdbMonthlyBalance"
                                    Display="None" ErrorMessage="Please enter blanace duration" ValidationGroup="LeavesGroups"
                                    meta:resourcekey="ReqrdbMonthlyBalanceResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="ReqrdbMonthlyBalance" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblMinDuration" runat="server" CssClass="Profiletitletxt" Text="Minimum duration"
                                    meta:resourcekey="lblMinDurationResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtRadMinDuration" runat="server" MinValue="0" MaxValue="9999"
                                    Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtRadMinDurationResource1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lbldays" runat="server" Text="Days" meta:resourcekey="lbldaysResource1"></asp:Label>
                                <asp:CompareValidator ID="cvBalanceMinDuration" runat="server" ControlToCompare="txtRadBalance"
                                    ControlToValidate="txtRadMinDuration" ErrorMessage="Minimum Duration Should be Less Than or Equal To Balance"
                                    Display="None" Operator="LessThanEqual" Type="Integer" ValidationGroup="LeavesGroups"
                                    meta:resourcekey="cvBalanceMinDurationResource1" />
                                <cc1:ValidatorCalloutExtender TargetControlID="cvBalanceMinDuration" ID="ValidatorCalloutExtender8"
                                    runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblMaxDuration" runat="server" CssClass="Profiletitletxt" Text="Maximum duration"
                                    meta:resourcekey="lblMaxDurationResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtRadMaxDuration" runat="server" MinValue="1" MaxValue="9999"
                                    Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtRadMaxDurationResource1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lbldays2" runat="server" Text="Days" meta:resourcekey="lbldays2Resource1"></asp:Label>
                                <asp:CompareValidator ID="cvDuration" runat="server" ControlToCompare="txtRadMinDuration"
                                    ControlToValidate="txtRadMaxDuration" ErrorMessage="Maximum Duration Should be Greater Than or Equal To Minimum Duration"
                                    Display="None" Operator="GreaterThanEqual" Type="Integer" ValidationGroup="LeavesGroups"
                                    meta:resourcekey="cvDurationResource1" />
                                <cc1:ValidatorCalloutExtender TargetControlID="cvDuration" ID="vcDuration" runat="server"
                                    Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png" />
                                <%-- <asp:CompareValidator ID="cvBalanceMaxDuration" runat="server" ControlToCompare="txtRadBalance"
                                                        ControlToValidate="txtRadMaxDuration" ErrorMessage="Maximum Duration Should be Less Than or Equal To Balance"
                                                        Display="None" Operator="LessThanEqual" Type="Integer" ValidationGroup="LeavesGroups"
                                                        meta:resourcekey="cvBalanceMaxDurationResource1" />
                                                    <cc1:ValidatorCalloutExtender TargetControlID="cvBalanceMaxDuration" ID="ValidatorCalloutExtender7"
                                                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png" />--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblAllowAfter" runat="server" CssClass="Profiletitletxt" Text="Allow after"
                                    meta:resourcekey="lblAllowAfterResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtRadMinServiceDay" runat="server" MaxValue="9999"
                                    MinValue="0" Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtRadMinServiceDayResource1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="LabelservDays" runat="server" Text="service days" meta:resourcekey="LabelservDaysResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblExcludeOffsDay" runat="server" CssClass="Profiletitletxt" Text="Exclude off days"
                                    meta:resourcekey="lblExcludeOffsDayResource1"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:RadioButtonList ID="rdbExcludeOffsDay" runat="server" RepeatDirection="Horizontal"
                                    meta:resourcekey="rdbExcludeOffsDayResource1">
                                    <asp:ListItem Text="Include off days" Value="False" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                    <asp:ListItem Text="Exclude off days" Value="True" meta:resourcekey="ListItemResource4"
                                        Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="reqrdbExcludeOffsDay" runat="server" ControlToValidate="rdbExcludeOffsDay"
                                    Display="None" ErrorMessage="Please choose an Exclude offs days" ValidationGroup="LeavesGroups"
                                    meta:resourcekey="reqrdbExcludeOffsDayResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="reqrdbExcludeOffsDay" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-8">
                                <asp:RadioButtonList ID="rdbExcludeHolidays" runat="server" RepeatDirection="Horizontal"
                                    meta:resourcekey="rdbExcludeHolidaysResource1">
                                    <asp:ListItem Text="Include Holidays" Value="False" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                    <asp:ListItem Text="Exclude Holidays" Value="True" meta:resourcekey="ListItemResource6"
                                        Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblMaxRoundBalance" runat="server" CssClass="Profiletitletxt" Text="Maximum round balance"
                                    meta:resourcekey="lblMaxRoundBalanceResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtRadMaxRoundBalance" runat="server" MinValue="0"
                                    MaxValue="99" Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtRadMaxRoundBalanceResource1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblPaymentConsideration" runat="server" CssClass="Profiletitletxt"
                                    Text="Payment consideration" meta:resourcekey="lblPaymentConsiderationResource1"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:RadioButtonList ID="rdbPaymentConsideration" runat="server" RepeatDirection="Horizontal"
                                    meta:resourcekey="rdbPaymentConsiderationResource1">
                                    <asp:ListItem Text="Full pay value " Value="1" meta:resourcekey="ListItemResource7"
                                        Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Half pay value " Value="2" meta:resourcekey="ListItemResource8"></asp:ListItem>
                                    <asp:ListItem Text="No pay value " Value="3" meta:resourcekey="ListItemResource9"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="reqrdbPaymentConsideration" runat="server" ControlToValidate="rdbPaymentConsideration"
                                    Display="None" ErrorMessage="Please choose a Payment consideration" ValidationGroup="LeavesGroups"
                                    meta:resourcekey="reqrdbPaymentConsiderationResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="reqrdbPaymentConsideration" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <%-- <tr>
                                                <td>
                                                    <asp:Label ID="lblExpiredBalanceIsCached" runat="server" CssClass="Profiletitletxt"
                                                        Text="Expired balance to be cache" meta:resourcekey="lblExpiredBalanceIsCachedResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdbExpiredBalanceIsCached" runat="server" RepeatDirection="Horizontal"
                                                        meta:resourcekey="rdbExpiredBalanceIsCachedResource1">
                                                        <asp:ListItem Text="Yes" Value="True" meta:resourcekey="ListItemResource10"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="False" meta:resourcekey="ListItemResource11"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>--%>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblAllowBalanceIsOver" runat="server" CssClass="Profiletitletxt" Text="Allow when balance not sufficient"
                                    meta:resourcekey="lblAllowBalanceIsOverResource1"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:RadioButtonList ID="rdbAllowBalanceIsOver" runat="server" RepeatDirection="Horizontal"
                                    meta:resourcekey="rdbAllowBalanceIsOverResource1">
                                    <asp:ListItem Text="Yes" Value="True" meta:resourcekey="ListItemResource12" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="False" meta:resourcekey="ListItemResource13"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="reqrdbAllowBalanceIsOver" runat="server" Display="None"
                                    ErrorMessage="Please choose If allowed balance not sufficient" ValidationGroup="LeavesGroups"
                                    meta:resourcekey="reqrdlblAllowBalanceIsOverResource1" ControlToValidate="rdbAllowBalanceIsOver"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="reqrdbAllowBalanceIsOver" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblIsAnnual" runat="server" CssClass="Profiletitletxt" Text="Annual leave "
                                    meta:resourcekey="lblIsAnnualResource1"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:RadioButtonList ID="rdbIsAnnual" runat="server" RepeatDirection="Horizontal"
                                    meta:resourcekey="rdbIsAnnualResource1">
                                    <asp:ListItem Text="Yes" Value="True" meta:resourcekey="ListItemResource14" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="False" meta:resourcekey="ListItemResource15"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="reqrdbIsAnnual" runat="server" ControlToValidate="rdbIsAnnual"
                                    Display="None" ErrorMessage="Please choose an Annual leave" ValidationGroup="LeavesGroups"
                                    meta:resourcekey="reqrdbIsAnnualResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="reqrdbIsAnnual" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
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
                                            ValidationGroup="LeavesGroups" ControlToValidate="radnumAutoApproveAfter"
                                            Display="None" meta:resourcekey="rfvAutoApproveAfterResource1" Enabled="false"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceAutoApproveAfter" TargetControlID="rfvAutoApproveAfter"
                                            runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="Label2" runat="server" Text="Day(s)" meta:resourcekey="lblDaysResource1"></asp:Label>
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
                        <div class="row" id="trLeaveApproval" runat="server">
                            <div class="col-md-4">
                                <asp:Label ID="lblLeaveApproval" runat="server" CssClass="Profiletitletxt" Text="Leaves and Permissions Approval"
                                    meta:resourcekey="lblLeaveApprovalResource1"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:RadioButtonList ID="rlstApproval" runat="server" AutoPostBack="true" meta:resourcekey="rlstApprovalResource1">
                                    <asp:ListItem Text="Direct Manager Only" Value="1" meta:resourcekey="rdbtnDirectmgrResource1"
                                        Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Human Resource Only" Value="2" meta:resourcekey="rdbtnHROnlyResource1"></asp:ListItem>
                                    <asp:ListItem Text="1st and 2nd Manager" Value="3" meta:resourcekey="rdbtnBothResource1"></asp:ListItem>
                                    <%--<asp:ListItem Text="DM,HR,GM" Value="4" meta:resourcekey="rdbtnDMHRGMResource1"></asp:ListItem>--%>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblLeaveRequestManagerLevelRequired" runat="server" Text="Leave Request Manager Level Required"
                                    meta:resourcekey="lblLeaveRequestManagerLevelRequiredResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="radcmbLevels" Filter="Contains" MarkFirstMatch="true" Skin="Vista"
                                    runat="server">
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
                                <asp:Label ID="Label1" runat="server" Text="Days" CssClass="Profiletitletxt" meta:resourcekey="lblDaysResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblMinLeaveApplyDay" runat="server" Text="Minimum Number of Days To Apply For Leave Request"
                                    meta:resourcekey="lblMinLeaveApplyDayResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="radtxtMinLeaveApplyDay" MaxValue="365" Skin="Vista" MinValue="0"
                                    runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="radtxtMinLeaveApplyDayResource1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lblMinLeaveApplyDayDays" runat="server" Text="Days" CssClass="Profiletitletxt" meta:resourcekey="lblDaysResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblAllowedGender" runat="server" Text="Allowed Gender" meta:resourcekey="lblAllowedGenderResource1"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:RadioButtonList ID="rblAllowedGender" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Text="All" Selected="True" meta:resourcekey="ListItemAllResource1"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Males Only" meta:resourcekey="ListItemMaleResource1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Females Only" meta:resourcekey="ListItemFemaleResource1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkAllowedForSelfService" Text="Allowed For Self Service" meta:resourcekey="lblAllowedForSelfServiceResource1"
                                    runat="server" />
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
                                    meta:resourcekey="rfvEmployeeTypeResource1" ValidationGroup="LeavesGroups"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvEmployeeType" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox Text="Show Remaining Balance In Self Service" ID="chkShowRemainingBalance"
                                    meta:resourcekey="chkShowRemainingBalanceResource1" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkLeave_NotificationException" runat="server" Text="Notification Exception"
                                    ToolTip="By Checking This Option Email and SMS Notifications Will Be Disabled For The Defined Type"
                                    meta:resourcekey="chkLeave_NotificationExceptionResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkValidateLeavesBeforeRestDays" runat="server" Text="Validate Leaves Before Rest Days"
                                    ToolTip="By Checking This Option System Will Prevent Users To Apply For Leaves Directly After Rest Day If a Leave Ouccered Directly Before Rest Day"
                                    meta:resourcekey="chkValidateLeavesBeforeRestDaysResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblAttachmentIsMandatory" runat="server" Text="Attachment Is Mandatory"
                                    meta:resourcekey="lblAttachmentIsMandatoryResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:RadioButtonList ID="rblAttachmentIsMandatory" runat="server">
                                    <asp:ListItem Value="0" Text="No" Selected="True" meta:resourcekey="ListItemNoResource1"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Yes" meta:resourcekey="ListItemYesResource1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblRemarksIsMandatory" runat="server" Text="Remarks Is Mandatory"
                                    meta:resourcekey="lblRemarksIsMandatoryResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:RadioButtonList ID="rblRemarksIsMandatory" runat="server">
                                    <asp:ListItem Value="0" Text="No" Selected="True" meta:resourcekey="ListItemNoResource1"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Yes" meta:resourcekey="ListItemYesResource1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblGeneralGuide" runat="server" CssClass="Profiletitletxt" Text="General Guide"
                                    meta:resourcekey="lblGeneralGuideResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <%--<FTB:FreeTextBox ID="txtGeneralGuide" runat="server" Width="125px" Height="20px"
                                                        EnableToolbars="false" BackColor="White" EnableHtmlMode="false" />--%>
                                <asp:TextBox ID="txtGeneralGuide" runat="server" TextMode="MultiLine" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblGeneralGuideAr" runat="server" CssClass="Profiletitletxt" Text="Arabic General Guide"
                                    meta:resourcekey="lblGeneralGuideArResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <%-- <FTB:FreeTextBox ID="txtGeneralGuideAr" runat="server" Width="125px" Height="20px"
                                                        EnableToolbars="false" BackColor="White" EnableHtmlMode="false" />--%>
                                <asp:TextBox ID="txtGeneralGuideAr" runat="server" TextMode="MultiLine" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblParentLeaveTypeId" runat="server" Text="Parent Leave Type" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblParentLeaveTypeIdResource1" />
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="ddlLeaveType" runat="server" AppendDataBoundItems="True"
                                    MarkFirstMatch="True" CausesValidation="false" Skin="Vista"
                                    meta:resourcekey="ddlLeaveTypeResource1" />
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="Tab2" runat="server" HeaderText="Allowed Occurance" meta:resourcekey="Tab2Resource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-4">
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
                            <div class="col-md-4">
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
                                <asp:Button ID="btnDeleteMaximumAllowed" OnClientClick="return ValidateDeleteMaximumAllowed()"
                                    runat="server" CausesValidation="False" CssClass="button" Text="Remove" meta:resourcekey="btnDeleteMaximumAllowedResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel2" meta:resourcekey="RadAjaxLoadingPanel2Resource1" />
                                <telerik:RadGrid ID="dgrdMaximumAllowed" runat="server" PageSize="15" Skin="Hay"
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
                                            <telerik:GridBoundColumn DataField="FK_LeaveId" SortExpression="FK_LeaveId" Visible="False"
                                                UniqueName="FK_LeaveId" meta:resourcekey="GridBoundColumnResource3" />
                                            <telerik:GridBoundColumn DataField="FK_DurationId" SortExpression="FK_DurationId"
                                                Visible="False" UniqueName="FK_DurationId" meta:resourcekey="GridBoundColumnResource4" />
                                            <telerik:GridBoundColumn DataField="DurationTypeName" SortExpression="DurationTypeName"
                                                HeaderText="Duration Type" UniqueName="DurationTypeName" meta:resourcekey="GridBoundColumnResource5" />
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
                <cc1:TabPanel ID="Tab3" runat="server" HeaderText="Leave Grades" meta:resourcekey="Tab3Resource1">
                    <ContentTemplate>
                        <div class="col-md-12 text-center">
                            <asp:RadioButtonList ID="rblGrades" runat="server" AutoPostBack="true">
                                <asp:ListItem Value="0" Text="All Grades" Selected="True" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Specific Grade" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="row" id="dvGradeList" runat="server" visible="false">
                            <div class="col-md-4">
                                <asp:Label ID="lblListGrades" runat="server" CssClass="Profiletitletxt" Text="List Of Grades"
                                    meta:resourcekey="lblListGradesResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; border-radius: 5px;">
                                    <asp:CheckBoxList ID="cblGradeList" runat="server" Style="height: 26px" DataTextField="GradeName"
                                        DataValueField="GradeId" meta:resourcekey="cblGradeList">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div>
                                    <a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size: 8pt">
                                        <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                                </div>
                                <a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size: 8pt">
                                    <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="Tab4" runat="server" HeaderText="Leave Grades & Balance" meta:resourcekey="Tab4Resource1" Visible="false">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblListBalanceGrade" runat="server" CssClass="Profiletitletxt" Text="List Of Grades"
                                    meta:resourcekey="lblListGradesResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; border-radius: 5px;">
                                    <asp:CheckBoxList ID="cblBalanceGradeList" runat="server" Style="height: 26px" DataTextField="GradeName"
                                        DataValueField="GradeId" meta:resourcekey="cblGradeList">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div>
                                    <a href="javascript:void(0)" onclick="CheckBoxGradeListSelect(true)" style="font-size: 8pt">
                                        <asp:Literal ID="Literal3" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                                </div>
                                <a href="javascript:void(0)" onclick="CheckBoxGradeListSelect(false)" style="font-size: 8pt">
                                    <asp:Literal ID="Literal4" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                            </div>
                        </div>

                        <div id="dvGradeBalance" runat="server" visible="true">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblGradeBalance" runat="server" CssClass="Profiletitletxt" Text="Grade Balance (days)"
                                        meta:resourcekey="lblGradeBalanceResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadNumericTextBox ID="txtRadGradeBalance" runat="server" MinValue="0" MaxValue="365"
                                        Culture="English (United States)" LabelCssClass="">
                                        <%--<NumberFormat DecimalDigits="2" GroupSeparator="" />--%>
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="rfvRadGradeBalance" runat="server" ControlToValidate="txtRadGradeBalance" Enabled="false"
                                        Display="None" ErrorMessage="Please Enter Grade Balance" ValidationGroup="grpAddGradeBalance"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" CssClass="AISCustomCalloutStyle"
                                        TargetControlID="rfvRadGradeBalance" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnAddGradeBalance" runat="server" Text="Add" ValidationGroup="grpAddGradeBalance"
                                    meta:resourcekey="btnAddGradeBalanceResource1" />
                                <asp:Button ID="btnRemoveGradeBalance" runat="server" Text="Remove"
                                    meta:resourcekey="btnRemoveGradeBalanceResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel3" />
                                <div class="filterDiv">
                                    <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter2" FilterContainerID="dgrdGradeBalance"
                                        ShowApplyButton="False" meta:resourcekey="RadFilter2Resource1" />
                                </div>
                                <telerik:RadGrid ID="dgrdGradeBalance" runat="server" AllowSorting="True" AllowPaging="True"
                                    PageSize="15" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                    ShowFooter="True">
                                    <SelectedItemStyle ForeColor="Maroon" />
                                    <MasterTableView CommandItemDisplay="Top" AllowMultiColumnSorting="True" AutoGenerateColumns="False"
                                        DataKeyNames="MaximumGradeBalanceId,FK_LeaveId,FK_GradeId">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_LeaveId" HeaderText="LeaveId"
                                                SortExpression="FK_LeaveId" Visible="False" UniqueName="FK_LeaveId">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FK_GradeId" HeaderText="FK_GradeId"
                                                Resizable="False" UniqueName="FK_GradeId" Visible="false" AllowFiltering="false" AllowSorting="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GradeName" HeaderText="Grade Name" SortExpression="GradeName"
                                                Resizable="False" UniqueName="GradeName" meta:resourcekey="GridCheckBoxColumnResource10">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GradeBalance" HeaderText="Grade Balance"
                                                SortExpression="GradeBalance" Resizable="False" DataType="System.Decimal" DataFormatString="{0:N2}"
                                                UniqueName="Balance" meta:resourcekey="GridCheckBoxColumnResource11">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar Skin="Hay" runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar2_ButtonClick">
                                                <Items>
                                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid1" ImageUrl="~/images/RadFilter.gif"
                                                        ImagePosition="Right" runat="server" meta:resourcekey="RadToolBar2ButtonResource1" />
                                                </Items>
                                            </telerik:RadToolBar>
                                        </CommandItemTemplate>
                                    </MasterTableView>
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="false"
                                        EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="false" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="LeavesGroups"
                        meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnDelete" OnClientClick="return ValidateDelete();" runat="server"
                        CausesValidation="False" CssClass="button" Text="Delete" meta:resourcekey="btnDeleteResource1" />
                    <asp:Button ID="btnClear" runat="server" CausesValidation="False" CssClass="button"
                        Text="Clear" meta:resourcekey="btnClearResource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                    <div class="filterDiv">
                        <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdVwLeaveTypes"
                            ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    </div>
                    <telerik:RadGrid ID="dgrdVwLeaveTypes" runat="server" AllowSorting="True" AllowPaging="True"
                        PageSize="15" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        ShowFooter="True" meta:resourcekey="dgrdVwLeaveTypesResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView CommandItemDisplay="Top" AllowMultiColumnSorting="True" AutoGenerateColumns="False"
                            DataKeyNames="LeaveName,LeaveId">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="LeaveId" HeaderText="LeaveId"
                                    SortExpression="LeaveId" Visible="False" UniqueName="LeaveId" meta:resourcekey="GridBoundColumnResource6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="LeaveName" HeaderText="Leave Name" SortExpression="LeaveName"
                                    Resizable="False" UniqueName="LeaveName" meta:resourcekey="GridBoundColumnResource7">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="LeaveArabicName" HeaderText="LeaveArabicName"
                                    SortExpression="LeaveArabicName" UniqueName="LeaveArabicName" meta:resourcekey="GridBoundColumnResource8" />
                                <telerik:GridBoundColumn DataField="Balance" HeaderText="Balance" AllowFiltering="False"
                                    SortExpression="Balance" Resizable="False" DataType="System.Decimal" DataFormatString="{0:N2}"
                                    UniqueName="Balance" meta:resourcekey="GridBoundColumnResource9">
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="IsAnnual" HeaderText="IsAnnual" SortExpression="IsAnnual"
                                    Resizable="False" UniqueName="IsAnnual" DataType="System.String" meta:resourcekey="GridCheckBoxColumnResource1"
                                    ItemStyle-CssClass="nocheckboxstyle">
                                </telerik:GridCheckBoxColumn>

                                <telerik:GridBoundColumn DataField="BalanceConsideration" AllowFiltering="False"
                                    Resizable="False" DataType="System.string"
                                    UniqueName="BalanceConsideration" Display="false">
                                </telerik:GridBoundColumn>

                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar Skin="Hay" runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                            ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                            EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
            <asp:HiddenField runat="server" ID="hdnWindow" />
            <cc1:ModalPopupExtender ID="mpeEffectiveDatePopup" runat="server" BehaviorID="modelEffectiveDatePopup"
                TargetControlID="hdnWindow" DropShadow="True" PopupControlID="divLeaveEffectiveDate"
                Enabled="true"
                BackgroundCssClass="ModalBackground" DynamicServicePath="" />

            <div id="divLeaveEffectiveDate" class="commonPopup" style="display: none">
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblBalanceChange" runat="server" Text="- Due To Leave Balance Value Change, Please Insert Re-Balance Effective Date" CssClass="Profiletitletxt"
                            meta:resourcekey="lblBalanceChangeDate" />
                    </div>
                </div>
                <div class="row">

                    <div class="col-md-4">
                        <asp:Label ID="lblEffectiveDate" runat="server" Text="Effective Date" CssClass="Profiletitletxt"
                            meta:resourcekey="lblEffectiveDate" />
                    </div>
                    <div class="col-md-3">
                        <telerik:RadDatePicker ID="dpEffectiveDate" runat="server" Culture="en-US">
                            <Calendar EnableWeekends="True" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelWidth="64px"
                                Width="">
                                <EmptyMessageStyle Resize="None" />
                                <ReadOnlyStyle Resize="None" />
                                <FocusedStyle Resize="None" />
                                <DisabledStyle Resize="None" />
                                <InvalidStyle Resize="None" />
                                <HoveredStyle Resize="None" />
                                <EnabledStyle Resize="None" />
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Button ID="btnSaveEffective" runat="server" Text="Save" CssClass="button" meta:resourcekey="btnSaveEffectiveResource1" />
                        <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" meta:resourcekey="btnCancelResource1" />--%>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="txtGeneralGuide" />
            <asp:PostBackTrigger ControlID="txtGeneralGuideAr" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdVwLeaveTypes.ClientID %>");
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

        function ValidateDeleteMaximumAllowed(sender, eventArgs) {
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



    </script>
</asp:Content>
