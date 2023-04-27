<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EmployeeFilter.ascx.vb"
    Inherits="Admin_UserControls_EmployeeFilter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    .style1 {
        width: 100%;
    }

    .style2 {
        height: 80px;
    }
</style>
<script type="text/javascript" language="javascript">
    function openRadWin(ctrl_id) {
        oWindow = radopen('../admin/EmployeeSearch.aspx?SourceControlId=' + ctrl_id, "RadWindow1");
    }


    function EntityClick() {
        var hdnIsEntityClick = document.getElementById("<%= hdnIsEntityClick.ClientID %>");
        hdnIsEntityClick.value = 'True';
    }

    //    function PageReload() {
    //        window.document.location.reload();
    //    }

</script>
<asp:UpdatePanel ID="upEmpFilter" runat="server">
    <ContentTemplate>
        <%--  <asp:HiddenField ID="hdnSearchEmployeeId" runat="server" />
        <asp:HiddenField ID="hdnSearchCompanyId" runat="server" />
        <asp:HiddenField ID="hdnSearchEmpNo" runat="server" />--%>
        <div class="row">
            <div class="col-md-8">
                <asp:RadioButtonList ID="rblSearchType" runat="server" CssClass="Profiletitletxt"
                    RepeatDirection="Horizontal" AutoPostBack="True">
                    <asp:ListItem Value="C" Selected="True" meta:resourcekey="lstCompanyResource1">Company</asp:ListItem>
                    <asp:ListItem Value="W" meta:resourcekey="lstWLocationResource1">Work Location</asp:ListItem>
                    <asp:ListItem Value="L" meta:resourcekey="lstLGroupResource1">Logical Group</asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <div id="trcompany" runat="server" class="row">
            <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text="Company" meta:resourcekey="Label1Resource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <telerik:RadComboBox ID="RadCmbBxCompanies" AutoPostBack="true" AllowCustomText="false"
                    CausesValidation="false" Filter="Contains" MarkFirstMatch="true" Skin="Vista"
                    runat="server" ValidationGroup="ValidateComp" Style="width: 350px">
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="rfvCompanies" runat="server" ControlToValidate="RadCmbBxCompanies"
                    Display="None" ErrorMessage="Please Select Company" ValidationGroup="ValidateComp"
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
                <%--<asp:DropDownList ID="RadCmbBxEntity" runat="server" AutoPostBack="true" ValidationGroup="ValidateLevels"
                             Style="width: 350px">
                        </asp:DropDownList>--%>
                <telerik:RadComboBox ID="RadCmbBxEntity" AllowCustomText="false" Filter="Contains"
                    EnableScreenBoundaryDetection="false" MarkFirstMatch="true" Skin="Vista" runat="server"
                    AutoPostBack="true" ValidationGroup="ValidateLevels" Style="width: 350px">
                </telerik:RadComboBox>
                <asp:HiddenField ID="hdnIsEntityClick" runat="server" />
                <asp:RequiredFieldValidator ID="rfvEntity" runat="server" ControlToValidate="RadCmbBxEntity"
                    InitialValue="--Please Select--" Display="None" ErrorMessage="Please Select Entity"
                    meta:resourcekey="rfvEntityResource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="vceEntity" runat="server" CssClass="AISCustomCalloutStyle"
                    TargetControlID="rfvEntity" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                </cc1:ValidatorCalloutExtender>
            </div>
            <div class="col-md-3">
                <asp:CheckBox ID="chkDirectStaff" runat="server" Text="Direct Staff Only" Visible="false" meta:resourcekey="chkDirectStaffResource1" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblEmployees" runat="server" Text="Employee" CssClass="Profiletitletxt"
                    meta:resourcekey="lblEmployeesResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <telerik:RadComboBox ID="RadCmbBxEmployees" AllowCustomText="false" Filter="Contains"
                    EnableScreenBoundaryDetection="false" MarkFirstMatch="true" Skin="Vista" runat="server"
                    AutoPostBack="true" ValidationGroup="ValidateLevels" Style="width: 350px">
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" ControlToValidate="RadCmbBxEmployees"
                    InitialValue="--Please Select--" Display="None" ErrorMessage="Please Select Employee"
                    meta:resourcekey="rfvEmployeeResource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                    TargetControlID="rfvEmployee" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                </cc1:ValidatorCalloutExtender>
            </div>
        </div>
        <div class="row" runat="server" id="EmployeestxtBox">
            <div class="col-md-2">
                <asp:Label ID="lblEmployeestxt" runat="server" Text="Employee" CssClass="Profiletitletxt"
                    meta:resourcekey="lblEmployeesResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtEmployee" runat="server" meta:resourcekey="TxtEmpNoResource1"
                    Width="350px"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblEmpNo" runat="server" Text="Emp No." CssClass="Profiletitletxt"
                    meta:resourcekey="lblEmpNoResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="TxtEmpNo" runat="server" meta:resourcekey="TxtEmpNoResource1" AutoPostBack="false"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="TxtEmpNo" ID="RegularExpressionValidator1"
                    ValidationExpression="^{0,50}$"  runat="server" ErrorMessage="Maximum 50 characters allowed."
                    ValidationGroup="validateEmployeeComp"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="revTxtEmpNo" Display="Dynamic" ValidationGroup="validateEmployeeComp"
                    runat="server" ErrorMessage="Special Characters are not allowed!" ValidationExpression="[^~`!@#$%^&*()+=|}]{['&quot;:?.>,<]+" 
                    ControlToValidate="TxtEmpNo">
                </asp:RegularExpressionValidator>
                <%--"^[\s\S]{0,50}$"--%><%--"[^~`!@#$%\^&\*\(\)\+=\\\|\}\]\{\['&quot;:?.>,</]+"--%>
                <asp:RequiredFieldValidator ID="rfvtxtEmpNo" runat="server" ControlToValidate="TxtEmpNo"
                    Display="None" ErrorMessage="Please Select Employee" Enabled="false" meta:resourcekey="rfvEmployeeResource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" CssClass="AISCustomCalloutStyle"
                    TargetControlID="rfvtxtEmpNo" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                </cc1:ValidatorCalloutExtender>
                <%--<asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Retrieve" CausesValidation="False"
                    OnClientClick="validateEmployeeComp()" meta:resourcekey="btnSearchResource1" />--%>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Retrieve" CausesValidation="False"
                    meta:resourcekey="btnSearchResource1" />
                <asp:Button ID="btnSearchEmployee" runat="server" CssClass="button" Text="Search"
                    CausesValidation="False" meta:resourcekey="btnSearchEmployeeResource1"
                    OnClientClick="openRadWin(this.id); return false" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtEmpNo"
                    Display="None" ErrorMessage="Please Select Emp No." ValidationGroup="validateEmployeeComp"
                    meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                    TargetControlID="RequiredFieldValidator2">
                </cc1:ValidatorCalloutExtender>
            </div>
        </div>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Behavior="Default"
            EnableShadow="True" InitialBehavior="None">
            <Windows>
                <telerik:RadWindow ID="RadWindow1" runat="server" Animation="FlyIn" Behavior="Close, Move, Resize"
                    Behaviors="Close, Move, Resize" EnableShadow="True" Height="530px" IconUrl="~/images/HeaderWhiteChrome.jpg"
                    InitialBehavior="None" ShowContentDuringLoad="False" VisibleStatusbar="False"
                    Width="700px">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <asp:HiddenField ID="hdnEmployeeId" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
