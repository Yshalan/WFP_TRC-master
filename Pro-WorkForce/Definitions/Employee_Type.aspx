<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Employee_Type.aspx.vb" Inherits="Definitions_Employee_Type" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlEmployeeType" runat="server">
        <ContentTemplate>
                   <uc1:PageHeader ID="PageHeader1" HeaderText="Employee Type" runat="server" />
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblTypeName" runat="server" CssClass="Profiletitletxt" Text="Type English Name"
                            meta:resourcekey="lblTypeNameResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtTypeName" runat="server" meta:resourcekey="txtTypeNameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqtxtTypeName" runat="server" ControlToValidate="txtTypeName"
                            Display="None" ErrorMessage="Please Enter Type English Name" ValidationGroup="GroupEmployeeType"
                            meta:resourcekey="reqtxtTypeNameResource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="vceTypeName" runat="server" TargetControlID="reqtxtTypeName"
                            CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                            Enabled="True">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblTypeNamear" runat="server" CssClass="Profiletitletxt" Text="Type Arabic Name"
                            meta:resourcekey="lblTypeNamearResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtTypeNamear" runat="server"  meta:resourcekey="txtTypeNamearResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqtxtTypeNamear" runat="server" ControlToValidate="txtTypeNamear"
                            Display="None" ErrorMessage="Please Enter Type Arabic Name" ValidationGroup="GroupEmployeeType"
                            meta:resourcekey="reqtxtTypeNamearResource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="vceTypeNamear" runat="server" TargetControlID="reqtxtTypeNamear"
                            CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                            Enabled="True">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="chkIsInternal" runat="server" AutoPostBack="True" Checked="True"
                            Text="Is Internal Employee"
                            meta:resourcekey="lblisinternalResource1" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblEmployeeNumberInitial" runat="server" CssClass="Profiletitletxt"
                            Text="Employee Number Initial(s)" meta:resourcekey="lblEmployeeNumberInitialResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEmployeeNumberInitial" runat="server" meta:resourcekey="txtEmployeeNumberInitialResource1"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="GroupEmployeeType"
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
                        <telerik:RadGrid ID="dgrdVwEmployeeType" runat="server" AllowPaging="True" 
                            AllowSorting="True" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                            AutoGenerateColumns="False" PageSize="25" OnItemCommand="dgrdVwEmployeeType_ItemCommand"
                            ShowFooter="True" meta:resourcekey="dgrdVwEmployeeTypeResource1">
                            <GroupingSettings CaseSensitive="False" />
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView CommandItemDisplay="Top" DataKeyNames="TypeName_En,EmployeeTypeId">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="EmployeeTypeId" HeaderText="EmployeeTypeId"
                                        SortExpression="EmployeeTypeId" Visible="False" UniqueName="EmployeeTypeId" meta:resourcekey="GridBoundColumnResource1" />
                                    <telerik:GridBoundColumn DataField="TypeName_En" HeaderText="Type English Name" SortExpression="TypeName_En"
                                        UniqueName="TypeName_En" meta:resourcekey="GridBoundColumnResource2" />
                                    <telerik:GridBoundColumn DataField="TypeName_Ar" HeaderText="Type Arabic Name" SortExpression="TypeName_Ar"
                                        UniqueName="TypeName_Ar" meta:resourcekey="GridBoundColumnResource3" />
                                    <telerik:GridBoundColumn DataField="IsInternaltype" HeaderText="Internal Employee"
                                        SortExpression="IsInternaltype" UniqueName="IsInternaltype" meta:resourcekey="GridBoundColumnResource4" />
                                </Columns>
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                <CommandItemTemplate>
                                    <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                        Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                        <Items>
                                            <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1"
                                                Owner="" />
                                        </Items>
                                    </telerik:RadToolBar>
                                </CommandItemTemplate>
                            </MasterTableView>
                            <SelectedItemStyle ForeColor="Maroon" />
                        </telerik:RadGrid>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdVwEmployeeType.ClientID %>");
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
