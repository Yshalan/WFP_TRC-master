<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Authorities.aspx.vb" Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master"
Inherits="Definitions_Authorities" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script language="javascript" type="text/javascript">
    function confirmDelete(dgrdAuthority) {
        var lang = '<%= MsgLang %>'
        var TargetBaseControl = null;
        try {
            //get target base control.
            TargetBaseControl = document.getElementById(dgrdAuthority);

        }
        catch (err) {
            TargetBaseControl = null;
        }

        if (TargetBaseControl == null) {
            if (lang == 'en') {
                ShowMessage('No data')
            }
            else {
                ShowMessage('لا يوجد بيانات')
            }
            return false;
        }

        //get all the control of the type INPUT in the base control.
        var Inputs = TargetBaseControl.getElementsByTagName("input");
        var TargetChildControl = "chk";
        for (var n = 0; n < Inputs.length; ++n) {
            if (Inputs[n].type == 'checkbox' && Inputs[n].checked && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0) {
                if (lang == 'en') {
                    return confirm('Are you sure you want to delete?');
                }
                else {
                    return confirm('هل أنت متأكد من الحذف؟');
                }
            }

        }
        if (lang == 'en') {
            ShowMessage('Please select from the list');
        }
        else {
            ShowMessage('الرجاء الاختيار من القائمة');
        }
        return false;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlAuthority" runat="server">
        <ContentTemplate>
             <uc1:PageHeader ID="UserCtrlAuthorities" HeaderText="Define Authorities" runat="server" />

                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblAuthorityCode" runat="server" CssClass="Profiletitletxt" Text="Authority Code"
                                            meta:resourcekey="lblAuthorityCodeResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtAuthorityCode" CssClass="AIStextBoxCss" runat="server" meta:resourcekey="txtAuthorityCodeResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqNationalCode" runat="server" ControlToValidate="txtAuthorityCode"
                                            Display="None" ErrorMessage="Please Enter Authority Code" ValidationGroup="GroupAuthority"
                                            meta:resourcekey="reqNationalCodeResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ExtenderreqNationalCode" runat="server" CssClass="AISCustomCalloutStyle"
                                            TargetControlID="reqNationalCode" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblAuthorityName" runat="server" CssClass="Profiletitletxt" Text="Authority English Name"
                                            meta:resourcekey="lblAuthorityNameResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtAuthorityName" runat="server"  meta:resourcekey="txtAuthorityNameResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqAuthorityName" runat="server" ControlToValidate="txtAuthorityName"
                                            Display="None" ErrorMessage="Please Enter Authority English Name" ValidationGroup="GroupAuthority"
                                            meta:resourcekey="reqAuthorityNameResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ExtenderAuthorityName" runat="server" TargetControlID="reqAuthorityName"
                                            CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblAuthorityArabicName" runat="server" CssClass="Profiletitletxt"
                                            Text="Authority Arabic Name" meta:resourcekey="lblAuthorityArabicNameResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtAuthorityarabicName" runat="server"  meta:resourcekey="txtAuthorityarabicNameResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqAuthorityArName" runat="server" ControlToValidate="txtAuthorityarabicName"
                                            Display="None" ErrorMessage="Please Enter Authority Arabic Name" ValidationGroup="GroupAuthority"
                                            meta:resourcekey="reqAuthorityArNameResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ExtenderreqAuthorityArName" runat="server" TargetControlID="reqAuthorityArName"
                                            CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True" />
                                    </div>
                                </div>
                                <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblActive" runat="server" CssClass="Profiletitletxt" Text="Active"
                                        meta:resourcekey="lblActiveResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:CheckBox ID="chkActive" runat="server" Text="&nbsp;" />
                                </div>
                            </div>
                                <div class="row">
                                    <div class="col-md-12 text-center ">
                                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="GroupAuthority"
                                            meta:resourcekey="btnSaveResource1" />
                                        <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button"
                                            Text="Delete" meta:resourcekey="btnDeleteResource1" />
                                        <asp:Button ID="btnClear" runat="server" CausesValidation="False" CssClass="button"
                                            Text="Clear" meta:resourcekey="btnClearResource1" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="table-responsive ">
                                        <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdAuthority"
                                            ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" >
                                            <ContextMenu FeatureGroupID="rfContextMenu">
                                            </ContextMenu>
                                        </telerik:RadFilter>
                                        <telerik:RadGrid ID="dgrdAuthority" runat="server" AllowPaging="True" 
                                            AllowSorting="True"
                                            GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" 
                                            AutoGenerateColumns="False" PageSize="15"
                                            OnItemCommand="dgrdAuthority_ItemCommand" ShowFooter="True" 
                                            meta:resourcekey="dgrdAuthorityResource1" CellSpacing="0">
                                            <SelectedItemStyle ForeColor="Maroon" />
                                            <MasterTableView CommandItemDisplay="Top" DataKeyNames="AuthorityCode,AuthorityId,Active">
                                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                                        UniqueName="TemplateColumn" 
                                                        FilterControlAltText="Filter TemplateColumn column">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource1" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="AuthorityId" HeaderText="AuthorityId"
                                                        SortExpression="AuthorityId" Visible="False" meta:resourcekey="GridBoundColumnResource1"
                                                        UniqueName="AuthorityId" 
                                                        FilterControlAltText="Filter AuthorityId column" />
                                                    <telerik:GridBoundColumn DataField="AuthorityCode" HeaderText="Authority Code"
                                                        SortExpression="AuthorityCode" Resizable="False" meta:resourcekey="GridBoundColumnResource2"
                                                        UniqueName="AuthorityCode" 
                                                        FilterControlAltText="Filter AuthorityCode column" />
                                                    <telerik:GridBoundColumn DataField="AuthorityName" HeaderText="AuthorityName"
                                                        SortExpression="AuthorityName" meta:resourcekey="GridBoundColumnResource3"
                                                        UniqueName="AuthorityName" 
                                                        FilterControlAltText="Filter AuthorityName column" />
                                                    <telerik:GridBoundColumn DataField="AuthorityArabicName" HeaderText="Arabic Name"
                                                        SortExpression="AuthorityArabicName" Resizable="False" meta:resourcekey="GridBoundColumnResource4"
                                                        UniqueName="AuthorityArabicName" 
                                                        FilterControlAltText="Filter AuthorityArabicName column" />
                                                        <telerik:GridBoundColumn DataField="Active" HeaderText="Is Active"
                                                        SortExpression="Active" Resizable="False" meta:resourcekey="GridBoundColumnResource5"
                                                        UniqueName="Active" FilterControlAltText="Filter Active column" />
                                                        
                                                </Columns>
                                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                <CommandItemTemplate>
                                                    <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                                        Skin="Hay" meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                                                        <Items>
                                                            <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                                ImagePosition="Right" runat="server" 
                                                                meta:resourcekey="RadToolBarButtonResource1" Owner="" />
                                                        </Items>
                                                    </telerik:RadToolBar>
                                                </CommandItemTemplate>
                                            </MasterTableView>
                                            <GroupingSettings CaseSensitive="False" />
                                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                                EnableRowHoverStyle="True">
                                                <Selecting AllowRowSelect="True" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
