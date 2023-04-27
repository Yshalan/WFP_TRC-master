<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EmpWorkSchedule.ascx.vb"
    Inherits="UserControls_EmpWorkSchedule" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:UpdatePanel runat="server" ID="upBUAccount">
    <ContentTemplate>
        <table width="600px">
            <tr>
                <td>
                    <uc1:PageHeader ID="PageHeader1" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <table cellspacing="0">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDateFrom0" runat="server" Text="From Date" 
                                                CssClass="Profiletitletxt" meta:resourcekey="lblDateFrom0Resource1"></asp:Label>
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
                                            <asp:Label ID="lblEndDate" runat="server" Text="To Date"  
                                                CssClass="Profiletitletxt" meta:resourcekey="lblEndDateResource1"></asp:Label>
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
                                            <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dteStartDate"
                                                ControlToValidate="dteEndDate" ErrorMessage="To Date should be greater than or equal to From Date"
                                                Display="None" Operator="GreaterThanEqual" Type="Date" 
                                                ValidationGroup="GrWS" meta:resourcekey="CVDateResource1"></asp:CompareValidator>
                                            <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender2"
                                                runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center" colspan="2">
                                            <asp:Button ID="ibtnSearch" runat="server" Text="Search" CssClass="button" 
                                                ValidationGroup="GrWS" meta:resourcekey="ibtnSearchResource1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid runat="server" ID="dgrdActive" AutoGenerateColumns="False" PageSize="25"
                                                Skin="Hay" AllowPaging="True" AllowSorting="True" 
                                                AllowFilteringByColumn="True" GridLines="None" 
                                                meta:resourcekey="dgrdActiveResource1">
                                                <MasterTableView CommandItemDisplay="Top" AllowFilteringByColumn="False">
                                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                    <Columns>
                                                        <%--       <telerik:GridTemplateColumn AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk" runat="server" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>--%>
                                                        <telerik:GridBoundColumn DataField="EmpWorkScheduleId" HeaderText="EmpWorkScheduleId"
                                                            SortExpression="EmpWorkScheduleId" Visible="false">
                                                        </telerik:GridBoundColumn>
                                                        <%-- <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName"
                                                            ShowFilterIcon="true" Resizable="false">
                                                        </telerik:GridBoundColumn>--%>
                                                        <telerik:GridBoundColumn DataField="ScheduleName" HeaderText="Schedule Name" AllowFiltering="true"
                                                            ShowFilterIcon="true" SortExpression="ScheduleName" Resizable="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ScheduleTypeName" HeaderText="Schedule Type"
                                                            SortExpression="ScheduleTypeName" />
                                                        <telerik:GridBoundColumn DataField="Active" HeaderText="Active" SortExpression="Active"
                                                            Visible="false" />
                                                        <telerik:GridBoundColumn DataField="FromDate" HeaderText="From Date" AllowFiltering="False"
                                                            DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="true" SortExpression="FromDate">
                                                            <HeaderStyle HorizontalAlign="left" />
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date" AllowFiltering="False"
                                                            DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="true" SortExpression="ToDate">
                                                            <HeaderStyle HorizontalAlign="left" />
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridCheckBoxColumn DataField="IsTemporary" SortExpression="IsTemporary"
                                                            HeaderText="Is Temporary" AllowFiltering="false" />
                                                    </Columns>
                                                    <CommandItemTemplate>
                                                    </CommandItemTemplate>
                                                </MasterTableView>
                                                <GroupingSettings CaseSensitive="False" />
                                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                                                    EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
