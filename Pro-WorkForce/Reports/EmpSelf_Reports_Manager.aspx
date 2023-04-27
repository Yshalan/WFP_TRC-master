﻿<%@ Page Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="EmpSelf_Reports_Manager.aspx.vb" Inherits="Emp_EmpSelf_Reports_Manager"
    Title="Untitled Page" UICulture="auto" meta:resourcekey="PageResource1" Theme="SvTheme" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>

<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/TA_innerpage.css" rel="stylesheet" type="text/css" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="Filter" runat="server">
            <%--           <center>
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
            <uc1:PageHeader ID="lblReportTitle" runat="server" />

            <uc2:PageFilter ID="EmployeeFilter" IsManager="1" runat="server"
                ShowRadioSearch="true" ForceCompanyFilter="true" ShowDirectStaffCheck="true" ValidationGroup="Save" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblReportName" runat="server" CssClass="Profiletitletxt"
                        meta:resourcekey="lblReportNameResource1" Text="Report Name" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCmbBxReports" runat="server" AutoPostBack="true"
                        CausesValidation="False" Filter="Contains" MarkFirstMatch="True"
                        meta:resourcekey="RadCmbBxReportsResource1" Skin="Vista" Style="width: 350px">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvReports" runat="server"
                        ControlToValidate="RadCmbBxReports" Display="None"
                        ErrorMessage="Please Select Report" meta:resourcekey="rfvReportsResource1"
                        ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceReports" runat="server" Enabled="True"
                        TargetControlID="rfvReports">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblFromDate" runat="server" CssClass="Profiletitletxt"
                        meta:resourcekey="lblFromDateResource1" Text="From Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Culture="en-US"
                        meta:resourcekey="RadDatePicker1Resource1" Width="180px">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                            ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                            LabelCssClass="" Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                        ControlToValidate="RadDatePicker1" Display="None"
                        ErrorMessage="Please Select Date"
                        meta:resourcekey="RequiredFieldValidator9Resource1" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator9_ValidatorCalloutExtender"
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblToDate" runat="server" CssClass="Profiletitletxt"
                        meta:resourcekey="lblToDateResource1" Text="To Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker2" runat="server" Culture="en-US"
                        meta:resourcekey="RadDatePicker2Resource1" Width="180px">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                            ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                            LabelCssClass="" Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="RadDatePicker2" Display="None"
                        ErrorMessage="Please Select Date"
                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                        Enabled="True" TargetControlID="RequiredFieldValidator1">
                    </cc1:ValidatorCalloutExtender>
                    <asp:CompareValidator ID="CompareValidator2" runat="server"
                        ControlToCompare="RadDatePicker1" ControlToValidate="RadDatePicker2"
                        Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                        meta:resourcekey="CompareValidator2Resource1" Operator="GreaterThanEqual"
                        Type="Date" ValidationGroup="Save"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                        CssClass="AISCustomCalloutStyle" Enabled="True"
                        TargetControlID="CompareValidator2" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div id="dvViolation_Selection" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblViolationSelection" runat="server" Text="Violation Selection"
                            meta:resourcekey="lblViolationSelectionResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadComboBox ID="radcmbxViolatonSelection" CausesValidation="False" Filter="Contains"
                            AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px">
                            <Items>
                                <telerik:RadComboBoxItem Value="1" Text="Delay" meta:resourcekey="RadComboBoxItem1Resource1" />
                                <telerik:RadComboBoxItem Value="2" Text="EarlyOut" meta:resourcekey="RadComboBoxItem2Resource1" />
                                <telerik:RadComboBoxItem Value="3" Text="Delay, EarlyOut or Absent" Selected="true" meta:resourcekey="RadComboBoxItem3Resource1" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>
                </div>
                <div id="dvDelayPolicy" runat="server" visible="false">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblMinDelay" runat="server" Text="Minimum Delay"
                                meta:resourcekey="lblMinDelayResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadMaskedTextBox ID="rmtxtDelayTime" runat="server" Mask="####:##" TextWithLiterals="0000:00"
                            DisplayMask="####:##" Text='000000' LabelCssClass="">
                                <%--      <ClientEvents OnBlur="ValidateDelayTime" />--%>
                            </telerik:RadMaskedTextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblhours_Minutes" runat="server" Text="(HH:MM)"
                                meta:resourcekey="lblhours_MinutesResource1"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblDelayPolicy" runat="server" Text="Delay Policy"
                                meta:resourcekey="lblDelayPolicyResource1"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="rblDelayPolicy" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Text="Equal" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Greater Than Or Equal" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Less Than Or Equal" meta:resourcekey="ListItem3Resource1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="radnumDelayNum" runat="server" MinValue="0" Skin="Vista">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                </div>
                <div id="dvEarlyOutPolicy" runat="server" visible="false">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblEarlyOut" runat="server" Text="Minimum Early Out"
                                meta:resourcekey="lblEarlyOutResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadMaskedTextBox ID="rmtxtEarlyOutTime" runat="server" Mask="####:##" TextWithLiterals="0000:00"
                            DisplayMask="####:##" Text='000000' LabelCssClass="">
                                <%--      <ClientEvents OnBlur="ValidateDelayTime" />--%>
                            </telerik:RadMaskedTextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblhours_Minutes2" runat="server" Text="(HH:MM)"
                                meta:resourcekey="lblhours_MinutesResource1"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblEarlyOutPolicy" runat="server" Text="Early Out Policy"
                                meta:resourcekey="lblEarlyOutPolicyResource1"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="rblEarlyOutPolicy" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Text="Equal" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Greater Than Or Equal" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Less Than Or Equal" meta:resourcekey="ListItem3Resource1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="radnumEarlyOutNum" runat="server" MinValue="0" Skin="Vista">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                </div>
                <div id="dvAbsentPolicy" runat="server" visible="false">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblAbsentPolicy" runat="server" Text="Absent Policy"
                                meta:resourcekey="lblAbsentPolicyResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:RadioButtonList ID="rblAbsentPolicy" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Text="Equal" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Greater Than Or Equal" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Less Than Or Equal" meta:resourcekey="ListItem3Resource1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="radnumAbsentNum" runat="server" MinValue="0" Skin="Vista">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div id="dvSummaryParams" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblWorkHours" runat="server" Text="Worked Hours" meta:resourcekey="lblWorkHoursResource1"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:RadioButtonList ID="rblWorkHours" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Equal" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Greater Than Or Equal" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Less Than Or Equal" meta:resourcekey="ListItem3Resource1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-4">
                        <telerik:RadMaskedTextBox ID="rmtxtWorkHours" runat="server" Mask="####:##" TextWithLiterals="0000:00"
                            DisplayMask="####:##" Text='000000' LabelCssClass="">
                        </telerik:RadMaskedTextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblhours_Minutes3" runat="server" Text="(HH:MM)"
                            meta:resourcekey="lblhours_MinutesResource1"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblAbsentDays" runat="server" Text="Absent Days" meta:resourcekey="lblAbsentDaysResource1"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:RadioButtonList ID="rblAbsentDays" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Equal" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Greater Than Or Equal" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Less Than Or Equal" meta:resourcekey="ListItem3Resource1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-4">
                        <telerik:RadNumericTextBox ID="radnumAbsentDays" runat="server" MinValue="0" Skin="Vista">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblDays" runat="server" Text="Days"
                            meta:resourcekey="lblDaysResource1"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-6">
                    <asp:RadioButtonList ID="rblFormat" runat="server" CssClass="Profiletitletxt"
                        meta:resourcekey="rblFormatResource1" RepeatDirection="Horizontal">
                        <asp:ListItem meta:resourcekey="ListItemResource1" Selected="True" Text="PDF"
                            Value="1"></asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource2" Text="MS Word" Value="2"></asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource3" Text="MS Excel" Value="3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div id="trControls" runat="server" class="row">
                <div class="col-md-2"></div>
                <div runat="server" class="col-md-6">
                    <asp:Button ID="btnPrint" runat="server" CssClass="button"
                        meta:resourcekey="btnPrintResource1" Text="Print" ValidationGroup="Save" />
                </div>
            </div>


        </asp:View>
        <asp:View ID="Report" runat="server">
            <table align="center" style="background-color: #fff; width: 1024px; height: 44px;">
                <tr>
                    <td align="center" style="border-left: 1px; border-right: 1px">
                        <CR2:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="True" SeparatePages="False"
                            GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False"
                            EnableDrillDown="False" GroupTreeImagesFolderUrl="" HasGotoPageButton="False"
                            HasPageNavigationButtons="False" ToolbarImagesFolderUrl="" ToolPanelWidth="200px" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
