<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EmpActiveSchedule.ascx.vb"
    Inherits="Employee_UserControls_EmpActiveSchedule" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

    <uc1:PageHeader ID="PageHeader1" runat="server" />

<asp:UpdatePanel ID="Update1" runat="server">
    <ContentTemplate>

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblschedule" runat="server" Text="Active Schedule" CssClass="Profiletitletxt"
                        meta:resourcekey="lblscheduleResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblActiveSchedule" runat="server"></asp:Label>
                    <%--<telerik:RadComboBox ID="RadCmbSchedule" MarkFirstMatch="True" Skin="Vista" runat="server"
                        meta:resourcekey="RadCmbScheduleResource1">
                    </telerik:RadComboBox>--%>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblStartDate" runat="server" Text="Schedule Date" CssClass="Profiletitletxt"
                        meta:resourcekey="lblStartDateResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpFromDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                        PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US"
                        meta:resourcekey="dtpFromDateResource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                </div>
            </div>
            <asp:Panel ID="PnlOTEnddate" runat="server" Visible="False" meta:resourcekey="PnlOTEnddateResource1">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="Label4" runat="server" Text="End date"
                            meta:resourcekey="Label4Resource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker ID="dtpToDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                            PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="English (United States)"
                            meta:resourcekey="dtpToDateResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                    </div>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadGrid ID="grdEmpSchedule" runat="server" AllowMultiRowSelection="True"
                        AllowPaging="True" AllowSorting="True" GridLines="None" PageSize="15" ShowFooter="True"
                        ShowStatusBar="True"  meta:resourcekey="grdEmpPermissionsResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView AutoGenerateColumns="False" IsFilterItemExpanded="False" DataKeyNames="FromDate,ToDate">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ScheduleName" SortExpression="ScheduleName" HeaderText="Schedule Name"
                                    meta:resourcekey="GridBoundColumnResource1" UniqueName="ScheduleName" />
                                <telerik:GridBoundColumn DataField="ScheduleArabicName" SortExpression="ScheduleArabicName"
                                    HeaderText="Schedule Arabic Name" meta:resourcekey="GridBoundColumnResource2"
                                    UniqueName="ScheduleArabicName" />
                                <telerik:GridBoundColumn DataField="FromDate" SortExpression="FromDate" DataFormatString="{0:dd/MM/yyyy}"
                                    HeaderText="From Date" meta:resourcekey="GridBoundColumnResource3" UniqueName="FromDate" />
                                <telerik:GridBoundColumn DataField="ToDate" SortExpression="ToDate" DataFormatString="{0:dd/MM/yyyy}"
                                    HeaderText="To Date" meta:resourcekey="GridBoundColumnResource4" UniqueName="ToDate" />
                                <telerik:GridBoundColumn DataField="Remarks" SortExpression="Remarks" HeaderText="Schedule Remarks"
                                    meta:resourcekey="GridBoundColumnResource5" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
    </ContentTemplate>
</asp:UpdatePanel>
