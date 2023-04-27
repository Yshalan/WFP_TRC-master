<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" CodeFile="Coordinator_Type.aspx.vb"
    Inherits="Definitions_Coordinator_Type" Theme="SvTheme" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlCoordinator_Type" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" HeaderText="Coordinator Type" runat="server" />
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblCoordinatorShortName" runat="server" Text="Coordinator Short Name" meta:resourcekey="lblCoordinatorShortNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtCoordinatorShortName" runat="server" meta:resourcekey="txtCoordinatorShortNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqCoordinatorShortName" runat="server" ControlToValidate="txtCoordinatorShortName"
                        Display="None" ErrorMessage="Please Enter Type Short Name" ValidationGroup="grpSave" meta:resourcekey="reqCoordinatorShortNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceCoordinatorShortName" runat="server" TargetControlID="reqCoordinatorShortName"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblCoordinatorTypeName" runat="server" Text="Coordinator Type Name" meta:resourcekey="lblCoordinatorTypeNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtCoordinatorTypeName" runat="server" meta:resourcekey="txtCoordinatorTypeNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqCoordinatorTypeName" runat="server" ControlToValidate="txtCoordinatorTypeName"
                        Display="None" ErrorMessage="Please Enter Type English Name" ValidationGroup="grpSave" meta:resourcekey="reqCoordinatorTypeNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceCoordinatorTypeName" runat="server" TargetControlID="reqCoordinatorTypeName"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblCoordinatorTypeArabicName" runat="server" Text="Coordinator Type Arabic Name" meta:resourcekey="lblCoordinatorTypeArabicNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtCoordinatorTypeArabicName" runat="server" meta:resourcekey="txtCoordinatorTypeArabicNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqCoordinatorTypeArabicName" runat="server" ControlToValidate="txtCoordinatorTypeArabicName"
                        Display="None" ErrorMessage="Please Enter Type Arabic Name" ValidationGroup="grpSave" meta:resourcekey="reqCoordinatorTypeArabicNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceCoordinatorTypeArabicName" runat="server" TargetControlID="reqCoordinatorTypeArabicName"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="grpSave" meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" meta:resourcekey="btnClearResource1" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" meta:resourcekey="btnDeleteResource1" />
                </div>
            </div>
            <div class="table-responsive">
                <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdCoordinatorType"
                    ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" >
                    <ContextMenu FeatureGroupID="rfContextMenu">
                    </ContextMenu>
                </telerik:RadFilter>
                <telerik:RadGrid ID="dgrdCoordinatorType" runat="server" AllowPaging="True"
                    AllowSorting="True" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                    AutoGenerateColumns="False" PageSize="25" OnItemCommand="dgrdCoordinatorType_ItemCommand"
                    ShowFooter="True" CellSpacing="0" meta:resourcekey="dgrdCoordinatorTypeResource1">
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                        EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="CoordinatorTypeId">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="False"
                                UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="CoordinatorTypeId" Visible="False" UniqueName="CoordinatorTypeId" FilterControlAltText="Filter CoordinatorTypeId column" meta:resourcekey="GridBoundColumnResource1" />
                            <telerik:GridBoundColumn DataField="CoordinatorShortName" HeaderText="Coordinator Short Name" SortExpression="CoordinatorShortName"
                                UniqueName="CoordinatorShortName" FilterControlAltText="Filter CoordinatorShortName column" meta:resourcekey="GridBoundColumnResource2" />
                            <telerik:GridBoundColumn DataField="CoordinatorTypeName" HeaderText="Coordinator Type Name" SortExpression="CoordinatorTypeName"
                                UniqueName="CoordinatorTypeName" FilterControlAltText="Filter CoordinatorTypeName column" meta:resourcekey="GridBoundColumnResource3" />
                            <telerik:GridBoundColumn DataField="CoordinatorTypeArabicName" HeaderText="Coordinator Type Arabic Name"
                                SortExpression="CoordinatorTypeArabicName" UniqueName="CoordinatorTypeArabicName" FilterControlAltText="Filter CoordinatorTypeArabicName column" meta:resourcekey="GridBoundColumnResource4" />
                        </Columns>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                Skin="Hay" meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                                <Items>
                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                        ImagePosition="Right" runat="server"
                                        Owner="" meta:resourcekey="RadToolBarButtonResource1" />
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                    </MasterTableView>
                    <SelectedItemStyle ForeColor="Maroon" />
                </telerik:RadGrid>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

