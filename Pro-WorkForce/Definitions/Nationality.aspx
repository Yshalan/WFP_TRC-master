<%@ Page Title="Define Nationalities" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="Nationality.aspx.vb" Inherits="Emp_EMP_Nationality"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlEmpNationality" runat="server">
        <ContentTemplate>

            <uc1:PageHeader ID="UserCtrlNationality" HeaderText="Employee Nationality" runat="server" />

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblNationalityCode" runat="server" CssClass="Profiletitletxt" Text="Code"
                        meta:resourcekey="lblNationalityCodeResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtNationalityCode" CssClass="AIStextBoxCss" runat="server" meta:resourcekey="txtNationalityCodeResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqNationalCode" runat="server" ControlToValidate="txtNationalityCode"
                        Display="None" ErrorMessage="Please enter nationality code" ValidationGroup="GroupNationality"
                        meta:resourcekey="reqNationalCodeResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderreqNationalCode" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="reqNationalCode" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblNationalityName" runat="server" CssClass="Profiletitletxt" Text="English name"
                        meta:resourcekey="lblNationalityNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtNationalityName" runat="server" Width="200px" meta:resourcekey="txtNationalityNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqNationalityName" runat="server" ControlToValidate="txtNationalityName"
                        Display="None" ErrorMessage="Please enter nationality english name" ValidationGroup="GroupNationality"
                        meta:resourcekey="reqNationalityNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderNationalityName" runat="server" TargetControlID="reqNationalityName"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblNationalityArabicName" runat="server" CssClass="Profiletitletxt"
                        Text="Arabic name" meta:resourcekey="lblNationalityArabicNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtNationalityarabicName" runat="server" Width="200px" meta:resourcekey="txtNationalityarabicNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqNationalityArName" runat="server" ControlToValidate="txtNationalityarabicName"
                        Display="None" ErrorMessage="Please enter nationality arabic name" ValidationGroup="GroupNationality"
                        meta:resourcekey="reqNationalityArNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderreqNationalityArName" runat="server" TargetControlID="reqNationalityArName"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-8 text-center">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="GroupNationality"
                        meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnDelete" OnClientClick="return ValidateDelete();" runat="server" CausesValidation="False" CssClass="button"
                        Text="Delete" meta:resourcekey="btnDeleteResource1" />
                    <asp:Button ID="btnClear" runat="server" CausesValidation="False" CssClass="button"
                        Text="Clear" meta:resourcekey="btnClearResource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdVwEsubNationality"
                        ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid ID="dgrdVwEsubNationality" runat="server" AllowPaging="True" AllowSorting="true"
                        GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" AutoGenerateColumns="False" PageSize="15"
                        OnItemCommand="dgrdVwEsubNationality_ItemCommand" ShowFooter="True" meta:resourcekey="dgrdVwEsubNationalityResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="NationalityCode,NationalityId">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                    UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="NationalityId" HeaderText="NationalityId"
                                    SortExpression="NationalityId" Visible="False" meta:resourcekey="GridBoundColumnResource1"
                                    UniqueName="NationalityId" />
                                <telerik:GridBoundColumn DataField="NationalityCode" HeaderText="Nationality Code"
                                    SortExpression="NationalityCode" Resizable="False" meta:resourcekey="GridBoundColumnResource2"
                                    UniqueName="NationalityCode" />
                                <telerik:GridBoundColumn DataField="NationalityName" HeaderText="NationalityName"
                                    SortExpression="NationalityName" meta:resourcekey="GridBoundColumnResource3"
                                    UniqueName="NationalityName" />
                                <telerik:GridBoundColumn DataField="NationalityArabicName" HeaderText="Arabic Name"
                                    SortExpression="NationalityArabicName" Resizable="False" meta:resourcekey="GridBoundColumnResource4"
                                    UniqueName="NationalityArabicName" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                            ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdVwEsubNationality.ClientID %>");
            var masterTable = grid.get_masterTableView();
            var value = false;
            for (var i = 0; i < masterTable.get_dataItems().length; i++) {
                var gridItemElement = masterTable.get_dataItems()[i].findElement("chk");
                if (gridItemElement.checked) {
                    value = true;
                }
            }
            if (value === false) {
                ShowMessage("<%= Resources.Strings.ErrorDeleteRecourd %>");
            }
            return value;
        }
    </script>
</asp:Content>
