<%@ Page Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="ViolationActions.aspx.vb" Inherits="Admin_ViolationActions"
    Title=" Violation Actions" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" runat="server" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblActionName" runat="server" CssClass="Profiletitletxt" Text="Action Name"
                        meta:resourcekey="lblActionNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtActionName" runat="server" meta:resourcekey="txtActionNameResource1" />
                    <asp:RequiredFieldValidator ID="reqActionName" runat="server" ControlToValidate="txtActionName"
                        Display="None" ErrorMessage="Please Enter a Action Name" ValidationGroup="Grp1"
                        meta:resourcekey="reqActionNameResource1"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtActionName"
                        ID="RegularExpressionValidator1" ValidationExpression="^[\s\S]{0,50}$" runat="server"
                        ErrorMessage="Maximum 50 characters allowed." ValidationGroup="Grp1"></asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderreqActionName" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="reqActionName" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblArabicName" runat="server" CssClass="Profiletitletxt" Text="Arabic Name"
                        meta:resourcekey="lblArabicNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtActionArabicName" runat="server" meta:resourcekey="txtActionArabicNameResource1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtActionArabicName"
                        Display="None" ErrorMessage="Please Enter a Action Arabic Name" ValidationGroup="Grp1"
                        meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtActionArabicName"
                        ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{0,50}$" runat="server"
                        ErrorMessage="Maximum 50 characters allowed." ValidationGroup="Grp1"></asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="ibtnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="Grp1"
                        meta:resourcekey="ibtnSaveResource1" />
                    <asp:Button ID="ibtnDelete" runat="server" OnClientClick="return ValidateDelete();"
                        Text="Delete" CssClass="button" meta:resourcekey="ibtnDeleteResource1" />
                    <asp:Button ID="ibtnRest" runat="server" Text="Clear" CssClass="button" meta:resourcekey="ibtnRestResource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdViolationActions"
                        ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid ID="dgrdViolationActions" runat="server" AllowSorting="True" AllowPaging="True"
                        PageSize="25" GridLines="None" ShowStatusBar="True" ShowFooter="True" meta:resourcekey="dgrdViolationActionsResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False"
                            DataKeyNames="ActionId">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                    UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="ActionName" HeaderText="Action Name" SortExpression="ActionName"
                                    meta:resourcekey="GridBoundColumnResource1" UniqueName="ActionName" />
                                <telerik:GridBoundColumn DataField="ActionArabicName" HeaderText="Arabic Name" SortExpression="ActionArabicName"
                                    meta:resourcekey="GridBoundColumnResource2" UniqueName="ActionArabicName" />
                                <telerik:GridBoundColumn DataField="ActionId" Visible="False" AllowFiltering="False"
                                    meta:resourcekey="GridBoundColumnResource3" UniqueName="ActionId" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
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
            var grid = $find("<%=dgrdViolationActions.ClientID %>");
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
