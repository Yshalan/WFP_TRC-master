﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExtraHoursAndOvertime_Reports.aspx.vb" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    Inherits="Reports_SelfServices_ExtraHoursAndOverTimeReports" meta:resourcekey="PageResource1" UICulture="auto" %>


<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagPrefix="uc1" TagName="EmployeeFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/TA_innerpage.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript">
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
    </script>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="Filter" runat="server">
            <%--   <center>
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
            </center>--%>

            <uc1:PageHeader ID="lblReportTitle" runat="server" HeaderText="Extra Hours And Overtime Reports"
                meta:resourcekey="lblReportTitleResource1" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblExtraReports" runat="server" CssClass="Profiletitletxt" Text="Report Name" meta:resourcekey="lblSelfServiceReportsResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadComboExtraReports" CausesValidation="False" Filter="Contains"
                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                        meta:resourcekey="RadComboExtraReportsResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvradExtraReports" runat="server" ControlToValidate="RadComboExtraReports"
                        Display="None" ErrorMessage="Please Select Report Type" InitialValue="--Please Select--"
                        ValidationGroup="btnPrint" meta:resourcekey="rfvradExtraReportsResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceradExtraReports" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvradExtraReports" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <uc1:EmployeeFilter ID="EmployeeFilter1" runat="server" ShowRadioSearch="true" ShowDirectStaffCheck="true" ValidationGroup="btnPrint" />
                </div>
            </div>
            <div id="trOvertimeType" runat="server" visible="False" class="row">

                <div class="col-md-2">
                    <asp:Label ID="lblOvertimeType" runat="server" CssClass="Profiletitletxt" Text="Overtime Type"
                        meta:resourcekey="lblOvertimeTypeResource1"></asp:Label>
                </div>
                <div class="col-md-5">
                    <asp:RadioButtonList ID="rblType" runat="server" CssClass="Profiletitletxt">
                        <asp:ListItem Value="1" Text="In/Out Time Within Schedule" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
                        <asp:ListItem Value="2" Text="In/Out Time Without Specifying" meta:resourcekey="ListItem2Resource1"
                            Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div id="trOvertimeStatus" runat="server" visible="False" class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblOvertimeStatus" runat="server" CssClass="Profiletitletxt" Text="Overtime Status"
                        meta:resourcekey="lblOvertimeStatusResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlOvertimeStatus" AutoPostBack="True" MarkFirstMatch="True"
                        Skin="Vista" runat="server" meta:resourcekey="ddlOvertimeStatusResource1">
                    </telerik:RadComboBox>
                </div>
            </div>

            <div id="trFromDate" runat="server" class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblFromDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblFromDateResource1"
                        Text="From Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Culture="en-US"
                        meta:resourcekey="RadDatePicker1Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="RadDatePicker1"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator9_ValidatorCalloutExtender"
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div id="trToDate" runat="server" class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblToDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblToDateResource1"
                        Text="To Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker2" runat="server" Culture="en-US"
                        meta:resourcekey="RadDatePicker2Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadDatePicker2"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator1">
                    </cc1:ValidatorCalloutExtender>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="RadDatePicker1"
                        ControlToValidate="RadDatePicker2" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="btnPrint" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender2"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:RadioButtonList ID="rblFormat" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt" meta:resourcekey="rblFormatResource1">
                        <asp:ListItem Text="PDF" Value="1" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div id="trControls" runat="server" class="row">
                <div class="col-md-6">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="button" meta:resourcekey="Button1Resource1"
                        ValidationGroup="btnPrint" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False" CssClass="button"
                        meta:resourcekey="btnClearResource1" />
                </div>
            </div>
        </asp:View>
        <asp:View ID="Report" runat="server">
            <table align="center" style="background-color: #fff; width: 1024px; height: 44px;">
                <tr>
                    <td align="center" style="border-left: 1px; border-right: 1px">
                        <CR2:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="True" SeparatePages="False"
                            GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False"
                            meta:resourcekey="CRVResource1" EnableDrillDown="False"
                            GroupTreeImagesFolderUrl="" HasGotoPageButton="False"
                            HasPageNavigationButtons="False" ToolbarImagesFolderUrl=""
                            ToolPanelWidth="200px" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
