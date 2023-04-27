<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Others_Reports.aspx.vb" MasterPageFile="~/Default/ReportMaster.master"
    Inherits="Reports_SelfServices_Other_Reports" meta:resourcekey="PageResource1" UICulture="auto" Theme="SvTheme" %>


<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc2" %>
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
            <uc1:PageHeader ID="lblReportTitle" runat="server" HeaderText="Other Reports" />
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
        </asp:View>
        <asp:View ID="vwEventLogReport" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <uc1:EmployeeFilter ID="EmployeeFilter1" runat="server" ShowRadioSearch="true" ValidationGroup="btnPrint" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblForm" runat="server" CssClass="Profiletitletxt" Text="Form Name"
                        meta:resourcekey="lblFormResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="radcmbbxForm" AutoPostBack="True" MarkFirstMatch="True"
                        Skin="Vista" runat="server" meta:resourcekey="radcmbbxFormResource1">
                    </telerik:RadComboBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblAction" runat="server" CssClass="Profiletitletxt" Text="Event Name"
                        meta:resourcekey="lblActionResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="radcmbbxAction" AutoPostBack="True" MarkFirstMatch="True"
                        Skin="Vista" runat="server" meta:resourcekey="radcmbbxActionResource1">
                        <Items>
                            <telerik:RadComboBoxItem Text="All" Value="0" runat="server" meta:resourcekey="RadComboBoxItemResource1"
                                Owner="" />
                            <telerik:RadComboBoxItem Text="Add" Value="Add" runat="server" meta:resourcekey="RadComboBoxItemResource2"
                                Owner="" />
                            <telerik:RadComboBoxItem Text="Edit" Value="Edit" runat="server" meta:resourcekey="RadComboBoxItemResource3"
                                Owner="" />
                            <telerik:RadComboBoxItem Text="Delete" Value="Delete" runat="server" meta:resourcekey="RadComboBoxItemResource4"
                                Owner="" />
                        </Items>
                    </telerik:RadComboBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblFromDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblFromDateResource1"
                        Text="From Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Culture="en-US" meta:resourcekey="RadDatePicker1Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
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
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
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
                    <asp:RadioButtonList ID="rblFormat" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt"
                        meta:resourcekey="rblFormatResource1">
                        <asp:ListItem Text="PDF" Value="1" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div id="trControls" runat="server" class="row">
                <div class="col-md-2"></div>
                <div class="col-md-2">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="button" meta:resourcekey="Button1Resource1"
                        ValidationGroup="btnPrint" />

                </div>
            </div>

        </asp:View>
        <asp:View ID="vwEventProjectGroups" runat="server">

            <center>
                <table style="width: 594px; border-width: 0px; background-color: #FFFFFF; visibility: hidden;"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="3">
                            <div id="Div3">
                                <a href="../Default/logout.aspx">
                                    <div id="Div4">
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
                                                    <telerik:RadComboBox ID="RadCmbBxEvent" MarkFirstMatch="True"
                                                        AutoPostBack="True" Skin="Vista" runat="server"
                                                        meta:resourcekey="RadCmbBxEventResource1">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblGroup" runat="server" Text="Logical Group Name" AutoPostBack="true"
                                                        CssClass="Profiletitletxt" meta:resourcekey="lblGroupResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="RadCmbBxGroup" MarkFirstMatch="True"
                                                        Skin="Vista" runat="server" meta:resourcekey="RadCmbBxGroupResource1">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:RadioButtonList ID="rblLogicalGroup" runat="server" RepeatDirection="Horizontal"
                                                        CssClass="Profiletitletxt" meta:resourcekey="rblFormatResource1">
                                                        <asp:ListItem Text="PDF" Value="1" Selected="True"
                                                            meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr id="tr1" runat="server" align="center">
                                                <td id="Td4" runat="server"></td>
                                                <td id="Td5" runat="server">
                                                    <asp:Button ID="btnPrintEvent" runat="server" Text="Print" CssClass="button" meta:resourcekey="Button1Resource1"
                                                        ValidationGroup="btnPrint" />

                                                </td>
                                                <td id="Td6" runat="server"></td>
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
        <asp:View ID="vwEventProjectEmployees" runat="server">

            <center>
                <table style="width: 594px; border-width: 0px; background-color: #FFFFFF; visibility: hidden;"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="3">
                            <div id="Div5">
                                <a href="../Default/logout.aspx">
                                    <div id="Div6">
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
                        <uc1:PageHeader ID="PageHeader3" runat="server" />
                        <div dir="<%=dir %>">
                            <table style="text-align: <%= iif(dir="ltr","left","right")%>; width: 450px">
                                <tr>
                                    <td>
                                        <table style="background-color: #fff">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Event\Project" AutoPostBack="true"
                                                        CssClass="Profiletitletxt" meta:resourcekey="lblEventResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="RadComboBox1" MarkFirstMatch="True"
                                                        AutoPostBack="True" Skin="Vista" runat="server"
                                                        meta:resourcekey="RadCmbBxEventResource1">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Logical Group Name" AutoPostBack="true"
                                                        CssClass="Profiletitletxt" meta:resourcekey="lblGroupResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="RadComboBox2" MarkFirstMatch="True" AutoPostBack="True"
                                                        Skin="Vista" runat="server" meta:resourcekey="RadCmbBxGroupResource1">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEmployee" runat="server" Text="Employee" AutoPostBack="true"
                                                        CssClass="Profiletitletxt" meta:resourcekey="lblEmployeeResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="RadCmbBxEmployee" MarkFirstMatch="True"
                                                        Skin="Vista" runat="server" meta:resourcekey="RadCmbBxEmployeeResource1">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:RadioButtonList ID="rblEmployee" runat="server" RepeatDirection="Horizontal"
                                                        CssClass="Profiletitletxt" meta:resourcekey="rblFormatResource1">
                                                        <asp:ListItem Text="PDF" Value="1" Selected="True"
                                                            meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr id="tr2" runat="server" align="center">
                                                <td id="Td7" runat="server"></td>
                                                <td id="Td8" runat="server">
                                                    <asp:Button ID="btnPrintProject" runat="server" Text="Print" CssClass="button" meta:resourcekey="Button1Resource1"
                                                        ValidationGroup="btnPrint" />
                                                </td>
                                                <td id="Td9" runat="server"></td>
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
        <asp:View ID="vwEmployeeEventsShifts" runat="server">

            <center>
                <table style="width: 594px; border-width: 0px; background-color: #FFFFFF; visibility: hidden;"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="3">
                            <div id="Div7">
                                <a href="../Default/logout.aspx">
                                    <div id="Div8">
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
                        <uc1:PageHeader ID="PageHeader4" runat="server" />
                        <div dir="<%=dir %>">
                            <table style="text-align: <%= iif(dir="ltr","left","right")%>; width: 450px">
                                <tr>
                                    <td>
                                        <table style="background-color: #fff">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Event\Project" AutoPostBack="true"
                                                        CssClass="Profiletitletxt" meta:resourcekey="lblEventResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="RadComboBox3" MarkFirstMatch="True" AutoPostBack="True"
                                                        Skin="Vista" runat="server" meta:resourcekey="RadCmbBxEventResource1">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="Logical Group Name" AutoPostBack="true"
                                                        CssClass="Profiletitletxt" meta:resourcekey="lblGroupResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="RadComboBox4" MarkFirstMatch="True" AutoPostBack="True"
                                                        Skin="Vista" runat="server" meta:resourcekey="RadCmbBxGroupResource1">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text="Employee" AutoPostBack="true" CssClass="Profiletitletxt"
                                                        meta:resourcekey="lblEmployeeResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="RadComboBox5" MarkFirstMatch="True" Skin="Vista" runat="server"
                                                        meta:resourcekey="RadCmbBxEmployeeResource1">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td width="138px">
                                                    <asp:Label ID="Label6" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblFromDateResource1"
                                                        Text="From Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="RadDatePicker3" runat="server" Culture="en-US" meta:resourcekey="RadDatePicker1Resource1"
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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RadDatePicker1"
                                                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3"
                                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="138px">
                                                    <asp:Label ID="Label7" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblToDateResource1"
                                                        Text="To Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="RadDatePicker4" runat="server" Culture="en-US" meta:resourcekey="RadDatePicker2Resource1"
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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadDatePicker2"
                                                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                                                        TargetControlID="RequiredFieldValidator1">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="RadDatePicker1"
                                                        ControlToValidate="RadDatePicker2" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                                                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="Grpfind" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                                                    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender5"
                                                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:RadioButtonList ID="rblShifts" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt"
                                                        meta:resourcekey="rblFormatResource1">
                                                        <asp:ListItem Text="PDF" Value="1" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr id="tr3" runat="server" align="center">
                                                <td id="Td10" runat="server"></td>
                                                <td id="Td11" runat="server">
                                                    <asp:Button ID="btnGroup" runat="server" Text="Print" CssClass="button" meta:resourcekey="Button1Resource1"
                                                        ValidationGroup="btnPrint" />
                                                </td>
                                                <td id="Td12" runat="server"></td>
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
        <asp:View ID="vwOrganization" runat="server">

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" Text="Company"
                        meta:resourcekey="lblCompanyeResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="radcmbCompany" Filter="Contains" MarkFirstMatch="True" Skin="Vista"
                        runat="server" meta:resourcekey="radcmbCompanyResource1">
                    </telerik:RadComboBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-2">
                    <asp:Button ID="btnPrintOrg" runat="server" Text="Print" CssClass="button" meta:resourcekey="Button1Resource1"
                        ValidationGroup="btnPrint" />
                </div>
            </div>
        </asp:View>
        <asp:View ID="vwIntegrationErrors" runat="server">
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblIntegrationFromDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblFromDateResource1"
                        Text="From Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpIntegrationFromDate" runat="server" Culture="en-US" meta:resourcekey="RadDatePicker1Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="rfvIntegrationFromDate" runat="server" ControlToValidate="dtpIntegrationFromDate"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="IntegrationErrors"
                        meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcerfvIntegrationFromDate"
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label9" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblToDateResource1"
                        Text="To Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpIntegrationToDate" runat="server" Culture="en-US" meta:resourcekey="RadDatePicker2Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="rfvIntegrationToDate" runat="server" ControlToValidate="dtpIntegrationToDate"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="IntegrationErrors" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceIntegrationToDate" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator1">
                    </cc1:ValidatorCalloutExtender>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="dtpIntegrationFromDate"
                        ControlToValidate="dtpIntegrationToDate" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="IntegrationErrors" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator3" ID="vceIntegrationErrors"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text">
                    <asp:RadioButtonList ID="rblIntegrationErrors" runat="server" RepeatDirection="Horizontal"
                        CssClass="Profiletitletxt" meta:resourcekey="rblFormatResource1">
                        <asp:ListItem Text="PDF" Value="1" Selected="True"
                            meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnPrintIntegrationErrors" runat="server" Text="Print" CssClass="button" meta:resourcekey="Button1Resource1"
                        ValidationGroup="IntegrationErrors" />
                </div>
            </div>


        </asp:View>
        <asp:View ID="vwIntegrationMissingLeaves" runat="server">
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblIntegrationMissingLeavesFromDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblFromDateResource1"
                        Text="From Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpIntegrationMissingLeavesFromDate" runat="server" Culture="en-US" meta:resourcekey="RadDatePicker1Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="rfvIntegrationMissingLeavesFromDate" runat="server" ControlToValidate="dtpIntegrationMissingLeavesFromDate"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="IntegrationMissingLeaves"
                        meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceIntegrationMissingLeavesFromDate"
                        runat="server" Enabled="True" TargetControlID="rfvIntegrationMissingLeavesFromDate">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblIntegrationMissingLeavesToDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblToDateResource1"
                        Text="To Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpIntegrationMissingLeavesToDate" runat="server" Culture="en-US" meta:resourcekey="RadDatePicker2Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="rfvIntegrationMissingLeavesToDate" runat="server" ControlToValidate="dtpIntegrationMissingLeavesToDate"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="IntegrationErrors" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceIntegrationMissingLeavesToDate" runat="server" Enabled="True"
                        TargetControlID="rfvIntegrationMissingLeavesToDate">
                    </cc1:ValidatorCalloutExtender>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToCompare="dtpIntegrationMissingLeavesFromDate"
                        ControlToValidate="dtpIntegrationMissingLeavesToDate" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="IntegrationErrors" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator4" ID="ValidatorCalloutExtender8"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text">
                    <asp:RadioButtonList ID="rblIntegrationMissingLeaves" runat="server" RepeatDirection="Horizontal"
                        CssClass="Profiletitletxt" meta:resourcekey="rblFormatResource1">
                        <asp:ListItem Text="PDF" Value="1" Selected="True"
                            meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnIntegrationMissingLeavesPrint" runat="server" Text="Print" CssClass="button" meta:resourcekey="Button1Resource1"
                        ValidationGroup="IntegrationMissingLeaves" />
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
        <asp:View ID="vwEntityManager" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <uc1:EmployeeFilter ID="EmployeeFilter2" runat="server" ShowDirectStaffCheck="false" ShowRadioSearch="true" ValidationGroup="btnPrintEntityManager" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:RadioButtonList ID="rblFormat2" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt"
                        meta:resourcekey="rblFormatResource1">
                        <asp:ListItem Text="PDF" Value="1" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text">
                    <asp:Button ID="btnPrintEntityManager" runat="server" Text="Print" CssClass="button" meta:resourcekey="Button1Resource1"
                        ValidationGroup="btnPrintEntityManager" />
                    <asp:Button ID="btnClearEntityManager" runat="server" CausesValidation="False" CssClass="button"
                        Text="Clear" meta:resourcekey="btnClearResource1" />
                </div>
            </div>
        </asp:View>
        <asp:View ID="vwATSNotAttend" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <uc1:EmployeeFilter ID="EmployeeFilter3" runat="server" ShowDirectStaffCheck="false" ShowRadioSearch="false" ValidationGroup="btnATSNotAttend" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:RadioButtonList ID="rblATSNotAttend" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt"
                        meta:resourcekey="rblFormatResource1">
                        <asp:ListItem Text="PDF" Value="1" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text">
                    <asp:Button ID="btnPrintATSNotAttend" runat="server" Text="Print" CssClass="button" meta:resourcekey="Button1Resource1"
                        ValidationGroup="btnPrintEntityManager" />
                </div>
            </div>
        </asp:View>
        <asp:View ID="vwEmployeeWorkStatus_Days" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <uc1:EmployeeFilter ID="EmployeeFilter4" runat="server" ShowRadioSearch="true" ValidationGroup="btnPrint" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label runat="server" ID="lblYear" CssClass="Profiletitletxt" Text="Year" meta:resourcekey="lblYearResource1" />
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlYear" DataTextField="txt" DataValueField="val"
                        meta:resourcekey="ddlYearResource1">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label id="lblYearMessage" style="color: Maroon;">
                    </label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label runat="server" ID="lblMonth" CssClass="Profiletitletxt" Text="Month" meta:resourcekey="lblMonthResource1" />
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlMonth" DataTextField="txt" DataValueField="val"
                        meta:resourcekey="ddlMonthResource1">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label id="lblMonthMessage" style="color: Maroon;">
                    </label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:RadioButtonList ID="rblEmployeeWorkStatus" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt"
                        meta:resourcekey="rblFormatResource1">
                        <asp:ListItem Text="PDF" Value="1" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="MS Excel" Value="3" Selected="True" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div id="dvbtnPrintEmployeeWorkStatus" runat="server" class="row">
                <div class="col-md-2"></div>
                <div class="col-md-2">
                    <asp:Button ID="btnPrintEmployeeWorkStatus" runat="server" Text="Print" CssClass="button" meta:resourcekey="Button1Resource1"
                        ValidationGroup="btnPrint" />

                </div>
            </div>
        </asp:View>
    </asp:MultiView>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-2">
            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="button"
                Text="Cancel" meta:resourcekey="btnCancelResource1" />
        </div>
    </div>
</asp:Content>
