<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Violations_Reports.aspx.vb"
    MasterPageFile="~/Default/ReportMaster.master" Inherits="Reports_SelfServices_Violations_Reports"
    meta:resourcekey="PageResource1" UICulture="auto" Theme="SvTheme" %>

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
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="Filter" runat="server">
            <%--  <center>
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
            <uc1:PageHeader ID="lblReportTitle" runat="server" HeaderText="Violations Reports"
                meta:resourcekey="lblReportTitleResource1" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblViolationsReports" runat="server" CssClass="Profiletitletxt" Text="Report Name"
                        meta:resourcekey="lblViolationsReportsResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadComboViolationsReports" CausesValidation="False" Filter="Contains"
                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                        meta:resourcekey="RadComboViolationsReportseResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvradViolationsReports" runat="server" ControlToValidate="RadComboViolationsReports"
                        Display="None" ErrorMessage="Please Select Report Type" InitialValue="--Please Select--"
                        ValidationGroup="btnPrint" meta:resourcekey="rfvradViolationsReportsResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceradViolationsReports" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvradViolationsReports" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <uc1:EmployeeFilter ID="EmployeeFilter1" runat="server" ShowRadioSearch="true" ShowDirectStaffCheck="true" ValidationGroup="btnPrint" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblFromDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblFromDateResource1"
                        Text="From Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Culture="en-US" meta:resourcekey="RadDatePicker1Resource1"
                        Width="180px">
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
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblToDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblToDateResource1"
                        Text="To Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker2" runat="server" Culture="en-US" meta:resourcekey="RadDatePicker2Resource1">
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
                    <br />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="RadDatePicker1"
                        ControlToValidate="RadDatePicker2" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="btnPrint" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender2"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div id="dvViolation_Selection" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="lblViolationSelection" runat="server" Text="Violation Selection"
                            meta:resourcekey="lblViolationSelectionResource1"></asp:Label>
                    </div>
                    <div class="col-md-3">
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
                        <div class="col-md-3">
                            <asp:Label ID="lblMinDelay" runat="server" Text="Minimum Delay"
                                meta:resourcekey="lblMinDelayResource1"></asp:Label>
                        </div>
                        <div class="col-md-3">
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
                        <div class="col-md-3">
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
                        <div class="col-md-3"></div>
                        <div class="col-md-3">
                            <telerik:RadNumericTextBox ID="radnumDelayNum" runat="server" MinValue="0" Skin="Vista">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                </div>
                <div id="dvEarlyOutPolicy" runat="server" visible="false">
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label ID="lblEarlyOut" runat="server" Text="Minimum Early Out"
                                meta:resourcekey="lblEarlyOutResource1"></asp:Label>
                        </div>
                        <div class="col-md-3">
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
                        <div class="col-md-3">
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
                        <div class="col-md-3"></div>
                        <div class="col-md-3">
                            <telerik:RadNumericTextBox ID="radnumEarlyOutNum" runat="server" MinValue="0" Skin="Vista">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                </div>
                <div id="dvAbsentPolicy" runat="server" visible="false">
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label ID="lblAbsentPolicy" runat="server" Text="Absent Policy"
                                meta:resourcekey="lblAbsentPolicyResource1"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:RadioButtonList ID="rblAbsentPolicy" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Text="Equal" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Greater Than Or Equal" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Less Than Or Equal" meta:resourcekey="ListItem3Resource1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3"></div>
                        <div class="col-md-3">
                            <telerik:RadNumericTextBox ID="radnumAbsentNum" runat="server" MinValue="0" Skin="Vista">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-8">
                    <asp:RadioButtonList ID="rblFormat" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt"
                        meta:resourcekey="rblFormatResource1">
                        <asp:ListItem Text="PDF" Value="1" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>


            <div class="row" id="trControls" runat="server">
                
                <div id="Div1" class="col-md-6" runat="server">
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
                            meta:resourcekey="CRVResource1" EnableDrillDown="False" GroupTreeImagesFolderUrl=""
                            HasGotoPageButton="False" HasPageNavigationButtons="False" ToolbarImagesFolderUrl=""
                            ToolPanelWidth="200px" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
