<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="StudyMajors.aspx.vb" Inherits="Definitions_StudyMajors" Theme="SvTheme" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function hideValidatorCalloutTab() {
            try {
                if (AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout != null) {
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.hide();
                }
            }
            catch (err) {
            }
            return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Define Major(s) & Specialization(s)" />
                </div>
            </div>
            <cc1:TabContainer ID="TabMajorsContainer" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab" meta:resourcekey="TabMajorsContainerResource1">
                <cc1:TabPanel ID="TabMajors" runat="server" HeaderText="Major(s)" meta:resourcekey="TabMajorsResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblMajorName" runat="server" Text="Major Name" meta:resourcekey="lblMajorNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtMajorName" runat="server" meta:resourcekey="txtMajorNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvMajorName" runat="server" ControlToValidate="txtMajorName"
                                    Display="None" ErrorMessage="Please Enter Major Name " ValidationGroup="grpSave" meta:resourcekey="rfvMajorNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceMajorName" runat="server" Enabled="True"
                                    TargetControlID="rfvMajorName" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblMajorArabicName" runat="server" Text="Major Arabic Name" meta:resourcekey="lblMajorArabicNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtMajorArabicName" runat="server" meta:resourcekey="txtMajorArabicNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvMajorArabicName" runat="server" ControlToValidate="txtMajorArabicName"
                                    Display="None" ErrorMessage="Please Enter Major Arabic Name " ValidationGroup="grpSave" meta:resourcekey="rfvMajorArabicNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceMajorArabicName" runat="server" Enabled="True"
                                    TargetControlID="rfvMajorArabicName" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
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
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabSpecializations" runat="server" HeaderText="Specialization(s)" Visible="False" meta:resourcekey="TabSpecializationsResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblSpecializationName" runat="server" Text="Specialization Name" meta:resourcekey="lblSpecializationNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtSpecializationName" runat="server" meta:resourcekey="txtSpecializationNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSpecializationName" runat="server" ControlToValidate="txtSpecializationName"
                                    Display="None" ErrorMessage="Please Enter Specialization Name " ValidationGroup="grpAdd" meta:resourcekey="rfvSpecializationNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceSpecializationName" runat="server" Enabled="True"
                                    TargetControlID="rfvSpecializationName" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblSpecializationArabicName" runat="server" Text="Specialization Arabic Name" meta:resourcekey="lblSpecializationArabicNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtSpecializationArabicName" runat="server" meta:resourcekey="txtSpecializationArabicNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSpecializationArabicName" runat="server" ControlToValidate="txtSpecializationArabicName"
                                    Display="None" ErrorMessage="Please Enter Specialization Arabic Name " ValidationGroup="grpAdd" meta:resourcekey="rfvSpecializationArabicNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceSpecializationArabicName" runat="server" Enabled="True"
                                    TargetControlID="rfvSpecializationArabicName" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="grpAdd" meta:resourcekey="btnAddResource1" />
                                <asp:Button ID="btnClearSpecialization" runat="server" Text="Clear" meta:resourcekey="btnClearSpecializationResource1" />
                                <asp:Button ID="btnRemove" runat="server" Text="Remove" meta:resourcekey="btnRemoveResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadFilter runat="server" ID="RadFilter2" FilterContainerID="dgrdSpecialization"
                                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter2Resource1" >
                                    <ContextMenu FeatureGroupID="rfContextMenu">
                                    </ContextMenu>
                                </telerik:RadFilter>
                                <telerik:RadGrid ID="dgrdSpecialization" runat="server" AllowSorting="True" AllowPaging="True"
                                    GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" PageSize="25"
                                    ShowFooter="True" CellSpacing="0" meta:resourcekey="dgrdSpecializationResource1">
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                    <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top"
                                        DataKeyNames="SpecializationId">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="False"
                                                UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="MajorName" HeaderText="Major Name" UniqueName="MajorName" FilterControlAltText="Filter MajorName column" meta:resourcekey="GridBoundColumnResource1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MajorArabicName" HeaderText="Major Arabic Name" UniqueName="MajorArabicName" FilterControlAltText="Filter MajorArabicName column" meta:resourcekey="GridBoundColumnResource2">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SpecializationName" HeaderText="Specialization Name" UniqueName="SpecializationName" FilterControlAltText="Filter SpecializationName column" meta:resourcekey="GridBoundColumnResource3">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SpecializationArabicName" HeaderText="Specialization Arabic Name" UniqueName="SpecializationArabicName" FilterControlAltText="Filter SpecializationArabicName column" meta:resourcekey="GridBoundColumnResource4">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar runat="server" ID="RadToolBar2" OnButtonClick="RadToolBar2_ButtonClick"
                                                Skin="Hay" meta:resourcekey="RadToolBar2Resource1" SingleClick="None">
                                                <Items>
                                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid2" ImageUrl="~/images/RadFilter.gif"
                                                        ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                                                </Items>
                                            </telerik:RadToolBar>
                                        </CommandItemTemplate>
                                    </MasterTableView>
                                    <SelectedItemStyle ForeColor="Maroon" />
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <br />
            <div class="table-responsive">
                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdMajors"
                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" >
                    <ContextMenu FeatureGroupID="rfContextMenu">
                    </ContextMenu>
                </telerik:RadFilter>
                <telerik:RadGrid ID="dgrdMajors" runat="server" AllowSorting="True" AllowPaging="True"
                    GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" PageSize="25"
                    ShowFooter="True" CellSpacing="0" meta:resourcekey="dgrdMajorsResource1">
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top"
                        DataKeyNames="MajorId">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="False"
                                UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource2">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource2" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="MajorName" HeaderText="Major Name" UniqueName="MajorName" FilterControlAltText="Filter MajorName column" meta:resourcekey="GridBoundColumnResource5">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MajorArabicName" HeaderText="Major Arabic Name" UniqueName="MajorArabicName" FilterControlAltText="Filter MajorArabicName column" meta:resourcekey="GridBoundColumnResource6">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                Skin="Hay" meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                                <Items>
                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid1" ImageUrl="~/images/RadFilter.gif"
                                        ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource2" />
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

