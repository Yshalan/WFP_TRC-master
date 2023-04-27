<%@ Page Title="Employee Permissions" Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/AdminMaster.master"
    AutoEventWireup="false" CodeFile="Emp_Permissions.aspx.vb" Inherits="Emp_PermissionsPage" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:UpdatePanel ID="pnlLeavesTypes" runat="server">
        <ContentTemplate>--%>
    <table width="750px" style="text-align: left">
        <tr>
            <td>
                <asp:MultiView ID="mvPermissions" runat="server" ActiveViewIndex="0">
                    <asp:View ID="AllPermissions" runat="server">
                        <tr>
                            <td colspan="2">
                                <uc1:PageHeader ID="PageHeader1" HeaderText="Employee Permissions" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <asp:LinkButton ID="lnkInfo" runat="server" OnClick="lnkInfo_Click" 
                                    meta:resourcekey="lnkInfoResource1">Add</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="background-color: #e2e2e2; border: solid 2px #d2d2d2;">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDateFrom0" runat="server" Text="From Date" 
                                                    meta:resourcekey="lblDateFrom0Resource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEndDate" runat="server" Text="To Date" 
                                                    meta:resourcekey="lblEndDateResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="Company" 
                                                    meta:resourcekey="Label3Resource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblArabicName" runat="server" Text="Entity name" 
                                                    meta:resourcekey="lblArabicNameResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Employee" 
                                                    meta:resourcekey="Label1Resource1"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
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
                                                    ErrorMessage="Select Start Date " ValidationGroup="GrPer" 
                                                    meta:resourcekey="ReqFVFromDayResource1"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="VCEFromDay" runat="server" Enabled="True" TargetControlID="ReqFVFromDay"
                                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                                </cc1:ValidatorCalloutExtender>
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
                                                    ValidationGroup="GrPer" meta:resourcekey="ReqEndDateResource1"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                                    TargetControlID="ReqEndDate" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                                </cc1:ValidatorCalloutExtender>
                                                <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dteStartDate"
                                                    ControlToValidate="dteEndDate" ErrorMessage="To Date should be greater than or equal to From Date"
                                                    Display="None" Operator="GreaterThanEqual" Type="Date" 
                                                    ValidationGroup="GrPer" meta:resourcekey="CVDateResource1"></asp:CompareValidator>
                                                <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender2"
                                                    runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="RadComboBoxCompany" runat="server" Width="150px" 
                                                    AutoPostBack="True" meta:resourcekey="RadComboBoxCompanyResource1">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="--Please Select--" 
                                                            meta:resourcekey="RadComboBoxItemResource1" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="RadComboBoxEntity" runat="server" Width="150px" 
                                                    AutoPostBack="True" meta:resourcekey="RadComboBoxEntityResource1">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="--Please Select--" 
                                                            meta:resourcekey="RadComboBoxItemResource2" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="RadComboBoxEmployee" runat="server" Width="150px" 
                                                    meta:resourcekey="RadComboBoxEmployeeResource1">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="--Please Select--" 
                                                            meta:resourcekey="RadComboBoxItemResource3" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;" colspan="5">
                                                <asp:Button ID="ibtnApply" runat="server" Text="Search" CssClass="Apply" 
                                                    ValidationGroup="GrPer" meta:resourcekey="ibtnApplyResource1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdEmployeePermission"
                                                    ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                                                <telerik:RadGrid ID="dgrdEmployeePermission" runat="server" AllowPaging="True" Skin="Hay"
                                                    GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" AutoGenerateColumns="False"
                                                    OnItemCommand="dgrdEmployeePermission_ItemCommand" ShowFooter="True" 
                                                    meta:resourcekey="dgrdEmployeePermissionResource1">
                                                    <GroupingSettings CaseSensitive="False" />
                                                    <ClientSettings AllowColumnsReorder="True" EnablePostBackOnRowClick="True" 
                                                        EnableRowHoverStyle="True" ReorderColumnsOnClient="True">
                                                        <Selecting AllowRowSelect="True" />
                                                    </ClientSettings>
                                                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="PermissionId,EmployeeNo,CompanyId,FK_EntityId,AttachedFile">
                                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                        <Columns>
                                                            <telerik:GridTemplateColumn AllowFiltering="False" 
                                                                meta:resourcekey="GridTemplateColumnResource1" UniqueName="TemplateColumn">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource1" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="PermissionId" 
                                                                HeaderText="PermissionId" meta:resourcekey="GridBoundColumnResource1" 
                                                                SortExpression="PermissionId" UniqueName="PermissionId" Visible="False">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="EmployeeNo" 
                                                                HeaderText="EmployeeNo" meta:resourcekey="GridBoundColumnResource2" 
                                                                SortExpression="EmployeeNo" UniqueName="EmployeeNo" Visible="False">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="CompanyId" 
                                                                HeaderText="CompanyId" meta:resourcekey="GridBoundColumnResource3" 
                                                                SortExpression="CompanyId" UniqueName="CompanyId" Visible="False">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EntityId" 
                                                                HeaderText="FK_EntityId" meta:resourcekey="GridBoundColumnResource4" 
                                                                SortExpression="FK_EntityId" UniqueName="FK_EntityId" Visible="False">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="PermDate" DataFormatString="{0:dd/MM/yyyy}" 
                                                                HeaderText="Permission Date" meta:resourcekey="GridBoundColumnResource5" 
                                                                Resizable="False" ShowFilterIcon="False" UniqueName="PermDate">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="PermEndDate" 
                                                                DataFormatString="{0:dd/MM/yyyy}" HeaderText="Permission End Date" 
                                                                meta:resourcekey="GridBoundColumnResource6" ShowFilterIcon="False" 
                                                                UniqueName="PermEndDate">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" 
                                                                meta:resourcekey="GridBoundColumnResource7" Resizable="False" 
                                                                SortExpression="EmployeeName" UniqueName="EmployeeName">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CompanyName" HeaderText="Company Name" 
                                                                meta:resourcekey="GridBoundColumnResource8" Resizable="False" 
                                                                SortExpression="CompanyName" UniqueName="CompanyName">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="EntityName" HeaderText="Entity Name" 
                                                                meta:resourcekey="GridBoundColumnResource9" Resizable="False" 
                                                                SortExpression="EntityName" UniqueName="EntityName">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="AttachedFile" 
                                                                HeaderText="AttachedFile" meta:resourcekey="GridBoundColumnResource10" 
                                                                SortExpression="AttachedFile" UniqueName="AttachedFile" Visible="False">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn meta:resourcekey="GridTemplateColumnResource2" 
                                                                UniqueName="TemplateColumn1">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDocumentName" runat="server" CausesValidation="False" 
                                                                        meta:resourcekey="lnkDocumentNameResource1" OnClick="lnkDocumentName_Click" 
                                                                        Text="Download Document"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                        <CommandItemTemplate>
                                                            <telerik:RadToolBar ID="RadToolBar1" runat="server" 
                                                                meta:resourcekey="RadToolBar1Resource1" OnButtonClick="RadToolBar1_ButtonClick" 
                                                                Skin="Hay">
                                                                <Items>
                                                                    <telerik:RadToolBarButton runat="server" CommandName="FilterRadGrid" 
                                                                        ImagePosition="Right" ImageUrl="~/images/RadFilter.gif"
                                                                        meta:resourcekey="RadToolBarButtonResource1" Text="Apply filter">
                                                                    </telerik:RadToolBarButton>
                                                                </Items>
                                                            </telerik:RadToolBar>
                                                        </CommandItemTemplate>
                                                    </MasterTableView>
                                                    <SelectedItemStyle ForeColor="Maroon" />
                                                </telerik:RadGrid>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </asp:View>
                    <asp:View ID="AddPermission" runat="server">
                        <table width="750px" style="text-align: left">
                            <tr>
                                <td style="text-align: right" colspan="3">
                                    <asp:LinkButton ID="lnkList" runat="server" OnClick="lnkList_Click" Text="Back" 
                                        meta:resourcekey="lnkListResource1"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                                        meta:resourcekey="TabContainer1Resource1">
                                        <cc1:TabPanel ID="Tab1" runat="server" HeaderText="Details" 
                                            meta:resourcekey="Tab1Resource1">
                                            <ContentTemplate>
                                                <table width="80%">
                                                    <tr>
                                                        <td colspan="2">
                                                            <uc1:PageHeader ID="PageHeader2" HeaderText="Add Employee Permissions" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Company" 
                                                                meta:resourcekey="Label2Resource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="RadComboBoxCompanyAdd" runat="server" Width="150px" 
                                                                AutoPostBack="True" meta:resourcekey="RadComboBoxCompanyAddResource1">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem runat="server" Text="--Please Select--" 
                                                                        meta:resourcekey="RadComboBoxItemResource4" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Entity name" 
                                                                meta:resourcekey="Label4Resource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="RadComboBoxEntityAdd" runat="server" Width="150px" 
                                                                AutoPostBack="True" meta:resourcekey="RadComboBoxEntityAddResource1">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem runat="server" Text="--Please Select--" 
                                                                        meta:resourcekey="RadComboBoxItemResource5" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Employee" 
                                                                meta:resourcekey="Label5Resource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="RadComboBoxEmployeeAdd" runat="server" Width="150px" 
                                                                meta:resourcekey="RadComboBoxEmployeeAddResource1">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem runat="server" Text="--Please Select--" 
                                                                        meta:resourcekey="RadComboBoxItemResource6" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                            <asp:RequiredFieldValidator ID="ReqValEmployeeAdd" runat="server" ControlToValidate="RadComboBoxEmployeeAdd"
                                                                InitialValue="--Please Select--" Display="None" ErrorMessage="Select Employee"
                                                                ValidationGroup="GrPerAdd" meta:resourcekey="ReqValEmployeeAddResource1"></asp:RequiredFieldValidator>
                                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                                                                TargetControlID="ReqValEmployeeAdd" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                                            </cc1:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="PermissionsType" 
                                                                meta:resourcekey="Label6Resource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="RadComboBoxPermissionsType" runat="server" 
                                                                Width="150px" meta:resourcekey="RadComboBoxPermissionsTypeResource1">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem runat="server" Text="--Please Select--" 
                                                                        meta:resourcekey="RadComboBoxItemResource7" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                            <asp:RequiredFieldValidator ID="ReqValPtype" runat="server" ControlToValidate="RadComboBoxPermissionsType"
                                                                InitialValue="--Please Select--" Display="None" ErrorMessage="Select Permission Type"
                                                                ValidationGroup="GrPerAdd" meta:resourcekey="ReqValPtypeResource1"></asp:RequiredFieldValidator>
                                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                                                                TargetControlID="ReqValPtype" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                                            </cc1:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblpermissionduration" runat="server" Text="Permission Duration" 
                                                                meta:resourcekey="lblpermissiondurationResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rdbpermissionduration" runat="server" RepeatDirection="Horizontal"
                                                                AutoPostBack="True" meta:resourcekey="rdbpermissiondurationResource1">
                                                                <asp:ListItem Text="One Day" Value="1" Selected="True" 
                                                                    meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                                <asp:ListItem Text="For Period" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                                <asp:ListItem Text="Specific Days" Value="3" 
                                                                    meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Permission Date" 
                                                                meta:resourcekey="Label7Resource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dtePermDate" runat="server" 
                                                                Culture="en-US" EnableTyping="False" Width="120px" 
                                                                meta:resourcekey="dtePermDateResource1">
                                                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                                                                </Calendar>
                                                                <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy"
                                                                    Width="" ReadOnly="True" LabelWidth="64px">
                                                                    <EmptyMessageStyle Resize="None" />
                                                                    <ReadOnlyStyle Resize="None" />
                                                                    <FocusedStyle Resize="None" />
                                                                    <DisabledStyle Resize="None" />
                                                                    <InvalidStyle Resize="None" />
                                                                    <HoveredStyle Resize="None" />
                                                                    <EnabledStyle Resize="None" />
                                                                </DateInput>
                                                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                            </telerik:RadDatePicker>
                                                            <asp:RequiredFieldValidator ID="ReqValdtePermDate" runat="server" 
                                                                ControlToValidate="dtePermDate" Display="None" 
                                                                ErrorMessage="Select Permission Date " ValidationGroup="GrPerAdd" 
                                                                meta:resourcekey="ReqValdtePermDateResource1"></asp:RequiredFieldValidator>
                                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True"
                                                                TargetControlID="ReqValdtePermDate" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                                            </cc1:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                    <div id="DivPENDDATE" runat="server">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPerEnddate" runat="server" Text="Permission End Date" 
                                                                    meta:resourcekey="lblPerEnddateResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadDatePicker ID="dtePermissionEnddate" runat="server" 
                                                                    Culture="en-US" EnableTyping="False" Width="120px" 
                                                                    meta:resourcekey="dtePermissionEnddateResource1">
                                                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                                                                    </Calendar>
                                                                    <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy"
                                                                        Width="" ReadOnly="True" LabelWidth="64px">
                                                                        <EmptyMessageStyle Resize="None" />
                                                                        <ReadOnlyStyle Resize="None" />
                                                                        <FocusedStyle Resize="None" />
                                                                        <DisabledStyle Resize="None" />
                                                                        <InvalidStyle Resize="None" />
                                                                        <HoveredStyle Resize="None" />
                                                                        <EnabledStyle Resize="None" />
                                                                    </DateInput>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                                </telerik:RadDatePicker>
                                                                <asp:RequiredFieldValidator ID="ReqValdtePermissionEnddate" runat="server" ControlToValidate="dtePermissionEnddate"
                                                                    Enabled="False" Display="None" ErrorMessage="Select Permission End Date " 
                                                                    ValidationGroup="GrPerAdd" 
                                                                    meta:resourcekey="ReqValdtePermissionEnddateResource1"></asp:RequiredFieldValidator>
                                                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True"
                                                                    TargetControlID="ReqValdtePermissionEnddate" CssClass="AISCustomCalloutStyle"
                                                                    WarningIconImageUrl="~/images/warning1.png">
                                                                </cc1:ValidatorCalloutExtender>
                                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="dtePermDate"
                                                                    ControlToValidate="dtePermissionEnddate" ErrorMessage="Permission End Date should be greater than  to Permission Date"
                                                                    Display="None" Operator="GreaterThan" Type="Date" 
                                                                    ValidationGroup="GrPerAdd" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                                                                <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender8"
                                                                    runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                                                </cc1:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>
                                                    </div>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblEndTime" runat="server" Text="From Time" 
                                                                meta:resourcekey="lblEndTimeResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadTimePicker ID="tpFromTime" runat="server" 
                                                                Culture="en-US" meta:resourcekey="tpFromTimeResource1" >
                                                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
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
                                                                <DateInput DateFormat="M/d/yyyy" DisplayDateFormat="M/d/yyyy" 
                                                                    Width="" LabelWidth="64px">
                                                                    <EmptyMessageStyle Resize="None" />
                                                                    <ReadOnlyStyle Resize="None" />
                                                                    <FocusedStyle Resize="None" />
                                                                    <DisabledStyle Resize="None" />
                                                                    <InvalidStyle Resize="None" />
                                                                    <HoveredStyle Resize="None" />
                                                                    <EnabledStyle Resize="None" />
                                                                </DateInput>
                                                            </telerik:RadTimePicker>
                                                            <asp:RequiredFieldValidator ID="ReqtpFromTime" runat="server" ControlToValidate="tpFromTime"
                                                                Display="None" ErrorMessage="Please Enter From Time" 
                                                                ValidationGroup="GrPerAdd" meta:resourcekey="ReqtpFromTimeResource1" />
                                                            <cc1:ValidatorCalloutExtender ID="VCEEndTime" runat="server" TargetControlID="ReqtpFromTime"
                                                                Enabled="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" Text="To Time" 
                                                                meta:resourcekey="Label9Resource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadTimePicker ID="tpToTime" runat="server" 
                                                                Culture="en-US" meta:resourcekey="tpToTimeResource1" >
                                                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
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
                                                                <DateInput DateFormat="M/d/yyyy" DisplayDateFormat="M/d/yyyy" 
                                                                    Width="" LabelWidth="64px">
                                                                    <EmptyMessageStyle Resize="None" />
                                                                    <ReadOnlyStyle Resize="None" />
                                                                    <FocusedStyle Resize="None" />
                                                                    <DisabledStyle Resize="None" />
                                                                    <InvalidStyle Resize="None" />
                                                                    <HoveredStyle Resize="None" />
                                                                    <EnabledStyle Resize="None" />
                                                                </DateInput>
                                                            </telerik:RadTimePicker>
                                                            <asp:RequiredFieldValidator ID="ReqtpToTime" runat="server" ControlToValidate="tpToTime"
                                                                Display="None" ErrorMessage="Please Enter To Time" 
                                                                ValidationGroup="GrPerAdd" meta:resourcekey="ReqtpToTimeResource1" />
                                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="ReqtpToTime"
                                                                Enabled="True" />
                                                        </td>
                                                    </tr>
                                                    <div id="divDays" runat="server">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" Text="Days" 
                                                                    meta:resourcekey="Label8Resource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBoxList ID="chkDays" runat="server" BorderStyle="None" RepeatColumns="3"
                                                                    RepeatDirection="Horizontal" Width="400px" 
                                                                    meta:resourcekey="chkDaysResource1">
                                                                </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                    </div>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks" 
                                                                meta:resourcekey="lblRemarksResource1" />
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtRemarks" runat="server" Style="margin-bottom: 0px" TextMode="MultiLine"
                                                                Width="300px" meta:resourcekey="txtRemarksResource1" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                                                                ValidationGroup="GrPerAdd" meta:resourcekey="btnSaveResource1" />
                                                            <asp:Button ID="btnDelete" runat="server" CausesValidation="False" 
                                                                CssClass="button"  OnClientClick="return confirm('Are you sure you want delete');"
                                                                Text="Delete" meta:resourcekey="btnDeleteResource1" />
                                                            <asp:Button ID="btnClear" runat="server" CausesValidation="False" CssClass="button"
                                                                Text="Clear" meta:resourcekey="btnClearResource1" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel ID="Tab2" runat="server" HeaderText="Permission Document" 
                                            Visible="False" meta:resourcekey="Tab2Resource1">
                                            <ContentTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                          <td colspan="2" align="center">
                                                            <telerik:RadUpload ID="RadUpload1" runat="server" ControlObjectsVisibility="None" 
                                                                  meta:resourcekey="RadUpload1Resource1">
                                                            </telerik:RadUpload>
                                                        </td>
                                                     
                                                    </tr>
                                                    <tr>
                                                       <td colspan="2" align="center">
                                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" 
                                                                meta:resourcekey="btnUploadResource1" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMessage" runat="server" Font-Italic="True" Font-Size="Small" 
                                                                ForeColor="#0033CC" meta:resourcekey="lblMessageResource1"></asp:Label>
                                                            &nbsp;
                                                            <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" 
                                                                OnClick="lnkRemove_Click" meta:resourcekey="lnkRemoveResource1"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <telerik:RadProgressArea ID="RadProgressArea1" runat="server" Skin="Windows7" ProgressIndicators="TotalProgressBar, TotalProgress, TotalProgressPercent, RequestSize, TimeElapsed, TimeEstimated, TransferSpeed"
                                                                Language="" meta:resourcekey="RadProgressArea1Resource1">
                                                                <Localization Uploaded="Uploaded"></Localization>
                                                            </telerik:RadProgressArea>
                                                            <telerik:RadProgressManager ID="RadProgressManager1" runat="server" 
                                                                meta:resourcekey="RadProgressManager1Resource1" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                    </cc1:TabContainer>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
