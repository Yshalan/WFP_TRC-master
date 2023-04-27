<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EmpLeaves.ascx.vb" Inherits="Emp_userControls_WebUserControl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:UpdatePanel ID="pnlEmpLeaves" runat="server">
    <ContentTemplate>
        <table width="720" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <div id="divEmpLeaves" style="display: block">
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                <uc1:PageHeader ID="userCtrlEmpLeavesHeader" runat="server" HeaderText="Employee Leaves" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label CssClass="Profiletitletxt" ID="lblEmployee" runat="server" Text="Employee english name"
                                                    meta:resourcekey="lblEmployeeResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="RadCmbBxEmployee" MarkFirstMatch="True" Skin="Vista" AppendDataBoundItems="True"
                                                    runat="server" meta:resourcekey="RadCmbBxEmployeeResource1">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="--Please Select--" Value="0" runat="server" meta:resourcekey="RadComboBoxItemResource1" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <asp:RequiredFieldValidator ID="reqEmployeeId" runat="server" ControlToValidate="RadCmbBxEmployee"
                                                    Display="None" ErrorMessage="Please select employee english name" InitialValue="--Please Select--"
                                                    ValidationGroup="EmpLeavesGroup" meta:resourcekey="reqEmployeeIdResource1"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="ExtenderreqEmployeeId" runat="server" CssClass="AISCustomCalloutStyle"
                                                    TargetControlID="reqEmployeeId" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label CssClass="Profiletitletxt" ID="lblLeaveType" runat="server" Text="Leave type"
                                                    meta:resourcekey="lblLeaveTypeResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="RadCmbBxLeavesTypes" MarkFirstMatch="True" Skin="Vista"
                                                    runat="server" meta:resourcekey="RadCmbBxLeavesTypesResource1">
                                                </telerik:RadComboBox>
                                                <asp:RequiredFieldValidator ID="reqRadCmbBxLeavesTypes" runat="server" ControlToValidate="RadCmbBxLeavesTypes"
                                                    Display="None" ErrorMessage="Please select leave english name" InitialValue="--Please Select--"
                                                    ValidationGroup="EmpLeavesGroup" meta:resourcekey="reqRadCmbBxLeavesTypesResource1"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="ExtenderRadCmbBxLeavesTypes" runat="server" CssClass="AISCustomCalloutStyle"
                                                    TargetControlID="reqRadCmbBxLeavesTypes" WarningIconImageUrl="~/images/warning1.png"
                                                    Enabled="True">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label CssClass="Profiletitletxt" ID="lblRequestDate" runat="server" Text="Request date"
                                                    meta:resourcekey="lblRequestDateResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dtpRequestDate" runat="server" AllowCustomText="false"
                                                    MarkFirstMatch="true" Skin="Vista" Culture="English (United States)" meta:resourcekey="dtpRequestDateResource1">
                                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                    </Calendar>
                                                    <DateInput ID="DateInput1" runat="server" ToolTip="View request time" DateFormat="dd/M/yyyy" />
                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                    <DateInput ID="DateInput1" runat="server" ToolTip="View request time" DateFormat="dd/M/yyyy" /><DatePopupButton
                                                        CssClass="" HoverImageUrl="" ImageUrl="" />
                                                <DateInput ID="DateInput1" runat="server" ToolTip="View request time" DateFormat="dd/M/yyyy" /><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" /><DateInput ID="DateInput1" runat="server" ToolTip="View request time" DateFormat="dd/M/yyyy" /><DatePopupButton
                                                        CssClass="" HoverImageUrl="" ImageUrl="" /></telerik:RadDatePicker>
                                                <asp:RequiredFieldValidator ID="reqRequestDate" runat="server" ControlToValidate="dtpRequestDate"
                                                    Display="None" ErrorMessage="Please select request date" ValidationGroup="EmpLeavesGroup"
                                                    meta:resourcekey="reqRequestDateResource1"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="ExtenderreqRequestDate" runat="server" CssClass="AISCustomCalloutStyle"
                                                    TargetControlID="reqRequestDate" WarningIconImageUrl="~/images/warning1.png"
                                                    Enabled="True">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label CssClass="Profiletitletxt" ID="lblFromDate" runat="server" Text="Leave date"
                                                    meta:resourcekey="lblFromDateResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label CssClass="Profiletitletxt" ID="lblLeaveFromDate" runat="server" Text="From"
                                                                meta:resourcekey="lblLeaveFromDateResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="reqFromDate" runat="server" ControlToValidate="dtpFromDate"
                                                                Display="None" ErrorMessage="Please select start date" ValidationGroup="EmpLeavesGroup"
                                                                meta:resourcekey="reqFromDateResource1"></asp:RequiredFieldValidator>
                                                            <cc1:ValidatorCalloutExtender ID="ExtenderreqFromDate" runat="server" CssClass="AISCustomCalloutStyle"
                                                                TargetControlID="reqFromDate" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                                            </cc1:ValidatorCalloutExtender>
                                                            <telerik:RadDatePicker ID="dtpFromDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                                                Skin="Vista" Culture="English (United States)" meta:resourcekey="dtpFromDateResource1">
                                                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                                </Calendar>
                                                                <DateInput ID="DateInput3" runat="server" ToolTip="View request time" DateFormat="dd/M/yyyy" />
                                                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                                <DateInput ID="DateInput3" runat="server" ToolTip="View request time" DateFormat="dd/M/yyyy" /><DatePopupButton
                                                                    CssClass="" HoverImageUrl="" ImageUrl="" />
                                                            <DateInput ID="DateInput3" runat="server" ToolTip="View request time" DateFormat="dd/M/yyyy" /><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" /><DateInput ID="DateInput3" runat="server" ToolTip="View request time" DateFormat="dd/M/yyyy" /><DatePopupButton
                                                                    CssClass="" HoverImageUrl="" ImageUrl="" /></telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            <asp:Label CssClass="Profiletitletxt" ID="lblToDate" runat="server" Text="To" meta:resourcekey="lblToDateResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dtpToDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                                                Skin="Vista" Culture="English (United States)" meta:resourcekey="dtpToDateResource1">
                                                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                                </Calendar>
                                                                <DateInput ID="DateInput2" runat="server" ToolTip="View request time" DateFormat="dd/M/yyyy" />
                                                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                                <DateInput ID="DateInput2" runat="server" ToolTip="View request time" DateFormat="dd/M/yyyy" /><DatePopupButton
                                                                    CssClass="" HoverImageUrl="" ImageUrl="" />
                                                            <DateInput ID="DateInput2" runat="server" ToolTip="View request time" DateFormat="dd/M/yyyy" /><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" /><DateInput ID="DateInput2" runat="server" ToolTip="View request time" DateFormat="dd/M/yyyy" /><DatePopupButton
                                                                    CssClass="" HoverImageUrl="" ImageUrl="" /></telerik:RadDatePicker>
                                                            <asp:RequiredFieldValidator ID="reqToDate" runat="server" ControlToValidate="dtpToDate"
                                                                Display="None" ErrorMessage="Please select end date" ValidationGroup="EmpLeavesGroup"
                                                                meta:resourcekey="reqToDateResource1"></asp:RequiredFieldValidator>
                                                            <cc1:ValidatorCalloutExtender ID="ExtenderreqToDate" runat="server" CssClass="AISCustomCalloutStyle"
                                                                TargetControlID="reqToDate" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                                            </cc1:ValidatorCalloutExtender>
                                                            <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dteFromDate" ControlToValidate="dteToDate"
                                                                ErrorMessage="To Date should be greater than or equal to From Date" Display="None"
                                                                Operator="GreaterThanEqual" Type="Date" ValidationGroup="EmpLeavesGroup" meta:resourcekey="CVDateResource1"></asp:CompareValidator>
                                                            <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="vceCompareDate" runat="server"
                                                                Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                                            </cc1:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label is="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt" />
                                            </td>
                                            <td>
                                                <asp:FileUpload ID="fuAttachFile" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label CssClass="Profiletitletxt" ID="lblRemarks" runat="server" Text="Remarks"
                                                    meta:resourcekey="lblRemarksResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" meta:resourcekey="txtRemarksResource1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label CssClass="Profiletitletxt" ID="lblHalfDay" runat="server" Text="Half day"
                                                    meta:resourcekey="lblHalfDayResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chckHalfDay" runat="server" meta:resourcekey="chckHalfDayResource1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="EmpLeavesGroup"
                                                    meta:resourcekey="btnSaveResource1" />
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" meta:resourcekey="btnDeleteResource1" />
                                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClearResource1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:GridView ID="dgrdVwEmpLeaves" runat="server" AllowPaging="True" AllowSorting="True" PageSize="25"
                                                    AutoGenerateColumns="False" CssClass="GridViewStyle" Width="650px" meta:resourcekey="dgrdVwEmpLeavesResource1">
                                                    <RowStyle CssClass="RowStyle" />
                                                    <Columns>
                                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkRow" runat="server" meta:resourcekey="chkRowResource1" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee English Name" SortExpression="EmployeeName"
                                                            meta:resourcekey="TemplateFieldResource2">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEmployeeName" runat="server" Text='<%# Bind("EmployeeName") %>'
                                                                    OnClick="lnkEmployeeName_Click" meta:resourcekey="lnkEmployeeNameResource1"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="LeaveName" HeaderText="Leave Type" SortExpression="LeaveName"
                                                            meta:resourcekey="BoundFieldResource1" />
                                                        <asp:BoundField DataField="FromDate" HeaderText="Start Date" SortExpression="FromDate"
                                                            DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="BoundFieldResource2" />
                                                        <asp:BoundField DataField="ToDate" HeaderText="End Date" SortExpression="ToDate"
                                                            DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="BoundFieldResource3" />
                                                        <asp:CheckBoxField DataField="IsHalfDay" HeaderText="Half Day" SortExpression="IsHalfDay"
                                                            meta:resourcekey="CheckBoxFieldResource1" />
                                                        <asp:TemplateField Visible="False" meta:resourcekey="TemplateFieldResource3">
                                                            <ItemTemplate>
                                                                <asp:Label CssClass="Profiletitletxt" ID="lblLeaveId" runat="server" Text='<%# Bind("LeaveId") %>'
                                                                    meta:resourcekey="lblLeaveIdResource1"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="PagerStyle" />
                                                    <HeaderStyle CssClass="HeaderStyle" />
                                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
