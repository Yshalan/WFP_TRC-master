<%@ Page Title="DashBoard" Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="DashBoard.aspx.vb" Inherits="Admin_DashBoard"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Employee/UserControls/Emp_DashBoard.ascx" TagName="Emp_DashBoard"
    TagPrefix="uc2" %>
<%@ Register Src="../Admin/UserControls/UserSecurityFilter.ascx" TagName="UserSecurityFilter"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 215px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <uc3:PageHeader ID="PageHeader1" runat="server" />
    </center>
    <br />
    <asp:UpdatePanel ID="pnlFilter" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td colspan="2">
                        <uc1:UserSecurityFilter ID="UserSecurityFilter1" runat="server" ValidationGroup="grpSave" />
                    </td>
                </tr>
                <tr>
                    <td width="138px">
                        <asp:Label ID="lbldate" runat="server" Text="From Date" Class="Profiletitletxt" meta:resourcekey="lbldateResource1"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="rdpDate" runat="server" Culture="English (United States)"
                            meta:resourcekey="RadDatePicker1Resource1" Width="180px">
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
                    <td width="138px">
                        <asp:Label ID="lblToDate" runat="server" Text="To Date" Class="Profiletitletxt" meta:resourcekey="lblToDateResource1"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="rdpToDate" runat="server" Culture="English (United States)"
                            meta:resourcekey="RadDatePicker1Resource1" Width="180px">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="rdpDate" ControlToValidate="rdpToDate"
                            ErrorMessage="End Date should be greater than or equal to From Date" Display="None"
                            Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpSave" meta:resourcekey="CVDateResource1"></asp:CompareValidator>
                        <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender2"
                            runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <asp:Button runat="server" ID="btnRetrive" Text="Retrieve" ValidationGroup="grpSave"
                            CssClass="button" meta:resourcekey="btnRetriveResource1" />
                    </td>
                </tr>
            </table>
            <div style: align="center">
                <uc2:Emp_DashBoard ID="Emp_DashBoard1" runat="server" />
                <asp:Label ID="lbldash" runat="server" Text="There is no Employees in your selection"
                    Visible="false"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
