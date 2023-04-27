<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Attendance_Reports.aspx.vb"
    MasterPageFile="~/Default/NewMaster.master" Inherits="Reports_SelfServices_AttendanceReports"
    meta:resourcekey="PageResource1" UICulture="auto" Theme="SvTheme" %>


<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>


<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagPrefix="uc1" TagName="EmployeeFilter" %>
<%@ Register Src="../Employee/UserControls/EmpDetails.ascx" TagPrefix="uc1" TagName="EmpDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/TA_innerpage.css" rel="stylesheet" type="text/css" />--%>
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
            <uc1:PageHeader ID="lblReportTitle" runat="server" HeaderText="Attendance Reports"
                meta:resourcekey="lblReportTitleResource1" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label Width="125px" ID="lblAttendanceReports" runat="server" CssClass="Profiletitletxt"
                        Text="Attendance Reports" meta:resourcekey="lblSelfServiceReportsResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadComboAttendanceReports" CausesValidation="False" Filter="Contains"
                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                        meta:resourcekey="RadComboAttendanceReportseResource1" ExpandDirection="Up">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvradAttendanceReports" runat="server" ControlToValidate="RadComboAttendanceReports"
                        Display="None" ErrorMessage="Please Select Report Type" InitialValue="--Please Select--"
                        ValidationGroup="btnPrint" meta:resourcekey="rfvradAttendanceReportsResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceradAttendanceReports" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvradAttendanceReports" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>


            </div>

            <div id="Div_Incomplete_Transaction_Status" visible="false" runat="server" class="row">
                <div class="col-md-2">
                </div>
                <div runat="server" class="col-md-5">
                    <asp:RadioButtonList ID="RadioButton_Incomplete_transaction" runat="server" AutoPostBack="True" CssClass="Profiletitletxt"
                        meta:resourcekey="rblFilterResource1">
                        <asp:ListItem Text="جميع الحركات" Value="3" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="لا يوجد خروج" Value="1"></asp:ListItem>
                        <asp:ListItem Text="لا يوجد دخول" Value="2"></asp:ListItem>


                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <uc1:EmployeeFilter ID="EmployeeFilter1" runat="server" ShowRadioSearch="true" ValidationGroup="btnPrint" ShowDirectStaffCheck="true" />
                </div>
            </div>


            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-6">
                    <asp:RadioButtonList ID="rblEmpStatus" runat="server" CssClass="Profiletitletxt"
                        RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblEmpStatus_SelectedIndexChanged">
                        <asp:ListItem Text="Active" Value="1" Selected="True" meta:resourcekey="rblEmpStatusResource1"></asp:ListItem>
                        <asp:ListItem Text="InActive" Value="2" meta:resourcekey="rblEmpStatusResource2"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>



            <div class="row" id="dvLogicalAbsent" runat="server" visible="false">
                <div class="col-md-3">
                    <asp:CheckBox ID="chkLogicalAbsent" runat="server" Text="Include Logical Absent" meta:resourcekey="chkLogicalGroupResource1" />
                </div>
            </div>
            <div class="row" id="trAuthorityType" runat="server" visible="False">
                <div class="col-md-2" id="Td1" runat="server">
                    <asp:Label ID="lblAuthorityType" runat="server" CssClass="Profiletitletxt" Text="Authority Name"
                        meta:resourcekey="lblLeavetypeResource2"></asp:Label>
                </div>
                <div class="col-md-4" id="Td2" runat="server">
                    <telerik:RadComboBox ID="radcomboAuthorityType" AutoPostBack="True" MarkFirstMatch="True"
                        Skin="Vista" runat="server" meta:resourcekey="ddlLeave_typeResource2">
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
                        ControlToValidate="RadDatePicker2" Display="Dynamic" ErrorMessage="To Date must be greater than or Equal From Date!"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="btnPrint" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                    <%--    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender2"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>--%>
                </div>
            </div>
            <div id="trDelayAndEarlyOut" visible="False" runat="server" class="row">
                <div class="col-md-2">
                </div>
                <div runat="server" class="col-md-5">
                    <asp:RadioButtonList ID="rblFilter" runat="server" AutoPostBack="True" CssClass="Profiletitletxt"
                        meta:resourcekey="rblFilterResource1">
                        <asp:ListItem Text="All Delays and Early out" Value="1" Selected="True" meta:resourcekey="ListItemResource5"></asp:ListItem>
                        <asp:ListItem Text="Delay and Early out per day" Value="2" meta:resourcekey="ListItemResource6"></asp:ListItem>
                        <asp:ListItem Text="Total Delay and Early out" Value="3" meta:resourcekey="ListItemResource7"></asp:ListItem>
                        <asp:ListItem Text="Total no of times Delay and Early out" Value="4" meta:resourcekey="ListItemResource4"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div id="trTotalDelayEarly" runat="server" visible="False" class="row">
                <div runat="server" class="col-md-2">
                    <asp:Label ID="lblValue" runat="server" CssClass="Profiletitletxt" Text="Value" meta:resourcekey="lblValueResource1"></asp:Label>
                </div>
                <div runat="server" class="col-md-4">
                    <telerik:RadMaskedTextBox ID="rmtTotalDelayEarly" runat="server" Mask="##:##" TextWithLiterals="00:00"
                        DisplayMask="##:##" Text='0000' LabelCssClass="">
                        <ClientEvents OnBlur="ValidateTextboxFrom" />
                    </telerik:RadMaskedTextBox>
                </div>
            </div>
            <div class="row" id="trTotalCount" runat="server" visible="False">
                <div runat="server" class="col-md-2">
                    <asp:Label ID="lblCountValue" runat="server" CssClass="Profiletitletxt" Text="Value"
                        meta:resourcekey="lblCountValueResource1"></asp:Label>
                </div>
                <div runat="server" class="col-md-4">
                    <telerik:RadNumericTextBox ID="txtTotalCount" MinValue="0" MaxValue="9999999" Skin="Vista"
                        runat="server" Culture="en-US" LabelCssClass="" Width="200px">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                    </telerik:RadNumericTextBox>
                </div>
            </div>
            <div id="dvAbsentParam" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblAbsentNumber" runat="server" Text="Absent Days" meta:resourcekey="lblAbsentNumberResource1"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:RadioButtonList ID="rblAbsentPolicy" runat="server" RepeatDirection="Horizontal">
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
                        <telerik:RadNumericTextBox ID="radnumAbsentNum" MinValue="0" MaxValue="9999999" Skin="Vista"
                            runat="server" Culture="en-US" LabelCssClass="" Width="200px">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                    </div>
                </div>
            </div>
            <div id="dvIncomplete_Transactions_Advance" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblMissingCount" runat="server" Text="Missing In\Out Number"
                            meta:resourcekey="lblMissingCountResource1"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:RadioButtonList ID="rblMissingCount" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Equal" meta:resourcekey="ListItem1Resource1" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Greater Than Or Equal" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Less Than Or Equal" meta:resourcekey="ListItem3Resource1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-4">
                        <telerik:RadNumericTextBox ID="radnumMissingCountVal" MinValue="0" MaxValue="9999999" Skin="Vista"
                            runat="server" Culture="en-US" LabelCssClass="" Width="200px">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
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
            <div id="trControls" runat="server" class="row">
                <div class="col-md-12">
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
                        <%--       <CR2:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="True" SeparatePages="False"
                            GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False"
                            meta:resourcekey="CRVResource1" EnableDrillDown="False" GroupTreeImagesFolderUrl=""
                            HasGotoPageButton="False" HasPageNavigationButtons="False" ToolbarImagesFolderUrl=""
                            ToolPanelWidth="200px" />--%>

                        <CR2:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="True" SeparatePages="False"
                            GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False"
                            EnableDrillDown="False" GroupTreeImagesFolderUrl=""
                            HasGotoPageButton="False" HasPageNavigationButtons="False" ToolbarImagesFolderUrl=""
                            ToolPanelWidth="200px" />

                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
