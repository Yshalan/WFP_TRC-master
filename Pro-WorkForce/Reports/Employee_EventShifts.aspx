<%@ Page Title="" Language="VB" MasterPageFile="~/Default/ArabicMaster.master" AutoEventWireup="false"  StylesheetTheme="Default"
    CodeFile="Employee_EventShifts.aspx.vb" Inherits="Reports_Employee_EventShifts"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="Filter" runat="server">
            <center>
                <table style="width: 594px; border-width: 0px; background-color: #FFFFFF; visibility: hidden;"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="3">
                            <div id="logo_div">
                                <a href="../Default/logout.aspx">
                                    <div id="logout2">
                                    </div>
                                </a>
                                <div style="text-align: left">
                                    <a href="../Default/Home.aspx">
                                        <img src="../images/logo.jpg" alt="smart time" /></a>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </center>
            <table>
                <tr>
                    <td>
                        <uc1:PageHeader ID="lblReportTitle" runat="server" />
                        <div dir="<%=dir %>">
                            <table style="text-align: <%= iif(dir="ltr","left","right")%>; width: 450px">
                                <tr>
                                    <td>
                                        <table style="background-color: #fff">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEvent" runat="server" Text="Event\Project" AutoPostBack="true"
                                                        CssClass="Profiletitletxt" meta:resourcekey="lblEventResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="RadCmbBxEvent" MarkFirstMatch="True" AutoPostBack="True"
                                                        Skin="Vista" runat="server" meta:resourcekey="RadCmbBxEventResource1">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblGroup" runat="server" Text="Logical Group Name" AutoPostBack="true"
                                                        CssClass="Profiletitletxt" meta:resourcekey="lblGroupResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="RadCmbBxGroup" MarkFirstMatch="True" AutoPostBack="True"
                                                        Skin="Vista" runat="server" meta:resourcekey="RadCmbBxGroupResource1">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEmployee" runat="server" Text="Employee" AutoPostBack="true" CssClass="Profiletitletxt"
                                                        meta:resourcekey="lblEmployeeResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="RadCmbBxEmployee" MarkFirstMatch="True" Skin="Vista" runat="server"
                                                        meta:resourcekey="RadCmbBxEmployeeResource1">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="138px">
                                                    <asp:Label ID="lblFromDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblFromDateResource1"
                                                        Text="From Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Culture="en-US" meta:resourcekey="RadDatePicker1Resource1"
                                                        Width="180px">
                                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                        </Calendar>
                                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                                            Width="">
                                                        </DateInput>
                                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="RadDatePicker1"
                                                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator9_ValidatorCalloutExtender"
                                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="138px">
                                                    <asp:Label ID="lblToDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblToDateResource1"
                                                        Text="To Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="RadDatePicker2" runat="server" Culture="en-US" meta:resourcekey="RadDatePicker2Resource1"
                                                        Width="180px">
                                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                        </Calendar>
                                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                                            Width="">
                                                        </DateInput>
                                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadDatePicker2"
                                                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                                        TargetControlID="RequiredFieldValidator1">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="RadDatePicker1"
                                                        ControlToValidate="RadDatePicker2" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                                                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="Grpfind" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                                                    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender2"
                                                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rblFormat" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt"
                                                        meta:resourcekey="rblFormatResource1">
                                                        <asp:ListItem Text="PDF" Value="1" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr id="trControls" runat="server" align="center">
                                                <td runat="server">
                                                </td>
                                                <td runat="server">
                                                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="button" meta:resourcekey="Button1Resource1"
                                                        ValidationGroup="btnPrint" />
                                                </td>
                                                <td runat="server">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width: 100%; height: 44px;">
                        </div>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="Report" runat="server">
            <table align="center" style="background-color: #fff; width: 1024px; height: 44px;">
                <tr>
                    <td align="center" style="border-left: 1px; border-right: 1px">
                        <CR2:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="True" SeparatePages="False"
                            GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False"
                            meta:resourcekey="CRVResource1" EnableDrillDown="False" GroupTreeImagesFolderUrl=""
                            HasGotoPageButton="False" HasPageNavigationButtons="False" ToolbarImagesFolderUrl=""
                            ToolPanelWidth="200px" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
