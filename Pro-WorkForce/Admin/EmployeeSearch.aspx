<%@ Page Language="VB" Title="Advanced Search" StylesheetTheme="Default" MasterPageFile="~/Default/EmptyMaster.master"
    AutoEventWireup="false" CodeFile="EmployeeSearch.aspx.vb" Inherits="Admin_EmployeeSearch"
    UICulture="auto" Theme="SvTheme" Culture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function CloseAndRefresh() {
            var oWnd = GetRadWindow();
            oWnd.close();
            GetRadWindow().BrowserWindow.location.reload();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updatepanel1">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" runat="server" />

            <div class="col-md-2">
                <asp:Label ID="lblType" runat="server" Text="Search Type" CssClass="Profiletitletxt" meta:resourcekey="lblTypeResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <telerik:RadComboBox ID="RadCmbBxType" MarkFirstMatch="True"
                    Skin="Vista" runat="server" ValidationGroup="GrpSearch" meta:resourcekey="RadCmbBxTypeResource1">
                    <Items>
                        <telerik:RadComboBoxItem Text="--Please Select--" Value="-1" runat="server" meta:resourcekey="RadComboBoxItemResource1" />
                        <telerik:RadComboBoxItem Text="Employee English Name" Value="1" runat="server" meta:resourcekey="RadComboBoxItemResource2" />
                        <telerik:RadComboBoxItem Text="Employee Arabic Name" Value="2" runat="server" meta:resourcekey="RadComboBoxItemResource3" />
                        <telerik:RadComboBoxItem Text="Employee English Nationality" Value="3" runat="server" meta:resourcekey="RadComboBoxItemResource4" />
                        <telerik:RadComboBoxItem Text="Employee Arabic Nationality" Value="4" runat="server" meta:resourcekey="RadComboBoxItemResource5" />
                        <telerik:RadComboBoxItem Text="Employee English Religon" Value="5" runat="server" meta:resourcekey="RadComboBoxItemResource6" />
                        <telerik:RadComboBoxItem Text="Employee Arabic Religon" Value="6" runat="server" meta:resourcekey="RadComboBoxItemResource7" />
                        <telerik:RadComboBoxItem Text="Employee Card No" Value="7" runat="server" meta:resourcekey="RadComboBoxItemResource8" />
                    </Items>
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="RadCmbBxType"
                    Display="None" ErrorMessage="Please Select Search Type" ValidationGroup="GrpSearch" meta:resourcekey="rfvTypeResource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="vceType" runat="server" CssClass="AISCustomCalloutStyle"
                    TargetControlID="rfvType" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                </cc1:ValidatorCalloutExtender>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblOpeartor" runat="server" Text="Search Operator" CssClass="Profiletitletxt" meta:resourcekey="lblOpeartorResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCmbBxOperator" MarkFirstMatch="True"
                        Skin="Vista" runat="server" ValidationGroup="GrpSearch" meta:resourcekey="RadCmbBxOperatorResource1">
                        <Items>
                            <telerik:RadComboBoxItem Text="--Please Select--" Value="-1" runat="server" meta:resourcekey="RadComboBoxItemResource9" />
                            <telerik:RadComboBoxItem Text="Starts With" Value="Starts_With" runat="server" meta:resourcekey="RadComboBoxItemResource10" />
                            <telerik:RadComboBoxItem Text="Contains" Value="Contains" runat="server" meta:resourcekey="RadComboBoxItemResource11" />
                            <telerik:RadComboBoxItem Text="Equals" Value="Equals" runat="server" meta:resourcekey="RadComboBoxItemResource12" />
                            <telerik:RadComboBoxItem Text="Ends With" Value="Ends_With" runat="server" meta:resourcekey="RadComboBoxItemResource13" />
                        </Items>
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvOperator" runat="server" ControlToValidate="RadCmbBxOperator"
                        Display="None" ErrorMessage="Please Select Search Operator" ValidationGroup="GrpSearch" meta:resourcekey="rfvOperatorResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceOperator" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvOperator" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblValue" runat="server" Text="Search Value" CssClass="Profiletitletxt" meta:resourcekey="lblValueResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtValue" runat="server" meta:resourcekey="txtValueResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvValue" runat="server" ControlToValidate="txtValue"
                        Display="None" ErrorMessage="Please Insert Search Value" ValidationGroup="GrpSearch" meta:resourcekey="rfvValueResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceValue" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvValue" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:Button ID="btnSearchEmp" runat="server" CssClass="button" Text="Search" ValidationGroup="GrpSearch" meta:resourcekey="btnSearchEmpResource1" />
                </div>
            </div>
            <div class="table-responsive">
                <telerik:RadGrid runat="server" ID="dgrdSearchResult" AutoGenerateColumns="False"
                    PageSize="25" Skin="Hay" AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" meta:resourcekey="dgrdSearchResultResource1">
                    <MasterTableView AllowFilteringByColumn="False"
                        DataKeyNames="EmployeeId,CompanyId">

                        <Columns>

                            <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEmpNo" runat="server" CausesValidation="False" Text='<%# DataBinder.Eval(Container,"DataItem.EmployeeNo") %>'
                                        CommandName="Emp_No" meta:resourcekey="lnkEmpNoResource1"></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="Employee Name"
                                UniqueName="EmployeeName" FilterControlAltText="Filter EmployeeName column" meta:resourcekey="GridBoundColumnResource1" />
                            <telerik:GridBoundColumn DataField="EmployeeArabicName" SortExpression="EmployeeArabicName"
                                HeaderText="Employee Arabic Name" UniqueName="EmployeeArabicName" FilterControlAltText="Filter EmployeeArabicName column" meta:resourcekey="GridBoundColumnResource2" />
                            <telerik:GridBoundColumn DataField="EmployeeId" SortExpression="EmployeeId" Visible="False"
                                AllowFiltering="False" UniqueName="EmployeeId" FilterControlAltText="Filter EmployeeId column" meta:resourcekey="GridBoundColumnResource3" />
                            <telerik:GridBoundColumn DataField="CompanyId" SortExpression="CompanyId" Visible="False"
                                AllowFiltering="False" UniqueName="CompanyId" FilterControlAltText="Filter CompanyId column" meta:resourcekey="GridBoundColumnResource4" />
                        </Columns>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                    </MasterTableView>
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
