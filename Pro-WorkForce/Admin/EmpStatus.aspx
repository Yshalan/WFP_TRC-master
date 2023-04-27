<%@ Page Title="Define Employee Status" Language="VB" Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="EmpStatus.aspx.vb" Inherits="Emp_EmpStatus"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlEmpStatus" runat="server">
        <ContentTemplate>
            
                                            <uc1:PageHeader ID="UserCtrlEmployeeStatus" HeaderText="Employee Status" runat="server" />
                      
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Code" runat="server" CssClass="Profiletitletxt" Text="Code" meta:resourcekey="CodeResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtStatusCode" runat="server" meta:resourcekey="txtStatusCodeResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqStatusCode" runat="server" ControlToValidate="txtStatusCode"
                                                Display="None" ErrorMessage="Please enter status code" ValidationGroup="StatusGroup"
                                                meta:resourcekey="reqStatusCodeResource1"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ExtenderStatusCode" runat="server" CssClass="AISCustomCalloutStyle"
                                                TargetControlID="reqStatusCode" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                            </cc1:ValidatorCalloutExtender>
                                        </div>
                                    </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblName" runat="server" CssClass="Profiletitletxt" Text="English name"
                                            meta:resourcekey="lblNameResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtName" runat="server" meta:resourcekey="txtNameResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqStatusName" runat="server" ErrorMessage="Please enter status english name"
                                            Display="None" ValidationGroup="StatusGroup" ControlToValidate="txtName" meta:resourcekey="reqStatusNameResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ExtenderreqStatusName" TargetControlID="reqStatusName"
                                            runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblArabicName" runat="server" CssClass="Profiletitletxt" Text="Arabic Name"
                                            meta:resourcekey="lblArabicNameResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtArabicName" runat="server" meta:resourcekey="txtArabicNameResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqStatusArName" runat="server" ErrorMessage="Please enter status arabic name"
                                            Display="None" ValidationGroup="StatusGroup" ControlToValidate="txtArabicName"
                                            meta:resourcekey="reqStatusArNameResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ExtenderStatusArName" TargetControlID="reqStatusArName"
                                            runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblDescribtion" runat="server" CssClass="Profiletitletxt" Text="Description"
                                            meta:resourcekey="lblDescribtionResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtDescribtion" runat="server" TextMode="MultiLine" meta:resourcekey="txtDescribtionResource1"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chBxConsiderActive" runat="server" Text="Active"
                                            meta:resourcekey="lblConsiderActiveResource1" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="StatusGroup"
                                            meta:resourcekey="btnSaveResource1" />
                                        <asp:Button ID="btnDelete" runat="server" OnClientClick="return ValidateDelete();" CausesValidation="False" CssClass="button"
                                            Text="Delete" meta:resourcekey="btnDeleteResource1" />
                                        <asp:Button ID="btnClear" runat="server" CausesValidation="False" CssClass="button"
                                            Text="Clear" meta:resourcekey="btnClearResource1" />
                                    </div>
                                </div>
                                <div class="row">
                                        <div class="col-md-2">
                                            <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdVwEmpStatus"
                                                ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                                        </div>
                                    </div>
            <div class="row">
                <div class="table-responsive">
                                        <telerik:RadGrid ID="dgrdVwEmpStatus" runat="server" AllowSorting="True" AllowPaging="True"
                                            PageSize="25"  GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                            ShowFooter="True" OnItemCommand="dgrdVwEmpStatus_ItemCommand" meta:resourcekey="dgrdVwEmpStatusResource1">
                                            <SelectedItemStyle ForeColor="Maroon" />
                                            <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="StatusId,StatusCode">
                                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                                        UniqueName="TemplateColumn">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="StatusId" HeaderText="StatusId"
                                                        SortExpression="StatusId" Visible="False" meta:resourcekey="GridBoundColumnResource1"
                                                        UniqueName="StatusId">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="StatusCode" HeaderText="Status Code" SortExpression="StatusCode"
                                                        Resizable="False" meta:resourcekey="GridBoundColumnResource2" UniqueName="StatusCode">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status Name" SortExpression="StatusName"
                                                        meta:resourcekey="GridBoundColumnResource3" UniqueName="StatusName" />
                                                    <telerik:GridBoundColumn DataField="StatusArabicName" HeaderText="Arabic Name" SortExpression="StatusArabicName"
                                                        Resizable="False" meta:resourcekey="GridBoundColumnResource4" UniqueName="StatusArabicName">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CosiderEmployeeActive" HeaderText="Active" SortExpression="CosiderEmployeeActive"
                                                        Resizable="False" meta:resourcekey="GridBoundColumnResource5" UniqueName="CosiderEmployeeActive">
                                                    </telerik:GridBoundColumn>
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
                                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                                EnableRowHoverStyle="True">
                                                <Selecting AllowRowSelect="True" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </div>
                                </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    
    <script type="text/javascript">
        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdVwEmpStatus.ClientID %>");
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
