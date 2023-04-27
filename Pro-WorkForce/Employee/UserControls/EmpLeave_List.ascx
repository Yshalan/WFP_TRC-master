<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EmpLeave_List.ascx.vb"
    Inherits="Emp_UserControls_EmpLeave_List" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Employee Leaves" />
<asp:UpdatePanel ID="Update1" runat="server">
    <ContentTemplate>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="Label7" runat="server" Text="From" meta:resourcekey="Label7Resource1"></asp:Label>
                    </div>
                <div class="col-md-4">
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dtpFromDate"
                        Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="grpsave"
                        meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="RequiredFieldValidator6" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
        <div class="row">
            <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="Label6" runat="server" Text="To" meta:resourcekey="Label6Resource1"></asp:Label>
                </div>
            <div class="col-md-4">
                   <telerik:RadDatePicker ID="dtpToDate" runat="server" AllowCustomText="false"
                        MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                        Culture="English (United States)" meta:resourcekey="dtpToDateResource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                </div>
            <div class="col-md-1">
                    <asp:Button ID="btnApply" runat="server" CssClass="button" Text="Apply" ValidationGroup="grpsave"
                        meta:resourcekey="btnApplyResource1" />
                </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="dtpToDate"
                        Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="grpsave"
                        meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="RequiredFieldValidator7" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="dtpFromDate"
                        ControlToValidate="dtpToDate" Display="None" ErrorMessage="To date should be greater than or equal to from date"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpsave" meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="CompareValidator1" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadGrid ID="grdEmpLeaves" runat="server" AllowMultiRowSelection="True" AllowPaging="True"
                        AllowSorting="True" GridLines="None" PageSize="15" ShowFooter="True" ShowStatusBar="True"
                         meta:resourcekey="grdEmpLeavesResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView AutoGenerateColumns="False" IsFilterItemExpanded="False">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee" meta:resourcekey="GridBoundColumnResource1" />
                                <telerik:GridBoundColumn DataField="RequestDate" DataFormatString="{0:dd/MM/yyyy}"
                                    HeaderText="Request Date" meta:resourcekey="GridBoundColumnResource2" />
                                <telerik:GridBoundColumn DataField="LeaveName" HeaderText="Leave Type" meta:resourcekey="GridBoundColumnResource3" />
                                <telerik:GridBoundColumn DataField="FromDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="From Date"
                                    meta:resourcekey="GridBoundColumnResource4" />
                                <telerik:GridBoundColumn DataField="ToDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="To Date"
                                    meta:resourcekey="GridBoundColumnResource5" />
                                <telerik:GridBoundColumn DataField="NoDays" HeaderText="No. Days" meta:resourcekey="GridBoundColumnResource6" />
                                <telerik:GridBoundColumn AllowFiltering="false" DataField="LeaveId" Visible="false" />
                                <telerik:GridBoundColumn AllowFiltering="false" DataField="FK_EmployeeId" Visible="false" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
    </ContentTemplate>
</asp:UpdatePanel>
