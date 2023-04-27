<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="BlackListVisitors.aspx.vb" Inherits="DailyTasks_BlackListVisitors" Theme="SvTheme" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Blacklist Visitors" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblIdNumber" runat="server" Text="ID Number" meta:resourcekey="lblIdNumberResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtIDNumber" runat="server" meta:resourcekey="txtIDNumberResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvIDNumber" runat="server" ControlToValidate="txtIDNumber"
                        Display="None" ErrorMessage="Please Enter ID Number"
                        ValidationGroup="grpSave" meta:resourcekey="rfvIDNumberResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceIDNumber" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvIDNumber"
                        WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblVisitorName" runat="server" Text="Visitor Name" meta:resourcekey="lblVisitorNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtVisitorName" runat="server" meta:resourcekey="txtVisitorNameResource1"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblVisitorArabicName" runat="server" Text="Visitor Arabic Name" meta:resourcekey="lblVisitorArabicNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtVisitorArabicName" runat="server" meta:resourcekey="txtVisitorArabicNameResource1"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblNationality" runat="server" Text="Nationality" meta:resourcekey="lblNationalityResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="radcmbxNationality" runat="server" MarkFirstMatch="True" Filter="Contains" ExpandDirection="Up" meta:resourcekey="radcmbxNationalityResource1"></telerik:RadComboBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="grpSave" meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" meta:resourcekey="btnClearResource1" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" meta:resourcekey="btnDeleteResource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <div class="filterDiv">
                        <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdBlacklistVisitors"
                            ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" >
                            <ContextMenu FeatureGroupID="rfContextMenu">
                            </ContextMenu>
                        </telerik:RadFilter>
                    </div>
                    <telerik:RadGrid ID="dgrdBlacklistVisitors" runat="server" AllowSorting="True" AllowPaging="True"
                        PageSize="25" GridLines="None" ShowStatusBar="True" ShowFooter="True"
                        OnItemCommand="dgrdBlacklistVisitors_ItemCommand" CellSpacing="0" meta:resourcekey="dgrdBlacklistVisitorsResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="BlacklistId">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False"
                                    UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="BlacklistId" SortExpression="BlacklistId" AllowFiltering="False"
                                    Visible="False" UniqueName="BlacklistId" FilterControlAltText="Filter BlacklistId column" meta:resourcekey="GridBoundColumnResource1" />
                                <telerik:GridBoundColumn DataField="IDNumber" SortExpression="IDNumber" HeaderText="ID Number"
                                    UniqueName="IDNumber" FilterControlAltText="Filter IDNumber column" meta:resourcekey="GridBoundColumnResource2" />
                                <telerik:GridBoundColumn DataField="VisitorName" SortExpression="VisitorName"
                                    HeaderText="Visitor Name" UniqueName="VisitorName" FilterControlAltText="Filter VisitorName column" meta:resourcekey="GridBoundColumnResource3" />
                                <telerik:GridBoundColumn DataField="VisitorArabicName" SortExpression="VisitorArabicName" HeaderText="Visitor Arabic Name"
                                    UniqueName="VisitorArabicName" FilterControlAltText="Filter VisitorArabicName column" meta:resourcekey="GridBoundColumnResource4" />
                                <telerik:GridBoundColumn DataField="Nationality" SortExpression="Nationality" HeaderText="Nationality"
                                    UniqueName="Nationality" FilterControlAltText="Filter Nationality column" meta:resourcekey="GridBoundColumnResource5" />

                                <telerik:GridBoundColumn DataField="NationalityName" SortExpression="NationalityName" HeaderText="Nationality Name"
                                    UniqueName="NationalityName" Display="False" AllowFiltering="False" FilterControlAltText="Filter NationalityName column" meta:resourcekey="GridBoundColumnResource6" />
                                <telerik:GridBoundColumn DataField="NationalityArabicName" SortExpression="NationalityArabicName" HeaderText="Nationality Arabic Name"
                                    UniqueName="NationalityArabicName" Display="False" AllowFiltering="False" FilterControlAltText="Filter NationalityArabicName column" meta:resourcekey="GridBoundColumnResource7" />

                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar Skin="Hay" runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

