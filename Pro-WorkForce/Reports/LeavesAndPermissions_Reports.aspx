<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LeavesAndPermissions_Reports.aspx.vb"
    Theme="SvTheme" MasterPageFile="~/Default/ReportMaster.master" Inherits="Reports_SelfServices_LeavesAndPermissionsReports"
    meta:resourcekey="PageResource1" UICulture="auto" %>

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
            <%-- <center>
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
            <uc1:PageHeader ID="lblReportTitle" runat="server" HeaderText="Leaves and Permissions Reports"
                meta:resourcekey="lblReportTitleResource1" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblLeavesandPermissionsReports" runat="server" CssClass="Profiletitletxt"
                        Text="Report Name" meta:resourcekey="lblSelfServiceReportsResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadComboLeavesandPermissionsReports" CausesValidation="False"
                        Filter="Contains" AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server" ExpandDirection="Up"
                        meta:resourcekey="RadComboLeavesandPermissionsReportseResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvradLeavesandPermissionsReports" runat="server"
                        ControlToValidate="RadComboLeavesandPermissionsReports" Display="None" ErrorMessage="Please Select Report Type"
                        InitialValue="--Please Select--" ValidationGroup="btnPrint" meta:resourcekey="rfvradLeavesandPermissionsReportsResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceradLeavesandPermissionsReports" runat="server"
                        CssClass="AISCustomCalloutStyle" Enabled="True" TargetControlID="rfvradLeavesandPermissionsReports"
                        WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <uc1:EmployeeFilter ID="EmployeeFilter1" runat="server" ShowRadioSearch="true" ShowDirectStaffCheck="true" ValidationGroup="btnPrint" />
                </div>
            </div>

            <div id="trPermissionPerType" runat="server" visible="False" class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblPermtype" runat="server" CssClass="Profiletitletxt" Text="Permission Type"
                        meta:resourcekey="lblPermtypeResource2"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlperm_type" AutoPostBack="True" MarkFirstMatch="True" ExpandDirection="Up"
                        Skin="Vista" runat="server" meta:resourcekey="ddlperm_typeResource2">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvperm_type" runat="server" ControlToValidate="ddlperm_type"
                        Display="None" ErrorMessage="Please Select Permission Type" ValidationGroup="Grpfind"
                        meta:resourcekey="rfvperm_typeResource2"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceperm_type" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvperm_type" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
       
            <div id="dvPermissionStatus" runat="server" visible="False" class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblPermissionStatus" runat="server" CssClass="Profiletitletxt" Text="Permission Request Status"
                        meta:resourcekey="lblPermissionStatusResource2"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="radcmbRequestStatus" AutoPostBack="True" MarkFirstMatch="True" ExpandDirection="Up"
                        Skin="Vista" runat="server" meta:resourcekey="radcmbRequestStatusResource2">
                    </telerik:RadComboBox>
                </div>
            </div>
                 
            <div id="trLeaveType" runat="server" visible="False" class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblLeaveType" runat="server" CssClass="Profiletitletxt" Text="Leave Type"
                        meta:resourcekey="lblLeavetypeResource2"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="radcomboLeaveType" AutoPostBack="True" MarkFirstMatch="True" ExpandDirection="Up"
                        Skin="Vista" runat="server" meta:resourcekey="ddlLeave_typeResource2">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radcomboLeaveType"
                        Display="None" ErrorMessage="Please Select Leave Type" ValidationGroup="Grpfind"
                        meta:resourcekey="rfvperm_typeResource2"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="RequiredFieldValidator2" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
             <div id="Div1" runat="server" visible="true" class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text="تجاوزوا العدد"
                        ></asp:Label>
                </div>
                <div class="col-md-4">
                  <telerik:RadNumericTextBox ID="txtGreaterthan" Value="0" 
                            Skin="Vista" runat="server" Culture="en-US" >

                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                  </telerik:RadNumericTextBox>
                           
                 
                </div>
            </div>
            <div id="dvLeaveStatus" runat="server" visible="False" class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblLeaveStatus" runat="server" CssClass="Profiletitletxt" Text="Leave Request Status"
                        meta:resourcekey="lblLeaveStatusResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="radcmbLeaveRequestStatus" AutoPostBack="True" MarkFirstMatch="True" ExpandDirection="Up"
                        Skin="Vista" runat="server" meta:resourcekey="radcmbRequestStatusResource2">
                    </telerik:RadComboBox>
                </div>
            </div>
            <div id="dvManualPerms" runat="server" visible="False" class="row">
                <div class="col-md-6">
                    <asp:CheckBox ID="chkManualPerms" runat="server" Text="Show Self-Service Added Permissions Only"
                        meta:resourceKey="chkManualPermsResource1" />
                </div>
            </div>
            <div id="dvManualLeaves" runat="server" visible="False" class="row">
                <div class="col-md-6">
                    <asp:CheckBox ID="chkManualLeaves" runat="server" Text="Show Self-Service Added Leaves Only"
                        meta:resourceKey="chkManualLeavesResource1" />
                </div>
            </div>
            <div id="dvDeductionPolicy" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblDeductionPolicy" runat="server" CssClass="Profiletitletxt" meta:resourceKey="lblDeductionPolicyResource1"
                            Text="Deduction Policy"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadNumericTextBox ID="radnumDeductionPolicy" MinValue="0" MaxValue="24"
                            Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="radnumDeductionPolicyResource1">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="rfvDeductionPolicy" runat="server" ControlToValidate="radnumDeductionPolicy"
                            Display="None" ErrorMessage="Please Insert Deduction Policy" ValidationGroup="btnPrint"
                            meta:resourcekey="rfvDeductionPolicyResource1" Enabled="false">
                        </asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="vceDeductionPolicy" runat="server" CssClass="AISCustomCalloutStyle"
                            TargetControlID="rfvDeductionPolicy" WarningIconImageUrl="~/images/warning1.png"
                            Enabled="True">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblHours" runat="server" CssClass="Profiletitletxt" meta:resourceKey="lblHoursResource1"
                            Text="Hour(s)"></asp:Label>
                    </div>
                </div>
            </div>
            <div id="dvDetailedStudyPemission" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblStudyYear" runat="server" Text="Study Year"
                            meta:resourceKey="lblStudyYearResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadNumericTextBox ID="txtStudyYear" MinValue="0" MaxValue="99999" Skin="Vista"
                            runat="server" Culture="en-US" LabelCssClass="" Width="158px">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblSemester" runat="server" Text="Semester" meta:resourceKey="lblSemesterResource1"></asp:Label>
                    </div>
                    <div class="col-md-4" id="dvSemesterText" runat="server" visible="false">
                        <asp:TextBox ID="txtSemester" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="dvSemesterSelection" runat="server" visible="false">
                        <telerik:RadComboBox ID="radcmbxSemester" CausesValidation="False" Filter="Contains"
                            AutoPostBack="false" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                            ExpandDirection="Up">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div id="dvDelayPermissions" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblDelayCount" runat="server" Text="Number of Delay Permission" meta:resourcekey="lblDelayCountResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadNumericTextBox ID="txtDelayCount" MinValue="0"
                            Skin="Vista" runat="server" Culture="en-US" LabelCssClass="">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblFromDate" runat="server" CssClass="Profiletitletxt" meta:resourceKey="lblFromDateResource1"
                        Text="From Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Culture="en-US" meta:resourceKey="RadDatePicker1Resource1"
                        Width="180px">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="RadDatePicker1"
                        Display="None" ErrorMessage="Please Select Date" meta:resourceKey="RequiredFieldValidator9Resource1"
                        ValidationGroup="btnPrint"></asp:RequiredFieldValidator>
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
                            meta:resourcekey="CRVResource1" EnableDrillDown="False" GroupTreeImagesFolderUrl=""
                            HasGotoPageButton="False" HasPageNavigationButtons="False" ToolbarImagesFolderUrl=""
                            ToolPanelWidth="200px" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
