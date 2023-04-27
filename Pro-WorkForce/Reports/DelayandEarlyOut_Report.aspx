<%@ Page Title="" Language="VB" MasterPageFile="~/Default/ReportMaster.master" AutoEventWireup="false"
    CodeFile="DelayandEarlyOut_Report.aspx.vb" Inherits="Reports_DelayandEarlyOut_Report"  StylesheetTheme="Default"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="EmployeeFilter"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ValidateTextboxFrom() {

            var tmpTime1 = $find("<%=rmtTotalDelayEarly.ClientID %>");

            txtValidate(tmpTime1, true);

        }
        function txtValidate(txt, IsFrom) {
            var strTime = String(txt._projectedValue);
            strTime = strTime.split(/\D/);

            if (strTime[0] == "") { strTime[0] = "00"; }
            if (strTime[1] == "") { strTime[1] = "00"; }
            if (strTime[1] > 59) {
                strTime[1] = "00";
                strTime[0] = String(Number(strTime[0]) + 1);
            }
            if (IsFrom) {
                if (strTime[0] > 23) {
                    strTime[0] = "00";
                }
            }
            else if (strTime[0] > 24) {
                strTime[0] = "24";
            }

            txt.set_value(strTime[0] + "" + strTime[1]);
            return false;
        }
    </script>
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
                                                <td colspan="3">
                                                    <uc1:EmployeeFilter ID="EmployeeFilter1" runat="server" ShowRadioSearch="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="130px">
                                                    <asp:Label ID="lblFromDate" runat="server" CssClass="Profiletitletxt" Text="From Date"
                                                        meta:resourcekey="lblFromDateResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Culture="en-US" Width="180px"
                                                        meta:resourcekey="RadDatePicker1Resource1">
                                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                        </Calendar>
                                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                                            Width="">
                                                        </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
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
                                                    <asp:Label ID="lblToDate" runat="server" CssClass="Profiletitletxt" Text="To Date"
                                                        meta:resourcekey="lblToDateResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="RadDatePicker2" runat="server" Culture="en-US" Width="180px"
                                                        meta:resourcekey="RadDatePicker2Resource1">
                                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                        </Calendar>
                                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                                            Width="">
                                                        </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
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
                                                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="btnPrint" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                                                    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender2"
                                                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="center">
                                                    <asp:RadioButtonList ID="rblFilter" runat="server" AutoPostBack="True" CssClass="Profiletitletxt"
                                                        meta:resourcekey="rblFilterResource1">
                                                        <asp:ListItem Text="All Delays and Early out" Value="1" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                        <asp:ListItem Text="Delay and Early out per day" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                        <asp:ListItem Text="Total Delay and Early out" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                        <asp:ListItem Text="Total no of times Delay and Early out" Value="4" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr id="trTotalDelayEarly" runat="server" visible="False">
                                                <td>
                                                    <asp:Label ID="lblValue" runat="server" CssClass="Profiletitletxt" Text="Value" meta:resourcekey="lblValueResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadMaskedTextBox ID="rmtTotalDelayEarly" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                                        Width="40px" DisplayMask="##:##" Text='0000' LabelCssClass="">
                                                        <ClientEvents OnBlur="ValidateTextboxFrom" />
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                            </tr>
                                            <tr id="trTotalCount" runat="server" visible="False">
                                                <td>
                                                    <asp:Label ID="lblCountValue" runat="server" CssClass="Profiletitletxt" Text="Value"
                                                        meta:resourcekey="lblCountValueResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtTotalCount" MinValue="0" MaxValue="9999999" Skin="Vista"
                                                        runat="server" Culture="en-US" LabelCssClass="" Width="200px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rblFormat" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt"
                                                        meta:resourcekey="rblFormatResource1">
                                                        <asp:ListItem Text="PDF" Value="1" Selected="True" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource6"></asp:ListItem>
                                                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource7"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr id="trControls" runat="server" align="center">
                                                <td runat="server">
                                                </td>
                                                <td runat="server">
                                                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="button" ValidationGroup="btnPrint" meta:resourcekey="btnPrintResource1"/>
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
                            EnableDrillDown="False" GroupTreeImagesFolderUrl="" HasGotoPageButton="False"
                            HasPageNavigationButtons="False" meta:resourcekey="CRVResource1" ToolbarImagesFolderUrl=""
                            ToolPanelWidth="200px" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
