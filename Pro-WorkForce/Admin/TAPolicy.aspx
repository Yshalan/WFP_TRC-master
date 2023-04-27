<%@ Page Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="TAPolicy.aspx.vb" Title="TA Policy" Inherits="Admin_Default3" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">

        function ValidatePage() {
            var tabContainer = $get('<%=TabContainer1.ClientID%>');
            var valCntl = $get('<%=reqPolicyEngName.ClientID%>');

            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderreqPolicyEngName.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }

            var valCntl = $get('<%=reqPolicyArName.ClientID%>');

            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderPolicyArName.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }

            var valCntl = $get('<%=reqGraceIn.ClientID%>');

            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderreqGraceIn.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }

            var valCntl = $get('<%=reqGraceOut.ClientID%>');

            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderreqGraceOut.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }

            var valCntl = $get('<%=reqGraceOut.ClientID%>');

            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderreqGraceOut.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

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

        function ValidateTextboxFirstTime() {

            var tmpTime1 = $find("<%=rmtxtFirstTime.ClientID%>");
            txtValidate(tmpTime1, true);
        }

        function ValidateTextboxDeductionHours() {

            var tmpTime1 = $find("<%=rmtxtDeductionHours.ClientID%>");
            txtValidate(tmpTime1, true);
        }
        function ValidateTextboxNoHours() {

            var tmpTime1 = $find("<%=rmtxtCompleteNoHours.ClientID%>");
            txtValidate(tmpTime1, true);
        }

        function ValidateTextboxNoHours() {

            var tmpTime1 = $find("<%=rmtNoOfHours.ClientID%>");
            txtValidate(tmpTime1, true);
        }

        function ValidateTextboxNoHours_StudyNurs_NotCompleteHrs() {

            var tmpTime1 = $find("<%=rmtNoOfHours_StudyNurs_NotCompleteHrs.ClientID%>");
            txtValidate(tmpTime1, true);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <uc1:PageHeader ID="PageHeader1" runat="server" />

            <div class="row">
                <div class="col-md-12">
                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab"
                        meta:resourcekey="TabContainer1Resource1">
                        <cc1:TabPanel ID="Tab1" runat="server" HeaderText="TA Policy" meta:resourcekey="Tab1Resource1">
                            <ContentTemplate>

                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text="TA Policy Name English"
                                            meta:resourcekey="Label1Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtPolicyEnglish" runat="server" meta:resourcekey="txtPolicyEnglishResource1"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="reqPolicyEngName" runat="server" ControlToValidate="txtPolicyEnglish"
                                            Display="None" ErrorMessage="Please Enter English Name " ValidationGroup="GrPolicy"
                                            meta:resourcekey="reqPolicyEngNameResource1"></asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                                                ID="ExtenderreqPolicyEngName" runat="server" Enabled="True" TargetControlID="reqPolicyEngName"
                                                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                            </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label2" runat="server" CssClass="Profiletitletxt" Text="TA Policy Name Arabic"
                                            meta:resourcekey="Label2Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtPolicyArabic" runat="server" meta:resourcekey="txtPolicyArabicResource1"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="reqPolicyArName" runat="server" ControlToValidate="txtPolicyArabic"
                                            Display="None" ErrorMessage="Please Enter Arablic Name" ValidationGroup="GrPolicy"
                                            meta:resourcekey="reqPolicyArNameResource1"></asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                                                ID="ExtenderPolicyArName" runat="server" Enabled="True" TargetControlID="reqPolicyArName"
                                                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                            </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkHasLaunchBreak" Text="Has Lunch Break" runat="server" AutoPostBack="true" meta:resourcekey="lblHasLaunchBreakResource1" />
                                    </div>
                                </div>

                                <div class="row" runat="server" id="trLaunchBreak" visible="false">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblLaunchBreakDuration" runat="server" Text="Launch Break Duration"
                                            CssClass="Profiletitletxt" meta:resourcekey="lblLaunchBreakDurationResource1" />
                                    </div>
                                    <div class="col-md-2">
                                        <telerik:RadNumericTextBox ID="txtLaunchBreakDuration" runat="server" DataType="System.Int64"
                                            Culture="English (United States)" LabelCssClass="" CssClass="RadNumericTextBoxWidth" MinValue="0">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="Label3" runat="server" Text="Minute(s)" CssClass="Profiletitletxt"
                                            meta:resourcekey="lblMinutesResource1" />
                                        <asp:RequiredFieldValidator ID="reqLaunchBreakDuration" runat="server" ControlToValidate="txtLaunchBreakDuration"
                                            Display="None" ErrorMessage="Please Enter Launch Breake Duration" ValidationGroup="GrPolicy"
                                            meta:resourcekey="reqLaunchBreakDurationResource1" />
                                        <cc1:ValidatorCalloutExtender ID="ExtenderreqLaunchBreakDuration" runat="server"
                                            Enabled="True" TargetControlID="reqLaunchBreakDuration" CssClass="AISCustomCalloutStyle"
                                            WarningIconImageUrl="~/images/warning1.png" />

                                    </div>
                                </div>
                                <div class="row" runat="server" id="trLunchBreakBetween" visible="false">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblLunchBreakBetween" runat="server" CssClass="Profiletitletxt"
                                            Text="Lunch Break Between" meta:resourcekey="lblLunchBreakBetweenResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-2">

                                        <telerik:RadMaskedTextBox ID="rmtLaunchBreakFromTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                            DisplayMask="##:##" Text='0000' LabelCssClass="" meta:resourcekey="rmtLaunchBreakFromTimeResource1">
                                        </telerik:RadMaskedTextBox>
                                    </div>
                                    <div class="col-sm-1 text-center">
                                        <asp:Label ID="lbl" runat="server" CssClass="Profiletitletxt" Text=" - "
                                            meta:resourcekey="lblResource1"></asp:Label>
                                    </div>

                                    <div class="col-md-2">
                                        <telerik:RadMaskedTextBox ID="rmtLaunchBreakToTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                            DisplayMask="##:##" Text='0000' LabelCssClass="" meta:resourcekey="rmtLaunchBreakFromTimeResource1">
                                        </telerik:RadMaskedTextBox>
                                    </div>
                                </div>
                                <div class="row" id="dvMonthlyBreak" runat="server" visible="false">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblMonthlyBreak" runat="server" Text="Maximum Monthly Break\Grace"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <telerik:RadNumericTextBox ID="radnumtxtMonthlyBreak" runat="server" DataType="System.Int64"
                                            Culture="English (United States)" LabelCssClass="" CssClass="RadNumericTextBoxWidth" MinValue="0">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="Label4" runat="server" Text="Minute(s)" CssClass="Profiletitletxt"
                                            meta:resourcekey="lblMinutesResource1" />
                                    </div>
                                </div>
                                <div class="row" runat="server" id="trCompensateLaunchbreak" visible="false">
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkCompensateLaunchbreak" Text="Compensate Launch Break" meta:resourcekey="lblCompensateLaunchbreakResource1" runat="server" />
                                    </div>
                                </div>
                                <div class="row" runat="server" id="trHasReasonBreakTime" visible="false">
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkHasReasonBreakTime" Text="Break Time Has Reason" meta:resourcekey="lblHasReasonBreakTimeResource1" runat="server" AutoPostBack="true" />
                                    </div>
                                </div>
                                <div class="row" runat="server" id="trLaunchbreakReason" visible="false">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblLaunchbreakReason" runat="server" Text="Launch Break Reason" CssClass="Profiletitletxt"
                                            meta:resourcekey="lblLaunchbreakReasonResource1" />
                                    </div>
                                    <div class="col-md-3">
                                        <telerik:RadComboBox ID="ddlLaunchbreakReason" runat="server" MarkFirstMatch="true"
                                            AppendDataBoundItems="True" Skin="Vista" CausesValidation="False" meta:resourcekey="ddlLaunchbreakReason" />

                                        <asp:RequiredFieldValidator ID="reqLaunchbreakReason" runat="server" Display="None"
                                            ValidationGroup="GrPolicy" InitialValue="--Please Select--" ErrorMessage="Please Select Launch Break Reason"
                                            ControlToValidate="ddlLaunchbreakReason" meta:resourcekey="reqLaunchbreakReasonResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                                            TargetControlID="reqLaunchbreakReason">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkHasPrayTime" Text="Has Pray Time" meta:resourcekey="lblHasPrayTimeResource1" runat="server" AutoPostBack="true" />
                                    </div>

                                </div>
                                <div class="row" id="dvNoOfAllowedPrays" runat="server" visible="false">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblNoOfAllowedPrays" runat="server" Text="Number Of Allowed Pray Break(s)" CssClass="Profiletitletxt"
                                            meta:resourcekey="lblNoOfAllowedPraysResource1" />
                                    </div>
                                    <div class="col-md-2">
                                        <telerik:RadNumericTextBox ID="txtNoOfAllowedPrays" runat="server" DataType="System.Int64"
                                            Culture="English (United States)" LabelCssClass="" CssClass="RadNumericTextBoxWidth" MinValue="0">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="rfvNoOfAllowedPrays" runat="server" ControlToValidate="txtNoOfAllowedPrays"
                                            Display="None" ErrorMessage="Please Enter Number Of Allowed Pray Break(s)" ValidationGroup="GrPolicy"
                                            meta:resourcekey="rfvNoOfAllowedPraysResource1" />
                                        <cc1:ValidatorCalloutExtender ID="vceNoOfAllowedPrays" runat="server" Enabled="True"
                                            TargetControlID="rfvNoOfAllowedPrays" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png" />
                                    </div>

                                </div>
                                <div class="row" runat="server" id="trPrayTime" visible="false">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblPrayTimeDuration" runat="server" Text="Pray Time Duration" CssClass="Profiletitletxt"
                                            meta:resourcekey="lblPrayTimeDurationResource1" />
                                    </div>
                                    <div class="col-md-2">
                                        <telerik:RadNumericTextBox ID="txtPrayTimeDuration" runat="server" DataType="System.Int64"
                                            Culture="English (United States)" LabelCssClass="" CssClass="RadNumericTextBoxWidth" MinValue="0">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="reqPrayTimeDuration" runat="server" ControlToValidate="txtPrayTimeDuration"
                                            Display="None" ErrorMessage="Please Enter Pray Time Duration" ValidationGroup="GrPolicy"
                                            meta:resourcekey="reqPrayTimeDurationResource1" />
                                        <cc1:ValidatorCalloutExtender ID="ExtenderreqPrayTimeDuration" runat="server" Enabled="True"
                                            TargetControlID="reqPrayTimeDuration" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png" />
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="Label11" runat="server" Text="Minute(s)" CssClass="Profiletitletxt"
                                            meta:resourcekey="lblMinutesResource1" />
                                    </div>
                                </div>
                                <div class="row" runat="server" id="dvPrayBetween" visible="false">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblPrayBetween" runat="server" CssClass="Profiletitletxt"
                                            Text="Pray Break Between" meta:resourcekey="lblPrayBetweenResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <telerik:RadMaskedTextBox ID="rmtPrayBreakFromTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                            DisplayMask="##:##" Text='0000' LabelCssClass="" meta:resourcekey="rmtPrayBreakFromTimeResource1">
                                        </telerik:RadMaskedTextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <%--<div class="col-sm-1 text-center">--%>
                                        <asp:Label ID="Label14" runat="server" CssClass="Profiletitletxt" Text="-"
                                            meta:resourcekey="lblResource1"></asp:Label>
                                    </div>

                                    <div class="col-md-2">
                                        <telerik:RadMaskedTextBox ID="rmtPrayBreakToTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                            DisplayMask="##:##" Text='0000' LabelCssClass="" meta:resourcekey="rmtPrayBreakToTimeResource1">
                                        </telerik:RadMaskedTextBox>
                                    </div>
                                </div>

                                <div class="row" runat="server" id="trPrayTimeReason" visible="false">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblPrayTimeReason" runat="server" Text="Pray Time Reason" CssClass="Profiletitletxt"
                                            meta:resourcekey="lblPrayTimeReasonResource1" />
                                    </div>
                                    <div class="col-md-3">
                                        <telerik:RadComboBox ID="ddlPrayTimeReason" runat="server" MarkFirstMatch="true"
                                            AppendDataBoundItems="True" Skin="Vista" CausesValidation="False" meta:resourcekey="ddlPrayTimeReasonReason" />

                                        <asp:RequiredFieldValidator ID="reqPrayTimeReason" runat="server" Display="None"
                                            ValidationGroup="GrPolicy" InitialValue="--Please Select--" ErrorMessage="Please Select Pray Time Reason"
                                            ControlToValidate="ddlLaunchbreakReason" meta:resourcekey="reqPrayTimeReasonResource1">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" Enabled="True"
                                            TargetControlID="reqPrayTimeReason">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="trCompensatePrayTime" visible="false">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkCompensatePrayTime" Text="Compensate Pray Time" meta:resourcekey="lblCompensatePrayTimeResource1" runat="server" />
                                    </div>
                                </div>

                                <div class="row" id="trGraceIn" runat="server">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblGraceIn" runat="server" CssClass="Profiletitletxt" Text="Grace In Mins"
                                            meta:resourcekey="lblGraceInResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadNumericTextBox ID="txtGraceIn" runat="server" DataType="System.Int64"
                                            Culture="English (United States)" LabelCssClass="" CssClass="RadNumericTextBoxWidth" MinValue="0">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>

                                        <asp:RequiredFieldValidator ID="reqGraceIn" runat="server" ControlToValidate="txtGraceIn"
                                            Display="None" ErrorMessage="Please Enter Grace In Minutes" ValidationGroup="GrPolicy"></asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                                                ID="ExtenderreqGraceIn" runat="server" Enabled="True" TargetControlID="reqGraceIn"
                                                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                            </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row" id="trGraceOut" runat="server">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblGraceOut" runat="server" CssClass="Profiletitletxt" Text="Grace Out Mins"
                                            meta:resourcekey="lblGraceOutResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadNumericTextBox ID="txtGraceOut" runat="server" DataType="System.Int64"
                                            Culture="English (United States)" LabelCssClass="" CssClass="RadNumericTextBoxWidth" MinValue="0">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>

                                        <asp:RequiredFieldValidator ID="reqGraceOut" runat="server" ControlToValidate="txtGraceOut"
                                            Display="None" ErrorMessage="Please Enter  Grace Out Minutes" ValidationGroup="GrPolicy"></asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                                                ID="ExtenderreqGraceOut" runat="server" Enabled="True" TargetControlID="reqGraceOut"
                                                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                            </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkDelalIsFromGrace" runat="server" Text="Delay Is From Grace" meta:resourcekey="lblIsFromGraceResource1" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkEarlyOutisFromGrace" runat="server" Text="Early Out Is From Grace"
                                            AutoPostBack="true" meta:resourcekey="lblEarlyOutIsFromGraceResource1" />
                                    </div>
                                </div>
                                <div class="row" id="dvIgnoreEarlyOut_WithinGrace" runat="server" visible="false">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkIgnoreEarlyOut_WithinGrace" runat="server" Text="Ignore Early Out Within Or After Grace Period"
                                            meta:resourcekey="chkIgnoreEarlyOut_WithinGrace" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkFirstInLastOut" runat="server" Text="Consider First In\ Last Out Only" meta:resourcekey="chkFirstInLastOutResource1" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkConsiderAbsent_IfNotCompleteNoHours" runat="server"
                                            Text="Consider Absent If Not Complete No. Of Hours" AutoPostBack="true"
                                            meta:resourcekey="chkConsiderAbsent_IfNotCompleteNoHoursResource1" />
                                    </div>
                                </div>
                                <div class="row" runat="server" id="dvConsiderAbsent_IfNotCompleteNoHours" visible="false">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblNoOfHours" runat="server" Text="No. Of Hours" meta:resourcekey="lblNoOfHoursResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadMaskedTextBox ID="rmtNoOfHours" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                            DisplayMask="##:##" Text='0000' LabelCssClass="">
                                            <ClientEvents OnBlur="ValidateTextboxNoHours" />
                                        </telerik:RadMaskedTextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblHours" runat="server" Text="(hh:mm)" meta:resourcekey="lblHoursResource1"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkConsiderAbsentIfStudyNurs_NotCompleteHrs" runat="server"
                                            Text="Consider Absent If Has Study Or Nursing Permission And Not Complete No. Of Hours" AutoPostBack="true"
                                            meta:resourcekey="chkConsiderAbsentIfStudyNurs_NotCompleteHrsResource1" />
                                    </div>
                                </div>
                                <div class="row" runat="server" id="dvConsiderAbsentIfStudyNurs_NotCompleteHrs" visible="false">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblNoOfHoursStudyNurs_NotCompleteHrs" runat="server" Text="No. Of Hours" meta:resourcekey="lblNoOfHoursResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadMaskedTextBox ID="rmtNoOfHours_StudyNurs_NotCompleteHrs" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                            DisplayMask="##:##" Text='0000' LabelCssClass="">
                                            <ClientEvents OnBlur="ValidateTextboxNoHours_StudyNurs_NotCompleteHrs" />
                                        </telerik:RadMaskedTextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblHours_StudyNurs_NotCompleteHrs" runat="server" Text="(hh:mm)" meta:resourcekey="lblHoursResource1"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblMinDurationAsViolation" runat="server"
                                            Text="Minimum Duration To Be Considered As Violation" meta:resourcekey="lblMinDurationAsViolationResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadNumericTextBox ID="radminDuration" runat="server" DataType="System.Int64"
                                            Culture="English (United States)" LabelCssClass="" CssClass="RadNumericTextBoxWidth" MinValue="0">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="Tab2" runat="server" HeaderText="Absent Rules" TabIndex="1" meta:resourcekey="Tab2Resource1">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="Absent Rule Type"
                                            meta:resourcekey="Label5Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadComboBox ID="ddlAbsentRuleType" runat="server" MarkFirstMatch="true"
                                            AutoPostBack="True" meta:resourcekey="ddlAbsentRuleTypeResource1">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="0" Text="--Please Select--" runat="server" meta:resourcekey="RadComboBoxItemResource1" />
                                                <telerik:RadComboBoxItem Value="1" Text="One Day Delay Limit" runat="server" meta:resourcekey="RadComboBoxItemResource2" />
                                                <%-- <telerik:RadComboBoxItem Value="2" Text="Consecutive Delays" runat="server" meta:resourcekey="RadComboBoxItemResource3" />
                                                        <telerik:RadComboBoxItem Value="3" Text="No. of delays per period" runat="server"
                                                            meta:resourcekey="RadComboBoxItemResource4" />--%>
                                                <telerik:RadComboBoxItem Value="2" Text="One Day Early Out Limit" runat="server"
                                                    meta:resourcekey="RadComboBoxItemResource3" />
                                                <telerik:RadComboBoxItem Value="3" Text="One Day Delay and Early Out Limit" runat="server"
                                                    meta:resourcekey="RadComboBoxItemResource4" />
                                            </Items>
                                        </telerik:RadComboBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAbsentRuleType"
                                            Display="None" ErrorMessage="Please Select Absent Rule" InitialValue="--Please Select--"
                                            ValidationGroup="groupAddAbsentRule" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                            TargetControlID="RequiredFieldValidator1" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label6" runat="server" CssClass="Profiletitletxt" Text="English Name"
                                            meta:resourcekey="Label6Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtAbsentRuleEnglishName" runat="server" meta:resourcekey="txtAbsentRuleEnglishNameResource1"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAbsentRuleEnglishName"
                                            Display="None" ErrorMessage="Please enter Absent Rule English Name" ValidationGroup="groupAddAbsentRule"
                                            meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                                            TargetControlID="RequiredFieldValidator2">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label7" runat="server" CssClass="Profiletitletxt" Text="Arabic Name"
                                            meta:resourcekey="Label7Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtAbsentRuleArabichName" runat="server" meta:resourcekey="txtAbsentRuleArabichNameResource1"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblVariable1" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblVariable1Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadNumericTextBox ID="txtVar1" runat="server" DataType="System.Int64" Culture="English (United States)"
                                            LabelCssClass="" meta:resourcekey="txtVar1Resource1" CssClass="RadNumericTextBoxWidth">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtVar1"
                                            Display="None" ValidationGroup="groupAddAbsentRule" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                                            TargetControlID="RequiredFieldValidator4">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblVariable2" runat="server" CssClass="Profiletitletxt" Text="Variable 2"
                                            meta:resourcekey="lblVariable2Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadNumericTextBox ID="txtVar2" runat="server" DataType="System.Int64" Culture="English (United States)"
                                            LabelCssClass="" meta:resourcekey="txtVar2Resource1" CssClass="RadNumericTextBoxWidth">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="button" ValidationGroup="groupAddAbsentRule"
                                            meta:resourcekey="btnAddResource1" />
                                        <asp:Button ID="btnRemove" runat="server" OnClientClick="return AbsentRulesValidateDelete();" CssClass="button" Text="Remove" meta:resourcekey="btnRemoveResource1" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="table-responsive">
                                        <telerik:RadGrid ID="dgrdAbsentRules" runat="server" AllowPaging="True"
                                            GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True"
                                            PageSize="25" meta:resourcekey="dgrdAbsentRulesResource1">
                                            <GroupingSettings CaseSensitive="False" />
                                            <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                                <Selecting AllowRowSelect="True" />
                                            </ClientSettings>
                                            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="AbsentRuleId,RuleName,RuleArabicName,AbsentRuleType,Variable1,Variable2">
                                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                                        UniqueName="TemplateColumn">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="RuleName" SortExpression="RuleName" HeaderText="English Name"
                                                        UniqueName="RuleName" meta:resourcekey="GridBoundColumnResource1" />
                                                    <telerik:GridBoundColumn DataField="RuleArabicName" SortExpression="RuleArabicName"
                                                        HeaderText="Arabic Name" UniqueName="RuleArabicName" meta:resourcekey="GridBoundColumnResource2" />
                                                    <telerik:GridBoundColumn DataField="AbsentRuleId" SortExpression="AbsentRuleId" Visible="False"
                                                        UniqueName="AbsentRuleId" meta:resourcekey="GridBoundColumnResource3" />
                                                    <telerik:GridBoundColumn DataField="FK_TAPolicyId" SortExpression="FK_TAPolicyId"
                                                        Visible="False" UniqueName="FK_TAPolicyId" meta:resourcekey="GridBoundColumnResource4" />
                                                    <telerik:GridBoundColumn DataField="AbsentRuleType" SortExpression="AbsentRuleType"
                                                        Visible="False" UniqueName="AbsentRuleType" meta:resourcekey="GridBoundColumnResource5" />
                                                    <telerik:GridBoundColumn DataField="Variable1" SortExpression="Variable1" Visible="False"
                                                        UniqueName="Variable1" meta:resourcekey="GridBoundColumnResource6" />
                                                    <telerik:GridBoundColumn DataField="Variable2" SortExpression="Variable2" Visible="False"
                                                        UniqueName="Variable2" meta:resourcekey="GridBoundColumnResource7" />
                                                    <telerik:GridTemplateColumn HeaderText="Rule Type" meta:resourcekey="GridTemplateColumnResource2"
                                                        UniqueName="TemplateColumn1">
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                            </MasterTableView>
                                            <SelectedItemStyle ForeColor="Maroon" />
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Violations" TabIndex="2"
                            meta:resourcekey="TabPanel1Resource1">
                            <ContentTemplate>

                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label8" runat="server" CssClass="Profiletitletxt" Text="Violation Rule Type"
                                            meta:resourcekey="Label8Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadComboBox ID="ddlViolationRuleType" runat="server" AutoPostBack="True"
                                            MarkFirstMatch="true" meta:resourcekey="ddlViolationRuleTypeResource1">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="0" Text="--Please Select--" runat="server" meta:resourcekey="RadComboBoxItemResource5" />
                                                <telerik:RadComboBoxItem Value="1" Text="One Day Delay Limit" runat="server" Visible="false"
                                                    meta:resourcekey="RadComboBoxItemResource6" />
                                                <telerik:RadComboBoxItem Value="2" Text="Consecutive Delays" runat="server" Visible="false"
                                                    meta:resourcekey="RadComboBoxItemResource7" />
                                                <telerik:RadComboBoxItem Value="3" Text="No. of delays per period" runat="server" Visible="false"
                                                    meta:resourcekey="RadComboBoxItemResource8" />
                                                <telerik:RadComboBoxItem Value="4" Text="One Absent Day" Visible="false" runat="server"
                                                    meta:resourcekey="RadComboBoxItemResource9" />
                                                <telerik:RadComboBoxItem Value="5" Text="Consecutive Absent Days" runat="server"
                                                    Visible="false" meta:resourcekey="RadComboBoxItemResource10" />
                                                <telerik:RadComboBoxItem Value="6" Text="Absent days per period" runat="server" meta:resourcekey="RadComboBoxItemResource11" />
                                                <telerik:RadComboBoxItem Value="7" Text="One Day Early Out Limit" Visible="false"
                                                    runat="server" meta:resourcekey="RadComboBoxItemResource12" />
                                                <telerik:RadComboBoxItem Value="8" Text="Consecutive Early Outs" Visible="false"
                                                    runat="server" meta:resourcekey="RadComboBoxItemResource13" />
                                                <telerik:RadComboBoxItem Value="9" Text="No. of Early Outs Per Period" runat="server" Visible="false"
                                                    meta:resourcekey="RadComboBoxItemResource14" />
                                                <telerik:RadComboBoxItem Value="10" Text="Missing In - Missing Out Per Period" runat="server"
                                                    meta:resourcekey="RadComboBoxItemResource15" />
                                                <telerik:RadComboBoxItem Value="11" Text="Delay or Early Out Per Period" runat="server"
                                                    meta:resourcekey="RadComboBoxItemResource16" />
                                            </Items>
                                        </telerik:RadComboBox>

                                        <asp:RequiredFieldValidator ID="reqViolationRuleType" runat="server" ControlToValidate="ddlViolationRuleType"
                                            Display="None" ErrorMessage="Please Select Violation Rule Type" InitialValue="--Please Select--"
                                            ValidationGroup="groupViolation" meta:resourcekey="reqViolationRuleTypeResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValreqViolationRuleType" runat="server" Enabled="True"
                                            TargetControlID="reqViolationRuleType" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label9" runat="server" CssClass="Profiletitletxt" Text="English Name"
                                            meta:resourcekey="Label9Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtViolationEn" runat="server" meta:resourcekey="txtViolationEnResource1"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="ReqViolationEn" runat="server" ControlToValidate="txtViolationEn"
                                            Display="None" ErrorMessage="Please enter Violation English Name" ValidationGroup="groupViolation"
                                            meta:resourcekey="ReqViolationEnResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="valReqViolationEn" runat="server" Enabled="True"
                                            TargetControlID="ReqViolationEn">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label10" runat="server" CssClass="Profiletitletxt" Text="Arabic Name"
                                            meta:resourcekey="Label10Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtViolationAr" runat="server" meta:resourcekey="txtViolationArResource1"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblViolationVAr1" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblViolationVAr1Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadNumericTextBox ID="txtViolationVar1" runat="server" DataType="System.Int64"
                                            Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtViolationVar1Resource1" CssClass="RadNumericTextBoxWidth">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>

                                        <asp:RequiredFieldValidator ID="reqViolationVar1" runat="server" ControlToValidate="txtViolationVar1"
                                            Display="None" ValidationGroup="groupViolation" meta:resourcekey="reqViolationVar1Resource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValreqViolationVar1" runat="server" Enabled="True"
                                            TargetControlID="reqViolationVar1">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblScenarioMode" runat="server" CssClass="Profiletitletxt" Text="Scenario Mode" Visible="false"
                                            meta:resourcekey="lblScenarioModeResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadComboBox ID="ddlScenarioMode" runat="server" Visible="false"
                                            MarkFirstMatch="true" meta:resourcekey="ddlViolationRuleTypeResource1">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="0" Text="--Please Select--" runat="server" meta:resourcekey="RadComboBoxItemResource5" />
                                                <telerik:RadComboBoxItem Value="1" Text="Occurrences" runat="server"
                                                    meta:resourcekey="OccurrencesItemResource6" />
                                                <telerik:RadComboBoxItem Value="2" Text="Continues" runat="server"
                                                    meta:resourcekey="ContinuesItemResource7" />
                                                <telerik:RadComboBoxItem Value="3" Text="Separate" runat="server"
                                                    meta:resourcekey="SeparateItemResource8" />

                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="reqScenarioMode" runat="server" ControlToValidate="ddlScenarioMode"
                                            Display="None" ErrorMessage="Please Select Scenario Mode" InitialValue="--Please Select--"
                                            ValidationGroup="groupViolation" meta:resourcekey="reqScenarioModeResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True"
                                            TargetControlID="reqScenarioMode" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblViolationVAr3" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblViolationVAr3Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadNumericTextBox ID="txtViolationVar3" runat="server" DataType="System.Int64"
                                            Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtViolationVar1Resource1" CssClass="RadNumericTextBoxWidth">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>

                                        <asp:RequiredFieldValidator ID="reqViolationVar3" runat="server" ControlToValidate="txtViolationVar1"
                                            Display="None" ValidationGroup="groupViolation" meta:resourcekey="reqViolationVar1Resource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValreqViolationVar3" runat="server" Enabled="True"
                                            TargetControlID="reqViolationVar1">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblViolationVAr2" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblViolationVAr2Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadComboBox ID="ddlViolationVAr2" runat="server" AutoPostBack="True" MarkFirstMatch="true"
                                            meta:resourcekey="ddlViolationVAr2Resource1">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="0" Text="--Please Select--" runat="server" meta:resourcekey="RadComboBoxItemResource5" />
                                                <telerik:RadComboBoxItem Value="1" Text="One Month" runat="server" meta:resourcekey="RadComboBoxItemResource17" />
                                                <telerik:RadComboBoxItem Value="2" Text="Two Month" runat="server" meta:resourcekey="RadComboBoxItemResource21" />
                                                <telerik:RadComboBoxItem Value="3" Text="Three Months" runat="server" meta:resourcekey="RadComboBoxItemResource18" />
                                                <telerik:RadComboBoxItem Value="4" Text="Four Months" runat="server" meta:resourcekey="RadComboBoxItemResource22" />
                                                <telerik:RadComboBoxItem Value="5" Text="Five Months" runat="server" meta:resourcekey="RadComboBoxItemResource23" />
                                                <telerik:RadComboBoxItem Value="6" Text="Six Months" runat="server" meta:resourcekey="RadComboBoxItemResource19" />
                                                <telerik:RadComboBoxItem Value="7" Text="Seven Months" runat="server" meta:resourcekey="RadComboBoxItemResource24" />
                                                <telerik:RadComboBoxItem Value="8" Text="Eight Months" runat="server" meta:resourcekey="RadComboBoxItemResource25" />
                                                <telerik:RadComboBoxItem Value="9" Text="Nine Months" runat="server" meta:resourcekey="RadComboBoxItemResource26" />
                                                <telerik:RadComboBoxItem Value="10" Text="Ten Months" runat="server" meta:resourcekey="RadComboBoxItemResource27" />
                                                <telerik:RadComboBoxItem Value="11" Text="Eleven Months" runat="server" meta:resourcekey="RadComboBoxItemResource28" />
                                                <telerik:RadComboBoxItem Value="12" Text="One Year" runat="server" meta:resourcekey="RadComboBoxItemResource20" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label13" runat="server" CssClass="Profiletitletxt" Text="Violation Action"
                                            meta:resourcekey="Label13Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadComboBox ID="ddlViolationAction" runat="server" MarkFirstMatch="true"
                                            MaxHeight="200px" meta:resourcekey="ddlViolationActionResource1" />

                                        <asp:RequiredFieldValidator ID="reqViolationAction" runat="server" ControlToValidate="ddlViolationAction"
                                            Display="None" ErrorMessage="Please Select Violation Action" InitialValue="--Please Select--"
                                            ValidationGroup="groupViolation" meta:resourcekey="reqViolationActionResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="valreqViolationAction" runat="server" Enabled="True"
                                            TargetControlID="reqViolationAction" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblViolationAction2" runat="server" CssClass="Profiletitletxt" Text="Violation Action"
                                            meta:resourcekey="lblViolationAction2Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadComboBox ID="ddlViolationAction2" runat="server" MarkFirstMatch="true"
                                            MaxHeight="200px" meta:resourcekey="ddlViolationAction2Resource1" />

                                        <asp:RequiredFieldValidator ID="reqViolationAction2" runat="server" ControlToValidate="ddlViolationAction2"
                                            Display="None" ErrorMessage="Please Select Violation Action" InitialValue="--Please Select--"
                                            ValidationGroup="groupViolation" meta:resourcekey="reqViolationAction2Resource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="valreqViolationAction2" runat="server" Enabled="True"
                                            TargetControlID="reqViolationAction2" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblViolationAction3" runat="server" CssClass="Profiletitletxt" Text="Violation Action"
                                            meta:resourcekey="lblViolationAction3Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadComboBox ID="ddlViolationAction3" runat="server" MarkFirstMatch="true"
                                            MaxHeight="200px" meta:resourcekey="ddlViolationAction3Resource1" />

                                        <asp:RequiredFieldValidator ID="reqViolationAction3" runat="server" ControlToValidate="ddlViolationAction3"
                                            Display="None" ErrorMessage="Please Select Violation Action" InitialValue="--Please Select--"
                                            ValidationGroup="groupViolation" meta:resourcekey="reqViolationAction3Resource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="valreqViolationAction3" runat="server" Enabled="True"
                                            TargetControlID="reqViolationAction3" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblViolationAction4" runat="server" CssClass="Profiletitletxt" Text="Violation Action"
                                            meta:resourcekey="lblViolationAction4Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadComboBox ID="ddlViolationAction4" runat="server" MarkFirstMatch="true"
                                            MaxHeight="200px" meta:resourcekey="ddlViolationAction4Resource1" />

                                        <asp:RequiredFieldValidator ID="reqViolationAction4" runat="server" ControlToValidate="ddlViolationAction4"
                                            Display="None" ErrorMessage="Please Select Violation Action" InitialValue="--Please Select--"
                                            ValidationGroup="groupViolation" meta:resourcekey="reqViolationAction4Resource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="valreqViolationAction4" runat="server" Enabled="True"
                                            TargetControlID="reqViolationAction4" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblViolationAction5" runat="server" CssClass="Profiletitletxt" Text="Violation Action"
                                            meta:resourcekey="lblViolationAction5Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadComboBox ID="ddlViolationAction5" runat="server" MarkFirstMatch="true"
                                            MaxHeight="200px" meta:resourcekey="ddlViolationAction5Resource1" />

                                        <asp:RequiredFieldValidator ID="reqViolationAction5" runat="server" ControlToValidate="ddlViolationAction5"
                                            Display="None" ErrorMessage="Please Select Violation Action" InitialValue="--Please Select--"
                                            ValidationGroup="groupViolation" meta:resourcekey="reqViolationAction5Resource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="valreqViolationAction5" runat="server" Enabled="True"
                                            TargetControlID="reqViolationAction5" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnAddViolation" runat="server" Text="Save" CssClass="button" ValidationGroup="groupViolation"
                                            meta:resourcekey="btnAddViolationResource1" />
                                        <asp:Button ID="btnRemoveViolation" runat="server" OnClientClick="return ViolationsValidateDelete();" CssClass="button" Text="Remove"
                                            meta:resourcekey="btnRemoveViolationResource1" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="table-responsive">
                                        <telerik:RadGrid ID="dgrdViolation" runat="server" AllowPaging="True"
                                            GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True"
                                            PageSize="25" meta:resourcekey="dgrdViolationResource1">
                                            <GroupingSettings CaseSensitive="False" />
                                            <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                                <Selecting AllowRowSelect="True" />
                                            </ClientSettings>
                                            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="ViolationRuleType,ViolationId,ViolationName,ViolationArabicName,Variable1,Variable2,Variable3,FK_ViolationActionId,FK_ViolationActionId2,FK_ViolationActionId3,FK_ViolationActionId4,FK_ViolationActionId5,ScenarioMode">
                                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource3"
                                                        UniqueName="TemplateColumn">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="ViolationName" SortExpression="ViolationName"
                                                        HeaderText="English Name" UniqueName="ViolationName" meta:resourcekey="GridBoundColumnResource8" />
                                                    <telerik:GridBoundColumn DataField="ViolationArabicName" SortExpression="ViolationArabicName"
                                                        HeaderText="Arabic Name" UniqueName="ViolationArabicName" meta:resourcekey="GridBoundColumnResource9" />
                                                    <telerik:GridBoundColumn DataField="ViolationId" SortExpression="ViolationId" Visible="False"
                                                        UniqueName="ViolationId" meta:resourcekey="GridBoundColumnResource10" />
                                                    <telerik:GridBoundColumn DataField="FK_TAPolicyId" SortExpression="FK_TAPolicyId"
                                                        Visible="False" UniqueName="FK_TAPolicyId" meta:resourcekey="GridBoundColumnResource11" />
                                                    <telerik:GridBoundColumn DataField="ViolationRuleType" SortExpression="ViolationRuleType"
                                                        Visible="False" UniqueName="ViolationRuleType" meta:resourcekey="GridBoundColumnResource12" />
                                                    <telerik:GridBoundColumn DataField="Variable1" SortExpression="Variable1" Visible="False"
                                                        UniqueName="Variable1" meta:resourcekey="GridBoundColumnResource13" />
                                                    <telerik:GridBoundColumn DataField="Variable2" SortExpression="Variable2" Visible="False"
                                                        UniqueName="Variable2" meta:resourcekey="GridBoundColumnResource14" />
                                                    <telerik:GridBoundColumn DataField="Variable3" SortExpression="Variable2" Visible="False"
                                                        UniqueName="Variable3" meta:resourcekey="GridBoundColumnResource14" />
                                                    <telerik:GridBoundColumn DataField="FK_ViolationActionId" SortExpression="FK_ViolationActionId"
                                                        Visible="False" UniqueName="FK_ViolationActionId" meta:resourcekey="GridBoundColumnResource15" />
                                                    <telerik:GridBoundColumn DataField="FK_ViolationActionId2" SortExpression="FK_ViolationActionId"
                                                        Visible="False" UniqueName="FK_ViolationActionId2" meta:resourcekey="GridBoundColumnResource15" />
                                                    <telerik:GridBoundColumn DataField="FK_ViolationActionId3" SortExpression="FK_ViolationActionId"
                                                        Visible="False" UniqueName="FK_ViolationActionId3" meta:resourcekey="GridBoundColumnResource15" />
                                                    <telerik:GridBoundColumn DataField="FK_ViolationActionId4" SortExpression="FK_ViolationActionId"
                                                        Visible="False" UniqueName="FK_ViolationActionId4" meta:resourcekey="GridBoundColumnResource15" />
                                                    <telerik:GridBoundColumn DataField="FK_ViolationActionId5" SortExpression="FK_ViolationActionId"
                                                        Visible="False" UniqueName="FK_ViolationActionId5" meta:resourcekey="GridBoundColumnResource15" />
                                                    <telerik:GridTemplateColumn HeaderText="Rule Type" meta:resourcekey="GridTemplateColumnResource4"
                                                        UniqueName="TemplateColumn1" DataField="TemplateColumn1">
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                            </MasterTableView>
                                            <SelectedItemStyle ForeColor="Maroon" />
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabDeduction" runat="server" HeaderText="Deduction Policy" TabIndex="3"
                            meta:resourcekey="TabDeductionResource1" Visible="true">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblAttention" SkinID="Remark" runat="server"
                                            Text="** Please Select the Deduction Type and Deduction Policy To Be Considered From Below"
                                            meta:resourcekey="lblAttentionResource1"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkAbsent" Text="Absent" runat="server"
                                            meta:resourcekey="chkAbsentResource1" />
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="imgbtnAbsent" runat="server" CssClass="infolinkbtn">
                                            <i id="I1" class="fa fa-info" runat="server"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkMissingIn" Text="Missing In" runat="server"
                                            meta:resourcekey="chkMissingInResource1" />
                                    </div>
                                    <div class="col-md-1">

                                        <asp:LinkButton ID="imgbtnMissingIn" runat="server" CssClass="infolinkbtn">
                                            <i id="I2" class="fa fa-info" runat="server"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkMissingOut" Text="Missing Out" runat="server"
                                            meta:resourcekey="chkMissingOutResource1" />
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="imgbtnMissingOut" runat="server" CssClass="infolinkbtn">
                                            <i id="I3" class="fa fa-info" runat="server"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkDelay_Earlyout" Text="Delay and Early Out" runat="server" AutoPostBack="true"
                                            meta:resourcekey="chkDelay_EarlyoutResource1" />
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="imgbtnDelay_Earlyout" runat="server" CssClass="infolinkbtn">
                                            <i id="I4" class="fa fa-info" runat="server"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div id="dvDelay_Earlyout" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblConsiderDelayEarlyBy" runat="server" Text="Consider Delay & Early Out By"
                                                meta:resourcekey="lblConsiderDelayEarlyByResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:RadioButtonList ID="rblConsiderDelayEarlyBy" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Total Duration" meta:resourcekey="ListItemDurationResource1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Occuerence" meta:resourcekey="ListItemOccuerenceResource1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div id="dvDelayEarlyConsiderDuration" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <asp:Label ID="lblFirstTime" runat="server" Text="First Time Deduct Hours"
                                                    meta:resourcekey="lblFirstTimeResource1"></asp:Label>
                                            </div>
                                            <div class="col-md-3">
                                                <telerik:RadMaskedTextBox ID="rmtxtFirstTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                                    DisplayMask="##:##" Text='0000' LabelCssClass="">
                                                    <ClientEvents OnBlur="ValidateTextboxFirstTime" />
                                                </telerik:RadMaskedTextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblTimeDesc1" runat="server" Text="(HH:MM)"
                                                    meta:resourcekey="lblTimeDesc1Resource1"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <asp:Label ID="lblDeductionHours" runat="server" Text="Deduction Hours"
                                                    meta:resourcekey="lblDeductionHoursResource1"></asp:Label>
                                            </div>
                                            <div class="col-md-3">
                                                <telerik:RadMaskedTextBox ID="rmtxtDeductionHours" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                                    DisplayMask="##:##" Text='0000' LabelCssClass="">
                                                    <ClientEvents OnBlur="ValidateTextboxDeductionHours" />
                                                </telerik:RadMaskedTextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblTimeDesc2" runat="server" Text="(HH:MM)"
                                                    meta:resourcekey="lblTimeDesc2Resource1"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <asp:CheckBox ID="chkIncludeLostTime" runat="server" Text="Include Lost Time Duration"
                                                    meta:resourcekey="chkIncludeLostTimeResource1"
                                                    ToolTip="Out Duration Will Be Added To The Total Delay and Early Out Duration" />
                                            </div>
                                            <div class="col-md-3"></div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <asp:CheckBox ID="chkRemainingBalancetoBeRounded" runat="server" Text="Remaining Duration To Be Rounded To Next Month"
                                                    meta:resourcekey="chkRemainingBalancetoBeRoundedResource1"
                                                    ToolTip="Remaining Duration After Deduction Will Be Rounded to the Next Month" />
                                            </div>
                                            <div class="col-md-3"></div>
                                        </div>
                                    </div>
                                    <div id="dvDelayEarlyConsiderOccuerence" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <asp:CheckBox ID="chkConsiderOneDeduction_DelayEarly" runat="server"
                                                    Text="Consider One Day Deduction When Delay & Early At The Same Day"
                                                    meta:resourcekey="chkConsiderOneDeduction_DelayEarlyResource1" />
                                            </div>
                                            <div class="col-md-3"></div>
                                        </div>
                                    </div>

                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkNotCompleteWork" Text="Not Complete Work Hours" runat="server" AutoPostBack="true"
                                            meta:resourcekey="chkNotCompleteWorkResource1" />
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="imgbtnNotCompleteWork" runat="server" CssClass="infolinkbtn">
                                            <i id="I5" class="fa fa-info" runat="server"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div id="dvNotCompleteWorkSelection" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:RadioButtonList ID="rblNotCompleteWork" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                <asp:ListItem Value="1" Text="Percentage" Selected="True" meta:resourcekey="rblNotCompleteWorkListItem1Resource1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="No. Of Hours" meta:resourcekey="rblNotCompleteWorkListItem2Resource1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div id="dvNotCompletePercentage" runat="server" visible="true">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <asp:Label ID="lblPercentage" runat="server" Text="Schedule Hours Percentage"
                                                    meta:resourcekey="lblPercentageResource1"></asp:Label>
                                            </div>
                                            <div class="col-md-3">
                                                <telerik:RadNumericTextBox ID="txtPercentage" MinValue="0" MaxValue="100" Skin="Vista"
                                                    runat="server" Culture="en-US" LabelCssClass="" Width="200px">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>

                                            </div>
                                            <div class="col-md-1"><i class="fa fa-percent"></i></div>
                                        </div>
                                    </div>
                                    <div id="dvNotCompleteNoHours" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <asp:Label ID="lblCompleteNoHours" runat="server" Text="No. Of Hours"
                                                    meta:resourcekey="lblCompleteNoHoursResource1"></asp:Label>
                                            </div>
                                            <div class="col-md-3">
                                                <telerik:RadMaskedTextBox ID="rmtxtCompleteNoHours" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                                    DisplayMask="##:##" Text='0000' LabelCssClass="">
                                                    <ClientEvents OnBlur="ValidateTextboxNoHours" />
                                                </telerik:RadMaskedTextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblTimeDesc3" runat="server" Text="(HH:MM)"
                                                    meta:resourcekey="lblTimeDesc3Resource1"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chkExcludePendingLeaves" runat="server" Text="Exclude Pending Leave Days From Deduction"
                                            meta:resourcekey="chkExcludePendingLeavesResource1" />
                                    </div>
                                    <div class="col-md-3"></div>
                                </div>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="ibtnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="GrPolicy"
                                meta:resourcekey="ibtnSaveResource1" />
                            <asp:Button ID="ibtnDelete" runat="server" Text="Delete" CssClass="button" CausesValidation="False"
                                meta:resourcekey="ibtnDeleteResource1" OnClientClick="return ValidateDelete();" />
                            <asp:Button ID="ibtnRest" runat="server" Text="Clear" CssClass="button" CausesValidation="False"
                                meta:resourcekey="ibtnRestResource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <div>
                                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdTAPolicy"
                                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                            </div>
                            <telerik:RadGrid ID="dgrdTAPolicy" runat="server" AllowSorting="True" AllowPaging="True"
                                PageSize="25" GridLines="None" ShowStatusBar="True"
                                AllowMultiRowSelection="True" ShowFooter="True" OnItemCommand="dgrdTAPolicy_ItemCommand"
                                meta:resourcekey="dgrdTAPolicyResource1">
                                <SelectedItemStyle ForeColor="Maroon" />
                                <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="TAPolicyId,TAPolicyName">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource5"
                                            UniqueName="TemplateColumn">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="TAPolicyName" SortExpression="TAPolicyName" HeaderText="TA PolicyEnglish Name"
                                            meta:resourcekey="GridBoundColumnResource16" UniqueName="TAPolicyName" />
                                        <telerik:GridBoundColumn DataField="TAPolicyArabicName" SortExpression="TAPolicyArabicName"
                                            HeaderText="TA Policy Arabic Name" meta:resourcekey="GridBoundColumnResource17"
                                            UniqueName="TAPolicyArabicName" />
                                        <telerik:GridBoundColumn DataField="TAPolicyId" SortExpression="TAPolicyId" Visible="False"
                                            AllowFiltering="False" meta:resourcekey="GridBoundColumnResource18" UniqueName="TAPolicyId" />
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdTAPolicy.ClientID %>");
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


        function AbsentRulesValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdAbsentRules.ClientID %>");
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


        function ViolationsValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdViolation.ClientID %>");
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
