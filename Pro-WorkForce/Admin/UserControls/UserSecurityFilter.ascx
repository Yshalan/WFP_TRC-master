<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserSecurityFilter.ascx.vb"
    Inherits="Admin_UserControls_UserSecurityFilter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    .style1
    {
        width: 100%;
    }

    .style2
    {
        height: 80px;
    }
</style>
<script type="text/javascript" language="javascript">
    function validateEmployeeComp() {
        if (Page_ClientValidate('validateEmployeeComp') && Page_ClientValidate("ValidateComp")) {
            return true;
        }
        return false;
    }
</script>

<div id="trCompany" runat="server" class="row">
    <div class="col-md-2">
        <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text="Company" meta:resourcekey="Label1Resource1"></asp:Label>
    </div>
    <div class="col-md-4">
        <telerik:RadComboBox ID="RadCmbBxCompanies" AutoPostBack="true" AllowCustomText="false"
            MarkFirstMatch="true" Filter="Contains" Skin="Vista" runat="server" CausesValidation="false"
            Style="width: 350px">
        </telerik:RadComboBox>
        <asp:RequiredFieldValidator ID="rfvCompanies" InitialValue="--Please Select--" runat="server"
            ControlToValidate="RadCmbBxCompanies" Display="None" ErrorMessage="Please Select Company"
            meta:resourcekey="rfvCompaniesResource1"></asp:RequiredFieldValidator>
        <cc1:ValidatorCalloutExtender ID="vceCompanies" runat="server" CssClass="AISCustomCalloutStyle"
            TargetControlID="rfvCompanies" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
        </cc1:ValidatorCalloutExtender>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <asp:Label ID="lblLevels" runat="server" Text="Entity" CssClass="Profiletitletxt"
            meta:resourcekey="lblLevelsResource1"></asp:Label>
    </div>
    <div class="col-md-4">
        <telerik:RadComboBox ID="RadCmbBxEntity" AllowCustomText="false" Filter="Contains"
            MarkFirstMatch="true" Skin="Vista" runat="server" AutoPostBack="true" ValidationGroup="ValidateLevels"
            CausesValidation="false" Style="width: 350px">
        </telerik:RadComboBox>
        <asp:RequiredFieldValidator ID="rfvEntity" runat="server" ControlToValidate="RadCmbBxEntity"
            Display="None" ErrorMessage="Please Select Entity" meta:resourcekey="rfvEntityResource1"></asp:RequiredFieldValidator>
        <cc1:ValidatorCalloutExtender ID="vceEntity" runat="server" CssClass="AISCustomCalloutStyle"
            TargetControlID="rfvEntity" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
        </cc1:ValidatorCalloutExtender>
    </div>
    <div class="col-md-3">
        <asp:CheckBox ID="chkDirectStaff" runat="server" Text="Direct Staff Only" Visible="false" meta:resourcekey="chkDirectStaffResource1" />
    </div>
</div>
