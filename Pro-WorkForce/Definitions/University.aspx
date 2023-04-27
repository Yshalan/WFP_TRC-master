<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="University.aspx.vb" Inherits="Definitions_University" Theme="SvTheme" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" HeaderText="Define University" runat="server" />

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblUniversityShortName" runat="server" Text="University Short Name" meta:resourcekey="lblUniversityShortNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtUniversityShortName" runat="server" meta:resourcekey="txtUniversityShortNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUniversityShortName" runat="server" ControlToValidate="txtUniversityShortName"
                        Display="None" ErrorMessage="Please Enter University Short Name" ValidationGroup="grpSave" meta:resourcekey="rfvUniversityShortNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceUniversityShortName" runat="server" TargetControlID="rfvUniversityShortName"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>

                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblUniversityName" runat="server" Text="University Name" meta:resourcekey="lblUniversityNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtUniversityName" runat="server" meta:resourcekey="txtUniversityNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUniversityName" runat="server" ControlToValidate="txtUniversityName"
                        Display="None" ErrorMessage="Please Enter University Name" ValidationGroup="grpSave" meta:resourcekey="rfvUniversityNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceUniversityName" runat="server" TargetControlID="rfvUniversityName"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblUniversityArabicName" runat="server" Text="University Arabic Name" meta:resourcekey="lblUniversityArabicNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtUniversityArabicName" runat="server" meta:resourcekey="txtUniversityArabicNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUniversityArabicName" runat="server" ControlToValidate="txtUniversityArabicName"
                        Display="None" ErrorMessage="Please Enter University Arabic Name" ValidationGroup="grpSave" meta:resourcekey="rfvUniversityArabicNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceUniversityArabicName" runat="server" TargetControlID="rfvUniversityArabicName"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblAddress" runat="server" Text="Address" meta:resourcekey="lblAddressResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtAddress" runat="server" meta:resourcekey="txtAddressResource1"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No." meta:resourcekey="lblPhoneNoResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtPhoneNo" runat="server" meta:resourcekey="txtPhoneNoResource1"></asp:TextBox>
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
                <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdUniversities"
                    ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1">
                </telerik:RadFilter>
                <telerik:RadGrid ID="dgrdUniversities" runat="server" AllowPaging="True"
                    AllowSorting="True" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                    AutoGenerateColumns="False" PageSize="25" OnItemCommand="dgrdUniversities_ItemCommand"
                    ShowFooter="True" CellSpacing="0" meta:resourcekey="dgrdUniversitiesResource1">
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                        EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="UniversityId">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="False"
                                UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="UniversityId" Visible="False" UniqueName="UniversityId"
                                FilterControlAltText="Filter UniversityId column" meta:resourcekey="GridBoundColumnResource1" />
                            <telerik:GridBoundColumn DataField="UniversityShortName" HeaderText="University Short Name" SortExpression="UniversityShortName"
                                UniqueName="UniversityShortName" FilterControlAltText="Filter UniversityShortName column" meta:resourcekey="GridBoundColumnResource2" />
                            <telerik:GridBoundColumn DataField="UniversityName" HeaderText="University Name" SortExpression="UniversityName"
                                UniqueName="UniversityName" FilterControlAltText="Filter UniversityName column" meta:resourcekey="GridBoundColumnResource3" />
                            <telerik:GridBoundColumn DataField="UniversityArabicName" HeaderText="University Arabic Name"
                                SortExpression="UniversityArabicName" UniqueName="UniversityArabicName"
                                FilterControlAltText="Filter UniversityArabicName column" meta:resourcekey="GridBoundColumnResource4" />
                        </Columns>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                Skin="Hay" SingleClick="None" meta:resourcekey="RadToolBar1Resource1">
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

