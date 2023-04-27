<%@ Control Language="VB" AutoEventWireup="false" CodeFile="HR_PermissionApproval.ascx.vb"
    Inherits="DailyTasks_UserControls_HR_PermissionApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:UpdatePanel ID="updatePnlEmpPerm" runat="server">
    <ContentTemplate>

        <div id="divEmpPermissions" runat="server">
            <div class="row">
                <uc1:PageFilter ID="EmployeeFilterUC" runat="server" HeaderText="Employee Filter"
                    ValidationGroup="EmpPermissionGroup" />
            </div>
            <div class="row" id="trPermType" runat="server">
                <div class="col-md-2" id="Td1" runat="server">
                    <asp:Label ID="lblPermission" runat="server" Text="Type"
                        meta:resourcekey="lblPermissionResource1"></asp:Label>
                </div>
                <div class="col-md-4" id="Td2" runat="server">
                    <telerik:RadComboBox ID="RadCmpPermissions" MarkFirstMatch="True" AutoPostBack="True"
                        Skin="Vista" ToolTip="View types of employee permission" runat="server">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqPermission" runat="server" ControlToValidate="RadCmpPermissions"
                        Display="None" ErrorMessage="Please select permission english name" InitialValue="--Please Select--"
                        ValidationGroup="EmpPermissionGroup"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderPermission" runat="server"
                        TargetControlID="reqPermission" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row" id="trType" runat="server">
                <div class="col-md-2" id="Td3" runat="server"></div>
                <div class="col-md-4" id="tdOption" runat="server">
                    <asp:RadioButton ID="radBtnOneDay" Text="One Time Permission" Checked="True" runat="server"
                        AutoPostBack="True" GroupName="LeaveGroup" meta:resourcekey="radBtnOneDayResource1" />
                    <asp:RadioButton ID="radBtnPeriod" Text="Permission For Period" runat="server" AutoPostBack="True"
                        GroupName="LeaveGroup" meta:resourcekey="radBtnPeriodResource1" />
                    <asp:RadioButton ID="radBtnSpecificDays" Text="Specific Days for Duration" runat="server"
                        AutoPostBack="True" GroupName="LeaveGroup" meta:resourcekey="radBtnSpecificDaysResource1" />
                </div>
            </div>
            <asp:Panel ID="PnlOneDayLeave" runat="server" meta:resourcekey="PnlOneDayLeaveResource1">
                <div class="svpanel">
                    <div class="row">
                        <div class="col-md-2" id="tdDate" runat="server">
                            <asp:Label CssClass="Profiletitletxt" ID="lblPermissionDate" runat="server" Text="Date"
                                meta:resourcekey="lblPermissionDateResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">

                            <asp:Label CssClass="Profiletitletxt" ID="lblAtDate" runat="server" Text="At" meta:resourcekey="lblAtDateResource1"></asp:Label>
                            <telerik:RadDatePicker ID="dtpPermissionDate" AllowCustomText="false" MarkFirstMatch="true"
                                Skin="Vista" runat="server" Culture="en-US" meta:resourcekey="dtpPermissionDateResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" ToolTip="View permission date" DisplayDateFormat="dd/MM/yyyy"
                                    LabelCssClass="" Width="">
                                </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqPermissionDate" runat="server" ControlToValidate="dtpPermissionDate"
                                Display="None" ErrorMessage="Please select permission date" ValidationGroup="EmpPermissionGroup"
                                meta:resourcekey="reqPermissionDateResource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ExtenderPermissionDate" runat="server" TargetControlID="reqPermissionDate"
                                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>


                        </div>
                    </div>
                </div>
            </asp:Panel>
            <br />
            <asp:Panel ID="pnlPeriodLeave" Visible="False" runat="server" meta:resourcekey="pnlPeriodLeaveResource1">
                <br />
                <div class="row" id="trWeekDays" runat="server">
                    <div class="col-md-2" id="Td4" runat="server">
                        <asp:Label ID="lblDaysList" runat="server" Text="Week Days" CssClass="Profiletitletxt" />
                    </div>
                    <div class="col-md-4" id="tdWeekDays" runat="server">
                        <asp:CheckBoxList ID="chkWeekDays" runat="server" RepeatDirection="Horizontal" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblDateFrom" runat="server" Text="Date"
                            meta:resourcekey="lblDateFromResource1"></asp:Label>
                    </div>
                    <div class="col-md-10" id="trDateFromTo" runat="server">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblFromDate" runat="server" Text="From"
                                meta:resourcekey="lblFromDateResource1"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <telerik:RadDatePicker ID="dtpStartDatePerm" ToolTip="Click" AllowCustomText="false"
                                MarkFirstMatch="true" Skin="Vista" runat="server" Culture="en-US" meta:resourcekey="dtpStartDatePermResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" ToolTip="View start date permission" DisplayDateFormat="dd/MM/yyyy"
                                    LabelCssClass="" Width="">
                                </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                <ClientEvents OnDateSelected="DateSelected" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqStartDate" runat="server" ControlToValidate="dtpStartDatePerm"
                                Display="None" ErrorMessage="Please select start date" ValidationGroup="EmpPermissionGroup"
                                meta:resourcekey="reqStartDateResource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ExtenderStartDate" runat="server" TargetControlID="reqStartDate"
                                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                            <asp:HiddenField ID="hdnNurdingDay" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblEndDate" runat="server" Text="To" meta:resourcekey="lblEndDateResource1"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <telerik:RadDatePicker ID="dtpEndDatePerm" AllowCustomText="false" MarkFirstMatch="true"
                                Skin="Vista" runat="server" AutoPostBack="True" Culture="en-US" meta:resourcekey="dtpEndDatePermResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" ToolTip="View end date permission" AutoPostBack="True"
                                    DisplayDateFormat="dd/MM/yyyy" LabelCssClass="" Width="">
                                </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqEndDate" runat="server" ControlToValidate="dtpEndDatePerm"
                                Display="None" ErrorMessage="Please select end date" ValidationGroup="EmpPermissionGroup"
                                meta:resourcekey="reqEndDateResource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ExtenderEndDate" runat="server" TargetControlID="reqEndDate"
                                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dtpStartDatePerm"
                                ControlToValidate="dtpEndDatePerm" ErrorMessage="To Date should be greater than or equal to From Date"
                                Display="None" Operator="GreaterThanEqual" Type="Date" ValidationGroup="EmpPermissionGroup"
                                meta:resourcekey="CVDateResource1" />
                            <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender1"
                                runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                </div>

            </asp:Panel>
            <div class="row" id="trNursingFlexibleDurationPermission" runat="server" visible="False">
                <div class="col-md-2" id="Td5" runat="server">
                    <asp:Label ID="lblNursingFlexibleDuration" runat="server" CssClass="Profiletitletxt"
                        Text="Flexible Permission Duration" />
                </div>
                <div class="col-md-4" id="tdNursingFlexibleDurationPermission" runat="server">
                    <telerik:RadComboBox ID="RadCmbFlixebleDuration" MarkFirstMatch="True" Skin="Vista"
                        runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Text="One Hour" Value="60" runat="server" Owner="" />
                            <telerik:RadComboBoxItem Text="Two Hours" Value="120" runat="server" Owner="" />
                        </Items>
                    </telerik:RadComboBox>
                </div>
            </div>
            <div class="row" id="trFullyDay" runat="server">
                <div class="col-md-2" id="Td6" runat="server">
                    <asp:Label ID="lblIsFullyDay" runat="server" Text="Full Day" CssClass="Profiletitletxt"
                        meta:resourcekey="lblIsFullyDayResource1" />
                </div>
                <div class="col-md-4" id="Td7" runat="server">
                    <asp:CheckBox ID="chckFullDay" runat="server" AutoPostBack="True" Text="&nbsp;" />
                </div>
            </div>
            <div class="row" id="trTime" runat="server">
                <div class="col-md-2" id="Td8" runat="server">
                    <asp:Label CssClass="Profiletitletxt" ID="lblTimeFrom" runat="server" Text="Time"
                        meta:resourcekey="lblTimeFromResource1"></asp:Label>
                </div>
                <div id="trTimeFromTo" runat="server">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:RadioButtonList ID="rdlTimeOption" runat="server" RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="rdlTimeOption_OnSelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Text="Specific Time" Value="0" Selected="True" meta:resourcekey="ListItemResource1" />
                                <asp:ListItem Text="Flexible Time" Value="1" meta:resourcekey="ListItemResource2" />
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row" id="trFlixibleTime" runat="server">
                        <div class="col-md-4" id="Td9" runat="server">
                            <telerik:RadMaskedTextBox ID="rmtFlexibileTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                CssClass="RadMaskedTextBox" DisplayMask="##:##" Text='0000' LabelCssClass="">
                                <ClientEvents OnBlur="ValidateTextboxFrom" />
                            </telerik:RadMaskedTextBox>
                            <asp:Label ID="lblhent" runat="server" Text="Hours : Minutes" CssClass="Profiletitletxt" />
                            <asp:RequiredFieldValidator ID="reqFlexibiletime" runat="server" ControlToValidate="rmtFlexibileTime"
                                Display="None" ErrorMessage="Please enter flixible time" ValidationGroup="EmpPermissionGroup"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ExtenderFlexibileTime" runat="server" CssClass="AISCustomCalloutStyle"
                                TargetControlID="reqFlexibiletime" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row" id="trSpecificTime" runat="server">
                        <div class="col-md-2" id="Td10" runat="server">
                            <asp:Label CssClass="Profiletitletxt" ID="lblFrom" Text="From" runat="server" meta:resourcekey="lblFromResource1"></asp:Label>
                        </div>
                        <div class="col-md-4" id="Td11" runat="server">
                            <telerik:RadTimePicker ID="RadTPfromTime" runat="server" AllowCustomText="false"
                                MarkFirstMatch="true" Skin="Vista" AutoPostBack="True" AutoPostBackControl="TimeView"
                                Culture="en-US">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                <TimeView CellSpacing="-1">
                                    <HeaderTemplate>
                                        Time Picker
                                    </HeaderTemplate>
                                    <TimeTemplate>
                                        <a id="A1" runat="server" href="#"></a>
                                    </TimeTemplate>
                                </TimeView>
                                <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                <DateInput ToolTip="View start time" DateFormat="HH:mm" AutoPostBack="True" DisplayDateFormat="HH:mm"
                                    LabelCssClass="" Width="" />
                            </telerik:RadTimePicker>
                            <asp:RequiredFieldValidator ID="reqFromtime" runat="server" ControlToValidate="RadTPfromTime"
                                Display="None" ErrorMessage="Please select start time" ValidationGroup="EmpPermissionGroup"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ExtenderFromTime" runat="server" CssClass="AISCustomCalloutStyle"
                                TargetControlID="reqFromtime" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2" id="Td12" runat="server">
                            <asp:Label CssClass="Profiletitletxt" ID="lblTo" runat="server" Text="To" meta:resourcekey="lblToResource1"></asp:Label>
                        </div>
                        <div class="col-md-3" id="Td13" runat="server">
                            <telerik:RadTimePicker ID="RadTPtoTime" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                Skin="Vista" AutoPostBack="True" AutoPostBackControl="TimeView" Culture="en-US">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                <TimeView CellSpacing="-1">
                                    <HeaderTemplate>
                                        Time Picker
                                    </HeaderTemplate>
                                    <TimeTemplate>
                                        <a id="A2" runat="server" href="#"></a>
                                    </TimeTemplate>
                                </TimeView>
                                <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                <DateInput ToolTip="View end time" DateFormat="HH:mm" AutoPostBack="True" DisplayDateFormat="HH:mm"
                                    LabelCssClass="" Width="" />
                            </telerik:RadTimePicker>
                            <asp:RequiredFieldValidator ID="reqToTime" runat="server" ControlToValidate="RadTPtoTime"
                                Display="None" ErrorMessage="Please select end time" ValidationGroup="EmpPermissionGroup"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ExtenderreqToTime" runat="server" CssClass="AISCustomCalloutStyle"
                                TargetControlID="reqToTime" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                        <div class="col-md-3" id="Td14" runat="server">
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="RadTPtoTime"
                                ClientValidationFunction="validate" Display="None" ValidationGroup="EmpPermissionGroup" />
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="RadTPfromTime"
                                ClientValidationFunction="validate" Display="None" ValidationGroup="EmpPermissionGroup" />
                            <cc1:ValidatorCalloutExtender ID="ExtenderCusToTime" runat="server" CssClass="AISCustomCalloutStyle"
                                TargetControlID="CustomValidator1" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:ValidatorCalloutExtender ID="ExtenderCusFromTime" runat="server" CssClass="AISCustomCalloutStyle"
                                TargetControlID="CustomValidator2" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                            <asp:HiddenField ID="hdnIsvalid" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" id="trDifTime" runat="server">
                <div class="col-md-2" id="Td15" runat="server">
                    <asp:Label CssClass="Profiletitletxt" ID="lblPeriodInterval" runat="server" Text="Period"
                        meta:resourcekey="lblPeriodIntervalResource1"></asp:Label>
                </div>
                <div class="col-md-4" id="trDif" runat="server">
                    <asp:TextBox ID="txtTimeDifference" ReadOnly="True" runat="server" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt"
                        meta:resourcekey="lblAttachFileResource1" />
                </div>
                <div class="col-md-4" id="trAttachedFile" runat="server">
                    <asp:FileUpload ID="fuAttachFile" runat="server" meta:resourcekey="fuAttachFileResource1" />
                    <a id="lnbLeaveFile" target="_blank" runat="server" visible="False">
                        <asp:Label ID="lblView" runat="server" Text="View" meta:resourcekey="lblViewResource1" />
                    </a>
                    <asp:LinkButton ID="lnbRemove" runat="server" Text="Remove" Visible="False" meta:resourcekey="lnbRemoveResource1" />
                    <asp:Label ID="lblNoAttachedFile" runat="server" Text="No Attached File" Visible="False"
                        meta:resourcekey="lblNoAttachedFileResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="lblRemarks" runat="server" Text="Remarks"
                        meta:resourcekey="lblRemarksResource1"></asp:Label>
                </div>
                <div class="col-md-4" id="trRemarks" runat="server">
                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" meta:resourcekey="txtRemarksResource1"></asp:TextBox>
                </div>
            </div>
            <asp:Panel ID="pnlTempHidRows" runat="server" Visible="False" meta:resourcekey="pnlTempHidRowsResource1">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblIsSpecifiedDays" runat="server" Text="Specified days"
                            meta:resourcekey="lblIsSpecifiedDaysResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="chckSpecifiedDays" runat="server" meta:resourcekey="chckSpecifiedDaysResource1" />
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblDays" runat="server" Text="Days" meta:resourcekey="lblDaysResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtDays" runat="server" meta:resourcekey="txtDaysResource1"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblIsFlexible" runat="server" Text="Flexible"
                            meta:resourcekey="lblIsFlexibleResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="chckIsFlexible" runat="server" meta:resourcekey="chckIsFlexibleResource1" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblIsDividable" runat="server" Text="Dividable"
                            meta:resourcekey="lblIsDividableResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="chckIsDividable" runat="server" meta:resourcekey="chckIsDividableResource1" />
                    </div>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnAccept" runat="server" Text="Accept" CssClass="button" meta:resourcekey="btnAcceptResource1" />
                    <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="button" CausesValidation="False"
                        meta:resourcekey="btnRejectResource1" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" CausesValidation="False"
                        meta:resourcekey="btnCancelResource1" />
                </div>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
