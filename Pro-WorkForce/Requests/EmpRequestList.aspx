<%@ Page Title="" Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="EmpRequestList.aspx.vb" Inherits="Requests_DirectManager_EmpRequests"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <uc1:PageHeader ID="DMLeaveApprovalHeader" runat="server" />
    </center>
    <asp:MultiView ID="mvEmpRequests" runat="server" ActiveViewIndex="0">
        <asp:View ID="viewRequests" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblRequestType" runat="server" Text="Request Type" CssClass="Profiletitletxt"
                            meta:resourcekey="lblRequestTypeResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblRequestType" runat="server" RepeatDirection="Horizontal"
                            CssClass="Profiletitletxt" meta:resourcekey="rblRequestTypeResource1">
                            <asp:ListItem Value="1" Text="Permissions" meta:resourcekey="ListItemResource1"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Leaves" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvrblRequestType" runat="server" ControlToValidate="rblRequestType"
                            Display="None" ErrorMessage="Please Select Request Type" ValidationGroup="GrpApply"
                            meta:resourcekey="rfvrblRequestTypeResource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                            TargetControlID="rfvrblRequestType">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFromDate" runat="server" Text="From Date" CssClass="Profiletitletxt"
                            meta:resourcekey="lblFromDateResource1"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="raddateFromDate" runat="server" Culture="en-US" Width="180px"
                            meta:resourcekey="raddateFromDateResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="raddateFromDate"
                            Display="None" ErrorMessage="Please Select Date" ValidationGroup="GrpApply" meta:resourcekey="rfvFromDateResource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="vceFromDate" runat="server" Enabled="True" TargetControlID="rfvFromDate">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td width="138px">
                        <asp:Label ID="lblToDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblToDateResource1"
                            Text="To Date"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="raddateToDate" runat="server" Culture="en-US" Width="180px"
                            meta:resourcekey="raddateToDateResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="raddateToDate"
                            Display="None" ErrorMessage="Please Select Date" ValidationGroup="GrpApply" meta:resourcekey="rfvToDateResource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="vceToDate" runat="server" Enabled="True" TargetControlID="rfvToDate">
                        </cc1:ValidatorCalloutExtender>
                        <br />
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="raddateFromDate"
                            ControlToValidate="raddateToDate" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                            Operator="GreaterThanEqual" Type="Date" ValidationGroup="GrpApply" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                        <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender2"
                            runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnApplyFilter" runat="server" Text="Apply Filter" CssClass="button"
                            ValidationGroup="GrpApply" meta:resourcekey="btnApplyFilteResource1" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClearResource1" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <telerik:RadFilter runat="server" ID="RadFilter_Leave" FilterContainerID="dgrdEmpLeaveRequests"
                            Skin="Hay" ShowApplyButton="False" Visible="false" meta:resourcekey="RadFilter_LeaveResource1" />
                        <telerik:RadGrid ID="dgrdEmpLeaveRequests" runat="server" AllowSorting="True" AllowPaging="True"
                            Skin="Hay" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                            PageSize="25" ShowFooter="True" Visible="False" meta:resourcekey="dgrdEmpLeaveRequestsResource1">
                            <GroupingSettings CaseSensitive="False" />
                            <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="LeaveId,AttachedFile">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Emp No" UniqueName="EmployeeNo"
                                        meta:resourcekey="GridBoundColumnResource9" />
                                    <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                                        meta:resourcekey="GridBoundColumnResource2" />
                                    <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Employee Name"
                                        UniqueName="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource2" />
                                    <telerik:GridBoundColumn DataField="RequestDate" DataFormatString="{0:dd/MM/yyyy}"
                                        HeaderText="Request Date" meta:resourcekey="GridBound1ColumnResource1" UniqueName="RequestDate" />
                                    <telerik:GridBoundColumn DataField="LeaveName" HeaderText="Leave Type" UniqueName="LeaveName"
                                        meta:resourcekey="GridBoundColumnResource27" />
                                    <telerik:GridBoundColumn DataField="FromDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="From Date"
                                        meta:resourcekey="GridBound4ColumnResource1" UniqueName="FromDate" />
                                    <telerik:GridBoundColumn DataField="ToDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="To Date"
                                        meta:resourcekey="GridBound5ColumnResource1" UniqueName="ToDate" />
                                    <telerik:GridBoundColumn DataField="Days" HeaderText="No. Days" UniqueName="Days"
                                        meta:resourcekey="GridBoundColumnResource21" />
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="LeaveId" Visible="False"
                                        meta:resourcekey="GridBoundColumnResource3" UniqueName="LeaveId" />
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EmployeeId" Visible="False"
                                        meta:resourcekey="GridBoundColumnResource4" UniqueName="FK_EmployeeId" />
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="LeaveTypeId" Visible="False"
                                        meta:resourcekey="GridBoundColumnResource5" UniqueName="LeaveTypeId" />
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="AttachedFile" Visible="False"
                                        meta:resourcekey="GridBoundColumnResource6" UniqueName="AttachedFile" />
                                    <telerik:GridBoundColumn DataField="TotalBalance" HeaderText="Total Balance" UniqueName="TotalBalance"
                                        DataType="System.Decimal" DataFormatString="{0:C2}" meta:resourcekey="GridBoundColumnResource7" />
                                    <telerik:GridTemplateColumn HeaderText="Attached File" AllowFiltering="False" UniqueName="TemplateColumn"
                                        meta:resourcekey="GridTemplateColumnResource1">
                                        <ItemTemplate>
                                            <a id="lnbView" runat="server">
                                                <asp:Label ID="lblView" runat="server" Text="View" meta:resourcekey="lnbViewResource1" />
                                            </a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status" UniqueName="StatusName"
                                        meta:resourcekey="GridBoundColumnResource8" />
                                    <telerik:GridBoundColumn DataField="StatusNameArabic" HeaderText="Status" UniqueName="StatusNameArabic"
                                        meta:resourcekey="GridBoundColumnResource8" />
                                </Columns>
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
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
                        <telerik:RadFilter runat="server" ID="RadFilter_Perm" FilterContainerID="dgrdEmpPermRequest"
                            Skin="Hay" ShowApplyButton="False" Visible="false" meta:resourcekey="RadFilter_PermResource1" />
                        <telerik:RadGrid ID="dgrdEmpPermRequest" runat="server" AllowSorting="True" AllowPaging="True"
                            Skin="Hay" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                            PageSize="25" ShowFooter="True" Visible="False" meta:resourcekey="dgrdEmpPermRequestResource1">
                            <GroupingSettings CaseSensitive="False" />
                            <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="PermissionId,AttachedFile">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Emp No" UniqueName="EmployeeNo"
                                        meta:resourcekey="GridBoundColumnResource9" />
                                    <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                                        meta:resourcekey="GridBoundColumnResource10" />
                                    <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Employee Name"
                                        UniqueName="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource10" />
                                    <telerik:GridBoundColumn DataField="PermName" HeaderText="Permission Type" UniqueName="PermName"
                                        meta:resourcekey="GridBoundColumnResource11" />
                                    <telerik:GridBoundColumn DataField="PermArabicName" HeaderText="Permission Type"
                                        UniqueName="PermArabicName" meta:resourcekey="GridBoundColumnResource11" />
                                    <telerik:GridBoundColumn DataField="PermDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Start Date"
                                        UniqueName="PermDate" meta:resourcekey="GridBoundColumnResource12" />
                                    <telerik:GridBoundColumn DataField="PermEndDate" DataFormatString="{0:dd/MM/yyyy}"
                                        HeaderText="End Date" UniqueName="PermEndDate" meta:resourcekey="GridBoundColumnResource13" />
                                    <telerik:GridBoundColumn DataField="FromTime" DataFormatString="{0:HH:mm}" HeaderText="From Time"
                                        UniqueName="FromTime" meta:resourcekey="GridBoundColumnResource14" />
                                    <telerik:GridBoundColumn DataField="ToTime" DataFormatString="{0:HH:mm}" HeaderText="To Time"
                                        UniqueName="ToTime" meta:resourcekey="GridBoundColumnResource15" />
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="PermissionId" Visible="False"
                                        UniqueName="PermissionId" meta:resourcekey="GridBoundColumnResource16" />
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EmployeeId" Visible="False"
                                        UniqueName="FK_EmployeeId" meta:resourcekey="GridBoundColumnResource17" />
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="PermTypeId" Visible="False"
                                        UniqueName="PermTypeId" meta:resourcekey="GridBoundColumnResource18" />
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="Remarks" Visible="False"
                                        UniqueName="Remarks" meta:resourcekey="GridBoundColumnResource19" />
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="IsForPeriod" Visible="False"
                                        UniqueName="IsForPeriod" meta:resourcekey="GridBoundColumnResource20" />
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="Days" Visible="False"
                                        UniqueName="Days" meta:resourcekey="GridBoundColumnResource21" />
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="PermissionOption" Visible="False"
                                        UniqueName="PermissionOption" meta:resourcekey="GridBoundColumnResource22" />
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFlexible" Visible="False"
                                        UniqueName="IsFlexible" meta:resourcekey="GridBoundColumnResource23" />
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFullDay" Visible="False"
                                        UniqueName="IsFullDay" meta:resourcekey="GridBoundColumnResource24" />
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="FlexibilePermissionDuration"
                                        Visible="False" UniqueName="FlexibilePermissionDuration" meta:resourcekey="GridBoundColumnResource25" />
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="AttachedFile" Visible="False"
                                        meta:resourcekey="GridBoundColumnResource26" UniqueName="AttachedFile" />
                                    <telerik:GridTemplateColumn HeaderText="Attached File" AllowFiltering="False" UniqueName="TemplateColumn"
                                        meta:resourcekey="GridTemplateColumnResource3">
                                        <ItemTemplate>
                                            <a id="lnbView" runat="server" onclick="View">
                                                <asp:Label ID="lblView" runat="server" Text="View" meta:resourcekey="lnbViewResource1" />
                                            </a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status" meta:resourcekey="GridBoundColumnResource30"
                                        UniqueName="StatusName" />
                                    <telerik:GridBoundColumn DataField="StatusNameArabic" HeaderText="Status" meta:resourcekey="GridBoundColumnResource30"
                                        UniqueName="StatusNameArabic" />
                                </Columns>
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                <CommandItemTemplate>
                                    <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                        Skin="Hay" meta:resourcekey="RadToolBar1Resource2">
                                        <Items>
                                            <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource2"
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
        </asp:View>
        <asp:View ID="viewLeaveRequest" runat="server">
            <asp:UpdatePanel ID="Update1" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td width="136px">
                                <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="Leave Type"
                                    meta:resourcekey="Label4Resource1"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ddlLeaveType" runat="server" AppendDataBoundItems="True"
                                    MarkFirstMatch="True" Skin="Vista" Width="200px" Enabled="False" meta:resourcekey="ddlLeaveTypeResource1">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="--Please Select--" meta:resourcekey="RadComboBoxItemResource1"
                                            Owner="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="Request Date"
                                    meta:resourcekey="Label5Resource1"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dtpRequestDate" runat="server" AllowCustomText="false"
                                    Enabled="False" Culture="en-US" MarkFirstMatch="true" PopupDirection="TopRight"
                                    ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpRequestDateResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                        Width="">
                                    </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" CssClass="Profiletitletxt" Text="Leave From"
                                    meta:resourcekey="Label7Resource1"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadDatePicker ID="dtpFromDate" runat="server" AllowCustomText="false" Culture="en-US"
                                    MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                                    Enabled="False" meta:resourcekey="dtpFromDateResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                        Width="">
                                    </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                                <asp:Label ID="Label6" runat="server" CssClass="Profiletitletxt" meta:resourcekey="Label6Resource1"
                                    Text="To"></asp:Label>
                                &nbsp;<telerik:RadDatePicker ID="dtpToDate" runat="server" AllowCustomText="false"
                                    Culture="en-US" MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True"
                                    Skin="Vista" Enabled="False" meta:resourcekey="dtpToDateResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                        Width="">
                                    </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" is="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblAttachFileResource1" />
                            </td>
                            <td>
                                <asp:FileUpload ID="fuAttachFile" runat="server" Enabled="False" meta:resourcekey="fuAttachFileResource1" />
                                <a id="lnbLeaveFile" runat="server" visible="False">
                                    <asp:Label ID="lblView" runat="server" Text="View" meta:resourcekey="lblViewResource1" />
                                </a>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="Label8" runat="server" CssClass="Profiletitletxt" Text="Remarks" meta:resourcekey="Label8Resource1"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemarks" runat="server" Height="60px" TextMode="MultiLine" Width="320px"
                                    Enabled="False" meta:resourcekey="txtRemarksResource1"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <asp:Button ID="btnLeaveCancel" runat="server" CausesValidation="False" CssClass="button"
                                    Text="Cancel" meta:resourcekey="btnLeaveCancelResource1" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="viewPermissionRequest" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="100%" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <div id="divEmpPermissions" style="display: block" width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td width="138px">
                                                            <asp:Label CssClass="Profiletitletxt" ID="lblPermission" runat="server" Text="Type"
                                                                meta:resourcekey="lblPermissionResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="RadCmpPermissions" MarkFirstMatch="True" Skin="Vista" ToolTip="View types of employee permission"
                                                                runat="server" Enabled="False" meta:resourcekey="RadCmpPermissionsResource1">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="radBtnOneDay" Text="One Time Permission" Checked="True" runat="server"
                                                                AutoPostBack="True" GroupName="LeaveGroup" Enabled="False" meta:resourcekey="radBtnOneDayResource1" />
                                                            <asp:RadioButton ID="radBtnPeriod" Text="Permission For Period" runat="server" AutoPostBack="True"
                                                                GroupName="LeaveGroup" Enabled="False" meta:resourcekey="radBtnPeriodResource1" />
                                                        </td>
                                                    </tr>
                                                    <asp:Panel ID="PnlOneDayLeave" runat="server" meta:resourcekey="PnlOneDayLeaveResource1">
                                                        <tr>
                                                            <td>
                                                                <asp:Label CssClass="Profiletitletxt" ID="lblPermissionDate" runat="server" Text="Date"
                                                                    meta:resourcekey="lblPermissionDateResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label CssClass="Profiletitletxt" ID="lblAtDate" runat="server" Text="At" meta:resourcekey="lblAtDateResource1"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadDatePicker ID="dtpPermissionDate" AllowCustomText="false" MarkFirstMatch="true"
                                                                                Skin="Vista" runat="server" Culture="en-US" Enabled="False" meta:resourcekey="dtpPermissionDateResource1">
                                                                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                                                </Calendar>
                                                                                <DateInput DateFormat="dd/MM/yyyy" ToolTip="View permission date" DisplayDateFormat="dd/MM/yyyy"
                                                                                    LabelCssClass="" Width="">
                                                                                </DateInput>
                                                                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                                            </telerik:RadDatePicker>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlPeriodLeave" Visible="False" runat="server" meta:resourcekey="pnlPeriodLeaveResource1">
                                                        <tr>
                                                            <td>
                                                                <asp:Label CssClass="Profiletitletxt" ID="lblDateFrom" runat="server" Text="Date"
                                                                    meta:resourcekey="lblDateFromResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label CssClass="Profiletitletxt" ID="Label2" runat="server" Text="From" Enabled="False"
                                                                                meta:resourcekey="Label2Resource1"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <telerik:RadDatePicker ID="dtpStartDatePerm" ToolTip="Click" AllowCustomText="false"
                                                                                MarkFirstMatch="true" Skin="Vista" runat="server" Culture="en-US" Enabled="False"
                                                                                meta:resourcekey="dtpStartDatePermResource1">
                                                                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                                                </Calendar>
                                                                                <DateInput DateFormat="dd/MM/yyyy" ToolTip="View start date permission" DisplayDateFormat="dd/MM/yyyy"
                                                                                    LabelCssClass="" Width="">
                                                                                </DateInput>
                                                                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                                            </telerik:RadDatePicker>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label CssClass="Profiletitletxt" ID="lblEndDate" runat="server" Text="To" meta:resourcekey="lblEndDateResource1"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <telerik:RadDatePicker ID="dtpEndDatePerm" AllowCustomText="false" MarkFirstMatch="true"
                                                                                Skin="Vista" runat="server" AutoPostBack="True" Culture="en-US" Enabled="False"
                                                                                meta:resourcekey="dtpEndDatePermResource1">
                                                                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                                                </Calendar>
                                                                                <DateInput DateFormat="dd/MM/yyyy" ToolTip="View end date permission" AutoPostBack="True"
                                                                                    DisplayDateFormat="dd/MM/yyyy" LabelCssClass="" Width="">
                                                                                </DateInput>
                                                                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                                            </telerik:RadDatePicker>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </asp:Panel>
                                                    <tr id="trFullyDay" runat="server">
                                                        <td width="146px" runat="server">
                                                            <asp:Label ID="lblIsFullyDay" runat="server" Text="Is Fully Day" CssClass="Profiletitletxt" />
                                                        </td>
                                                        <td runat="server">
                                                            <asp:CheckBox ID="chckFullDay" runat="server" AutoPostBack="True" Enabled="False" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label CssClass="Profiletitletxt" ID="lblTimeFrom" runat="server" Text="Time"
                                                                meta:resourcekey="lblTimeFromResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Label CssClass="Profiletitletxt" ID="lblFrom" Text="From" runat="server" meta:resourcekey="lblFromResource1"></asp:Label>
                                                                    </td>
                                                                    <td align="center">
                                                                        <telerik:RadTimePicker ID="RadTPfromTime" runat="server" AllowCustomText="false"
                                                                            MarkFirstMatch="true" Skin="Vista" AutoPostBack="True" AutoPostBackControl="TimeView"
                                                                            Enabled="False" Culture="en-US" meta:resourcekey="RadTPfromTimeResource1">
                                                                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                                            </Calendar>
                                                                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                                            <TimeView CellSpacing="-1">
                                                                                <HeaderTemplate>
                                                                                    Time Picker
                                                                                </HeaderTemplate>
                                                                                <TimeTemplate>
                                                                                    <a runat="server" href="#"></a>
                                                                                </TimeTemplate>
                                                                            </TimeView>
                                                                            <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                                            <DateInput ToolTip="View start time" DateFormat="HH:mm" AutoPostBack="True" DisplayDateFormat="HH:mm"
                                                                                LabelCssClass="" Width="" />
                                                                        </telerik:RadTimePicker>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label CssClass="Profiletitletxt" ID="lblTo" runat="server" Text="To" meta:resourcekey="lblToResource1"></asp:Label>
                                                                    </td>
                                                                    <td align="center">
                                                                        <telerik:RadTimePicker ID="RadTPtoTime" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                                                            Skin="Vista" AutoPostBack="True" AutoPostBackControl="TimeView" Enabled="False"
                                                                            Culture="en-US" meta:resourcekey="RadTPtoTimeResource1">
                                                                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                                            </Calendar>
                                                                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                                            <TimeView CellSpacing="-1">
                                                                                <HeaderTemplate>
                                                                                    Time Picker
                                                                                </HeaderTemplate>
                                                                                <TimeTemplate>
                                                                                    <a runat="server" href="#"></a>
                                                                                </TimeTemplate>
                                                                            </TimeView>
                                                                            <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                                            <DateInput ToolTip="View end time" DateFormat="HH:mm" AutoPostBack="True" DisplayDateFormat="HH:mm"
                                                                                LabelCssClass="" Width="" />
                                                                        </telerik:RadTimePicker>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label CssClass="Profiletitletxt" ID="lblPeriodInterval" runat="server" Text="Period"
                                                                meta:resourcekey="lblPeriodIntervalResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTimeDifference" ReadOnly="True" runat="server" Enabled="False"
                                                                meta:resourcekey="txtTimeDifferenceResource1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" is="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt"
                                                                meta:resourcekey="Label3Resource1" />
                                                        </td>
                                                        <td>
                                                            <asp:FileUpload ID="FileUpload1" runat="server" Enabled="False" meta:resourcekey="FileUpload1Resource1" />
                                                            <a id="A1" runat="server" visible="False">
                                                                <asp:Label ID="Label9" runat="server" Text="View" meta:resourcekey="Label9Resource1" />
                                                            </a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label CssClass="Profiletitletxt" ID="lblRemarks" runat="server" Text="Remarks"
                                                                meta:resourcekey="lblRemarksResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPermRemarks" runat="server" TextMode="MultiLine" Enabled="False"
                                                                meta:resourcekey="txtPermRemarksResource1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <asp:Panel ID="pnlTempHidRows" runat="server" Visible="False" meta:resourcekey="pnlTempHidRowsResource1">
                                                        <tr>
                                                            <td>
                                                                <asp:Label CssClass="Profiletitletxt" ID="lblIsSpecifiedDays" runat="server" Text="Specified days"
                                                                    meta:resourcekey="lblIsSpecifiedDaysResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chckSpecifiedDays" runat="server" Enabled="False" meta:resourcekey="chckSpecifiedDaysResource1" />
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label CssClass="Profiletitletxt" ID="lblDays" runat="server" Text="Days" meta:resourcekey="lblDaysResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDays" runat="server" Enabled="False" meta:resourcekey="txtDaysResource1"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label CssClass="Profiletitletxt" ID="lblIsFlexible" runat="server" Text="Flexible"
                                                                    meta:resourcekey="lblIsFlexibleResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chckIsFlexible" runat="server" Enabled="False" meta:resourcekey="chckIsFlexibleResource1" />
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label CssClass="Profiletitletxt" ID="lblIsDividable" runat="server" Text="Dividable"
                                                                    meta:resourcekey="lblIsDividableResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chckIsDividable" runat="server" Enabled="False" meta:resourcekey="chckIsDividableResource1" />
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </asp:Panel>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Button ID="btnPermCancel" runat="server" Text="Cancel" CssClass="button" CausesValidation="False"
                                                                meta:resourcekey="btnPermCancelResource1" />
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
        </asp:View>
    </asp:MultiView>
</asp:Content>
