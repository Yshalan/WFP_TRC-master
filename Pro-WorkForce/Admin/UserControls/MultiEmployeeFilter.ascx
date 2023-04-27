<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MultiEmployeeFilter.ascx.vb"
    Inherits="Admin_UserControls_MultiEmployeeFilter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="row">
    <div class="col-md-8">
        <asp:RadioButtonList ID="rlsSearchCriteria" runat="server" RepeatDirection="Horizontal"
            AutoPostBack="True" meta:resourcekey="rlsSearchCriteriaResource1">
            <asp:ListItem Text="Entity" Value="1" meta:resourcekey="ListItemResource1" />
            <asp:ListItem Text="Work Group" Value="2" meta:resourcekey="ListItemResource2" />
            <asp:ListItem Text="Work Locations" Value="3" meta:resourcekey="ListItemResource3" />
            <asp:ListItem Text="Employee Number" Value="4" meta:resourcekey="ListItemResource4" />
        </asp:RadioButtonList>
    </div>
</div>
<div class="row" id="trCompany" runat="server">
    <div class="col-md-2">
        <asp:Label ID="lblCompany" runat="server" Text="Company" CssClass="Profiletitletxt"
            meta:resourcekey="lblCompanyResource1" />
    </div>
    <div class="col-md-4">
        <telerik:RadComboBox ID="RadCmbBxCompany" AllowCustomText="false" Filter="Contains"
            MarkFirstMatch="true" Skin="Vista" runat="server" AutoPostBack="true" />
    </div>
</div>
<div class="row" id="trEntity" runat="server">
    <div class="col-md-2">
        <asp:Label ID="lblEntity" runat="server" Text="Entity" CssClass="Profiletitletxt"
            meta:resourcekey="lblEntityResource1" />
    </div>
    <div class="col-md-4">
        <telerik:RadComboBox ID="RadCmbBxEntity" AllowCustomText="false" Filter="Contains"
            MarkFirstMatch="true" Skin="Vista" runat="server" AutoPostBack="true" />
    </div>
    <div class="col-md-3">
        <asp:CheckBox ID="chkDirectStaff" runat="server" Text="Direct Staff Only" Visible="false" meta:resourcekey="chkDirectStaffResource1" />
    </div>
</div>
<div class="row" id="trWorkGroup" runat="server">
    <div class="col-md-2">
        <asp:Label ID="lblWorkGroup" runat="server" CssClass="Profiletitletxt" Text="Work Group"
            meta:resourcekey="lblWorkGroupResource1" />
    </div>
    <div class="col-md-4">
        <telerik:RadComboBox ID="RadCmbBxWorkGroup" AllowCustomText="false" Filter="Contains"
            MarkFirstMatch="true" Skin="Vista" runat="server" AutoPostBack="true" />
    </div>
</div>
<div class="row" id="trWorkLocation" runat="server">
    <div class="col-md-2">
        <asp:Label ID="lblWorkLocations" runat="server" Text="Work Locations" CssClass="Profiletitletxt"
            meta:resourcekey="lblWorkLocationsResource1" />
    </div>
    <div class="col-md-4">
        <telerik:RadComboBox ID="RadCmbBxWorkLocations" AllowCustomText="false" Filter="Contains"
            MarkFirstMatch="true" Skin="Vista" runat="server" AutoPostBack="true" />
    </div>
</div>
<div class="row" id="trEmployee" runat="server">
    <div class="col-md-2">
        <asp:Label ID="lblEmpNo" runat="server" Text="Employee Number" CssClass="Profiletitletxt"
            meta:resourcekey="lblEmpNoResource1" />
    </div>
    <div class="col-md-4">
        <asp:TextBox ID="txtEmpNo" runat="server" meta:resourcekey="txtEmpNoResource1" />
        <asp:RegularExpressionValidator ID="revEmpNoSplicalChar" ErrorMessage="not allow splical characters"
            ControlToValidate="txtEmpNo" ValidationGroup="validateEmployee" Display="None"
            Enabled="false" ValidationExpression="^[a-zA-Z0-9]*$" runat="server" meta:resourcekey="revEmpNoSplicalCharResource1" />
        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="true"
            TargetControlID="revEmpNoSplicalChar">
        </cc1:ValidatorCalloutExtender>
        <asp:RegularExpressionValidator Display="None" ControlToValidate="txtEmpNo" ID="RegularExpressionValidator2"
            ValidationExpression="^{0,50}$" runat="server" ErrorMessage="Maximum 50 characters allowed."
            ValidationGroup="ReligionGroup"></asp:RegularExpressionValidator>
        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="true"
            TargetControlID="RegularExpressionValidator2">
        </cc1:ValidatorCalloutExtender>
        <asp:RequiredFieldValidator ID="rfvEmpNo" runat="server" ControlToValidate="txtEmpNo"
            Display="None" ErrorMessage="Please enter Emp No." ValidationGroup="validateEmployee"
            meta:resourcekey="RequiredFieldValidator2Resource1" />
        <cc1:ValidatorCalloutExtender ID="vceEmpNo" runat="server" Enabled="True" TargetControlID="rfvEmpNo">
        </cc1:ValidatorCalloutExtender>
    <%--    <asp:RegularExpressionValidator ID="ReqValContactPerson_SpecialChars" runat="server"
            CssClass="changecolor" ControlToValidate="txtEmpNo" Display="None" ErrorMessage="Special characters not allowed."
            SetFocusOnError="True" ValidationExpression="[^~`!@#$%^&*()+=|}]{['&quot;:?.>,<]+" ValidationGroup="validateEmployee"
            meta:resourcekey="revEmpNoSplicalCharResource1"></asp:RegularExpressionValidator>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
            TargetControlID="ReqValContactPerson_SpecialChars">
        </cc1:ValidatorCalloutExtender>--%>
        <%--"^[\s\S]{0,50}$"--%><%--"[^~`!@#$%\^&\*\(\)\+=\\\|\}\]\{\['&quot;:?.>,</]+"--%>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
    </div>
    <div class="col-md-4">
        <asp:Button ID="btnRetrieve" runat="server" Text="Retrieve" CssClass="button" ValidationGroup="validateEmployee"
            meta:resourcekey="btnRetrieveResource1" />
    </div>
</div>
