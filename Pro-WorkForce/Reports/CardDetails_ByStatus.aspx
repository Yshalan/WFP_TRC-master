<%@ Page Title="" Language="VB" MasterPageFile="~/Default/ReportMaster.master"  StylesheetTheme="Default" AutoEventWireup="false"
    CodeFile="CardDetails_ByStatus.aspx.vb" Inherits="Reports_CardDetails_ByStatus"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="EmployeeFilter"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <asp:multiview id="MultiView1" runat="server" activeviewindex="0">
        <asp:view id="Filter" runat="server">
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
                                                <td colspan="3">
                                                    <uc1:EmployeeFilter ID="EmployeeFilter1" runat="server" ShowRadioSearch="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:label id="lblStatus" runat="server" cssclass="Profiletitletxt" text="Status"
                                                        meta:resourcekey="lblStatusResource1"></asp:label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="ddlStatus" runat="server" AppendDataBoundItems="True" Width="200px"
                                                        MarkFirstMatch="True" AutoPostBack="True" Skin="Vista" meta:resourcekey="ddlStatusResource1" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="130px">
                                                    <asp:label id="lblFromDate" runat="server" cssclass="Profiletitletxt" text="From Date"
                                                        meta:resourcekey="lblFromDateResource1"></asp:label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Culture="en-US" Width="180px"
                                                        meta:resourcekey="RadDatePicker1Resource1">
                                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                        </Calendar>
                                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                                            Width="">
                                                        </DateInput>
                                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td>
                                                    <asp:requiredfieldvalidator id="RequiredFieldValidator9" runat="server" controltovalidate="RadDatePicker1"
                                                        display="None" errormessage="Please Select Date" validationgroup="btnPrint" meta:resourcekey="RequiredFieldValidator9Resource1">
                                                    </asp:requiredfieldvalidator>
                                                    <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator9_ValidatorCalloutExtender"
                                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="138px">
                                                    <asp:label id="lblToDate" runat="server" cssclass="Profiletitletxt" text="To Date"
                                                        meta:resourcekey="lblToDateResource1"></asp:label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="RadDatePicker2" runat="server" Culture="en-US" Width="180px"
                                                        meta:resourcekey="RadDatePicker2Resource1">
                                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                        </Calendar>
                                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                                            Width="">
                                                        </DateInput>
                                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td>
                                                    <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" controltovalidate="RadDatePicker2"
                                                        display="None" errormessage="Please Select Date" validationgroup="btnPrint" meta:resourcekey="RequiredFieldValidator1Resource1">
                                                    </asp:requiredfieldvalidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                                        TargetControlID="RequiredFieldValidator1">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <asp:comparevalidator id="CompareValidator2" runat="server" controltocompare="RadDatePicker1"
                                                        controltovalidate="RadDatePicker2" display="None" errormessage="To Date must be greater than or Equal From Date!"
                                                        operator="GreaterThanEqual" type="Date" validationgroup="btnPrint" meta:resourcekey="CompareValidator2Resource1">
                                                    </asp:comparevalidator>
                                                    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender2"
                                                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:radiobuttonlist id="rblFormat" runat="server" repeatdirection="Horizontal" cssclass="Profiletitletxt"
                                                        meta:resourcekey="rblFormatResource1">
                                                        <asp:listitem text="PDF" value="1" selected="True" meta:resourcekey="ListItemResource1">
                                                        </asp:listitem>
                                                        <asp:listitem text="MS Word" value="2" meta:resourcekey="ListItemResource2">
                                                        </asp:listitem>
                                                        <asp:listitem text="MS Excel" value="3" meta:resourcekey="ListItemResource3">
                                                        </asp:listitem>
                                                    </asp:radiobuttonlist>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr id="trControls" runat="server" align="center">
                                                <td runat="server">
                                                </td>
                                                <td runat="server">
                                                    <asp:button id="btnPrint" runat="server" text="Print" cssclass="button" validationgroup="btnPrint" meta:resourcekey="Button1Resource1"/>
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
        </asp:view>
        <asp:view id="Report" runat="server">
            <table align="center" style="background-color: #fff; width: 1024px; height: 44px;">
                <tr>
                    <td align="center" style="border-left: 1px; border-right: 1px">
                        <CR2:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="True" SeparatePages="False"
                            GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False"
                            EnableDrillDown="False" GroupTreeImagesFolderUrl="" HasGotoPageButton="False"
                            HasPageNavigationButtons="False" meta:resourcekey="CRVResource1" ToolbarImagesFolderUrl=""
                            ToolPanelWidth="200px" />
                    </td>
                </tr>
            </table>
        </asp:view>
    </asp:multiview>
</asp:content>
