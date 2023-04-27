<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LKP.ascx.vb" Inherits="INV_UserControls_LKP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <h1>
        <asp:Label ID="Title" runat="server"></asp:Label></h1>
        <table>
            <asp:Panel ID="PnCode" runat="server" meta:resourcekey="PnCodeResource1">
                <tr>
                    <td>
                        <asp:Label ID="lblCode" runat="server" CssClass="Profiletitletxt"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCode" runat="server" CssClass="AIStextBoxCss"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="reqLKPCode" runat="server" ControlToValidate="txtCode"
                            Display="None" ErrorMessage="Please enter code" ValidationGroup="GroupLKP" meta:resourcekey="reqNationalCodeResource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ExtenderreqNationalCode" runat="server" CssClass="AISCustomCalloutStyle"
                            TargetControlID="reqLKPCode" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblNameResource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" CssClass="AIStextBoxCss" meta:resourcekey="txtNameResource1"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="reqLKPName" runat="server" ControlToValidate="txtName"
                        Display="None" ErrorMessage="Please enter english name" ValidationGroup="GroupLKP"
                        meta:resourcekey="reqNationalityNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderNationalityName" runat="server" TargetControlID="reqLKPName"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblArName" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblArNameResource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtArName" runat="server" CssClass="AIStextBoxCss" meta:resourcekey="txtArNameResource1"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="reqLKPArName" runat="server" ControlToValidate="txtArName"
                        Display="None" ErrorMessage="Please enter arabic name" ValidationGroup="GroupLKP"
                        meta:resourcekey="reqNationalityArNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderreqNationalityArName" runat="server" TargetControlID="reqLKPArName"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True" />
                </td>
            </tr>
            <asp:Panel ID="PnRemarks" runat="server" meta:resourcekey="PnRemarksResource1">
                <tr>
                    <td>
                        <asp:Label ID="lblRemarks" runat="server" CssClass="Profiletitletxt"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="AIStextBoxCss"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="PnArRemarks" runat="server" meta:resourcekey="PnArRemarksResource1">
                <tr>
                    <td>
                        <asp:Label ID="lblArRemarks" runat="server" CssClass="Profiletitletxt"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtArRemarks" runat="server" CssClass="AIStextBoxCss"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnOther1" runat="server" meta:resourcekey="pnOther1Resource1">
                <tr>
                    <td>
                        <asp:Label ID="lblOther1" runat="server" CssClass="Profiletitletxt"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOther1" runat="server" CssClass="AIStextBoxCss"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnOther2" runat="server" meta:resourcekey="pnOther2Resource1">
                <tr>
                    <td>
                        <asp:Label ID="lblOther2" runat="server" CssClass="Profiletitletxt"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOther2" runat="server" CssClass="AIStextBoxCss"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnOther3" runat="server" meta:resourcekey="pnOther3Resource1">
                <tr>
                    <td>
                        <asp:Label ID="lblOther3" runat="server" CssClass="Profiletitletxt"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOther3" runat="server" CssClass="AIStextBoxCss"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnOther4" runat="server" meta:resourcekey="pnOther4Resource1">
                <tr>
                    <td>
                        <asp:Label ID="lblOther4" runat="server" CssClass="Profiletitletxt"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOther4" runat="server" CssClass="AIStextBoxCss"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnOther5" runat="server" meta:resourcekey="pnOther5Resource1">
                <tr>
                    <td>
                        <asp:Label ID="lblOther5" runat="server" CssClass="Profiletitletxt"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOther5" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
            </asp:Panel>
            <tr>
            <td></td>
                <td>
                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" ValidationGroup="GroupLKP"
                        meta:resourcekey="btnSaveResource1" CssClass="button"></asp:LinkButton>
                    <asp:LinkButton ID="btnDelete" runat="server" CssClass="button"
                        Text="Delete" meta:resourcekey="btnDeleteResource1"></asp:LinkButton>
                    <asp:LinkButton ID="btnClear" runat="server" CssClass="button"
                        Text="Clear" meta:resourcekey="btnClearResource1"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <table class="table2">
        <tr>
                <td colspan="2">
                    <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdLKP"
                        ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid ID="dgrdLKP" runat="server" AllowPaging="True" Skin="Hay" AllowSorting="true"
                        GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" AutoGenerateColumns="False"
                        PageSize="25" OnItemCommand="dgrdLKP_ItemCommand"
                        OnPreRender="dgrdLKP_PreRender" ShowFooter="True" meta:resourcekey="dgrdVwEsubNationalityResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView CommandItemDisplay="Top" AllowMultiColumnSorting="true">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                    UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource1" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="lkpId" HeaderText="lkpId"
                                    SortExpression="lkpId" Visible="False" UniqueName="lkpId" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="lkpTypeId" HeaderText="lkpTypeId"
                                    SortExpression="lkpTypeId" Visible="False" UniqueName="lkpTypeId" />
                                <telerik:GridBoundColumn DataField="LKPCode" Resizable="False" HeaderText="Code" SortExpression="LKPCode"
                                    Visible="true" UniqueName="LKPCode" />
                                <telerik:GridBoundColumn DataField="LKPName" Resizable="False" HeaderText="English Name" SortExpression="LKPName"
                                    Visible="true" UniqueName="LKPName" />
                                <telerik:GridBoundColumn DataField="LKPNameAr" Resizable="False" HeaderText="Arabic Name" SortExpression="LKPNameAr"
                                    Visible="true" UniqueName="LKPNameAr" />
                                <telerik:GridBoundColumn DataField="Remarks" Resizable="False" HeaderText="English Remarks" SortExpression="LKPRemarks"
                                    Visible="False" UniqueName="LKPRemarks" />
                                <telerik:GridBoundColumn DataField="RemarksAr" HeaderText="Arabic Remarks"
                                    SortExpression="LKPArabicRemarks" Visible="False" UniqueName="LKPArabicRemarks" />
                                <telerik:GridBoundColumn DataField="Other1" HeaderText="Other1" Resizable="False" SortExpression="Other1"
                                    Visible="False" UniqueName="Other1" />
                                <telerik:GridBoundColumn DataField="Other2" HeaderText="Other2" Resizable="False" SortExpression="Other2"
                                    Visible="False" UniqueName="Other2" />
                                <telerik:GridBoundColumn DataField="Other3" HeaderText="Other3" Resizable="False" SortExpression="Other3"
                                    Visible="False" UniqueName="Other3" />
                                <telerik:GridBoundColumn DataField="Other4" HeaderText="Other4" Resizable="False" SortExpression="Other4"
                                    Visible="False" UniqueName="Other4" />
                                <telerik:GridBoundColumn DataField="Other5" HeaderText="Other5" Resizable="False" SortExpression="Other1"
                                    Visible="False" UniqueName="Other5" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="<%# GetFilterIcon() %>"
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
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
