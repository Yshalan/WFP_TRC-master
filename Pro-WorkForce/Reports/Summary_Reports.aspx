<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Summary_Reports.aspx.vb" MasterPageFile="~/Default/ReportMaster.master"
    Inherits="Reports_SelfServices_Summary_Reports" meta:resourcekey="PageResource1" UICulture="auto" Theme="SvTheme" %>


<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagPrefix="uc1" TagName="EmployeeFilter" %>
<%@ Register Src="../Admin/UserControls/UserSecurityFilter.ascx" TagName="EmployeeFilterEfficiency"
    TagPrefix="uc1" %>

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

            <uc1:PageHeader ID="lblReportTitle" runat="server" HeaderText="Summary Reports"
                meta:resourcekey="lblReportTitleResource1" />

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblExtraReports" runat="server" CssClass="Profiletitletxt" Text="Report Name" meta:resourcekey="lblSelfServiceReportsResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadComboExtraReports" CausesValidation="False" Filter="Contains"
                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                        meta:resourcekey="RadComboExtraReportsResource1" ExpandDirection="Up">
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
            <div class="row" id="trOvertimeType" runat="server" visible="False">

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
            <div class="row" id="trOvertimeStatus" runat="server" visible="False">
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
            <div class="row" id="dvGrade" runat="server" visible="false">
                <div class="col-md-2">
                    <asp:Label ID="lblGrades" runat="server" Text="Grades" meta:resourcekey="lblGradesResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlGrades" AutoPostBack="false" MarkFirstMatch="True"
                        Skin="Vista" runat="server" meta:resourcekey="ddlGradesResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvGrades" runat="server" ControlToValidate="ddlGrades"
                        Display="None" ErrorMessage="Please Select Employee Grade" InitialValue="--Please Select--"
                        ValidationGroup="btnPrint" meta:resourcekey="rfvGradesResource1" Enabled="false"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceGrade" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvGrades" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row" id="trFromDate" runat="server">
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
                    <br />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="RadDatePicker1"
                        ControlToValidate="RadDatePicker2" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="btnPrint" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender2"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div id="dvWorkHoursParam" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblWorkDurationMinValue" runat="server" Text="Minimum Working Hours" meta:resourcekey="lblWorkDurationMinValueResource1"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <telerik:RadMaskedTextBox ID="rmtxtMinDuration" runat="server" Mask="####:##" TextWithLiterals="0000:00"
                            DisplayMask="####:##" Text='000000' LabelCssClass="">
                        </telerik:RadMaskedTextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblhours_Minutes" runat="server" Text="(HH:MM)"
                            meta:resourcekey="lblhours_MinutesResource1"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblWorkDurationSelection" runat="server" Text="Occurrence" meta:resourcekey="lblWorkDurationSelectionResource1"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:RadioButtonList ID="rblWorkDurationSelection" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Equal" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Greater Than Or Equal" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Less Than Or Equal" meta:resourcekey="ListItem3Resource1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-4">
                        <telerik:RadNumericTextBox ID="radnumWorkDurationNum" runat="server" MinValue="0" Skin="Vista">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblTimes" runat="server" Text="Times"
                            meta:resourcekey="lblTimesResource1"></asp:Label>
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
            <div id="dvMinWorkHrs_Detailed" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblMinWorkHrs_Detailed" runat="server" Text="Minimum Working Hours" meta:resourcekey="lblWorkDurationMinValueResource1"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <telerik:RadMaskedTextBox ID="rmtMinWorkHrs_Detailed" runat="server" Mask="##:##" TextWithLiterals="00:00"
                            DisplayMask="##:##" Text='0000' LabelCssClass="">
                        </telerik:RadMaskedTextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblhours_Minutes2" runat="server" Text="(HH:MM)"
                            meta:resourcekey="lblhours_MinutesResource1"></asp:Label>
                    </div>
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
            <div class="row" id="trControls" runat="server">
                <div class="col-md-6">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="button" meta:resourcekey="Button1Resource1"
                        ValidationGroup="btnPrint" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False" CssClass="button"
                        meta:resourcekey="btnClearResource1" />
                </div>
            </div>
        </asp:View>
        <asp:View ID="Efficiency" runat="server">
            <%-- <center>
                <table style="width: 594px; border-width: 0px; background-color: #FFFFFF; visibility: hidden;"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="3">
                            <div id="Div1">
                                <a href="../Default/logout.aspx">
                                    <div id="Div2">
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

            <uc1:PageHeader ID="PageHeaderEfficiency" runat="server" />

            <div class="row">
                <div class="col-md-12">
                    <uc1:EmployeeFilterEfficiency ID="EmployeeFilter2" runat="server" ShowRadioSearch="true" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text="From Date" meta:resourcekey="Label1Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker3" runat="server" Culture="en-US"
                        meta:resourcekey="RadDatePicker3Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RadDatePicker1"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3"
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label2" runat="server" CssClass="Profiletitletxt" Text="To Date" meta:resourcekey="Label2Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker4" runat="server" Culture="en-US"
                        Width="180px" meta:resourcekey="RadDatePicker4Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadDatePicker2"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator1">
                    </cc1:ValidatorCalloutExtender>
                    <br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="RadDatePicker1"
                        ControlToValidate="RadDatePicker2" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="btnPrint" meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender5"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt" meta:resourcekey="RadioButtonList1Resource1">
                        <asp:ListItem Text="PDF" Value="1" Selected="True" meta:resourcekey="ListItemResource4"></asp:ListItem>
                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource5"></asp:ListItem>
                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource6"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row" id="tr1" runat="server">
                <div class="col-md-6">
                    <asp:Button ID="btnPrintEfficiency" runat="server" Text="Print" CssClass="button" ValidationGroup="btnPrint" />
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="button"
                        Text="Cancel" meta:resourcekey="btnCancelResource1" />
                </div>
            </div>
        </asp:View>
        <asp:View ID="Discipline" runat="server">

            <uc1:PageHeader ID="PageHeaderDiscipline" runat="server" />

            <div class="row">
                <div class="col-md-12">
                    <uc1:EmployeeFilterEfficiency ID="EmployeeFilterDiscipline" runat="server" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label3" runat="server" CssClass="Profiletitletxt" Text="From Date" meta:resourcekey="Label1Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker5" runat="server" Culture="en-US"
                        meta:resourcekey="RadDatePicker3Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="RadDatePicker1"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6"
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="To Date" meta:resourcekey="Label2Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker6" runat="server" Culture="en-US"
                        Width="180px" meta:resourcekey="RadDatePicker4Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="RadDatePicker2"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator1">
                    </cc1:ValidatorCalloutExtender>
                    <br />
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="RadDatePicker1"
                        ControlToValidate="RadDatePicker2" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="btnPrint" meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender8"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt" meta:resourcekey="RadioButtonList1Resource1">
                        <asp:ListItem Text="PDF" Value="1" Selected="True" meta:resourcekey="ListItemResource4"></asp:ListItem>
                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource5"></asp:ListItem>
                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource6"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row" id="Div1" runat="server">
                <div class="col-md-6">
                    <asp:Button ID="btnPrintDisciplineReport" runat="server" Text="Print" CssClass="button" ValidationGroup="btnPrint"
                        meta:resourcekey="Button1Resource1" />
                    <asp:Button ID="btnCancelDiscipline" runat="server" CausesValidation="False" CssClass="button"
                        Text="Cancel" meta:resourcekey="btnClearResource1" />
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

