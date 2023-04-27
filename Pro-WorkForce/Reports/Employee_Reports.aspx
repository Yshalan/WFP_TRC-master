<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Employee_Reports.aspx.vb" MasterPageFile="~/Default/NewMaster.master"
    Inherits="Reports_SelfServices_EmployeeReports" meta:resourcekey="PageResource1" UICulture="auto" Theme="SvTheme" %>


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
            <%--    <center>
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

            <uc1:PageHeader ID="lblReportTitle" runat="server" HeaderText="Employee Reports"
                meta:resourcekey="lblReportTitleResource1" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblEmployeeReports" runat="server" CssClass="Profiletitletxt" Text="Employee Reports" meta:resourcekey="lblSelfServiceReportsResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadComboEmployeeReports" CausesValidation="False" Filter="Contains"
                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px" ExpandDirection="Up"
                        meta:resourcekey="RadComboEmployeeReportsResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvradEmployeeReports" runat="server" ControlToValidate="RadComboEmployeeReports"
                        Display="None" ErrorMessage="Please Select Report Type" InitialValue="--Please Select--"
                        ValidationGroup="btnPrint" meta:resourcekey="rfvradEmployeeReportsResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceradEmployeeReports" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvradEmployeeReports" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>


            <div class="row">
                <div class="col-md-12">
                    <uc1:EmployeeFilter ID="EmployeeFilter1" runat="server" ShowRadioSearch="true" ValidationGroup="btnPrint" ShowDirectStaffCheck="true" />
                </div>
            </div>
            <div id="trWorkSchedule" runat="server" visible="false" class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblWorkSchedule" runat="server" CssClass="Profiletitletxt" Text="Work Schedule" />

                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlWorkSchedule" CausesValidation="False" Filter="Contains"
                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                        meta:resourcekey="RadComboScheduleTypeResource1" DataTextField="ScheduleName" DataValueField="ScheduleId"
                        OnSelectedIndexChanged="ddlWorkSchedule_onselectedindexchanged">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlWorkSchedule"
                        Display="None" ErrorMessage="Please Select Schedule Type" InitialValue="--Please Select--"
                        meta:resourcekey="rfvradEmployeeReportsResource1" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="RequiredFieldValidator4" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div id="trShifts" runat="server" visible="false" class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblShifts" runat="server" CssClass="Profiletitletxt" Text="Shifts" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlShifts" CausesValidation="False" Filter="Contains"
                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                        meta:resourcekey="RadComboScheduleTypeResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlShifts"
                        Display="None" ErrorMessage="Please Select Schedule Type" InitialValue="--Please Select--"
                        meta:resourcekey="rfvradEmployeeReportsResource1" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="RequiredFieldValidator5" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>

            </div>
            <div class="row" id="trNoOfShifts" runat="server" visible="false">
                <div class="col-md-2"></div>
                <div class="col-md-3">
                    <asp:RadioButtonList ID="rblNoofShifts" runat="server" AutoPostBack="True" CssClass="Profiletitletxt">
                        <asp:ListItem Text="All Shifts" Value="0" Selected="True" meta:resourcekey="ListItemResource5"></asp:ListItem>
                        <asp:ListItem Text="Number of Shifts >=" Value="1" meta:resourcekey="ListItemResource5"></asp:ListItem>
                        <asp:ListItem Text="Number of Shifts <" Value="2" meta:resourcekey="ListItemResource6"></asp:ListItem>

                    </asp:RadioButtonList>
                </div>
            </div>
            <div id="trlblNoofShifts" runat="server" visible="false" class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblNoofShiftsValue" runat="server" Text="Value : " CssClass="Profiletitletx"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadNumericTextBox ID="txtNoOfShifts" MinValue="0" MaxValue="9999999" Skin="Vista"
                        runat="server" Culture="en-US" LabelCssClass="" Width="30px">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                    </telerik:RadNumericTextBox>
                </div>
            </div>
            <div id="trScheduleGroup" runat="server" visible="False" class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblGroupName" runat="server" CssClass="Profiletitletxt" Text="Group Name"
                        meta:resourcekey="lblGroupNameResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCombBxGroupName" CausesValidation="False" Filter="Contains"
                        MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                        meta:resourcekey="RadCombBxGroupNameResource1">
                    </telerik:RadComboBox>
                </div>
            </div>
            <div id="EmployeeCardDetailsStatus" runat="server" visible="False" class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblStatus" runat="server" CssClass="Profiletitletxt" Text="Status"
                        meta:resourcekey="lblStatusResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlStatus" runat="server"
                        MarkFirstMatch="True" AutoPostBack="True" Skin="Vista" meta:resourcekey="ddlStatusResource1" />
                </div>
            </div>
            <div id="dvStudyNursingSchedule" class="row" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblStudyNursingScheduleType" runat="server" Text="Schedule Type" meta:resourcekey="lblStudyNursingScheduleTypeResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadComboBox ID="ddlStudyNursingScheduleType" CausesValidation="False" Filter="Contains"
                            AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                            meta:resourcekey="ddlStudyNursingScheduleTypeResource1">
                            <Items>
                                <telerik:RadComboBoxItem Value="-1" Text="--Please Select--" meta:resourcekey="RadComboBoxItem1Resource1" />
                                <telerik:RadComboBoxItem Value="2" Text="Nursing Schedule" meta:resourcekey="RadComboBoxItem2Resource1" />
                                <telerik:RadComboBoxItem Value="3" Text="Study Schedule" meta:resourcekey="RadComboBoxItem3Resource1" />
                            </Items>
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="rfvddlScheduleType" runat="server" ControlToValidate="ddlStudyNursingScheduleType"
                            Display="None" ErrorMessage="Please Select Schedule Type" InitialValue="--Please Select--"
                            meta:resourcekey="rfvradEmployeeReportsResource1" ValidationGroup="btnPrint" Enabled="false"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="vceddlScheduleType" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="false" TargetControlID="rfvddlScheduleType" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblStudyNursingScheduleName" runat="server" Text="Schedule Name" meta:resourcekey="lblStudyNursingScheduleNameResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadComboBox ID="ddlStudyNursingScheduleName" CausesValidation="False" Filter="Contains"
                            AutoPostBack="false" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                            meta:resourcekey="ddlScheduleNameResource1" />
                    </div>
                </div>
            </div>
            <div id="dvNotifications" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblNotificationType" runat="server" Text="Notification Types"
                            meta:resourcekey="lblNotificationTypeResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadComboBox ID="radcmbxNotificationType" CausesValidation="False" Filter="Contains"
                            MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px" ExpandDirection="Up">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="row" id="dvHoliday" runat="server" visible="false">
                <div class="col-md-2">
                    <asp:Label ID="lblHoliday" runat="server" Text="Holiday"  meta:resourcekey="lblHolidayResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                     <telerik:RadComboBox ID="radcmbxHolidays" CausesValidation="False" Filter="Contains"
                            MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px" ExpandDirection="Up">
                        </telerik:RadComboBox>
                </div>
            </div>
            <div class="row" id="trFromDate" runat="server">
                <div class="col-md-2">
                    <asp:Label ID="lblFromDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblFromDateResource1"
                        Text="From Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Culture="en-US"
                        meta:resourcekey="RadDatePicker1Resource1" Width="180px">
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
                        meta:resourcekey="RadDatePicker2Resource1" Width="180px">
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
        <asp:View ID="vwSchedulesList" runat="server">
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

            <uc1:PageHeader ID="PageHeader1" runat="server" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblScheduleType" runat="server" CssClass="Profiletitletxt" Text="Schedule Type"
                        meta:resourcekey="lblScheduleTypeResource1" />

                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadComboScheduleType" CausesValidation="False" Filter="Contains"
                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                        meta:resourcekey="RadComboScheduleTypeResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RadComboScheduleType"
                        Display="None" ErrorMessage="Please Select Schedule Type" InitialValue="--Please Select--"
                        meta:resourcekey="rfvradEmployeeReportsResource1" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="RequiredFieldValidator2" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblScheduleName" runat="server" CssClass="Profiletitletxt" Text="Schedule Name"
                        meta:resourcekey="lblScheduleNameResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCmbBxScheduleName" CausesValidation="False" Filter="Contains"
                        Skin="Vista" runat="server"
                        meta:resourcekey="RadCmbBxScheduleNameResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadCmbBxScheduleName"
                        Display="None" ErrorMessage="Please Select Schedule Name" InitialValue="--Please Select--"
                        meta:resourcekey="rfvradEmployeeReportsResource1" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="RequiredFieldValidator3" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>

            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:RadioButtonList ID="rblSchedules" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt"
                        meta:resourcekey="rblFormatResource1">
                        <asp:ListItem Text="PDF" Value="1" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div id="tr1" runat="server" class="row">

                <div class="col-md-6">
                    <asp:Button ID="btnPrintSchedules" runat="server" Text="Print" CssClass="button" ValidationGroup="Save"
                        meta:resourcekey="Button1Resource1" />
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="button"
                        Text="Cancel" meta:resourcekey="btnCancelResource1" />
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
