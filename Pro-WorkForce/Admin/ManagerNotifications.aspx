<%@ Page Title="" Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="ManagerNotifications.aspx.vb" Inherits="Admin_ManagerNotifications"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="EmployeeFilter" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <uc1:PageHeader ID="PageHeader1" runat="server" />
    </center>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlManagerInfo" runat="server" GroupingText="Manager Information"
                Width="700px" meta:resourcekey="pnlManagerInfoResource1">
                <uc3:EmployeeFilter ID="EmployeeFilter1" runat="server" ShowOnlyManagers="true" ValidationGroup="grpPrint" />
            </asp:Panel>
            <table width="100%">
                <tr>
                    <td width="135px">
                        <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="From Date"
                            meta:resourcekey="Label4Resource1"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="dtpFromdate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                            PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US"
                            meta:resourcekey="dtpFromdateResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEndDate" runat="server" CssClass="Profiletitletxt" Text="End date"
                            meta:resourcekey="lblEndDateResource1" />
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="dtpEndDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                            PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US"
                            meta:resourcekey="dtpEndDateResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:CompareValidator ID="cvDate" runat="server" ControlToCompare="dtpFromdate" ControlToValidate="dtpEndDate"
                            ErrorMessage="End Date should be greater than or equal to From Date" Display="None"
                            Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpPrint" meta:resourcekey="cvDateResource1" />
                        <cc1:ValidatorCalloutExtender TargetControlID="cvDate" ID="vceDate" runat="server"
                            Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="button" ValidationGroup="grpPrint"
                            meta:resourcekey="btnPrintResource1" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div id="divTableHTML" runat="server">
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
