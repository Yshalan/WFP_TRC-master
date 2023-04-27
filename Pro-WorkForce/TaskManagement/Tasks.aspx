<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Tasks.aspx.vb" Inherits="Definitions_Tasks" Theme="SvTheme" meta:resourcekey="PageResource1" uiculture="auto" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" runat="server" />
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                    <div class="filterDiv">
                        <telerik:RadFilter runat="server" ID="RadProjects" Skin="Hay" FilterContainerID="dgrdProjects"
                            ShowApplyButton="False" CssClass="RadFilter RadFilter_Default" meta:resourcekey="RadProjectsResource1">
                        </telerik:RadFilter>
                    </div>

                    <telerik:RadGrid runat="server" ID="dgrdProjects" AutoGenerateColumns="False" PageSize="15"
                        AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" meta:resourcekey="dgrdProjectsResource1">
                        <MasterTableView CommandItemDisplay="Top" AllowFilteringByColumn="False" DataKeyNames="ProjectName,ProjectId">
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick" SingleClick="None" meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                            ImageUrl="~/images/RadFilter.gif" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                            <Columns>
                                <telerik:GridBoundColumn DataField="ProjectId" HeaderText="ProjectId"
                                    AllowFiltering="False" SortExpression="ProjectId" Display="False" FilterControlAltText="Filter ProjectId column" meta:resourcekey="GridBoundColumnResource1" UniqueName="ProjectId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName" Resizable="False" FilterControlAltText="Filter ProjectName column" meta:resourcekey="GridBoundColumnResource2" UniqueName="ProjectName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ProjectArabicName" HeaderText="Project Arabic Name"
                                    SortExpression="ProjectArabicName" FilterControlAltText="Filter ProjectArabicName column" meta:resourcekey="GridBoundColumnResource3" UniqueName="ProjectArabicName" />
                                <telerik:GridBoundColumn DataField="PlannedStartDate" HeaderText="Planned Start Date" SortExpression="PlannedStartDate" Resizable="False"
                                    DataFormatString="{0:dd/MM/yyyy}" FilterControlAltText="Filter PlannedStartDate column" meta:resourcekey="GridBoundColumnResource4" UniqueName="PlannedStartDate">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PlannedEndDate" HeaderText="Planned End Date" SortExpression="PlannedEndDate" Resizable="False"
                                    DataFormatString="{0:dd/MM/yyyy}" FilterControlAltText="Filter PlannedEndDate column" meta:resourcekey="GridBoundColumnResource5" UniqueName="PlannedEndDate">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="False"></GroupingSettings>

                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                            EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
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

