<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="Emp_DeputyManager.aspx.vb" Inherits="Employee_Emp_DeputyManager"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Employee Leaves" />
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlManagerInfo" runat="server" GroupingText="Manager Information"
                meta:resourcekey="pnlManagerInfoResource1">
                <uc1:PageFilter ID="EmployeeFilterUC" runat="server" HeaderText="Employee Filter"
                    OneventEmployeeSelect="FillGrid" ValidationGroup="add" ShowOnlyManagers="true" />
            </asp:Panel>
            <asp:Panel ID="pnlEmpInfo" runat="server" GroupingText="Employee Information" meta:resourcekey="pnlEmpInfoResource1">
                <%-- <tr>
                    <td colspan="2">
                       
                    </td>
                </tr>--%>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblEmpNo" runat="server" Text="Employee No." CssClass="Profiletitletxt"
                            meta:resourcekey="lblEmpNoResource1" />
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEmpNo" runat="server" meta:resourcekey="txtEmpNoResource1" />
                        <asp:Button ID="btnRetrieve" runat="server" Text="Retrieve" CssClass="button" meta:resourcekey="btnRetrieveResource1" />
                        <asp:RequiredFieldValidator ID="rfvEmployeeNo" runat="server" ControlToValidate="txtEmpNo"
                            Display="None" ErrorMessage="Please enter Emp No." ValidationGroup="add" meta:resourcekey="rfvEmployeeNoResource1" />
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                            TargetControlID="rfvEmployeeNo" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtEmpNo" ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{0,50}$" runat="server"
                            ErrorMessage="Maximum 50 characters allowed." ValidationGroup="add" meta:resourcekey="txtEmpNoRousource"></asp:RegularExpressionValidator>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblEmpName" runat="server" Text="Employee Name" CssClass="Profiletitletxt"
                            meta:resourcekey="lblEmpNameResource1" />
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEmpName" runat="server" ReadOnly="True" meta:resourcekey="txtEmpNameResource1" />
                        <asp:RequiredFieldValidator ID="rfvEmployeeName" runat="server" ControlToValidate="txtEmpName"
                            Display="None" ErrorMessage="Please enter Emp Name" ValidationGroup="add" meta:resourcekey="rfvEmployeeNameResource1" />
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                            TargetControlID="rfvEmployeeName" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblFromDate" runat="server" CssClass="Profiletitletxt" Text="From Date"
                            meta:resourcekey="lblFromDateResource1" />
                    </div>
                    <div class="col-md-4">
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
                        <asp:RequiredFieldValidator SkinID="RequiredFieldValidator1" ID="RequiredFieldValidator1"
                            runat="server" ControlToValidate="dtpFromdate" Display="None" ErrorMessage="Please select From Date"
                            ValidationGroup="add" meta:resourcekey="reqFromdateResource1">
                        </asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" CssClass="AISCustomCalloutStyle"
                            TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/images/warning1.png"
                            Enabled="True" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="chckTemporary" runat="server" AutoPostBack="True" Text="Is Temporary"
                            meta:resourcekey="lblTemporaryResource1" />
                    </div>
                </div>
                <div class="row" id="trEndDate" runat="server" visible="False">
                    <div class="col-md-2">
                        <asp:Label ID="lblEndDate" runat="server" CssClass="Profiletitletxt" Text="To Date"
                            meta:resourcekey="lblEndDateResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker ID="dtpEndDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                            PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dtpFromdate" ControlToValidate="dtpEndDate"
                            ErrorMessage="To Date should be greater than or equal to From Date" Display="None"
                            Operator="GreaterThanEqual" Type="Date" ValidationGroup="add"
                            meta:resourcekey="CVDateResource1" />
                        <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender3"
                            runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                        <asp:RequiredFieldValidator SkinID="RequiredFieldValidator1" ID="reqdtpEndDate" runat="server"
                            ControlToValidate="dtpEndDate" Display="None" ErrorMessage="Please select End Date"
                            ValidationGroup="add" meta:resourcekey="reqreqdtpEndDateResource1">
                        </asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ExtreqdtpEndDate" runat="server" CssClass="AISCustomCalloutStyle"
                            TargetControlID="reqdtpEndDate" WarningIconImageUrl="~/images/warning1.png" Enabled="True" />
                    </div>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="add"
                        meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" CausesValidation="False"
                        meta:resourcekey="btnDeleteResource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">

                    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                    <div class="filterDiv">
                        <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="grdEmpDeputyManager"
                            ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" />
                    </div>
                    <telerik:RadGrid ID="grdEmpDeputyManager" runat="server" AllowSorting="True" AllowPaging="True"
                        Width="100%" PageSize="15" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        ShowFooter="True" meta:resourcekey="grdEmpDeputyManagerResource1"
                        OnItemCommand="grdEmpDeputyManager_ItemCommand">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False"
                            DataKeyNames="Id,FromDate,ToDate,CompanyArabicName,EntityArabicName,ManagerArabicName,DeputyArabicName,FK_ManagerId">
                            <CommandItemTemplate>
                                <telerik:RadToolBar Skin="Hay" runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                            ImageUrl="~/images/RadFilter.gif" meta:resourcekey="RadToolBarButtonResource1"
                                            runat="server" Owner="" />
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
                                <telerik:GridBoundColumn DataField="CompanyName" HeaderText="Company Name" meta:resourcekey="GridBoundColumnResource1"
                                    UniqueName="CompanyName" />
                                <telerik:GridBoundColumn DataField="EntityName" HeaderText="Entity Name" meta:resourcekey="GridBoundColumnResource2"
                                    UniqueName="EntityName" />
                                <telerik:GridBoundColumn DataField="ManagerName" HeaderText="Manager Name" meta:resourcekey="GridBoundColumnResource3"
                                    UniqueName="ManagerName" />
                                <telerik:GridBoundColumn DataField="DeputyName" HeaderText="Deputy Name" meta:resourcekey="GridBoundColumnResource4"
                                    UniqueName="DeputyName" />
                                <telerik:GridBoundColumn DataField="FromDate" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}"
                                    meta:resourcekey="GridBoundColumnResource5" UniqueName="FromDate" />
                                <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}"
                                    meta:resourcekey="GridBoundColumnResource6" UniqueName="ToDate" />
                                <telerik:GridBoundColumn DataField="Id" AllowFiltering="False" Visible="False" meta:resourcekey="GridBoundColumnResource7"
                                    UniqueName="Id" />
                                <telerik:GridBoundColumn DataField="FK_DeputyManagerId" AllowFiltering="False" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource8" UniqueName="FK_DeputyManagerId" />
                                <telerik:GridBoundColumn DataField="FK_CompanyId" AllowFiltering="False" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource9" UniqueName="FK_CompanyId" />
                                <telerik:GridBoundColumn DataField="FK_EntityId" AllowFiltering="False" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource10" UniqueName="FK_EntityId" />
                                <telerik:GridBoundColumn DataField="FK_ManagerId" AllowFiltering="False" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource11" UniqueName="FK_ManagerId" />
                                <telerik:GridBoundColumn DataField="CompanyArabicName" AllowFiltering="False" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource12" UniqueName="CompanyArabicName" />
                                <telerik:GridBoundColumn DataField="EntityArabicName" AllowFiltering="False" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource13" UniqueName="EntityArabicName" />
                                <telerik:GridBoundColumn DataField="ManagerArabicName" AllowFiltering="False" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource14" UniqueName="ManagerArabicName" />
                                <telerik:GridBoundColumn DataField="DeputyArabicName" AllowFiltering="False" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource15" UniqueName="DeputyArabicName" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                        </MasterTableView>
                        <SelectedItemStyle ForeColor="Maroon" />
                    </telerik:RadGrid>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
