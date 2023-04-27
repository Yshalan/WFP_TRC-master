<%@ Control Language="VB" AutoEventWireup="false" CodeFile="NotificationException_LogicalGroup.ascx.vb" Inherits="Employee_UserControls_NotificationException_LogicalGroup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script language="javascript" type="text/javascript">

    function Refresh() {
        window.location.href = "../Employee/NotificationExceptions_MultiSelection.aspx";

    }

</script>

<div class="row">
    <div class="col-md-2">
        <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" Text="Company" Visible="False" meta:resourcekey="lblCompanyResource1"></asp:Label>
    </div>
    <div class="col-md-4">
        <telerik:RadComboBox ID="RadCmbBxCompanies" runat="server" AutoPostBack="True" CausesValidation="False"
            MarkFirstMatch="True" Skin="Vista" Style="width: 350px" OnSelectedIndexChanged="CompanyChanged" Visible="False" meta:resourcekey="RadCmbBxCompaniesResource1">
        </telerik:RadComboBox>
        <asp:RequiredFieldValidator ID="rfvCompanies" runat="server" ControlToValidate="RadCmbBxCompanies"
            Display="None" InitialValue="--Please Select--" ErrorMessage="Please Select Company" ValidationGroup="LGNotificationException" Enabled="False" meta:resourcekey="rfvCompaniesResource1"></asp:RequiredFieldValidator>
        <cc1:ValidatorCalloutExtender ID="vceCompanies" runat="server" CssClass="AISCustomCalloutStyle"
            Enabled="True" TargetControlID="rfvCompanies" WarningIconImageUrl="~/images/warning1.png">
        </cc1:ValidatorCalloutExtender>
    </div>
</div>

<div class="row">
    <div class="col-md-2">
        <asp:Label ID="lblLogicalGroup" runat="server" CssClass="Profiletitletxt" Text="Logical Group" meta:resourcekey="lblLogicalGroupResource1"></asp:Label>
    </div>
    <div class="col-md-4">
        <telerik:RadComboBox ID="RadCmbBxLogicalGroup" runat="server" AutoPostBack="True" CausesValidation="False"
            MarkFirstMatch="True" Skin="Vista" Style="width: 350px" meta:resourcekey="RadCmbBxLogicalGroupResource1">
        </telerik:RadComboBox>
        <asp:RequiredFieldValidator ID="rfvLogicalGroup" runat="server" ControlToValidate="RadCmbBxLogicalGroup"
            Display="None" InitialValue="--Please Select--" ErrorMessage="Please Select Logical Group" ValidationGroup="LGNotificationException" meta:resourcekey="rfvLogicalGroupResource1"></asp:RequiredFieldValidator>
        <cc1:ValidatorCalloutExtender ID="vceLogicalGroup" runat="server" CssClass="AISCustomCalloutStyle"
            Enabled="True" TargetControlID="rfvLogicalGroup" WarningIconImageUrl="~/images/warning1.png">
        </cc1:ValidatorCalloutExtender>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <asp:Label ID="lblFromDate" runat="server" CssClass="Profiletitletxt"
            Text="From Date" meta:resourcekey="lblFromDateResource1"></asp:Label>
    </div>
    <div class="col-md-4">
        <telerik:RadDatePicker ID="dtpFromdate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
            PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
            Culture="en-US" meta:resourcekey="dtpFromdateResource1">
            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True"></Calendar>
            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                Width="" LabelWidth="64px">
                <EmptyMessageStyle Resize="None" />
                <ReadOnlyStyle Resize="None" />
                <FocusedStyle Resize="None" />
                <DisabledStyle Resize="None" />
                <InvalidStyle Resize="None" />
                <HoveredStyle Resize="None" />
                <EnabledStyle Resize="None" />
            </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
        </telerik:RadDatePicker>
        <asp:RequiredFieldValidator ID="reqFromdate" runat="server" ControlToValidate="dtpFromdate"
            Display="None" ErrorMessage="Please select from date"
            ValidationGroup="LGNotificationException" meta:resourcekey="reqFromdateResource1"></asp:RequiredFieldValidator>
        <cc1:ValidatorCalloutExtender ID="ExtenderPermissionDate" runat="server" TargetControlID="reqFromdate"
            CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
            Enabled="True">
        </cc1:ValidatorCalloutExtender>
    </div>
</div>
<div class="row">
    <div class="col-md-2"></div>
    <div class="col-md-4">
        <asp:CheckBox ID="chckTemporary" runat="server" AutoPostBack="True"
            Text="Is Temporary" meta:resourcekey="chckTemporaryResource1" />
    </div>
</div>
<asp:Panel ID="pnlEndDate" runat="server" Visible="False" meta:resourcekey="pnlEndDateResource1">
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblEndDate" runat="server" CssClass="Profiletitletxt"
                Text="End date" meta:resourcekey="lblEndDateResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadDatePicker ID="dtpEndDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                Culture="en-US" meta:resourcekey="dtpEndDateResource1">
                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True"></Calendar>
                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                    Width="" LabelWidth="64px">
                    <EmptyMessageStyle Resize="None" />
                    <ReadOnlyStyle Resize="None" />
                    <FocusedStyle Resize="None" />
                    <DisabledStyle Resize="None" />
                    <InvalidStyle Resize="None" />
                    <HoveredStyle Resize="None" />
                    <EnabledStyle Resize="None" />
                </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="dtpEndDate"
                Display="None" ErrorMessage="Please select to date"
                ValidationGroup="LGNotificationException" meta:resourcekey="rfvToDateResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvToDate"
                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                Enabled="True">
            </cc1:ValidatorCalloutExtender>
            <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dtpFromdate" ControlToValidate="dtpEndDate"
                ErrorMessage="To Date should be greater than or equal to From Date" Display="None"
                Operator="GreaterThanEqual" Type="Date" ValidationGroup="EmpPermissionGroup" meta:resourcekey="CVDateResource1" />
            <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender3"
                runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
</asp:Panel>
<div class="row">
    <div class="col-md-2">
        <asp:Label ID="lblReason" runat="server" Text="Reason"
            CssClass="Profiletitletxt" meta:resourcekey="lblReasonResource1" />
    </div>
    <div class="col-md-4">
        <asp:TextBox ID="txtReason" runat="server" Rows="4" Columns="45"
            TextMode="MultiLine" meta:resourcekey="txtReasonResource1" /><asp:RequiredFieldValidator ID="rfvReason" runat="server" ControlToValidate="txtReason"
                Display="None" ErrorMessage="Please enter reason"
                ValidationGroup="LGNotificationException" meta:resourcekey="rfvReasonResource1"></asp:RequiredFieldValidator>
        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvReason"
            CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
            Enabled="True">
        </cc1:ValidatorCalloutExtender>
    </div>
</div>

<br />
<div class="row">
    <div class="col-md-12 text-center">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button"
            ValidationGroup="LGNotificationException" meta:resourcekey="btnSaveResource1" />
        <asp:Button ID="btnClear" runat="server" Text="Clear"
            CssClass="button" meta:resourcekey="btnClearResource1" />
    </div>
</div>
