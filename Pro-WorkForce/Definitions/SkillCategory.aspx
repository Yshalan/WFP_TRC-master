<%@ Page Title="Skill Category" Language="VB" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="SkillCategory.aspx.vb" Inherits="Definitions_SkillCategory"
    Theme="SvTheme" Culture="Auto" UICulture="Auto" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
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
    <asp:UpdatePanel ID="pnlSkillCategory" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" HeaderText="Skill Category" runat="server" meta:resourcekey="PageHeader1Resource1" />
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab" meta:resourcekey="TabContainer1Resource1">
                <cc1:TabPanel ID="tabCategory" runat="server" HeaderText="Skill Category" meta:resourcekey="tabCategoryResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblCategoryName" runat="server" Text="Category Name" meta:resourcekey="lblCategoryNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtCategoryName" runat="server" meta:resourcekey="txtCategoryNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" ControlToValidate="txtCategoryName" Display="None"
                                    ErrorMessage="Please Enter Category Name" ValidationGroup="grpSave" meta:resourcekey="rfvCategoryNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceCategoryName" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvCategoryName" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblCategoryArabicName" runat="server" Text="Category Arabic Name" meta:resourcekey="lblCategoryArabicNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtCategoryArabicName" runat="server" meta:resourcekey="txtCategoryArabicNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCategoryArabicName" runat="server" ControlToValidate="txtCategoryArabicName" Display="None"
                                    ErrorMessage="Please Enter Category Arabic Name" ValidationGroup="grpSave" meta:resourcekey="rfvCategoryArabicNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceCategoryArabicName" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvCategoryArabicName" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblDisplayName" runat="server" Text="Category Display Name" meta:resourcekey="lblDisplayNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtDisplayName" runat="server"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName" Display="None"
                                    ErrorMessage="Please Enter Category Display Name" ValidationGroup="grpSave"
                                      meta:resourcekey="rfvDisplayNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceDisplayName" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvDisplayName" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2"><asp:Label ID="lblDisplayArabicName" runat="server" Text="Category Display Arabic Name" meta:resourcekey="lblDisplayArabicNameResource1"></asp:Label></div>
                            <div class="col-md-4"><asp:TextBox ID="txtDisplayArabicName" runat="server"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvDisplayArabicName" runat="server" ControlToValidate="txtDisplayArabicName" Display="None"
                                    ErrorMessage="Please Enter Category Display Arabic Name" ValidationGroup="grpSave"
                                     meta:resourcekey="rfvDisplayArabicNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceDisplayArabicName" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvDisplayArabicName" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2"></div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkHasDate" runat="server" Text ="Has Date" meta:resourcekey="chkHasDateResource1"/>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="grpSave" meta:resourcekey="btnSaveResource1" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False" meta:resourcekey="btnClearResource1" />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CausesValidation="False" meta:resourcekey="btnDeleteResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadFilter runat="server" ID="RadFilterCategory" FilterContainerID="dgrdSkillCategory"
                                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilterCategoryResource1">
                                    <ContextMenu FeatureGroupID="rfContextMenu">
                                    </ContextMenu>
                                </telerik:RadFilter>
                                <telerik:RadGrid ID="dgrdSkillCategory" runat="server" AllowSorting="True" AllowPaging="True"
                                    GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" PageSize="25"
                                    ShowFooter="True" CellSpacing="0" meta:resourcekey="dgrdSkillCategoryResource1">
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                    <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top"
                                        DataKeyNames="CategoryId">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="False" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1" UniqueName="TemplateColumn">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="CategoryName" SortExpression="CategoryName"
                                                HeaderText="Category Name" UniqueName="CategoryName" FilterControlAltText="Filter CategoryName column" meta:resourcekey="GridBoundColumnResource1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CategoryArabicName" SortExpression="CategoryArabicName"
                                                HeaderText="Category Arabic Name" UniqueName="CategoryArabicName" FilterControlAltText="Filter CategoryArabicName column" meta:resourcekey="GridBoundColumnResource2">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar runat="server" ID="RadToolBarCategory" OnButtonClick="RadToolBarCategory_ButtonClick"
                                                Skin="Hay" meta:resourcekey="RadToolBarCategoryResource1" SingleClick="None">
                                                <Items>
                                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGridCategory" ImageUrl="~/images/RadFilter.gif"
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
                <cc1:TabPanel ID="tabSkills" runat="server" HeaderText="Skills" Visible="False" meta:resourcekey="tabSkillsResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblSkillName" runat="server" Text="Skill Name" meta:resourcekey="lblSkillNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtSkillName" runat="server" meta:resourcekey="txtSkillNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSkillName" runat="server" ControlToValidate="txtSkillName" Display="None"
                                    ErrorMessage="Please Enter Skill Name" ValidationGroup="grpSaveSkill" meta:resourcekey="rfvSkillNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceSkillName" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvSkillName" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblSkillArabicName" runat="server" Text="Skill Arabic Name" meta:resourcekey="lblSkillArabicNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtSkillArabicName" runat="server" meta:resourcekey="txtSkillArabicNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSkillArabicName" runat="server" ControlToValidate="txtSkillArabicName" Display="None"
                                    ErrorMessage="Please Enter Skill Arabic Name" ValidationGroup="grpSaveSkill" meta:resourcekey="rfvSkillArabicNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceSkillArabicName" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvSkillArabicName" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblDesc_En" runat="server" Text="Skill Description" meta:resourcekey="lblDesc_EnResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtDesc_En" runat="server" TextMode="MultiLine" meta:resourcekey="txtDesc_EnResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblDesc_Ar" runat="server" Text="Skill Arabic Description" meta:resourcekey="lblDesc_ArResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtDesc_Ar" runat="server" TextMode="MultiLine" meta:resourcekey="txtDesc_ArResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSaveSkill" runat="server" Text="Save" ValidationGroup="grpSaveSkill" meta:resourcekey="btnSaveSkillResource1" />
                                <asp:Button ID="btnClearSkill" runat="server" Text="Clear" CausesValidation="False" meta:resourcekey="btnClearSkillResource1" />
                                <asp:Button ID="btnDeleteSkill" runat="server" Text="Delete" CausesValidation="False" meta:resourcekey="btnDeleteSkillResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadFilter runat="server" ID="RadFilterSkills" FilterContainerID="dgrdSkills"
                                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilterSkillsResource1">
                                    <ContextMenu FeatureGroupID="rfContextMenu">
                                    </ContextMenu>
                                </telerik:RadFilter>
                                <telerik:RadGrid ID="dgrdSkills" runat="server" AllowSorting="True" AllowPaging="True"
                                    GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" PageSize="25"
                                    ShowFooter="True" CellSpacing="0" meta:resourcekey="dgrdSkillsResource1">
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                    <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top"
                                        DataKeyNames="SkillId">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="False" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource2" UniqueName="TemplateColumn">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource2" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="SkillName" SortExpression="SkillName"
                                                HeaderText="Skill Name" UniqueName="SkillName" FilterControlAltText="Filter SkillName column" meta:resourcekey="GridBoundColumnResource3">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SkillArabicName" SortExpression="SkillArabicName"
                                                HeaderText="Skill Arabic Name" UniqueName="SkillArabicName" FilterControlAltText="Filter SkillArabicName column" meta:resourcekey="GridBoundColumnResource4">
                                            </telerik:GridBoundColumn>

                                        </Columns>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar runat="server" ID="RadToolBarSkills" OnButtonClick="RadToolBarSkills_ButtonClick"
                                                Skin="Hay" meta:resourcekey="RadToolBarSkillsResource1" SingleClick="None">
                                                <Items>
                                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGridSkills" ImageUrl="~/images/RadFilter.gif"
                                                        ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource2" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

