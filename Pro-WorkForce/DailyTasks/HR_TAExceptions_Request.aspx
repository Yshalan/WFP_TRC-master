<%@ Page Title="" Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="HR_TAExceptions_Request.aspx.vb" Inherits="Definitions_HR_Emp_TAExceptions"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="EmployeeFilter"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <uc1:PageHeader ID="PageHeader1" runat="server" />
    </center>
    <asp:UpdatePanel ID="pnlTAExceptions" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td colspan="3">
                        <uc3:EmployeeFilter ID="EmployeeFilter1" runat="server" ValidationGroup="TaException" />
                    </td>
                </tr>
                <tr>
                    <td width="135px">
                        <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="From Date"
                            meta:resourcekey="Label4Resource1"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="dtpFromdate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                            PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US"
                            meta:resourcekey="dtpFromdateResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="reqFromdate" runat="server" ControlToValidate="dtpFromdate"
                            Display="None" ErrorMessage="Please select from date" ValidationGroup="TaException"
                            meta:resourcekey="reqFromdateResource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ExtenderPermissionDate" runat="server" TargetControlID="reqFromdate"
                            CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                            Enabled="True">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td width="135px">
                        <asp:Label ID="Label6" runat="server" CssClass="Profiletitletxt" Text="Is Temporary"
                            meta:resourcekey="Label6Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chckTemporary" runat="server" AutoPostBack="True" meta:resourcekey="chckTemporaryResource1" />
                    </td>
                    <td>
                    </td>
                </tr>
                <asp:Panel ID="pnlEndDate" runat="server" Visible="False" meta:resourcekey="pnlEndDateResource1">
                    <tr>
                        <td width="145px">
                            <asp:Label ID="lblEndDate" runat="server" CssClass="Profiletitletxt" Text="End date"
                                meta:resourcekey="lblEndDateResource1"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="dtpEndDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US"
                                meta:resourcekey="dtpEndDateResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                    Width="">
                                </DateInput>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="dtpEndDate"
                                Display="None" ErrorMessage="Please select to date" ValidationGroup="TaException"
                                meta:resourcekey="rfvToDateResource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvToDate"
                                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td>
                        <asp:Label ID="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt"
                            meta:resourcekey="lblAttachFileResource1" />
                    </td>
                    <td>
                        <asp:FileUpload ID="fuAttachFile" runat="server" />
                        <a id="lnbLeaveFile" target="_blank" runat="server" visible="false">
                            <asp:Label ID="lblView" runat="server" Text="View" meta:resourcekey="lblViewResource1" />
                        </a>&nbsp;
                        <asp:LinkButton ID="lnbRemove" runat="server" Text="Remove" Visible="false" meta:resourcekey="lnbRemoveResource1" />
                        <asp:Label ID="lblNoAttachedFile" runat="server" Text="No Attached File" Visible="false"
                            meta:resourcekey="lblNoAttachedFileResource1" />
                    </td>
                </tr>
                <tr>
                    <td width="135px">
                        <asp:Label ID="lblReason" runat="server" Text="Reason" CssClass="Profiletitletxt"
                            meta:resourcekey="lblReasonResource1" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtReason" runat="server" Rows="4" Columns="45" TextMode="MultiLine"
                            meta:resourcekey="txtReasonResource1" />
                        <asp:RequiredFieldValidator ID="rfvReason" runat="server" ControlToValidate="txtReason"
                            Display="None" ErrorMessage="Please enter reason" ValidationGroup="TaException"
                            meta:resourcekey="rfvReasonResource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvReason"
                            CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                            Enabled="True">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <center>
                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="110px" CssClass="button"
                                ValidationGroup="TaException" meta:resourcekey="btnSaveResource1" />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="110px" CssClass="button"
                                meta:resourcekey="btnDeleteResource1" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" Width="110px" CssClass="button"
                                meta:resourcekey="btnClearResource1" />
                        </center>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdTAExceptions"
                            Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                        <telerik:RadGrid runat="server" ID="dgrdTAExceptions" AllowSorting="True" AllowPaging="True"
                            Skin="Hay" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                            PageSize="25" ShowFooter="True" meta:resourcekey="dgrdTAExceptionsResource1">
                            <GroupingSettings CaseSensitive="False" />
                            <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="FK_EmployeeId,FromDate,ToDate">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource1" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee Number" SortExpression="EmployeeNo"
                                        UniqueName="EmployeeNo" meta:resourcekey="GridBoundColumnResource1">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName"
                                        UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource2">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Employee Arabic Name"
                                        SortExpression="EmployeeArabicName" UniqueName="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource3">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FK_EmployeeId" DataType="System.Int32" Visible="False"
                                        HeaderText="FK_EmployeeId" UniqueName="FK_EmployeeId" meta:resourcekey="GridBoundColumnResource4">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FromDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="From Date"
                                        UniqueName="FromDate" meta:resourcekey="GridBoundColumnResource5">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ToDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="To Date"
                                        UniqueName="ToDate" meta:resourcekey="GridBoundColumnResource6" />
                                </Columns>
                                <CommandItemTemplate>
                                    <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                        Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                        <Items>
                                            <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1"
                                                Owner="" />
                                        </Items>
                                    </telerik:RadToolBar>
                                </CommandItemTemplate>
                            </MasterTableView>
                            <SelectedItemStyle ForeColor="Maroon" />
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
