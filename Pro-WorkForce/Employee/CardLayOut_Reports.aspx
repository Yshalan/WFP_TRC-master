<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" CodeFile="CardLayOut_Reports.aspx.vb"
    Inherits="Employee_CardLayOut_Reports" Theme="SvTheme" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="pageHeader1" runat="server" HeaderText="Card Templates" />

    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblDesc" runat="server" Text="Description" CssClass="Profiletitletxt" meta:resourcekey="lblDescResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtDesc" runat="server" meta:resourcekey="txtDescResource1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqDesc" runat="server" ControlToValidate="txtDesc"
                Display="None" ErrorMessage="Please Enter Description" ValidationGroup="grpSave" meta:resourcekey="reqDescResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vceDesc" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="reqDesc" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblDescAr" runat="server" Text="Description Arabic" CssClass="Profiletitletxt" meta:resourcekey="lblDescArResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtDescAr" runat="server" meta:resourcekey="txtDescArResource1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqDescAr" runat="server" ControlToValidate="txtDescAr"
                Display="None" ErrorMessage="Please Enter Description Arabic" ValidationGroup="grpSave" meta:resourcekey="reqDescArResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vceDescAr" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="reqDescAr" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblFile" CssClass="Profiletitletxt" runat="server" Text="Template File" meta:resourcekey="lblFileResource1"></asp:Label>
        </div>
        <asp:UpdatePanel ID="update1" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />

            </Triggers>
            <ContentTemplate>
                <div id="trAttachedFile" runat="server" class="col-md-4">
                    <div class="input-group">
                        <span class="input-group-btn"><span class="btn btn-default" onclick="$(this).parent().find('input[type=file]').click();">Browse</span>
                            <asp:FileUpload ID="FileUpload1" runat="server"
                                name="uploaded_file" onchange="$(this).parent().parent().find('.form-control').html($(this).val().split(/[\\|/]/).pop());"
                                Style="display: none;" type="file" meta:resourcekey="FileUpload1Resource1" />
                        </span>
                        <span class="form-control"></span>
                    </div>
                    <div class="veiw_remove">
                        <a id="lnbLeaveFile" target="_blank" runat="server" visible="False">
                            <asp:Label ID="lblView" runat="server" Text="View" meta:resourcekey="lblViewResource1" />
                        </a>
                        <asp:LinkButton ID="lnbRemove" runat="server" Text="Remove" Visible="False" meta:resourcekey="lnbRemoveResource1" />
                        <asp:Label ID="lblNoAttachedFile" runat="server" Text="No Attached File" Visible="False" meta:resourcekey="lblNoAttachedFileResource1" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="row">

        <div id="divControls" runat="server" class="col-md-12 text-center">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button"
                ValidationGroup="grpSave" meta:resourcekey="btnSaveResource1" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClearResource1" />
            <asp:Button ID="btnDelete" OnClientClick="return ValidateDelete();" runat="server" Text="Delete" CssClass="button" meta:resourcekey="btnDeleteResource1" />
        </div>
    </div>
    <div class="row">
        <div class="table-responsive">
            <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
            <div class="filterDiv">
                <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdTemplate"
                    ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" >
                    <ContextMenu FeatureGroupID="rfContextMenu">
                    </ContextMenu>
                </telerik:RadFilter>
            </div>
            <telerik:RadGrid ID="dgrdTemplate" runat="server" AllowSorting="True" AllowPaging="True"
                PageSize="15" GridLines="None" ShowStatusBar="True" ShowFooter="True"
                OnItemCommand="dgrdTemplate_ItemCommand" CellSpacing="0" meta:resourcekey="dgrdTemplateResource1">
                <SelectedItemStyle ForeColor="Maroon" />
                <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="TemplateId">
                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="TemplateName" SortExpression="TemplateName" HeaderText="Description"
                            UniqueName="TemplateName" meta:resourcekey="GridBoundColumnResource1" />
                        <telerik:GridBoundColumn DataField="TemplateArabicName" SortExpression="TemplateArabicName"
                            HeaderText="Description Arabic" UniqueName="TemplateArabicName" meta:resourcekey="GridBoundColumnResource2" />
                        <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="Design File" meta:resourcekey="GridTemplateColumnResource2">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" CommandName="download"
                                    CommandArgument='<%# Eval("TemplateId") %>' meta:resourcekey="lnkDownloadResource1"></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="TemplateId" Visible="False" UniqueName="TemplateId" meta:resourcekey="GridBoundColumnResource3" />
                    </Columns>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                    <CommandItemTemplate>
                        <telerik:RadToolBar Skin="Hay" runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick" meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                            <Items>
                                <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                    ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                            </Items>
                        </telerik:RadToolBar>
                    </CommandItemTemplate>
                </MasterTableView>
                <GroupingSettings CaseSensitive="False" />
                <ClientSettings EnablePostBackOnRowClick="false" EnableRowHoverStyle="false">
                    <Selecting AllowRowSelect="false" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

