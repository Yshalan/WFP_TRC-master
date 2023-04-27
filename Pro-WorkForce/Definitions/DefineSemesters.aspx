<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" CodeFile="DefineSemesters.aspx.vb"
    Inherits="Definitions_DefineSemesters" Theme="SvTheme" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" HeaderText="Define Semesters" runat="server" />
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblSemesterName" runat="server" Text="Semester Name" meta:resourcekey="lblSemesterNameResource1"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtSemesterName" runat="server" meta:resourcekey="txtSemesterNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSemesterName" runat="server" ControlToValidate="txtSemesterName"
                        Display="None" ErrorMessage="Please Enter Semester Name" ValidationGroup="grpSave" meta:resourcekey="rfvSemesterNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceSemesterName" runat="server" TargetControlID="rfvSemesterName"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>

            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblSemesterArabicName" runat="server" Text="Semester Arabic Name" meta:resourcekey="lblSemesterArabicNameResource1" Visible="false"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtSemesterArabicName" runat="server" meta:resourcekey="txtSemesterArabicNameResource1" Visible="false"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSemesterArabicName" runat="server" ControlToValidate="txtSemesterArabicName"
                        Display="None" ErrorMessage="Please Enter Semester Arabic Name" ValidationGroup="grpSave"
                         meta:resourcekey="rfvSemesterArabicNameResource1" Enabled="false"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceSemesterArabicName" runat="server" TargetControlID="rfvSemesterArabicName"
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
            <div class="row">
                <div class="table-responsive"></div>
                <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdCoordinatorType"
                    ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1">
                </telerik:RadFilter>
                <telerik:RadGrid ID="dgrdSemesters" runat="server" AllowPaging="True"
                    AllowSorting="True" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                    AutoGenerateColumns="False" PageSize="25" OnItemCommand="dgrdSemesters_ItemCommand"
                    ShowFooter="True" CellSpacing="0" meta:resourcekey="dgrdSemestersResource1">
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                        EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="SemesterId">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="False"
                                UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="SemesterId" Visible="False" UniqueName="SemesterId" 
                                FilterControlAltText="Filter SemesterId column" meta:resourcekey="GridBoundColumnResource1" />
                            <telerik:GridBoundColumn DataField="SemesterName" HeaderText="Semester Name" SortExpression="SemesterName"
                                UniqueName="SemesterName" FilterControlAltText="Filter SemesterName column" meta:resourcekey="GridBoundColumnResource2" />
                            <telerik:GridBoundColumn DataField="SemesterArabicName" HeaderText="Semester Arabic Name" SortExpression="SemesterArabicName"
                                UniqueName="SemesterArabicName" FilterControlAltText="Filter SemesterArabicName column" meta:resourcekey="GridBoundColumnResource3" Display="false" />
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

