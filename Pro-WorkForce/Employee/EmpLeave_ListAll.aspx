<%@ Page Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/AdminMaster.master" AutoEventWireup="false"
    CodeFile="EmpLeave_ListAll.aspx.vb" Inherits="EmpLeave_ListAll" Title="Untitled Page"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Leave Request Lists" />
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td colspan="3">
                        <asp:Label CssClass="Profiletitletxt" ID="Label9" runat="server" Text="From" meta:resourcekey="Label9Resource1"></asp:Label>
                        <telerik:RadDatePicker ID="dtpFromSearch" runat="server" AllowCustomText="false"
                            MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                            Culture="English (United States)" meta:resourcekey="dtpFromSearchResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="dtpFromSearch"
                            Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="grpApply"
                            meta:resourcekey="RequiredFieldValidator8Resource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="RequiredFieldValidator8" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                        <asp:Label CssClass="Profiletitletxt" ID="Label10" runat="server" Text="To" meta:resourcekey="Label10Resource1"></asp:Label>
                        &nbsp;<telerik:RadDatePicker ID="dtToSearch" runat="server" AllowCustomText="false"
                            MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                            Culture="English (United States)" meta:resourcekey="dtToSearchResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        &nbsp;<asp:Button ID="btnApply" runat="server" CssClass="button" Text="Apply" ValidationGroup="grpApply"
                            meta:resourcekey="btnApplyResource1" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="dtToSearch"
                            Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="grpApply"
                            meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="RequiredFieldValidator9" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="dtpFromSearch"
                            ControlToValidate="dtToSearch" Display="None" ErrorMessage="To date should be greater than or equal to from date"
                            Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpApply" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="CompareValidator2" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass="Profiletitletxt" ID="Label1" runat="server" Text="Company" meta:resourcekey="Label1Resource1"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="ddlCompany" runat="server" AppendDataBoundItems="True" Width="200px"
                            AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" meta:resourcekey="ddlCompanyResource1">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="--Please Select--" meta:resourcekey="RadComboBoxItemResource1" />
                            </Items>
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="grpsave"
                            runat="server" ControlToValidate="ddlCompany" Display="None" ErrorMessage="Please Select  Company"
                            InitialValue="--Please Select--" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass="Profiletitletxt" ID="Label2" runat="server" Text="Entity" meta:resourcekey="Label2Resource1"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="ddlEntity" runat="server" AppendDataBoundItems="True" Width="200px"
                            AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" meta:resourcekey="ddlEntityResource1">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="--Please Select--" meta:resourcekey="RadComboBoxItemResource2" />
                            </Items>
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="grpsave"
                            runat="server" ControlToValidate="ddlEntity" Display="None" ErrorMessage="Please Select Entity "
                            InitialValue="--Please Select--" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="RequiredFieldValidator2" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass="Profiletitletxt" ID="Label3" runat="server" Text="Employee"
                            meta:resourcekey="Label3Resource1"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="ddlEmployee" runat="server" AppendDataBoundItems="True"
                            Width="200px" MarkFirstMatch="True" Skin="Vista" AutoPostBack="True" meta:resourcekey="ddlEmployeeResource1">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="--Please Select--" meta:resourcekey="RadComboBoxItemResource3" />
                            </Items>
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="grpsave"
                            runat="server" ControlToValidate="ddlEmployee" Display="None" ErrorMessage="Please Select Employee"
                            InitialValue="--Please Select--" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="RequiredFieldValidator3" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass="Profiletitletxt" ID="Label4" runat="server" Text="Leave Type"
                            meta:resourcekey="Label4Resource1"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="ddlLeaveType" runat="server" AppendDataBoundItems="True"
                            Width="200px" MarkFirstMatch="True" Skin="Vista" meta:resourcekey="ddlLeaveTypeResource1">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="--Please Select--" meta:resourcekey="RadComboBoxItemResource4" />
                            </Items>
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="grpsave"
                            runat="server" ControlToValidate="ddlLeaveType" Display="None" ErrorMessage="Please Select Leave Type"
                            InitialValue="--Please Select--" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="RequiredFieldValidator4" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass="Profiletitletxt" ID="Label5" runat="server" Text="Request Date"
                            meta:resourcekey="Label5Resource1"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="dtpRequestDate" runat="server" AllowCustomText="false"
                            ShowPopupOnFocus="True" MarkFirstMatch="true" PopupDirection="TopRight" Skin="Vista"
                            Culture="English (United States)" meta:resourcekey="dtpRequestDateResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="grpsave"
                            runat="server" ControlToValidate="dtpRequestDate" Display="None" ErrorMessage="Please Enter Request Date"
                            meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="RequiredFieldValidator5" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass="Profiletitletxt" ID="Label7" runat="server" Text="Leave From"
                            meta:resourcekey="Label7Resource1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <telerik:RadDatePicker ID="dtpFromDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                            PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="English (United States)"
                            meta:resourcekey="dtpFromDateResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="grpsave"
                            runat="server" ControlToValidate="dtpFromDate" Display="None" ErrorMessage="Please Enter From Date"
                            meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="RequiredFieldValidator6" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                        <asp:Label CssClass="Profiletitletxt" ID="Label6" runat="server" Text="To" meta:resourcekey="Label6Resource1"></asp:Label>
                        &nbsp;<telerik:RadDatePicker ID="dtpToDate" runat="server" AllowCustomText="false"
                            MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                            Culture="English (United States)" meta:resourcekey="dtpToDateResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="grpsave"
                            runat="server" ControlToValidate="dtpToDate" Display="None" ErrorMessage="Please Enter To Date"
                            meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="RequiredFieldValidator7" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                        <asp:CompareValidator ID="CompareValidator1" ValidationGroup="grpsave" Display="None"
                            runat="server" ControlToValidate="dtpToDate" ControlToCompare="dtpFromDate" Operator="GreaterThanEqual"
                            Type="Date" ErrorMessage="To date should be greater than or equal to from date"
                            meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="CompareValidator1" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:CheckBox ID="chkHalfDay" runat="server" Text="Half Day" meta:resourcekey="chkHalfDayResource1" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:Label CssClass="Profiletitletxt" ID="Label8" runat="server" Text="Remarks" meta:resourcekey="Label8Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="320px" Height="60px"
                            meta:resourcekey="txtRemarksResource1"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="grpsave"
                            meta:resourcekey="btnSaveResource1" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" OnClientClick="return confirm('Are you sure you want to delete?');"
                            meta:resourcekey="btnDeleteResource1" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClearResource1" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <telerik:RadGrid ID="grdEmpLeaves" runat="server" AllowSorting="True" AllowPaging="True"
                            Width="100%" PageSize="15" Skin="Hay" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                            ShowFooter="True" meta:resourcekey="grdEmpLeavesResource1">
                            <SelectedItemStyle ForeColor="Maroon" />
                            <MasterTableView IsFilterItemExpanded="False" AutoGenerateColumns="False" DataKeyNames="LeaveID">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee" />
                                    <telerik:GridBoundColumn DataField="RequestDate" HeaderText="Request Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <telerik:GridBoundColumn DataField="LeaveName" HeaderText="Leave Type" />
                                    <telerik:GridBoundColumn DataField="FromDate" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <telerik:GridBoundColumn DataField="NoDays" HeaderText="No. Days" />
                                    <telerik:GridBoundColumn DataField="LeaveId" AllowFiltering="false" Visible="false" />
                                    <telerik:GridBoundColumn DataField="FK_EmployeeId" AllowFiltering="false" Visible="false" />
                                </Columns>
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            </MasterTableView>
                            <GroupingSettings CaseSensitive="False" />
                            <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
