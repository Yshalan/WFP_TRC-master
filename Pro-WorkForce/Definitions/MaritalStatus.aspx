<%@ Page Title="Define Marital Status" Language="VB"  Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="MaritalStatus.aspx.vb" Inherits="Emp_MaritalStatus"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlEmpNationality" runat="server">
        <ContentTemplate>
           
                                            <uc1:PageHeader ID="UserCtrlMaritalStatus" HeaderText="Marital Status" runat="server" />
                                      
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblMaritalStatusCode" runat="server" CssClass="Profiletitletxt" Text="Code"
                                            meta:resourcekey="lblMaritalStatusCodeResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtMaritalstatusCode" runat="server" meta:resourcekey="txtMaritalstatusCodeResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqMaritalStatusCode" runat="server" ErrorMessage="Please enter marital status code"
                                            Display="None" ControlToValidate="txtMaritalstatusCode" ValidationGroup="GroupMaritalstatus"
                                            meta:resourcekey="reqMaritalStatusCodeResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ExtenderMaritalStatusCode" TargetControlID="reqMaritalStatusCode"
                                            runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblMaritalStatusName" runat="server" CssClass="Profiletitletxt" Text="English name"
                                            meta:resourcekey="lblMaritalStatusNameResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtMaritalStatusName" runat="server" meta:resourcekey="txtMaritalStatusNameResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqMaritalStatusName" runat="server" ErrorMessage="Please enter marital status english name"
                                            Display="None" ControlToValidate="txtMaritalStatusName" ValidationGroup="GroupMaritalstatus"
                                            meta:resourcekey="reqMaritalStatusNameResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ExtenderMaritalStatusName" TargetControlID="reqMaritalStatusName"
                                            runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblMaritalStatusArabName" runat="server" CssClass="Profiletitletxt"
                                            Text="Arabic name" meta:resourcekey="lblMaritalStatusArabNameResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtMaritalStatusArabName" runat="server" meta:resourcekey="txtMaritalStatusArabNameResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqMaritalStatusArName" runat="server" ErrorMessage="Please enter marital status arabic name"
                                            Display="None" ControlToValidate="txtMaritalStatusArabName" ValidationGroup="GroupMaritalstatus"
                                            meta:resourcekey="reqMaritalStatusArNameResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ExtenderMaritalStatusArName" TargetControlID="reqMaritalStatusArName"
                                            runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" ValidationGroup="GroupMaritalstatus"
                                            meta:resourcekey="btnSaveResource1" />
                                        <asp:Button ID="btnDelete" OnClientClick="return ValidateDelete();" CssClass="button" runat="server" Text="Delete" CausesValidation="False"
                                            meta:resourcekey="btnDeleteResource1" />
                                        <asp:Button ID="btnClear" CssClass="button" runat="server" Text="Clear" CausesValidation="False"
                                            meta:resourcekey="btnClearResource1" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="table-responsive">
                                        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                                        <div class="filterDiv">
                                            <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdMaritalStatus"
                                                ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                                        </div>
                                        <telerik:RadGrid ID="dgrdMaritalStatus" runat="server" AllowPaging="True" AllowSorting="true"
                                             GridLines="None" PageSize="25" ShowStatusBar="True" AllowMultiRowSelection="True"
                                            ShowFooter="True" meta:resourcekey="dgrdMaritalStatusResource1">
                                            <SelectedItemStyle ForeColor="Maroon" />
                                            <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="MaritalStatusCode,MaritalStatusId">
                                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                                        UniqueName="TemplateColumn">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="MaritalStatusId" SortExpression="MaritalStatusId"
                                                        AllowFiltering="False" Visible="False" meta:resourcekey="GridBoundColumnResource1"
                                                        UniqueName="MaritalStatusId" />
                                                    <telerik:GridBoundColumn DataField="MaritalStatusCode" SortExpression="MaritalStatusCode"
                                                        HeaderText="Code" meta:resourcekey="GridBoundColumnResource2" UniqueName="MaritalStatusCode" />
                                                    <telerik:GridBoundColumn DataField="MatitalStatusName" SortExpression="MatitalStatusName"
                                                        HeaderText="Matital Status Name" meta:resourcekey="GridBoundColumnResource3"
                                                        UniqueName="MatitalStatusName" />
                                                    <telerik:GridBoundColumn DataField="MaritalStatusArabicName" SortExpression="MaritalStatusArabicName"
                                                        HeaderText="Marital Status Arabic Name" meta:resourcekey="GridBoundColumnResource4"
                                                        UniqueName="MaritalStatusArabicName" />
                                                </Columns>
                                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                <CommandItemTemplate>
                                                    <telerik:RadToolBar Skin="Hay" runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                                        meta:resourcekey="RadToolBar1Resource1">
                                                        <Items>
                                                            <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                                ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                                                        </Items>
                                                    </telerik:RadToolBar>
                                                </CommandItemTemplate>
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
    <script type="text/javascript">
        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdMaritalStatus.ClientID %>");
            var masterTable = grid.get_masterTableView();
            var value = false;
            for (var i = 0; i < masterTable.get_dataItems().length; i++) {
                var gridItemElement = masterTable.get_dataItems()[i].findElement("chk");
                if (gridItemElement.checked) {
                    value = true;
                }
            }
            if (value === false) {
                ShowMessage("<%= Resources.Strings.ErrorDeleteRecourd %>");
            }
            return value;
        }
    </script>
</asp:Content>
