<%@ Page Title="Employee Work Schedule" Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/AdminMaster.master"
    AutoEventWireup="false" CodeFile="EmpWorkSchedule.aspx.vb" Inherits="EmpWorkSchedule" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckOptions() {

            var checked = false;
            var chkBoxList = document.getElementById("<%=chkLstEmployee.ClientID%>");
            var count = chkBoxList.getElementsByTagName("input");

            for (var i = 0; i < count.length; i++) {
                if (count[i].checked) {
                    checked = true;
                    break;
                }
            }
            if (checked == false) {
                ShowMessage("Please select atleast one employee");
                return false;
            }
            return true;
        }
    </script>

    <style type="text/css">
        .Hello
        {
            font: 10pt arial bold italic;
            color: Gray;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlLeavesTypes" runat="server">
        <ContentTemplate>
            <table width="600px" cellspacing="0" cellpadding="0">
                <tr align="left">
                    <td>
                        <div id="divLeavesTypes" style="display: block">
                            <table width="600px">
                                <tr>
                                    <td colspan="2">
                                        <uc1:PageHeader ID="PageHeader1" HeaderText="Employee Work Schedule" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" CssClass="Profiletitletxt" Text="Company" 
                                            meta:resourcekey="Label3Resource1"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="RadComboBoxCompany" runat="server" Width="210px" 
                                            AutoPostBack="True" meta:resourcekey="RadComboBoxCompanyResource1">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RadComboBoxCompany"
                                            InitialValue="--Please Select--" Display="None" ErrorMessage="Select Company"
                                            ValidationGroup="GrWS" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                                            TargetControlID="RequiredFieldValidator2" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblArabicName" runat="server" CssClass="Profiletitletxt" 
                                            Text="Entity name" meta:resourcekey="lblArabicNameResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="RadComboBoxEntity" runat="server" Width="210px" 
                                            AutoPostBack="True" meta:resourcekey="RadComboBoxEntityResource1">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSchedules" runat="server" CssClass="Profiletitletxt" 
                                            Text="Work Schedules" meta:resourcekey="lblSchedulesResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="RadComboBoxWorkSchedules" runat="server" Width="210px" 
                                            AutoPostBack="True" meta:resourcekey="RadComboBoxWorkSchedulesResource1">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadComboBoxWorkSchedules"
                                            InitialValue="--Please Select--" Display="None" ErrorMessage="Select Work Schedule"
                                            ValidationGroup="GrWS" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                                            TargetControlID="RequiredFieldValidator1" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                        </cc1:ValidatorCalloutExtender>
                                        &nbsp;
                                        <asp:Label ID="lblScheduleType" runat="server" CssClass="Hello" 
                                            meta:resourcekey="lblScheduleTypeResource1"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDateFrom0" runat="server" CssClass="Profiletitletxt" 
                                            Text="Start Date" meta:resourcekey="lblDateFrom0Resource1"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="dteStartDate" runat="server" 
                                            Culture="English (United States)" EnableTyping="False" Width="120px" 
                                            meta:resourcekey="dteStartDateResource1">
                                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                            </Calendar>
                                            <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                                Width="" ReadOnly="True">
                                            </DateInput>
                                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="ReqFVFromDay" runat="server" 
                                            ControlToValidate="dteStartDAte" Display="None" 
                                            ErrorMessage="Select Start Date " ValidationGroup="GrWS" 
                                            meta:resourcekey="ReqFVFromDayResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="VCEFromDay" runat="server" Enabled="True" TargetControlID="ReqFVFromDay"
                                            CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTemporary" runat="server" CssClass="Profiletitletxt" 
                                            Text="Is Temporary" meta:resourcekey="lblTemporaryResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chckBxTemporary" runat="server" AutoPostBack="True" 
                                            meta:resourcekey="chckBxTemporaryResource1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEndDate" runat="server" CssClass="Profiletitletxt" 
                                            Text="End Date" meta:resourcekey="lblEndDateResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="dteEndDate" runat="server" 
                                            Culture="English (United States)" EnableTyping="False" Width="120px" 
                                            meta:resourcekey="dteEndDateResource1">
                                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                            </Calendar>
                                            <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                                Width="" ReadOnly="True">
                                            </DateInput>
                                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="ReqEndDate" runat="server" ControlToValidate="dteEndDate"
                                            Enabled="False" Display="None" ErrorMessage="Select End Date " 
                                            ValidationGroup="GrWS" meta:resourcekey="ReqEndDateResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                            TargetControlID="ReqEndDate" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                        </cc1:ValidatorCalloutExtender>
                                          <asp:CompareValidator ID="CVDate" runat="server" 
                                            ControlToCompare="dteStartDate" ControlToValidate="dteEndDate"
                                                ErrorMessage="End Date should be greater than or equal to Start Date" Display="None"
                                                Operator="GreaterThanEqual" Type="Date" ValidationGroup="GrWS" 
                                            meta:resourcekey="CVDateResource1"></asp:CompareValidator>
                                            <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender4"
                                                runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                            </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="dvCompany" runat="server" style="background-color: #fafafa; border: solid 1px 000000;">
                                            <table width="500px">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" 
                                                            meta:resourcekey="Label1Resource1" Text="Employees"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkLstEmployee" runat="server" BorderStyle="None" 
                                                            meta:resourcekey="chkLstEmployeeResource1" RepeatColumns="3" 
                                                            RepeatDirection="Horizontal" Width="400px">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                                            ValidationGroup="GrWS" meta:resourcekey="btnSaveResource1" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
