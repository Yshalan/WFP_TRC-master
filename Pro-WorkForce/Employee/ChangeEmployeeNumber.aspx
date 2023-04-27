<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="ChangeEmployeeNumber.aspx.vb" Inherits="Employee_ChangeEmployeeNumber"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" runat="server" />

            <div class="row">
                <div class="col-md-12">
                    <uc2:PageFilter ID="EmployeeFilterUC" runat="server" HeaderText="Employee Filter"
                        ValidationGroup="grpChangeNo." />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="lblNewNo" runat="server" Text="New Employee No."
                        meta:resourcekey="lblNewNoResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtNewEmpNo" runat="server" meta:resourcekey="txtNewEmpNoResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtNewEmpNo" runat="server" ControlToValidate="txtNewEmpNo"
                        Display="None" ErrorMessage="Please Insert New Employee Number" ValidationGroup="grpChangeNo"
                        meta:resourcekey="rfvtxtNewEmpNoResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceNewEmpNo" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvtxtNewEmpNo" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="lblReason" runat="server" Text="Reason"
                        meta:resourcekey="lblReasonResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" meta:resourcekey="txtReasonResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvReason" runat="server" ControlToValidate="txtReason"
                        Display="None" ErrorMessage="Please Insert Reason" ValidationGroup="grpChangeNo"
                        meta:resourcekey="rfvReasonResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceReason" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvReason" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="grpChangeNo"
                        meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button"
                        Text="Delete" meta:resourcekey="btnDeleteResource1" Visible="false" />
                    <asp:Button ID="btnClear" runat="server" CausesValidation="False" CssClass="button"
                        Text="Clear" meta:resourcekey="btnClearResource1" />
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblFromDate" runat="server" Text="From Date"  meta:resourcekey="lblFromDateResource1"></asp:Label>
                </div>
                <div class="col-md-3">
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
                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="dtpFromDate"
                        Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="grpFilter"
                        meta:resourcekey="rfvFromDateResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceFromDate" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvFromDate" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblToDate" runat="server" Text="ToDate" meta:resourcekey="lblToDateResource1"></asp:Label>
                </div>
                <div class="col-md-3">
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
                    <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="dtpToDate"
                        Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="grpFilter"
                        meta:resourcekey="rfvToDateResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceToDate" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvToDate" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                    <asp:CompareValidator ID="CVFilterDate" Display="None" runat="server" ControlToValidate="dtpToDate"
                        ControlToCompare="dtpFromDate" Operator="GreaterThanEqual" Type="Date" ErrorMessage="To date should be greater than or equal to from date"
                        meta:resourcekey="CVFilterDateResource1" ValidationGroup="empleave"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender ID="vceFilterDate" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="CVFilterDate" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnFilter" runat="server" Text="Filter" ValidationGroup="grpFilter" />
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-4">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdChangeNo"
                        Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadGrid ID="dgrdChangeNo" runat="server" AllowSorting="True" AllowPaging="True"
                        Width="100%" PageSize="15" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        ShowFooter="True" meta:resourcekey="dgrdChangeNoResource1">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="EmpNumberlogId">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn1" meta:resourcekey="GridTemplateColumnResource1">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                                    meta:resourcekey="GridBoundColumnResource1" />
                                <telerik:GridBoundColumn DataField="OldEmpNo" HeaderText="Old Employee No." UniqueName="OldEmpNo"
                                    meta:resourcekey="GridBoundColumnResource2" />
                                <telerik:GridBoundColumn DataField="Reason" HeaderText="Reason" UniqueName="Reason"
                                    meta:resourcekey="GridBoundColumnResource3" />
                                <telerik:GridBoundColumn DataField="NewEmpNo" HeaderText="New Employee No." UniqueName="NewEmpNo"
                                    meta:resourcekey="GridBoundColumnResource4" />
                                <telerik:GridBoundColumn DataField="CREATED_DATE" HeaderText="Change Date/Time" UniqueName="CREATED_DATE"
                                    DataFormatString="{0:dd/MM/yyyy HH:MM}"
                                    meta:resourcekey="GridBoundColumnResource6" />
                                <telerik:GridBoundColumn DataField="EmpNumberlogId" AllowFiltering="False" Visible="False"
                                    UniqueName="EmpNumberlogId" meta:resourcekey="GridBoundColumnResource5" />
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnEmployeeNameAr" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton runat="server" CausesValidation="False" CommandName="FilterRadGrid"
                                            ImagePosition="Right" ImageUrl="~/images/RadFilter.gif" Owner="" Text="Apply filter"
                                            meta:resourcekey="RadToolBarButtonResource1">
                                        </telerik:RadToolBarButton>
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                        </MasterTableView><SelectedItemStyle ForeColor="Maroon" />
                    </telerik:RadGrid>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
