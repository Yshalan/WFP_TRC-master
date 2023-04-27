<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" CodeFile="SliderImages.aspx.vb" Inherits="Admin_SliderImages"
    Theme="SvTheme" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblSliderImage" runat="server" Text="Slider Image" meta:resourcekey="lblSliderImageResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <div class="input-group">
                        <span class="input-group-btn"><span class="btn btn-default" onclick="$(this).parent().find('input[type=file]').click();">Browse</span>
                            <asp:FileUpload ID="fuAttachFile" runat="server"
                                name="uploaded_file" onchange="$(this).parent().parent().find('.form-control').html($(this).val().split(/[\\|/]/).pop());"
                                Style="display: none;" type="file" meta:resourcekey="fuAttachFileResource1" />
                        </span><span class="form-control"></span>
                    </div>
                    <asp:Label ID="lblImageExt" runat="server" Text="Allowed File Extension (jpg, png, jpeg, gif, bmp)" SkinID="Remark"
                        meta:resourcekey="lblImageExtResource1"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvAttachFile" runat="server" ControlToValidate="fuAttachFile"
                        Display="None" ErrorMessage="Please Upload Image" ValidationGroup="grpSave" meta:resourcekey="rfvAttachFileResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceAttachFile" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvAttachFile" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblImageInfo" runat="server" Text="Recommended Image Size Is: 2560 * 1600" SkinID="Remark"
                        meta:resourcekey="lblImageInfoResource1"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblImageOrder" runat="server" Text="Image Order" meta:resourcekey="lblImageOrderResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadNumericTextBox ID="txtImageOrder" MinValue="1" MaxValue="9999999"
                        Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" DbValueFactor="1" LabelWidth="64px" meta:resourcekey="txtImageOrderResource1">
                        <NegativeStyle Resize="None" />
                        <NumberFormat DecimalDigits="0" GroupSeparator="" ZeroPattern="n" />
                        <EmptyMessageStyle Resize="None" />
                        <ReadOnlyStyle Resize="None" />
                        <FocusedStyle Resize="None" />
                        <DisabledStyle Resize="None" />
                        <InvalidStyle Resize="None" />
                        <HoveredStyle Resize="None" />
                        <EnabledStyle Resize="None" />
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="rfvImageOrder" runat="server" ControlToValidate="txtImageOrder"
                        Display="None" ErrorMessage="Please enter Image Order" ValidationGroup="grpSave" meta:resourcekey="rfvImageOrderResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceImageOrder" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvImageOrder" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="grpSave" meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" meta:resourcekey="btnDeleteResource1" />
                </div>
            </div>
            <div class="table-responsive">
                <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                <div class="filterDiv">
                    <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrdImages"
                        ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1">
                        <ContextMenu FeatureGroupID="rfContextMenu">
                        </ContextMenu>
                    </telerik:RadFilter>
                </div>
                <telerik:RadGrid runat="server" ID="dgrdImages" AutoGenerateColumns="False" PageSize="15"
                    AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" meta:resourcekey="dgrdImagesResource1">
                    <MasterTableView CommandItemDisplay="Top" AllowFilteringByColumn="False" DataKeyNames="ImageId">
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick" meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                                <Items>
                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                        ImageUrl="~/images/RadFilter.gif" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="False" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1" UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="ImageId" HeaderText="ImageId" DataType="System.Int32"
                                AllowFiltering="False" SortExpression="ImageId" Visible="False" FilterControlAltText="Filter ImageId column" meta:resourcekey="GridBoundColumnResource1" UniqueName="ImageId">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ImageName" HeaderText="Image Name"
                                AllowFiltering="False" SortExpression="ImageName" FilterControlAltText="Filter ImageName column" meta:resourcekey="GridBoundColumnResource2" UniqueName="ImageName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ImageOrder" HeaderText="Image Order"
                                AllowFiltering="False" SortExpression="ImageOrder" FilterControlAltText="Filter ImageOrder column" meta:resourcekey="GridBoundColumnResource3" UniqueName="ImageOrder">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Image" ItemStyle-HorizontalAlign="Justify">
                                <ItemTemplate>
                                    <asp:Image Height="100" Width="200" ID="imgURL" runat="server" ImageUrl='<%# Bind("ImageName", "~/Images/SliderImages/{0}")%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                        EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

