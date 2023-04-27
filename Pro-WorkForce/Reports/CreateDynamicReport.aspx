<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreateDynamicReport.aspx.vb" MasterPageFile="~/Default/NewMaster.master" Theme="SvTheme"
    Inherits="Reports_CreateDynamicReport" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlDynamicRpt" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Create Dynamic Report" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblReportName" runat="server" CssClass="Profiletitletxt" Text="Report Name"
                        meta:resourcekey="lblReportNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtReportName" runat="server" meta:resourcekey="txtReportNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqReportName" runat="server" ControlToValidate="txtReportName"
                        Display="None" ErrorMessage="Please enter Report Name" ValidationGroup="ReportGroup"
                        meta:resourcekey="ReportNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderreqReportName" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="reqReportName" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblReportView" runat="server" CssClass="Profiletitletxt" Text="Report View"
                        meta:resourcekey="lblReportViewResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtReportView" runat="server" meta:resourcekey="txtReportViewResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqReportView" runat="server" ErrorMessage="Please enter Report View"
                        Display="None" ValidationGroup="ReportGroup" ControlToValidate="txtReportView"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        meta:resourcekey="reqReportViewResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderReportView" TargetControlID="reqReportView"
                        runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblSQLQuery" runat="server" CssClass="Profiletitletxt" Text="SQL Query"
                        meta:resourcekey="lblSQLQueryResource1"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:TextBox ID="txtSQLQuery" runat="server" TextMode="MultiLine" Rows="10" meta:resourcekey="txtSQLQueryResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqSQLQuery" runat="server" ErrorMessage="Please enter SQL Query"
                        Display="None" ValidationGroup="ReportGroup" ControlToValidate="txtSQLQuery"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        meta:resourcekey="reqSQLQueryResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderSQLQuery" TargetControlID="reqSQLQuery"
                        runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="ReportGroup"
                        CssClass="button" meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CausesValidation="False"
                        CssClass="button" meta:resourcekey="btnDeleteResource1" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False" CssClass="button"
                        meta:resourcekey="btnClearResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                    <div class="filterDiv">
                        <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdVwDynamicRpt"
                            ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" />
                    </div>
                    <div class="table-responsive">
                        <telerik:RadGrid ID="dgrdVwDynamicRpt" runat="server" AllowSorting="True" AllowPaging="True"
                            PageSize="25" GridLines="None" ShowStatusBar="True" ShowFooter="True"
                            OnItemCommand="dgrdVwDynamicRpt_ItemCommand" meta:resourcekey="dgrdVwDynamicRptResource1">

                            <SelectedItemStyle ForeColor="Maroon" />
                            <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="ReportId,ViewName">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" Text="&nbsp;" runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="ReportId" SortExpression="ReportId" Visible="False"
                                        AllowFiltering="False" meta:resourcekey="GridBoundColumnResource1" UniqueName="ReportId" />
                                    <telerik:GridBoundColumn DataField="ReportName" SortExpression="ReportName" HeaderText="Report Name"
                                        meta:resourcekey="GridBoundColumnResource2" UniqueName="ReportName" />
                                    <telerik:GridBoundColumn DataField="ViewName" SortExpression="ViewName" HeaderText="View Name"
                                        meta:resourcekey="GridBoundColumnResource3" UniqueName="ViewName" />


                                </Columns>
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                <CommandItemTemplate>
                                    <telerik:RadToolBar Skin="Hay" runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                        meta:resourcekey="RadToolBar1Resource1">
                                        <Items>
                                            <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                ImagePosition="Right" runat="server"
                                                meta:resourcekey="RadToolBarButtonResource1" Owner="" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
