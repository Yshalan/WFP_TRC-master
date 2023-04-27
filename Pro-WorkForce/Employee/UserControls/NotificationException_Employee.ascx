<%@ Control Language="VB" AutoEventWireup="false" CodeFile="NotificationException_Employee.ascx.vb" Inherits="Employee_UserControls_NotificationException_Employee" %>
<%@ Register Src="~/Admin/UserControls/MultiEmployeeFilter.ascx" TagName="EmployeeFilter"
    TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script language="javascript" type="text/javascript">
    function CheckBoxListSelect(state) {
        var chkBoxList = document.getElementById("<%= cblEmpList.ClientID %>");
        var chkBoxCount = chkBoxList.getElementsByTagName("input");
        for (var i = 0; i < chkBoxCount.length; i++) {
            chkBoxCount[i].checked = state;
        }
        return false;
    }

    function Refresh() {
        window.location.href = "../Employee/NotificationExceptions_MultiSelection.aspx";

    }

</script>
<div class="row">
    <div class="col-md-2">
        <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" Text="Company" meta:resourcekey="lblCompanyResource1"></asp:Label>
    </div>
    <div class="col-md-4">
        <telerik:RadComboBox ID="RadCmbBxCompanies" runat="server" AutoPostBack="True" CausesValidation="False"
            MarkFirstMatch="True" Skin="Vista" Style="width: 350px" ValidationGroup="EmpPermissionGroup"
            OnSelectedIndexChanged="CompanyChanged" meta:resourcekey="RadCmbBxCompaniesResource1">
        </telerik:RadComboBox>
        <asp:RequiredFieldValidator ID="rfvCompanies" runat="server" ControlToValidate="RadCmbBxCompanies"
            Display="None" InitialValue="--Please Select--" ErrorMessage="Please Select Company" ValidationGroup="EmpNotificationException" meta:resourcekey="rfvCompaniesResource1"></asp:RequiredFieldValidator>
        <cc1:ValidatorCalloutExtender ID="vceCompanies" runat="server" CssClass="AISCustomCalloutStyle"
            Enabled="True" TargetControlID="rfvCompanies" WarningIconImageUrl="~/images/warning1.png">
        </cc1:ValidatorCalloutExtender>
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        <uc3:EmployeeFilter ID="MultiEmployeeFilterUC" runat="server" ShowDirectStaffCheck="true"
            OneventEntitySelected="EntityChanged" OneventWorkGroupSelect="WorkGroupChanged" OneventWorkLocationsSelected="WorkLocationsChanged" />
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
            ValidationGroup="EmpNotificationException" meta:resourcekey="reqFromdateResource1"></asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender ID="ExtenderPermissionDate" runat="server" TargetControlID="reqFromdate"
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
                ValidationGroup="EmpNotificationException" meta:resourcekey="rfvToDateResource1"></asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvToDate"
                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                    Enabled="True">
                </cc1:ValidatorCalloutExtender>
            <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dtpFromdate" ControlToValidate="dtpEndDate"
                ErrorMessage="To Date should be greater than or equal to From Date" Display="None"
                Operator="GreaterThanEqual" Type="Date" ValidationGroup="EmpPermissionGroup" meta:resourcekey="CVDateResource1" /><cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender3"
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
                ValidationGroup="EmpNotificationException" meta:resourcekey="rfvReasonResource1"></asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvReason"
                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                    Enabled="True">
                </cc1:ValidatorCalloutExtender>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <asp:Label ID="lblListOfEmployee" runat="server" CssClass="Profiletitletxt" Text="List Of Employees" meta:resourcekey="lblListOfEmployeeResource1"></asp:Label>
    </div>
    <div class="col-md-4">
        <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; margin-top: 5px; border-radius: 5px">
            <asp:CheckBoxList ID="cblEmpList" runat="server" Style="height: 26px" DataTextField="EmployeeName"
                DataValueField="EmployeeId" meta:resourcekey="cblEmpListResource1">
            </asp:CheckBoxList>
        </div>
    </div>
    <div class="col-md-2">
        <a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size: 8pt">
            <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
        <br />
        <a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size: 8pt">
            <asp:Literal ID="Literal2" runat="server" Text="UnSelect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
    </div>
</div>
<div class="row">
    <div class="col-md-2"></div>
    <div class="col-md-10">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("PageNo") %>' meta:resourcekey="LinkButton1Resource1"></asp:LinkButton>|
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
<br />
<div class="row">
    <div class="col-md-12 text-center">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button"
            ValidationGroup="EmpNotificationException" meta:resourcekey="btnSaveResource1" />
        <asp:Button ID="btnClear" runat="server" Text="Clear"
            CssClass="button" meta:resourcekey="btnClearResource1" />
    </div>
</div>

