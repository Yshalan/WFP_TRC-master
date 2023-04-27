<%@ Page Language="VB" AutoEventWireup="false" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    CodeFile="TAPolicyMassUpdate.aspx.vb" Inherits="Admin_TAPolicyMassUpdate" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/UserSecurityFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<%@ Register Src="../Admin/UserControls/MultiEmployeeFilter.ascx" TagName="MultiEmployeeFilter"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function CheckBoxListSelect(state) {
            var chkBoxList = document.getElementById("<%= cblEmpList.ClientID %>");
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        } 
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="TA Policy Mass Update" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblCompanyResource1"
                        Text="Company"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCmbBxCompanies" runat="server" AllowCustomText="false"
                        AutoPostBack="true" CausesValidation="false" MarkFirstMatch="true" Skin="Vista"
                        ValidationGroup="EmpPermissionGroup">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvCompanies" runat="server" ControlToValidate="RadCmbBxCompanies"
                        Display="None" ErrorMessage="Please Select Company" meta:resourcekey="rfvCompaniesResource1"
                        ValidationGroup="grpSave"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceCompanies" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvCompanies" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <%--<uc2:PageFilter ID="EmployeeFilterUC" runat="server" OneventCompanySelect="CompanyChanged"
                            OneventEntitySelected="FillEmployee" />--%>
                    <uc4:MultiEmployeeFilter ID="MultiEmployeeFilterUC" runat="server" OneventEntitySelected="EntityChanged"
                        OneventWorkGroupSelect="WorkGroupChanged" OneventWorkLocationsSelected="WorkLocationsChanged"
                        ShowOtherCompany="false" ValidationGroup="grpSave" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblTAPolicy" runat="server" CssClass="Profiletitletxt" Text="TA Policy"
                        meta:resourcekey="lblTAPolicyResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCmbBxPolicies" MarkFirstMatch="True" Skin="Vista" runat="server"
                        meta:resourcekey="RadCmbBxPoliciesResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvPolicies" ValidationGroup="grpSave" InitialValue="--Please Select--"
                        runat="server" ControlToValidate="RadCmbBxPolicies" Display="None" ErrorMessage="Please Select Policy"
                        meta:resourcekey="rfvPoliciesResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcePolicies" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvPolicies" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblFromDate" runat="server" CssClass="Profiletitletxt" Text="From Date"
                        meta:resourcekey="lblFromDateResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpFromdate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                        PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="English (United States)"
                        meta:resourcekey="dtpFromdateResource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chckTemporary" runat="server" AutoPostBack="True" Text="Is Temporary"
                        meta:resourcekey="lblTemporaryResource1" />
                </div>
            </div>
            <div class="row" id="trEndDate" runat="server" visible="False">
                <div class="col-md-2">
                    <asp:Label ID="lblEndDate" runat="server" CssClass="Profiletitletxt" Text="End date"
                        meta:resourcekey="lblEndDateResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpEndDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                        PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="English (United States)">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dtpFromdate" ControlToValidate="dtpEndDate"
                        ErrorMessage="End Date should be greater than or equal to From Date" Display="None"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpSave" meta:resourcekey="CVDateResource1"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender2"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="List Of Employees"
                        meta:resourcekey="Label5Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px;
                        border-color: #ccc; margin-top: 5px; border-radius: 5px">
                        <asp:CheckBoxList ID="cblEmpList" runat="server" Style="height: 26px" DataTextField="EmployeeName"
                            DataValueField="EmployeeId" meta:resourcekey="cblEmpListResource1">
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="col-md-2">
                    <a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size: 8pt">
                        <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                    <a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size: 8pt">
                        <asp:Literal ID="Literal2" runat="server" Text="UnSelect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-10">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<% # Eval("PageNo")%>'></asp:LinkButton>|
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Assign TA Policy" ValidationGroup="grpSave"
                        CssClass="button" meta:resourcekey="btnSaveResource1" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
