<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" CodeFile="About.aspx.vb" Inherits="Admin_About"
    Theme="SvTheme" UICulture="auto" Culture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <div class="Svpanel">
        <div class="row">
            <div class="col-md-4">
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblLicneseDetails" runat="server" Text="Licnese Details" meta:resourcekey="lblLicneseDetailsResource1" Font-Underline="true"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblCustomerDetails" runat="server" Text="Customer Details" meta:resourcekey="lblCustomerDetailsResource1" Font-Underline="true"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name" meta:resourcekey="lblCustomerNameResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblCustomerNameVal" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Label ID="lblNoOfEmployees" runat="server" Text="No. of Employees" meta:resourcekey="lblNoOfEmployeesResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblNoOfEmployeesVal" runat="server"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblActualNoOfEmployeesVal" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Label ID="lblNoOfUsers" runat="server" Text="No. of Users" meta:resourcekey="lblNoOfUsersResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblNoOfUsersVal" runat="server"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblActualNoOfUsersVal" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Label ID="lblNoOfCompanies" runat="server" Text="No. of Company(s)" meta:resourcekey="lblNoOfCompaniesResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblNoOfCompaniesVal" runat="server"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblActualNoOfCompaniesVal" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row" id="dvMobileDevices" runat="server" visible="false">
            <div class="col-md-4">
                <asp:Label ID="lblNoOfMobileDevices" runat="server" Text="No. of Mobile Devices" meta:resourcekey="lblNoOfMobileDevicesResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblNoOfMobileDevicesVal" runat="server"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblActualNoOfMobileDevicesVal" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row" id="dvMobileWorkLocation" runat="server" visible="false">
            <div class="col-md-4">
                <asp:Label ID="lblNoOfMobileWorkLocations" runat="server" Text="No. of Mobile Work Locations" meta:resourcekey="lblNoOfMobileWorkLocationsResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblNoOfMobileWorkLocationsVal" runat="server"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblActualNoOfMobileWorkLocationsVal" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Label ID="lblEndDate" runat="server" Text="Support End Date" meta:resourcekey="lblEndDateResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblEndDateVal" runat="server"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblActualEndDateVal" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Label ID="lblReleasNo" runat="server" Text="Release No." meta:resourcekey="lblReleasNoResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblReleasNoVal" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Label ID="lblMacAddress" runat="server" Text="MAC Address" meta:resourcekey="lblMacAddressResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblMacAddressVal" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Label ID="lblDVC" runat="server" Text="DVC" ToolTip="Deployment Verification Code" meta:resourcekey="lblDVCResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblDVCVal" runat="server" ForeColor="Green" Font-Size="9pt"></asp:Label>
            </div>
        </div>
    </div>
    <br />
    <br />
    <div class="row">
        <div class="col-md-12 text-center">
            <asp:Label ID="lblContactUs" runat="server" Text="ContactUs" meta:resourcekey="lblContactUsResource1"></asp:Label>
        </div>
    </div>
    <br />
    <br />
    <div class="row">
        <div class="col-md-12 text-center">
            <asp:Label ID="lblChangeLicenseKey" runat="server" Text="Change License Key" meta:resourcekey="lblChangeLicenseKeyResource1"></asp:Label>
            <asp:TextBox ID="txtChangeLicneseKey" runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvChangeLicenseKey"
                runat="server" ControlToValidate="txtChangeLicneseKey" Display="None" ErrorMessage="Please Enter Product Key"
                ValidationGroup="grpSubmit" meta:resourcekey="rfvChangeLicenseKeyResource1"
                SkinID="RequiredFieldValidator1">
            </asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vceChangeLicenseKey" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="rfvChangeLicenseKey" WarningIconImageUrl="~/images/warning1.png"
                Enabled="True">
            </cc1:ValidatorCalloutExtender>

        </div>
    </div>
    <div class="row">
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="grpSubmit" meta:resourcekey="btnSubmitResource1" />
        </div>
    </div>

</asp:Content>

