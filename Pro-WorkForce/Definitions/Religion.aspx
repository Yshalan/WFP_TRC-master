<%@ Page Title="Define Religion" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false"  CodeFile="Religion.aspx.vb" Inherits="Emp_EmpReligion"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlEmpReligion" runat="server">
        <ContentTemplate>

            <uc1:PageHeader ID="UserCtrlReligion" runat="server" HeaderText="Employee Religion" />

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblReligionCode" runat="server" CssClass="Profiletitletxt" Text="Code"
                        meta:resourcekey="lblReligionCodeResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtReligionCode" runat="server" meta:resourcekey="txtReligionCodeResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqReligionCode" runat="server" ControlToValidate="txtReligionCode"
                        Display="None" ErrorMessage="Please enter religion code" ValidationGroup="ReligionGroup"
                        meta:resourcekey="reqReligionCodeResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderreqReligionCode" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="reqReligionCode" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>

            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblReligionName" runat="server" CssClass="Profiletitletxt" Text="English name"
                        meta:resourcekey="lblReligionNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtReligioName" runat="server" meta:resourcekey="txtReligioNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqReligionName" runat="server" ErrorMessage="Please enter religion english name"
                        Display="None" ValidationGroup="ReligionGroup" ControlToValidate="txtReligioName"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        meta:resourcekey="reqReligionNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderReligioName" TargetControlID="reqReligionName"
                        runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblReligionArabicName" runat="server" CssClass="Profiletitletxt" Text="Arabic Name"
                        meta:resourcekey="lblReligionArabicNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtReligionArabicName" runat="server" meta:resourcekey="txtReligionArabicNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqReligionArabicName" runat="server" ErrorMessage="Please enter religion arabic name"
                        Display="None" ValidationGroup="ReligionGroup" ControlToValidate="txtReligionArabicName"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        meta:resourcekey="reqReligionArabicNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderReligionArName" TargetControlID="reqReligionArabicName"
                        runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-dm-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="ReligionGroup"
                        CssClass="button" meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnDelete" OnClientClick="return ValidateDelete();" runat="server" Text="Delete" CausesValidation="False"
                        CssClass="button" meta:resourcekey="btnDeleteResource1" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False" CssClass="button"
                        meta:resourcekey="btnClearResource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                    <div class="filterDiv">
                        <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdVwReligion"
                            ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" />
                    </div>
                    <telerik:RadGrid ID="dgrdVwReligion" runat="server" AllowSorting="True" AllowPaging="True"
                        PageSize="25" GridLines="None" ShowStatusBar="True" ShowFooter="True"
                        OnItemCommand="dgrdVwReligion_ItemCommand" meta:resourcekey="dgrdVwReligionResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="ReligionId,ReligionCode">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                    UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="ReligionCode" SortExpression="ReligionCode" HeaderText="Religion Code"
                                    Visible="False" meta:resourcekey="GridBoundColumnResource1" UniqueName="ReligionCode" />
                                <telerik:GridBoundColumn DataField="ReligionCode" SortExpression="ReligionCode" HeaderText="Religion Code"
                                    meta:resourcekey="GridBoundColumnResource2" UniqueName="ReligionCode1" />
                                <telerik:GridBoundColumn DataField="ReligionName" SortExpression="ReligionName" HeaderText="Religion English Name"
                                    meta:resourcekey="GridBoundColumnResource3" UniqueName="ReligionName" />
                                <telerik:GridBoundColumn DataField="ReligionArabicName" SortExpression="ReligionArabicName"
                                    HeaderText="Religion Arabic Name" meta:resourcekey="GridBoundColumnResource4"
                                    UniqueName="ReligionArabicName" />
                                <telerik:GridCheckBoxColumn DataField="Active" SortExpression="Active" HeaderText="Is Active"
                                    AllowFiltering="False" Visible="False" meta:resourcekey="GridCheckBoxColumnResource1"
                                    UniqueName="Active" ItemStyle-CssClass="nocheckboxstyle" />
                                <telerik:GridBoundColumn DataField="ReligionId" SortExpression="ReligionId" Visible="False"
                                    AllowFiltering="False" meta:resourcekey="GridBoundColumnResource5" UniqueName="ReligionId" />
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
                </td>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdVwReligion.ClientID %>");
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
