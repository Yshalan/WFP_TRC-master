<%@ Page Language="VB" Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="OrgLevel.aspx.vb" Inherits="Admin_OrgLevel" Title="Organization Levels"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdatePanel ID="pnlEmpNationality" runat="server">
            <ContentTemplate>

                                <uc1:PageHeader ID="PageHeader1" runat="server" />
                                <div class="row" id="trCompany" runat="server">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text="Company" meta:resourcekey="Label1Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadComboBox ID="RadComboBoxCompany" runat="server" Width="210px" AutoPostBack="True"
                                            meta:resourcekey="RadComboBoxCompanyResource1">
                                        </telerik:RadComboBox>
                                    
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                            ErrorMessage="Please Select a Company" ControlToValidate="RadComboBoxCompany"
                                            InitialValue="--Please Select--" ValidationGroup="Grp1" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label2" runat="server" CssClass="Profiletitletxt" Text="Level Name"
                                            meta:resourcekey="Label2Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBoxLevelName" runat="server" Width="200px" meta:resourcekey="TextBoxLevelNameResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                                            ErrorMessage="Please Enter Level Name" ControlToValidate="TextBoxLevelName" ValidationGroup="Grp1"
                                            meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label3" runat="server" CssClass="Profiletitletxt" Text="Level Arabic Name"
                                            meta:resourcekey="Label3Resource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBoxLevelNameArabic" runat="server" Width="200px" meta:resourcekey="TextBoxLevelNameArabicResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                                            ErrorMessage="Please Enter Level Name" ControlToValidate="TextBoxLevelNameArabic"
                                            ValidationGroup="Grp1" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender"
                                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>

                    <div class="row">
                        <div class="col-md-12 text-center ">
                            <asp:Button ID="ibtnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="Grp1"
                                meta:resourcekey="ibtnSaveResource1" />
                            <asp:Button ID="ibtnRest" runat="server" Text="Clear" CssClass="button" CausesValidation="False"
                                meta:resourcekey="ibtnRestResource1" />
                        </div>
                    </div>
                   
                    <div class="row">

                            <div class="table-responsive">
                                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdLevel" ShowApplyButton="False"
                                    Skin="Hay" meta:resourcekey="RadFilter1Resource1" />
                            <telerik:RadGrid ID="dgrdLevel" runat="server" AllowSorting="True" AllowPaging="True"
                                Width="700px" PageSize="15"  GridLines="None" ShowStatusBar="True"
                                AllowMultiRowSelection="True" ShowFooter="True" meta:resourcekey="dgrdLevelResource1">
                                <SelectedItemStyle ForeColor="Maroon" />
                                <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="LevelId,FK_CompanyId">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="LevelId" HeaderText="LevelId" SortExpression="LevelId"
                                            AllowFiltering="False" Visible="False" meta:resourcekey="GridBoundColumnResource1"
                                            UniqueName="LevelId">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LevelName" HeaderText="Level Name" SortExpression="LevelName"
                                            Resizable="False" meta:resourcekey="GridBoundColumnResource2" UniqueName="LevelName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LevelArabicName" HeaderText="Arabic Name" SortExpression="LevelArabicName"
                                            Resizable="False" meta:resourcekey="GridBoundColumnResource3" UniqueName="LevelArabicName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FK_CompanyId" HeaderText="FK_CompanyId" Visible="False"
                                            SortExpression="FK_CompanyId" Resizable="False" meta:resourcekey="GridBoundColumnResource4"
                                            UniqueName="FK_CompanyId">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Enabledelete" HeaderText="Enabledelete" Visible="False"
                                            AllowFiltering="False" SortExpression="Enabledelete" Resizable="False" meta:resourcekey="GridBoundColumnResource5"
                                            UniqueName="Enabledelete">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn CommandName="Delete" ConfirmText="Are you sure ?" Text="Delete"
                                            UniqueName="column" meta:resourcekey="GridButtonColumnResource1">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                    <CommandItemTemplate>
                                        <telerik:RadToolBar runat="server" Skin="Hay" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
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
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
