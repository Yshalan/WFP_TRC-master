<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelfServices_Reports.aspx.vb" MasterPageFile="~/Default/NewMaster.master"
    Inherits="Reports_SelfServices_SelfServices_Reports" meta:resourcekey="PageResource1" UICulture="auto" Theme="SvTheme" %>


<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="Filter" runat="server">
            <uc1:PageHeader ID="lblReportTitle" runat="server" HeaderText="Self Service Reports"
                meta:resourcekey="lblReportTitleResource1" />
            <div class="row">
                <div class="col-md-4">

                    <asp:Label ID="lblSelfServiceReports" runat="server" Text="Self Service Reports"
                        meta:resourcekey="lblSelfServiceReportsResource1" />

                    <telerik:RadComboBox ID="RadComboSelfServiceReports" CausesValidation="False" Filter="Contains"
                        AutoPostBack="True" MarkFirstMatch="True" runat="server"
                        meta:resourcekey="RadComboSelfServiceReportseResource1" ExpandDirection="Up">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvradSelfServiceReports" runat="server" ControlToValidate="RadComboSelfServiceReports"
                        Display="None" ErrorMessage="Please Select Report Type" InitialValue="--Please Select--"
                        ValidationGroup="Save" meta:resourcekey="rfvradSelfServiceReportsResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceradSelfServiceReports" runat="server"
                        Enabled="True" TargetControlID="rfvradSelfServiceReports" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>

                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblFromDate" runat="server" meta:resourcekey="lblFromDateResource1"
                        Text="From Date"></asp:Label>

                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Culture="en-US"
                        meta:resourcekey="RadDatePicker1Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="RadDatePicker1"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="Save" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator9_ValidatorCalloutExtender"
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                    </cc1:ValidatorCalloutExtender>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblToDate" runat="server" meta:resourcekey="lblToDateResource1"
                        Text="To Date"></asp:Label>

                    <telerik:RadDatePicker ID="RadDatePicker2" runat="server" Culture="en-US"
                        meta:resourcekey="RadDatePicker2Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                            Width="">
                        </DateInput>
                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadDatePicker2"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="Save" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator1">
                    </cc1:ValidatorCalloutExtender>

                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="RadDatePicker1"
                        ControlToValidate="RadDatePicker2" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="Save" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender2"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>

            </div>
            <div id="dvViolation_Selection" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-4">
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
                        <div class="col-md-4">
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
                        <div class="col-md-4">
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
                        <div class="col-md-4"></div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="radnumDelayNum" runat="server" MinValue="0" Skin="Vista">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                </div>
                <div id="dvEarlyOutPolicy" runat="server" visible="false">
                    <div class="row">
                        <div class="col-md-4">
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
                        <div class="col-md-4">
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
                        <div class="col-md-4"></div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="radnumEarlyOutNum" runat="server" MinValue="0" Skin="Vista">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                </div>
                <div id="dvAbsentPolicy" runat="server" visible="false">
                    <div class="row">
                        <div class="col-md-4">
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
                        <div class="col-md-4"></div>
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
                    <div class="col-md-4">
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
                    <div class="col-md-4"></div>
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
                    <div class="col-md-4">
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
                    <div class="col-md-4"></div>
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
                <div class="col-md-8">

                    <asp:RadioButtonList ID="rblFormat" runat="server" RepeatDirection="Horizontal"
                        meta:resourcekey="rblFormatResource1" RepeatColumns="3">
                        <asp:ListItem Text="PDF" Value="1" Selected="True"
                            meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>


            <div class="row">
                <div class="col-md-12">
                    <div id="trControls" runat="server">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" meta:resourcekey="btnPrintResource1"
                            ValidationGroup="Save" />
                    </div>
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
