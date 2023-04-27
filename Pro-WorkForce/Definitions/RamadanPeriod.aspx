<%@ Page Title="Ramadan Period" Language="VB" Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="RamadanPeriod.aspx.vb" Inherits="Definitions_RamadanPeriod" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Ramadan Period" />
    <asp:UpdatePanel ID="pnlRamadanPeriod" runat="server">
        <ContentTemplate>
          
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblDateFrom" runat="server" CssClass="Profiletitletxt" Text="From Date"
                                        meta:resourcekey="lblDateFromResource1" />
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDatePicker ID="dteFromDate" runat="server" Culture="English (United States)"
                                        meta:resourcekey="dteFromDateResource1">
                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                            Width="">
                                        </DateInput><Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                            ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="ReqFVFromDate" runat="server" ControlToValidate="dteFromDate"
                                        Display="None" ErrorMessage="Select From Date " ValidationGroup="Ramadan" meta:resourcekey="ReqFVFromDateResource1" />
                                    <cc1:ValidatorCalloutExtender ID="VCEFromDate" runat="server" Enabled="True" TargetControlID="ReqFVFromDate"
                                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                                </div>
            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblDateTo" runat="server" CssClass="Profiletitletxt" Text="To Date"
                                        meta:resourcekey="lblDateToResource1" />
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDatePicker ID="dteToDate" runat="server" Culture="English (United States)"
                                        meta:resourcekey="dteFromDateResource1" Width="120px">
                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                            Width="">
                                        </DateInput><Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                            ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="ReqFVToDate" runat="server" ControlToValidate="dteToDate"
                                        Display="None" ErrorMessage="Select To Date " ValidationGroup="Ramadan" meta:resourcekey="ReqFVToDateResource1" />
                                    <cc1:ValidatorCalloutExtender ID="VCEToDate" runat="server" Enabled="True" TargetControlID="ReqFVToDate"
                                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                    <asp:CompareValidator ID="RamadanDates" runat="server" ControlToCompare="dteFromDate"
                                        ControlToValidate="dteToDate" ErrorMessage="To Date should be greater than or equal to From Date"
                                        Display="Dynamic" Operator="GreaterThanEqual" Type="Date" ValidationGroup="Ramadan"
                                        meta:resourcekey="RamadanDatesResource1"></asp:CompareValidator>
                              <%--      <cc1:ValidatorCalloutExtender TargetControlID="RamadanDates" ID="ValidatorCalloutExtender2"
                                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>--%>
                                </div>
                            </div>

                <div class="row">
                    <div class="col-md-12 text-center">
                        <asp:Button ID="ibtnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="Ramadan"
                            meta:resourcekey="ibtnSaveResource1" />
                        <asp:Button ID="ibtnDelete" runat="server" Text="Delete" CssClass="button" CausesValidation="False"
                            meta:resourcekey="ibtnDeleteResource1" />
                    </div>
                </div>
                        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                        <div class="filterDiv">
                            <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrRamadanPeriod"
                                ShowApplyButton="False" CssClass="RadFilter RadFilter_Default" meta:resourcekey="RadFilter1Resource1" />
                        </div>
            <div class="row">
                <div class="table-responsive">
                        <telerik:RadGrid runat="server" ID="dgrRamadanPeriod" AutoGenerateColumns="False"
                            PageSize="15"  AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True"
                            GridLines="None" meta:resourcekey="dgrRamadanPeriodResource1" Width="100%">
                            <MasterTableView CommandItemDisplay="Top" AllowFilteringByColumn="False" DataKeyNames="FromDate,ToDate,Id">
                                <CommandItemTemplate>
                                    <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick"
                                        meta:resourcekey="RadToolBar1Resource1">
                                        <Items>
                                            <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                                ImageUrl="~/images/RadFilter.gif" runat="server" meta:resourcekey="RadToolBarButtonResource1"
                                                Owner="" />
                                        </Items>
                                    </telerik:RadToolBar>
                                </CommandItemTemplate>
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="Id" HeaderText="Id" UniqueName="Id" Visible="false" />
                                    <telerik:GridBoundColumn DataField="FromDate" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}"
                                        meta:resourcekey="GridBoundColumnResource1" UniqueName="FromDate" />
                                    <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}"
                                        meta:resourcekey="GridBoundColumnResource2" UniqueName="ToDate" />
                                </Columns>
                            </MasterTableView>
                            <GroupingSettings CaseSensitive="False" />
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
